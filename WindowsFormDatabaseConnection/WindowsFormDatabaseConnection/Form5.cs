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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        
        string connectionString = @"Data Source=DESKTOP-3EU7UMS\SQLEXPRESS;Initial Catalog=STUDENT_MANAGEMENT;Integrated Security=True";
        SqlDataAdapter adapter = null;  
        DataSet ds = null;

        private void Form5_Load(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter($"Select * from STUDENT",connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            ds = new DataSet();

            adapter.Fill(ds,"Student");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dtgStudent.DataSource = ds.Tables[0];
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int result = 0;

            //create a new row

            DataRow row = ds.Tables[0].NewRow();
            row[0] = txtStudentId.Text;
            row["Name"] = txtName.Text;
            row[2] = txtClassID.Text;

            ds.Tables[0].Rows.Add(row);

            try
            {
                adapter.Update(ds, "Student");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "\n Insert fail");
            }
            if (result > 0) MessageBox.Show("insert successful");


        }

        int position = -1; //there is no selected row

        private void dtgStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            position = e.RowIndex;
            if(position == -1)
            {
                MessageBox.Show("No row is select");
                return;
            }
            // get select row
            DataRow row = ds.Tables[0].Rows[position];
            txtStudentId.Text = row[0].ToString();
            txtName.Text = row["Name"].ToString();
            txtClassID.Text = row[2].ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(position == -1)
            {
                MessageBox.Show("No roe is select");
            }

            DataRow row = ds.Tables[0].Rows[position];
            //edit
            row.BeginEdit();
            row[0] = txtStudentId.Text;
            row[1] = txtName.Text;
            row[2] = txtClassID.Text;
            row.EndEdit();
            //update
            int result = adapter.Update(ds.Tables[0]);
            if(result > 0)
            {
                MessageBox.Show("Update Successful");
            }
            else
            {
                MessageBox.Show("Update Fail");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(position == -1)
            {
                MessageBox.Show("No roe is select");
            }

            DataRow row = ds.Tables[0].Rows[position];
            row.Delete();
            int result = adapter.Update(ds.Tables[0]);
            if(result > 0)
            {
                MessageBox.Show("Delete Successful");
            }
            else
            {
                MessageBox.Show("Delete Fail");
            }

            txtClassID.Text = "";
            txtName.Text = "";
            txtStudentId.Text = "";

        }
    }
}
