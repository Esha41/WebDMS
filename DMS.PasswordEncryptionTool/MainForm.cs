using Intelli.DMS.Api.Helpers;
using System;
using System.Windows.Forms;

namespace DMS.PasswordEncryptionTool
{
    /// <summary>
    /// The MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button Encrypt Event
        /// </summary>       
        private void Btn_encrypt_Click(object sender, EventArgs e)
        {
            txtEncryptedText.Text = EncryptString(txtPlainText.Text);
        }

        /// <summary>
        /// Button Decrypt Event
        /// </summary> 
        private void Btn_decrypt_Click(object sender, EventArgs e)
        {
            txtPlainText.Text = DecryptString(txtEncryptedText.Text);
        }

        /// <summary>
        /// Encrypt the PlainText
        /// </summary>
        /// <param name="plainText">The PlainText.</param>
        /// <returns>Encrypted String.</returns>
        public string EncryptString(string plainText)
        {
            try
            {
                return EncryptionHelper.EncryptString(plainText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Decrypt the CipherText
        /// </summary>
        /// <param name="cipherText">The CipherText.</param>
        /// <returns>Decrypted String.</returns>
        public string DecryptString(string cipherText)
        {
            try
            {
                return EncryptionHelper.DecryptString(cipherText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }
    }
}
