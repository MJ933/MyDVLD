// Ignore Spelling: DVLD
// Ignore Spelling: ctrl

using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace DVLD_PresentationLayer
{
    public partial class ucAddNewPersonCtrl : UserControl
    {
        private string SelectedImagePath = @"D:\DVLD_Images\man.png";
        private byte SelectedGender = 0;
        private bool ImageChanged = false;
        private bool NotAdded = true;

        public ucAddNewPersonCtrl()
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

        private void ucAddNewPersonCtrl_Load(object sender, EventArgs e)
        {
            DateTime eighteenYearsAgo = DateTime.Today.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Today.AddYears(-100);
            dateTimePicker1.MaxDate = eighteenYearsAgo;
            GetCountriesNames();
        }

        private int GetCountryID()
        {
            if (comboBox1.SelectedIndex == -1)
                comboBox1.SelectedIndex = 3;

            string selectedCountry = comboBox1.SelectedItem.ToString();
            clsCountriesBL Country1 = clsCountriesBL.FindByName(selectedCountry);
            return Country1.CountryID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;
            if (NotAdded)
            {
                clsPeopleBL person1 = new clsPeopleBL();

                person1.FirstName = txtFirstName.Text;
                person1.SecondName = txtSecondName.Text;
                person1.ThirdName = txtThirdName.Text;
                person1.LastName = txtLastName.Text;
                person1.NationalNo = txtNationalNo.Text;
                person1.Gender = SelectedGender;
                person1.Email = txtEmail.Text;
                person1.Address = rtxtAddress.Text;
                person1.DateOfBirth = dateTimePicker1.Value;
                person1.Phone = txtPhone.Text;
                person1.NationalityCountryID = GetCountryID();
                if (ImageChanged)
                    ChangeImageForFandM();

                person1.ImagePath = SelectedImagePath;

                if (person1.Save())
                {
                    txtPersonID.Text = person1.ID.ToString();
                    NotAdded = false;
                    MessageBox.Show("Person has been saved successfully with LDLicenseID=" + person1.ID);
                }
                else
                {
                    MessageBox.Show("Person has not been saved. LDLicenseID=" + person1.ID);
                }
            }
            else MessageBox.Show("The Person Is Already has been added");
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
            ImageChanged = true;
        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox10.ImageLocation = @"D:\DVLD_Images\woman.png";
            SelectedGender = 1;
            ImageChanged = true;
        }

        private void txtNationalNo_TextChanged(object sender, EventArgs e)
        {
            if (clsPeopleBL.checkPersonNationalNo(txtNationalNo.Text))
            {
                erpNationalNo.SetError(txtNationalNo, "Please enter another National Number.");
            }
            else
            {
                erpNationalNo.Clear();
            }
        }

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
                else
                {
                    errorProvider1.Clear();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}