using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_PresentationLayer
{
    public partial class ucAddNewUserFormCtrl : UserControl
    {
        private bool NotAdded = true;
        public ucAddNewUserFormCtrl()
        {
            InitializeComponent();
        }
        private void ucAddNewUserFormCtrl_Load(object sender, EventArgs e)
        {
            cbFilters.SelectedIndex = 0;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = clsPeopleBL.GetAllData();

            DataView dataView = new DataView(dt);
            if (dataView != null && cbFilters.SelectedItem != null)
            {
                string selectedColumn = cbFilters.SelectedItem.ToString();
                string filterText = txtFilter.Text;
                try
                {
                    if (string.IsNullOrWhiteSpace(filterText))
                    {
                        dataView.RowFilter = string.Empty;
                    }
                    else
                    {
                        DataColumn column = dataView.Table.Columns[selectedColumn];
                        if (column.DataType == typeof(string))
                        {
                            dataView.RowFilter = $"[{selectedColumn}] LIKE '%{filterText}%'";
                        }
                        else if (column.DataType == typeof(int))
                        {

                            int.TryParse(filterText, out int value);
                            dataView.RowFilter = $"[{selectedColumn}]={value}";

                        }
                        else if (column.DataType == typeof(DateTime))
                        {
                            DateTime.TryParse(filterText, out DateTime value);
                            dataView.RowFilter = $"[{selectedColumn}]=#{value:yyyy/MM/dd}#";
                        }
                        else
                        {
                            dataView.RowFilter = "1=0";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"There was an error applying the filter: {ex.Message}");
                }

            }


        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilters.SelectedItem.ToString() == "Person LDLicenseID")
            {

                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = false;
            }

        }

        private clsPeopleBL GetPersonInfo()
        {
            if (!ValidateForm())
                return null;

            bool NotFound = true;
            clsPeopleBL Person1;
            if (cbFilters.SelectedIndex == 0)
                Person1 = clsPeopleBL.FindPersonByID(Convert.ToInt16(txtFilter.Text));
            else
                Person1 = clsPeopleBL.FindPersonByNationalNo(txtFilter.Text);

            if (Person1 != null)
            {
                NotFound = clsUsersBL.FindUserByPersonID(Person1.ID) == null;
                if (NotFound)
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
                }
                else
                {
                    MessageBox.Show("Selected Person Already has a user, Choose another one.");
                    ClearFields();
                    Person1 = null;
                }
            }
            else
            {
                MessageBox.Show("The PersonID or NationalNo is Wrong, Please Try Again!");
            }

            return Person1;
        }
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                MessageBox.Show("PersonID or NationalNo Is required.");
                return false;
            }
            return true;
        }
        private string GetCountryName(int ID)
        {
            clsCountriesBL Country = clsCountriesBL.FindByID(ID);
            return Country.CountryName;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            GetPersonInfo();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPersonID.Text))
                MessageBox.Show("Please choose a valid Person First");
            else
                tabControl1.SelectedIndex = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPersonID.Text))
                MessageBox.Show("Please choose a valid Person First");
            if (NotAdded)
            {
                clsUsersBL user1 = new clsUsersBL();

                user1.PersonID = Convert.ToInt16(txtPersonID.Text);
                if (!string.IsNullOrWhiteSpace(txtUserName.Text))
                    user1.UserName = txtUserName.Text;
                else
                {
                    MessageBox.Show("User Name Cannot be Empty!");
                    return;
                }
                if (txtPassword.Text == txtConfirmPassword.Text)
                    user1.Password = txtPassword.Text;
                else
                {
                    MessageBox.Show("Password Confirmation does not match Password!");
                    return;
                }
                if (chkbIsActive.Checked)
                    user1.IsActive = true;
                else user1.IsActive = false;

                if (user1.Save())
                {
                    NotAdded = false;
                    lblUserID.Text = user1.UserID.ToString();
                    MessageBox.Show("User Has Been Added Successfully.");
                }
                else MessageBox.Show("User Has Not Added Successfully.");
            }
            else MessageBox.Show("The User Is Already has been added");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
        private void ClearFields()
        {
            txtPersonID.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtNationalNo.Text = string.Empty;
            txtGender.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtDateOfBirth.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtCountry.Text = string.Empty;
            pictureBox10.ImageLocation = null; // or set to a default image path if needed
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new AddNewPersonForm();
            frm.ShowDialog();
        }

        private bool CheckPassword()
        {
            return txtPassword.Text == txtConfirmPassword.Text;
        }
        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
                erpConfirmPassword.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            else erpConfirmPassword.Clear();
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
                erpPassword.SetError(txtPassword, "Password Cannot be Blank");
            else erpPassword.Clear();
        }

        private void LlblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdatePerosn.ID = Convert.ToInt16(txtPersonID.Text);
            Form frm = new UpdatePerosn();
            frm.ShowDialog();
        }
    }
}
