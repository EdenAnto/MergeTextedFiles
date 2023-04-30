using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;



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
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
                    document.Open();
                    Paragraph paragraph = new Paragraph(data);
                    document.Add(paragraph);
                    document.Close();
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
