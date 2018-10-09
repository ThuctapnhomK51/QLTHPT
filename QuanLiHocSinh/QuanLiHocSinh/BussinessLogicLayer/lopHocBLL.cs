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
    class lopHocBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSLop()
        {
            string sql = "SELECT * FROM lopHoc ";
            return connData.GetDataTable(sql);
        }
        public DataTable layDSLop2()
        {
            string sql = "SELECT maLop,tenLop FROM lopHoc ";
            return connData.GetDataTable(sql);
        }
        public DataTable layDSLop1(string ml)
        {
            string sql = "SELECT maLop,tenLop,maKhoi,siSo FROM lopHoc WHERE maLop='"+ml+"' ";
            return connData.GetDataTable(sql);
        }

        public bool XoaLop(string malop,string ss)
        {
            if (ss.Equals("0"))
            {
                string sql = "DELETE FROM diemMonHoc WHERE maLop in ('" + malop + "')"
                    + "DELETE FROM diemTBMonChungHocKy WHERE maLop in ('" + malop + "')"
                    + "DELETE FROM diemTBChungCaNam WHERE maLop in ('" + malop + "')"
                    + "DELETE FROM hocSinh WHERE maLop in ('" + malop + "')"
                    + "DELETE FROM lopHoc WHERE maLop in ('" + malop + "')";

                if (connData.ExecuteQuyery(sql))
                {
                    MessageBox.Show("Xóa thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Không thể xóa vì có học sinh", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return false;
        }
        //Thêm một hàng hóa
        public bool themLop(lopHoc l)
        {
            string[] param = { "@maLop", "@tenLop", "@maKhoi", "@siSo"};
            object[] values = { l.maLop, l.tenLop, l.maKhoi, l.siSo };

                string sql = "INSERT INTO lopHoc(maLop,tenLop,maKhoi,siSo)"
                    + "VALUES(@maLop,@tenLop,@maKhoi,@siSo)";
                if (connData.ExxecuteNonQueryPara(sql, param, values)) 
                    MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
        }

        //Sửa một hàng hóa
        public bool SuaLop(lopHoc l)
        {
            string[] param = { "@maLop", "@tenLop", "@maKhoi", "@siSo" };
            object[] values = { l.maLop, l.tenLop, l.maKhoi, l.siSo };

            string sql = "UPDATE lopHoc SET tenLop=@tenLop,"
            + "maKhoi=@maKhoi WHERE maLop= @maLop ";
            if (connData.ExxecuteNonQueryPara(sql, param, values))
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
        }

        public string NextID()
        {
            string lastID = connData.GetLastID("lopHoc", "maLop");
            return autoID.NextID(lastID, "L");
        }
        public bool CheckExits(string ml)
        {
            if (connData.CheckExitsValue("lopHoc", "maLop", ml))
                return true;
            return false;
        }
    }
}
