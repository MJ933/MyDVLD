using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.ApplicationForms
{
    public partial class ShowListDetainedLicenses : Form
    {
        public ShowListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void ShowListDetainedLicenses_Load(object sender, EventArgs e)
        {
            DataTable dt = clsDetainedLicensesBL.GetAllDetainedLicensesData();
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
        private void CheckDetianed()
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = false;

            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLicense = (int)(row.Cells["L.ID"].Value);
            clsDetainedLicensesBL DLicense = clsDetainedLicensesBL.FindDetainedLicenseByLicenseID(LDLicense);
            if (!DLicense.IsReleased)
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
            }
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
            if (cbFilters.SelectedItem.ToString() == "D.ID")
            {

                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }
            }
            else if (cbFilters.SelectedItem.ToString() == "L.ID")
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
                    CheckDetianed();

                }
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReloadDataGridView()
        {
            DataTable dt = clsDetainedLicensesBL.GetAllDetainedLicensesData();
            DataView dataView1 = new DataView(dt);
            dataGridView1.DataSource = dataView1;
            lblResult.Text = dataGridView1.RowCount.ToString();
        }

        private void btnAddNewDetainedLicense_Click(object sender, EventArgs e)
        {
            Form frm = new DetainedLicense();
            frm.ShowDialog();
            ReloadDataGridView();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            Form frm = new ReleaseDetianLicense();
            frm.ShowDialog();
            ReloadDataGridView();

        }

        private void PersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            string NationalNo = (string)(row.Cells["N.No"].Value);
            if (NationalNo != "")
            {
                ShowPersonDetailsForm.ID = clsPeopleBL.FindPersonByNationalNo(NationalNo).ID;
                Form frm = new ShowPersonDetailsForm();
                frm.ShowDialog();
            }
            else MessageBox.Show("The Application is empty!");
        }

        private void LicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLicense = (int)(row.Cells["L.ID"].Value);
            if (LDLicense != 0)
            {
                clsLicensesBL License = clsLicensesBL.FindLicenseByLicenseID(LDLicense);
                DriverLicenseInfo.AppID = License.ApplicationID;
                Form frm = new DriverLicenseInfo();
                frm.ShowDialog();
            }
            else MessageBox.Show($"The LDLicense is empty! LDLicense = {LDLicense}");
        }

        private void LicenseHistoryApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLicense = (int)(row.Cells["L.ID"].Value);
            if (LDLicense != 0)
            {
                clsLicensesBL License = clsLicensesBL.FindLicenseByLicenseID(LDLicense);
                LicenseHistory.LDLicenseID = LDLicense;
                LicenseHistory.LDLAppID = License.ApplicationID;
                Form frm = new LicenseHistory();
                frm.ShowDialog();
            }
            else MessageBox.Show($"The LDLicense is empty! LDLicense = {LDLicense}");

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int LDLicense = (int)(row.Cells["L.ID"].Value);
            clsDetainedLicensesBL DLicense = clsDetainedLicensesBL.FindDetainedLicenseByLicenseID(LDLicense);
            if (!DLicense.IsReleased)
            {
                ReleaseDetianLicense.CurrentLicenseID = LDLicense;
                Form frm = new ReleaseDetianLicense();
                frm.ShowDialog();
            }
        }
    }
}
