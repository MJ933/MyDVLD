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
    public partial class LoginForm : Form
    {
        public static bool IsUser;

        public LoginForm()
        {
            InitializeComponent();

            ucLoginFormCtrl1.LoginSuccessful += LoginControl_LoginSuccessful;
        }
        private void LoginControl_LoginSuccessful(object sender, EventArgs e)
        {
            IsUser = ucLoginFormCtrl.IsUser;
            this.Close(); // Close the form when login is successful
        }


    }
}
