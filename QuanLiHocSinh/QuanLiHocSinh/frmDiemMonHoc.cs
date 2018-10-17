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
using System.Threading;
using System.Text.RegularExpressions; 

namespace QuanLiHocSinh
{
    public partial class frmDiemMonHoc : Form
    {
        private lopHocBLL lbll = new lopHocBLL();
        private hocSinhBLL hsbll = new hocSinhBLL();
        private hocKyBLL hkbll = new hocKyBLL();
        private namHocBLL nhbll = new namHocBLL();
        private monHocBLL mhbll = new monHocBLL();
        private diemMonHocBLL dmhbll = new diemMonHocBLL();
        public frmDiemMonHoc()
        {
            InitializeComponent();
        }

        private void frmLopHocSinh_Load(object sender, EventArgs e)
        {
            comboML.DataSource = lbll.layDSLop2();
            comboML.DisplayMember = "tenLop";
            comboML.ValueMember = "maLop";
            comboHKy.DataSource = hkbll.layDSHocKy();
            comboHKy.DisplayMember = "tenHocKy";
            comboHKy.ValueMember = "maHocKy";
            comboMonHoc.DataSource = mhbll.layDSMonHoc();
            comboMonHoc.DisplayMember = "tenMonHoc";
            comboMonHoc.ValueMember = "maMonHoc";
            dataGridHS.DataSource = hsbll.layDSHocSinh1(comboML.SelectedValue.ToString().Trim());
            dataGridDMH.DataSource = dmhbll.layDSDiemMH(comboHKy.SelectedValue.ToString().Trim(), comboML.SelectedValue.ToString().Trim(),labelMaHS.Text);
            EnableEditing(false);
            textNamHoc.Text = "" + Get_Year() + "-" + Get_Year1() + "";
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

        private void Resettext()
        {
            //textTenHS.Text = "";
            textDiemMie.Text = "";
            textDiem15.Text = "";
            textDiem45.Text = "";
            textDiemThi.Text = "";
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
            dataGridDMH.Enabled = !editing;
            dataGridHS.Enabled = !editing;
            //--------------
            textDiemMie.Enabled = editing;
            textDiem15.Enabled = editing;
            textDiem45.Enabled = editing;
            textDiemThi.Enabled = editing;

            //comboHKy.Enabled = editing;
            comboMonHoc.Enabled = editing;
            //comboML.Enabled =editing;
        }
        private void EnableEditing1(bool editing)
        {
            lopHoc l = new lopHoc();
            //button
            butthem.Enabled = !editing;
            butsua.Enabled = !editing;
            butxoa.Enabled = !editing;
            butluu.Enabled = editing;
            butkhongluu.Enabled = editing;

            //datagridview
            //dataGridDMH.Enabled = editing;
            //dataGridHS.Enabled = !editing;
           
        }

        private void butthem_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            textSTT.Text = dmhbll.NextID();
            Resettext();
            dataGridDMH.Enabled = false;
            dataGridHS.Enabled = false;
        }

        private void dataGridDMH_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

                try
                {
                    int dong = e.RowIndex;
                    textSTT.Text = dataGridDMH.Rows[dong].Cells["STT"].Value.ToString();
                    comboML.Text = dataGridDMH.Rows[dong].Cells["tenLop"].Value.ToString();
                    textTenHS.Text = dataGridDMH.Rows[dong].Cells["hoTen"].Value.ToString();
                    comboHKy.Text = dataGridDMH.Rows[dong].Cells["tenHocKy"].Value.ToString();
                    textNamHoc.Text = dataGridDMH.Rows[dong].Cells["tenNamHoc"].Value.ToString();
                    comboMonHoc.Text = dataGridDMH.Rows[dong].Cells["tenMonHoc"].Value.ToString();
                    textDiemMie.Text = dataGridDMH.Rows[dong].Cells["diemMieng"].Value.ToString();
                    textDiem15.Text = dataGridDMH.Rows[dong].Cells["diem15P"].Value.ToString();
                    textDiem45.Text = dataGridDMH.Rows[dong].Cells["diem45P"].Value.ToString();
                    textDiemThi.Text = dataGridDMH.Rows[dong].Cells["diemThi"].Value.ToString();
                    labelMaHS.Text = dataGridDMH.Rows[dong].Cells["maHocSinh"].Value.ToString();
                }
                catch
                {
                    return;
                }

        }
        //----------------load len datagridview-to-add-------------------------------
        private void dataGridHS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void dataGridHS_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                textTenHS.Text = dataGridHS.Rows[dong].Cells["hoTenhs"].Value.ToString();
                labelMaHS.Text = dataGridHS.Rows[dong].Cells["maHS"].Value.ToString();

                dataGridDMH.DataSource = dmhbll.layDSDiemMH(comboHKy.SelectedValue.ToString().Trim(), comboML.SelectedValue.ToString().Trim(), labelMaHS.Text);
            }
            catch
            {
                return;
            }
        }
        //------------------------------------------------------------------------

        private diemMonHoc LayDSDiem()
        {
            diemMonHoc dmh = new diemMonHoc();
            dmh.STT = textSTT.Text;
            dmh.maLop = comboML.SelectedValue.ToString().Trim();
            dmh.maHS = labelMaHS.Text;
            dmh.maHocKy = comboHKy.SelectedValue.ToString().Trim();
            dmh.namHoc = textNamHoc.Text;
            dmh.maMonHoc = comboMonHoc.SelectedValue.ToString().Trim();
            dmh.diemMieng = double.Parse("0" + textDiemMie.Text.Trim());
            dmh.diem15P = double.Parse("0" + textDiem15.Text.Trim());
            dmh.diem45P = double.Parse("0" + textDiem45.Text.Trim());
            dmh.diemThi = double.Parse("0" + textDiemThi.Text.Trim());
            return dmh;
        }
        private bool ValidateData()
        {
            bool dm = ValidateDiemMie();
            bool d15 = ValidateDiem15();
            bool d45 = ValidateDiem45();
            bool dt = ValidateDiemThi();
            bool ht = ValidateTenHS();
            return (dm && d15 && d45 && dt && ht);
        }
        private bool ktra(string hs,string mh, string hky)
        {
            bool b = true;
            
            //string itemcode = "";
            for (int i = 0; i < dataGridDMH.Rows.Count - 1; i++)
            {
                if (dataGridDMH.Rows[i].Cells[3].Value.Equals("" + hs + "") && dataGridDMH.Rows[i].Cells[4].Value.Equals("" + mh + "") && dataGridDMH.Rows[i].Cells[10].Value.Equals("" + hky + ""))
                    b = false;
            }
            return b;
        }
        private void butluu_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }
            diemMonHoc dmh = LayDSDiem();
            if (dmhbll.CheckExits(dmh.STT))
            {
                if (dmhbll.SuaDiemMonHoc(dmh))
                    frmLopHocSinh_Load(sender, e);   
            }
            else
            {
                if (ktra(textTenHS.Text, comboMonHoc.Text, comboHKy.Text) == true)
                {
                    if (dmhbll.themDiemMonHoc(dmh))
                        frmLopHocSinh_Load(sender, e);
                }
                else
                    MessageBox.Show("" + comboMonHoc.Text + " đã tồn tại", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            //frmBCHocSinh frm = new frmBCHocSinh();
            //frm.Refresh();
        }

        private void butkhongluu_Click(object sender, EventArgs e)
        {
            EnableEditing(false);
        }

        private void butxoa_Click(object sender, EventArgs e)
        {
            //if (!ValidateData())
            //{
            //    return;
            //}
            if (MessageBox.Show("Bạn có chắc muốn xóa " + textTenHS.Text + " Không",
                "Hỏi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dmhbll.XoaDiemMonHoc(textSTT.Text))
                    frmLopHocSinh_Load(sender, e);
            }
        }

        private void butsua_Click(object sender, EventArgs e)
        {
            EnableEditing(true);
            dataGridDMH.Enabled = true;
            dataGridHS.Enabled = true;
        }
      
        private void textMHS_Validating(object sender, CancelEventArgs e)
        {
        }
        private void comboML_Validating(object sender, CancelEventArgs e)
        {
        }

        private void textDiemMie_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateDiemMie())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateDiemMie()
        {
            bool bvalidated = false;
            if (textDiemMie.Text.Trim().Equals(string.Empty) || Convert.ToDouble("0" + textDiemMie.Text) > 10.0)
            {
                this.textDiemMie.Focus();
                this.errorProvider1.SetError(this.textDiemMie, "Chưa nhập điểm miệng hoặc điểm không đúng!");
                bvalidated = false;
                //if ()
                //{
                //    this.textDiemMie.Focus();
                //    this.errorProvider1.SetError(this.textDiemMie, "điểm phải >=0 và <=10!");
                //    bvalidated = false;
                //}
            }
            else
            {
                this.errorProvider1.SetError(this.textDiemMie, "");
                bvalidated = true;
            }
            return bvalidated;
          
        }
        
        private void textDiem15_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateDiem15())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateDiem15()
        {
            bool bvalidated = false;
            if (textDiem15.Text.Trim().Equals(string.Empty) || Convert.ToDouble("0" + textDiem15.Text) > 10.0)
            {
                this.textDiem15.Focus();
                this.errorProvider1.SetError(this.textDiem15, "Chưa nhập điểm 15 hoặc điểm không đúng!'!");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textDiem15, "");
                bvalidated = true;
            }
            return bvalidated;
        }

        private void textDiem45_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateDiem45())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateDiem45()
        {
            bool bvalidated = false;
            if (textDiem45.Text.Trim().Equals(string.Empty) || Convert.ToDouble("0" + textDiem45.Text) > 10.0)
            {
                this.textDiem45.Focus();
                this.errorProvider1.SetError(this.textDiem45, "Chưa nhập điểm 45 hoặc điểm không đúng!'!");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textDiem45, "");
                bvalidated = true;
            }
            return bvalidated;
        }

        private void textDiemThi_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateDiemThi())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateDiemThi()
        {
            bool bvalidated = false;
            if (textDiemThi.Text.Trim().Equals(string.Empty) || Convert.ToDouble("0" + textDiemThi.Text) > 10.0)
            {
                this.textDiemThi.Focus();
                this.errorProvider1.SetError(this.textDiemThi, "Chưa nhập điểm thi hoặc điểm không đúng!'!");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textDiemThi, "");
                bvalidated = true;
            }
            return bvalidated;
        }

        private void textTenHS_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateTenHS())
            {
                e.Cancel = true;
            }
        }
        private bool ValidateTenHS()
        {
            bool bvalidated = false;
            if (textTenHS.Text.Trim().Equals(string.Empty))
            {
                this.textTenHS.Focus();
                this.errorProvider1.SetError(this.textTenHS, "Chưa nhập tên học sinh'!");
                bvalidated = false;
            }
            else
            {
                this.errorProvider1.SetError(this.textTenHS, "");
                bvalidated = true;
            }
            return bvalidated;
        }

        private void comboMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textDiemMie_KeyPress(object sender, KeyPressEventArgs e)
        {
            string decimalString = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            char decimalChar = Convert.ToChar(decimalString);

            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) { }
            else if (e.KeyChar == decimalChar && textDiemMie.Text.IndexOf(decimalString) == -1)
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void textDiem15_KeyPress(object sender, KeyPressEventArgs e)
        {
            string decimalString = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            char decimalChar = Convert.ToChar(decimalString);

            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) { }
            else if (e.KeyChar == decimalChar && textDiem15.Text.IndexOf(decimalString) == -1)
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void textDiem45_KeyPress(object sender, KeyPressEventArgs e)
        {
            string decimalString = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            char decimalChar = Convert.ToChar(decimalString);

            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) { }
            else if (e.KeyChar == decimalChar && textDiem45.Text.IndexOf(decimalString) == -1)
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void textDiemThi_KeyPress(object sender, KeyPressEventArgs e)
        {
            string decimalString = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            char decimalChar = Convert.ToChar(decimalString);

            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) { }
            else if (e.KeyChar == decimalChar && textDiemThi.Text.IndexOf(decimalString) == -1)
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void comboML_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridHS.DataSource = hsbll.layDSHocSinh1(comboML.SelectedValue.ToString().Trim());
        }

        private void comboHKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridDMH.DataSource = dmhbll.layDSDiemMH(comboHKy.SelectedValue.ToString().Trim(), comboML.SelectedValue.ToString().Trim(), labelMaHS.Text);

        }

        
    }
}
