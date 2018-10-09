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
    class diemTBChungCaNamBLL
    {
        ConnectData connData = new ConnectData();
        public DataTable layDSDiemCaNam(string lop)
        {
            string sql = "SELECT STT,tenLop,diemTBChungCaNam.maHS,hoTen,diemTBMonCN,hocLuc,tenHanhKiem,ketQua,danhHieu,namHoc FROM diemTBChungCaNam,lopHoc,HocSinh,HanhKiem WHERE diemTBChungCaNam.maLop=lopHoc.maLop and diemTBChungCaNam.maHS=hocSinh.maHS and HanhKiem.maHanhKiem=diemTBChungCaNam.maHanhKiem and diemTBChungCaNam.maLop='"+lop+"'";
            return connData.GetDataTable(sql);
        }
        public bool themDiemCaNam(diemTBChungCaNam dcn)
        {
            string sql = string.Format("INSERT INTO diemTBChungCaNam(STT,maLop,maHS,"
                + "diemTBMonCN,hocLuc,ketQua,danhHieu,namHoc)"
                + "VALUES(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}')",
                dcn.STT, dcn.maLop, dcn.maHS,
                dcn.diemTBMonCN, dcn.hocLuc, dcn.ketQua, dcn.danhHieu, dcn.namHoc);
            if (connData.ExecuteQuyery(sql))
                MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
            //string[] param = { "@STT", "@maLop", "@maHS", "@namHoc" };
            //object[] values = {dcn.STT, dcn.maLop, dcn.maHS, dcn.namHoc };

            //string sql = "INSERT INTO diemTBChungCaNam(STT,maLop,maHS,namHoc)"
            //    + "VALUES(@STT,@maLop,@maHS,@namHoc)";

            //if (connData.ExxecuteNonQueryPara(sql, param, values))
            //    MessageBox.Show("Thêm thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //return true;

        }
        public bool SuaDiemCaNam(diemTBChungCaNam dhk)
        {
            string sql = string.Format("UPDATE diemTBChungCaNam SET maLop=N'{0}',maHS=N'{1}',diemTBMonCN=N'{2}',hocLuc=N'{3}',"
           + "ketQua=N'{4}',danhHieu=N'{5}',namHoc=N'{6}' WHERE STT= N'{7}' ",
                dhk.maLop, dhk.maHS,
               dhk.diemTBMonCN, dhk.hocLuc, dhk.ketQua, dhk.danhHieu, dhk.namHoc, dhk.STT);
            if (connData.ExecuteQuyery(sql))
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

            //string[] param = { "@STT", "@maLop", "@maHS", "@namHoc" };
            //object[] values = { dcn.STT, dcn.maLop, dcn.maHS, dcn.namHoc };

            //string sql = "UPDATE diemTBChungCaNam SET maLop=@maLop,maHS=@maHS,diemTBMonCN=@namHoc WHERE STT= @STT ";
            //if (connData.ExxecuteNonQueryPara(sql, param, values))
            //    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //return true;

        }
        public bool XoaDiemCaNam(string dmh)
        {
            string sql = "DELETE FROM diemTBChungCaNam WHERE STT in ('" + dmh + "')";
            if (connData.ExecuteQuyery(sql))
            {
                MessageBox.Show("Xóa thành công", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        public string NextID()
        {
            string lastID = connData.GetLastID("diemTBChungCaNam", "STT");
            return autoID.NextID(lastID, "");
        }
        public bool CheckExits(string dmh)
        {
            if (connData.CheckExitsValue("diemTBChungCaNam", "STT", dmh))
                return true;
            return false;
        }
    }
}
