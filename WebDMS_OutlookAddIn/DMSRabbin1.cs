using Microsoft.Office.Tools.Ribbon;
using System;
using System.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net.Mail;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebDMS_OutlookAddIn
{
    public partial class DMSRabbin1
    {
        
        private void DMSRabbin1_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                // Get Application
                Outlook.Application application = Globals.ThisAddIn.Application;

                // Get the current item for this Inspect to object and check if is type
                // of MailItem
                Outlook.Inspector inspector = application.ActiveInspector();
                Outlook.MailItem mailItem = inspector.CurrentItem as Outlook.MailItem;
                string sendEmail = mailItem.SenderEmailAddress;
                string toEmail = GetSMTPAddressForRecipients(mailItem);
                MailMessage mailMessage = new MailMessage(sendEmail,
                                                          toEmail);
                mailMessage.Subject = mailItem.Subject;
                mailMessage.Body = mailItem.Body;
                
                //If Current Document Version Directory does Not Exists Create It 
                if (!Directory.Exists("C:\\WebDMS Email Body As EML\\"))
                {
                    Directory.CreateDirectory("C:\\WebDMS Email Body As EML\\");
                }

                SmtpClient client = new SmtpClient("mysmtphost");
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = @"C:\WebDMS Email Body As EML\";
                client.Send(mailMessage);

                var directory = new DirectoryInfo(client.PickupDirectoryLocation);
                var myFile = (from f in directory.GetFiles()
                              orderby f.LastWriteTime descending
                              select f).First();

                using (var httpClient = new HttpClient())
                using (var multipartFormContent = new MultipartFormDataContent())
                {

                    //Load the file and set the file's Content-Type header
                    var fileStreamContent = new StreamContent(File.OpenRead(myFile.FullName));
                    var emailAddress = new StringContent(StaticConstants.Email);
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    string mailsubjectText = Regex.Replace(mailItem?.Subject, "[^a-zA-Z0-9\\s]", String.Empty);
                    //Add the file
                    multipartFormContent.Add(fileStreamContent, name: "File", fileName: $"{mailsubjectText}_.eml");
                    multipartFormContent.Add(emailAddress, "Email");

                    //Send it
                    var response =new HttpResponseMessage();
                    try
                    {
                        response = httpClient.PostAsync(StaticConstants.FileUploadUrl, multipartFormContent).Result;
                    }
                    catch(Exception ex)
                    {
                        MsgBox.Show($"{StaticConstants.FileUploadErrorMsg} Web DMS Not Connect.Its May be Internet Connection Problem Or Web DMS Not available .",
                                          "Error",
                                          MsgBox.Buttons.OK,
                                          MsgBox.Icon.Error,
                                          MsgBox.AnimateStyle.FadeIn);
                        return;
                    }
                    string responseText = response?.Content?.ReadAsStringAsync()?.Result?.ToString();

                    if (File.Exists(myFile.FullName))
                    {
                        File.Delete(myFile.FullName);
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        MsgBox.Show($"{StaticConstants.FileUploadErrorMsg} {responseText}",
                                           "Error",
                                           MsgBox.Buttons.OK,
                                           MsgBox.Icon.Error,
                                           MsgBox.AnimateStyle.FadeIn);
                    }
                    else if (response.StatusCode == HttpStatusCode.OK)
                    {
                        MsgBox.Show($"{StaticConstants.FileUploadSuccessMsg} ",
                                          "Message",
                                          MsgBox.Buttons.OK,
                                          MsgBox.Icon.Info,
                                          MsgBox.AnimateStyle.FadeIn);
                    }
                    else if(response.StatusCode == HttpStatusCode.NotFound)
                    {
                        MsgBox.Show($"Url Not Found Set Base Url In Registry Variable which finds in  HKEY_CURRENT_USER > Web_DMS_Base_URL Response Status Code :: {response.StatusCode}",
                                             "Error",
                                             MsgBox.Buttons.OK,
                                             MsgBox.Icon.Error,
                                             MsgBox.AnimateStyle.FadeIn);
                    }
                    else
                    {
                        MsgBox.Show($"{StaticConstants.FileUploadErrorMsg} {responseText} Response Status Code :: {response.StatusCode}",
                                             "Error",
                                             MsgBox.Buttons.OK,
                                             MsgBox.Icon.Error,
                                             MsgBox.AnimateStyle.FadeIn);
                    }
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show($"File Not Upload {ex.Message}",
                                              "Error",
                                              MsgBox.Buttons.OK,
                                              MsgBox.Icon.Error,
                                              MsgBox.AnimateStyle.FadeIn);
            }
            //If Current Directory  Exists Delete It 
            if (Directory.Exists(StaticConstants.EMLDirectoryPath))
            {
                string[] files = Directory.GetFiles(StaticConstants.EMLDirectoryPath);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
                Directory.Delete(StaticConstants.EMLDirectoryPath);
            }
        }
        private string GetSMTPAddressForRecipients(Outlook.MailItem mail)
        {
            string RecipientEmails = "";
            const string PR_SMTP_ADDRESS =
                "http://schemas.microsoft.com/mapi/proptag/0x39FE001E";
            Outlook.Recipients recips = mail.Recipients;
            foreach (Outlook.Recipient recip in recips)
            {
                Outlook.PropertyAccessor pa = recip.PropertyAccessor;
                string smtpAddress =
                    pa.GetProperty(PR_SMTP_ADDRESS).ToString();
                RecipientEmails += $"{smtpAddress},";
            }
            return RecipientEmails.Remove(RecipientEmails.Length-1,1);
        }
        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            // Get Application
            Outlook.Application application = Globals.ThisAddIn.Application;

            // Get the current item for this Inspect to object and check if is type
            // of MailItem
            Outlook.Inspector inspector = application.ActiveInspector();
            Outlook.MailItem mailItem = inspector.CurrentItem as Outlook.MailItem;

            if (mailItem.Attachments.Count > 0)
            {
                AttachmentsForm attachmentsForm = new AttachmentsForm();
                attachmentsForm.Show();
            }
            else
            {
                MsgBox.Show(StaticConstants.AttanchmentNotFoundErrorMsg,
                                              "Message",
                                              MsgBox.Buttons.OK,
                                              MsgBox.Icon.Info,
                                              MsgBox.AnimateStyle.FadeIn);
            }
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            Change_Web_DMS_Url change_Web_DMS_Url = new Change_Web_DMS_Url();
            change_Web_DMS_Url.Show();
        }
    }
}
