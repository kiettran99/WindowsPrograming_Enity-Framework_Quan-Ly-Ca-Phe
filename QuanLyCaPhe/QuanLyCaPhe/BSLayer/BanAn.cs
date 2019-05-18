using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;

namespace QuanLyCaPhe.BSLayer
{
    public class BanAn
    {
        DBMain db;
        public static double ChieuRongBan = 70;
        public static double ChieuDaiBan = 70;

        public BanAn()
        {
            db = new DBMain();
        }

        public List<Ban> DanhsachBan()
        {
            List<Ban> lBanAn = new List<Ban>();

            DataSet dt = db.ExecuteQueryDataSet("select * from BanAn", CommandType.Text);

            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                Ban b = new Ban(dr);
                lBanAn.Add(b);
            }

            return lBanAn;
        }

        public DataSet LayBanAn()
        {
            return db.ExecuteQueryDataSet("select * from BanAn", CommandType.Text);
        }

        public void ThayDoiTinhTrang(int idBan, bool b, ref string error)
        {
            string strSQL = "";
            if (b) strSQL = $"update BanAn set TinhTrang = N'Đã có người' where IDBanAn = {idBan}";
            else strSQL = $"update BanAn set TinhTrang = N'Trống' where IDBanAn = {idBan}";
            db.MyExecuteNonQuery(strSQL, CommandType.Text, ref error);
        }

        public bool ThemBanAn(string MaBan, string TenBan, string TinhTrang, ref string error)
        {
            string sqlString;
            try
            {
                sqlString = $"Insert into BanAn values('{MaBan.Trim()}', N'{TenBan.Trim()}',N'{TinhTrang.Trim()}')";
            }
            catch
            {
                error = "Thêm không được";
                return false;
            }
            error = "Thêm thành công";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

        public bool SuaBanAn(string MaBan, string TenBan, string TinhTrang, ref string error)
        {
            string sqlString;
            try
            {
                sqlString = $"Update BanAn Set TenBan = '{TenBan}', TinhTrang = N'{TinhTrang}' where IDBanAn = '{MaBan}' ";
            }
            catch
            {
                error = "Sửa không được";
                return false;
            }
            error = "Sửa thành công";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

        public bool XoaBanAn(string MaBan, ref string error)
        {
            string sqlString = $"delete from BanAn where IDBanAn = '{MaBan}'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

    }

    public class Ban
    {
        private int id;
        private string tenBan;
        private string tinhTrang;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string TenBan
        {
            get
            {
                return tenBan;
            }

            set
            {
                tenBan = value;
            }
        }

        public string TinhTrang
        {
            get
            {
                return tinhTrang;
            }

            set
            {
                tinhTrang = value;
            }
        }

        public Ban()
        {
            this.id = 0;
            this.tenBan = "Không có tên";
            this.tinhTrang = "Trống";
        }

        public Ban(int idBan, string tenban, string tinhtrang)
        {
            this.id = idBan;
            this.tenBan = tenban;
            this.tinhTrang = tinhtrang;
        }

        public Ban(DataRow row)
        {
            this.id = (int)row["IDBanAn"];
            this.tenBan = row["TenBan"].ToString();
            this.tinhTrang = row["TinhTrang"].ToString();
        }

    }
}
