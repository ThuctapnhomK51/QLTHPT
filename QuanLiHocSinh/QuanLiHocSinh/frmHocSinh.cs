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
    public partial class frmHocSinh : Form
    {
        private hocSinhBLL hsbll = new hocSinhBLL();
        private lopHocBLL lbll = new lopHocBLL();
        public frmHocSinh()
        {
            InitializeComponent();
        }
        int day = DateTime.Now.Day;
        int month = DateTime.Now.Month;
        int year = DateTime.Now.Year;
        private void frmHocSinh_Load(object sender, EventArgs e)
        {
            labelTTSS.Visible = false;
            
            //textSiSo.Text = "" + month + "/" + day + "/" + year + "";
            dataGridTTHS.DataSource = hsbll.layDSHocSinh();
            comboMaLop.DataSource = lbll.layDSLop2();
            comboMaLop.DisplayMember = "tenLop";
            comboMaLop.ValueMember = "maLop";
            dataGridSiSo.Enabled = false;
            dataGridSiSo.DataSource = lbll.layDSLop1(comboMaLop.SelectedValue.ToString().Trim());
            EnableEditing(false);
            int b = int.Parse("0" + textSiSo.Text.Trim());
            int c = 10 - b;
            if (c == 0)
            {
                labelTTSS.Text = "" + comboMaLop.Text + " Đã hết chỗ!";
            }
            else
            {
                labelTTSS.Text = "" + comboMaLop.Text + " Còn " + c + " Chỗ!";
            }
        }
        public string Get_Day()
        {
            string str = DateTime.Now.ToString().Trim();
            str = str.Substring(0, 2);
            return str;
        }
        public string Get_Month()
        {
            string str = DateTime.Now.ToString().Trim();
            str = str.Substring(3, 2);
            return str;
        }
        public string Get_Year()
        {
            string str = DateTime.Now.ToString().Trim();
            str = str.Substring(6, 4);
            return str;
        }
        private void colorPickerButton1_SelectedColorChanged(object sender, EventArgs e)
        {

        }

        private void reflectionLabel1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridTTHS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void dataGridTTHS_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                textMHS.Text = dataGridTTHS.Rows[dong].Cells["maHS"].Value.ToString();
                comboMaLop.Text = dataGridTTHS.Rows[dong].Cells["tenLop"].Value.ToString();
                textHT.Text = dataGridTTHS.Rows[dong].Cells["hoTen"].Value.ToString();
                dateTimeNS.Text = dataGridTTHS.Rows[dong].Cells["ngaySinh"].Value.ToString();
                textDanToc.Text = dataGridTTHS.Rows[dong].Cells["danToc"].Value.ToString();

                //string tt = dataGridTTHS.Rows[dong].Cells["ngaySinh"].Value.ToString();
                //DateTime dt = Convert.ToDateTime(tt);
                //dateTimeNS.Text = String.Format("{0:MM/dd/yyyy}", dt);

                textDC.Text = dataGridTTHS.Rows[dong].Cells["diaChi"].Value.ToString();
                textSDT.Text = dataGridTTHS.Rows[dong].Cells["SDT"].Value.ToString();
                textHTME.Text = dataGridTTHS.Rows[dong].Cells["hoTenMe"].Value.ToString();
                textHTBo.Text = dataGridTTHS.Rows[dong].Cells["hoTenBo"].Value.ToString();
                textGC.Text = dataGridTTHS.Rows[dong].Cells["ghiChu"].Value.ToString();
                string gt = dataGridTTHS.Rows[dong].Cells["gioiTinh"].Value.ToString();
                if (gt.Trim() == "Nam") //trim() cắt khoản trắng 2 đầu
                {
                    //radioButton1.Checked = true;
                    radioNam.Checked = true;
                }
                else
                {
                    //radioButton2.Checked = true;
                    radioNu.Checked = true;
                }
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
            comboMaLop.Enabled = editing;
            textDanToc.Enabled = editing;
            textHTBo.Enabled = editing;
            textHTME.Enabled = editing;
            radioNam.Enabled = editing;
            radioNu.Enabled = editing;
            dateTimeNS.Enabled = editing;
            textDC.Enabled = editing;
            textSDT.Enabled = editing;
            textGC.Enabled = editing;
            //datagridview
            dataGridTTHS.Enabled = !editing;
        }
        private void Resettext()
        {
            textMHS.Text = "";
            textHT.Text = "";
            //dateTimeNS.Text = "";
            textDC.Text = "";
            textSDT.Text = "";
            textGC.Text = "";
            textHTBo.Text = "";
            textHTME.Text = "";
            textDanToc.Text = "";
            radioNam.Checked = true;

        }
        

        private void butthem_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            labelTTSS.Visible = true;
            Resettext();
            textMHS.Text = hsbll.NextID();
        }

        private void butsua_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            dataGridTTHS.Enabled = true;
           
        }

        private void butxoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa " + textHT.Text + " Không",
                "Hỏi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (hsbll.XoaHS(textMHS.Text))
                    frmHocSinh_Load(sender, e);
            }
        }
        //Lấy dữ liệu từ textbox
        private hocSinh LayTTHS()
        {
            hocSinh hs = new hocSinh();
            hs.maHS = textMHS.Text;
            hs.maLop = comboMaLop.SelectedValue.ToString().Trim();
            hs.hoTen = textHT.Text;

            if (radioNam.Checked == true)
            {
                hs.gioiTinh = "Nam";
            }
            else
            {
                hs.gioiTinh = "Nữ";
            }

            //DateTime dt = Convert.ToDateTime(dateTimeNS.Text);
            //dateTimeNS.Text = String.Format("{0:dd/mm/yyyy}", dt);
            hs.ngaySinh = Convert.ToDateTime(dateTimeNS.Text);
            hs.danToc = textDanToc.Text;
            hs.diaChi = textDC.Text;
            hs.hoTenBo = textHTBo.Text;
            hs.hoTenMe = textHTME.Text;
            if (textSDT.Text == "")
            {
                hs.SDT = "".Trim();
            }
            else
            {
                hs.SDT = textSDT.Text.Trim();
            }
            hs.ghiChu = textGC.Text;
            return hs;
        }
       
        private bool ValidateData()
        {
            bool ht = ValidateHT();
            bool ns = ValidateNgay();
            bool dc = ValidateDC();
            bool dt = ValidateDanToc();
            bool htb = ValidateHTBo();
            bool htm = ValidateHTME();
            bool sdt = ValidateSDT();

            bool n = false;
            if (textSDT.Text=="")
            {
                n = (ht && ns && dc && dt && htb && htm );
            }
            else
            {
                n = (ht && ns && dc && dt && htb && htm && sdt);
            }
            return n;
        }
        private void butluu_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dateTimeNS.Text) >= DateTime.Now)
            {
                //MessageBox.Show("ngay sn", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                errorProvider1.SetError(dateTimeNS, "ngày sinh lớn hơn ngày hiện tại");
            }
            else
            {
                if (!ValidateData())
                {
                    return;
                }
                hocSinh hs = LayTTHS();
                if (int.Parse("0" + textSiSo.Text.Trim()) == 10)
                {
                    MessageBox.Show("" + comboMaLop.Text + " sỉ số đã đầy", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (hsbll.CheckExits(hs.maHS))
                    {
                        if (hsbll.SuaHS(hs))
                            frmHocSinh_Load(sender, e);
                    }
                    else
                    {
                        if (hsbll.themHS(hs))
                            frmHocSinh_Load(sender, e);
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
            if (textSDT.Text.Equals(string.Empty))
            {
                this.errorProvider1.SetError(textSDT, "");
            }
            else
            {
                if (bTest == true)
                {
                    this.errorProvider1.SetError(textSDT, "");

                }
                else
                {
                    this.errorProvider1.SetError(textSDT,
                    "Số điện thoại phải có dạng 0XXX-XXXXXX");
                }
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
        private void dateTimeNS_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateNgay())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateNgay()
        {
            bool bvalidated = false;
            if (dateTimeNS.Text.Trim().Equals(string.Empty))
            {
                this.dateTimeNS.Focus();
              
                this.errorProvider1.SetError(this.dateTimeNS, "Chưa chọn ngày sinh!");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.dateTimeNS, "");
                bvalidated = true;
            }
            return bvalidated;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult thoat;
            thoat = MessageBox.Show("ban co muon thoat khong", "tra loi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (thoat == DialogResult.OK)
                this.Close();
        }

        private void groupPanel3_Click(object sender, EventArgs e)
        {

        }

        private void textSDT_TextChanged(object sender, EventArgs e)
        {
            //Regex regex;
            ////regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            //regex = new Regex(@"\S\S\S\S\S\S\S\S\S\S");
            //Control ctrl = (Control)sender;
            //if (regex.IsMatch(ctrl.Text))
            //{
            //    errorProvider1.SetError(ctrl, "");
            //}
            //else
            //{
            //    errorProvider1.SetError(ctrl,
            //    "Sai cấu trúc Email!.");
            //}
        }

        

        private void textDanToc_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateDanToc())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateDanToc()
        {
            bool bvalidated = false;
            if (textDanToc.Text.Trim().Equals(string.Empty))
            {
                this.textDanToc.Focus();

                this.errorProvider1.SetError(this.textDanToc, "Chưa nhập dân tộc!");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textDanToc, "");
                bvalidated = true;
            }
            return bvalidated;
        }

        private void textHTBo_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateHTBo())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateHTBo()
        {
            bool bvalidated = false;
            if (textHTBo.Text.Trim().Equals(string.Empty))
            {
                this.textHTBo.Focus();

                this.errorProvider1.SetError(this.textHTBo, "Chưa nhập họ tên bố!");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textHTBo, "");
                bvalidated = true;
            }
            return bvalidated;
        }

        private void textHTME_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateHTME())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateHTME()
        {
            bool bvalidated = false;
            if (textHTME.Text.Trim().Equals(string.Empty))
            {
                this.textHTME.Focus();

                this.errorProvider1.SetError(this.textHTME, "Chưa nhập họ tên mẹ!");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textHTME, "");
                bvalidated = true;
            }
            return bvalidated;
        }

        private void textSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            //if (textSDT.Text.Length > 10)
            //{
            //    e.KeyChar = (char)0;
            //    errorProvider1.SetError(textSDT, "Số chữ số không được lớn hơn 11");
            //}
            //else
            //    errorProvider1.SetError(textSDT, "");
        }

        private void textSDT_MouseClick(object sender, MouseEventArgs e)
        {
            //if (textSDT.Text.Length > 10)
            //{
            //    //e.KeyChar = (char)0;
            //    errorProvider1.SetError(textSDT, "");
            //}

        }

        private void comboMaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridSiSo.DataSource = lbll.layDSLop1(comboMaLop.SelectedValue.ToString().Trim());
            //int a = 5;
            int b = int.Parse("0" + textSiSo.Text.Trim());
            int c = 10 - b;
            if (c == 0)
            {
                labelTTSS.Text = "" + comboMaLop.Text + " Đã hết chỗ!";
            }
            else
            {
                labelTTSS.Text = "" + comboMaLop.Text + " Còn " + c + " Chỗ!";
            }
        }

        private void dataGridSiSo_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            textSiSo.Text = dataGridSiSo.Rows[dong].Cells["SiSo"].Value.ToString();
        }

        private void textGC_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelX8_Click(object sender, EventArgs e)
        {

        }


    }
}
