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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_PresentationLayer.ApplicationForms
{
    public partial class ucNewLocalDrivingLicenseApplicationCtrl : UserControl
    {
        private clsLicenseClassesBL Lclass0;
        public ucNewLocalDrivingLicenseApplicationCtrl()
        {
            InitializeComponent();
        }
        private bool NotAdded = true;
        private clsApplicationsBL Application0;

        private string GetCountryName(int ID)
        {
            clsCountriesBL Country = clsCountriesBL.FindByID(ID);
            return Country.CountryName;
        }

        private decimal GetClassFees()
        {
            string ClassName = null;
            if (cbLicenseClass.SelectedItem != null)
                ClassName = cbLicenseClass.SelectedItem.ToString();

            if (ClassName != null)
            {
                Lclass0 = clsLicenseClassesBL.FinLicenseClassIDByClassName(ClassName);
                if (Lclass0 != null)
                    return Lclass0.ClassFees;
                else
                    return 0;
            }
            else MessageBox.Show("The Class Name is Wrong!");
            return 0;
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

        private void ucNewLocalDrivingLicenseApplicationCtrl_Load(object sender, EventArgs e)
        {
            cbFilters.SelectedIndex = 1;
            DataTable dt = clsLicenseClassesBL.GetAllData();
            DataView dataView1 = new DataView(dt);

            // Add only the "ClassName" column to the cbLicenseClass ComboBox
            if (dt.Columns.Contains("ClassName"))
            {
                foreach (DataRow row in dt.Rows)
                {
                    cbLicenseClass.Items.Add(row["ClassName"].ToString());
                }
            }
            else
            {
                MessageBox.Show("The 'ClassName' column does not exist in the DataTable.");
            }

            if (cbLicenseClass.Items.Count > 0)
            {
                cbLicenseClass.SelectedIndex = 0;
            }
        }

        private clsPeopleBL GetPersonInfo()
        {
            if (!ValidateForm())
                return null;

            clsPeopleBL Person1;
            if (cbFilters.SelectedIndex == 0)
                Person1 = clsPeopleBL.FindPersonByID(Convert.ToInt16(txtFilter.Text));
            else
                Person1 = clsPeopleBL.FindPersonByNationalNo(txtFilter.Text);

            if (Person1 != null)
            {
                //if (clsDrivingLicenseServicesBL.FindLDLAppByPersonID(Person1.LDLicenseID) == null)
                //{

                txtPersonID.Text = Person1.ID.ToString();
                txtFullName.Text = $" {Person1.FirstName} {Person1.SecondName} {Person1.ThirdName} {Person1.LastName}";
                txtNationalNo.Text = Person1.NationalNo;
                txtGender.Text = Person1.Gender == 0 ? "Male" : "Female";
                txtEmail.Text = Person1.Email;
                txtAddress.Text = Person1.Address;
                txtDateOfBirth.Text = Person1.DateOfBirth.ToString();
                txtPhone.Text = Person1.Phone;
                txtCountry.Text = GetCountryName(Person1.NationalityCountryID);
                pictureBox10.ImageLocation = Person1.ImagePath;
                lblApplicationDate.Text = DateTime.Today.ToString();
                lblCreatedBy.Text = clsGlobalSettings.User.UserName;
                lblApplicationFees.Text = GetClassFees().ToString();


                //}
                //else
                //{
                //    MessageBox.Show("Selected Person Already has Application, Choose another one.");
                //    ClearFields();
                //    Person1 = null;
                //}
            }
            else
            {
                MessageBox.Show("The PersonID or NationalNo is Wrong, Please Try Again!");
            }

            return Person1;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = clsPeopleBL.GetAllData();

            DataView dataView = new DataView(dt);
            if (dataView == null || cbFilters.SelectedItem == null)
                return;

            string selectedColumn = cbFilters.SelectedItem.ToString();
            string filterText = txtFilter.Text;
            if (string.IsNullOrWhiteSpace(filterText))
            {
                dataView.RowFilter = string.Empty;
            }
            else
            {
                if (!dataView.Table.Columns.Contains(selectedColumn))
                    return;

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

        private void btnFind_Click(object sender, EventArgs e)
        {
            GetPersonInfo();
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
                clsApplicationsBL Application1 = new clsApplicationsBL();

                if (!clsApplicationsBL.NotHaveActiveApplicationOfSameLicenseClass(cbLicenseClass.SelectedIndex + 1
                    , Convert.ToInt16(txtPersonID.Text)))
                {
                    MessageBox.Show("Choose Another License class, The selected person already have an active" +
                    $"an application for the selected class with id = {txtPersonID.Text}");
                    return;
                }

                Application1.PersonID = Convert.ToInt16(txtPersonID.Text);
                Application1.ApplicationDate = DateTime.Today;

                Application1.LicenseClassID = cbLicenseClass.SelectedIndex + 1;

                Application1.ApplicationFees = GetClassFees();
                Application1.CreatedBy = clsGlobalSettings.User.UserName;
                Application1.ApplicationStatus = 1;
                Application1.LastStatusDate = DateTime.Today;
                Application1.ApplicationTypeID = 1;



                if (Application1.Save())
                {
                    NotAdded = false;
                    lblDLApplicationID.Text = Application1.ApplicationID.ToString();
                    MessageBox.Show("Application Has Been Added Successfully.");
                }
                else MessageBox.Show("Application Has Not Added Successfully.");
            }
            else MessageBox.Show("The Application Is Already has been added");
        }

        private void cbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = GetClassFees().ToString();
        }

        private void LlblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.TryParse(txtPersonID.Text, out int personID))
            {
                UpdatePerosn.ID = personID;
                Form frm = new UpdatePerosn();
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Invalid Person LDLicenseID.");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }


    }
}