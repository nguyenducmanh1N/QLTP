using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTN_.Dao
{
    internal class CategoryDataAccess
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nguye\\source\\repos\\QLTN!\\rentaldata.mdf;Integrated Security=True;Connect Timeout=30";

        public void ThemDanhMuc(string categoryName, string remarks)
        {
            string query = "INSERT INTO Category (Category, Remarks) VALUES (@Category, @Remarks)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Category", categoryName);
                    command.Parameters.AddWithValue("@Remarks", remarks);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SuaDanhMuc(int maDanhMuc, string categoryName, string remarks)
        {
            string query = "UPDATE Category SET Category = @Category, Remarks = @Remarks WHERE CNum = @MaDanhMuc";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDanhMuc", maDanhMuc);
                    command.Parameters.AddWithValue("@Category", categoryName);
                    command.Parameters.AddWithValue("@Remarks", remarks);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void XoaDanhMuc(int maDanhMuc)
        {
            string query = "UPDATE Category SET Deleted = 1 WHERE CNum = @MaDanhMuc";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDanhMuc", maDanhMuc);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable LayDanhSachDanhMuc()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Category WHERE Deleted = 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable TimKiemDanhMuc(string tuKhoa)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Category WHERE Deleted = 0 AND (Category LIKE @TuKhoa OR Remarks LIKE @TuKhoa)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");

                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public int CountCategories()
        {
            int count = 0;
            string query = "SELECT COUNT (*) FROM Category";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }

            return count;
        }
    }
}
