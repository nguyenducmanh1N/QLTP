using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTN_.Dao
{
    internal class RentalDataAccess
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nguye\\source\\repos\\QLTN!\\rentaldata.mdf;Integrated Security=True;Connect Timeout=30";

        public void ThemThuePhong(int maPhong, int soTien, int maKhachHang, DateTime ngayDen, DateTime ngayDi, int tongTien)
        {
            string query = "INSERT INTO Rental (Apartment, Amount, Tenant, Arrives, Leave, Total) VALUES (@MaPhong, @SoTien, @MaKhachHang, @NgayDen, @NgayDi, @TongTien)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong);
                    command.Parameters.AddWithValue("@SoTien", soTien);
                    command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    command.Parameters.AddWithValue("@NgayDen", ngayDen);
                    command.Parameters.AddWithValue("@NgayDi", ngayDi);
                    command.Parameters.AddWithValue("@TongTien", tongTien);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SuaThuePhong(int maThuePhong, int maPhong, int soTien, int maKhachHang, DateTime ngayDen, DateTime ngayDi, int tongTien)
        {
            string query = "UPDATE Rental SET Apartment = @MaPhong, Amount = @SoTien, Tenant = @MaKhachHang, Arrives = @NgayDen, Leave = @NgayDi, Total = @TongTien WHERE RCode = @MaThuePhong";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaThuePhong", maThuePhong);
                    command.Parameters.AddWithValue("@MaPhong", maPhong);
                    command.Parameters.AddWithValue("@SoTien", soTien);
                    command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                    command.Parameters.AddWithValue("@NgayDen", ngayDen);
                    command.Parameters.AddWithValue("@NgayDi", ngayDi);
                    command.Parameters.AddWithValue("@TongTien", tongTien);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void XoaThuePhong(int maThuePhong)
        {
            string query = "UPDATE Rental SET Deleted = 1 WHERE RCode = @MaThuePhong";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaThuePhong", maThuePhong);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable LayDanhSachThuePhong()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Rental WHERE Deleted = 0";
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

        public DataTable TimKiemThuePhong(string tuKhoa)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Rental WHERE Deleted = 0 AND (RCode LIKE @TuKhoa OR Apartment LIKE @TuKhoa OR Amount LIKE @TuKhoa OR Tenant LIKE @TuKhoa)";
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


        public int SumTotalIncome()
        {
            int sum = 0;
            string query = "SELECT SUM(Total) FROM Rental";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        sum = Convert.ToInt32(result);
                    }
                }
            }

            return sum;
        }

    }
}
