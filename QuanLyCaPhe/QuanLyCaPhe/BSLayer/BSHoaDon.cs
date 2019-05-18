using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;

namespace QuanLyCaPhe.BSLayer
{
    public class BSHoaDon
    {
        DBMain db = null;
        string err;
        public BSHoaDon()
        {
            db = new DBMain();
        }

        public DataTable LayHoaDon()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            var hd = from b in qlcp.HoaDons
                     select b;
            DataTable dt = new DataTable();
            dt.Columns.Add("IDHoaDon");
            dt.Columns.Add("NgayTaoHoaDon");
            dt.Columns.Add("NgayKetThucHoaDon");
            dt.Columns.Add("TongTien");
            dt.Columns.Add("GiamGia");
            dt.Columns.Add("TinhTrang");


            foreach (var b in qlcp.HoaDons)
            {
                dt.Rows.Add(b.IDHoaDon, b.NgayTaoHoaDon, b.NgayKetThucHoaDon, b.TongTien, b.GiamGia, b.TinhTrang);
            }

            return dt;
        }

        public int LayIDHoaDonTheoBan(int idBan)
        {
            DataTable dt = db.ExecuteQueryDataSet($"select * from HoaDon where IDBanAn = '{idBan}' and HoaDon.TinhTrang = 0", CommandType.Text).Tables[0];
            int id = -1;    //ID Hóa đơn mặc định không tìm thấy
            //Kiểm tra xem dt có dữ liệu hay không ?
            if (dt.Rows.Count > 0)
                id = (int)dt.Rows[0]["IDHoaDon"];
            return id;

            //try
            //{
            //    QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            //    var hd = (from b in qlcp.HoaDons
            //              where b.IDBanAn == idBan
            //              && b.TinhTrang == false
            //              select b).SingleOrDefault();
            //    if (hd != null)
            //    {
            //        return hd.IDHoaDon;
            //    }
            //    else return -1;
            //}
            //catch
            //{
            //    return -1;
            //}

        }
        public void CheckOut(int id, int discount, float TongTien)
        {
            //string query = "";
            //query = $"Update HoaDon set TinhTrang = '1', GiamGia = {discount}, TongTien = {TongTien}, NgayKetThucHoaDon = getdate() where IDHoaDon='" + id + "'";
            //db.MyExecuteNonQuery(query, CommandType.Text, ref err);

            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                var hd = (from b in qlcp.HoaDons
                          where b.IDHoaDon == id
                          select b).SingleOrDefault();

                if (hd != null)
                {
                    hd.TinhTrang = true;
                    hd.GiamGia = discount;
                    hd.TongTien = TongTien;
                    hd.NgayKetThucHoaDon = DateTime.Now;
                    qlcp.SaveChanges();
                }

            }
            catch
            {
                
            }
        }
        public void ThemHoaDonTheoBan(int idBan, ref string error)
        {
            //int maxID = MaxIDHoaDon(ref error) + 1;
            //string strSQL = $"Insert into HoaDon values({maxID}, getdate(), null, {idBan}, 0, 0, 0)";
            //db.MyExecuteNonQuery(strSQL, CommandType.Text, ref error);

            try
            {
                int maxID = MaxIDHoaDon(ref error) + 1;
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

                HoaDon hd = new HoaDon();
                hd.IDHoaDon = maxID;
                hd.NgayTaoHoaDon = DateTime.Now;
                hd.NgayKetThucHoaDon = null;
                hd.IDBanAn = idBan;
                hd.GiamGia = 0;
                hd.TinhTrang = false;

                qlcp.HoaDons.Add(hd);
                qlcp.SaveChanges();
            }
            catch (Exception err)
            {
                error = err.Message;
            }
        }

        public int MaxIDHoaDon(ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                var max = (from hd in qlcp.HoaDons
                           select hd.IDHoaDon).Max();
                return max;
            }
            catch (Exception err)
            {
                error = err.Message;
                return -1;
            }
        }

        public void XoaHoaDon(int idHoaDon, ref string error)
        {
            //string query = "";
            //query = $"delete from HoaDon where idHoaDon = {idHoaDon} and TinhTrang = 0";
            //db.MyExecuteNonQuery(query, CommandType.Text, ref err);

            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                var hd = (from b in qlcp.HoaDons
                          where b.IDHoaDon == idHoaDon
                          select b).SingleOrDefault();

                if (hd != null)
                {
                    qlcp.HoaDons.Remove(hd);
                    qlcp.SaveChanges();
                }
            }
            catch (Exception err)
            {
                error = err.Message;
            }
        }
    }
}
