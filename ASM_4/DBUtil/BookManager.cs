using ASM_4.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_4.DBUtil
{
    public class BookManager
    {
        string connectionString;

        public BookManager()
        {
            connectionString = GetConnectionString();
        }

        private string GetConnectionString()
        {
            string str = "server=.;database=BookStore;uid=sa;pwd=123";
            return str;
        }

        public bool CheckLogin(string id, string password)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Employees WHERE ID = @id AND Password = @password";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("password", password);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    result = false;
                }
            }
            return result;
        } 

        public bool UpdateEmployee(Employee e)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Employees " +
                                "SET Password = @password, Role = @role " +
                                "WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", e.ID);
                cmd.Parameters.AddWithValue("password", e.Password);
                cmd.Parameters.AddWithValue("role", e.Role);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    int count = cmd.ExecuteNonQuery();
                    result = count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wrong Id");
                }
            }
            return result;
        }

        public Employee FindEmployee(string id)
        {
            Employee result = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Employees WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", id);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = new Employee
                            {
                                ID = reader.GetString(0),
                                Password = reader.GetString(1),
                                Role = reader.GetBoolean(2),
                            };
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Wrong Id");
                }
            }
            return result;
        }

        public DataTable GetBooks()
        {
            string sql = "SELECT * FROM Books";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbBooks = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                da.Fill(tbBooks);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return tbBooks;
        }

        public DataTable GetBooksByTitle(string str)
        {
            string sql = "SELECT * FROM Books " +
                        "WHERE BookName LIKE '%" + str + "%'";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtBook = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.Fill(dtBook);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return dtBook;
        }
        public bool AddNewBook(Book b)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Books " +
                                "VALUES(@id, @name, @price)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", b.ID);
                cmd.Parameters.AddWithValue("name", b.Name);
                cmd.Parameters.AddWithValue("price", b.Price);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    int count = cmd.ExecuteNonQuery();
                    result = count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ID is duplicated");
                }
            }
            return result;
        }

        public bool UpdateBook(Book e)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Books " +
                                "SET BookName = @name, Price = @price " +
                                "WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("id", e.ID);
                cmd.Parameters.AddWithValue("name", e.Name);
                cmd.Parameters.AddWithValue("price", e.Price);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    int count = cmd.ExecuteNonQuery();
                    result = count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wrong Id");
                }
            }
            return result;
        }

        public bool DeleteBook(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "DELETE Books WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int count = -1;
                try
                {
                    count = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }
                return count > 0;
            }
        }
    }
}
