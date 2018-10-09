using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLiHocSinh.DataAccessLayer;
using QuanLiHocSinh.BussinessObject;
namespace QuanLiHocSinh
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
       
       

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmDiemTBMonHocKy frm = new frmDiemTBMonHocKy();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);

        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmDiemMonHoc frm = new frmDiemMonHoc();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmLopHoc frm = new frmLopHoc();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmMonHoc frm = new frmMonHoc();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmHocSinh frm = new frmHocSinh();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmGiaoVien frm = new frmGiaoVien();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void tổngKếtCảNămToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmDiemCaNam frm = new frmDiemCaNam();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmDiemMonHoc frm = new frmDiemMonHoc();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            bar1.Text = "Chức năng";
            newToolStripMenuItem.Enabled = false;
            enbalMenu(false);
            textMatKhau.Text = "123456";
            textTaiKhoan.Text = "admin";
            toolStrip1.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn muốn thoát?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK) Application.Exit(); ;
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer3.Controls.Clear();
            frmTimKiemHocSinh frm = new frmTimKiemHocSinh();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer3.Controls.Add(frm);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            groupPanel1.Show();
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupPanel1.Show();
            textMatKhau.Text = "";
            textTaiKhoan.Text = "";
            enbalMenu(false);
            bar2.Visible = false;
            toolStrip1.Enabled = false;
        }

        private void groupPanel1_Click(object sender, EventArgs e)
        {

        }
        public void enbalMenu(bool edit)
        {
            editToolStripMenuItem.Enabled = edit;
            toolsToolStripMenuItem.Enabled = edit;
            helpToolStripMenuItem.Enabled = edit;
            //trợGiúpToolStripMenuItem.Enabled = edit;
            butnhapdiem.Enabled = edit;
            buttongketHK.Enabled = edit;
            buttongketCN.Enabled = edit;
            butTKHocSinh.Enabled = edit;
            butTKGiaoVien.Enabled = edit;
            //butBaoCaoHS.Enabled = edit;
            //butBaoCaoLop.Enabled = edit;
            //butTiLeKhaGioi.Enabled = edit;

        }
        private void butDNhap_Click(object sender, EventArgs e)
        {
            ConnectData dn = new ConnectData();
            if (textTaiKhoan.Text == "")
            {
                MessageBox.Show("Chưa nhập tên đăng nhập", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textTaiKhoan.Focus();
            }
            else if (textMatKhau.Text == "")
            {
                MessageBox.Show("Chưa nhập mật khẩu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textMatKhau.Focus();
            }
            else if (dn.CheckLogin(textTaiKhoan.Text, textMatKhau.Text))
            {
                MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //frmMain Main = (frmMain)this.MdiParent;
                enbalMenu(true);

                //Main.dnhDX(false);
                frmDiemCaNam frm = new frmDiemCaNam();
                //frm.Show():
                //this.Close();
                groupPanel1.Hide();
                toolStripMenuItem1.Enabled = false;
                newToolStripMenuItem.Enabled = true;
                toolStrip1.Enabled = true;
            }
            else
                MessageBox.Show("Tên đăng nhập, Mật khẩu chưa đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmDiemCaNam frm = new frmDiemCaNam();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void expandablePanel3_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void butnhapdiem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmDiemMonHoc frm = new frmDiemMonHoc();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void buttongketHK_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmDiemTBMonHocKy frm = new frmDiemTBMonHocKy();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void expandablePanel1_Click(object sender, EventArgs e)
        {
             
        }

        private void butTKHocSinh_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer3.Controls.Clear();
            frmTimKiemHocSinh frm = new frmTimKiemHocSinh();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer3.Controls.Add(frm);
        }

        private void butThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn muốn thoát?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK) groupPanel1.Hide();
            toolStripMenuItem1.Enabled = true;
            newToolStripMenuItem.Enabled = false;

        }

        private void butBaoCaoHS_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            //panelDockContainer4.Controls.Clear();
            //frmBCHocSinh frm = new frmBCHocSinh();
            //frm.TopLevel = false;
            //frm.Dock = DockStyle.Fill;
            //frm.Visible = true;
            //frm.FormBorderStyle = FormBorderStyle.None;
            //panelDockContainer4.Controls.Add(frm);
        }

        private void butBaoCaoLop_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            //panelDockContainer4.Controls.Clear();
            //frmBCLop frm = new frmBCLop();
            //frm.TopLevel = false;
            //frm.Dock = DockStyle.Fill;
            //frm.Visible = true;
            //frm.FormBorderStyle = FormBorderStyle.None;
            //panelDockContainer4.Controls.Add(frm);
        }

        private void butTiLeKhaGioi_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            //panelDockContainer4.Controls.Clear();
            //frmThongKe frm = new frmThongKe();
            //frm.TopLevel = false;
            //frm.Dock = DockStyle.Fill;
            //frm.Visible = true;
            //frm.FormBorderStyle = FormBorderStyle.None;
            //panelDockContainer4.Controls.Add(frm);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmDiemTBMonHocKy frm = new frmDiemTBMonHocKy();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }

        private void điểmCảNămToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bar2.Show();
            groupPanel1.Hide();
            panelDockContainer2.Controls.Clear();
            frmDiemCaNam frm = new frmDiemCaNam();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelDockContainer2.Controls.Add(frm);
        }
    }
}
