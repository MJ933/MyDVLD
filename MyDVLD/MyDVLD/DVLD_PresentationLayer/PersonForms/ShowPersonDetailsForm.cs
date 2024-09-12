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
    public partial class ShowPersonDetailsForm : Form
    {
        public static int ID { get; set; }

        public ShowPersonDetailsForm()
        {
            InitializeComponent();
            ucShowPersonDetailsCtrl.ID = ID;
        }
    }
}
