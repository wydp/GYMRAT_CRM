using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.domain.Entities
{
    public class Device
    {
        public int DeviceId { get; set; }
        public int CompanyId { get; set; }
        public string DeviceCode { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public Company? Company { get; set; }
    }
}
