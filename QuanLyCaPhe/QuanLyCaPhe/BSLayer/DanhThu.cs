using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;

namespace QuanLyCaPhe.BSLayer
{
    public class DanhThu
    {

        DBMain db = null;

        public DanhThu()
        {
            db = new DBMain();
        }

        /// <summary>
        /// Lấy danh sách danh thu
        /// </summary>
        /// <param name="idBan"></param>
        public DataSet LayDanhThu(DateTime ngayTaoHoaDon, DateTime ngayKetThucHoaDon)
        {
            string strSQL = $"select IDHoaDon, TenBan, NgayTaoHoaDon, NgayKetThucHoaDon, GiamGia,TongTien from HoaDon join BanAn on HoaDon.IDBanAn = BanAn.IDBanAn where HoaDon.TinhTrang = 1 and cast(NgayTaoHoaDon as date) >= '{ngayTaoHoaDon.Year}-{ngayTaoHoaDon.Month}-{ngayTaoHoaDon.Day}' and cast(NgayKetthucHoaDon as Date) <= '{ngayKetThucHoaDon.Year}-{ngayKetThucHoaDon.Month}-{ngayKetThucHoaDon.Day} '";
            return db.ExecuteQueryDataSet(strSQL, CommandType.Text);
        }       
    }
}
