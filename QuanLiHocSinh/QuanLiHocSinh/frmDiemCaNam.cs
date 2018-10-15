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
    public partial class frmDiemCaNam : Form
    {
        private hanhKiemBLL hanhkbll = new hanhKiemBLL();
        private lopHocBLL lbll = new lopHocBLL();
        private hocSinhBLL hsbll = new hocSinhBLL();
        private namHocBLL nhbll = new namHocBLL();
        private diemTBChungCaNamBLL dcnbll = new diemTBChungCaNamBLL();
        public frmDiemCaNam()
        {
            InitializeComponent();
        }

        private void frmDiemCaNam_Load(object sender, EventArgs e)
        {
            comboML.DataSource = lbll.layDSLop2();
            comboML.DisplayMember = "tenLop";
            comboML.ValueMember = "maLop";
            dataGridHS.DataSource = hsbll.layDSHocSinh1(comboML.SelectedValue.ToString().Trim());
            dataGridDTBCN.DataSource = dcnbll.layDSDiemCaNam(comboML.SelectedValue.ToString().Trim());
            textHanhK.Text = "" + Get_Year() + "-" + Get_Year1() + "";
            EnableEditing(false);
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
            str = str.Substring(6, 4);
            return str;
        }


        private void dataGridHS_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                textTenHS.Text = dataGridHS.Rows[dong].Cells["hoTenhs"].Value.ToString();
                labelMaHS.Text = dataGridHS.Rows[dong].Cells["maHS"].Value.ToString();
            }
            catch
            {
                return;
            }
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
            dataGridDTBCN.Enabled = !editing;
            dataGridHS.Enabled = editing;
            //--------------
            //textHanhK.Enabled = editing;
            comboML.Enabled = editing;
        }

        private void dataGridDTBCN_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                textSTT.Text = dataGridDTBCN.Rows[dong].Cells["STT"].Value.ToString();
                comboML.Text = dataGridDTBCN.Rows[dong].Cells["tenLop"].Value.ToString();
                textTenHS.Text = dataGridDTBCN.Rows[dong].Cells["hoTen"].Value.ToString();
                textHanhK.Text = dataGridDTBCN.Rows[dong].Cells["tenNamHoc"].Value.ToString();
                labelMaHS.Text = dataGridDTBCN.Rows[dong].Cells["maHocSinh"].Value.ToString();
            }
            catch
            {
                return;
            }
        }

        private void butthem_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            textSTT.Text = dcnbll.NextID();
        }

        private void butkhongluu_Click(object sender, EventArgs e)
        {
            EnableEditing(false);
        }

        private void butsua_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            dataGridHS.Enabled = false;
            dataGridDTBCN.Enabled = true;
        }
        private diemTBChungCaNam LayDSDiemCN()
        {
            diemTBChungCaNam dhk = new diemTBChungCaNam();
            dhk.STT = textSTT.Text;
            dhk.maLop = comboML.SelectedValue.ToString().Trim();
            dhk.maHS = labelMaHS.Text;
            dhk.namHoc = textHanhK.Text;
            return dhk;
        }
        private bool ktra(string hs, string nh)
        {
            bool b = true;
            for (int i = 0; i < dataGridDTBCN.Rows.Count - 1; i++)
            {
                if (dataGridDTBCN.Rows[i].Cells[3].Value.Equals("" + hs + "") && dataGridDTBCN.Rows[i].Cells[9].Value.Equals("" + nh + ""))
                    b = false;
            }
            return b;
        }
        private void butluu_Click(object sender, EventArgs e)
        {
            diemTBChungCaNam dhk = LayDSDiemCN();
            if (dcnbll.CheckExits(dhk.STT))
            {
                if (dcnbll.SuaDiemCaNam(dhk))
                    frmDiemCaNam_Load(sender, e);
            }
            else
            {
                if (ktra(textTenHS.Text, textHanhK.Text)==true)
                {
                    if (dcnbll.themDiemCaNam(dhk))
                        frmDiemCaNam_Load(sender, e);
                }
                else
                    MessageBox.Show("" + textTenHS.Text + " đã tồn tại", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void butxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa " + textTenHS.Text + " Không",
                "Hỏi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dcnbll.XoaDiemCaNam(textSTT.Text))
                    frmDiemCaNam_Load(sender, e);
            }
        }

        private void comboML_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridHS.DataSource = hsbll.layDSHocSinh1(comboML.SelectedValue.ToString().Trim());
            dataGridDTBCN.DataSource = dcnbll.layDSDiemCaNam(comboML.SelectedValue.ToString().Trim());
        }

        private void textTenHS_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelX6_Click(object sender, EventArgs e)
        {

        }
    }
}
