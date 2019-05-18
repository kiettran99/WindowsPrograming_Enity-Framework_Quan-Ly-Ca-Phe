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
using System.Data.SqlClient;
namespace QuanLyCaPhe
{
    public partial class FormThanhPho : Form
    {
        DataTable dataTable = null;
        BSThanhPho TP = new BSThanhPho();
        string err;
        public FormThanhPho()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            try
            {
                DataTable dt = TP.LayThanhPho();
                dgvThanhPho.DataSource = dt;

            }
            catch(SqlException errr)
            {
                MessageBox.Show(errr.Message);

            }
            txtTenTP.ResetText();
            btThem.Enabled = true;
            btSua.Enabled = true;
            BtXoa.Enabled = true;
            btnReload.Enabled = true;
            btnThoat.Enabled = true;
            btLuu.Enabled = false;
            btnHuy.Enabled = false;

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FormThanhPho_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        bool Them;
        private void btThem_Click(object sender, EventArgs e)
        {
            Them = true;
            txtTenTP.ResetText();
            btThem.Enabled = false;
            btSua.Enabled = false;
            BtXoa.Enabled = false;
            btnThoat.Enabled = false;
            btnReload.Enabled = false;
            btnHuy.Enabled = true;
            btLuu.Enabled = true;

        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            if (!txtTenTP.Text.Trim().Equals(""))
            {
                if (Them)
                {
                    //thuc hiện lệnh
                    BSThanhPho TP = new BSThanhPho();
                    TP.ThemThanhPho(txtTenTP.Text.Trim(), ref err);
                    // Load lại DataGridView
                    LoadData();
                    // THông Báo
                    MessageBox.Show(err);
                }
                else
                {
                    //thuc hiện lệnh
                    BSThanhPho TP = new BSThanhPho();
                    int r = dgvThanhPho.CurrentCell.RowIndex;
                    TP.SuaThanhPho(txtTenTP.Text.Trim(), dgvThanhPho.Rows[r].Cells[0].Value.ToString(), ref err);
                    // Load lại DataGridView
                    LoadData();
                    // THông Báo
                    MessageBox.Show(err);
                }
            }
            else
            {
                txtTenTP.Focus();
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            Them = false;
            txtTenTP.ResetText();
            btThem.Enabled = false;
            btSua.Enabled = false;
            BtXoa.Enabled = false;
            btnThoat.Enabled = false;
            btnReload.Enabled = false;
            btnHuy.Enabled = true;
            btLuu.Enabled = true;
            dgvThanhPho_CellClick(null, null);
        }

        private void dgvThanhPho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvThanhPho.CurrentCell.RowIndex;
            txtTenTP.Text = dgvThanhPho.Rows[r].Cells[1].Value.ToString();
        }

        private void BtXoa_Click(object sender, EventArgs e)
        {
            dgvThanhPho_CellClick(null, null);
            DialogResult kq = new DialogResult();
            kq = MessageBox.Show("Bạn thật sự muốn xóa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(kq == DialogResult.OK)
            {
                int r = dgvThanhPho.CurrentCell.RowIndex;
                TP.Xoa(ref err, dgvThanhPho.Rows[r].Cells[0].Value.ToString());
                LoadData();
                MessageBox.Show(err);
            }
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
            dgvThanhPho_CellClick(null, null);
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
