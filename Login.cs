using QLTN_.HashPassword;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTN_
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }
        
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nguye\\source\\repos\\QLTN!\\rentaldata.mdf;Integrated Security=True;Connect Timeout=30";

        

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Kiểm tra xem các trường nhập liệu có trống không
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!");
                return;
            }

            // Kiểm tra xem mật khẩu có chính xác không
            if (CheckPassword(username, password))
            {
                // Đăng nhập thành công
                MessageBox.Show("Đăng nhập thành công!");
                
                Home Obj = new Home();
                Obj.Show();
                this.Hide();
            }
            else
            {
                // Đăng nhập không thành công
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!");
            }
        }

        public static bool CheckPassword(string username, string password)
        {
            // Truy vấn cơ sở dữ liệu để lấy mật khẩu đã lưu dựa trên tên người dùng
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\nguye\\source\\repos\\QLTN!\\rentaldata.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT Password FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    string hashedPassword = (string)command.ExecuteScalar();

                    // Kiểm tra mật khẩu đã lưu với mật khẩu nhập vào
                    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                }
            }
        }
        private void InsertUser(string username, string password)
        {
            string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Kiểm tra xem các trường nhập liệu có trống không
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!");
                return;
            }

            // Mã hóa mật khẩu và lưu vào cơ sở dữ liệu
            string hashedPassword = HashPassword(password);
            InsertUser(username, hashedPassword);

            MessageBox.Show("Đã thêm người dùng thành công!");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đóng ứng dụng", "Thông báo", MessageBoxButtons.YesNo);
            this.Close();
        }
    }
}
