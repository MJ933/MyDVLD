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
using System.IO;
namespace DVLD_PresentationLayer
{
    public partial class ShowAllUsersForm : Form
    {
        public ShowAllUsersForm()
        {
            InitializeComponent();
        }

        private void ShowAllUsersForm_Load(object sender, EventArgs e)
        {
            DataTable dt = clsUsersBL.GetAllData();
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

            // Initialize Active Status ComboBox
            cbActiveStatus.Visible = false;
        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilters.SelectedItem.ToString() == "Is Active")
            {
                txtFilter.Visible = false;
                cbActiveStatus.Visible = true;
                cbActiveStatus.SelectedIndex = 0; // Default to "True"
            }
            else
            {
                txtFilter.Visible = true;
                cbActiveStatus.Visible = false;
            }
        }

        private void cbActiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            DataView dataView = dataGridView1.DataSource as DataView;
            if (dataView != null && cbFilters.SelectedItem != null)
            {
                string selectedColumn = cbFilters.SelectedItem.ToString();
                try
                {
                    if (selectedColumn == "Is Active")
                    {
                        string selectedValue = cbActiveStatus.SelectedItem.ToString();
                        if (cbActiveStatus.SelectedItem.ToString() == "Yes")
                            selectedValue = "True";
                        else if (cbActiveStatus.SelectedItem.ToString() == "No")
                            selectedValue = "False";
                        else if (cbActiveStatus.SelectedItem.ToString() == "All")
                        {
                            dataView.RowFilter = string.Empty;
                            lblResult.Text = dataGridView1.RowCount.ToString();
                            return;
                        }
                        dataView.RowFilter = $"[{selectedColumn}] = {selectedValue}";
                    }
                    else
                    {
                        string filterText = txtFilter.Text;
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
            Form frm = new AddNewUserForm();
            frm.ShowDialog();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilters.SelectedItem.ToString() == "Person ID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                {
                    e.Handled = true;
                }
            }
            else if (cbFilters.SelectedItem.ToString() == "User ID")
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int UserID = (int)row.Cells["User ID"].Value;
            clsUsersBL Person1 = clsUsersBL.FindUserByUserID(UserID);

            if (Person1 != null)
            {
                if (clsUsersBL.DeleteUser(UserID))
                {

                    MessageBox.Show($"User With  ID {UserID} Was Deleted Successfully");
                }
                else
                    MessageBox.Show($"User With  ID \"{UserID}\" Was Not Deleted Successfully Du to Data Related to id");
            }
            else
                MessageBox.Show($"There is Not User with ID {UserID}");

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int PersonID = (int)row.Cells["Person ID"].Value;
            UpdateUserInfo.ID = PersonID;
            Form frm = new UpdateUserInfo();
            frm.ShowDialog();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int PersonID = (int)row.Cells["Person ID"].Value;
            ShowUserDetails.ID = PersonID;
            Form frm = new ShowUserDetails();
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new AddNewUserForm();
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int PersonID = (int)row.Cells["Person ID"].Value;
            ChangeUserPassword.ID = PersonID;
            Form frm = new ChangeUserPassword();
            frm.ShowDialog();
        }
    }
}
