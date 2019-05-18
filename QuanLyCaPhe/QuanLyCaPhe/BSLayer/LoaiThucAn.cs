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
    public class LoaiThucAn
    {
        DBMain dbMain = null;

        public LoaiThucAn()
        {
            dbMain = new DBMain();
        }

        public DataSet LayLoaiThucAn()
        {
            return dbMain.ExecuteQueryDataSet("select *from LoaiThucAn", CommandType.Text);
        }

        public DataSet LayDanhMuc()
        {
            return dbMain.ExecuteQueryDataSet("select *from LoaiThucAn", CommandType.Text);
        }
        public bool ThemDanhMuc(string MaDanhMuc, string TenDanhMuc, ref string error)
        {
            string sqlString;
            try
            {
                sqlString = $"Insert into LoaiThucAn values('{MaDanhMuc.Trim()}', N'{TenDanhMuc.Trim()}')";
            }
            catch (SqlException)
            {
                error = "Thêm không được";
                return false;
            }
            error = "Thêm thành công";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }
        public bool SuaDanhMuc(string MaDanhMuc, string TenDanhMuc, ref string error)
        {
            string sqlString;
            try
            {
                sqlString = "Update LoaiThucAn Set MaDanhMuc=N'" + MaDanhMuc + "',TenDanhMuc=N'" + TenDanhMuc + "'";
            }
            catch (SqlException)
            {
                error = "Sửa không được";
                return false;
            }
            error = "Sửa thành công";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }
        public bool XoaDanhMuc(string MaDanhMuc, ref string error)
        {
            string sqlString = $"delete from LoaiThucAn where MaThucAn = '{MaDanhMuc}'";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }
    }
}
