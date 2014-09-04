using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checksum
{
    public partial class frmMain : Form
    {
        /*Notes
         * Hash methods to add:
         *  MD5
         *  SHA1
         *  SHA3 still needs to be implemented, maybe later on (:
         *  SHA256
         *  SHA512
        */
        
        public frmMain()
        {
            InitializeComponent();
            cmbMethod.Items.AddRange(new string[] { "MD5", "SHA1", "SHA256", "SHA512" } );
            cmbMethod.SelectedIndex = 0;
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://github.com/victorheld/checksum#checksum";
            alblGitHub.Links.Add(link);

            
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length != 2)
                return;
            if (cmbMethod.Items.Contains(args[0]))
                cmbMethod.SelectedIndex = cmbMethod.Items.IndexOf(args[0]);
            if (System.IO.File.Exists(args[1]))
            {

            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Test
            //test2
        }

        #region "Functions"

        public string CalculateMD5Hash(string input)
        {
            //calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            //convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public string CalculateSHA1Hash(string input)
        {
            //calculate SHA1 hash from input
            SHA1 sha1 = SHA1.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = sha1.ComputeHash(inputBytes);

            //convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public string CalculateSHA256Hash(string input)
        {
            //calculate SHA256 hash from input
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = sha256.ComputeHash(inputBytes);

            //convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public string CalculateSHA512Hash(string input)
        {
            //calculate SHA512 hash from input
            SHA512 sha512 = SHA512.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = sha512.ComputeHash(inputBytes);

            //convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        #endregion

        private void alblGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo(e.Link.LinkData as string);
            System.Diagnostics.Process.Start(sInfo);
        }
    }
}
