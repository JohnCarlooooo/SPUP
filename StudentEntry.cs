using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using System.Configuration;

namespace SPUP
{
    public partial class StudentEntry : Form
    {

        DataTable SearchTable = new DataTable();
        Configuration config;
        string connectionString = "";
        public StudentEntry()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            InitializeComponent();
        }
        public string getConnectionString()
        {
            //returns connectionstring from app.config
            return config.ConnectionStrings.ConnectionStrings["connection"].ConnectionString;
        }
        private void SearchStudent()
        {
            string QuerySearch = $"SELECT *, TIMESTAMPDIFF(YEAR, students.Birthday, CURRENT_DATE()) AS AGE FROM `students` WHERE RFIDTag = '{ Main.ReceivedRfid }'";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(QuerySearch, con);
            MySqlDataAdapter student = new MySqlDataAdapter();
            student.SelectCommand = command;

            SearchTable.Rows.Clear();
            student.Fill(SearchTable);

            if(SearchTable.Rows.Count == 1)
            {
                setForm();
            }
            else
            {
                MessageBox.Show("Card is not registered");
                Close();
            }
        }

        private void StudentEntry_Load(object sender, EventArgs e)
        {
            connectionString = getConnectionString();
            SearchStudent();
            //Close();
        }
        private void setForm()
        {
            FullName_lbl.Text = $"{SearchTable.Rows[0]["FName"].ToString()} {SearchTable.Rows[0]["MName"].ToString()} {SearchTable.Rows[0]["LName"].ToString()}";

            Address_lbl.Text = $"{SearchTable.Rows[0]["Address"].ToString()}";
            Age_lbl.Text = $"Age {SearchTable.Rows[0]["Age"].ToString()}";
            Phone_lbl.Text = $"CP# : {SearchTable.Rows[0]["PhoneNumber"].ToString()}";
            Dept_lbl.Text = $"{SearchTable.Rows[0]["Department"].ToString()}";
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Admit_btn_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                //Get Current Time, Set SqlQuery to Select Data For Today And Execute Said Query
                DateTime Today = DateTime.Now;
                string dtoday = Today.ToString("yyyy-MM-dd H:mm:ss");
                string QueryToday = $"INSERT INTO Log (ID, Temperature, Purpose, TimeIn) Values ({SearchTable.Rows[0]["ID"]}, '{Temperature_tb.Text}', 'Student', '{dtoday}')";
                MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();
                MySqlCommand command = new MySqlCommand(QueryToday, con);
                command.ExecuteNonQuery();
                con.Close();

                //ShowMessage
                dtoday = Today.ToString("h:mm tt");
                MessageBox.Show($"Visitor {SearchTable.Rows[0]["LName"].ToString()}, {SearchTable.Rows[0]["FName"].ToString()} {SearchTable.Rows[0]["MName"].ToString()} of age {SearchTable.Rows[0]["Age"].ToString()} has entered the campus at {dtoday}", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
            else
            {
                MessageBox.Show("Missing or invalid fields", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Temperature_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == 46)
            {
                if (Temperature_tb.Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
        private bool Validation()
        {
            int numErrors = 0;
            Temperature_tb.BackColor = Color.White;

            if (Temperature_tb.Text.Length == 1)
            {
                if (Temperature_tb.Text[0].ToString() == ".")
                {
                    numErrors++;
                    Temperature_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
                }
            }
            if (Temperature_tb.Text.Length == 2)
            {
                if (Temperature_tb.Text[0].ToString() == "." || Temperature_tb.Text[1].ToString() == ".")
                {
                    numErrors++;
                    Temperature_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
                }
            }

            if (Temperature_tb.Text.Length == 3)
            {
                if (Temperature_tb.Text[0].ToString() == "." || Temperature_tb.Text[1].ToString() == ".")
                {
                    numErrors++;
                    Temperature_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
                }
            }
            if (Temperature_tb.Text.Length == 4)
            {
                if (Temperature_tb.Text[0].ToString() == "." || Temperature_tb.Text[1].ToString() == "." || Temperature_tb.Text[3].ToString() == ".")
                {
                    numErrors++;
                    Temperature_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
                }
            }

            if (Temperature_tb.Text.Length == 5)
            {
                if (Temperature_tb.Text[0].ToString() == "." || Temperature_tb.Text[1].ToString() == "." || Temperature_tb.Text[3].ToString() == "." || Temperature_tb.Text[4].ToString() == ".")
                {
                    numErrors++;
                    Temperature_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
                }
            }
            if (Temperature_tb.Text == "" || Temperature_tb.Text.IndexOf(".") == -1 || Temperature_tb.Text[Temperature_tb.Text.Length - 1].ToString() == ".")
            {
                numErrors++;
                Temperature_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
            }

            if (numErrors != 0)
            {
                return false;
            }
            else return true;
        }

    }
}
