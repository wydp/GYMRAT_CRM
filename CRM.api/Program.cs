using CRM.domain.Entities;
using CRM.infrastructure.Data;
using Microsoft.Data.SqlClient;
using CRM.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddDbContext<MasterErpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MasterErp")));

builder.Services.AddDbContext<TenantCrmDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TenantErp")));

builder.Services.AddScoped<ITenantDatabaseResolver, TenantDatabaseResolver>();
builder.Services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/companies", async (
    Company company,
    MasterErpDbContext db) =>
{
    db.Companies.Add(company);
    await db.SaveChangesAsync();
    return Results.Created($"/companies/{company.CompanyId}", company);
});

app.MapPost("/devices", async (
    Device device,
    MasterErpDbContext db) =>
{
    db.Devices.Add(device);
    await db.SaveChangesAsync();
    return Results.Created($"/devices/{device.DeviceId}", device);
});

app.MapPost("/company-databases", async (
    CompanyDatabase companyDatabase,
    MasterErpDbContext db) =>
{
    db.CompanyDatabases.Add(companyDatabase);
    await db.SaveChangesAsync();
    return Results.Created(
        $"/company-databases/{companyDatabase.CompanyDatabaseId}",
        companyDatabase);
});

app.MapGet("/test-tenant/{companyId:int}", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var productCount = await tenantDb.Products.CountAsync();
    return Results.Ok(new { companyId, productCount });
});

app.MapPost("/tenant/{companyId:int}/products", async (
    int companyId,
    Product product,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    tenantDb.Products.Add(product);
    await tenantDb.SaveChangesAsync();
    return Results.Created($"/tenant/{companyId}/products/{product.ProductId}", product);
});

app.MapGet("/tenant/{companyId:int}/products", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var products = await tenantDb.Products
        .AsNoTracking()
        .OrderBy(x => x.ProductId)
        .ToListAsync();
    return Results.Ok(products);
});

app.MapPost("/tenant/{companyId:int}/customers", async (
    int companyId,
    Customer customer,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    tenantDb.Customers.Add(customer);

    try
    {
        await tenantDb.SaveChangesAsync();
    }
    catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
    {
        return Results.Conflict(new { message = "A customer with this code already exists." });
    }

    return Results.Created($"/tenant/{companyId}/customers/{customer.CustomerId}", customer);
});

app.MapGet("/tenant/{companyId:int}/customers", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var customers = await tenantDb.Customers
        .AsNoTracking()
        .OrderBy(x => x.CustomerId)
        .ToListAsync();
    return Results.Ok(customers);
});

app.MapPut("/tenant/{companyId:int}/customers/{customerId:int}", async (
    int companyId,
    int customerId,
    Customer updated,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var existing = await tenantDb.Customers.FindAsync(customerId);
    if (existing is null) return Results.NotFound();

    existing.CustomerCode = updated.CustomerCode;
    existing.CustomerName = updated.CustomerName;
    existing.ContactNumber = updated.ContactNumber;
    existing.EmailAddress = updated.EmailAddress;
    existing.Address = updated.Address;
    existing.IsActive = updated.IsActive;

    try
    {
        await tenantDb.SaveChangesAsync();
    }
    catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
    {
        return Results.Conflict(new { message = "A customer with this code already exists." });
    }

    return Results.Ok(existing);
});

app.MapDelete("/tenant/{companyId:int}/customers/{customerId:int}", async (
    int companyId,
    int customerId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var existing = await tenantDb.Customers.FindAsync(customerId);
    if (existing is null) return Results.NotFound();

    existing.IsActive = false;
    await tenantDb.SaveChangesAsync();
    return Results.Ok(existing);
});

app.MapPost("/tenant/{companyId:int}/membershipplans", async (
    int companyId,
    MembershipPlan plan,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    tenantDb.MembershipPlans.Add(plan);

    try
    {
        await tenantDb.SaveChangesAsync();
    }
    catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
    {
        return Results.Conflict(new { message = "A membership plan with this code already exists." });
    }

    return Results.Created($"/tenant/{companyId}/membershipplans/{plan.MembershipPlanId}", plan);
});

app.MapGet("/tenant/{companyId:int}/membershipplans", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var plans = await tenantDb.MembershipPlans
        .AsNoTracking()
        .OrderBy(x => x.MembershipPlanId)
        .ToListAsync();
    return Results.Ok(plans);
});

app.MapPut("/tenant/{companyId:int}/membershipplans/{planId:int}", async (
    int companyId,
    int planId,
    MembershipPlan updated,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var existing = await tenantDb.MembershipPlans.FindAsync(planId);
    if (existing is null) return Results.NotFound();

    existing.PlanCode = updated.PlanCode;
    existing.PlanName = updated.PlanName;
    existing.Description = updated.Description;
    existing.Price = updated.Price;
    existing.DurationInDays = updated.DurationInDays;
    existing.IsActive = updated.IsActive;

    try
    {
        await tenantDb.SaveChangesAsync();
    }
    catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
    {
        return Results.Conflict(new { message = "A membership plan with this code already exists." });
    }

    return Results.Ok(existing);
});

app.MapDelete("/tenant/{companyId:int}/membershipplans/{planId:int}", async (
    int companyId,
    int planId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var existing = await tenantDb.MembershipPlans.FindAsync(planId);
    if (existing is null) return Results.NotFound();

    existing.IsActive = false;
    await tenantDb.SaveChangesAsync();
    return Results.Ok(existing);
});

app.MapPost("/tenant/{companyId:int}/inquiries", async (
    int companyId,
    Inquiry inquiry,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);

    var customerExists = await tenantDb.Customers.AnyAsync(c => c.CustomerId == inquiry.CustomerId);
    if (!customerExists)
        return Results.BadRequest(new { message = "The specified customer does not exist." });

    tenantDb.Inquiries.Add(inquiry);
    await tenantDb.SaveChangesAsync();
    return Results.Created($"/tenant/{companyId}/inquiries/{inquiry.InquiryId}", inquiry);
});

app.MapGet("/tenant/{companyId:int}/inquiries", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var inquiries = await tenantDb.Inquiries
        .Include(x => x.Customer)
        .AsNoTracking()
        .OrderBy(x => x.InquiryId)
        .ToListAsync();
    return Results.Ok(inquiries);
});

app.MapPut("/tenant/{companyId:int}/inquiries/{inquiryId:int}/status", async (
    int companyId,
    int inquiryId,
    UpdateStatusRequest request,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var existing = await tenantDb.Inquiries.FindAsync(inquiryId);
    if (existing is null) return Results.NotFound();

    existing.Status = request.Status;
    await tenantDb.SaveChangesAsync();
    return Results.Ok(existing);
});

app.MapDelete("/tenant/{companyId:int}/inquiries/{inquiryId:int}", async (
    int companyId,
    int inquiryId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var existing = await tenantDb.Inquiries.FindAsync(inquiryId);
    if (existing is null) return Results.NotFound();

    existing.IsActive = false;
    await tenantDb.SaveChangesAsync();
    return Results.Ok(existing);
});

app.MapPost("/tenant/{companyId:int}/feedbacks", async (
    int companyId,
    Feedback feedback,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);

    var customerExists = await tenantDb.Customers.AnyAsync(c => c.CustomerId == feedback.CustomerId);
    if (!customerExists)
        return Results.BadRequest(new { message = "The specified customer does not exist." });

    tenantDb.Feedbacks.Add(feedback);
    await tenantDb.SaveChangesAsync();
    return Results.Created($"/tenant/{companyId}/feedbacks/{feedback.FeedbackId}", feedback);
});

app.MapGet("/tenant/{companyId:int}/feedbacks", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var feedbacks = await tenantDb.Feedbacks
        .Include(x => x.Customer)
        .AsNoTracking()
        .OrderBy(x => x.FeedbackId)
        .ToListAsync();
    return Results.Ok(feedbacks);
});

app.MapPut("/tenant/{companyId:int}/feedbacks/{feedbackId:int}/status", async (
    int companyId,
    int feedbackId,
    UpdateStatusRequest request,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var existing = await tenantDb.Feedbacks.FindAsync(feedbackId);
    if (existing is null) return Results.NotFound();

    existing.Status = request.Status;
    await tenantDb.SaveChangesAsync();
    return Results.Ok(existing);
});

app.MapDelete("/tenant/{companyId:int}/feedbacks/{feedbackId:int}", async (
    int companyId,
    int feedbackId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var existing = await tenantDb.Feedbacks.FindAsync(feedbackId);
    if (existing is null) return Results.NotFound();

    existing.IsActive = false;
    await tenantDb.SaveChangesAsync();
    return Results.Ok(existing);
});

app.Run();