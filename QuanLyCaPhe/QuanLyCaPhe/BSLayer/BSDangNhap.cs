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
    class BSDangNhap
    {
        DBMain dbMain = null;
        public BSDangNhap()
        {
            dbMain = new DBMain();
        }

        public bool KiemTra(string tk, string mk, ref string error, ref string MaNV)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

                var soluong = (from dangnhap in qlcp.DangNhaps
                               where dangnhap.TaiKhoan == tk && dangnhap.MatKhau == mk
                               select dangnhap).Count();

                if (soluong > 0)
                {
                    int? idnv = (from dangnhap in qlcp.DangNhaps
                                 where dangnhap.TaiKhoan == tk
                                 select dangnhap.MaNV).SingleOrDefault();
                    MaNV = idnv.ToString();
                    error = "Thành Công";
                    return true;
                }
                else
                {
                    error = "Sai tài khoản hoặc mật khẩu";
                    return false;
                }
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        public DataTable LayTK()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

            var dsdangnhap = from dangnhap in qlcp.DangNhaps
                             select dangnhap;

            DataTable dt = new DataTable();
            dt.Columns.Add("TaiKhoan");
            dt.Columns.Add("MatKhau");
            dt.Columns.Add("MaNV");

            foreach (var dangnhap in dsdangnhap)
            {
                dt.Rows.Add(dangnhap.TaiKhoan, dangnhap.MatKhau, dangnhap.MaNV);
            }

            return dt;
        }


        public bool ThemTK(string TenTK, string Pass, string MaNV, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

                DangNhap dn = new DangNhap();
                dn.TaiKhoan = TenTK;
                dn.MatKhau = Pass;
                dn.MaNV = int.Parse(MaNV);

                qlcp.DangNhaps.Add(dn);
                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }
        public bool XoaTK(string MaNV, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

                int idnv = int.Parse(MaNV);

                var dangnhap = (from dn in qlcp.DangNhaps
                                where dn.MaNV == idnv
                                select dn).SingleOrDefault();

                if (dangnhap != null) qlcp.DangNhaps.Remove(dangnhap);

                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }
        public bool RenewTK(string MaNV, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

                int idnv = int.Parse(MaNV);

                var dangnhap = (from dn in qlcp.DangNhaps
                                where dn.MaNV == idnv
                                select dn).SingleOrDefault();

                if (dangnhap != null)
                {
                    dangnhap.MatKhau = "123";
                    qlcp.SaveChanges();
                    error = "Sửa thành công";
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
