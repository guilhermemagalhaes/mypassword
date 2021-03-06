﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPassword.Entity;
using MyPassword.Services.Contract;

namespace MyPassword.Presentation.Controllers
{
    public class PlataformaController : BaseController
    {
        public readonly IPlataformaService _plataformaService;

        public PlataformaController(IPlataformaService plataformaService)
        {
            _plataformaService = plataformaService;
        }

        public ActionResult Index()
        {
            var item = _plataformaService.GetAll();
            if (HttpExtensions.IsAjaxRequest(Request))
                return PartialView(item);
            else
                return View(item);
        }

        public ActionResult Edit(int? id)
        {
            return PartialView("_EditPartial", id.HasValue ? _plataformaService.GetById((int)id) : new Plataforma());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Plataforma plataforma)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_EditPartial");
                }
                
                _plataformaService.InsertOrUpdate(plataforma);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return PartialView("_EditPartial");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _plataformaService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return PartialView();
            }
        }
    }
}