using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Model;
using WebDeveloper.Repository;
using WebDeveloper.Filters;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    [AuditControl]
    public class PersonController : PersonBaseController<Person>
    {
        public ActionResult Index()
        {
            return View(_repository.GetList().Take(15));
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
            _repository.Add(person);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var person = _repository.GetById(x=> x.BusinessEntityID==id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (!ModelState.IsValid) return View(person);
            _repository.Update(person);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var person = _repository.GetById(x => x.BusinessEntityID == id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }

        [HttpPost]
        public ActionResult Delete(Person person)
        {
            person = _repository.GetById(x => x.BusinessEntityID == person.BusinessEntityID);
            _repository.Delete(person);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var person = _repository.GetById(x => x.BusinessEntityID == id);
            if (person == null) return RedirectToAction("Index");
            return View(person);
        }
    }
}