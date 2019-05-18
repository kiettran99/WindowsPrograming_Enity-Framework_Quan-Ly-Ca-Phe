using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;

namespace QuanLyCaPhe.BSLayer
{
    public class BSBanAn
    {
        DBMain db;
        public static double ChieuRongBan = 70;
        public static double ChieuDaiBan = 70;

        public BSBanAn()
        {
            db = new DBMain();
        }

        public List<Ban> DanhsachBan()
        {
            List<Ban> lBanAn = new List<Ban>();

            DataTable dt = LayBanAn();

            foreach (DataRow dr in dt.Rows)
            {
                Ban b = new Ban(dr);
                lBanAn.Add(b);
            }

            return lBanAn;
        }

        public DataTable LayBanAn()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            var ba = from b in qlcp.BanAns
                     select b;

            DataTable dt = new DataTable();
            dt.Columns.Add("IDBanAn");
            dt.Columns.Add("TenBan");
            dt.Columns.Add("TinhTrang");

            foreach (var b in qlcp.BanAns)
            {
                dt.Rows.Add(b.IDBanAn, b.TenBan, b.TinhTrang);
            }

            return dt;
        }

        public void ThayDoiTinhTrang(int idBan, bool b, ref string error)
        {
            //string strSQL = "";
            //if (b) strSQL = $"update BanAn set TinhTrang = N'Đã có người' where IDBanAn = {idBan}";
            //else strSQL = $"update BanAn set TinhTrang = N'Trống' where IDBanAn = {idBan}";
            //db.MyExecuteNonQuery(strSQL, CommandType.Text, ref error);

            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                var ba = (from ban in qlcp.BanAns
                          where ban.IDBanAn == idBan
                          select ban).SingleOrDefault();

                if (b)
                {
                    if (ba != null)
                    {
                        ba.TinhTrang = "Đã có người";
                        qlcp.SaveChanges();
                    }
                }
                else
                {
                    if (ba != null)
                    {
                        ba.TinhTrang = "Trống";
                        qlcp.SaveChanges();
                    }
                }
            }
            catch (Exception err)
            {
                error = err.Message;
            }
        }

        public bool ThemBanAn(string MaBan, string TenBan, string TinhTrang, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                BanAn banAn = new BanAn();
                banAn.IDBanAn = int.Parse(MaBan);
                banAn.TenBan = TenBan;
                banAn.TinhTrang = TinhTrang;

                qlcp.BanAns.Add(banAn);

                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        public bool SuaBanAn(string MaBan, string TenBan, string TinhTrang, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                int idBa = int.Parse(MaBan);
                var ba = (from b in qlcp.BanAns
                          where b.IDBanAn == idBa
                          select b).SingleOrDefault();
                //Khi không tìm thấy đối tượng không cần sửa.
                if (ba != null)
                {
                    ba.TenBan = TenBan;
                    ba.TinhTrang = TinhTrang;
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

        public bool XoaBanAn(string MaBan, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                int maban = int.Parse(MaBan);
                //Lấy địa chỉ của đối tượng có MaBan
                var ba = (from b in qlcp.BanAns
                          where b.IDBanAn == maban
                          select b).SingleOrDefault();
                //Khi không tìm thấy đối tượng không cần sửa.
                if (ba != null)
                {
                    qlcp.BanAns.Remove(ba);
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

    public class Ban
    {
        private int id;
        private string tenBan;
        private string tinhTrang;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string TenBan
        {
            get
            {
                return tenBan;
            }

            set
            {
                tenBan = value;
            }
        }

        public string TinhTrang
        {
            get
            {
                return tinhTrang;
            }

            set
            {
                tinhTrang = value;
            }
        }

        public Ban()
        {
            this.id = 0;
            this.tenBan = "Không có tên";
            this.tinhTrang = "Trống";
        }

        public Ban(int idBan, string tenban, string tinhtrang)
        {
            this.id = idBan;
            this.tenBan = tenban;
            this.tinhTrang = tinhtrang;
        }

        public Ban(DataRow row)
        {
            this.id = int.Parse(row.ItemArray[0].ToString());
            this.tenBan = row.ItemArray[1].ToString();
            this.tinhTrang = row.ItemArray[2].ToString();
        }

    }
}