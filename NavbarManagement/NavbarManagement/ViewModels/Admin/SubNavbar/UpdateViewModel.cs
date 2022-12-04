using System;
namespace NavbarManagement.ViewModels.Admin.SubNavbar
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }
        public int NavbarId { get; set; }
        public List<ListItemViewModel>? Navbar { get; set; }
    }
}