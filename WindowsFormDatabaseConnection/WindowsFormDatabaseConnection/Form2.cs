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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
       // string connectionString = @"database = STUDENT_MANAGEMENT;Server = localhost;user id = sa;password = 12341234";
        string connectionString = @"Data Source=DESKTOP-3EU7UMS\SQLEXPRESS;Initial Catalog=STUDENT_MANAGEMENT;Integrated Security=True";
        private void btn_Detail_Click(object sender, EventArgs e)
        {
            if (conn == null)
                conn = new SqlConnection(connectionString);
               if(conn.State == ConnectionState.Closed)
            conn.Open();

            txtId.Text = "";
            txtName.Text = "";
            txtYear.Text = "";
          

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"Select * from CLASS where ClassID = '{txtEnterId.Text}'";
            command.Connection = conn;  
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtId.Text = reader.GetString(0);
                txtName.Text = reader.GetString(1);
                txtYear.Text = reader.GetInt32(2).ToString();
            }
            conn.Close();   

        }

        private void btnViewList_Click(object sender, EventArgs e)
        {
            lsvStudent.Items.Clear();   
            if(conn == null) conn = new SqlConnection(connectionString);
            if(conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand command = new SqlCommand($"select * from Student where ClassID ='{txtEnterId.Text}'", conn);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string studentID = reader.GetString(0);
                string name = reader.GetString(1);
                string classID = reader.GetString(2);
                ListViewItem item = new ListViewItem(studentID);
                item.SubItems.Add(name);
                item.SubItems.Add(classID);
                lsvStudent.Items.Add(item); 
            }
            conn.Close();

        }
        
    }
}
