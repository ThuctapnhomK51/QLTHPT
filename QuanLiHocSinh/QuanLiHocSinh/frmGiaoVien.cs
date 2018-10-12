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
using System.Text.RegularExpressions;

namespace QuanLiHocSinh
{
    public partial class frmGiaoVien : Form
    {
        private giaoVienBLL gvbll = new giaoVienBLL();
        public frmGiaoVien()
        {
            InitializeComponent();
        }

        private void frmGiaoVien_Load(object sender, EventArgs e)
        {
            dataGridTTGV.DataSource = gvbll.layDSGiaoVien();
            EnableEditing(false);
        }

        private void dataGridTTGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           
        }
        private void dataGridTTGV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                textMGV.Text = dataGridTTGV.Rows[dong].Cells["maGV"].Value.ToString();
                textHT.Text = dataGridTTGV.Rows[dong].Cells["hoTen"].Value.ToString();
                textChuyenMon.Text = dataGridTTGV.Rows[dong].Cells["chuyenMon"].Value.ToString();
                textSDT.Text = dataGridTTGV.Rows[dong].Cells["sDT"].Value.ToString();
                textEmail.Text = dataGridTTGV.Rows[dong].Cells["Email"].Value.ToString();
                textDC.Text = dataGridTTGV.Rows[dong].Cells["diaChi"].Value.ToString();
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
            //textbox,combobox
            textHT.Enabled = editing;
            textChuyenMon.Enabled = editing;
            textDC.Enabled = editing;
            textSDT.Enabled = editing;
            textEmail.Enabled = editing;
            //datagridview
            dataGridTTGV.Enabled = !editing;
        }
        private void Resettext()
        {
            textMGV.Text = "";
            textHT.Text = "";
            textChuyenMon.Text = "";
            textDC.Text = "";
            textSDT.Text = "";
            textEmail.Text = "";

        }
        

        private void butthem_Click_1(object sender, EventArgs e)
        {
            EnableEditing(true);

            Resettext();
            textMGV.Text = gvbll.NextID();
        }

        private void butsua_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            dataGridTTGV.Enabled = true;
        }

        private void butxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa " + textHT.Text + " Không",
                "Hỏi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (gvbll.XoaGV(textMGV.Text))
                    frmGiaoVien_Load(sender, e);     
            }
        }
        private giaoVien LayTTGV()
        {
            giaoVien gv = new giaoVien();
            gv.maGV = textMGV.Text;
            gv.hoTen = textHT.Text;
            gv.chuyenMon = textChuyenMon.Text;
            gv.diaChi = textDC.Text;
            gv.SDT = textSDT.Text;
            gv.EMail = textEmail.Text;
            return gv;
        }
        private bool ValidateData()
        {
            bool ht = ValidateHT();
            bool cm = ValidateChuyenMon();
            bool sdt = ValidateSDT();
            bool em = ValidateEmail();
            bool dc = ValidateDC();
            return (ht && dc && sdt && em && cm);
        }
        private void butluu_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }

                //this.Close();
                giaoVien gv = LayTTGV();
                if (gvbll.CheckExits(gv.maGV))
                {
                    if (gvbll.SuaGV(gv))
                        frmGiaoVien_Load(sender, e);
                }
                else
                {
                    if (gvbll.CheckExits1(gv.EMail))
                    {
                        MessageBox.Show("Email "+textEmail.Text+" đã tồn tại", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (gvbll.CheckExits2(gv.SDT))
                        {
                            MessageBox.Show("SDT " + textSDT.Text + " đã tồn tại", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (gvbll.themGV(gv))
                                frmGiaoVien_Load(sender, e);
                        }
                    }
                }

        }

        private void butkhongluu_Click(object sender, EventArgs e)
        {
            EnableEditing(false);
        }

        private void textHT_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateHT())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateHT()
        {
            bool bvalidated = false;
            if (textHT.Text.Trim().Equals(string.Empty))
            {
                this.textHT.Focus();
                this.errorProvider1.SetError(this.textHT, "Chưa nhập họ tên");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textHT, "");
                bvalidated = true;
            }
            return bvalidated;
        }

       

        private void textDC_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateDC())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateDC()
        {
            bool bvalidated = false;
            if (textDC.Text.Trim().Equals(string.Empty))
            {
                this.textDC.Focus();
                this.errorProvider1.SetError(this.textDC, "Chưa nhập địa chỉ");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textDC, "");
                bvalidated = true;
            }
            return bvalidated;
        }
       
        private void textSDT_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateSDT())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateSDT()
        {
            bool bTest = txtMinLengthTestIsValid(textSDT.Text.ToString());
            if (bTest == true)
            {
                this.errorProvider1.SetError(textSDT, "");

            }
            else
            {
                this.errorProvider1.SetError(textSDT,
                "Số điện thoại phải có dạng 0XXX-XXXXXX");
            }
            return bTest;
        }
        private bool txtMinLengthTestIsValid(string textToValidate)
        {
            Regex TheRegExpression;
            string TheTextToValidate;
            string TheRegExTest = @"0";
            TheTextToValidate = textToValidate;
            TheRegExpression = new Regex(TheRegExTest);
            // test text with expression
            
            char[] testArr = textSDT.Text.ToCharArray();
            bool testBool = false;
            if (testArr.Length > 9 && testArr.Length < 12)
            {

                if (TheRegExpression.IsMatch(TheTextToValidate))
                {
                    return true;
                    testBool = true;
                }
                else
                    return false;
            }
            
            return testBool;
        }
       

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult thoat;
            thoat = MessageBox.Show("ban co muon thoat khong", "tra loi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (thoat == DialogResult.OK)
                this.Close();
        }

        private void groupPanel1_Click(object sender, EventArgs e)
        {

        }

        private void textDC_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void textChuyenMon_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateChuyenMon())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateChuyenMon()
        {
            bool bvalidated = false;
            if (textChuyenMon.Text.Trim().Equals(string.Empty))
            {
                this.textChuyenMon.Focus();
                this.errorProvider1.SetError(this.textChuyenMon, "Chưa nhập chuyên môn");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textChuyenMon, "");
                bvalidated = true;
            }
            return bvalidated;
        }
        private void textEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateEmail())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateEmail()
        {
            bool bTest = txtRegExStringIsValid(textEmail.Text.ToString());
            if (bTest == false)
            {
                this.errorProvider1.SetError(textEmail, "Email phải có cấu trúc [a-z]@[a-z].com!");
            }
            else
            {
                this.errorProvider1.SetError(textEmail, "");
            }
            return bTest;
           
        }
        private bool txtRegExStringIsValid(string textToValidate)
        {
            Regex TheRegExpression;
            string TheTextToValidate;
            //string TheRegExTest = @"[2-9]\d{2}-\d{3}-\d{4}";
            string TheRegExTest = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            TheTextToValidate = textToValidate;
            TheRegExpression = new Regex(TheRegExTest);
            // test text with expression
            if (TheRegExpression.IsMatch(TheTextToValidate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void textSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            
        }

    }
}
