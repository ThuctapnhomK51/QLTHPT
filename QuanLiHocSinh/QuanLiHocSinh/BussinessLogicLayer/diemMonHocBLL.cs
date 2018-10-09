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
    class diemMonHocBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSDiemMH(string hk,string lop,string hs)
        {
            string sql = "SELECT STT,tenLop,diemMonHoc.maHS,hoTen,tenMonHoc,diemMieng,diem15P,diem45P,diemThi,diemTBMon,tenHocKy,namHoc,diemMonHoc.ghiChu FROM diemMonHoc,lopHoc,HocSinh,hocKy,monHoc WHERE diemMonHoc.maLop=lopHoc.maLop and diemMonHoc.maHS=hocSinh.maHS and diemMonHoc.maHocKy=hocKy.maHocKy and diemMonHoc.maMonHoc=monHoc.maMonHoc and diemMonHoc.maHocKy='" + hk + "' and diemMonHoc.maLop='" + lop + "' and diemMonHoc.maHS='"+hs+"' ";
            return connData.GetDataTable(sql);
        }
        //Thêm một hàng hóa
        public bool themDiemMonHoc(diemMonHoc dmh)
        {

                string[] param = { "@STT", "@maLop", "@maHS", "@maMonHoc", "@diemMieng", "@diem15P", "@diem45P", "@diemThi", "@diemTBMon", "@maHocKy", "@namHoc" };
                object[] values = { dmh.STT, dmh.maLop, dmh.maHS, dmh.maMonHoc, dmh.diemMieng, dmh.diem15P, dmh.diem45P, dmh.diemThi, dmh.diemTBMon, dmh.maHocKy, dmh.namHoc };

                string sql = "INSERT INTO diemMonHoc(STT,maLop,maHS,maMonHoc,diemMieng,"
                    + "diem15P,diem45P,diemThi,diemTBMon,maHocKy,namHoc)"
                    + "VALUES(@STT,@maLop,@maHS,@maMonHoc,@diemMieng,@diem15P,@diem45P,@diemThi,@diemTBMon,@maHocKy,@namHoc)";

                if (connData.ExxecuteNonQueryPara(sql, param, values))
                    MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }
        //Sửa một hàng hóa
        public bool SuaDiemMonHoc(diemMonHoc dmh)
        {
            string[] param = { "@STT", "@maLop", "@maHS", "@maMonHoc", "@diemMieng", "@diem15P", "@diem45P", "@diemThi", "@diemTBMon", "@maHocKy", "@namHoc" };
            object[] values = { dmh.STT, dmh.maLop, dmh.maHS, dmh.maMonHoc, dmh.diemMieng, dmh.diem15P, dmh.diem45P, dmh.diemThi, dmh.diemTBMon, dmh.maHocKy, dmh.namHoc };


            string sql = "UPDATE diemMonHoc SET maLop=@maLop,maHS=@maHS,maMonHoc=@maMonHoc,diemMieng=@diemMieng,"
            + "diem15P=@diem15P,diem45P=@diem45P,diemThi=@diemThi,diemTBMon=@diemTBMon,maHocKy=@maHocKy,namHoc=@namHoc WHERE STT= @STT ";
            if (connData.ExxecuteNonQueryPara(sql, param, values))
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }
        public bool XoaDiemMonHoc(string dmh)
        {
            string sql = "DELETE FROM diemMonHoc WHERE STT in ('" + dmh + "')";
            if (connData.ExecuteQuyery(sql))
            {
                MessageBox.Show("Xóa thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        public string NextID()
        {
            string lastID = connData.GetLastID("diemMonHoc", "STT");
            return autoID.NextID(lastID, "");
        }
        public bool CheckExits(string dmh)
        {
            if (connData.CheckExitsValue("diemMonHoc", "STT", dmh))
                return true;
            return false;
        }
        public bool CheckExits1(string dmh)
        {
            if (connData.CheckExitsValue("diemMonHoc", "maHS", dmh))
                return true;
            return false;
        }
        public bool CheckExits2(string dmh)
        {
            if (connData.CheckExitsValue("diemMonHoc", "maMonHoc", dmh))
                return true;
            return false;
        }
        public bool CheckExits3(string dmh)
        {
            if (connData.CheckExitsValue("diemMonHoc", "maHocKy", dmh))
                return true;
            return false;
        }

    }
}
