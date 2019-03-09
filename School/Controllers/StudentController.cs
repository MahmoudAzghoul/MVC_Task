using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School.Controllers
{
    public class StudentController : Controller
    {

        static List<Student> studentList = new List<Student>();


        //Show students list
        public ActionResult List(Student std)
        {
            ViewBag.TotalStudents = studentList.Count();
            return View(studentList);
        }
        //----------------------------------------------------------


        //Create Student
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student std)
        {
            bool nameAlreadyExists = false;
                foreach (var item in studentList)
                {
                    if (std.StudentName == item.StudentName)
                    {
                        nameAlreadyExists = true;
                        break;
                    }
                }
            if (nameAlreadyExists)
            {
                ModelState.AddModelError(string.Empty, "Student Name already exists.");
                return View(std);
            }
            studentList.Add(std);
            TempData["msg"] = "Successfully Added";
            return View();
        }
        //----------------------------------------------------------


        //Delete Student
        public ActionResult Delete(int id)
        {
            var std = studentList.Where(s => s.StudentId == id).FirstOrDefault();
            return View(std);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var st = studentList.Find(c => c.StudentId == id);
            studentList.Remove(st);
            return RedirectToAction("List");
        }
        //----------------------------------------------------------


        //Get a Student
        public ActionResult Details(int id)
        {
            var std = studentList.Where(s => s.StudentId == id).FirstOrDefault();
            return View(std);
        }
        //----------------------------------------------------------


        //Edit a Student
        public ActionResult Edit(int? id)
        {
            var std = studentList.Where(s => s.StudentId == id).FirstOrDefault();
            return View(std);
        }

        [HttpPost]
        public ActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                if (std.StudentId == null)
                {
                    var s = studentList.Find(c => c.StudentId == std.StudentId);
                    studentList.Remove(s);
                    return RedirectToAction("Create", std);
                }
                else
                {
                    var s = studentList.Find(c => c.StudentId == std.StudentId);
                    studentList.Remove(s);
                    studentList.Add(std);
                    return RedirectToAction("List");
                }
            }
            return View(std);
            
        }







    }

}