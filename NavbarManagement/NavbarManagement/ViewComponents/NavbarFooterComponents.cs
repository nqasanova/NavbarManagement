using System;
using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NavbarManagement.ViewComponents
{
    [ViewComponent(Name = "NavbarFooter")]
    public class NavbarFooterComponents : ViewComponent
    {
        private readonly DataContext _datacontext;
        public NavbarFooterComponents(DataContext datacontext)
        {
            _datacontext = datacontext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _datacontext.Navbars
                .Include(n => n.SubNavbars.OrderBy(sn => sn.Order))
                .Where(n => n.IsFooter)
                .OrderBy(n => n.Order)

                .ToList();

            return View("~/Views/Shared/Components/NavbarFooter/Index.cshtml", model);
        }
    }
}