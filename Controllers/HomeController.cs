using library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace library.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(UserModel User)
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
                ModelState.Clear();
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        //public ActionResult Booklist()
        //{
        //    BookRepository bookRepo = new BookRepository();
        //    ModelState.Clear();
        //    return View(bookRepo.GetBook());
        //}




        // POST: Home/Contact
        [HttpPost]
        public ActionResult Contact(ContactModel Contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContactRepository contactRepo = new ContactRepository();
                    if (contactRepo.AddContact(Contact))
                    {
                        ViewBag.Message="Message submitted successfully";
                    }
                }
                ModelState.Clear();
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (login == null || !ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid login details.";
                return View();
            }

            string sqlCon = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(sqlCon))
            {
                using (SqlCommand cmd = new SqlCommand("GetLogindetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", login.Email);
                    cmd.Parameters.AddWithValue("@Password", login.Password);

                    SqlParameter outputIdParam = new SqlParameter("@Status", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputIdParam);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                        int status = (int)outputIdParam.Value;

                        if (status == 0)
                        {
                            // Assuming status 0 means successful login
                            ViewData["Message"] = "Login Successful!";
                            return RedirectToAction("User", "User"); // Redirect to User action in User controller
                        }
                        else
                        {
                            // Assuming any non-zero status means failure
                            ViewData["Message"] = "Invalid Email or Password!";
                            return View();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details here
                        ViewData["Message"] = "An error occurred: " + ex.Message;
                        return View();
                    }
                }
            }

        }
        public ActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Admin(AdminLogin login)
        {
            if (login == null || !ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid login details.";
                return View();
            }

            string sqlCon = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(sqlCon))
            {
                using (SqlCommand cmd = new SqlCommand("Admindetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminName", login.AdminName);
                    cmd.Parameters.AddWithValue("@AdminPassword", login.AdminPassword);

                    SqlParameter outputIdParam = new SqlParameter("@Status", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputIdParam);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                        int status = (int)outputIdParam.Value;

                        if (status == 0)
                        {
                            // Assuming status 0 means successful login
                            ViewData["Message"] = "Login Successful!";
                            return RedirectToAction("Adminhome", "Admin");
                        }
                        else
                        {
                            // Assuming any non-zero status means failure
                            ViewData["Message"] = "Invalid Email or Password!";
                            return View();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception details here
                        ViewData["Message"] = "An error occurred: " + ex.Message;
                        return View();
                    }
                }
            }


          
        }

        //search

        public ActionResult Booklist(string searchBy, string search)
        {
            BookRepository bookRepo = new BookRepository();
            var books = bookRepo.GetBook();
            
            if (searchBy == "Title")
            {
                return View(books.Where(book => book.BookTitle.Trim().Equals(search, StringComparison.OrdinalIgnoreCase)));
            }
            else 
            {
                return View(books.Where(book => book.Category.Trim().Equals(search, StringComparison.OrdinalIgnoreCase)));
            }

        }

      
    }
}