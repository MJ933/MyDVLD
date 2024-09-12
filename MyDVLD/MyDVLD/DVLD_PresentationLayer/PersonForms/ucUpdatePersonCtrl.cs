using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DVLD_PresentationLayer
{
    public partial class ucUpdatePersonCtrl : UserControl
    {
        public static int ID { get; set; }
        clsPeopleBL Person0;
        private string SelectedImagePath = "";
        private byte SelectedGender = 0;
        private bool GenderChanged = false;
        private bool IsLoadingData = false;
        public ucUpdatePersonCtrl()
        {
            InitializeComponent();
        }

        private void GetCountriesNames()
        {
            DataTable dataTable = clsCountriesBL.GetCountriesList();
            foreach (DataRow row in dataTable.Rows)
            {
                comboBox1.Items.Add(row["CountryName"].ToString());
            }
        }
        private bool isValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private int GetCountryID()
        {
            if (comboBox1.SelectedIndex == -1)
                comboBox1.SelectedIndex = 3;

            string selectedCountry = comboBox1.SelectedItem.ToString();
            clsCountriesBL Country1 = clsCountriesBL.FindByName(selectedCountry);
            return Country1.CountryID;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("First name and last name are required.");
                return false;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a nationality.");
                return false;
            }

            return true;
        }

        private void ucUpdatePersonCtrl_Load(object sender, EventArgs e)
        {
            DateTime eighteenYearsAgo = DateTime.Today.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Today.AddYears(-100);
            dateTimePicker1.MaxDate = eighteenYearsAgo;
            GetCountriesNames();
            IsLoadingData = true;
            Person0 = GetPersonInfo(ID);
            IsLoadingData = false;
        }

        private clsPeopleBL GetPersonInfo(int ID)
        {
            clsPeopleBL Person1 = clsPeopleBL.FindPersonByID(ID);


            if (Person1 != null)
            {
                txtPersonID.Text = Person1.ID.ToString();
                txtFirstName.Text = Person1.FirstName;
                txtSecondName.Text = Person1.SecondName;
                txtThirdName.Text = Person1.ThirdName;
                txtLastName.Text = Person1.LastName;
                txtNationalNo.Text = Person1.NationalNo;
                if (Person1.Gender == 0)
                {
                    rbtnMale.Checked = true;
                }
                else
                {
                    rbtnFemale.Checked = true;
                }
                IsLoadingData = false;
                txtEmail.Text = Person1.Email;
                rtxtAddress.Text = Person1.Address; DateTime dob = Person1.DateOfBirth;
                if (dob >= dateTimePicker1.MinDate && dob <= dateTimePicker1.MaxDate)
                {
                    dateTimePicker1.Value = dob;
                }
                else
                {
                    MessageBox.Show("Invalid Date OF Birth");
                }
                txtPhone.Text = Person1.Phone;
                comboBox1.SelectedIndex = Person1.NationalityCountryID;
                SelectedImagePath = Person1.ImagePath;
                pictureBox10.ImageLocation = SelectedImagePath;

            }
            return Person1;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            //if (!ValidateForm())
            //    return;

            clsPeopleBL Person1 = Person0;


            Person1.FirstName = txtFirstName.Text;
            Person1.SecondName = txtSecondName.Text;
            Person1.ThirdName = txtThirdName.Text;
            Person1.LastName = txtLastName.Text;
            Person1.NationalNo = txtNationalNo.Text;
            Person1.Gender = SelectedGender;
            Person1.Email = txtEmail.Text;
            Person1.Address = rtxtAddress.Text;
            Person1.DateOfBirth = dateTimePicker1.Value;
            Person1.Phone = txtPhone.Text;
            Person1.NationalityCountryID = GetCountryID();
            if (GenderChanged)
            {
                ChangeImageForFandM();
                GenderChanged = false;
            }

            Person1.ImagePath = SelectedImagePath;

            if (Person1.Save())
            {
                txtPersonID.Text = Person1.ID.ToString();

                MessageBox.Show("Person has been saved successfully with LDLicenseID=" + Person1.ID);

            }
            else
            {
                MessageBox.Show("Person has not been saved. LDLicenseID=" + Person1.ID);
            }
        }

        private void LlblChangeImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string OldIamge = SelectedImagePath;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.bmp)|*.png;*.bmp",
                Title = "Select an Image"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox10.Image = Image.FromFile(openFileDialog.FileName);
                    if (!string.IsNullOrEmpty(SelectedImagePath) && File.Exists(SelectedImagePath) &&
                 !SelectedImagePath.Equals(@"D:\DVLD_Images\man.png") && !SelectedImagePath.Equals(@"D:\DVLD_Images\woman.png"))
                    {
                        File.Delete(SelectedImagePath);
                    }
                    Guid guid = Guid.NewGuid();
                    string fileName = guid.ToString() + Path.GetExtension(openFileDialog.FileName);
                    string folderPath = @"D:\C19\DVLD_People-Images\";
                    string filePath = Path.Combine(folderPath, fileName);

                    File.Copy(openFileDialog.FileName, filePath, true);
                    SelectedImagePath = filePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
        }
        private void ChangeImageForFandM()
        {
            string imagePath;
            if (SelectedGender == 0)
                imagePath = @"D:\DVLD_Images\man.png";
            else
                imagePath = @"D:\DVLD_Images\woman.png";

            pictureBox10.Image = Image.FromFile(imagePath);

            if (!string.IsNullOrEmpty(SelectedImagePath) && File.Exists(SelectedImagePath) &&
                !SelectedImagePath.Equals(@"D:\DVLD_Images\man.png") && !SelectedImagePath.Equals(@"D:\DVLD_Images\woman.png"))
            {
                File.Delete(SelectedImagePath);
            }

            Guid guid = Guid.NewGuid();
            string fileName = guid.ToString() + Path.GetExtension(imagePath);
            string folderPath = @"D:\C19\DVLD_People-Images\";
            string filePath = Path.Combine(folderPath, fileName);
            File.Copy(imagePath, filePath, true);

            SelectedImagePath = filePath;
        }

        private void rbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox10.ImageLocation = @"D:\DVLD_Images\man.png";
            SelectedGender = 0;
            if (!IsLoadingData)
            {
                GenderChanged = true;
            }
        }
        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox10.ImageLocation = @"D:\DVLD_Images\woman.png";
            SelectedGender = 1;
            if (!IsLoadingData)
            {
                GenderChanged = true;
            }
        }

        //private void txtNationalNo_TextChanged(object sender, EventArgs e)
        //{
        //    if (clsPeopleBL.checkPersonNationalNo(txtNationalNo.Text))
        //    {
        //        erpNationalNo.SetError(txtNationalNo, "Please enter another National Number.");
        //    }
        //    else
        //    {
        //        erpNationalNo.Clear();
        //    }
        //}

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (isValidEmail(txtEmail.Text))
            {
                errorProvider1.Clear();

            }
            else
            {
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    errorProvider1.SetError(txtEmail, "Please enter a Valid Email!");

                }
                else { errorProvider1.Clear(); }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}
