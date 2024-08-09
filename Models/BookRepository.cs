using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace library.Models
{
    public class BookRepository
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["mycon"].ToString();
            con = new SqlConnection(constr);
        }

        //to add book details
        public bool AddBook(BookModel obj)
        {
            connection();
            SqlCommand com = new SqlCommand("InsertBookdetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@BookId", obj.BookId);
            com.Parameters.AddWithValue("@BookTitle", obj.BookTitle);
            com.Parameters.AddWithValue("@Author", obj.Author);
            com.Parameters.AddWithValue("@NumberOfBooks", obj.NumberOfBooks);
            com.Parameters.AddWithValue("@Category", obj.Category);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }

        }

        //to view book details
        public List<BookModel> GetBook()
        {
            connection();
            List<BookModel> BookList = new List<BookModel>();
            SqlCommand com = new SqlCommand("GetBookdetails", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            
            BookList = (from DataRow dr in dt.Rows

                        select new BookModel()
                        {
                            BookId = Convert.ToInt32(dr["BookId"]),
                            BookTitle = Convert.ToString(dr["BookTitle"]),
                            Author = Convert.ToString(dr["Author"]),
                            NumberOfBooks= Convert.ToInt32(dr["NumberOfBooks"]),
                            Category = Convert.ToString(dr["Category"]),

                        }).ToList();
            return BookList;
        }

        //to update bookdetails
        public bool UpdateBook(BookModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdateBookdetails", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@BookId", obj.BookId);
            com.Parameters.AddWithValue("@BookTitle", obj.BookTitle);
            com.Parameters.AddWithValue("@Author", obj.Author);
            com.Parameters.AddWithValue("@NumberOfBooks", obj.NumberOfBooks);
            com.Parameters.AddWithValue("@Category", obj.Category);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }

        // to delete user
        public bool DeleteBook(int id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeleteBookdetails", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@BookId", id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
        //request book
        public List<BookModel> RequestBook()
        {
            connection();
            List<BookModel> BookList = new List<BookModel>();
            SqlCommand com = new SqlCommand("RequestBookdetails", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();


            BookList = (from DataRow dr in dt.Rows

                        select new BookModel()
                        {
                            BookId = Convert.ToInt32(dr["BookId"]),
                            BookTitle = Convert.ToString(dr["BookTitle"]),
                            Author = Convert.ToString(dr["Author"]),
                            NumberOfBooks= Convert.ToInt32(dr["NumberOfBooks"]),
                            Category = Convert.ToString(dr["Category"]),

                        }).ToList();
            return BookList;
        }

        //search by category
    
    }
}