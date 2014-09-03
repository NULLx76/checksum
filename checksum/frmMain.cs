using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
         *  SHA3
         *  SHA256
         *  SHA512
         *  AES256
        */
        
        public frmMain()
        {
            InitializeComponent();
            cmbMethod.Items.AddRange(new string[] { "MD5", "SHA1", "SHA256", "SHA512", "SHA3", "AES256" } );
            
            string[] args = Environment.GetCommandLineArgs();
            

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Test
            //test2
        }
    }
}
