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
    public partial class ManageTestTypes : Form
    {
        public ManageTestTypes()
        {
            InitializeComponent();
        }

        private void ManageTestTypes_Load(object sender, EventArgs e)
        {
            DataTable dt = clsTestTypeBL.GetAllData();
            DataView dataView1 = new DataView(dt);
            dataGridView1.DataSource = dataView1;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }


            lblResult.Text = dataGridView1.RowCount.ToString();
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
            int PersonID = (int)row.Cells["LDLicenseID"].Value;
            EditTestType.ID = PersonID;
            Form frm = new EditTestType();
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
