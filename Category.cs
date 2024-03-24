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
    public partial class Category : Form
    {
        private CategoryDataAccess categoryDataAccess;
        private int key = 0; // 
        public Category()
        {
            InitializeComponent();
            categoryDataAccess = new CategoryDataAccess();
            HienThiDanhSachDanhMuc();
        }
        private void Reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void HienThiDanhSachDanhMuc()
        {
            dataGridView1.DataSource = categoryDataAccess.LayDanhSachDanhMuc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                string categoryName = textBox1.Text;
                string remarks = textBox2.Text;

                categoryDataAccess.ThemDanhMuc(categoryName, remarks);
                HienThiDanhSachDanhMuc();
            }
            Reset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                int maDanhMuc = key;
                string categoryName = textBox1.Text;
                string remarks = textBox2.Text;

                categoryDataAccess.SuaDanhMuc(maDanhMuc, categoryName, remarks);
                HienThiDanhSachDanhMuc();
            }
            Reset();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int maDanhMuc = key;
            categoryDataAccess.XoaDanhMuc(maDanhMuc);
            HienThiDanhSachDanhMuc();
            Reset();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string tuKhoa = textBox3.Text;
            DataTable dataTable = categoryDataAccess.TimKiemDanhMuc(tuKhoa);
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo rằng dòng được chọn là hợp lệ
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                key = Convert.ToInt32(selectedRow.Cells["CNum"].Value);

            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedrow = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedrow.Cells[1].Value.ToString();
                textBox2.Text = selectedrow.Cells[2].Value.ToString();
                
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Reset();
            HienThiDanhSachDanhMuc();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
           
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }
    }
}
