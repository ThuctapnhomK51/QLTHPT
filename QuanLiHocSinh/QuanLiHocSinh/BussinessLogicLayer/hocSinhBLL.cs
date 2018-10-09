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
    class hocSinhBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSHocSinh()
        {
            string sql = "SELECT maHS,hocSinh.MaLop,tenLop,hoTen,gioiTinh,ngaySinh,danToc,diaChi,hoTenMe,hoTenBo,SDT,ghiChu FROM HocSinh,lopHoc WHERE hocSinh.maLop=lopHoc.maLop";
            return connData.GetDataTable(sql);
        }
        public DataTable layDSHocSinh2()
        {
            string sql = "SELECT * FROM HocSinh";
            return connData.GetDataTable(sql);
        }
        public DataTable layDSHocSinh1(string malop)
        {
            string sql = "SELECT maHS, hoTen FROM HocSinh WHERE maLop=N'"+malop+"'";
            return connData.GetDataTable(sql);
        }
        public DataTable TimKiemHS(string tenhs)
        {
            string sql = "SELECT maHS,hocSinh.MaLop,tenLop,hoTen,gioiTinh,ngaySinh,danToc,diaChi,hoTenMe,hoTenBo,SDT,ghiChu FROM HocSinh,lopHoc WHERE hocSinh.maLop=lopHoc.maLop and hoTen like '%" + tenhs + "%'";

            //string sql = "Select * from HocSinh where hoTen like '%" + tenhs + "%'";
            return connData.GetDataTable(sql);
        }
        //public DataTable TimKiemHH(HangHoaPublic p)
        //{
        //    int nparemeter = 1;
        //    string[] name = new string[nparemeter];
        //    object[] value = new object[nparemeter];
        //    name[0] = "@mahh";
        //    value[0] = p.Mahh;
        //    return connData.GetDataTable("SELECT Mahh,Tenhh,Loaihh,Nhasx,Donvitinh,Gia,Soluong FROM HangHoa WHERE Mahh like '%" + p.Mahh + "%' ");
        //}
        public DataTable TimKiemTheolop(string malop)
        {
            string sql = "SELECT maHS,hocSinh.MaLop,tenLop,hoTen,gioiTinh,ngaySinh,danToc,diaChi,hoTenMe,hoTenBo,SDT,ghiChu FROM HocSinh,lopHoc WHERE hocSinh.maLop=lopHoc.maLop and hocSinh.MaLop like '%" + malop + "%'";

            //string sql = "Select * from HocSinh where hocSinh.MaLop = '" + malop + "'";
            return connData.GetDataTable(sql);
        }
        public bool XoaHS(string mahs)
        {

            string sql = "DELETE FROM diemMonHoc WHERE maHS in ('" + mahs + "')"
                    + "DELETE FROM diemTBMonChungHocKy WHERE maHS in ('" + mahs + "')"
                    + "DELETE FROM diemTBChungCaNam WHERE maHS in ('" + mahs + "')"
                    + "DELETE FROM HocSinh WHERE maHS in ('" + mahs + "')";
                if (connData.ExecuteQuyery(sql))
                {
                    MessageBox.Show("Xóa thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                return false;
        }
        
        //Thêm một hàng hóa
        public bool themHS(hocSinh hs)
        {
            string[] param = { "@maHS", "@maLop", "@hoTen", "@gioiTinh", "@ngaySinh", "@danToc", "@diaChi", "@hoTenMe", "@hoTenBo", "@SDT", "@ghiChu" };
            object[] values = { hs.maHS, hs.maLop, hs.hoTen, hs.gioiTinh, hs.ngaySinh, hs.danToc, hs.diaChi, hs.hoTenMe, hs.hoTenBo, hs.SDT, hs.ghiChu };

            string sql = "INSERT INTO hocSinh(maHS,maLop,hoTen,gioiTinh,ngaySinh,danToc,diaChi,hoTenMe,hoTenBo,SDT,ghiChu)"
                    + "VALUES(@maHS,@maLop,@hoTen,@gioiTinh,@ngaySinh,@danToc,@diaChi,@hoTenMe,@hoTenBo,@SDT,@ghiChu)";

            if (connData.ExxecuteNonQueryPara(sql, param, values))
                MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        //Sửa một hàng hóa
        public bool SuaHS(hocSinh hs)
        {
            string[] param = { "@maHS", "@maLop", "@hoTen", "@gioiTinh", "@ngaySinh", "@danToc", "@diaChi", "@hoTenMe", "@hoTenBo", "@SDT", "@ghiChu" };
            object[] values = { hs.maHS, hs.maLop, hs.hoTen, hs.gioiTinh, hs.ngaySinh, hs.danToc, hs.diaChi, hs.hoTenMe, hs.hoTenBo, hs.SDT, hs.ghiChu };

            string sql = "UPDATE hocSinh SET maLop=@maLop, hoTen=@hoTen,gioiTinh=@gioiTinh,"
                + "ngaySinh=@ngaySinh,danToc=@danToc,diaChi=@diaChi,hoTenMe=@hoTenMe,hoTenBo=@hoTenBo,SDT=@SDT,ghiChu=@ghiChu WHERE maHS= @maHS ";
            if (connData.ExxecuteNonQueryPara(sql, param, values))
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }
        public bool SuaHS1(hocSinh hs)
        {
            string[] param = { "@maHS", "@maLop", "@hoTen" };
            object[] values = { hs.maHS, hs.maLop, hs.hoTen };

            string sql = "UPDATE hocSinh SET maLop=@maLop, hoTen=@hoTen WHERE maHS= @maHS ";
            if (connData.ExxecuteNonQueryPara(sql, param, values))
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            { MessageBox.Show("Sỉ số lớp đã đầy", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            return true;

        }
        //Xóa một hàng hóa
        
        public string NextID()
        {
            string lastID = connData.GetLastID("hocSinh", "maHS");
            return autoID.NextID(lastID, "HS");
        }
        public bool CheckExits(string mhs)
        {
            if (connData.CheckExitsValue("HocSinh", "maHS", mhs))
                return true;
            return false;
        }
        public bool CheckExits1(string mhs)
        {
            if (connData.CheckExitsValue("HocSinh", "SDT", mhs))
                return true;
            return false;
        }
    }
}
