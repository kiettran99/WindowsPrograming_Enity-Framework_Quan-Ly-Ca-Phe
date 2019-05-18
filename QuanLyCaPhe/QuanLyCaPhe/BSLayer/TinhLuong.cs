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
    class TinhLuong
    {
        DBMain dbMain = null;

        public TinhLuong()
        {
            dbMain = new DBMain();
        }

        public DataSet LayTTTL()
        {
            return dbMain.ExecuteQueryDataSet("select * from TinhLuong", CommandType.Text);
        }
        public bool ThemNhanVien(string MaNV, string TenNV, ref string error)
        {
            string sqlString;
            sqlString = $"Insert into TinhLuong values('{MaNV}', N'{TenNV}',N'{0}',N'{0}')";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

        public bool TinhLuongNhanVien(string MaNV, float sogiolam,float luong, ref string error)
        {
            string sqlString;
            try
            {
                sqlString = "Update TinhLuong Set SoGioLam=N'" + sogiolam + "',Luong=N'" + luong + "' Where MaNV= '" + MaNV + "'";
            }
            catch (SqlException)
            {
                error = "Tính lương không được";
                return false;
            }
            error = "Tính lương thành công";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

        public bool ReStartL(string MaNV,ref string error)
        {
            string sqlString = "Update TinhLuong Set SoGioLam=N'" + 0+ "',Luong =N'" + 0 + "' Where MaNV= '" + MaNV + "'";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);

        }
        
        public void LayDuLieu(string MaNV,ref TimeSpan dl1,ref TimeSpan dl2)
        {
             string sqlString1 = "Select GioIn from ChamCong where MaNV ='" + MaNV + "'";

             string sqlString2 = "Select GioOut from ChamCong where MaNV ='" + MaNV + "'";
            dbMain.LayTime(sqlString1,sqlString2, CommandType.Text, ref dl1,ref dl2);
            
            return;
        }
        public bool XoaNV(string MaNV,ref string error)
        {
            string sqlString = $"delete from TinhLuong where MaNV = '{MaNV}'";

            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

        public void LaySoTime(string MaNV, ref float SoTime)
        {
            string sqlString = "Select SoGioLam from TinhLuong where MaNV ='" + MaNV + "'";
            try { dbMain.LaySoTime(sqlString, CommandType.Text, ref SoTime); }
            catch { }
            return;
        }
    }
}
