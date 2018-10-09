using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QuanLiHocSinh.DataAccessLayer;
using QuanLiHocSinh.BussinessObject;
using System.Windows.Forms;

namespace QuanLiHocSinh.BussinessLogicLayer
{
    class namHocBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSNamHoc()
        {
            string sql = "SELECT maNamHoc, tenNamHoc FROM namHoc ";
            return connData.GetDataTable(sql);
        }
    }
}
