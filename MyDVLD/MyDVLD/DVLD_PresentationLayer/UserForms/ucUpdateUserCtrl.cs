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
    public partial class ucUpdateUserCtrl : UserControl
    {
        public ucUpdateUserCtrl()
        {
            InitializeComponent();
        }
        private bool NotAdded = true;
        private clsUsersBL user0;
        public static int ID { get; set; }

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
        private void ucUpdateUserCtrl_Load(object sender, EventArgs e)
        {
            GetPersonInfo(ID);
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
            if (cbFilters.SelectedItem.ToString() == "Person id")
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
        private void btnFind_Click(object sender, EventArgs e)
        {
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPersonID.Text))
                MessageBox.Show("Please choose a valid Person First");
            else
                tabControl1.SelectedIndex = 1;
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
                txtUserName.Text = user1.UserName;
                txtPassword.Text = user1.Password;
                txtConfirmPassword.Text = user1.Password;
                chkbIsActive.Checked = user1.IsActive;
                user0 = user1;
            }
            else
            {
                MessageBox.Show($"There is not Person with LDLicenseID = {ID}");
            }

            return Person1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPersonID.Text))
                MessageBox.Show("Please choose a valid Person First");
            if (NotAdded)
            {
                int ID = Convert.ToInt16(txtPersonID.Text);
                clsUsersBL user1 = user0;

                user1.UserName = txtUserName.Text;
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
                    MessageBox.Show("User Has Been Updated Successfully.");
                }
                else MessageBox.Show("User Has Not Updated Successfully.");
            }
            else MessageBox.Show("The User Is Already has been Updated");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new AddNewPersonForm();
            frm.ShowDialog();
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
