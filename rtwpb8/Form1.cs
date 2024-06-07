using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace rtwpb8
{
    public partial class Form1 : Form
    {
      
       
        public Form1()
        {
            InitializeComponent();
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if all ComboBoxes have selections
                if (desired.SelectedItem == null || type.SelectedItem == null || taken.SelectedItem == null)
                {
                    MessageBox.Show("Please make a selection in all ComboBoxes.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get inputs from ComboBoxes
                string daterecieved = DateTime.Now.ToString("MMMM dd, yyyy");
                string input1 = type.SelectedItem.ToString();
                string input2 = particular.Text.ToString();
                string input3 = desired.SelectedItem.ToString();
                string input4 = taken.SelectedItem.ToString();
                string dateInput = DateTime.Now.ToString("MMMM dd, yyyy");
                string timeout = DateTime.Now.ToString("hh:mm tt"); // Assuming timeout is a DateTimePicker with time format
                string input7 = remarks.Text.ToString();

                // Combine all inputs into a single formatted string
                string combinedInput = $"Date Received: {daterecieved}\n" +
                                       $"Document Type: {input1}\n" +
                                       $"Particulars: {input2}\n" +
                                       $"Action Desired: {input3}\n" +
                                       $"Action Taken: {input4}\n" +
                                       $"Date: {dateInput}\n" +
                                       $"Time Out: {timeout}\n" +
                                       $"Remarks: {input7}";

                // Generate QR code
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(combinedInput, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                // Display QR code in PictureBox
                pictureBox1.Image = qrCodeImage;

                // Convert QR code image to byte array
                byte[] qrCodeImageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    qrCodeImageBytes = ms.ToArray();
                }

                SaveToDatabase(daterecieved, input1, input2, input3, input4, dateInput, timeout, input7, qrCodeImageBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while generating the QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if there is an image in the PictureBox
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("No QR code image to export.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Prompt the user to select a save location and file name
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                    saveFileDialog.Title = "Save QR Code Image";
                    saveFileDialog.FileName = "QRCode.png";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Get the selected file path
                        string filePath = saveFileDialog.FileName;

                        // Save the image to the selected file
                        System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                        switch (System.IO.Path.GetExtension(filePath).ToLower())
                        {
                            case ".jpg":
                                format = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = System.Drawing.Imaging.ImageFormat.Bmp;
                                break;
                        }
                        pictureBox1.Image.Save(filePath, format);

                        MessageBox.Show("QR code image exported successfully.", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while exporting the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void SaveToDatabase(string datereceived, string type, string particular, string desired, string taken, string datenow, string timeout, string remarks, byte[] qrCodeImage)
        {
            try
            {
                // Generate a unique file name for the image
                string uniqueFileName = $"QRCode_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.png";
                string imagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), uniqueFileName);
                File.WriteAllBytes(imagePath, qrCodeImage);

                pcup_class.dbconnect = new dbconn();
                pcup_class.dbconnect.Openconnection();

                string query = "INSERT INTO tbl_check (check_date, check_type, check_particular, check_desired, check_taken, check_datenow, check_timeout, check_remark, check_qr) " +
                               "VALUES (@check_date, @check_type, @check_particular, @check_desired, @check_taken, @check_datenow, @check_timeout, @check_remark, @check_qr)";

                using (var cmd = new MySqlCommand(query, pcup_class.dbconnect.myconnect))
                {
                    cmd.Parameters.AddWithValue("@check_date", datereceived);
                    cmd.Parameters.AddWithValue("@check_type", type);
                    cmd.Parameters.AddWithValue("@check_particular", particular);
                    cmd.Parameters.AddWithValue("@check_desired", desired);
                    cmd.Parameters.AddWithValue("@check_taken", taken);
                    cmd.Parameters.AddWithValue("@check_datenow", datenow);
                    cmd.Parameters.AddWithValue("@check_timeout", timeout);
                    cmd.Parameters.AddWithValue("@check_remark", remarks);
                    cmd.Parameters.AddWithValue("@check_qr", imagePath); // Save the unique image location

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Information saved to the database successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving to the database: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pcup_class.dbconnect.Closeconnection();
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            checker view = new checker();
            view.Show();
        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
