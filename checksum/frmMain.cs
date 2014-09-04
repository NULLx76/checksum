using System;
using System.Security.Cryptography;
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

        private int lastcmbIndex = 0;
        private string lastFileLocation = "C:\\";
        private string lastFile1 = "";
        private string lastFile2 = "";

        public frmMain()
        {
            InitializeComponent();
            cmbMethod.Items.AddRange(new string[] { "MD5", "SHA1", "SHA256", "SHA512" });
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
            //test
            //test2
        }

        #region "Functions"

        public string CalculateMD5Hash(string file)
        {
            MD5 md5 = MD5.Create();

            using (var stream = System.IO.File.OpenRead(file))
            {
                byte[] b = md5.ComputeHash(stream);
                stream.Close();
                return BitConverter.ToString(b).Replace("-", "").ToLower();
            }
        }

        public string CalculateSHA1Hash(string file)
        {
            SHA1 sha1 = SHA1.Create();

            using (var stream = System.IO.File.OpenRead(file))
            {
                byte[] b = sha1.ComputeHash(stream);
                stream.Close();
                return BitConverter.ToString(b).Replace("-", "").ToLower();
            }
        }

        public string CalculateSHA256Hash(string file)
        {
            SHA256 sha256 = SHA256.Create();

            using (var stream = System.IO.File.OpenRead(file))
            {
                byte[] b = sha256.ComputeHash(stream);
                stream.Close();
                return BitConverter.ToString(b).Replace("-", "").ToLower();
            }
        }

        public string CalculateSHA512Hash(string file)
        {
            SHA512 sha512 = SHA512.Create();

            using (var stream = System.IO.File.OpenRead(file))
            {
                byte[] b = sha512.ComputeHash(stream);
                stream.Close();
                return BitConverter.ToString(b).Replace("-", "").ToLower();
            }
        }

        public string CalculateHash(string file, string method)
        {
            if (!System.IO.File.Exists(file))
                return "";

            string output = "";

            switch (method)
            {
                case "SHA1":
                    output = CalculateSHA1Hash(file);
                    break;

                case "SHA256":
                    output = CalculateSHA256Hash(file);
                    break;

                case "SHA512":
                    output = CalculateSHA512Hash(file);
                    break;

                default:
                    output = CalculateMD5Hash(file);
                    break;
            }
            lastFileLocation = System.IO.Path.GetFullPath(file);
            return output;
        }

        #endregion "Functions"

        #region "Old Functions"

        /*
        public string CalculateMD5Hash(byte[] input)
        {
            //calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(input);

            //convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public string CalculateSHA1Hash(byte[] input)
        {
            //calculate SHA1 hash from input
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(input);

            //convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public string CalculateSHA256Hash(byte[] input)
        {
            //calculate SHA256 hash from input
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(input);

            //convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public string CalculateSHA512Hash(byte[] input)
        {
            //calculate SHA512 hash from input
            SHA512 sha512 = SHA512.Create();
            byte[] hash = sha512.ComputeHash(input);

            //convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
        */

        #endregion "Old Functions"

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

        #endregion "Events"
    }
}