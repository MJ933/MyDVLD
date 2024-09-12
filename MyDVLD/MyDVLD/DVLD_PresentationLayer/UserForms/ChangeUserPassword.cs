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
    public partial class ChangeUserPassword : Form
    {
        public static int ID { get; set; }

        public ChangeUserPassword()
        {
            InitializeComponent();
            ucChangeUserPasswordCtrl.ID = ID;
        }
    }
}
