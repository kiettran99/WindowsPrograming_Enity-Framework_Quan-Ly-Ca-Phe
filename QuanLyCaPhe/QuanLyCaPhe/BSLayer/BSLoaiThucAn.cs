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
    public class BSLoaiThucAn
    {
        DBMain dbMain = null;

        public BSLoaiThucAn()
        {
            dbMain = new DBMain();
        }

        public DataTable LayLoaiThucAn()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            var lta = from b in qlcp.LoaiThucAns
                      select b;

            DataTable dt = new DataTable();
            dt.Columns.Add("IDLoaiThucAn");
            dt.Columns.Add("TenLoaiThucAn");


            foreach (var b in qlcp.LoaiThucAns)
            {
                dt.Rows.Add(b.IDLoaiThucAn, b.TenLoaiThucAn);
            }

            return dt;
        }

        public DataTable LayDanhMuc()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            var lta = from b in qlcp.LoaiThucAns
                      select b;

            DataTable dt = new DataTable();
            dt.Columns.Add("IDLoaiThucAn");
            dt.Columns.Add("TenLoaiThucAn");


            foreach (var b in qlcp.LoaiThucAns)
            {
                dt.Rows.Add(b.IDLoaiThucAn, b.TenLoaiThucAn);
            }

            return dt;
        }
        public bool ThemDanhMuc(string MaDanhMuc, string TenDanhMuc, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                LoaiThucAn lta = new LoaiThucAn();
                lta.IDLoaiThucAn = int.Parse(MaDanhMuc);
                lta.TenLoaiThucAn = TenDanhMuc;


                qlcp.LoaiThucAns.Add(lta);

                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }
        public bool SuaDanhMuc(string MaDanhMuc, string TenDanhMuc, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                var lta = (from b in qlcp.LoaiThucAns
                           where b.IDLoaiThucAn == int.Parse(MaDanhMuc)
                           && b.TenLoaiThucAn == TenDanhMuc
                           select b).SingleOrDefault();
                //Khi không tìm thấy đối tượng không cần sửa.
                if (lta != null)
                {
                    lta.TenLoaiThucAn = TenDanhMuc;
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
        public bool XoaDanhMuc(string MaDanhMuc, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                LoaiThucAn lta = new LoaiThucAn();

                lta.IDLoaiThucAn = int.Parse(MaDanhMuc);

                qlcp.LoaiThucAns.Add(lta);
                qlcp.LoaiThucAns.Remove(lta);

                qlcp.SaveChanges();

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
