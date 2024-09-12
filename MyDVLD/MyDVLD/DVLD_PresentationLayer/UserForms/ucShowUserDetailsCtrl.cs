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
    public partial class ucShowUserDetailsCtrl : UserControl
    {
        public static int ID { get; set; }
        public ucShowUserDetailsCtrl()
        {
            InitializeComponent();
        }

        private void ucShowUserDetailsCtrl_Load(object sender, EventArgs e)
        {
            if (ID != 0)
                GetPersonInfo(ID);
            //else MessageBox.Show("The LDLicenseID is not set!");
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
    }
}
