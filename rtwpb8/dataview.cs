using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace rtwpb8
{
    public partial class dataview : Form
    {

        public dataview()
        {
            InitializeComponent();
            loadview();


        }
        private DataTable GetDataTableFromDataGridView(DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count > 0)
            {
                DataTable dt = new DataTable();

                // Create columns in DataTable (starting from index 1)
                for (int i = 1; i < dataGridView.Columns.Count; i++) // Start from index 1
                {
                    dt.Columns.Add(dataGridView.Columns[i].HeaderText);
                }

                // Add rows to DataTable
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int i = 1; i < dataGridView.Columns.Count; i++) // Start from index 1
                    {
                        dataRow[i - 1] = row.Cells[i].Value; // Adjust index to match DataTable columns
                    }
                    dt.Rows.Add(dataRow);
                }

                return dt;
            }
            else
            {
                return null;
            }
        }
        private void DeleteSelectedRows()
        {
            List<DataGridViewRow> rowsToDelete = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in data_dg.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value)) // Assuming the checkbox column is at index 0
                {
                    string householdId = row.Cells[1].Value.ToString(); // Change index to the column holding the ID

                    DialogResult result = MessageBox.Show($"Are you sure you want to delete row with ID: {householdId}?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Perform deletion from the database here using the accreditationId
                        // For example:
                        pcup_class.dbconnect = new dbconn();
                        pcup_class.dbconnect.Openconnection();
                        pcup_class.cmd = new MySqlCommand("DELETE FROM tbl_check WHERE check_id = @id", pcup_class.dbconnect.myconnect);
                        pcup_class.cmd.Parameters.AddWithValue("@id", householdId);
                        int rowsAffected = pcup_class.cmd.ExecuteNonQuery();
                        pcup_class.dbconnect.Closeconnection();
                       
                        // Assuming deletion is successful, add the row to delete from DataGridView
                        rowsToDelete.Add(row);
                    }
                }
            }

            // Remove the selected rows from the DataGridView
            loadview();
        }
        public void loadview()
        {
            data_dg.Rows.Clear();

            try
            {
                pcup_class.dbconnect = new dbconn();
                pcup_class.dbconnect.Openconnection();

                pcup_class.cmd = new MySqlCommand("SELECT check_id, check_date, check_type, check_particular, check_desired, check_taken, check_datenow, check_timeout, check_remark, check_qr FROM tbl_check", pcup_class.dbconnect.myconnect);
                pcup_class.Myreader = pcup_class.cmd.ExecuteReader();
                
                while (pcup_class.Myreader.Read())
                {
                    string checkdatereceived = pcup_class.Myreader["check_date"].ToString();
                    string checkdatenow = pcup_class.Myreader["check_datenow"].ToString();
                    string checktimeout = pcup_class.Myreader["check_timeout"].ToString();

                    DateTime datereceived;
                    DateTime datenow;
                    DateTime timeout;

                    if (DateTime.TryParse(checkdatereceived, out datereceived) && DateTime.TryParse(checkdatenow, out datenow) && DateTime.TryParse(checktimeout, out timeout))
                    {
                        pcup_class.dgrec = new string[]
                        {
                            pcup_class.Myreader["check_id"].ToString(),
                            datereceived.ToString("MMMM dd, yyyy"),
                            pcup_class.Myreader["check_type"].ToString(),
                            pcup_class.Myreader["check_particular"].ToString(),
                            pcup_class.Myreader["check_desired"].ToString(),
                            pcup_class.Myreader["check_taken"].ToString(),
                            datenow.ToString("MMMM dd, yyyy"),
                            timeout.ToString("hh:mm tt"),
                            pcup_class.Myreader["check_remark"].ToString(),
                            pcup_class.Myreader["check_qr"].ToString(),
                        };
                        
                        DataGridViewRow row = new DataGridViewRow();
                        
                        // Create a new checkbox cell and set its value to false (unchecked)
                        DataGridViewCheckBoxCell checkBoxCell = new DataGridViewCheckBoxCell
                        {
                            Value = false, // Unchecked by default
                            ReadOnly = false // Allow checking and unchecking
                        };
                        row.Cells.Add(checkBoxCell);

                        // Populate the rest of the columns with corresponding data
                        for (int i = 0; i < pcup_class.dgrec.Length; i++)
                        {
                            DataGridViewCell cell = new DataGridViewTextBoxCell();
                            cell.Value = pcup_class.dgrec[i];
                            row.Cells.Add(cell);
                        }
                        data_dg.Rows.Add(row);
                        

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message);
            }
            finally
            {
                pcup_class.dbconnect.Closeconnection();
            }
        }
        

        private void dataview_Load(object sender, EventArgs e)
        {
           
            loadview();
            this.Bounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            checker view = new checker();
            view.Show();
        }
       

        private void data_dg_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteSelectedRows();
        }
      
        
        private DataTable GetDataTable(DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count > 0)
            {
                DataTable dt = new DataTable();

                // Create columns in DataTable (starting from index 1)
                for (int i = 1; i < dataGridView.Columns.Count; i++) // Start from index 1
                {
                    dt.Columns.Add(dataGridView.Columns[i].HeaderText);
                }

                // Add rows to DataTable
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int i = 1; i < dataGridView.Columns.Count; i++) // Start from index 1
                    {
                        dataRow[i - 1] = row.Cells[i].Value; // Adjust index to match DataTable columns
                    }
                    dt.Rows.Add(dataRow);
                }

                return dt;
            }
            else
            {
                return null;
            }
        }
        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    DataTable dt = GetDataTable(data_dg); // Change dg_pending to your DataGridView name

                    if (dt != null)
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        saveFileDialog1.Filter = "Excel File|*.xlsx";
                        saveFileDialog1.Title = "Save Excel File";
                        saveFileDialog1.ShowDialog();

                        if (saveFileDialog1.FileName != "")
                        {
                            var worksheet = workbook.Worksheets.Add("Sheet1");
                            worksheet.Cell(1, 1).InsertTable(dt);
                            workbook.SaveAs(saveFileDialog1.FileName);
                            MessageBox.Show("Data exported to Excel successfully.", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data available for export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data to Excel: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int checkedRowCount = 0;
            DataGridViewRow selectedRow = null;

            foreach (DataGridViewRow row in data_dg.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value)) // Assuming the checkbox column is at index 0
                {
                    checkedRowCount++;
                    selectedRow = row;
                }
            }

            if (checkedRowCount == 1 && selectedRow != null) // Only proceed if exactly one row is checked
            {
                DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)selectedRow.Cells[0]; // Assuming the checkbox column index is 0

                if (Convert.ToBoolean(chkCell.Value))
                {
                    openform(data_dg);
                }
                else
                {
                    MessageBox.Show("Please check the checkbox in the row you want to edit.", "No Checkbox Checked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (checkedRowCount == 0)
            {
                MessageBox.Show("Please select a row to edit.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select only one row at a time for editing.", "Multiple Rows Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void refreshdata()
        {
            loadview();
        }
        private void openform(DataGridView dataGridView)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                string crimeid = selectedRow.Cells[1].Value.ToString();
                string crimerecieve = selectedRow.Cells[2].Value.ToString();
                string crimeviolation = selectedRow.Cells[3].Value.ToString();
                string crimedate = selectedRow.Cells[4].Value.ToString();
                string crimevictim = selectedRow.Cells[5].Value.ToString();
                string crimeperpetrator = selectedRow.Cells[5].Value.ToString();
                string crimedatenow = selectedRow.Cells[7].Value.ToString();
                string crimetimeout = selectedRow.Cells[8].Value.ToString();
                string crimeremarks = selectedRow.Cells[9].Value.ToString();

                // Create a new instance of the test form
                Form1 testForm = new Form1();

                // Populate the controls in the test form with data from the selected row
                testForm.label1.Text = crimeid; // Replace "name" with your control name
                testForm.daterecieved.Text = crimerecieve; // Replace "name" with your control name
                testForm.type.Text = crimeviolation; // Replace "name" with your control name
                testForm.particular.Text = crimedate; // Replace "barangaylist" with your control name
                testForm.desired.Text = crimevictim; // Replace "address" with your control name
                testForm.taken.Text = crimeperpetrator; // Replace "contactperson" with your control name
                testForm.datee.Text = crimedatenow; // Replace "phone" with your control name
                testForm.dateTimePicker1.Text = crimetimeout; // Replace "phone" with your control name
                testForm.remarks.Text = crimeremarks; // Replace "phone" with your control name



                testForm.ShowDialog();
                loadview();

            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            string searchValue = metroTextBox1.Text.Trim().ToLower(); // Get the search text and trim any leading/trailing spaces

            // Iterate through the rows of the dg_approve DataGridView
            foreach (DataGridViewRow row in data_dg.Rows)
            {
                bool rowVisible = false;

                // Iterate through the cells of the row and check if any cell contains the searchValue
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(searchValue))
                    {
                        rowVisible = true;
                        break; // If a cell contains the searchValue, no need to check other cells
                    }
                }

                // Set the row's visibility based on whether it matches the search criteria
                row.Visible = rowVisible;
            }

        }
    }
}
