using System;
using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NavbarManagement.ViewComponents
{
    public class NavbarHeaderViewComponents
    {
        [ViewComponent(Name = "NavbarHeader")]
        public class NavbarHeaderViewComponent : ViewComponent
        {
            private readonly DataContext _datacontext;
            public NavbarHeaderViewComponent(DataContext datacontext)
            {
                _datacontext = datacontext;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                var model = _datacontext.Navbars
                    .Include(n => n.SubNavbars.OrderBy(sn => sn.Order))
                    .Where(n => n.IsHeader)
                    .OrderBy(n => n.Order)

                    .ToList();
                return View("~/Views/Shared/Components/NavbarHeader/Index.cshtml", model);
            }
        }
    }
}