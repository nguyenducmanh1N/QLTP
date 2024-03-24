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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLTN_
{
    public partial class Tenant : Form
    {
        private TenantDataAccess tenantDataAccess;
        public Tenant()
        {
            InitializeComponent();
            tenantDataAccess = new TenantDataAccess();
            HienThiDanhSachKhachHang();
        }

        private void Reset()
        {
            
                textBox1.Text = "";
                comboBox1.SelectedIndex = -1;
                textBox2.Text = "";
                textBox3.Text = "";

            
        }
        private void HienThiDanhSachKhachHang()
        {
            dataGridView1.DataSource = tenantDataAccess.LayDanhSachKhachHang();
        }
        // them
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedIndex== -1 )
            {
                MessageBox.Show("thieu thong tin");
            }
            else
            {
                string tenKhachHang = textBox1.Text;
                string soDienThoai = textBox2.Text;
                string gioiTinh = comboBox1.Text;

                tenantDataAccess.ThemKhachHang(tenKhachHang, soDienThoai, gioiTinh);
                HienThiDanhSachKhachHang();
            }
            Reset();
        }

        int key = 0;
        // sua
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("thieu thong tin");
            }
            else
            {
                //int maKhachHang = LayMaKhachHangTuDataGridView();
                int maKhachHang = key;
            string tenKhachHang = textBox1.Text;
            string soDienThoai = textBox2.Text;
            string gioiTinh = comboBox1.Text;

            tenantDataAccess.SuaKhachHang(maKhachHang, tenKhachHang, soDienThoai, gioiTinh);
            HienThiDanhSachKhachHang();
            }
            Reset();
        }

        // xoa
        private void button3_Click(object sender, EventArgs e)
        {

            //int maKhachHang = LayMaKhachHangTuDataGridView();
            int maKhachHang = key;
            tenantDataAccess.XoaKhachHang(maKhachHang);
            HienThiDanhSachKhachHang();
            Reset();
        }

        // tim
        private void button4_Click(object sender, EventArgs e)
        {
            string tuKhoa = textBox3.Text;
            DataTable dataTable = tenantDataAccess.TimKiemKhachHang(tuKhoa);
            dataGridView1.DataSource = dataTable;
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo rằng dòng được chọn là hợp lệ
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                key = Convert.ToInt32(selectedRow.Cells["TenId"].Value);
                
            }
        }

        /*private int LayMaKhachHangTuDataGridView()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                return Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TenId"].Value);

            }
            return -1;
        }*/
        /*private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                textBox1.Text = selectedRow.Cells["TenName"].Value.ToString(); // Tên khách hàng
                textBox2.Text = selectedRow.Cells["TenPhone"].Value.ToString(); // Số điện thoại
                comboBox1.Text = selectedRow.Cells["TenGen"].Value.ToString(); // Giới tính
            }
        }*/
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedrow = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedrow.Cells[1].Value.ToString();
                textBox2.Text = selectedrow.Cells[2].Value.ToString();
                comboBox1.Text = selectedrow.Cells[3].Value.ToString();
                


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reset();
            HienThiDanhSachKhachHang();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }
        

        private void label5_Click_1(object sender, EventArgs e)
        {
            Apart Obj = new Apart();
            Obj.Show();
            this.Hide();
        }

        private void label13_Click_1(object sender, EventArgs e)
        {
            Tenant Obj = new Tenant();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            LandLord Obj = new LandLord();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click_1(object sender, EventArgs e)
        {
            Tenant Obj = new Tenant();
            Obj.Show();
            this.Hide();
        }
    }
}
