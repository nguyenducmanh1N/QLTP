using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTN_.Dao
{
    internal class LandLordDataAccess
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nguye\\source\\repos\\QLTN!\\rentaldata.mdf;Integrated Security=True;Connect Timeout=30";

        public void ThemChuNha(string ten, string soDienThoai, string gioiTinh)
        {
            string query = "INSERT INTO Landlord (LLName, LLPhone, LLGen) VALUES (@Ten, @SoDienThoai, @GioiTinh)";

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

        public void SuaChuNha(int maChuNha, string ten, string soDienThoai, string gioiTinh)
        {
            string query = "UPDATE Landlord SET LLName = @Ten, LLPhone = @SoDienThoai, LLGen = @GioiTinh WHERE LLId = @MaChuNha";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaChuNha", maChuNha);
                    command.Parameters.AddWithValue("@Ten", ten);
                    command.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                    command.Parameters.AddWithValue("@GioiTinh", gioiTinh);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void XoaChuNha(int maChuNha)
        {
            string query = "UPDATE Landlord SET Deleted = 1 WHERE TenId = @MaChuNha";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaChuNha", maChuNha);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable LayDanhSachChuNha()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Landlord WHERE Deleted = 0";
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

        public DataTable TimKiemChuNha(string tuKhoa)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Landlord WHERE Deleted = 0 AND (LLName LIKE @TuKhoa OR LLPhone LIKE @TuKhoa OR LLGen LIKE @TuKhoa)";
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

        public int CountLandLord()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM LandLord";

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
