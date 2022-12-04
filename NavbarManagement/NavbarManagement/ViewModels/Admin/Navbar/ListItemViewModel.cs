using System;
namespace NavbarManagement.ViewModels.Admin.Navbar
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }

        public ListItemViewModel(int id, string name, int order, bool isBold, bool isHeader, bool isFooter)
        {
            Id = id;
            Name = name;
            Order = order;
            IsBold = isBold;
            IsHeader = isHeader;
            IsFooter = isFooter;
        }
    }
}