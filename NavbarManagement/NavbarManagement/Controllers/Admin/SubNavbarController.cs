using System;
using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavbarManagement.Database.Models;
using NavbarManagement.ViewModels.Admin.SubNavbar;

namespace NavbarManagement.Controllers.Admin
{

    [Route("admin/subnavbar")]
    public class SubNavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public SubNavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }

        #region List

        [HttpGet("list", Name = "admin-subnavbar-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.SubNavbars.Select(
                sn => new ListItemViewModel(sn.Id, sn.Name, sn.URL, sn.Order, sn.Navbar.Name)
                ).ToListAsync();

            return View("~/Views/Admin/SubNavbar/List.cshtml", model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-subnavbar-add")]
        public IActionResult Add()
        {
            var model = new AddViewModel
            {
                Navbar = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToList()
            };

            return View("~/Views/Admin/SubNavbar/Add.cshtml", model);
        }

        [HttpPost("add", Name = "admin-subnavbar-add")]
        public IActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = new AddViewModel
                {
                    Navbar = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToList()
                };
                return View("~/Views/Admin/Subnavbar/Add.cshtml", model);
            }

            var newModel = new SubNavbar()
            {
                Name = model.Name,
                URL = model.URL,
                Order = model.Order,
                NavbarId = model.NavbarId,
            };

            _dataContext.SubNavbars.Add(newModel);
            _dataContext.SaveChanges();

            return RedirectToRoute("admin-subnavbar-list");
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-subnavbar-update")]
        public IActionResult Update([FromRoute] int id)
        {

            var subnavbar = _dataContext.SubNavbars.FirstOrDefault(n => n.Id == id);
            if (subnavbar is null)
            {
                return NotFound();
            }
            var model = new AddViewModel
            {
                Name = subnavbar.Name,
                URL = subnavbar.URL,
                Order = subnavbar.Order,
                NavbarId = subnavbar.NavbarId,
                Navbar = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToList()
            };
            return View("~/Views/Admin/SubNavbar/Add.cshtml", model);
        }

        [HttpPost("update/{id}", Name = "admin-subnavbar-update")]
        public IActionResult Update(UpdateViewModel model)
        {

            if (!ModelState.IsValid)
            {

                model.Navbar = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Name)).ToList();
                return View("~/Views/Admin/SubNavbar/Update.cshtml", model);
            }
            var subnavbar = _dataContext.SubNavbars.Include(n => n.Navbar).FirstOrDefault(n => n.Id == model.Id);
            if (subnavbar is null)
            {
                return NotFound();
            }

            subnavbar.Name = model.Name;
            subnavbar.URL = model.URL;
            subnavbar.Order = model.Order;
            subnavbar.NavbarId = model.NavbarId;

            _dataContext.SaveChanges();
            return RedirectToRoute("admin-subnavbar-list");

        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-subnavbar-delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            var model = _dataContext.SubNavbars.FirstOrDefault(b => b.Id == id);
            if (model is null)
            {
                return NotFound();
            }

            _dataContext.SubNavbars.Remove(model);
            _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }

        #endregion
    }
}