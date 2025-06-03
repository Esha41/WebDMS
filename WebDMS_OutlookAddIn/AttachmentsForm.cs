using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using System.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace WebDMS_OutlookAddIn
{
    public partial class AttachmentsForm : Form
    {

        public AttachmentsForm()
        {
            InitializeComponent();
        }

        private void CheckBoxChecked(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            var attanchmentName = checkBox.Text;
            if(StaticConstants.selectedAttanchmentNames.Any(x=>x==attanchmentName))
            {
                StaticConstants.selectedAttanchmentNames.Remove(attanchmentName);
            }
            else
            {
                StaticConstants.selectedAttanchmentNames.Add(attanchmentName);
            }
        }

        private void AttachmentSendButton(object sender, EventArgs e)
        {
            try
            {
                if (StaticConstants.selectedAttanchmentNames.Count > 0)
                {
                    // Get Application
                    Outlook.Application application = Globals.ThisAddIn.Application;

                    // Get the current item for this Inspect to object and check if is type
                    // of MailItem
                    Outlook.Inspector inspector = application.ActiveInspector();
                    Outlook.MailItem mailItem = inspector.CurrentItem as Outlook.MailItem;
                    Button button = sender as Button;
                    if (mailItem.Attachments.Count > 0)
                    {
                        for (int i = 1; i <= mailItem
                           .Attachments.Count; i++)
                        {
                            if (StaticConstants.selectedAttanchmentNames.Contains(mailItem.Attachments[i].FileName))
                            {
                                string directoryPath = StaticConstants.DirectoryPath;
                                string saveFilePath = $"{directoryPath}{mailItem.Attachments[i].FileName}";
                                
                                //If Current Document Version Directory does Not Exists Create It 
                                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

                                mailItem.Attachments[i].SaveAsFile
                                (saveFilePath);

                                using (var httpClient = new HttpClient())
                                using (var multipartFormContent = new MultipartFormDataContent())
                                {

                                    //Load the file and set the file's Content-Type header
                                    var fileStreamContent = new StreamContent(File.OpenRead(saveFilePath));
                                    var emailAddress = new StringContent(StaticConstants.Email);
                                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                                    //Add the file
                                    multipartFormContent.Add(fileStreamContent, name: "File", fileName: mailItem.Attachments[i].FileName);
                                    
                                    //Add the Email
                                    multipartFormContent.Add(emailAddress, "Email");

                                    //Send it
                                    var response = httpClient.PostAsync(StaticConstants.FileUploadUrl, 
                                                                        multipartFormContent).Result;
                                    string responseText = response.Content.ReadAsStringAsync().Result.ToString();

                                    if (File.Exists(saveFilePath))
                                    {
                                        File.Delete(saveFilePath);
                                    }

                                    if (response.StatusCode == HttpStatusCode.BadRequest)
                                    {
                                        MsgBox.Show($"{mailItem.Attachments[i].FileName}" +
                                                    $" {StaticConstants.FileUploadErrorMsg}" +
                                                    $" {responseText}",
                                                           "Error",
                                                           MsgBox.Buttons.OK,
                                                           MsgBox.Icon.Error,
                                                           MsgBox.AnimateStyle.FadeIn);
                                    }
                                    else if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        MsgBox.Show($"{mailItem.Attachments[i].FileName}" +
                                            $"{StaticConstants.FileUploadSuccessMsg} ",
                                                          "Message",
                                                          MsgBox.Buttons.OK,
                                                          MsgBox.Icon.Info,
                                                          MsgBox.AnimateStyle.FadeIn);
                                    }
                                    else if (response.StatusCode == HttpStatusCode.NotFound)
                                    {
                                        MsgBox.Show($"Url Not Found Set Base Url In Registry Variable which finds in  HKEY_CURRENT_USER > Web_DMS_Base_URL Response Status Code :: {response.StatusCode}",
                                                             "Error",
                                                             MsgBox.Buttons.OK,
                                                             MsgBox.Icon.Error,
                                                             MsgBox.AnimateStyle.FadeIn);
                                    }
                                    else
                                    {
                                        MsgBox.Show($"{mailItem.Attachments[i].FileName}" +
                                                    $" {StaticConstants.FileUploadErrorMsg}" +
                                                    $" {responseText}",
                                                             "Error",
                                                             MsgBox.Buttons.OK,
                                                             MsgBox.Icon.Error,
                                                             MsgBox.AnimateStyle.FadeIn);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MsgBox.Show(StaticConstants.AttanchmentsErrorMsg,
                                              "Message",
                                              MsgBox.Buttons.OK,
                                              MsgBox.Icon.Info,
                                              MsgBox.AnimateStyle.FadeIn);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show($"File Not Upload {ex.Message}",
                                                             "Error",
                                                             MsgBox.Buttons.OK,
                                                             MsgBox.Icon.Error,
                                                             MsgBox.AnimateStyle.FadeIn);
            }
        }

        private void AttachmentsForm_Load(object sender, EventArgs e)
        {
            StaticConstants.selectedAttanchmentNames = new List<string>();
            // Get Application
            Outlook.Application application = Globals.ThisAddIn.Application;

            // Get the current item for this Inspect to object and check if is type
            // of MailItem
            Outlook.Inspector inspector = application.ActiveInspector();
            Outlook.MailItem mailItem = inspector.CurrentItem as Outlook.MailItem;
            label1.Text = StaticConstants.AttanchmentLabelText;
            checkBox1.Text = StaticConstants.SelectAll;

            if (mailItem.Attachments.Count > 0)
            {
                int index = 1;
                for (int i = 1; 
                         i <= mailItem.Attachments.Count; 
                         i++)
                {
                    if (i != 0) { index = i; }
                    CheckBox checkBox = new CheckBox();
                    checkBox.Text = mailItem.Attachments[i].FileName;
                    checkBox.Location = new Point(100, index * 800);
                    checkBox.AutoSize = true;
                    checkBox.CheckedChanged += CheckBoxChecked;
                    checkBox.Visible = true;
                    checkBox.BringToFront();
                    flowLayoutPanel1.Controls.Add(checkBox);
                }

                button1.Text = StaticConstants.AttanchmentButtonText;
                button1.Click += AttachmentSendButton;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if(item is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)item;
                    if (checkBox.Checked)
                    {
                        checkBox.Checked = false;
                    }
                    else
                    {
                        checkBox.Checked = true;
                    }
                }
            }
        }
    }
}
