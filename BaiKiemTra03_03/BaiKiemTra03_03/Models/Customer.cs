using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace BaiKiemTra03_03.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<Contract> Contracts { get; set; }
    }
}
