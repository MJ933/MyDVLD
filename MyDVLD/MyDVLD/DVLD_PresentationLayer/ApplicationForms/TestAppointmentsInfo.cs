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
    public partial class TestAppointmentsInfo : Form
    {
        public static int ID { get; set; }
        public static int CurrentTest { get; set; }
        public TestAppointmentsInfo()
        {
            if (ID != 0)
                ucLDLAppInfoCtrl.ID = ID;
            else MessageBox.Show("The id is not found!");
            ScheduleVisionTest.NotUpdate = true;

            InitializeComponent();
        }

        private void VisionTestAppointments_Load(object sender, EventArgs e)
        {
            CurrentTest = LocalDrivingLicenseApplications.CurrentTest;
            if (CurrentTest == 2)
                lblTitle.Text = "Written Test Appointments";
            if (CurrentTest == 3)
                lblTitle.Text = "Street Test Appointments";

            DataTable dt;
            // Retrieve all appointments from the business logic layer

            dt = clsAppointmentsBL.GetAllAppointments(ID, CurrentTest);

            // Create a DataView from the DataTable
            DataView dataView1 = new DataView(dt);

            // Apply the filter to show only records where LocalDrivingLicenseApplicationID is 30

            //dataView1.RowFilter = $"LocalDrivingLicenseApplicationID = {LDLicenseID}";

            // Set the DataSource of dataGridView1 to the filtered DataView
            dataGridView1.DataSource = dataView1;

            // Enable automatic sorting for each column in dataGridView1
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
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

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int AppointmentID = (int)(row.Cells["Appointment ID"].Value);
            ScheduleVisionTest.ID = ID;
            ScheduleVisionTest.NotUpdate = false;
            ScheduleVisionTest.appointmentID = AppointmentID;
            Form frm = new ScheduleVisionTest();
            frm.ShowDialog();
            ReloadDataGridView();
        }
        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {

            // Get the DataView from the DataGridView's DataSource
            DataView dv = (DataView)dataGridView1.DataSource;

            // Access the underlying DataTable
            DataTable dt = dv.Table;

            // Check if there are any rows in the DataTable
            if (dt.Rows.Count > 0)
            {
                // Get the last row
                DataRow lastRow = dt.Rows[dt.Rows.Count - 1];

                // Get the Appointment ID from the last row
                int AppointmentID = (int)lastRow["Appointment ID"];

                if (AppointmentID != 0)
                    ScheduleVisionTest.appointmentID = AppointmentID;
            }
            ScheduleVisionTest.ID = ID;

            Form frm = new ScheduleVisionTest();
            frm.ShowDialog();
            ReloadDataGridView();
        }
        private void TakeTestApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)contextMenuStrip1.Tag;
            int AppointmentID = (int)(row.Cells["Appointment ID"].Value);
            TakeTest.CurrentTest = CurrentTest;
            ;
            clsAppointmentsBL appointment1 = clsAppointmentsBL.FindAppointmentByID(AppointmentID);

            if (!appointment1.IsLocked)
            {
                TakeTest.ID = ID;
                TakeTest.appointmentID = AppointmentID;
                Form frm = new TakeTest();
                frm.ShowDialog();
            }
            else
                MessageBox.Show("The Appointment is Locked!");
            ReloadDataGridView();
        }
        private void ReloadDataGridView()
        {
            DataTable dt = clsAppointmentsBL.GetAllAppointments(ID, CurrentTest);
            DataView dataView1 = new DataView(dt);
            dataGridView1.DataSource = dataView1;
        }

        private void ucApplicationInfoCtrl1_Load(object sender, EventArgs e)
        {

        }
    }
}
