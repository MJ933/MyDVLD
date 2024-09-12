using DVLD_PresentationLayer.ApplicationForms;
using DVLD_PresentationLayer.DriversForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD_PresentationLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form loginForm = new LoginForm();
            loginForm.FormClosed += LoginForm_FormClosed;
            loginForm.ShowDialog();
            //menuStrip1.ImageScalingSize = new Size(32, 32);
            this.FormClosed += Form1_FormClose;
        }

        private void LoginForm_FormClosed(Object sender, FormClosedEventArgs e)
        {
            if (!LoginForm.IsUser)
                Application.Exit();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ShowAllPeopleForm();
            frm.ShowDialog();
        }

        private void accountSToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ShowAllUsersForm();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserDetails.ID = clsGlobalSettings.User.PersonID;
            Form frm = new ShowUserDetails();
            frm.ShowDialog();
        }

        private void SignOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            RestartApplication();
        }

        public void Form1_FormClose(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void RestartApplication()
        {
            Application.Restart();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeUserPassword.ID = clsGlobalSettings.User.PersonID;
            Form frm = new ChangeUserPassword();
            frm.ShowDialog();
        }

        private void manageTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ManageTestTypes();
            frm.ShowDialog();
        }

        private void ManageApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new NewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void manageApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ManageApplicationType();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new LocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ManageDrivers();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ShowInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void nationalDrivingLiceseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new NewInternationalDrivingLicense();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new RenewLicenseApplication();
            frm.ShowDialog();
        }

        private void replacementForDamageOrLostDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ReplacementForLostLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ShowListDetainedLicenses();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new DetainedLicense();
            frm.ShowDialog();
        }

        private void reToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ReleaseDetianLicense();
            frm.ShowDialog();
        }

        private void releaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ReleaseDetianLicense();
            frm.ShowDialog();
        }

        private void retakeLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new LocalDrivingLicenseApplications();
            frm.ShowDialog();
        }
    }
}