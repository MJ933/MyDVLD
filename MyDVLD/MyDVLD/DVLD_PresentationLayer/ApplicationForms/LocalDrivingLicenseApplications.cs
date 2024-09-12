using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.ApplicationForms
{
    public partial class LocalDrivingLicenseApplications : Form
    {
        public static int CurrentTest { get; set; }
        public LocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        private void LocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            DataTable dt = clsApplicationsBL.GetAllData();
            DataView dataView1 = new DataView(dt);
            dataGridView1.DataSource = dataView1;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            foreach (DataColumn column in dt.Columns)
            {
                cbFilters.Items.Add(column.ColumnName);
            }
            if (cbFilters.Items.Count > 0)
            {
                cbFilters.SelectedIndex = 0;
            }
            lblResult.Text = dataGridView1.RowCount.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = dataGridView1.DataSource as DataView;
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
                lblResult.Text = dataGridView1.RowCount.ToString();

            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.SelectedItem.ToString() == "L.D.L.AppID")
            {

                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }
            }
            else if (cbFilters.SelectedItem.ToString() == "Passed Test")
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new NewLocalDrivingLicenseApplication();
            frm.ShowDialog();
            ReloadDataGridView();

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
                    CheckTests();
                    CheckMenu();
                }
            }


        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            if (LDLAppID > 0)
            {
                LDLAppInfo.AppID = LDLAppID;
                Form frm = new LDLAppInfo();
                frm.ShowDialog();
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            clsApplicationsBL Application1 = clsApplicationsBL.FindApplicationByLDLApplicationID(LDLAppID);
            if (Application1 != null)
            {
                if (MessageBox.Show("Do you want to cancel this application?") == DialogResult.OK)
                {
                    if (Application1.ApplicationStatus == 1)
                    {
                        if (Application1.CancelApplicationStatus())
                        {
                            MessageBox.Show("The Application has Canceled");
                            ReloadDataGridView();
                        }
                        else MessageBox.Show("The Application has a problem and NOT Canceled!");
                    }
                    else if (Application1.ApplicationStatus == 2)
                        MessageBox.Show("The Application is Already  Canceled!");
                    else if (Application1.ApplicationStatus == 3)
                        MessageBox.Show("The Application is Already  Completed!");


                    else MessageBox.Show("The Application has a problem and NOT Canceled!");

                }
                else return;

            }
            else MessageBox.Show($"There IS Not LDLApplication With LDLicenseID = {LDLAppID}");
            ReloadDataGridView();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReloadDataGridView()
        {
            DataTable dt = clsApplicationsBL.GetAllData();
            DataView dataView1 = new DataView(dt);
            dataGridView1.DataSource = dataView1;
            lblResult.Text = dataGridView1.RowCount.ToString();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            clsApplicationsBL Application1 = clsApplicationsBL.FindApplicationByLDLApplicationID(LDLAppID);
            if (Application1 != null)
            {
                if (clsApplicationsBL.DeleteLocalApplication(Application1.ApplicationID))
                    MessageBox.Show("The application was successfully Deleted!");
                else MessageBox.Show("The application was NOT Deleted!");
            }
            else MessageBox.Show("The Application is empty!");
            ReloadDataGridView();

        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            //clsDrivingLicenseServicesBL Application1 = clsDrivingLicenseServicesBL.FindApplicationByLDLApplicationID(LDLAppID);
            TestAppointmentsInfo.ID = LDLAppID;
            Form frm = new TestAppointmentsInfo();
            frm.ShowDialog();
            ReloadDataGridView();

        }
        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            //clsDrivingLicenseServicesBL Application1 = clsDrivingLicenseServicesBL.FindApplicationByLDLApplicationID(LDLAppID);
            TestAppointmentsInfo.ID = LDLAppID;
            Form frm = new TestAppointmentsInfo();
            frm.ShowDialog();
            ReloadDataGridView();

        }
        private void ScheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            //clsDrivingLicenseServicesBL Application1 = clsDrivingLicenseServicesBL.FindApplicationByLDLApplicationID(LDLAppID);
            TestAppointmentsInfo.ID = LDLAppID;
            Form frm = new TestAppointmentsInfo();
            frm.ShowDialog();
            ReloadDataGridView();

        }

        private void CheckMenu()
        {

            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            clsApplicationsBL app1 = clsApplicationsBL.FindApplicationByLDLApplicationID(LDLAppID);
            if (app1.ApplicationStatus == 3)
            {
                editApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                ScheduleToolStripMenuItem.Enabled = false;
                if (clsLicensesBL.DoesLicenseExistForApplication(app1.ApplicationID))
                {
                    issuingDrivingLicenseToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
                ScheduleToolStripMenuItem.Enabled = true;
            }

        }
        // CheckTests Function is completed.
        private void CheckTests()
        {
            scheduleVisionTestToolStripMenuItem.Enabled = false;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            ScheduleStreetTestToolStripMenuItem.Enabled = false;
            ScheduleToolStripMenuItem.Enabled = true;

            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            CurrentTest = 1;
            if (clsTestsBL.HasTestResult(LDLAppID, 1))
            {
                CurrentTest = 2;
                if (!clsTestsBL.GetTestResult(LDLAppID, 1))
                {
                    scheduleVisionTestToolStripMenuItem.Enabled = true;
                    return;
                }
                else

                {
                    scheduleVisionTestToolStripMenuItem.Enabled = false;

                    if (clsTestsBL.HasTestResult(LDLAppID, 2))
                    {
                        CurrentTest = 3;
                        if (!clsTestsBL.GetTestResult(LDLAppID, 2))
                        {
                            scheduleWrittenTestToolStripMenuItem.Enabled = true;
                            return;
                        }
                        else
                        {
                            scheduleWrittenTestToolStripMenuItem.Enabled = false;
                            if (clsTestsBL.HasTestResult(LDLAppID, 3))
                            {
                                //CurrentTest = 3;
                                if (!clsTestsBL.GetTestResult(LDLAppID, 3))
                                    ScheduleStreetTestToolStripMenuItem.Enabled = true;
                                else
                                {
                                    ScheduleStreetTestToolStripMenuItem.Enabled = false;
                                    ScheduleToolStripMenuItem.Enabled = false;
                                    issuingDrivingLicenseToolStripMenuItem.Enabled = true;
                                }
                            }
                            else ScheduleStreetTestToolStripMenuItem.Enabled = true;
                        }
                    }
                    else
                        scheduleWrittenTestToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                scheduleVisionTestToolStripMenuItem.Enabled = true;
            }
            //else MessageBox.Show("We cannot show the tests correctly!,There is an error in CheckTests Function!");
        }


        private void issuingDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            //clsDrivingLicenseServicesBL Application1 = clsDrivingLicenseServicesBL.FindApplicationByLDLApplicationID(LDLAppID);
            IssueDrivingLicense.ID = LDLAppID;
            Form frm = new IssueDrivingLicense();
            frm.ShowDialog();
            ReloadDataGridView();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            clsApplicationsBL app1 = clsApplicationsBL.FindApplicationByLDLApplicationID(LDLAppID);
            if (app1 != null)
            {
                DriverLicenseInfo.AppID = app1.ApplicationID;
                Form form = new DriverLicenseInfo();
                form.ShowDialog();
            }
            else MessageBox.Show("Invalid Application LDLicenseID!");

        }

        private void showPersonLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLAppID = (int)(row.Cells["L.D.L.AppID"].Value);
            clsApplicationsBL app1 = clsApplicationsBL.FindApplicationByLDLApplicationID(LDLAppID);
            if (app1 != null)
            {
                LicenseHistory.LDLAppID = app1.ApplicationID;
                Form form = new LicenseHistory();
                form.ShowDialog();
            }
            else MessageBox.Show("Invalid Application LDLicenseID!");
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
