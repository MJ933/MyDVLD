using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
namespace DVLD_PresentationLayer
{
    public partial class ShowAllPeopleForm : Form
    {
        public ShowAllPeopleForm()
        {
            InitializeComponent();
        }

        private void ShowAllPeopleForm_Load(object sender, EventArgs e)
        {
            DataTable dt = clsPeopleBL.GetAllData();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new AddNewPersonForm();
            frm.ShowDialog();
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

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int PersonID = (int)row.Cells["Person LDLicenseID"].Value;
            UpdatePerosn.ID = PersonID;
            Form frm = new UpdatePerosn();
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int PersonID = (int)row.Cells["Person LDLicenseID"].Value;
            clsPeopleBL Person1 = clsPeopleBL.FindPersonByID(PersonID);
            if (Person1 != null)
            {
                File.Delete(Person1.ImagePath);
                if (clsPeopleBL.DeletePerson(PersonID))
                {

                    MessageBox.Show($"Person With  LDLicenseID {PersonID} Was Deleted Successfully");
                }
                else
                    MessageBox.Show($"Person With  LDLicenseID \"{PersonID}\" Was Not Deleted Successfully");
            }
            else
                MessageBox.Show($"There is not person with LDLicenseID {PersonID}");
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = ((DataGridViewRow)contextMenuStrip1.Tag);
            int PersonID = (int)row.Cells["Person LDLicenseID"].Value;
            ShowPersonDetailsForm.ID = PersonID;
            Form form = new ShowPersonDetailsForm();
            form.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new AddNewPersonForm();
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
