using System;
using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavbarManagement.Database.Models;
using NavbarManagement.ViewModels.Admin.Navbar;

namespace NavbarManagement.Controllers.Admin
{
    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }

        #region List

        [HttpGet("list", Name = "admin-navbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Navbars
                .Select(n => new ListItemViewModel(
                   n.Id, n.Name, n.Order, n.IsBold, n.IsHeader, n.IsFooter)).ToListAsync();

            return View("~/Views/Admin/Navbar/List.cshtml", model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-navbar-add")]
        public IActionResult Add()
        {
            return View("~/Views/Admin/Navbar/Add.cshtml");
        }

        [HttpPost("add", Name = "admin-navbar-add")]
        public IActionResult Add(AddViewModel model)
        {
            if (!model.IsFooter && !model.IsHeader)
            {
                ModelState.AddModelError(String.Empty, "Choose either Header or Footer!");
                return View("~/Views/Admin/Navbar/Add.cshtml", model);
            }

            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Navbar/Add.cshtml", model);
            }

            var newNavModel = new Navbar()
            {
                Name = model.Name,
                URL = model.URL,
                Order = model.Order,
                IsBold = model.IsBold,
                IsHeader = model.IsHeader,
                IsFooter = model.IsFooter,
            };

            _dataContext.Navbars.Add(newNavModel);
            _dataContext.SaveChanges();

            return RedirectToRoute("admin-navbar-list");
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var model = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (model is null)
            {
                return NotFound();
            }

            var newModel = new UpdateViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                URL = model.URL,
                Order = model.Order,
                IsBold = model.IsBold,
                IsHeader = model.IsHeader,
                IsFooter = model.IsFooter,
            };


            return View("~/Views/Admin/Navbar/Update.cshtml", newModel);
        }

        [HttpPost("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            if (!model.IsFooter && !model.IsHeader)
            {
                ModelState.AddModelError(String.Empty, "You Must Choose Header or Footer");
                return View("~/Views/Admin/Navbar/Update.cshtml", model);
            }

            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Navbar/Update.cshtml", model);
            }

            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);

            if (navbar is null)
            {
                return NotFound();
            }

            navbar.Name = model.Name;
            navbar.URL = model.URL;
            navbar.Order = model.Order;
            navbar.IsBold = model.IsBold;
            navbar.IsHeader = model.IsHeader;
            navbar.IsFooter = model.IsFooter;

            _dataContext.SaveChanges();

            return RedirectToRoute("admin-navbar-list");
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-navbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var model = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == id);
            if (model is null)
            {
                return NotFound();
            }

            _dataContext.Navbars.Remove(model);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }

        #endregion
    }
}