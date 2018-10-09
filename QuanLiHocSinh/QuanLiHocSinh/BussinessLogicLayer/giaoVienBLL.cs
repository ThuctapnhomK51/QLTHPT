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
    class giaoVienBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSGiaoVien()
        {
            string sql = "SELECT maGV, hoTen,chuyenMon, sDT, Email,diaChi FROM GiaoVien";
            return connData.GetDataTable(sql);
        }
        public DataTable layDSGiaoVien1()
        {
            string sql = "SELECT maGV, hoTen,ghiChu FROM GiaoVien";
            return connData.GetDataTable(sql);
        }
        //public DataTable layDSGiaoVien1(string note)
        //{
        //    string sql = "SELECT maGV, hoTen,ghiChu FROM GiaoVien WHERE ghiChu=N'" + note + "'";
        //    return connData.GetDataTable(sql);
        //}
        public bool XoaGV(string magv)
        {

            string sql = "DELETE FROM giaoVien WHERE  maGV in ('" + magv + "')";
                    
                if (connData.ExecuteQuyery(sql))
                {
                    MessageBox.Show("Xóa thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            return false;
        }
        //Thêm một hàng hóa
        public bool themGV(giaoVien gv)
        {

            string sql = string.Format("INSERT INTO GiaoVien(maGV,hoTen,chuyenMon,SDT,Email,diaChi)"
                    + "VALUES(N'{0}',N'{1}',N'{2}','{3}',N'{4}',N'{5}')",
                    gv.maGV, gv.hoTen, gv.chuyenMon, gv.SDT, gv.EMail, gv.diaChi);
                if (connData.ExecuteQuyery(sql))
                    MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;

        }
        //Sửa một hàng hóa
        public bool SuaGV(giaoVien gv)
        {

                string sql = string.Format("UPDATE giaoVien SET hoTen=N'{0}',chuyenMon=N'{1}',"
                + "SDT=N'{2}',Email='{3}',diaChi='{4}' WHERE maGV= N'{5}' ",
                     gv.hoTen, gv.chuyenMon, gv.SDT,gv.EMail,gv.diaChi, gv.maGV);
                if (connData.ExecuteQuyery(sql))
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;

        }
        //Xóa một hàng hóa

        public string NextID()
        {
            string lastID = connData.GetLastID("giaoVien", "maGV");
            return autoID.NextID(lastID, "GV");
        }
        public bool CheckExits(string mhs)
        {
            if (connData.CheckExitsValue("GiaoVien", "maGV", mhs))
                return true;
            return false;
        }
        public bool CheckExits1(string mhs)
        {
            if (connData.CheckExitsValue("GiaoVien", "Email", mhs))
                return true;
            return false;
        }
        public bool CheckExits2(string mhs)
        {
            if (connData.CheckExitsValue("GiaoVien", "SDT", mhs))
                return true;
            return false;
        }
    }
}
