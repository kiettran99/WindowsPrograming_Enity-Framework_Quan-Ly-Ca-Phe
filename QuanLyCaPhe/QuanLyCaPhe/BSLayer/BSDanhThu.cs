using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;

namespace QuanLyCaPhe.BSLayer
{
    public class BSDanhThu
    {

        DBMain db = null;

        public BSDanhThu()
        {
            db = new DBMain();
        }

        /// <summary>
        /// Lấy danh sách danh thu
        /// </summary>
        /// <param name="idBan"></param>
        public DataTable LayDanhThu(DateTime ngayTaoHoaDon, DateTime ngayKetThucHoaDon)
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();

            var dsDanhThu = from hd in qlcp.HoaDons join ba in qlcp.BanAns on hd.IDBanAn equals ba.IDBanAn
                            where hd.TinhTrang == true && hd.NgayTaoHoaDon.Day >= ngayTaoHoaDon.Day
                            && hd.NgayKetThucHoaDon.Value.Day <= ngayKetThucHoaDon.Day
                            select new { hd.IDHoaDon, ba.TenBan, hd.NgayTaoHoaDon, hd.NgayKetThucHoaDon, hd.GiamGia, hd.TongTien};

            DataTable dt = new DataTable();

            dt.Columns.Add("IDHoaDon");
            dt.Columns.Add("TenBan");
            dt.Columns.Add("NgayTaoHoaDon");
            dt.Columns.Add("NgayKetThucHoaDon");
            dt.Columns.Add("GiamGia");
            dt.Columns.Add("TongTien");

            foreach(var danhthu in dsDanhThu)
            {
                dt.Rows.Add(danhthu.IDHoaDon, danhthu.TenBan, danhthu.NgayTaoHoaDon, danhthu.NgayKetThucHoaDon, danhthu.GiamGia, danhthu.TongTien);
            }

            return dt;
        }       
    }
}
