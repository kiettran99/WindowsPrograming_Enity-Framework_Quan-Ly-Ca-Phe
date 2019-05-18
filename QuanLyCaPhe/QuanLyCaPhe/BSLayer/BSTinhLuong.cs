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
    class BSTinhLuong
    {
        DBMain dbMain = null;

        public BSTinhLuong()
        {
            dbMain = new DBMain();
        }

        public DataTable LayTTTL()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            var tl = from b in qlcp.TinhLuongs
                     select b;

            DataTable dt = new DataTable();
            dt.Columns.Add("MaNV");
            dt.Columns.Add("TenNV");
            dt.Columns.Add("SoGioLam");
            dt.Columns.Add("Luong");
            foreach (var b in qlcp.TinhLuongs)
            {
                dt.Rows.Add(b.MaNV, b.TenNV, b.SoGioLam, b.Luong);
            }


            return dt;
        }

        public bool ThemNhanVien(string MaNV, string TenNV, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                TinhLuong tl = new TinhLuong();

                tl.MaNV = int.Parse(MaNV);
                tl.TenNV = TenNV;

                qlcp.TinhLuongs.Add(tl);

                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        public bool TinhLuongNhanVien(string MaNV, float sogiolam, float luong, ref string error)
        {

            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int idnv = int.Parse(MaNV);
                var nhanvien = (from nv in qlcp.TinhLuongs
                                where nv.MaNV == idnv
                                select nv).SingleOrDefault();

                if (nhanvien != null)
                {
                    nhanvien.SoGioLam = sogiolam;
                    nhanvien.Luong = luong;
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

        public bool ReStartL(string MaNV, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int idnv = int.Parse(MaNV);
                var nhanvien = (from nv in qlcp.TinhLuongs
                                where nv.MaNV == idnv
                                select nv).SingleOrDefault();

                if (nhanvien != null)
                {
                    nhanvien.SoGioLam = 0;
                    nhanvien.Luong = 0;
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

        public void LayDuLieu(string MaNV, ref TimeSpan dl1, ref TimeSpan dl2)
        {
            //string sqlString1 = "Select GioIn from ChamCong where MaNV ='" + MaNV + "'";

            //string sqlString2 = "Select GioOut from ChamCong where MaNV ='" + MaNV + "'";
            //dbMain.LayTime(sqlString1, sqlString2, CommandType.Text, ref dl1, ref dl2);

            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int idnv = int.Parse(MaNV);
                var GioLamViec = (from nv in qlcp.ChamCongs
                                where nv.MaNV == idnv
                                select new {nv.GioIn, nv.GioOut}).SingleOrDefault();
                dl1 = GioLamViec.GioIn.Value;
                dl2 = GioLamViec.GioOut.Value;
            }
            catch
            {

            }
        }
        public bool XoaNV(string MaNV, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int idnv = int.Parse(MaNV);
                var nhanvien = (from nv in qlcp.TinhLuongs
                                where nv.MaNV == idnv
                                select nv).SingleOrDefault();

                if (nhanvien != null)
                {
                    qlcp.TinhLuongs.Remove(nhanvien);
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

        public void LaySoTime(string MaNV, ref float SoTime)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int idnv = int.Parse(MaNV);
                var nhanvien = (from nv in qlcp.TinhLuongs
                                where nv.MaNV == idnv
                                select nv.SoGioLam).SingleOrDefault();
                SoTime = (float)nhanvien;
            }
            catch
            {
                
            }
        }
    }
}
