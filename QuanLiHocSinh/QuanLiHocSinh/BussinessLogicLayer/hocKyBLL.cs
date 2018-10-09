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
    class hocKyBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSHocKy()
        {
            string sql = "SELECT maHocKy, tenHocKy FROM hocKy ";
            return connData.GetDataTable(sql);
        }
    }
}
