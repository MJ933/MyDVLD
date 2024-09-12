using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer
{
    public partial class ShowUserDetails : Form
    {
        public static int ID { get; set; }
        public ShowUserDetails()
        {
            InitializeComponent();
            ucShowUserDetailsCtrl.ID = ID;
        }

        private void ShowUserDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
