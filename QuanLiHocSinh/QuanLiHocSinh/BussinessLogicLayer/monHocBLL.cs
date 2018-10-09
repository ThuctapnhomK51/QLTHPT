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
    class monHocBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSMonHoc()
        {
            string sql = "SELECT * FROM MonHoc";
            return connData.GetDataTable(sql);
        }
        //Thêm một hàng hóa
        public bool themMonHoc(monHoc mh)
        {
            string[] param = { "@maMonHoc", "@tenMonHoc", "@soTiet", "@heSo" };
            object[] values = { mh.maMonHoc, mh.tenMonHoc, mh.soTiet, mh.heSo };

            string sql = "INSERT INTO MonHoc(maMonHoc,tenMonHoc,soTiet,heSo)"
                + "VALUES(@maMonHoc,@tenMonHoc,@soTiet,@heSo)";
            if (connData.ExxecuteNonQueryPara(sql, param, values))
                MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }
        public bool SuaMH(monHoc mh)
        {
            string[] param = { "@maMonHoc", "@tenMonHoc", "@soTiet", "@heSo" };
            object[] values = { mh.maMonHoc, mh.tenMonHoc, mh.soTiet, mh.heSo };

            string sql = "UPDATE MonHoc SET tenMonHoc=@tenMonHoc,soTiet=@soTiet,heSo=@heSo WHERE maMonHoc= @maMonHoc ";
            if (connData.ExxecuteNonQueryPara(sql, param, values))
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }
        public bool XoaMH(string mamh)
        {
            string sql = "DELETE FROM diemMonHoc WHERE maMonHoc in ('" + mamh + "')"
                    + "DELETE FROM monHoc WHERE maMonHoc in ('" + mamh + "')";
                if (connData.ExecuteQuyery(sql))
                {
                    MessageBox.Show("Xóa thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            return false;
        }
        public string NextID()
        {
            string lastID = connData.GetLastID("monHoc", "maMonHoc");
            return autoID.NextID(lastID, "MH");
        }
        public bool CheckExits(string mh)
        {
            if (connData.CheckExitsValue("monHoc", "maMonHoc", mh))
                return true;
            return false;
        }
    }
}
