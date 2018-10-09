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
    class diemTBMonHocKyBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSDiemMH(string lop,string hs)
        {
            string sql = "SELECT STT,tenLop,diemTBMonChungHocKy.maHS,hoTen,diemTBHocKy,hocLuc,tenHanhKiem,tenHocKy,namHoc FROM diemTBMonChungHocKy,lopHoc,HocSinh,hocKy,HanhKiem WHERE diemTBMonChungHocKy.maLop=lopHoc.maLop and diemTBMonChungHocKy.maHS=hocSinh.maHS and diemTBMonChungHocKy.maHocKy=hocKy.maHocKy and HanhKiem.maHanhKiem=diemTBMonChungHocKy.maHanhKiem and diemTBMonChungHocKy.maLop='" + lop + "' and diemTBMonChungHocKy.maHS='"+hs+"'";
            return connData.GetDataTable(sql);
        }
        //Thêm một hàng hóa
        public bool themDiemTBHocKy(diemTBMonChungHocKy dhk)
        {
            string sql = string.Format("INSERT INTO diemTBMonChungHocKy(STT,maLop,maHS,"
                + "diemTBHocKy,hocLuc,maHanhKiem,maHocKy,namHoc)"
                + "VALUES(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}')",
                dhk.STT, dhk.maLop, dhk.maHS,
                dhk.diemTBHocKy, dhk.hocLuc, dhk.hanhKiem, dhk.maHocKy, dhk.namHoc);
            if (connData.ExecuteQuyery(sql))
                MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
            //string[] param = { "@STT", "@maLop", "@maHS", "@maHanhKiem", "@maHocKy", "@namHoc" };
            //object[] values = { dhk.STT, dhk.maLop, dhk.maHS,dhk.hanhKiem, dhk.maHocKy, dhk.namHoc };


            //string sql = "INSERT INTO diemTBMonChungHocKy(STT,maLop,maHS,"
            //    + "maHanhKiem,maHocKy,namHoc)"
            //    + "VALUES(@STT,@maLop,@maHS,@maHanhKiem,@maHocKy,@namHoc)";
            //if (connData.ExxecuteNonQueryPara(sql, param, values))
            //    MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //return true;

        }
        public bool SuaDiemTBHK(diemTBMonChungHocKy dhk)
        {
            string sql = string.Format("UPDATE diemTBMonChungHocKy SET maLop=N'{0}',maHS=N'{1}',diemTBHocKy=N'{2}',hocLuc=N'{3}',"
            + "maHanhKiem=N'{4}',maHocKy=N'{5}',namHoc=N'{6}' WHERE STT= N'{7}' ",
                 dhk.maLop, dhk.maHS,
                dhk.diemTBHocKy, dhk.hocLuc, dhk.hanhKiem, dhk.maHocKy, dhk.namHoc, dhk.STT);
            if (connData.ExecuteQuyery(sql))
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
            //string[] param = { "@STT", "@maLop", "@maHS", "@maHanhKiem", "@maHocKy", "@namHoc" };
            //object[] values = { dhk.STT, dhk.maLop, dhk.maHS, dhk.hanhKiem, dhk.maHocKy, dhk.namHoc };

            //string sql ="UPDATE diemTBMonChungHocKy SET maLop=@maLop,maHS=@maHS,"
            //+ "maHanhKiem=@maHanhKiem,maHocKy=@maHocKy,namHoc=@namHoc WHERE STT= @STT ";
            // if (connData.ExxecuteNonQueryPara(sql, param, values))
            //    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //return true;

        }
        public bool XoaDiemDTHK(string dmh)
        {
            string sql = "DELETE FROM diemTBMonChungHocKy WHERE STT in ('" + dmh + "')";
            if (connData.ExecuteQuyery(sql))
            {
                MessageBox.Show("Xóa thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        public string NextID()
        {
            string lastID = connData.GetLastID("diemTBMonChungHocKy", "STT");
            return autoID.NextID(lastID, "");
        }
        public bool CheckExits(string dmh)
        {
            if (connData.CheckExitsValue("diemTBMonChungHocKy", "STT", dmh))
                return true;
            return false;
        }
    }
}
