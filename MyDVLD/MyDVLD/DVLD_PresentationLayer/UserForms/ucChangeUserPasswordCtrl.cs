using DVLD_BusinessLayer;
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
    public partial class ucChangeUserPasswordCtrl : UserControl
    {
        public static int ID { get; set; }
        clsUsersBL user0;

        private void ucChangeUserPasswordCtrl_Load(object sender, EventArgs e)
        {
            if (ID != 0)
                GetPersonInfo(ID);
        }
        public ucChangeUserPasswordCtrl()
        {
            InitializeComponent();
        }
        private clsPeopleBL GetPersonInfo(int id)
        {
            clsPeopleBL Person1;
            clsUsersBL user1;
            Person1 = clsPeopleBL.FindPersonByID(id);
            user1 = clsUsersBL.FindUserByPersonID(Person1.ID);
            if (Person1 != null)
            {
                txtPersonID.Text = Person1.ID.ToString();
                txtFullName.Text = " " + Person1.FirstName;
                txtFullName.Text += " " + Person1.SecondName;
                txtFullName.Text += " " + Person1.ThirdName;
                txtFullName.Text += " " + Person1.LastName;
                txtNationalNo.Text = Person1.NationalNo;
                txtGender.Text = Person1.Gender == 0 ? "Male" : "Female";
                txtEmail.Text = Person1.Email;
                txtAddress.Text = Person1.Address;
                txtDateOfBirth.Text = Person1.DateOfBirth.ToString();
                txtPhone.Text = Person1.Phone;
                txtCountry.Text = GetCountryName(Person1.NationalityCountryID);
                pictureBox10.ImageLocation = Person1.ImagePath;
                lblUserID.Text = user1.UserID.ToString();
                lblUserName.Text = user1.UserName;
                if (user1.IsActive)
                    lblIsActive.Text = "Yes";
                else lblIsActive.Text = "No";
                user0 = user1;
            }
            else
            {
                MessageBox.Show($"There is not Person with LDLicenseID = {ID}");
            }

            return Person1;
        }
        private string GetCountryName(int ID)
        {
            clsCountriesBL Country = clsCountriesBL.FindByID(ID);
            return Country.CountryName;
        }
        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            if (user0.Password != txtCurrentPassword.Text)
                erpCurrentPassword.SetError(txtCurrentPassword, "Current Password Is Wrong");
            else erpCurrentPassword.Clear();
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text))
                erpNewPassword.SetError(txtNewPassword, "The Password cannot be Blank");
            else erpNewPassword.Clear();
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text != txtNewPassword.Text)
                erpNewPassword.SetError(txtConfirmPassword, "This Not Match The Password");
            else erpNewPassword.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsUsersBL user1 = clsUsersBL.FindUserByPersonID(user0.PersonID);
            if (user1 != null)
            {
                user1.Password = txtNewPassword.Text;
                if (user1.Save())
                    MessageBox.Show($"There user with id {user1.UserID}  has Update his Password");
            }
            else
            {
                MessageBox.Show($"There is no user with id {user1.UserID}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}
