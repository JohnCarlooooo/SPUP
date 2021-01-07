using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace SPUP
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if(Main.RfidPort == "")
            {
                Selected_lbl.Text = $"Selected Port : none";
            }
            else
            {
                Selected_lbl.Text = $"Selected Port : {Main.RfidPort}";
            }
            SearchPorts();
        }
        private void Search_btn_Click(object sender, EventArgs e)
        {
            SearchPorts();
        }
        private void SearchPorts()
        {
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);
            Ports_cb.Items.Clear();
            Ports_cb.Items.Add("Select Device");
            Ports_cb.SelectedIndex = 0;

            foreach (ManagementObject item in searcher.Get())
            {
                string desc = item["Description"].ToString();
                string deviceId = item["DeviceID"].ToString();
                Ports_cb.Items.Add(deviceId);
            }
            if (Ports_cb.Items.Count < 2)
            {
                MessageBox.Show("There are no serial devices available!");
                Apply_btn.Enabled = false;
            }
            else
            {
                MessageBox.Show($"There are { Ports_cb.Items.Count - 1 } serial device(s) available.");
                Apply_btn.Enabled = true;
            }

        }

        private void Apply_btn_Click(object sender, EventArgs e)
        {
            if (Ports_cb.Text == "Select Device")
            {
                MessageBox.Show($"Please select a device.");
            }
            else
            {
                MessageBox.Show($"{ Ports_cb.Text } is selected.");
                Main.RfidPort = Ports_cb.Text;
                Close();
            }
        }
    }
}
