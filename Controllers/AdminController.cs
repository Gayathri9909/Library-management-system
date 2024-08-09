using library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace library.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Adminhome()
        {
            return View();
        }

        /*-----------User section-------------*/
        public ActionResult Usermanagement()
        {
            Userrepository userRepo = new Userrepository();
            ModelState.Clear();
            return View(userRepo.GetUser());
        }
       
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserModel User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Userrepository userRepo = new Userrepository();

                    if (userRepo.AddUser(User))
                    {
                        ViewBag.Message="User details added successfully";
                    }
                }
              
                
            
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        //public ActionResult Delete(string Email)
        //{
        //    return View();
        //}


        [HttpPost]
      
        public ActionResult Delete(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    ViewBag.AlertMsg = "Email is required.";
                    return RedirectToAction("GetUser");
                }

                Userrepository userRepo = new Userrepository();
                bool isDeleted = userRepo.DeleteUser(email);

                if (isDeleted)
                {
                    ViewBag.AlertMsg = "Employee details deleted successfully.";
                }
                else
                {
                    ViewBag.AlertMsg = "Failed to delete employee details.";
                }

                return RedirectToAction("GetUser");
            }
            catch (Exception ex)
            {
                ViewBag.AlertMsg = $"An error occurred: {ex.Message}";
                return RedirectToAction("GetUser");
            }
        }

    



        /*------------book section-------------*/
        ///
        public ActionResult Bookmanagement()
        {
            BookRepository bookRepo = new BookRepository();
            ModelState.Clear();
            return View(bookRepo.GetBook());
        }
        // GET: Book/BookCreate
        public ActionResult BookCreate()
        {
            return View();
        }

        // POST: Book/BookCreate
        [HttpPost]
        public ActionResult BookCreate(BookModel Book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BookRepository bookRepo = new BookRepository();
                    if (bookRepo.AddBook(Book))
                    {
                        ViewBag.Message="Book details added successfully";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "An unexpected error occurred.";
                return View();
            }
        }

        // GET: Book/BookEdit/5
        public ActionResult BookEdit(int id)
        {

            BookRepository bookRepo = new BookRepository();
            return View(bookRepo.GetBook().Find(Book => Book.BookId == id));
        }

        // POST: Book/BookEdit/5
        [HttpPost]
        public ActionResult BookEdit(int id, BookModel obj)
        {
            try
            {
                BookRepository bookRepo = new BookRepository();
                bookRepo.UpdateBook(obj);
                return View(bookRepo.GetBook());
                //return RedirectToAction("GetBook");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the book.");
                return View();
            }
        }

        // GET: Book/DeleteBook/5
        //public ActionResult BookDelete(int id)
        //{
        //    return View();
        //}

        // POST: Book/DeleteBook/5
        [HttpPost]
        public ActionResult BookDelete(int id)
        {
            try
            {
                BookRepository bookRepo = new BookRepository();
                if (bookRepo.DeleteBook(id))
                {
                    ViewBag.AlertMsg="Book details deleted";
                }
                return View(bookRepo.GetBook());
                //return RedirectToAction("GetBook");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //************contact details
        public ActionResult ContactMessage()
        {
            ContactRepository contactRepo = new ContactRepository();
            ModelState.Clear();
            return View(contactRepo.GetContact());
        }

        //*************admin section

        // GET: Admin/AdminDetails/5
        public ActionResult AdminDetails()
        {
            AdminRepository adminRepo = new AdminRepository();
            ModelState.Clear();
            return View(adminRepo.GetAdmin());
        }

        //// GET: User/Create
        public ActionResult AdminCreate()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult AdminCreate(AdminModel admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AdminRepository adminRepo = new AdminRepository();
                    if (adminRepo.AddAdmin(admin))
                    {
                        ViewBag.Message="Admin details added successfully";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult EditAdmin(int id)
        {
            AdminRepository adminRepo = new AdminRepository();
            return View(adminRepo.GetAdmin().Find(Admin => Admin.AdminId == id));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult EditAdmin(int  id, AdminModel obj)
        {
            try
            {
                AdminRepository adminRepo = new AdminRepository();
                adminRepo.UpdateAdmin(obj);

                return RedirectToAction("GetAdmin");
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        // GET: User/Delete/5
        //public ActionResult AdminDelete(int B)
        //{
        //    return View();
        //}

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult AdminDelete(int id)
        {
            try
            {
                AdminRepository adminRepo = new AdminRepository();
                if (adminRepo.DeleteAdmin(id))
                {
                    ViewBag.AlertMsg="Admin details deleted";
                }

                return RedirectToAction("GetAdmin");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}
