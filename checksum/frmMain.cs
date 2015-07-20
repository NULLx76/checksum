using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace checksum
{
    public partial class frmMain : Form
    {
        private int lastcmbIndex = 0;
        private string lastFileLocation = "C:\\";
        private string lastFile1 = "";
        private string lastFile2 = "";
        private string output = "";

        private Thread thd;

        private delegate void delEnableForm();

        private delEnableForm EnableFormdel;

        private delegate void delSetText1();

        private delSetText1 SetText1del;

        private delegate void delSetText2();

        private delSetText2 SetText2del;

        public frmMain()
        {
            checkRegistry();

            InitializeComponent();
            cmbMethod.Items.AddRange(new string[] { "MD5", "SHA1", "SHA256", "SHA512" });
            cmbMethod.SelectedIndex = 0;
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://github.com/victorheld/checksum#checksum";
            alblGitHub.Links.Add(link);

            EnableFormdel = new delEnableForm(EnableForm);
            SetText1del = new delSetText1(SetText1);
            SetText2del = new delSetText2(SetText2);

            //Drag&Drop
            //this.AllowDrop = true;
            //this.DragEnter += new DragEventHandler(frmMain_DragEnter);
            //this.DragDrop += new DragEventHandler(frmMain_DragDrop);

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length != 2)
                return;
            if (cmbMethod.Items.Contains(args[0].ToUpper()))
                cmbMethod.SelectedIndex = cmbMethod.Items.IndexOf(args[0]);
            if (System.IO.File.Exists(args[1]))
            {
                //tbChecksum1.Text = CalculateHash(args[1], args[0]);
                StartHashing(args[1], args[0], 1);
            }
        }

        //Not in region events because well, easy edits & stuff :D ? (TESTING YO)
        private void frmMain_Load(object sender, EventArgs e)
        {
            //test1
            //test2
            //test3
        }

        #region "Functions"

        public void checkRegistry()
        {
            string regkey = (string)Registry.GetValue("HKEY_CLASSES_ROOT\\*\\shell\\Checksum\\command", null, null);
            string regIcon = (string)Registry.GetValue("HKEY_CLASSES_ROOT\\*\\shell\\Checksum", "Icon", null);
            if (regkey == null || !regkey.Contains(Application.ExecutablePath))
                Registry.SetValue("HKEY_CLASSES_ROOT\\*\\shell\\Checksum\\command", null, Application.ExecutablePath + " %1");
            if (regIcon == null || regIcon != Application.ExecutablePath)
                Registry.SetValue("HKEY_CLASSES_ROOT\\*\\shell\\Checksum", "Icon", Application.ExecutablePath);
        }

        public string CalculateMD5Hash(string file)
        {
            MD5 md5 = MD5.Create();

            using (var stream = System.IO.File.OpenRead(file))
            {
                byte[] b = md5.ComputeHash(stream);
                stream.Close();
                return BitConverter.ToString(b).Replace("-", "").ToLowerInvariant();
            }
        }

        public string CalculateSHA1Hash(string file)
        {
            SHA1 sha1 = SHA1.Create();

            using (var stream = System.IO.File.OpenRead(file))
            {
                byte[] b = sha1.ComputeHash(stream);
                stream.Close();
                return BitConverter.ToString(b).Replace("-", "").ToLowerInvariant();
            }
        }

        public string CalculateSHA256Hash(string file)
        {
            SHA256 sha256 = SHA256.Create();

            using (var stream = System.IO.File.OpenRead(file))
            {
                byte[] b = sha256.ComputeHash(stream);
                stream.Close();
                return BitConverter.ToString(b).Replace("-", "").ToLowerInvariant();
            }
        }

        public string CalculateSHA512Hash(string file)
        {
            SHA512 sha512 = SHA512.Create();

            using (var stream = System.IO.File.OpenRead(file))
            {
                byte[] b = sha512.ComputeHash(stream);
                stream.Close();
                return BitConverter.ToString(b).Replace("-", "").ToLowerInvariant();
            }
        }

        public void StartHashing(string file, string method, int tb)
        {
            this.Text = "Checksum - Calculating Hash...";
            foreach (Control c in this.Controls)
            {
                if (c.GetType() != typeof(LinkLabel))
                    c.Enabled = false;
            }

            thd = new Thread(() => CalculateHash(file, method, tb));
            thd.Start();
        }

        public void CalculateHash(string file, string method, int tb)
        {
            output = "";
            if (!System.IO.File.Exists(file))
                return;

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
            EnableForm();
            if (tb == 1)
                SetText1();
            else
                SetText2();
            lastFileLocation = System.IO.Path.GetFullPath(file);
        }

        public void EnableForm()
        {
            foreach (Control c in this.Controls)
            {
                if (c.InvokeRequired)
                    c.Invoke(EnableFormdel);
                else
                    c.Enabled = true;
            }

            if (this.InvokeRequired)
                this.Invoke(EnableFormdel);
            else
                this.Text = "Checksum";
        }

        public void SetText1()
        {
            if (tbChecksum1.InvokeRequired)
            {
                tbChecksum1.Invoke(SetText1del);
            }
            else
            {
                tbChecksum1.Text = output;
            }
        }

        public void SetText2()
        {
            if (tbChecksum2.InvokeRequired)
            {
                tbChecksum2.Invoke(SetText2del);
            }
            else
            {
                tbChecksum2.Text = output;
            }
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
            {
                if (tbChecksum1.Text != "")
                    pbCheck.Image = checksum.Properties.Resources.Check;
                else
                    pbCheck.Image = null;
            }
            else
                pbCheck.Image = checksum.Properties.Resources.Error;
        }

        private void tbChecksum2_TextChanged(object sender, EventArgs e)
        {
            if (tbChecksum1.Text == tbChecksum2.Text)
            {
                if (tbChecksum1.Text != "")
                    pbCheck.Image = checksum.Properties.Resources.Check;
                else
                    pbCheck.Image = null;
            }
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
                //tbChecksum1.Text = CalculateHash(ofd.FileName, cmbMethod.SelectedItem.ToString());
                StartHashing(ofd.FileName, cmbMethod.SelectedItem.ToString(), 1);
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
                //tbChecksum2.Text = CalculateHash(ofd.FileName, cmbMethod.SelectedItem.ToString());
                StartHashing(ofd.FileName, cmbMethod.SelectedItem.ToString(), 2);
            }
            ofd.Dispose();
        }

        private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMethod.SelectedIndex != lastcmbIndex)
            {
                if (lastFile1 != "")
                    StartHashing(lastFile1, cmbMethod.SelectedItem.ToString(), 1);
                if (lastFile2 != "")
                    StartHashing(lastFile2, cmbMethod.SelectedItem.ToString(), 2);

                lastcmbIndex = cmbMethod.SelectedIndex;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thd != null)
                thd.Abort();
        }

        //private void frmMain_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        //}

        //private void frmMain_DragDrop(object sender, DragEventArgs e)
        //{
        //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        //    lastFile1 = files[0];
        //    StartHashing(files[0], cmbMethod.SelectedItem.ToString(), 1);
        //}

        #endregion "Events"
    }
}