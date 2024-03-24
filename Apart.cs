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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QLTN_
{
    public partial class Apart : Form

    {
        private ApartDataAccess apartDataAccess;
        private int key = 0; // luu id apart 

        public Apart()
        {
            InitializeComponent();
            apartDataAccess = new ApartDataAccess();
            HienThiDanhSachPhong();
        }

        private void Reset()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void HienThiDanhSachPhong()
        {
            dataGridView1.DataSource = apartDataAccess.LayDanhSachPhong();
        }
        // them
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("thieu thong tin");
            }
            else
            {
                string tenPhong = textBox1.Text;
                string diaChi = textBox2.Text;
                string giaPhong = textBox3.Text;
                string loaiPhong = comboBox1.Text;
                string chuNha = comboBox2.Text;


                apartDataAccess.ThemPhong(tenPhong, diaChi, loaiPhong, giaPhong, chuNha);
                HienThiDanhSachPhong();
            }
            Reset();
        }

        //sua
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("thieu thong tin");
            }
            else
            {
                int maPhong = key;
                //int maKhachHang = LayMaKhachHangTuDataGridView();
                string tenPhong = textBox1.Text;
                string diaChi = textBox2.Text;
                string giaPhong = textBox3.Text;
                string loaiPhong = comboBox1.Text;
                string chuNha = comboBox2.Text;

                apartDataAccess.SuaPhong(maPhong,tenPhong, diaChi, loaiPhong, giaPhong, chuNha);
                HienThiDanhSachPhong();
            }
            Reset();
        }
        // xoa
        private void button3_Click(object sender, EventArgs e)
        {
            int maPhong = key;
            apartDataAccess.XoaPhong(maPhong);
            HienThiDanhSachPhong();
            Reset();
        }
        //thim
        private void button4_Click(object sender, EventArgs e)
        {
            string tuKhoa = textBox4.Text;
            DataTable dataTable = apartDataAccess.TimKiemPhong(tuKhoa);
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                key = Convert.ToInt32(selectedRow.Cells["Anum"].Value);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedrow = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedrow.Cells[1].Value.ToString();
                textBox2.Text = selectedrow.Cells[2].Value.ToString();
                textBox3.Text = selectedrow.Cells[2].Value.ToString();
                comboBox1.Text = selectedrow.Cells[3].Value.ToString();
                comboBox2.Text = selectedrow.Cells[3].Value.ToString();

            }
        }
        // huy
        private void button5_Click(object sender, EventArgs e)
        {
            Reset();
            HienThiDanhSachPhong();
        }

        private void Apart_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'rentaldataDataSet1.LandLord' table. You can move, or remove it, as needed.
            this.landLordTableAdapter.Fill(this.rentaldataDataSet1.LandLord);
            // TODO: This line of code loads data into the 'rentaldataDataSet.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.rentaldataDataSet.Category);

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
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
            Rental Obj = new Rental();
            Obj.Show();
            this.Hide();
        }
    }
}
