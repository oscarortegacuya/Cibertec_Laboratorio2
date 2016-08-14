﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Model;
using WebDeveloper.Repository;
using WebDeveloper.Filters;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    [ExceptionContol]
    [AuditControl]
    public class PersonController : Controller
    {
        // GET: Person
        private PersonRepository _person = new PersonRepository();        
        public ActionResult Index()
        {
            return View(_person.GetListBySize(15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (!ModelState.IsValid) return View(person);
            person.rowguid = Guid.NewGuid();
            person.BusinessEntity = new BusinessEntity
            {
                rowguid = person.rowguid,
                ModifiedDate = DateTime.Now
            };
            _person.Add(person);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var person = _person.GetById(id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (!ModelState.IsValid) return View(person);
            _person.Update(person);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var person = _person.GetById(id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(Person person)
        {
            person = _person.GetById(person.BusinessEntityID);
            _person.Delete(person);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var person = _person.GetById(id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }
    }
}