using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTN_.Dao
{
    internal class TenantDataAccess
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nguye\\source\\repos\\QLTN!\\rentaldata.mdf;Integrated Security=True;Connect Timeout=30"; 

        public void ThemKhachHang(string ten, string soDienThoai, string gioiTinh)
        {
            string query = "INSERT INTO Tenant (TenName, TenPhone, TenGen) VALUES (@Ten, @SoDienThoai, @GioiTinh)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ten", ten);
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@GioiTinh", gioiTinh);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SuaKhachHang(int maKhachHang, string ten, string soDienThoai, string gioiTinh)
        {
            string query = "UPDATE Tenant SET TenName = @Ten, TenPhone = @SoDienThoai, TenGen = @GioiTinh WHERE TenId = @MaKhachHang";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    command.Parameters.AddWithValue("@Ten", ten);
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@GioiTinh", gioiTinh);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void XoaKhachHang(int maKhachHang)
        {
            string query = "UPDATE Tenant SET Deleted = 1 WHERE TenId = @MaKhachHang";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable LayDanhSachKhachHang()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tenant WHERE Deleted = 0";
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

        public DataTable TimKiemKhachHang(string tuKhoa)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tenant WHERE Deleted = 0 AND (TenName LIKE @TuKhoa OR TenPhone LIKE @TuKhoa OR TenGen LIKE @TuKhoa)";
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

        

        public int CountTenants()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Tenant";

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
