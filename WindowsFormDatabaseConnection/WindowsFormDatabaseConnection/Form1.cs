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
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cnn = null;
         string connetionString = @"database = STUDENT_MANAGEMENT;Server = DESKTOP-3EU7UMS\SQLEXPRESS;user id= sa;password = 12341234";
       // string connetionString = "Data Source=DESKTOP-3EU7UMS\\SQLEXPRESS;Initial Catalog=STUDENT_MANAGEMENT;Integrated Security=True";


        private void btnConnect_Click(object sender, EventArgs e)
        {

            try
            {
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                MessageBox.Show("Successful Connection", "Hi");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Fail connection" + ex.Message);
            }


        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (cnn != null && cnn.State == ConnectionState.Open)
            {
                cnn.Close();
                MessageBox.Show("Successful Disconnect");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
