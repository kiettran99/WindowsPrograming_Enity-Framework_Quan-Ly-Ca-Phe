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
    class DangNhap
    {
        DBMain dbMain = null;
        public DangNhap()
        {
            dbMain = new DBMain();
        }

        public bool KiemTra(string tk,string mk,ref string err,ref string MaNV)
        {            
                string sqlstring = "Select Count(*) From DangNhap Where TaiKhoan = '" + tk + "'" +  "AND MatKhau='" + mk + "'";
            if (dbMain.CheckThongTin(sqlstring, CommandType.Text, ref err) == false)
            {
                err = "Thất Bại";
                return false;
            }
            else
            {
                err = "Thành Công";
                sqlstring = "Select MaNV From DangNhap where TaiKhoan='" + tk + "'";
                dbMain.LayMa(sqlstring, CommandType.Text,ref  MaNV);

            }

            return true;
        }

        public DataSet LayTK()
        {
            return dbMain.ExecuteQueryDataSet("select *from DangNhap", CommandType.Text);
        }


        public bool ThemTK(string TenTK, string Pass, string MaNV, ref string error)
        {
            string sqlString;
            try
            {
                sqlString = $"Insert into DangNhap values('{TenTK}', N'{Pass}', N'{MaNV}')";
            }
            catch (SqlException)
            {
                error = "Thêm không được";
                return false;
            }
            error = "Thêm thành công";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }
        public bool XoaTK(string MaNV, ref string error)
        {
            string sqlString = $"delete from DangNhap where MaNV = '{MaNV}'";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }
        public bool RenewTK(string MaNV, ref string error)
        {
            string sqlString;
            try
            {
                sqlString = "Update DangNhap Set MatKhau=N'" + 123 +
                  "' Where MaNV= '" + MaNV + "'";
            }
            catch (SqlException)
            {
                error = "Sửa không được";
                return false;
            }
            error = "Sửa thành công";
            return dbMain.MyExecuteNonQuery(sqlString, CommandType.Text, ref error);
        }

    }
}
