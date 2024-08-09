using library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace library.Controllers
{


    public class UserController : Controller
    {

        // GET: User
        public ActionResult User()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details()
        {
            Userrepository userRepo = new Userrepository();
            ModelState.Clear();
            return View(userRepo.GetUser());
        }


        // GET: User/Edit/5
        public ActionResult EditProfile(string Email)
        {
            Userrepository UserRepo = new Userrepository();
            return View(UserRepo.GetUser().Find(User => User.Email == Email));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult EditProfile(string email, UserModel obj)
        {
            try
            {
                Userrepository userRepo = new Userrepository();
                userRepo.UpdateUser(obj);

                return RedirectToAction("GetUser");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //collection


        // GET: User/Delete/5
        //public ActionResult Delete(string Email)
        //{
        //    return View();
        //}

        // POST: User/Delete/5
        //[HttpPost]
        //public ActionResult Delete(string Email, UserModel obj)
        //{
        //    try
        //    {
        //        Userrepository userRepo = new Userrepository();
        //        if (userRepo.DeleteUser(Email))
        //        {
        //            ViewBag.AlertMsg="Employee details deleted";
        //        }

        //        return RedirectToAction("GetUser");
        //    }
        //    catch(Exception ex)
        //    {
        //        return View();
        //    }
        //}

        ///collection of books <summary>
        /////this is correct*******************
        //public ActionResult Collection()
        //{
        //    //BookRepository bookRepo = new BookRepository();
        //    //ModelState.Clear();
        //    //return View(bookRepo.GetBook());

        //    RequestModel book = new RequestModel();
        //    using(var connection = new SqlConnection("mycon"))
        //    {
        //        using (var cmd = new SqlCommand("Requestbookdetails", connection))
        //        {
        //            cmd.ExecuteNonQuery();
        //            book.Books=connection.book.Tolist<BookModel>();
        //        }

        //    }
        //    return View(book);

        //}
        public ActionResult Collections()
        {
            RequestModel bookModel = new RequestModel();

            // Define the connection string (ensure this is correctly set up in your configuration)
            string connectionString = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;


            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Open the connection

                using (var cmd = new SqlCommand("Getbookdetails", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Ensure the command is set to stored procedure
                    using (var reader = cmd.ExecuteReader())
                    {
                        var books = new List<BookModel>();

                        // Assuming your BookModel has these properties
                        while (reader.Read())
                        {
                            var book = new BookModel
                            {
                                BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                                BookTitle = reader.GetString(reader.GetOrdinal("BookTitle")),
                                Author = reader.GetString(reader.GetOrdinal("Author")),
                                NumberOfBooks = reader.GetInt32(reader.GetOrdinal("NumberOfBooks")),
                                Category = reader.GetString(reader.GetOrdinal("Category"))
                            };
                            books.Add(book);
                        }

                        bookModel.Books = books;
                    }
                }
            }

            return View(bookModel);
        }


        //public ActionResult UserRequest(int id)
        //{

        //    BookRepository bookRepo = new BookRepository();
        //    return View(bookRepo.GetBook().Find(Book => Book.BookId == id));
        //}
        //[HttpPost]
        //public ActionResult UserRequest(int id, BookModel obj)
        //{
        //    try
        //    {
        //        BookRepository bookRepo = new BookRepository();
        //        ModelState.Clear();
        //        return View("RequestedBooksList");
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult UserRequest(int id)
        //{
        //    var bookRepo = new BookRepository();
        //    var book = bookRepo.GetBook().Find(b => b.BookId == id);

        //    if (book == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(book);
        //}
        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Abandon();
        //    return RedirectToAction("Index", "UserLogin");
        //}


    }
}
