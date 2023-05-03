using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string[] selectedFileNames;
        string fileData;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // The user selected one or more files, so do something with them.
                selectedFileNames = openFileDialog1.FileNames;
            }

        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // The user selected a folder, so do something with it.
                string selectedFolderPath = folderBrowserDialog1.SelectedPath;
                lblPath.Text = selectedFolderPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedFileNames != null && txtOutName.Text != "" && lblPath.Text != "_______________")
            {
                string path = lblPath.Text + "//" + txtOutName.Text + "." + cmbExtension.Text;

                if (File.Exists(path))
                {
                    DialogResult toContinue = MessageBox.Show("file name: " + txtOutName.Text + "." + cmbExtension.Text + " already exist in the directory, would you like to overide the file?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (toContinue == DialogResult.No)
                        return;

                }

                string data = "";
                foreach (string fileName in selectedFileNames)
                {
                    StreamReader sr = new StreamReader(fileName);
                    if (chkFileName.Checked)
                        fileData = "/////" + Path.GetFileName(fileName) + "/////\n\n";
                    fileData += sr.ReadToEnd();

                    data+= (fileData + "\n");

                }


                if (cmbExtension.Text == "pdf")
                {


                    //Document document = new Document();
                    //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
                    //document.Open();
                    //Paragraph paragraph = new Paragraph(data);
                    //document.Add(paragraph);
                    //document.Close();


                    // Create a new PDF document
                    //var document = new PdfDocument();

                    // Add a page to the document
                    //var page = document.AddPage();


                    //// Draw some text on the page
                    //var gfx = XGraphics.FromPdfPage(page);
                    //gfx.DrawString(data,new XFont("Gisha",12),new XBrush(),new)

                    //// Save the document to a file
                    //document.Save(path);

                    //// Close the document
                    //document.Close();

                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        using (Document doc = new Document())
                        {
                            PdfWriter.GetInstance(doc, fs);

                            doc.Open();

                            // // Disable page margins to allow text to be positioned freely
                            //doc.SetMargins(0, 0, 0, 0);

                            // // Create a single-column table with auto-layout
                            // PdfPTable table = new PdfPTable(1);
                            // table.WidthPercentage = 100;
                            // table.DefaultCell.Border = 0;

                            // // Add each line of data to the table as a separate cell
                            // string[] lines = data.Split('\n');
                            // foreach (string line in lines)
                            // {
                            //     PdfPCell cell = new PdfPCell(new Phrase(line));
                            //     cell.Border = 0;
                            //     table.AddCell(cell);
                            // }

                            // // Add the table to the document
                            //// doc.Add(table);
                            doc.Add(new Paragraph(data));

                            doc.Close();
                        }
                    }
                }
                else
                {
                    StreamWriter sw = new StreamWriter(path);
                    sw.WriteLine(data);
                    sw.Close();
                }

               


                DialogResult result = MessageBox.Show("File: "+ txtOutName.Text + "." + cmbExtension.Text + " created succesfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Check if the user clicked the OK button
                    if (result == DialogResult.OK)
                    {
                        // Save the changes here...

                        // Close the form
                        this.Close();
                    }
                }
            else
            {
                    DialogResult result = MessageBox.Show("insert all neccesary details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            }


        

    }
}
