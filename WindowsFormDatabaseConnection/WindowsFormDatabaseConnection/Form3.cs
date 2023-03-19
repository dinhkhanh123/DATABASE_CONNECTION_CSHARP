using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormDatabaseConnection
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection conn = null;
        string connectionString = @"database = STUDENT_MANAGEMENT;Server =DESKTOP-3EU7UMS\SQLEXPRESS; user id = sa; password= 12341234";

        private void Form3_Load(object sender, EventArgs e)
        {
           


            if (conn == null) conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
         


             SqlCommand cmd = new SqlCommand($"select * from CLASS",conn);
            lsbClass.ClearSelected();
            SqlDataReader reader = cmd.ExecuteReader(); 
            while (reader.Read())
            {
                string classID = reader.GetString(0);
                string className = reader.GetString(1);
                string Year = reader.GetInt32(2).ToString();
                string line = classID + "-" + className + "-" + Year;
                lsbClass.Items.Add(line);

            }
            conn.Close();
        }

        private void lsbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsvStudent.Items.Clear();
            if (lsbClass.SelectedIndex == -1) return;
            string line = lsbClass.SelectedItem.ToString();
            string[] array = line.Split('-');
            string classID = array[0];

            if(conn == null) conn=new SqlConnection(connectionString);
            if(conn.State == ConnectionState.Closed) conn.Open();

            SqlCommand cmd = new SqlCommand($"select * from STUDENT where ClassID = '{classID}'",conn);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string studenID = reader.GetString(0);
                string name =  reader.GetString(1);
                string classIDRow = reader.GetString(2);
                ListViewItem item = lsvStudent.Items.Add(studenID);
                item.SubItems.Add(name);
                item.SubItems.Add(classIDRow);  
            }
            conn.Close();

        }
    }
}
