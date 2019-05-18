using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;

namespace QuanLyCaPhe.BSLayer
{
    class BSChiTietHoaDon
    {
        DBMain db = null;

        public BSChiTietHoaDon()
        {
            db = new DBMain();
        }

        /// <summary>
        /// Lấy hóa đơn từ 1 bàn
        /// </summary>
        /// <param name="idBan"></param>
        public DataTable LayChiTietHoaDon(int idBan)
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            var cthd = from a in qlcp.ChiTietHoaDons
                       join b in qlcp.HoaDons
                        on a.IDHoaDon equals b.IDHoaDon
                       join c in qlcp.ThucAns
                    on a.IDThucAn equals c.IDThucAn
                       join d in qlcp.BanAns
                        on b.IDBanAn equals d.IDBanAn
                       where d.IDBanAn == idBan && b.TinhTrang == false
                       select new { c.TenThucAn, a.SoLuong, c.Gia };
            DataTable dt = new DataTable();
            dt.Columns.Add("TenThucAn");
            dt.Columns.Add("SoLuong");
            dt.Columns.Add("Gia");

            foreach (var b in cthd)
            {
                dt.Rows.Add(b.TenThucAn, b.SoLuong, b.Gia);
            }

            return dt;
        }

        public bool ThemChiTietHD(int idHoaDon, int idThucAn, int count, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.IDHoaDon = idHoaDon;
                cthd.IDThucAn = idThucAn;
                cthd.SoLuong = count;
                qlcp.ChiTietHoaDons.Add(cthd);

                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        public bool ThemChiTietHDTonTai(int idHoaDon, int idThucAn, int count, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.IDHoaDon = idHoaDon;
                cthd.IDThucAn = idThucAn;
                cthd.SoLuong = count;
                qlcp.ChiTietHoaDons.Add(cthd);

                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        public bool XoaChiTietHoaDon(int idBan, int idThucAn, ref string error)
        {
            string strSQL = $"delete from ChiTietHoaDon where IDThucAn = {idThucAn} and IDHoaDon = (select IDHoaDon from HoaDon where IDBanAn = {idBan} and TinhTrang = 0)";
            db.MyExecuteNonQuery(strSQL, CommandType.Text, ref error);

            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

                var idHD = (from hd in qlcp.HoaDons
                            where hd.IDBanAn == idBan && hd.TinhTrang == false
                            select hd.IDHoaDon).SingleOrDefault();

                var cthd = (from ct in qlcp.ChiTietHoaDons
                            where ct.IDThucAn == idThucAn && ct.IDHoaDon == idHD
                            select ct).SingleOrDefault();

                if (cthd != null)
                {
                    qlcp.ChiTietHoaDons.Remove(cthd);
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
