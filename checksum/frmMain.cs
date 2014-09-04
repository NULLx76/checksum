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

        int lastcmbIndex = 0;
        string lastFileLocation = "C:\\";
        string lastFile1 = "";
        string lastFile2 = "";
        
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
            if (cmbMethod.Items.Contains(args[0].ToUpper()))
                cmbMethod.SelectedIndex = cmbMethod.Items.IndexOf(args[0]);
            if (System.IO.File.Exists(args[1]))
            {
                tbChecksum1.Text = CalculateHash(args[1], args[0]);
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

        public string CalculateHash(string file, string method)
        {
            string input = System.Text.Encoding.Default.GetString(System.IO.File.ReadAllBytes(file));
            string output = "";

            switch (method)
            {
                case "SHA1":
                    output = CalculateSHA1Hash(input);
                    break;
                case "SHA256":
                    output = CalculateSHA256Hash(input);
                    break;
                case "SHA512":
                    output = CalculateSHA512Hash(input);
                    break;
                default:
                    output = CalculateMD5Hash(input);
                    break;
            }
            lastFileLocation = System.IO.Path.GetFullPath(file);
            return output;
        }

        #endregion

        #region "Events"
        private void alblGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo(e.Link.LinkData as string);
            System.Diagnostics.Process.Start(sInfo);
        }

        private void tbChecksum1_TextChanged(object sender, EventArgs e)
        {
            if (tbChecksum1.Text == tbChecksum2.Text)
                pbCheck.Image = checksum.Properties.Resources.Check;
            else
                pbCheck.Image = checksum.Properties.Resources.Error;
        }

        private void tbChecksum2_TextChanged(object sender, EventArgs e)
        {
            if (tbChecksum1.Text == tbChecksum2.Text)
                pbCheck.Image = checksum.Properties.Resources.Check;
            else
                pbCheck.Image = checksum.Properties.Resources.Error;
        }

        private void btnFile1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select File";
            ofd.Filter = "All Files|*.*";
            ofd.CheckFileExists = true;
            ofd.Multiselect = false;
            ofd.InitialDirectory = lastFileLocation;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lastFile1 = ofd.FileName;
                tbChecksum1.Text = CalculateHash(ofd.FileName, cmbMethod.SelectedItem.ToString());
            }
        }

        private void btnFile2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select File";
            ofd.Filter = "All Files|*.*";
            ofd.CheckFileExists = true;
            ofd.Multiselect = false;
            ofd.InitialDirectory = lastFileLocation;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lastFile2 = ofd.FileName;
                tbChecksum2.Text = CalculateHash(ofd.FileName, cmbMethod.SelectedItem.ToString());
            }
            ofd.Dispose();
        }

        private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMethod.SelectedIndex != lastcmbIndex)
            {
                tbChecksum1.Text = CalculateHash(lastFile1, cmbMethod.SelectedItem.ToString());
                tbChecksum2.Text = CalculateHash(lastFile2, cmbMethod.SelectedItem.ToString());
                lastcmbIndex = cmbMethod.SelectedIndex;
            }
        }
        #endregion
    }
}
