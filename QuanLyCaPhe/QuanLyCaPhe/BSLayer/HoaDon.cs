using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;

namespace QuanLyCaPhe.BSLayer
{
    public class HoaDon
    {
        DBMain db = null;
        string err;
        public HoaDon()
        {
            db = new DBMain();
        }

        public DataSet LayHoaDon()
        {
            return db.ExecuteQueryDataSet("select * from HoaDon", CommandType.Text);
        }

        public int LayIDHoaDonTheoBan(int idBan)
        {
            DataTable dt = db.ExecuteQueryDataSet($"select * from HoaDon where IDBanAn = '{idBan}' and HoaDon.TinhTrang = 0", CommandType.Text).Tables[0];
            int id = -1;    //ID Hóa đơn mặc định không tìm thấy
            //Kiểm tra xem dt có dữ liệu hay không ?
            if (dt.Rows.Count > 0)
                id = (int)dt.Rows[0]["IDHoaDon"];
            return id;
        }
        public void CheckOut(int id, int discount, float TongTien)
        {
            string query = "";
            query = $"Update HoaDon set TinhTrang = '1', GiamGia = {discount}, TongTien = {TongTien}, NgayKetThucHoaDon = getdate() where IDHoaDon='" + id + "'";
            db.MyExecuteNonQuery(query, CommandType.Text, ref err);
        }
        public void ThemHoaDonTheoBan(int idBan, ref string error)
        {
            int maxID = MaxIDHoaDon(ref error) + 1;
            string strSQL = $"Insert into HoaDon values({maxID}, getdate(), null, {idBan}, 0, 0, 0)";
            db.MyExecuteNonQuery(strSQL, CommandType.Text, ref error);
        }

        public int MaxIDHoaDon(ref string error)
        {
            string strSQL = $"Select Max(HoaDon.IDHoaDon) from HoaDon";
            return (int)db.FirstRowQuery(strSQL, CommandType.Text, ref error);
        }

        public void XoaHoaDon(int idHoaDon, ref string error)
        {
            string query = "";
            query = $"delete from HoaDon where idHoaDon = {idHoaDon} and TinhTrang = 0";
            db.MyExecuteNonQuery(query, CommandType.Text, ref err);
        }
    }
}
