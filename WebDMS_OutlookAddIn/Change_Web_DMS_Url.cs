using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDMS_OutlookAddIn
{
    public partial class Change_Web_DMS_Url : Form
    {
        public Change_Web_DMS_Url()
        {
            InitializeComponent();
        }

        private void Change_Web_DMS_Url_Load(object sender, EventArgs e)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "Web_DMS_Base_URL";
            const string keyName = userRoot + "\\" + subkey;
            string defaultUrl = "http://20.82.3.149:56981";
            string baseurlNull = "";
            string baseUrl = Registry.GetValue(keyName, "", baseurlNull)?.ToString();
            if (string.IsNullOrEmpty(baseUrl))
            {
                Registry.SetValue(keyName, "", defaultUrl);
                baseUrl = Registry.GetValue(keyName, "", null).ToString();
               
            }
            textBox1.Text = baseUrl;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "Web_DMS_Base_URL";
            const string keyName = userRoot + "\\" + subkey;
            string baseurl = textBox1.Text.TrimEnd('/').ToString(); 
            
            Registry.SetValue(keyName, "", baseurl);
            
            MsgBox.Show($"Base Url Changed",
                           "Message",
                           MsgBox.Buttons.OK,
                           MsgBox.Icon.Info,
                           MsgBox.AnimateStyle.FadeIn);
        }
    }
}
