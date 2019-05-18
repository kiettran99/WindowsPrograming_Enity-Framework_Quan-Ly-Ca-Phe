using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;

namespace QuanLyCaPhe.BSLayer
{
    class KhachHang
    {
        DBMain db = null;

        public KhachHang()
        {
            db = new DBMain();
        }

        public DataSet LayKhachHang()
        {
            string strSQL = "select * from KhachHang";
            return db.ExecuteQueryDataSet(strSQL, CommandType.Text);
        }

        public bool ThemKhachHang(string maKhachHang, string hoKhachHang, string tenKhachHang, string soDienThoai, string diachi, string gioiTinh, DateTime ngaySinh, ref string error)
        {
            string strSQL = $"insert into KhachHang values('{maKhachHang.Trim()}', '{hoKhachHang.Trim()}', '{tenKhachHang.Trim()}', '{gioiTinh.Trim()}','{ngaySinh.ToShortDateString()}', '{soDienThoai.Trim()}', '{diachi.Trim()}')";
            return db.MyExecuteNonQuery(strSQL, CommandType.Text, ref error);
        }

        public bool SuaKhachHang(string maKhachhang, string hoKhachHang, string tenKhachHang, string soDienThoai, string diachi, string gioiTinh, DateTime ngaySinh, ref string error)
        {
            string strSQL = $"update KhachHang set HoKH = '{hoKhachHang}', TenKH = '{tenKhachHang}', GioiTinh = '{gioiTinh}', NgaySinh = '{ngaySinh.ToShortDateString()}', SDT = '{soDienThoai}', DiaChi = '{diachi}' where MAKH = '{maKhachhang}'";
            return db.MyExecuteNonQuery(strSQL, CommandType.Text, ref error);
        }

        public bool XoaKhachHang(string MaKhachHang, ref string error)
        {
            string sqlString = "Delete From KhachHang Where MaKH='" + MaKhachHang + "'";
            return db.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }
    }
}
