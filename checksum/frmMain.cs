using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace checksum
{
    public enum HashAlgorithms
    {
        SHA1,
        SHA256,
        SHA512,
        MD5
    }

    public partial class frmMain : Form
    {
        private int lastcmbIndex = 0;
        private string lastFile1 = "";
        private string lastFile2 = "";
        private string output = "";

        private Thread thd;

        // Delegates Type
        private delegate void delEnableForm();
        private delegate void delSetText1();
        private delegate void delSetText2();

        // Delegates object
        private delEnableForm EnableFormdel;
        private delSetText1 SetText1del;
        private delSetText2 SetText2del;

        private readonly OpenFileDialog _openFileDialogue = new OpenFileDialog()
        { Title = "Select File", Filter = "All files|*.*", CheckFileExists = true, Multiselect = false };

        public frmMain()
        {
            checkRegistry();

            InitializeComponent();
            cmbMethod.Items.AddRange(new string[] { "SHA1", "SHA256", "SHA512", "MD5" });
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
                try
                {
                    int arg = int.Parse(args[0]);
                    var method = (HashAlgorithms)arg;
                    StartHashing(args[1], method, 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        public void StartHashing(string file, HashAlgorithms method, int tb)
        {
            Text = "Checksum - Calculating Hash...";
            foreach (Control c in Controls)
            {
                if (c.GetType() != typeof(LinkLabel))
                    c.Enabled = false;
            }

            thd = new Thread(() => CalculateHash(file, method, tb));
            thd.Start();
        }

        public void CalculateHash(string file, HashAlgorithms method, int tb)
        {
            if (!File.Exists(file))
                return;
            output = string.Empty;
            switch (method)
            {
                case HashAlgorithms.SHA1:
                    output = Utils.CalculateSHA1Hash(file);
                    break;
                case HashAlgorithms.SHA256:
                    output = Utils.CalculateSHA256Hash(file);
                    break;
                case HashAlgorithms.SHA512:
                    output = Utils.CalculateSHA512Hash(file);
                    break;
                case HashAlgorithms.MD5:
                    output = Utils.CalculateMD5Hash(file);
                    break;
            }
            EnableForm();
            if (tb == 1)
                SetText1();
            else
                SetText2();
        }

        private static bool _childControlsEnabled = false;
        public void EnableForm()
        {
            if (!_childControlsEnabled)
            {
                foreach (Control c in this.Controls)
                {
                    if (c.InvokeRequired)
                    {
                        c.Invoke(EnableFormdel);
                        return; // prevent Main-Thread to Title more than once
                    }
                    else
                    {
                        c.Enabled = true;
                    }
                }
            }

            // set state to enable to indicate the main thread that child-controls are already enable
            _childControlsEnabled = true;

            if (InvokeRequired)
            {
                Invoke(EnableFormdel);
            }
            else
            {
                Text = "Checksum";
                // reset state
                _childControlsEnabled = false;
            }
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
            var sInfo = new System.Diagnostics.ProcessStartInfo(e.Link.LinkData as string);
            System.Diagnostics.Process.Start(sInfo);
        }

        private void tbChecksum1_TextChanged(object sender, EventArgs e)
        {
            if (tbChecksum1.Text == tbChecksum2.Text)
            {
                if (tbChecksum1.Text != "")
                    pbCheck.Image = Properties.Resources.Check;
                else
                    pbCheck.Image = null;
            }
            else
                pbCheck.Image = Properties.Resources.Error;
        }

        private void tbChecksum2_TextChanged(object sender, EventArgs e)
        {
            if (tbChecksum1.Text == tbChecksum2.Text)
            {
                if (tbChecksum1.Text != "")
                    pbCheck.Image = Properties.Resources.Check;
                else
                    pbCheck.Image = null;
            }
            else
                pbCheck.Image = Properties.Resources.Error;
        }

        private void btnFile1_Click(object sender, EventArgs e)
        {
            if (_openFileDialogue.ShowDialog() == DialogResult.OK)
            {
                lastFile1 = _openFileDialogue.FileName;
                //tbChecksum1.Text = CalculateHash(ofd.FileName, cmbMethod.SelectedItem.ToString());
                StartHashing(_openFileDialogue.FileName, (HashAlgorithms)cmbMethod.SelectedIndex, 1);
            }
        }

        private void btnFile2_Click(object sender, EventArgs e)
        {
            if (_openFileDialogue.ShowDialog() == DialogResult.OK)
            {
                lastFile2 = _openFileDialogue.FileName;
                //tbChecksum2.Text = CalculateHash(ofd.FileName, cmbMethod.SelectedItem.ToString());
                StartHashing(_openFileDialogue.FileName, (HashAlgorithms)cmbMethod.SelectedIndex, 2);
            }
        }

        private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMethod.SelectedIndex != lastcmbIndex)
            {
                if (lastFile1 != string.Empty)
                    StartHashing(lastFile1, (HashAlgorithms)cmbMethod.SelectedIndex, 1);
                if (lastFile2 != string.Empty)
                    StartHashing(lastFile2, (HashAlgorithms)cmbMethod.SelectedIndex, 2);

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