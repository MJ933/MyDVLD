using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer
{
    public partial class ucLoginFormCtrl : UserControl
    {
        private const string CredentialsFilePath = "user_credentials.txt";
        public static bool IsUser = false;
        public event EventHandler<EventArgs> LoginSuccessful;
        public bool IsRememberMe = false;

        public ucLoginFormCtrl()
        {
            InitializeComponent();
            LoadCredentials();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUsersBL user1 = clsUsersBL.FindUserByUserName(txtUserName.Text);

            if (user1 == null)
            {
                MessageBox.Show("The UserName or Password Is Wrong!");
                return;
            }

            if (txtUserName.Text == user1.UserName && txtPassword.Text == user1.Password)
            {
                if (user1.IsActive)
                {
                    IsUser = true;
                    clsGlobalSettings.User = user1;

                    OnLoginSuccessful();
                    SaveCredentials();
                    return;
                }
                else
                {
                    MessageBox.Show("The User is Not Active!");
                }
            }
            else
            {
                MessageBox.Show("The UserName or Password Is Wrong!");
            }
        }

        private void OnLoginSuccessful()
        {
            LoginSuccessful?.Invoke(this, EventArgs.Empty);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            IsRememberMe = checkBox1.Checked;
            if (!IsRememberMe)
            {
                File.Delete(CredentialsFilePath);
            }
        }

        private void SaveCredentials()
        {
            if (IsRememberMe)
            {
                string credentials = $"{txtUserName.Text},{txtPassword.Text},{IsRememberMe}";
                File.WriteAllText(CredentialsFilePath, credentials);
            }
        }

        private void LoadCredentials()
        {
            if (File.Exists(CredentialsFilePath))
            {
                string[] credentials = File.ReadAllText(CredentialsFilePath).Split(',');
                if (credentials.Length == 3 && bool.Parse(credentials[2]))
                {
                    txtUserName.Text = credentials[0];
                    txtPassword.Text = credentials[1];
                    checkBox1.Checked = true;
                    IsRememberMe = true;
                }
            }
        }
    }
}