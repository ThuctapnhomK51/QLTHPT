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
    class hanhKiemBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSHanhKiem()
        {
            string sql = "SELECT maHanhKiem,tenHanhKiem FROM HanhKiem";
            return connData.GetDataTable(sql);
        }
    }
}
