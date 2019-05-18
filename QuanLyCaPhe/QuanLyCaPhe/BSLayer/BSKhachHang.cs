using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;

namespace QuanLyCaPhe.BSLayer
{
    class BSKhachHang
    {
        DBMain db = null;

        public BSKhachHang()
        {
            db = new DBMain();
        }

        public DataTable LayKhachHang()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            var kh = from b in qlcp.KhachHangs
                     select b;

            DataTable dt = new DataTable();
            dt.Columns.Add("MaKh");
            dt.Columns.Add("HoKH");
            dt.Columns.Add("TenKH");
            dt.Columns.Add("GioiTinh");
            dt.Columns.Add("NgaySinh");
            dt.Columns.Add("SDT");
            dt.Columns.Add("DiaChi");

            foreach (var b in qlcp.KhachHangs)
            {
                dt.Rows.Add(b.MaKH, b.HoKH, b.TenKH, b.GioiTinh, b.NgaySinh, b.SDT, b.DiaChi);
            }

            return dt;
        }

        public bool ThemKhachHang(string maKhachHang, string hoKhachHang, string tenKhachHang, string soDienThoai, string diachi, string gioiTinh, DateTime ngaySinh, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                KhachHang kh = new KhachHang();
                kh.MaKH = int.Parse(maKhachHang);
                kh.HoKH = hoKhachHang;
                kh.TenKH = tenKhachHang;
                kh.SDT = soDienThoai;
                kh.DiaChi = diachi;

                qlcp.KhachHangs.Add(kh);

                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        public bool SuaKhachHang(string maKhachhang, string hoKhachHang, string tenKhachHang, string soDienThoai, string diachi, string gioiTinh, DateTime ngaySinh, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int makh = int.Parse(maKhachhang);
                //Lấy địa chỉ của đối tượng có MaBan
                var kh = (from b in qlcp.KhachHangs
                          where b.MaKH == makh
                          select b).SingleOrDefault();
                //Khi không tìm thấy đối tượng không cần sửa.
                if (kh != null)
                {
                    kh.MaKH = int.Parse(maKhachhang);
                    kh.HoKH = hoKhachHang;
                    kh.TenKH = tenKhachHang;
                    kh.SDT = soDienThoai;
                    kh.DiaChi = diachi;
                    //Cập nhật
                    qlcp.SaveChanges();
                }
                return true;

            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        public bool XoaKhachHang(string MaKhachHang, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int makh = int.Parse(MaKhachHang);

                var kh = (from b in qlcp.KhachHangs
                          where b.MaKH == makh         
                          select b).SingleOrDefault();
                //Khi không tìm thấy đối tượng không cần sửa.
                if (kh != null)
                {
                    qlcp.KhachHangs.Remove(kh);
                    //Cập nhật
                    qlcp.SaveChanges();
                }
                return true;

            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }
    }
}
