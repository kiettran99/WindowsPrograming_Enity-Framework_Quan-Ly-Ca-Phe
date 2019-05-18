using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data.SqlClient;
using System.Data;
namespace QuanLyCaPhe.BSLayer
{
    class BSThanhPho
    {
        DBMain dbMain = null;
        public BSThanhPho()
        {
            dbMain = new DBMain();
        }

        public DataTable LayThanhPho()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

            var dstp = from tp in qlcp.ThanhPhoes
                       select tp;

            DataTable dt = new DataTable();
            dt.Columns.Add("MaThanhPho");
            dt.Columns.Add("TenThanhPho");

            foreach (var tp in dstp)
            {
                dt.Rows.Add(tp.MaThanhPho, tp.TenThanhPho);
            }

            return dt;
        }

        public bool ThemThanhPho(string TenTP, ref string error)
        {
            Random rd = new Random();
            int num ;
            num = rd.Next(10, 1000);

            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

                ThanhPho tp = new ThanhPho();
                tp.MaThanhPho = num;
                tp.TenThanhPho = TenTP;

                qlcp.ThanhPhoes.Add(tp);

                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        public bool SuaThanhPho(string TenTP,string MaTP, ref string error)
        {
            try
            {
                int maTP = int.Parse(MaTP);
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                var thanhpho = (from tp in qlcp.ThanhPhoes
                          where tp.MaThanhPho == maTP
                          select tp).SingleOrDefault();
                //Khi không tìm thấy đối tượng không cần sửa.
                if (thanhpho != null)
                {
                    thanhpho.TenThanhPho = TenTP;              
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

        public bool Xoa(ref string error, string MaTP)
        {
            try
            {
                int maTP = int.Parse(MaTP);
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                var thanhpho = (from tp in qlcp.ThanhPhoes
                                where tp.MaThanhPho == maTP
                                select tp).SingleOrDefault();
                //Khi không tìm thấy đối tượng không cần sửa.
                if (thanhpho != null)
                {
                    qlcp.ThanhPhoes.Remove(thanhpho);
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
