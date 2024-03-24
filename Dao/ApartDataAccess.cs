using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLTN_.Dao
{
    internal class ApartDataAccess
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nguye\\source\\repos\\QLTN!\\rentaldata.mdf;Integrated Security=True;Connect Timeout=30";

        public void ThemPhong(string tenPhong, string diaChi, string loaiPhong, string giaPhong, string chuNha)
        {
            string query = "INSERT INTO Apart (AName, AAdress, AType, ACost, Owner) VALUES (@TenPhong, @DiaChi, @LoaiPhong, @GiaPhong, @ChuNha)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenPhong", tenPhong);
                    command.Parameters.AddWithValue("@DiaChi", diaChi);
                    command.Parameters.AddWithValue("@LoaiPhong", loaiPhong);
                    command.Parameters.AddWithValue("@GiaPhong", giaPhong);
                    command.Parameters.AddWithValue("@ChuNha", chuNha);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SuaPhong(int maPhong, string tenPhong, string diaChi, string loaiPhong, string giaPhong, string chuNha)
        {
            string query = "UPDATE Apart SET AName = @TenPhong, AAdress = @DiaChi, AType = @LoaiPhong, ACost = @GiaPhong, Owner = @ChuNha WHERE Anum = @MaPhong";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong);
                    command.Parameters.AddWithValue("@TenPhong", tenPhong);
                    command.Parameters.AddWithValue("@DiaChi", diaChi);
                    command.Parameters.AddWithValue("@LoaiPhong", loaiPhong);
                    command.Parameters.AddWithValue("@GiaPhong", giaPhong);
                    command.Parameters.AddWithValue("@ChuNha", chuNha);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void XoaPhong(int maPhong)
        {
            string query = "UPDATE Apart SET Deleted = 1 WHERE Anum = @MaPhong";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable LayDanhSachPhong()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Apart WHERE Deleted = 0";
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

        public DataTable TimKiemPhong(string tuKhoa)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Apart WHERE Deleted = 0 AND (AName LIKE @TuKhoa OR AAdress LIKE @TuKhoa)";
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
        public int LayGiaPhong(int maPhong)
        {
            int giaPhong = 0;

            // Truy vấn cơ sở dữ liệu để lấy giá phòng dựa trên mã phòng
            string query = "SELECT ACost FROM Apart WHERE Anum = @MaPhong";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong);

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        giaPhong = Convert.ToInt32(result);
                    }
                }
            }

            return giaPhong;
        }
        public int CountRooms()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Apart";

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



