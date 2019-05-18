using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;
using System.Data.SqlClient;
namespace QuanLyCaPhe.BSLayer
{
    class BSNhanVien
    {
        DBMain dbMain = null;

        public BSNhanVien()
        {
            dbMain = new DBMain();
        }

        public DataTable LayNhanVien()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

            var dsnv = from nv in qlcp.NhanViens
                       select nv;

            DataTable dt = new DataTable();
            dt.Columns.Add("MaNV");
            dt.Columns.Add("HoNV");
            dt.Columns.Add("TenNV");
            dt.Columns.Add("Nu");
            dt.Columns.Add("NgaySinh");
            dt.Columns.Add("SDT");
            dt.Columns.Add("DiaChi");
            dt.Columns.Add("NgayBD");

            foreach (var nv in dsnv)
            {
                dt.Rows.Add(nv.MaNV, nv.HoNV, nv.TenNV, nv.Nu, nv.NgaySinh, nv.SDT, nv.DiaChi, nv.NgayBD);
            }

            return dt;

        }

        public bool SuaNhanVien(string MaNV, string Ho, string TenNV, bool Nu, DateTime NgayNV, DateTime NgaySinh, string DiaChi, string SDT, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                int idnv = int.Parse(MaNV);
                var nhanvien = (from nv in qlcp.NhanViens
                                where nv.MaNV == idnv
                                select nv).SingleOrDefault();

                //Khi không tìm thấy đối tượng không cần sửa.
                if (nhanvien != null)
                {
                    nhanvien.HoNV = Ho;
                    nhanvien.TenNV = TenNV;
                    nhanvien.Nu = Nu;
                    nhanvien.NgaySinh = NgaySinh;
                    nhanvien.SDT = SDT;
                    nhanvien.DiaChi = DiaChi;
                    nhanvien.NgayBD = NgayNV;
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
        public void LayTKMK(string MaNV, ref string TK, ref string MK)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                int idnv = int.Parse(MaNV);
                var dangnhap = (from dn in qlcp.DangNhaps
                                where dn.MaNV == idnv
                                select new { dn.TaiKhoan, dn.MatKhau }).SingleOrDefault();
                TK = dangnhap.TaiKhoan;
                MK = dangnhap.MatKhau;
            }
            catch
            {

            }
        }

        public bool ThemNhanVien(string MaNV, string Ho, string TenNV, bool Nu, DateTime NgayNV, DateTime NgaySinh, string DiaChi, string SDT, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                int idnv = int.Parse(MaNV);

                NhanVien nhanvien = new NhanVien();
                nhanvien.MaNV = idnv;
                nhanvien.HoNV = Ho;
                nhanvien.TenNV = TenNV;
                nhanvien.Nu = Nu;
                nhanvien.NgaySinh = NgaySinh;
                nhanvien.SDT = SDT;
                nhanvien.DiaChi = DiaChi;
                nhanvien.NgayBD = NgayNV;

                qlcp.NhanViens.Add(nhanvien);
                qlcp.SaveChanges();
                return true;

            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        public bool XoaNhanVien(string MaNV, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                int idnv = int.Parse(MaNV);
                var nhanvien = (from nv in qlcp.NhanViens
                                where nv.MaNV == idnv
                                select nv).SingleOrDefault();

                //Khi không tìm thấy đối tượng không cần sửa.
                if (nhanvien != null)
                {
                    qlcp.NhanViens.Remove(nhanvien);
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
