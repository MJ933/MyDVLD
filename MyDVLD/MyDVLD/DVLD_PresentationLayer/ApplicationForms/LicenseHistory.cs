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

namespace DVLD_PresentationLayer.ApplicationForms
{
    public partial class LicenseHistory : Form
    {
        public static int LDLAppID { get; set; }
        public static int LDLicenseID { get; set; }
        //clsApplicationsBL app1;
        public LicenseHistory()
        {
            InitializeComponent();
        }
        private void LicenseHistory_Load(object sender, EventArgs e)
        {
            if (LDLAppID != 0)
            {
                LoadPersonInfo();
                LoadLDLicenses();
                LoadInternationalLicenses();
            }
        }
        private void LoadPersonInfo()
        {
            clsLicensesBL LDLicense1 = clsLicensesBL.FindLicenseByApplicationID(LDLAppID);
            clsApplicationsBL app1 = clsApplicationsBL.FindApplicationByApplicationID(LDLicense1.ApplicationID);
            if (app1 != null)
            {
                clsPeopleBL person1 = clsPeopleBL.FindPersonByID(app1.PersonID);
                string FullName = person1.FirstName + " " + person1.SecondName + " " + person1.ThirdName + " " + person1.LastName;
                txtPersonID.Text = person1.ID.ToString();
                txtFullName.Text = FullName;
                txtNationalNo.Text = person1.NationalNo;
                if (person1.Gender == 0)
                    txtGender.Text = "Male";
                else txtGender.Text = "Female";
                txtEmail.Text = person1.Email;
                txtAddress.Text = person1.Address;
                txtDateOfBirth.Text = person1.DateOfBirth.ToString();
                txtPhone.Text = person1.Phone;
                txtCountry.Text = clsCountriesBL.FindByID(person1.NationalityCountryID).CountryName;
                pictureBox10.ImageLocation = person1.ImagePath;
            }
        }

        private void LoadLDLicenses()
        {
            clsLicensesBL LDLicense1 = clsLicensesBL.FindLicenseByApplicationID(LDLAppID);
            clsApplicationsBL app1 = clsApplicationsBL.FindApplicationByApplicationID(LDLicense1.ApplicationID);

            if (LDLicense1 != null)
            {
                if (app1 != null)
                {
                    DataTable dt = clsLicensesBL.GetLocalLicenseHistoryByPersonID(app1.PersonID);
                    DataView dataView1 = new DataView(dt);
                    dataGridView1.DataSource = dataView1;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.Automatic;
                    }
                    lblRecords.Text = (dataGridView1.RowCount).ToString();
                }

            }
        }

        private void LoadInternationalLicenses()
        {
            clsLicensesBL LDLicense1 = clsLicensesBL.FindLicenseByApplicationID(LDLAppID);
            //clsApplicationsBL app1 = clsApplicationsBL.FindLDLApplicationByApplicationID(LDLicense1.ApplicationID);
            if (LDLicense1 != null)
            {
                DataTable dt = clsInternationalLicensesBL.GetInternationalLicensesDataByLDLicense(LDLicenseID);
                DataView dataView2 = new DataView(dt);
                dataGridView2.DataSource = dataView2;
                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }
                lblRecords.Text = (dataGridView2.RowCount).ToString();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int AppID = (int)(row.Cells["App.ID"].Value);
            if (AppID != 0)
            {
                DrivingLicenseInfo.ID = AppID;
                Form frm = new DrivingLicenseInfo();
                frm.ShowDialog();
            }
            else MessageBox.Show($"The LDLicense is empty! LDLicense = {AppID}");
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    int rowIndex = e.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    DataGridViewCell cell = dataGridView1.Rows[rowIndex].Cells[e.ColumnIndex];
                    Point collection = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;

                    contextMenuStrip1.Show(dataGridView1, collection);
                    contextMenuStrip1.Tag = row;

                }
            }
        }
    }
}
