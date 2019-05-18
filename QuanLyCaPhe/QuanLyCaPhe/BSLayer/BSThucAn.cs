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
    class BSThucAn
    {
        DBMain dbMain = null;
        public BSThucAn()
        {
            dbMain = new DBMain();
        }

        public DataTable LayThucAn()
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            var ta = from b in qlcp.ThucAns
                     select b;

            DataTable dt = new DataTable();
            dt.Columns.Add("IDThucAn");
            dt.Columns.Add("TenThucAn");
            dt.Columns.Add("IDloaithucan");
            dt.Columns.Add("Gia");


            foreach (var b in qlcp.ThucAns)
            {
                dt.Rows.Add(b.IDThucAn, b.TenThucAn, b.IDLoaiThucAn, b.Gia);
            }

            return dt;
        }

        public DataTable LayThucAnTheoLoai(string tenLoaiThucAn)
        {
            QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
            var ta = from a in qlcp.ThucAns
                     join b in qlcp.LoaiThucAns on
                    a.IDLoaiThucAn equals b.IDLoaiThucAn
                     where b.TenLoaiThucAn == tenLoaiThucAn
                     select a;

            DataTable dt = new DataTable();
            dt.Columns.Add("IDThucAn");
            dt.Columns.Add("TenThucAn");
            dt.Columns.Add("IDLoaiThucAn");
            dt.Columns.Add("Gia");

            foreach (var thucan in ta)
            {
                dt.Rows.Add(thucan.IDLoaiThucAn, thucan.TenThucAn, thucan.Gia);
            }

            return dt;
        }


        public bool ThemThucAn(string MaThucAn, string DanhMuc, string Gia, string TenMon, ref string error)
        {
            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                ThucAn ta = new ThucAn();
                ta.IDThucAn = int.Parse(MaThucAn);
                ta.TenThucAn = TenMon;
                ta.Gia = int.Parse(Gia);

                qlcp.ThucAns.Add(ta);

                qlcp.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }
        public bool SuaThucAn(string MaThucAn, string DanhMuc, string Gia, string TenMon, ref string error)
        {
            try
            {
                int idthucan = int.Parse(MaThucAn);
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                var ta = (from b in qlcp.ThucAns
                          where b.IDThucAn == idthucan
                          select b).SingleOrDefault();
                //Khi không tìm thấy đối tượng không cần sửa.
                if (ta != null)
                {
                    ta.TenThucAn = TenMon;
                    ta.Gia = int.Parse(Gia);
                    ta.IDLoaiThucAn = int.Parse(DanhMuc);
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
        public bool XoaThucAn(string MaThucAn, ref string error)
        {
            try
            {
                int idthucan = int.Parse(MaThucAn);
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                var ta = (from b in qlcp.ThucAns
                          where b.IDThucAn == idthucan
                          select b).SingleOrDefault();
                //Khi không tìm thấy đối tượng không cần sửa.
                if (ta != null)
                {
                    qlcp.ThucAns.Remove(ta);
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

        public int TimIDThucAn(string tenThucAn, ref string error)
        {
            //string strSQL = $"select IDThucAn from ThucAn where TenThucAn = N'{tenThucAn}'";
            //return (int)dbMain.FirstRowQuery(strSQL, CommandType.Text, ref error);

            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                var ta = (from b in qlcp.ThucAns
                          where b.TenThucAn == tenThucAn
                          select b.IDThucAn).SingleOrDefault();
                return ta;

            }
            catch (Exception err)
            {
                error = err.Message;
                return -1;
            }
        }
        public DataTable TimKimF(string TenThucAn)
        {
            //return dbMain.ExecuteQueryDataSet($"select *from ThucAn where TenThucAn like N'%{TenThucAn}%'", CommandType.Text);

            try
            {
                QuanLyCaPheEntities qlcp = new QuanLyCaPheEntities();
                //Lấy địa chỉ của đối tượng có MaBan
                var dsta = from b in qlcp.ThucAns
                           where b.TenThucAn.Contains(TenThucAn)
                           select b;

                DataTable dt = new DataTable();
                dt.Columns.Add("IDThucAn");
                dt.Columns.Add("TenThucAn");
                dt.Columns.Add("IDloaithucan");
                dt.Columns.Add("Gia");


                foreach (var b in dsta)
                {
                    dt.Rows.Add(b.IDThucAn, b.IDLoaiThucAn, b.TenThucAn, b.Gia);
                }

                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}