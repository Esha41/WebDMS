using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using Microsoft.Win32;

namespace WebDMS_OutlookAddIn
{
    public static class StaticConstants
    {
        //static string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];

        static string BaseUrl 
        {
            get
            {
                const string userRoot = "HKEY_CURRENT_USER";
                const string subkey = "Web_DMS_Base_URL";
                const string keyName = userRoot + "\\" + subkey;
                string defaultUrl = "http://20.82.3.149:56981";
                string baseurlNull = "";
                string baseUrl =Registry.GetValue(keyName,"", baseurlNull)?.ToString();
                if(string.IsNullOrEmpty(baseUrl))
                {
                    Registry.SetValue(keyName, "", defaultUrl);
                    baseUrl = Registry.GetValue(keyName, "", null).ToString();
                    return baseUrl;
                }
                return baseUrl;
            }
        }
        public static string Email { get; set; }
        public static string FileUploadUrl => $"{BaseUrl}/OutLookAddIn/UploadTempDocument";
        public static List<string> selectedAttanchmentNames { get; set; }
        public static string AttanchmentLabelText => "Please Select Attachments Shown below";
        public static string AttanchmentButtonText => "Send To Web DMS";
        public static string AttanchmentNotFoundErrorMsg => "No Attachment Found In Email";
        public static string AttanchmentsErrorMsg => "Please Select Attachment To Send To Web DMS";
        public static string FileUploadErrorMsg => "File Not Upload To WebDMS";
        public static string FileUploadSuccessMsg => "File Upload To WebDMS";
        public static string DirectoryPath => $"{Path.GetPathRoot(Environment.SystemDirectory)}DMSTempFileSave/";
        public static string EMLDirectoryPath => $"{Path.GetPathRoot(Environment.SystemDirectory)}WebDMS Email Body As EML/";
        public static string SelectAll => "Select All";
       
    }
}
