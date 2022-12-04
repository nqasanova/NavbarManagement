using System;
using DemoApplication.Database.Models.Common;

namespace NavbarManagement.Database.Models
{
    public class Navbar : BaseEntity
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }
        public List<SubNavbar> SubNavbars { get; set; }
    }
}