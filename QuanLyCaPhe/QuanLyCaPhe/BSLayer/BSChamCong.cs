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
    class BSChamCong
    {
        DBMain dbMain = null;

        public BSChamCong()
        {
            dbMain = new DBMain();
        }
        public DataTable LayTT()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

            var chamcong = (from ccong in qlcp.ChamCongs
                            select ccong);

            DataTable dt = new DataTable();
            dt.Columns.Add("MaNV");
            dt.Columns.Add("TenNV");
            dt.Columns.Add("GioIn");
            dt.Columns.Add("GioOut");

            foreach (var ccong in chamcong)
            {
                dt.Rows.Add(ccong.MaNV, ccong.TenNV, ccong.GioIn, ccong.GioOut);
            }

            return dt;
        }
        public bool ThemNhanVien(string MaNV,  string TenNV, ref string error)
        {         
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

                ChamCong chamcong = new ChamCong();

                chamcong.MaNV = int.Parse(MaNV);
                chamcong.TenNV = TenNV;
                chamcong.GioIn = TimeSpan.Zero;
                chamcong.GioOut = TimeSpan.Zero;

                qlcp.ChamCongs.Add(chamcong);
                qlcp.SaveChanges();

                return true;
            }
            catch(Exception err)
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

                ChamCong chamcong = new ChamCong();
                chamcong.MaNV = int.Parse(MaNV);

                qlcp.ChamCongs.Attach(chamcong);
                qlcp.ChamCongs.Remove(chamcong);

                qlcp.SaveChanges();

                return true;
            }
            catch(Exception err)
            {
                error = err.Message;
                return false;
            }
          
        }

        public bool ChamCongNhanVien(string MaNV, TimeSpan timein,TimeSpan timeout,  ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int idnv = int.Parse(MaNV);
                var chamcong = (from ccong in qlcp.ChamCongs
                                where ccong.MaNV == idnv
                                select ccong).SingleOrDefault();

                if (chamcong != null)
                {
                    chamcong.GioIn = timein;
                    chamcong.GioOut = timeout;
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

        public bool BoChamCong(string MaNV,ref string error)
        {
            TimeSpan time = new TimeSpan();
            time = TimeSpan.Zero;
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int idnv = int.Parse(MaNV);

                var chamcong = (from ccong in qlcp.ChamCongs
                               where ccong.MaNV == idnv
                               select ccong).SingleOrDefault();

                if (chamcong != null)
                {
                    chamcong.GioIn = time;
                    chamcong.GioOut = time;
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
