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
    class khoiHocBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSKhoi()
        {
            string sql = "SELECT maKhoi, tenkhoi FROM khoiHoc ";
            return connData.GetDataTable(sql);
        }
    }
}
