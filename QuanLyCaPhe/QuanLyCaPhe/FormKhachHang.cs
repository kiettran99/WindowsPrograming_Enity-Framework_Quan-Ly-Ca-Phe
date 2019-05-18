using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCaPhe.BSLayer;

namespace QuanLyCaPhe
{
    public partial class FormKhachHang : Form
    {
        DataTable dt = null;
        KhachHang kh = null;
        bool them = false;
        string error;

        public FormKhachHang()
        {
            InitializeComponent();
            kh = new KhachHang();
            cbGioiTinh.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {

                dt = new DataTable();
                dt.Clear();
                DataSet ds = kh.LayKhachHang();
                dt = ds.Tables[0];
                DgvKH.DataSource = dt;

                tbTen.ResetText();
                btThem.Enabled = true;
                btSua.Enabled = true;
                BtXoa.Enabled = true;
                btnReload.Enabled = true;
                btnThoat.Enabled = true;
                btLuu.Enabled = false;
                btnHuy.Enabled = false;
                cbGioiTinh.SelectedIndex = 0;

                DgvKH_CellClick(null, null);
            }
            catch (Exception errr)
            {
                MessageBox.Show(errr.Message);
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            them = true;

            btThem.Enabled = false;
            btSua.Enabled = false;
            BtXoa.Enabled = false;
            btnThoat.Enabled = false;
            btnReload.Enabled = false;
            btnHuy.Enabled = true;
            btLuu.Enabled = true;
            
            tbTen.ResetText();
            tbMKH.ResetText();
            tbDiaChi.ResetText();
            tbHKH.ResetText();
            tbSDT.ResetText();
            
            Random rd = new Random();
            tbMKH.Text = rd.Next(0, 10000).ToString();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btLuu.Enabled = false;
            btThem.Enabled = true;
            btSua.Enabled = true;
            BtXoa.Enabled = true;
            btnThoat.Enabled = true;
            btnReload.Enabled = true;
            DgvKH_CellClick(null, null);
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (!tbTen.Text.Trim().Equals(""))
            {
                if (them)
                {

                    kh.ThemKhachHang(tbMKH.Text, tbHKH.Text, tbTen.Text, tbSDT.Text, tbDiaChi.Text, cbGioiTinh.Text, dtNgaySinh.Value, ref error);
                    // Load lại DataGridView
                    LoadData();
                    // THông Báo
                   // MessageBox.Show(error);
                }
                else
                {
                    int r = DgvKH.CurrentCell.RowIndex;
                    kh.SuaKhachHang(tbMKH.Text, tbHKH.Text, tbTen.Text, tbSDT.Text, tbDiaChi.Text, cbGioiTinh.Text, dtNgaySinh.Value, ref error);
                    // Load lại DataGridView
                    LoadData();
                    // THông Báo
                   // MessageBox.Show(error);
                }
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            them = false;
            tbTen.ResetText();

            btThem.Enabled = false;
            btSua.Enabled = false;
            BtXoa.Enabled = false;
            btnThoat.Enabled = false;
            btnReload.Enabled = false;
            btnHuy.Enabled = true;
            btLuu.Enabled = true;
            DgvKH_CellClick(null, null);
        }

        private void BtXoa_Click(object sender, EventArgs e)
        {
            DgvKH_CellClick(null, null);
            DialogResult kq = new DialogResult();
            kq = MessageBox.Show("Bạn thật sự muốn xóa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (kq == DialogResult.OK)
            {
                int r = DgvKH.CurrentCell.RowIndex;
                kh.XoaKhachHang(DgvKH.Rows[r].Cells[0].Value.ToString(), ref error);
                LoadData();
                MessageBox.Show(error);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
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

        private void DgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = DgvKH.CurrentCell.RowIndex;
                // tbMKH.Text = ran
                tbMKH.Text = DgvKH.Rows[r].Cells[0].Value.ToString();
                tbHKH.Text = DgvKH.Rows[r].Cells[1].Value.ToString();
                tbTen.Text = DgvKH.Rows[r].Cells[2].Value.ToString();
                cbGioiTinh.Text = DgvKH.Rows[r].Cells[3].Value.ToString();
                dtNgaySinh.Value = (DateTime)DgvKH.Rows[r].Cells[4].Value;
                tbSDT.Text = DgvKH.Rows[r].Cells[5].Value.ToString();
                tbDiaChi.Text = DgvKH.Rows[r].Cells[6].Value.ToString();
            }
            catch { }
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tbMKH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
