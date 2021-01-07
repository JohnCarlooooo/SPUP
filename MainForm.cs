using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.IO.Ports;
using System.Threading.Tasks;

namespace SPUP
{
    public partial class Main : Form
    {
        /// <Movingaborderlesswindow>
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        /// </Movingaborderlesswindow>

        /// <GlobalVariables>
        /// 
        /// <ConnectionStringForDatabase>
        Configuration config;
        string connectionString = "";
        /// </ConnectionStringForDatabase>

        public static string RfidPort = "";

        private SerialPort Arduino;

        public static string ReceivedRfid;

        /// <DatetimeUsedForCurrentDate,BirthdayAndTimerforDigitalClock>
         DateTime Today = new DateTime();
         DateTime Birthdate = new DateTime();
         Timer time = new Timer();
        /// </DatetimeUsedForCurrentDate,BirthdayAndTimerforDigitalClock>

        /// <DataTableToStoreDataSelectedFromDatabase>
        DataTable visitorsTable = new DataTable();
        /// </DataTableToStoreDataSelectedFromDatabase>
        /// 
        /// </GlobalVariables>
        /// 
        private Timer timer1;
        public Main()
        {
            Arduino = new SerialPort();
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            InitializeComponent();
        }
        public string getConnectionString()
        {
            //returns connectionstring from app.config
            return config.ConnectionStrings.ConnectionStrings["connection"].ConnectionString;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(RFIDStatus);
            timer1.Interval = 2000; // in miliseconds
            timer1.Start();

            //gets connection string from app.config and stored in connectionString variable
            connectionString = getConnectionString();

            //Setting background and foreground color for aesthetics//
            TitlePage_btn.ForeColor = System.Drawing.ColorTranslator.FromHtml("#044a33");
            TitlePage_btn.BackColor = SystemColors.Control;
            Log_btn.ForeColor = SystemColors.Control;
            Log_btn.BackColor = System.Drawing.ColorTranslator.FromHtml("#044a33");

            //Getting current time and formatting//
            Today = DateTime.Now;
            Date.Text = Today.ToString("dddd, dd MMMM yyyy");

            //Set Datetimepicker's max date for birthday
            Birthday_date.MaxDate = Today.AddDays(1);

            //Showing the Mainpage and hiding the Logpage
            Main_Panel.Visible = true;
            Log_Panel.Visible = false;

            //timer interval
            time.Interval = 1000;  //in milliseconds
            time.Tick += new EventHandler(this.time_tick);
            //start timer when form loads
            time.Start();  //this will use t_Tick() method\

            //Get Current data from database for today
            GetDataToday();

            Dept_cb.SelectedItem = "Select Department";

            //RFIDStatus();
        }

        public void KeyPressAlphabetOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;

            FName_tb.CharacterCasing = CharacterCasing.Upper;
            LName_tb.CharacterCasing = CharacterCasing.Upper;
            MName_tb.CharacterCasing = CharacterCasing.Upper;
        }
        private void Admit_btn_Click(object sender, EventArgs e)
        {
            //Checks If there are missing fields, Admit if Validation is true and Displays message if false
            if (Validation())
            {
                AdmitVisitor();
                clearForm();
            }
            else
            {
                MessageBox.Show("Missing or invalid fields", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        ///DigitalClockTick
        private void time_tick(object sender, EventArgs e)
        {
            //get current time
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;
            string ampm = "";

            //time
            string time = "";

            //padding leading zero

            if (hh > 12)
            {
                hh = hh - 12;
                ampm = " PM";
            }
            else
            {
                ampm = " AM";
            }

            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }
            //update label
            TimeToday.Text = time + ampm;
        }

        private void TitlePage_btn_Click(object sender, EventArgs e)
        {
            //Setting background and foreground color for aesthetics//
            TitlePage_btn.ForeColor = System.Drawing.ColorTranslator.FromHtml("#044a33");
            TitlePage_btn.BackColor = SystemColors.Control;
            Log_btn.ForeColor = SystemColors.Control;
            Log_btn.BackColor = System.Drawing.ColorTranslator.FromHtml("#044a33");

            Log_Panel.Visible = false;
            GetDataToday();
        }

        private void Log_btn_Click(object sender, EventArgs e)
        {
            //Setting background and foreground color for aesthetics//
            Log_btn.ForeColor = System.Drawing.ColorTranslator.FromHtml("#044a33");
            Log_btn.BackColor = SystemColors.Control;
            TitlePage_btn.ForeColor = SystemColors.Control;
            TitlePage_btn.BackColor = System.Drawing.ColorTranslator.FromHtml("#044a33");

            //Show Logpage and display log
            Log_Panel.Visible = true;
            DisplayLog();
        }
        private void GetDataToday()
        {
            //Get current time
            Today = DateTime.Now;

            //Set SqlQuery to Select Data For Today And Execute Said Query
            string QueryToday = $"SELECT log.LogNumber, log.ID, log.Temperature, log.Purpose, log.TimeIn," +
                $" students.FName, students.MName, students.LName, students.PhoneNumber, students.Address, students.Department," +
                $" TIMESTAMPDIFF(YEAR, students.Birthday, CURRENT_DATE()) AS AGE" +
                $" FROM log" +
                $" INNER JOIN students ON students.ID=log.ID" +
                $" WHERE DATEDIFF(TimeIn, '{ Today.ToString("yyyy-MM-dd") }') = 0" +
                $" UNION" +
                $" SELECT log.LogNumber, log.ID, log.Temperature, log.Purpose, log.TimeIn," +
                $" visitors.FName, visitors.MName, visitors.LName, visitors.PhoneNumber, visitors.Address, visitors.Department," +
                $" TIMESTAMPDIFF(YEAR, visitors.Birthday, CURRENT_DATE()) AS AGE" +
                $" FROM log" +
                $" INNER JOIN visitors ON visitors.ID=log.ID" +
                $" WHERE DATEDIFF(TimeIn, '{ Today.ToString("yyyy-MM-dd") }') = 0" +
                $" ORDER BY TimeIn ASC";

            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(QueryToday, con);
            MySqlDataAdapter visitors = new MySqlDataAdapter();
            visitors.SelectCommand = command;

            //Clear The Datatable And Populate With New Data From Database
            visitorsTable.Clear();
            visitors.Fill(visitorsTable);
            VisitorsToday_grid.Rows.Clear();

            //Populate DataGrid With Datatable data
            for (int x = 0; x < visitorsTable.Rows.Count; x++)
            {
                VisitorsToday_grid.Rows.Add((x + 1), $"{visitorsTable.Rows[x]["FName"]} {visitorsTable.Rows[x]["MName"]} {visitorsTable.Rows[x]["LName"]}"
                    , visitorsTable.Rows[x]["Age"], visitorsTable.Rows[x]["PhoneNumber"], visitorsTable.Rows[x]["Address"], visitorsTable.Rows[x]["Department"], visitorsTable.Rows[x]["Temperature"], visitorsTable.Rows[x]["Purpose"], visitorsTable.Rows[x]["TimeIn"]);

                VisitorsToday_grid.Columns[8].DefaultCellStyle.Format = "hh:mm tt";
            }
            con.Close();
        }

        private void AdmitVisitor()
        {
            //Get Current Time, Set SqlQuery to Select Data For Today And Execute Said Query
            Today = DateTime.Now;
            string dtoday = Today.ToString("yyyy-MM-dd H:mm:ss");
            string dbirth = Birthday_date.Value.ToString("yyyy-MM-dd H:mm:ss");
            string QueryVisitor = $"INSERT INTO Visitors (FName, MName, LName, Birthday, PhoneNumber, Address, Department) " +
                $"values ( '{FName_tb.Text}', '{MName_tb.Text}', '{LName_tb.Text}', '{dbirth}', {Phone_tb.Text} , '{ Address_tb.Text }', '{ Dept_cb.Text }');" +
                $"INSERT INTO Log (ID, Purpose, TimeIn, Temperature) Values (LAST_INSERT_ID(), '{Purpose_tb.Text}', '{dtoday}', '{ Temperature_tb.Text }')";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(QueryVisitor, con);
            command.ExecuteNonQuery();
            con.Close();

            //ShowMessage
            dtoday = Today.ToString("h:mm tt");
            MessageBox.Show($"Visitor {LName_tb.Text}, {FName_tb.Text} {MName_tb.Text} of age {Age_tb.Text} has entered the campus at {dtoday}", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Update Data in Data Grid
            GetDataToday();
        }

        private void Birthday_date_ValueChanged(object sender, EventArgs e)
        {
            //Update The Textbox for Age when Birthday is Changed
            Today = DateTime.Now;
            Birthdate = new DateTime(Birthday_date.Value.Year, Birthday_date.Value.Month, Birthday_date.Value.Day, 0, 0, 0);
            TimeSpan YearDiff = new TimeSpan();
            YearDiff = Today - Birthdate;
            if (((int)(YearDiff.Days / 365.25)) < 10)
            {
                Age_tb.Text = "";
            }
            else
            {
                Age_tb.Text = ((int)(YearDiff.Days / 365.25)).ToString();
            }
        }

        private void Top_MouseDown(object sender, MouseEventArgs e)
        {
            //For Moving a Borderless Window
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void DisplayLog()
        {
            //Display all the values in log if DislayAll is selected and Data for a specific data if ByDate is selected
            if (All_rb.Checked == true)
            {
                DisplayAll();
            }
            else if (Date_rb.Checked == true)
            {
                DisplaybyDate();
            }
        }

        private void Date_rb_CheckedChanged(object sender, EventArgs e)
        {
            //Update DataGrid from Log if ByDate is selected
            if (Date_rb.Checked == true)
            { 
                DisplaybyDate();
                DatetoDisplay.Enabled = true;
            }
        }

        private void All_rb_CheckedChanged(object sender, EventArgs e)
        {
            //Update DataGrid from Log if DisplayAll is selected
            if (All_rb.Checked == true)
            {
                DisplayAll();
                DatetoDisplay.Enabled = false;
            }
        }

        private void DisplayAll()
        {
            //Set SqlQuery to Select All The Data And Execute Said Query

            string QueryLog = $"SELECT log.ID, log.Temperature, log.Purpose, log.TimeIn," +
                $" students.FName, students.MName, students.LName, students.PhoneNumber, students.Address, students.Department," +
                $" TIMESTAMPDIFF(YEAR, students.Birthday, CURRENT_DATE()) AS AGE" +
                $" FROM log" +
                $" INNER JOIN students ON students.ID=log.ID" +
                $" UNION" +
                $" SELECT log.ID, log.Temperature, log.Purpose, log.TimeIn," +
                $" visitors.FName, visitors.MName, visitors.LName, visitors.PhoneNumber, visitors.Address, visitors.Department," +
                $" TIMESTAMPDIFF(YEAR, visitors.Birthday, CURRENT_DATE()) AS AGE" +
                $" FROM log" +
                $" INNER JOIN visitors ON visitors.ID=log.ID" +
                $" ORDER BY TimeIn ASC";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(QueryLog, con);
            MySqlDataAdapter visitors = new MySqlDataAdapter();
            visitors.SelectCommand = command;

            //Clear The Datatable And Populate With New Data From Database
            visitorsTable.Clear();
            visitors.Fill(visitorsTable);
            DataLog.Rows.Clear();

            //Populate DataGrid With Datatable data
            for (int x = 0; x < visitorsTable.Rows.Count; x++)
            {
                DataLog.Rows.Add((x + 1), $"{visitorsTable.Rows[x]["FName"]} {visitorsTable.Rows[x]["MName"]} {visitorsTable.Rows[x]["LName"]}"
                    , visitorsTable.Rows[x]["Age"], visitorsTable.Rows[x]["PhoneNumber"], visitorsTable.Rows[x]["Address"], visitorsTable.Rows[x]["Department"], visitorsTable.Rows[x]["Temperature"], visitorsTable.Rows[x]["Purpose"], visitorsTable.Rows[x]["TimeIn"]);

                DataLog.Columns[8].DefaultCellStyle.Format = "hh:mm tt";

            }


            con.Close();
        }
        private void DisplaybyDate()
        {
            //Set SqlQuery to Select Data From Specific Date And Execute Said Query
            string QueryLog = $"SELECT log.ID, log.Temperature, log.Purpose, log.TimeIn," +
                $" students.FName, students.MName, students.LName, students.PhoneNumber, students.Address, students.Department," +
                $" TIMESTAMPDIFF(YEAR, students.Birthday, CURRENT_DATE()) AS AGE" +
                $" FROM log" +
                $" INNER JOIN students ON students.ID=log.ID" +
                $" WHERE DATEDIFF(TimeIn, '{ DatetoDisplay.Value.ToString("yyyy-MM-dd") }') = 0" +
                $" UNION" +
                $" SELECT log.ID, log.Temperature, log.Purpose, log.TimeIn," +
                $" visitors.FName, visitors.MName, visitors.LName, visitors.PhoneNumber, visitors.Address, visitors.Department," +
                $" TIMESTAMPDIFF(YEAR, visitors.Birthday, CURRENT_DATE()) AS AGE" +
                $" FROM log" +
                $" INNER JOIN visitors ON visitors.ID=log.ID" +
                $" WHERE DATEDIFF(TimeIn, '{ DatetoDisplay.Value.ToString("yyyy-MM-dd") }') = 0" +
                $" ORDER BY TimeIn ASC";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand command = new MySqlCommand(QueryLog, con);
            MySqlDataAdapter visitors = new MySqlDataAdapter();
            visitors.SelectCommand = command;

            //Clear The Datatable And Populate With New Data From Database
            visitorsTable.Clear();
            visitors.Fill(visitorsTable);
            DataLog.Rows.Clear();

            //Populate DataGrid With Datatable data
            for (int x = 0; x < visitorsTable.Rows.Count; x++)
            {
                DataLog.Rows.Add((x + 1), $"{visitorsTable.Rows[x]["FName"]} {visitorsTable.Rows[x]["MName"]} {visitorsTable.Rows[x]["LName"]}"
                    , visitorsTable.Rows[x]["Age"], visitorsTable.Rows[x]["PhoneNumber"], visitorsTable.Rows[x]["Address"], visitorsTable.Rows[x]["Department"], visitorsTable.Rows[x]["Temperature"], visitorsTable.Rows[x]["Purpose"], visitorsTable.Rows[x]["TimeIn"]);

                DataLog.Columns[8].DefaultCellStyle.Format = "hh:mm tt";

            }

            con.Close();
        }
        private void DatetoDisplay_ValueChanged(object sender, EventArgs e)
        {
            //Update DataGrid from Log if Selected Date is changed
            DisplaybyDate();
        }

        private void clearForm()
        {
            FName_tb.Text = "";
            MName_tb.Text = "";
            LName_tb.Text = "";
            Birthday_date.Value = DateTime.Now;
            Age_tb.Text = "";
            Phone_tb.Text = "";
            Address_tb.Text = "";
            Dept_cb.Text = "Select Department";
            Purpose_tb.Text = "";

            FName_tb.BackColor = Color.White;
            MName_tb.BackColor = Color.White;
            LName_tb.BackColor = Color.White;
            Age_tb.BackColor = Color.White;
            Phone_tb.BackColor = Color.White;
            Address_tb.BackColor = Color.White;
            Dept_cb.BackColor = Color.White;
            Purpose_tb.BackColor = Color.White;
        }

        private bool Validation()
        {
            //Setting Default Colors for TextBoxes
            FName_tb.BackColor = Color.White;
            MName_tb.BackColor = Color.White;
            LName_tb.BackColor = Color.White;
            Age_tb.BackColor = Color.White;
            Phone_tb.BackColor = Color.White;
            Address_tb.BackColor = Color.White;
            Dept_cb.BackColor = Color.White;
            Purpose_tb.BackColor = Color.White;
            Temperature_tb.BackColor = Color.White;

            //Checks if there are blanks textboxes, Change the textbox color if it is empty and Increment the ErrorCount
            int numErrors = 0;
            if (FName_tb.Text == "")
            {
                numErrors++;
                FName_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
            }
            if (MName_tb.Text == "")
            {
                numErrors++;
                MName_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
            }
            if (LName_tb.Text == "")
            {
                numErrors++;
                LName_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
            }
            if (Age_tb.Text == "")
            {
                numErrors++;
                Age_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
            }
            if(Temperature_tb.Text.Length == 1)
            {
                if (Temperature_tb.Text[0].ToString() == ".")
                {
                    numErrors++;
                    Temperature_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
                }
            }
            if (Temperature_tb.Text.Length == 2)
            {
                if (Temperature_tb.Text[0].ToString() == "." || Temperature_tb.Text[1].ToString() == "." )
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
                if (Temperature_tb.Text[0].ToString() == "." || Temperature_tb.Text[1].ToString() == "." || Temperature_tb.Text[3].ToString() == "." )
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
            if (Dept_cb.Text == "Select Department")
            {
                numErrors++;
                Dept_cb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
            }
            if (Purpose_tb.Text == "")
            {
                numErrors++;
                Purpose_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
            }
            if (Phone_tb.Text.Length != 11)
            {
                numErrors++;
                Phone_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
            }
            if (Address_tb.Text == "")
            {
                numErrors++;
                Address_tb.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff7a7a");
            }

            //Returns False if there are Errors/Blanktextboxes and returns True if there are non
            if (numErrors != 0)
            {
                return false;
            }
            else return true;
        }


        private void Close_btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to close this window?", "Close Window", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                // Do something  
            }
        }

        private void about_btn_Click(object sender, EventArgs e)
        {
            about_Form aboutForm = new about_Form();
            aboutForm.ShowDialog();
        }

        private void Phone_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void Settings_btn_Click(object sender, EventArgs e)
        {
            Settings NewSet = new Settings();
            NewSet.ShowDialog();
            if(RfidPort != "" && !Arduino.IsOpen)
            {
                SetRFID();
            }
        }
        private void SetRFID()
        {
            Arduino.BaudRate = 9600;
            Arduino.PortName = RfidPort;
            Arduino.DataBits = 8;
            Arduino.StopBits = StopBits.One;
            Arduino.DataReceived += RFID_DataReceived;

            try
            {
                Arduino.Open();
                MessageBox.Show("Connected!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                //RfidPort = "";
            }

        }

        void RFID_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReceivedRfid="";
            ReceivedRfid = Arduino.ReadLine().Replace("\r", string.Empty);
            ReceivedRfid = ReceivedRfid.Substring(ReceivedRfid.Length - 8);

            Arduino.Close();
            this.Invoke(new EventHandler(displaydata_event));
            Arduino.Open();
        }

        private void displaydata_event(object sender, EventArgs e)
        {
            StudentEntry newStudent = new StudentEntry();
            newStudent.ShowDialog();
            GetDataToday();
        }

        private void RFIDStatus(object sender, EventArgs e)
        {
            if(Arduino.IsOpen)
            {
                RfidStatus_lbl.Text = $"RFID : Available ({ RfidPort })";
            }
            else
            {
                RfidStatus_lbl.Text = $"RFID : Unavailable";
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
    }
}
