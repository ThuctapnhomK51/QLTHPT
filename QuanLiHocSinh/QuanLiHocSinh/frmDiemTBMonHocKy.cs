using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLiHocSinh.BussinessObject;
using QuanLiHocSinh.BussinessLogicLayer;

namespace QuanLiHocSinh
{
    public partial class frmDiemTBMonHocKy : Form
    {
        private hanhKiemBLL hanhkbll = new hanhKiemBLL();
        private lopHocBLL lbll = new lopHocBLL();
        private hocSinhBLL hsbll = new hocSinhBLL();
        private hocKyBLL hkbll = new hocKyBLL();
        private namHocBLL nhbll = new namHocBLL();
        private diemTBMonHocKyBLL dtbbll = new diemTBMonHocKyBLL();
        public frmDiemTBMonHocKy()
        {
            InitializeComponent();
        }

        private void frmDiemTBMonHocKy_Load(object sender, EventArgs e)
        {
            comboHanhK.DataSource = hanhkbll.layDSHanhKiem();
            comboHanhK.DisplayMember = "tenHanhKiem";
            comboHanhK.ValueMember = "maHanhKiem";
            comboML.DataSource = lbll.layDSLop2();
            comboML.DisplayMember = "tenLop";
            comboML.ValueMember = "maLop";
            comboHKy.DataSource = hkbll.layDSHocKy();
            comboHKy.DisplayMember = "tenHocKy";
            comboHKy.ValueMember = "maHocKy";
            dataGridHS.DataSource = hsbll.layDSHocSinh1(comboML.SelectedValue.ToString().Trim());
            dataGridDTBHK.DataSource = dtbbll.layDSDiemMH(comboML.SelectedValue.ToString().Trim(),labelMaHS.Text);
            EnableEditing(false);
            textHanhK.Text = "" + Get_Year().Trim() + "-" + Get_Year1().Trim() + "";
        }
        public string Get_Year()
        {
            string str = DateTime.Now.AddYears(-1).ToString().Trim();
            str = str.Substring(6,4);
            return str;
        }
        public string Get_Year1()
        {
            string str = DateTime.Now.ToString().Trim();
            str = str.Substring(6,4);
            return str;
        }
        private void EnableEditing(bool editing)
        {
            //button
            butthem.Enabled = !editing;
            butsua.Enabled = !editing;
            butxoa.Enabled = !editing;
            butluu.Enabled = editing;
            butkhongluu.Enabled = editing;

            //datagridview
            dataGridDTBHK.Enabled = !editing;
            dataGridHS.Enabled = !editing;
            //--------------
            //textHanhK.Enabled = editing;
            //comboHanhK.Enabled = editing;
            //comboHKy.Enabled = editing;
            //comboML.Enabled = editing;
        }
        private void Resettext()
        {
            //textHanhK.Text = "";
            //textHanhK.Text = "";
        }

        private diemTBMonChungHocKy LayDSDiemHK()
        {
            diemTBMonChungHocKy dhk = new diemTBMonChungHocKy();
            dhk.STT = textSTT.Text.Trim();
            dhk.maLop = comboML.SelectedValue.ToString().Trim();
            dhk.maHS = labelMaHS.Text.Trim();
            dhk.hanhKiem = comboHanhK.SelectedValue.ToString().Trim();
            dhk.maHocKy = comboHKy.SelectedValue.ToString().Trim();
            dhk.namHoc = textHanhK.Text.Trim();
            return dhk;
        }
        //Public Function Ktra(ByVal mahd As String, ByVal mh As String)
        //Dim Mahang As String
        //Dim b As Boolean = True
        //For i = 0 To lvNoidung.Items.Count - 1
        //If lvNoidung.Items(i).SubItems(0).Text = mahd Then
        //Mahang = lvNoidung.Items(i).SubItems(1).Text
        //If Mahang = mh Then
        //b = False
        //End If
        //End If
        //Next
        //Return b
        //End Function
        private bool ktra(string hs, string hky)
        {
            bool b = true;
            //string itemcode = "";
            for (int i = 0; i < dataGridDTBHK.Rows.Count -1; i++)
            {
                if (dataGridDTBHK.Rows[i].Cells[3].Value.Equals("" + hs + "") && dataGridDTBHK.Rows[i].Cells[7].Value.Equals("" + hky + ""))
                    b = false;
            }
            return b;
        }
        
        private void butluu_Click(object sender, EventArgs e)
        {
            diemTBMonChungHocKy dhk = LayDSDiemHK();
            if (dtbbll.CheckExits(dhk.STT))
            {
                if (dtbbll.SuaDiemTBHK(dhk))
                    frmDiemTBMonHocKy_Load(sender, e);
            }
            else
            {
               if(ktra(textTenHS.Text,comboHKy.Text)==true)
               {
                   if (dtbbll.themDiemTBHocKy(dhk))
                       frmDiemTBMonHocKy_Load(sender, e);
               }
               else
                    MessageBox.Show(""+textTenHS.Text+" đã tồn tại", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridHS_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int dong = e.RowIndex;
                textTenHS.Text = dataGridHS.Rows[dong].Cells["hoTenhs"].Value.ToString();
                labelMaHS.Text = dataGridHS.Rows[dong].Cells["maHS"].Value.ToString();
                dataGridDTBHK.DataSource = dtbbll.layDSDiemMH(comboML.SelectedValue.ToString().Trim(), labelMaHS.Text);
            }
            catch
            {
                return;
            }
        }

        private void butthem_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            textSTT.Text = dtbbll.NextID();
            Resettext();
        }

        private void butsua_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            dataGridHS.Enabled = true;
            dataGridDTBHK.Enabled = true;
        }

        private void butkhongluu_Click(object sender, EventArgs e)
        {
            EnableEditing(false);
        }

        private void dataGridDTBHK_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                textSTT.Text = dataGridDTBHK.Rows[dong].Cells["STT"].Value.ToString();
                comboML.Text = dataGridDTBHK.Rows[dong].Cells["tenLop"].Value.ToString();
                textTenHS.Text = dataGridDTBHK.Rows[dong].Cells["hoTen"].Value.ToString();
                comboHanhK.Text = dataGridDTBHK.Rows[dong].Cells["hanhKiem"].Value.ToString();
                comboHKy.Text = dataGridDTBHK.Rows[dong].Cells["tenHocKy"].Value.ToString(); 
                textHanhK.Text = dataGridDTBHK.Rows[dong].Cells["tenNamHoc"].Value.ToString();
                labelMaHS.Text = dataGridDTBHK.Rows[dong].Cells["maHocSinh"].Value.ToString();
            }
            catch
            {
                return;
            }
        }

        private void butxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa " + textTenHS.Text + " Không",
                "Hỏi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dtbbll.XoaDiemDTHK(textSTT.Text))
                    frmDiemTBMonHocKy_Load(sender, e);
            }
        }

        private void comboHKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dataGridHS.DataSource = hsbll.layDSHocSinh1(comboML.SelectedValue.ToString().Trim());
            //dataGridDTBHK.DataSource = dtbbll.layDSDiemMH(comboML.SelectedValue.ToString().Trim(), labelMaHS.Text);
        }

        private void comboML_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridHS.DataSource = hsbll.layDSHocSinh1(comboML.SelectedValue.ToString().Trim());
        }
    }
}
