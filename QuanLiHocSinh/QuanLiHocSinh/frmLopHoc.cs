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
    public partial class frmLopHoc : Form
    {
        private lopHocBLL lbll = new lopHocBLL();
        private giaoVienBLL gvbll = new giaoVienBLL();
        private khoiHocBLL kbll = new khoiHocBLL();
        public frmLopHoc()
        {
            InitializeComponent();
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            dataGridTTL.DataSource = lbll.layDSLop();

            comboKhoi.DataSource = kbll.layDSKhoi();
            comboKhoi.DisplayMember = "tenKhoi";
            comboKhoi.ValueMember = "maKhoi";
            EnableEditing(false);
        }
        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Resettext()
        {
            textTL.Text = "";
        }
        private void EnableEditing(bool editing)
        {
            //button
            butthem.Enabled = !editing;
            butsua.Enabled = !editing;
            butxoa.Enabled = !editing;
            butluu.Enabled = editing;
            butkhongluu.Enabled = editing;
            //textbox,combobox
            textTL.Enabled = editing;
            comboKhoi.Enabled = editing;
            //datagridview
            dataGridTTL.Enabled = !editing;
        }
        private void butthem_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            textML.Text = lbll.NextID();
            Resettext();
        }

       

        private void dataGridTTL_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridTTL_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                textML.Text = dataGridTTL.Rows[dong].Cells["maLop"].Value.ToString();
                textTL.Text = dataGridTTL.Rows[dong].Cells["tenLop"].Value.ToString();
                labelAnMK.Text = dataGridTTL.Rows[dong].Cells["maKhoi"].Value.ToString();
                comboKhoi.Text = dataGridTTL.Rows[dong].Cells["maKhoi"].Value.ToString();
                labelSiSo.Text = dataGridTTL.Rows[dong].Cells["siSo"].Value.ToString();
            }
            catch
            {
                return;
            }

        }
        private lopHoc LayTTLop()
        {
            lopHoc l = new lopHoc();
            l.maLop = textML.Text;
            l.tenLop = textTL.Text;
            l.maKhoi =labelAnMK.Text; 
            return l;
        }
        private bool ValidateData()
        {
            bool tl = ValidateTL();
            return (tl);
        }
        private void butluu_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }
            lopHoc l = LayTTLop();
            if (lbll.CheckExits(l.maLop))
            {
                if (lbll.SuaLop(l))
                    frmLop_Load(sender, e);
            }
            else
            {
                if (lbll.themLop(l))
                    frmLop_Load(sender, e);
            }
        }

        private void butsua_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            dataGridTTL.Enabled = true;
            //Resettext();

        }

        private void butkhongluu_Click(object sender, EventArgs e)
        {
            EnableEditing(false);
        }


        private void comboKhoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelAnMK.Text = comboKhoi.SelectedValue.ToString().Trim();
        }

        private void groupPanel1_Click(object sender, EventArgs e)
        {

        }
        private void butxoa_Click(object sender, EventArgs e)
        {
            lopHoc lop = new lopHoc();
            if (MessageBox.Show("Bạn có chắc muốn xóa " + textTL.Text + " Không",
                "Hỏi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (lbll.XoaLop(textML.Text,labelSiSo.Text))
                    frmLop_Load(sender, e);
            }
        }

        private void textTL_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateTL())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateTL()
        {
            bool bvalidated = false;
            if (textTL.Text.Trim().Equals(string.Empty))
            {
                this.textTL.Focus();

                this.errorProvider1.SetError(this.textTL, "Chưa nhập tên lớp!");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textTL, "");
                bvalidated = true;
            }
            return bvalidated;
        }
       
       
    }
}
