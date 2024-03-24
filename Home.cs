using QLTN_.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTN_
{
    public partial class Home : Form
    {
        private TenantDataAccess tenantDataAccess;
        private ApartDataAccess apartDataAccess;
        private CategoryDataAccess categoryDataAccess;
        private RentalDataAccess rentalDataAccess;
        private LandLordDataAccess lordDataAccess;

        public Home()
        {
            InitializeComponent();
            
            tenantDataAccess = new TenantDataAccess();
            apartDataAccess = new ApartDataAccess();
            categoryDataAccess = new CategoryDataAccess();
            rentalDataAccess = new RentalDataAccess();
            lordDataAccess = new LandLordDataAccess();
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            // Tính toán các thông tin thống kê từ cơ sở dữ liệu của bạn
            int totalTenants = tenantDataAccess.CountTenants();
            int totalRooms = apartDataAccess.CountRooms();
            int totallandLord = lordDataAccess.CountLandLord();
            int totalCategories = categoryDataAccess.CountCategories();
            int totalIncome = rentalDataAccess.SumTotalIncome();

            // Hiển thị thông tin thống kê trong các TextBox tương ứng
            textBox1.Text = totalTenants.ToString();
            textBox2.Text = totalRooms.ToString();
            textBox3.Text = totallandLord.ToString();
            textBox4.Text = totalCategories.ToString();
            textBox5.Text = totalIncome.ToString();
        }
        // show form apart 
        private void label5_Click(object sender, EventArgs e)
        {
            Apart Obj = new Apart();
            Obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Tenant Obj = new Tenant();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            LandLord Obj = new LandLord();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Rental Obj = new Rental();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đóng ứng dụng", "Thông báo", MessageBoxButtons.YesNo);
            this.Close();
        }
    }
}
