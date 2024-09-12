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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_PresentationLayer
{
    public partial class ucShowPersonDetailsCtrl : UserControl
    {
        public static int ID { get; set; }
        public ucShowPersonDetailsCtrl()
        {
            InitializeComponent();
        }

        private clsPeopleBL GetPersonInfo(int ID)
        {
            clsPeopleBL Person1 = clsPeopleBL.FindPersonByID(ID);


            if (Person1 != null)
            {
                txtPersonID.Text = Person1.ID.ToString();
                txtFullName.Text = " " + Person1.FirstName;
                txtFullName.Text += " " + Person1.SecondName;
                txtFullName.Text += " " + Person1.ThirdName;
                txtFullName.Text += " " + Person1.LastName;
                txtNationalNo.Text = Person1.NationalNo;
                if (Person1.Gender == 0)
                {
                    txtGender.Text = "Male";
                }
                else
                {
                    txtGender.Text = "Female";
                }
                txtEmail.Text = Person1.Email;
                txtAddress.Text = Person1.Address;
                txtDateOfBirth.Text = Person1.DateOfBirth.ToString();

                txtPhone.Text = Person1.Phone;
                txtCountry.Text = GetCountryName(Person1.NationalityCountryID);
                pictureBox10.ImageLocation = Person1.ImagePath;
            }
            return Person1;
        }
        private string GetCountryName(int ID)
        {
            clsCountriesBL Country = clsCountriesBL.FindByID(ID);
            return Country.CountryName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void ucShowPersonDetailsCtrl_Load(object sender, EventArgs e)
        {
            GetPersonInfo(ID);
        }

        private void LlblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdatePerosn.ID = Convert.ToInt16(txtPersonID.Text);
            Form frm = new UpdatePerosn();
            frm.ShowDialog();
        }
    }
}
