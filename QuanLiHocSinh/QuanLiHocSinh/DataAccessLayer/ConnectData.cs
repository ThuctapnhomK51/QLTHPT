using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace QuanLiHocSinh.DataAccessLayer
{
    class ConnectData
    {
        private SqlConnection conn;
        private SqlDataAdapter dataAp;
        private DataTable dataTable;
        private SqlCommand cmmd;
           //tao contructor goi ket noi khi new lop ConnectDB
        public ConnectData()
        {
            Connect();
        }
        //--------------
        public void Connect()
        {
            string strConn = @"Data Source=NPL97;Initial Catalog=QuanLiHocSinh;Integrated Security=True";
            try
            {
                conn = new SqlConnection(strConn);
                conn.Open();
                //conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi:" + ex.Message);

            }
        }
        //-----------------
        //ham lay du lieu DataTable tu cau truy van truyen vao
        public DataTable GetDataTable(string sql)
        {
            //tao dataAdapter, thuc hien cau lenh query
            dataAp = new SqlDataAdapter(sql, conn);
            //do du lieu vao dataTable
            dataTable = new DataTable();
            dataAp.Fill(dataTable);
            return dataTable;
        }
        //ham thuc hien truy van insert, del, update tra ve thuc hien thanh cong hay khong
        public bool ExecuteQuyery(string sql)
        {
            int numRecordsEffect = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                numRecordsEffect = cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi:" + ex.Message);
            }
            if (numRecordsEffect > 0)
                return true;
            return false;

        }
        //hamg them sua xoa qua paramates
        public bool ExxecuteNonQueryPara(string sql, string[] parameters, object[] values)
        {

            int number = 0;
            try
            {
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                cmmd = new SqlCommand(sql, conn);
                SqlParameter p;
                for (int i = 0; i < parameters.Length; i++)
                {
                    p = new SqlParameter(parameters[i], values[i]);
                    cmmd.Parameters.Add(p);

                }
                number = cmmd.ExecuteNonQuery();
            }
            catch (Exception ex) {}// MessageBox.Show("Lỗi5 :" + ex.Message); }
            if (number > 0)
                return true;
            else
                return false;
        }
        //lay ma cuoi cung
        public string GetLastID(string nameTable, string nameFiled)
        {
            string sql = "SELECT TOP 1 " + nameFiled + " FROM " + nameTable + " ORDER BY " + nameFiled + " DESC";
            //thuc hien cau truy van tren
            GetDataTable(sql);
            return dataTable.Rows[0][nameFiled].ToString();
        }
       
        public bool CheckExitsValue(string nameTable, string nameFiled, string value)
        {

            string sql = "SELECT * FROM " + nameTable + " WHERE " + nameFiled + " = '" + value + "' ";
            GetDataTable(sql);            
            if (dataTable.Rows.Count > 0)
                return true;
            return false;
        }
        public bool CheckLogin(string name, string pass)
        {

            Connect();
            SqlCommand cmd = new SqlCommand("select * from Dangnhap where tendn='" + name + "' and matkhau='" + pass + "'", conn);
            //conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                conn.Close();
                return true;
            }
            conn.Close();
            return false;
        }
    }
}
