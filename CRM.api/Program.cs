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

app.MapPost("/tenant/{companyId:int}/membershipsales", async (
    int companyId,
    MembershipSale sale,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);

    var customerExists = await tenantDb.Customers.AnyAsync(c => c.CustomerId == sale.CustomerId);
    var planExists = await tenantDb.MembershipPlans.AnyAsync(p => p.MembershipPlanId == sale.MembershipPlanId);
    if (!customerExists || !planExists)
        return Results.BadRequest(new { message = "The specified customer or plan does not exist." });

    tenantDb.MembershipSales.Add(sale);
    await tenantDb.SaveChangesAsync();
    return Results.Created($"/tenant/{companyId}/membershipsales/{sale.MembershipSaleId}", sale);
});

app.MapGet("/tenant/{companyId:int}/membershipsales", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);
    var sales = await tenantDb.MembershipSales
        .Include(x => x.Customer)
        .Include(x => x.MembershipPlan)
        .AsNoTracking()
        .OrderBy(x => x.MembershipSaleId)
        .ToListAsync();
    return Results.Ok(sales);
});

app.MapPost("/tenant/{companyId:int}/membershipsales/seed", async (
    int companyId,
    int count,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);

    var customerIds = await tenantDb.Customers.Where(c => c.IsActive).Select(c => c.CustomerId).ToListAsync();
    var plans = await tenantDb.MembershipPlans.Where(p => p.IsActive).ToListAsync();

    if (customerIds.Count == 0 || plans.Count == 0)
        return Results.BadRequest(new { message = "Create at least one active Customer and one active MembershipPlan before seeding sales." });

    var random = new Random();
    var sales = new List<MembershipSale>();

    for (int i = 0; i < count; i++)
    {
        var plan = plans[random.Next(plans.Count)];
        var daysAgo = random.Next(0, 180);

        sales.Add(new MembershipSale
        {
            CustomerId = customerIds[random.Next(customerIds.Count)],
            MembershipPlanId = plan.MembershipPlanId,
            SaleDate = DateTime.UtcNow.AddDays(-daysAgo),
            AmountPaid = plan.Price,
            IsActive = true,
        });
    }

    tenantDb.MembershipSales.AddRange(sales);
    await tenantDb.SaveChangesAsync();
    return Results.Ok(new { message = $"Seeded {count} membership sales.", count });
});

// Add this to Program.cs, right after the MembershipSale endpoints you just added,
// and before app.Run();
//
// DEV-ONLY — wipes existing Customers/MembershipPlans/Inquiries/Feedback/
// MembershipSales for this tenant, then reseeds fresh realistic-looking data.
// Delete this endpoint before submitting/shipping — it is destructive by design.

app.MapPost("/tenant/{companyId:int}/dev/reset-and-seed", async (
    int companyId,
    ITenantDbContextFactory tenantFactory) =>
{
    await using var tenantDb = await tenantFactory.CreateAsync(companyId);

    // 1. Delete in FK-safe order: children before parents.
    tenantDb.MembershipSales.RemoveRange(tenantDb.MembershipSales);
    tenantDb.Inquiries.RemoveRange(tenantDb.Inquiries);
    tenantDb.Feedbacks.RemoveRange(tenantDb.Feedbacks);
    await tenantDb.SaveChangesAsync();

    tenantDb.Customers.RemoveRange(tenantDb.Customers);
    tenantDb.MembershipPlans.RemoveRange(tenantDb.MembershipPlans);
    await tenantDb.SaveChangesAsync();

    var random = new Random();

    // 2. Seed 5 realistic gym membership plans.
    var plans = new List<MembershipPlan>
    {
        new() { PlanCode = "BASIC-01", PlanName = "Basic", Description = "Gym floor access only", Price = 799m, DurationInDays = 30, IsActive = true },
        new() { PlanCode = "STD-01", PlanName = "Standard", Description = "Gym floor + group classes", Price = 1299m, DurationInDays = 30, IsActive = true },
        new() { PlanCode = "GOLD-01", PlanName = "Gold", Description = "Standard + sauna and locker", Price = 1899m, DurationInDays = 30, IsActive = true },
        new() { PlanCode = "PREM-01", PlanName = "Premium", Description = "Gold + 2 personal training sessions/month", Price = 2999m, DurationInDays = 30, IsActive = true },
        new() { PlanCode = "VIP-01", PlanName = "Platinum", Description = "All access + unlimited personal training", Price = 4999m, DurationInDays = 30, IsActive = true },
    };
    tenantDb.MembershipPlans.AddRange(plans);
    await tenantDb.SaveChangesAsync(); // save now so plans get real IDs before sales reference them

    // 3. Seed 25 realistic-looking customers.
    string[] firstNames = { "Juan", "Maria", "Jose", "Ana", "Pedro", "Carmen", "Miguel", "Rosa",
        "Antonio", "Elena", "Carlos", "Sofia", "Luis", "Isabel", "Mark", "Grace", "James",
        "Nicole", "Paul", "Angela", "John", "Andrea", "Daniel", "Kristine", "Ryan" };
    string[] lastNames = { "Santos", "Reyes", "Cruz", "Bautista", "Garcia", "Torres", "Flores",
        "Ramos", "Villanueva", "Castro", "Mendoza", "Aquino", "Gonzales", "Rivera", "Diaz",
        "Fernandez", "Perez", "Lopez", "Dela Cruz", "Ramirez", "Domingo", "Salazar", "Navarro",
        "Pascual", "Ocampo" };

    var customers = new List<Customer>();
    for (int i = 0; i < 25; i++)
    {
        var first = firstNames[i % firstNames.Length];
        var last = lastNames[i % lastNames.Length];
        customers.Add(new Customer
        {
            CustomerCode = $"CUST-{(i + 1):D4}",
            CustomerName = $"{first} {last}",
            ContactNumber = $"09{random.Next(100000000, 999999999)}",
            EmailAddress = $"{first.ToLower()}.{last.ToLower().Replace(" ", "")}@example.com",
            Address = $"{random.Next(1, 999)} Sample St., Davao City",
            IsActive = true,
        });
    }
    tenantDb.Customers.AddRange(customers);
    await tenantDb.SaveChangesAsync(); // save now so customers get real IDs before sales reference them

    // 4. Seed 200 membership sales spread across the last 6 months,
    //    referencing the customers/plans just created.
    var sales = new List<MembershipSale>();
    for (int i = 0; i < 200; i++)
    {
        var plan = plans[random.Next(plans.Count)];
        var customer = customers[random.Next(customers.Count)];
        var daysAgo = random.Next(0, 180);

        sales.Add(new MembershipSale
        {
            CustomerId = customer.CustomerId,
            MembershipPlanId = plan.MembershipPlanId,
            SaleDate = DateTime.UtcNow.AddDays(-daysAgo),
            AmountPaid = plan.Price,
            IsActive = true,
        });
    }
    tenantDb.MembershipSales.AddRange(sales);
    await tenantDb.SaveChangesAsync();

    return Results.Ok(new
    {
        message = "Reset and reseed complete.",
        plansCreated = plans.Count,
        customersCreated = customers.Count,
        salesCreated = sales.Count,
    });
});

app.Run();