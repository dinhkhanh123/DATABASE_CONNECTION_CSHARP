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
using System.Xml.Linq;

namespace WindowsFormDatabaseConnection
{
    public partial class Form4 : Form
    {
       
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection conn = null;
        string connectionString = @"Server = DESKTOP-3EU7UMS\SQLEXPRESS;database = STUDENT_MANAGEMENT;user id = sa;password = 12341234";
        private void Form4_Load(object sender, EventArgs e)
        {
            ViewListOfStudent();
        }



       public void ViewListOfStudent()
        {
            lsvStudent.Items.Clear();


            if (conn == null) conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand command = new SqlCommand($"select * from STUDENT", conn);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string studentID = reader.GetString(0);
                string fullName = reader.GetString(1);
                string classID = reader.GetString(2);
                ListViewItem item = lsvStudent.Items.Add(studentID);
                item.SubItems.Add(fullName);
                item.SubItems.Add(classID);

            }
            conn.Close();
        }






        int result = -1;
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (conn == null) conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.Connection = conn;
            command.CommandText = "Insert into STUDENT (StudentID,Name,ClassID) " +
                "values(@StudentID,@Name,@ClassID)";

            SqlParameter parameter1 = new SqlParameter("@StudentID",txtStudentId.Text);
            command.Parameters.Add(parameter1);
            SqlParameter parameter2 = new SqlParameter("@Name", txtFullName.Text);
            command.Parameters.Add(parameter2);
            SqlParameter parameter3 = new SqlParameter("@ClassID", txtClassId.Text);
            command.Parameters.Add(parameter3);

            try
            {
                result = command.ExecuteNonQuery();
                //using excutenonquery if SQL command = insert,update,delete;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n Insert a record failed");
            }

            if(result > 0)
            {
                ViewListOfStudent();
            }

        }

        private void lsvStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lsvStudent.SelectedItems.Count > 0)
            {
                txtStudentId.Text = lsvStudent.SelectedItems[0].SubItems[0].Text;
                txtFullName.Text = lsvStudent.SelectedItems[0].SubItems[1].Text;
                txtClassId.Text = lsvStudent.SelectedItems[0].SubItems[2].Text;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (conn == null) conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand command = new SqlCommand($"update STUDENT set Name = '{txtFullName.Text}',ClassID='{txtClassId.Text}' where StudentID = '{txtStudentId.Text}' ",conn);
            try
            {
               result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "update fail");
            }

            if(result > 0)
            {
                ViewListOfStudent();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (conn == null) conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand command = new SqlCommand($"delete from STUDENT where StudentID = '{txtStudentId.Text}' ",conn);

            try
            {
                result = command.ExecuteNonQuery();
                MessageBox.Show("delete success");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "\ndelete fail");
            }

            if( result > 0) { 
                ViewListOfStudent();
                txtClassId.Text = "";
                txtFullName.Text = "";
                txtStudentId.Text = "";
            }
        }
    }
}
