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
    public partial class frmMonHoc : Form
    {
        private monHocBLL mhbll = new monHocBLL();
        public frmMonHoc()
        {
            InitializeComponent();
        }

        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            dataGridMH.DataSource = mhbll.layDSMonHoc();
            //dataGridMH.DataSource = mhbll.layDSMonHoc();
            EnableEditing(false);
        }

        private void textMM_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataGridMH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
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
            textTMH.Enabled = editing;
            textSoTiet.Enabled =editing;
            textHeSo.Enabled = editing;
            //datagridview
            dataGridMH.Enabled = !editing;
        }
        private void Resettext()
        {
            textTMH.Text = "";
            textSoTiet.Text = "";
            textHeSo.Text = "";

        }

       

        private void butsua_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            dataGridMH.Enabled = true;
        }

        private void butkhongluu_Click(object sender, EventArgs e)
        {
            EnableEditing(false);
            
        }

        private void textTMH_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateTMH())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateTMH()
        {
            bool bvalidated = false;
            if (textTMH.Text.Trim().Equals(string.Empty))
            {
                this.textTMH.Focus();
                this.errorProvider1.SetError(this.textTMH, "Chưa nhập tên môn học");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textTMH, "");
                bvalidated = true;
            }
            return bvalidated;
        }
        private bool ValidateData()
        {
            bool tm = ValidateTMH();
            bool st = ValidateSoTiet();
            bool hs = ValidateHeSo();
            return (tm && st && hs);
        }
        private monHoc LayTTHS()
        {
            monHoc mh = new monHoc();
            mh.maMonHoc = textMMH.Text;
            mh.tenMonHoc = textTMH.Text;
            mh.soTiet = int.Parse("0" + textSoTiet.Text.Trim());
            mh.heSo = int.Parse("0" + textHeSo.Text.Trim());
            return mh;
        }
        private void butluu_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }
            monHoc mh = LayTTHS();
            if (mhbll.CheckExits(mh.maMonHoc))
            {
                if(mhbll.SuaMH(mh))
                    frmMonHoc_Load(sender, e);
            }
            else
                if (mhbll.themMonHoc(mh))
                    frmMonHoc_Load(sender, e);
        }

        private void butxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa " + textTMH.Text + " Không",
                "Hỏi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (mhbll.XoaMH(textMMH.Text))
                    frmMonHoc_Load(sender, e);
            }
        }

        private void textSoTiet_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateSoTiet())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateSoTiet()
        {
            bool bvalidated = false;
            if (textSoTiet.Text.Trim().Equals(string.Empty))
            {
                this.textTMH.Focus();
                this.errorProvider1.SetError(this.textSoTiet, "Chưa nhập tên môn học");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textSoTiet, "");
                bvalidated = true;
            }
            return bvalidated;
        }

        private void textHeSo_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateHeSo())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateHeSo()
        {
            bool bvalidated = false;
            if (textHeSo.Text.Trim().Equals(string.Empty))
            {
                this.textHeSo.Focus();
                this.errorProvider1.SetError(this.textHeSo, "Chưa nhập tên môn học");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textHeSo, "");
                bvalidated = true;
            }
            return bvalidated;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //frmReport frm = new frmReport();
            //frm.Show();
        }

        private void textSoTiet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textHeSo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridMH_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                textMMH.Text = dataGridMH.Rows[dong].Cells["maMonHoc"].Value.ToString();
                textTMH.Text = dataGridMH.Rows[dong].Cells["tenMonHoc"].Value.ToString();
                textSoTiet.Text = dataGridMH.Rows[dong].Cells["soTiet"].Value.ToString();
                textHeSo.Text = dataGridMH.Rows[dong].Cells["heSo"].Value.ToString();
            }
            catch
            {
                return;
            }
        }

        private void butthem_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            textMMH.Text = mhbll.NextID();
            Resettext();
        }




    }
}
