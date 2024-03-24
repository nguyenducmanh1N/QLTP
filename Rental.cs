using QLTN_.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;



namespace QLTN_
{
    public partial class Rental : Form
    {
        private RentalDataAccess rentalDataAccess;
        private ApartDataAccess apartDataAccess;
        private TenantDataAccess tenantDataAccess;
        private int key = 0; // luu id  

        public Rental()
        {
            InitializeComponent();
            rentalDataAccess = new RentalDataAccess();
            apartDataAccess = new ApartDataAccess();
            tenantDataAccess = new TenantDataAccess();
            HienThiDanhSachThuePhong();
            HienThiDanhSachPhong();
            HienThiDanhSachKhachHang();
        }

        private void Reset()
        {
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void HienThiDanhSachThuePhong()
        {
            dataGridView1.DataSource = rentalDataAccess.LayDanhSachThuePhong();
        }

        private void HienThiDanhSachPhong()
        {
            comboBox1.DataSource = apartDataAccess.LayDanhSachPhong();
            //comboBox1.DisplayMember = "AName";
            comboBox1.DisplayMember = "Anum";
            comboBox1.ValueMember = "Anum";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                int maPhong = Convert.ToInt32(comboBox1.SelectedValue);
                int giaPhong = apartDataAccess.LayGiaPhong(maPhong);
                textBox1.Text = giaPhong.ToString();
            }
        }
        private void HienThiDanhSachKhachHang()
        {
            comboBox2.DataSource = tenantDataAccess.LayDanhSachKhachHang();
            //comboBox2.DisplayMember = "TenName";
            comboBox2.DisplayMember = "TenId";
            comboBox2.ValueMember = "TenId";
            
        }
        // them
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                int maPhong = Convert.ToInt32(comboBox1.SelectedValue);
                int maKhachHang = Convert.ToInt32(comboBox2.SelectedValue);
                int soTien = Convert.ToInt32(textBox1.Text);
                DateTime ngayDen = dateTimePicker1.Value;
                DateTime ngayDi = dateTimePicker2.Value;
                int tongTien = (ngayDi - ngayDen).Days * soTien;

                rentalDataAccess.ThemThuePhong(maPhong, soTien, maKhachHang, ngayDen, ngayDi, tongTien);
                HienThiDanhSachThuePhong();
            }
            Reset();
        }
        // sua
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                int maThuePhong = key;
                int maPhong = Convert.ToInt32(comboBox1.SelectedValue);
                int maKhachHang = Convert.ToInt32(comboBox2.SelectedValue);
                int soTien = Convert.ToInt32(textBox1.Text);
                DateTime ngayDen = dateTimePicker1.Value;
                DateTime ngayDi = dateTimePicker2.Value;
                int tongTien = (ngayDi - ngayDen).Days * soTien;

                rentalDataAccess.SuaThuePhong(maThuePhong, maPhong, soTien, maKhachHang, ngayDen, ngayDi, tongTien);
                HienThiDanhSachThuePhong();
            }
            Reset();
        }
        // xoa
        private void button3_Click(object sender, EventArgs e)
        {
            int maThuePhong = key;
            rentalDataAccess.XoaThuePhong(maThuePhong);
            HienThiDanhSachThuePhong();
            Reset();
        }
        // tim
        private void button4_Click(object sender, EventArgs e)
        {
            string tuKhoa = textBox3.Text;
            DataTable dataTable = rentalDataAccess.TimKiemThuePhong(tuKhoa);
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo rằng dòng được chọn là hợp lệ
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                key = Convert.ToInt32(selectedRow.Cells["RCode"].Value);

            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedrow = dataGridView1.Rows[e.RowIndex];
                comboBox1.Text = selectedrow.Cells[1].Value.ToString();
                textBox1.Text = selectedrow.Cells[2].Value.ToString();
                comboBox2.Text = selectedrow.Cells[3].Value.ToString();
                dateTimePicker1.Text = selectedrow.Cells[4].Value.ToString();
                dateTimePicker2.Text = selectedrow.Cells[5].Value.ToString();
                textBox2.Text = selectedrow.Cells[6].Value.ToString();



            }
        }
        // huy
        private void button5_Click(object sender, EventArgs e)
        {
            Reset();
            HienThiDanhSachThuePhong();
        }
        // reload dữ diệu trong data 
        private void Rental_Load(object sender, EventArgs e)
        {
            
            this.apartTableAdapter1.Fill(this.rentaldataDataSet4.Apart);
            
            this.tenantTableAdapter.Fill(this.rentaldataDataSet3.Tenant);
            
            this.apartTableAdapter.Fill(this.rentaldataDataSet2.Apart);
            // 
            dateTimePicker1.ValueChanged += dateTimePicker_ValueChanged;
            dateTimePicker2.ValueChanged += dateTimePicker_ValueChanged;


        }

        // hiển thị total ngay khi chọn ngày
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Lấy giá phòng từ textbox 1
                int giaPhong = Convert.ToInt32(textBox1.Text);

                // Lấy thời gian đến và thời gian đi từ datetimepicker
                DateTime ngayDen = dateTimePicker1.Value;
                DateTime ngayDi = dateTimePicker2.Value;

                // Tính số ngày thuê phòng
                int soNgay = (int)(ngayDi - ngayDen).TotalDays;

                // Tính tổng tiền và hiển thị vào textbox 2
                int tongTien = giaPhong * soNgay;
                textBox2.Text = tongTien.ToString();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các điều khiển trên giao diện người dùng
            string idPhong = comboBox1.SelectedValue.ToString();
            string giaTien = textBox1.Text;
            string idKhachHang = comboBox2.SelectedValue.ToString();
            string ngayDen = dateTimePicker1.Value.ToShortDateString();
            string ngayDi = dateTimePicker2.Value.ToShortDateString();
            string tongTien = textBox2.Text;

            // Tạo một hộp thoại lưu tệp
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog1.FileName;

                // Tạo một tài liệu PDF mới
                Document document = new Document();
                // Tạo một đối tượng PdfWriter để ghi vào tài liệu PDF
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                // Mở tài liệu để bắt đầu viết vào
                document.Open();

                // Tạo các đối tượng phông chữ và định dạng
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                // Tạo một font với kiểu chữ NORMAL
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);

                // Thêm nội dung vào tài liệu PDF
                Paragraph para = new Paragraph("Hoa don", font);
                para.Alignment = Element.ALIGN_CENTER;
                document.Add(para);

                // Thêm thông tin khách hàng và hóa đơn vào tài liệu
                document.Add(new Paragraph($"     ID Phong: {idPhong} ", font));
                document.Add(new Paragraph($"     Giá tien phong 1 ngay : {giaTien} VND", font));
                document.Add(new Paragraph($"     ID Khách hàng : {idKhachHang}", font));
                document.Add(new Paragraph($"     Ngay thue phong   : {ngayDen}", font));
                document.Add(new Paragraph($"     Ngay tra phong   : {ngayDi}", font));
                document.Add(new Paragraph($"     Tổng tiền : {tongTien} VND ", font));

                // Đóng tài liệu
                document.Close();

                MessageBox.Show("Hóa đơn đã được lưu thành công!");
            }
        }


        private void pictureBox7_Click(object sender, EventArgs e)
        {
            
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }
       

        private void label7_Click_1(object sender, EventArgs e)
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

        private void label8_Click(object sender, EventArgs e)
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
    }
}
