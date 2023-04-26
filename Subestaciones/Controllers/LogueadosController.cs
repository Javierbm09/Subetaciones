using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;

namespace Subestaciones.Controllers
{


    public class LogueadosController : Controller
    {
        // GET: Logueados
        public ActionResult Index()
        {
            return View();
        }

        // GET: Logueados/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Logueados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logueados/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Logueados/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Logueados/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Logueados/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Logueados/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
