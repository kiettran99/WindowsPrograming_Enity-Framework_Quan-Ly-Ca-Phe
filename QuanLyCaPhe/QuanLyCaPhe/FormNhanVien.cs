using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyCaPhe.BSLayer;
namespace QuanLyCaPhe
{
    public partial class FormNhanVien : Form
    {
        DataTable dataTable = null;
        NhanVien BLNV = new NhanVien();
        string err;
        string tk, mk;
        public FormNhanVien()
        {
            InitializeComponent();
            
        }
        void LoadData()
        {

           
                try
                {
                    dataTable = new DataTable();
                    dataTable.Clear();
                    DataSet ds = BLNV.LayNhanVien();
                    dataTable = ds.Tables[0];
                    // đưa dữ liệu vào dataGridView
                    dgvNhanVien.DataSource = dataTable;
                    
                }
                catch(SqlException errr)
                {
                    MessageBox.Show(errr.Message);
                }
                txtMaNV.ResetText();
                txtTenNV.ResetText();
                txtHoNV.ResetText();
                txtDiaChi.ResetText();
                txtDienThoai.ResetText();
                txtHoNV.ResetText();
                dtbNgayNV.ResetText();
                dtbNgaySinh.ResetText();
                // không cho thao tác trên các nút lưu/hủy
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;              
                // cho phép thao tác trên thêm/sửa/xóa/thoát               
                btnSua.Enabled = true;
                                          
        }
       
       

        private void btnReLoad_Click(object sender, EventArgs e)
        {
            LoadData();
            
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
            BLNV.LayTKMK(FormDangNhap.MaNV, ref tk, ref mk);
            txtTenTK.Text = tk;
            txtPassTK.Text = mk;
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvNhanVien.CurrentCell.RowIndex;
                txtMaNV.Text = dgvNhanVien.Rows[r].Cells[0].Value.ToString();
                rdbNu.Checked = (bool)dgvNhanVien.Rows[r].Cells[3].Value;
                dtbNgayNV.Text = dgvNhanVien.Rows[r].Cells[7].Value.ToString();
                dtbNgayNV.Text = dgvNhanVien.Rows[r].Cells[4].Value.ToString();
                txtHoNV.Text = dgvNhanVien.Rows[r].Cells[1].Value.ToString();
                txtTenNV.Text = dgvNhanVien.Rows[r].Cells[2].Value.ToString();
                txtDienThoai.Text = dgvNhanVien.Rows[r].Cells[5].Value.ToString();
                txtDiaChi.Text = dgvNhanVien.Rows[r].Cells[6].Value.ToString();
            }
            catch
            {

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            //thuc hiện lệnh
            NhanVien blnv = new NhanVien();
            if (FormDangNhap.MaNV != txtMaNV.Text.Trim())
                MessageBox.Show("Bạn không đủ quyền để thay đổi thông tin của người khác", "Thông báo",MessageBoxButtons.OKCancel ,MessageBoxIcon.Error);
           else blnv.SuaNhanVien(FormDangNhap.MaNV, txtHoNV.Text.Trim(), txtTenNV.Text.Trim(), rdbNu.Checked, dtbNgayNV.Value,
                dtbNgaySinh.Value, txtDiaChi.Text.Trim(), txtDienThoai.Text.Trim(), ref err);
            // Load lại DataGridView
            LoadData();
            // THông Báo
            MessageBox.Show(err);

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
           
            btnSua.Enabled = false;

            btnReLoad.Enabled = false;
            btnThoat.Enabled = false;

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            txtMaNV.ResetText();
            txtMaNV.Enabled = false;
            txtTenNV.ResetText();
            txtHoNV.ResetText();
            txtDiaChi.ResetText();
            txtDienThoai.ResetText();
            txtHoNV.ResetText();
            dtbNgayNV.ResetText();
            dtbNgaySinh.ResetText();

            dgvNhanVien_CellClick(null, null);
        }       

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dgvNhanVien_CellClick(null, null);
            
            btnSua.Enabled = true;
         
            btnThoat.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnReLoad.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = new DialogResult();
            kq = MessageBox.Show("Bạn thật sự muốn thoát", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (kq == DialogResult.OK)
            {
                Close();
            }
        }

       
    }
}
