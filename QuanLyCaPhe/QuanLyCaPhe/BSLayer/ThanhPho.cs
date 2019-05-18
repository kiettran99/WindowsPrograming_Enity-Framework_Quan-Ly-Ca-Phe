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
    class ThanhPho
    {
        DBMain dbMain = null;
        public ThanhPho()
        {
            dbMain = new DBMain();
        }

        public DataSet LayThanhPho()
        {
            return dbMain.ExecuteQueryDataSet("select *from ThanhPho", CommandType.Text);
        }
        public bool ThemThanhPho(string TenTP, ref string error)
        {
            Random rd = new Random();
            int num ;
            num = rd.Next(10, 100);
            string sqlString;
            try
            {
                sqlString = "Insert Into ThanhPho Values(" + "N'" + num + "',N'" + TenTP +  "')";
                
            }
            catch (SqlException err)
            {
                error = err.Message;
                return false;
            }
            error = "Thêm thành công";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

        public bool SuaThanhPho(string TenTP,string MaTP, ref string error)
        {
            string sqlString;
            try
            {
                sqlString = "Update ThanhPho Set TenThanhPho=N'" + TenTP + "' Where MaThanhPho= '" + MaTP + "'";
            }
            catch (SqlException err)
            {
                error = err.Message;
                return false;
            }
            error = "Sửa thành công";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

        public bool Xoa(ref string error, string MaTP)
        {
            string sqlString;
            try
            {
                sqlString = "Delete From ThanhPho Where MaThanhPho='" + MaTP + "'";
            }
            catch (SqlException err)
            {
                error = err.Message;
                return false;
            }
            error = "Xóa thành công";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

    }
}
