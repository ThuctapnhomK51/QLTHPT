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
    public partial class frmTimKiemHocSinh : Form
    {
        lopHocBLL lh = new lopHocBLL();
        hocSinhBLL hsbll = new hocSinhBLL();
        public frmTimKiemHocSinh()
        {
            InitializeComponent();
        }

        private void frmTimKiemHocSinh_Load(object sender, EventArgs e)
        {
            comboML.DataSource = lh.layDSLop2();
            comboML.DisplayMember = "tenLop";
            comboML.ValueMember = "maLop";
            comboEDIT.DataSource = lh.layDSLop2();
            comboEDIT.DisplayMember = "tenLop";
            comboEDIT.ValueMember = "maLop";
            dataGridTTHS.DataSource = hsbll.layDSHocSinh();
            radioLop.Checked = true;
            textTenHS.Enabled = false;
            if (radioLop.Checked == true)
            {

                dataGridTTHS.DataSource = hsbll.TimKiemTheolop(comboML.SelectedValue.ToString().Trim());
            }
        }

        private void butTim_Click(object sender, EventArgs e)
        {
            if (radioLop.Checked == true)
            {

                dataGridTTHS.DataSource = hsbll.TimKiemTheolop(comboML.SelectedValue.ToString().Trim());
            }
            else 
                if (radioHS.Checked == true)
                {

                    dataGridTTHS.DataSource = hsbll.TimKiemHS(textTenHS.Text);
                }
                else { MessageBox.Show("Hãy chọn kiểu tìm kiếm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }

        private void groupPanel1_Click(object sender, EventArgs e)
        {

        }

        private void radioLop_CheckedChanged(object sender, EventArgs e)
        {
            comboML.Enabled = true;
            textTenHS.Enabled = false;
        }

        private void radioHS_CheckedChanged(object sender, EventArgs e)
        {
            comboML.Enabled = false;
            textTenHS.Enabled = true;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn muốn thoát?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK) this.Close();

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            frmTimKiemHocSinh_Load(sender,e);
        }

        private void textTenHS_TextChanged(object sender, EventArgs e)
        {
            if (radioLop.Checked == true)
            {

                dataGridTTHS.DataSource = hsbll.TimKiemTheolop(comboML.SelectedValue.ToString().Trim());
            }
            else
                if (radioHS.Checked == true)
                {

                    dataGridTTHS.DataSource = hsbll.TimKiemHS(textTenHS.Text);
                }
                //else { MessageBox.Show("Hãy chọn kiểu tìm kiếm", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }


        }

        private void comboML_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioLop.Checked == true)
            {

                dataGridTTHS.DataSource = hsbll.TimKiemTheolop(comboML.SelectedValue.ToString().Trim());
            }
            else
                if (radioHS.Checked == true)
                {

                    dataGridTTHS.DataSource = hsbll.TimKiemHS(textTenHS.Text);
                }
                

        }

        private void dataGridTTHS_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                textMaHS.Text = dataGridTTHS.Rows[dong].Cells["maHS"].Value.ToString();
                txtTenHS.Text = dataGridTTHS.Rows[dong].Cells["hoTen"].Value.ToString();
                comboEDIT.Text = dataGridTTHS.Rows[dong].Cells["tenLop"].Value.ToString();
            }
            catch
            {
                return;
            }
        }

        private void comboEDIT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private hocSinh LayTTHS()
        {
            hocSinh hs = new hocSinh();
            hs.maHS = textMaHS.Text;
            hs.maLop = comboEDIT.SelectedValue.ToString().Trim();
            hs.hoTen = txtTenHS.Text;
            return hs;
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            hocSinh hs = LayTTHS();
            if (hsbll.CheckExits(hs.maHS))
            {
                if (hsbll.SuaHS1(hs))
                    frmTimKiemHocSinh_Load(sender, e);
            }
            
        }
    }
}
