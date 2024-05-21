// Programmer: Bao Han Phan
// Project Name: Hopkins School Registration Form
// Due Date: 04/26/2024
// Project Description: Individual Assignment 4

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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Phan_4
{

    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        // Declare all constants and variables used
        private const decimal LIVEACTION_PRICE = 79.95m;
        private const decimal ANIMATION_PRICE = 99.95m;
        private const int MAX_CLASSES = 4;

        private decimal totalPrice;
        private decimal classes;
        private decimal pricePerClass;

        private string paymentType;
        private string emailReceiptRequested;
        private string displayMessage;
        private string classesSelected;

        // Custom method to extract data from txt file into List Box
        private void PopulateList()
        {
            try
            {
                StreamReader inputFile;
                if (animationRadioButton.Checked)
                {
                    inputFile = File.OpenText("AnimationClasses.txt");  // Read data from AnimationClasses File
                    classesListBox.Items.Clear();
                    while (!inputFile.EndOfStream)
                    {
                        // Read lines from the input file and add it to list box
                        classesListBox.Items.Add(inputFile.ReadLine());
                    }
                    inputFile.Close();
                }
                else
                {
                    inputFile = File.OpenText("LiveActionClasses.txt");  // Read data from LiveActionClasses File
                    classesListBox.Items.Clear();
                    while (!inputFile.EndOfStream)
                    {
                        // Read lines from the input file and add it to list box
                        classesListBox.Items.Add(inputFile.ReadLine());
                    }
                    inputFile.Close();
                }
            }
            catch (Exception ex)
            {
                // Display a message if error occurs when reading file
                MessageBox.Show(ex.Message);
            }            
        }

        // Custom method to update information
        private void UpdateTotals()
        {
            // Calculate the price per class and total price
            if (animationRadioButton.Checked)
            {
                classes = classesListBox.SelectedItems.Count;
                pricePerClass = ANIMATION_PRICE;
                totalPrice = pricePerClass * classes;
            }
            else
            {
                classes = classesListBox.SelectedItems.Count;
                pricePerClass = LIVEACTION_PRICE;
                totalPrice = pricePerClass * classes;
            }
        }

        // Custom method to reset form
        private void ResetForm()
        {
            // Set up the information in the initial form
            dateMaskedTextBox.Focus();
            dateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
            firstNameTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            dateOfBirthMaskedTextBox.Text = string.Empty;
            statusComboBox.SelectedIndex = -1;
            liveActionRadioButton.Checked = true;
            cashRadioButton.Checked = true;
            emailReceiptCheckBox.Checked = false;
            classesListBox.SelectedIndex = -1;
            noteLabel.Text = "Note: The maximum registered courses is " + MAX_CLASSES.ToString() + ".";
        }

        // Handle Form Load Event
        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            // Set up initial information when loading form
            dateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");
            statusComboBox.SelectedIndex = -1;
            liveActionRadioButton.Checked = true;
            cashRadioButton.Checked = true;
            emailReceiptCheckBox.Checked = false;
            noteLabel.Text = "Note: The maximum registered courses is " + MAX_CLASSES.ToString() + ".";

            //Set ToolTip for Menu Item
            saveToolStripMenuItem.ToolTipText = "Click to show summary of registration and save the data.";
            clearToolStripMenuItem.ToolTipText = "Click to clear all information in the Form";
            exitToolStripMenuItem.ToolTipText = "Click to exit the Form";
            aboutToolStripMenuItem.ToolTipText = "Click to see more information about school.";

            // Read information from array into statusComboBox
            string[] status = { "Astor", "Producer", "Director", "Animator", "Cinematographer", "Drama Teacher", "Sound Technician", "Light Technician" };
            foreach (string value in status)
            {
                statusComboBox.Items.Add(value);
            }

            PopulateList();
            UpdateTotals();
        }


        // Handle save menu item click event
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Checking the blank value in FirstName, LastName, Email, DateofBirth
            if (string.IsNullOrEmpty(firstNameTextBox.Text) || string.IsNullOrEmpty(lastNameTextBox.Text) || string.IsNullOrEmpty(emailTextBox.Text) || !dateOfBirthMaskedTextBox.MaskCompleted)
            {
                MessageBox.Show("Check the entered information for First Name, Last Name, Email Address and Date of Birth!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Check the number of selected courses
                if (classesListBox.SelectedItems.Count == 0 || classesListBox.SelectedItems.Count > MAX_CLASSES)
                {
                    MessageBox.Show("The total selected classes must be greater than 0 and smaller than 5!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Check payment method
                    if (cashRadioButton.Checked)
                    {
                        paymentType = "Cash";
                    }
                    else  
                    {
                        paymentType = "Check";
                    }
                    // Check email receipt requested
                    if (emailReceiptCheckBox.Checked)
                    {
                        emailReceiptRequested = "Yes";
                    }
                    else
                    {
                        emailReceiptRequested = "No";
                    }

                    // Summary of selected classes
                    for (int i = 0; i < classesListBox.Items.Count; i++)
                    {
                        if (classesListBox.GetSelected(i))
                        {
                            classesSelected += ("\n" + " - " + classesListBox.Items[i]);
                        }
                    }

                    // Summary of display message
                    displayMessage = "Registration Date: " + dateMaskedTextBox.Text.ToString() + "\n" +
                        "Registrant Name: " + firstNameTextBox.Text.ToString() + " " + lastNameTextBox.Text.ToString() + "\n" +
                        "Email Adress: " + emailTextBox.Text.ToString() + "\n" +
                        "Date of Birth: " + dateOfBirthMaskedTextBox.Text.ToString() + "\n" +
                        "Status: " + statusComboBox.SelectedItem.ToString() + "\n" +
                        "Classes Selected: " + classesSelected + "\n" +
                        "Number of Class Selected: " + classes + "\n" +
                        "Price Per Classes: " + pricePerClass.ToString("c") + "\n" +
                        "Total Price: " + totalPrice.ToString("c") + "\n" +
                        "Payment Type: " + paymentType + "\n" +
                        "Email Receipt Requested: " + emailReceiptRequested + "\n" + "\n";

                    // Displaying the summary of registation information in messagebox
                    MessageBox.Show(displayMessage                
                        , "Summary of Registration Information"
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information);

                    // Write the registration information to RegistrationData.txt
                    StreamWriter outputFile;
                    outputFile = File.AppendText("RegistrationData.txt");
                    outputFile.Write(displayMessage);
                    outputFile.Close();

                    // Clear the form 
                    ResetForm();
                }            
            }

        }

        // Hanlde clear menu item click event
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        // Handle exit menu item click event
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you wish to exit the form?", "Exit option", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result==DialogResult.Yes)
            {
                this.Close();
            }
        }

        // Change information in list box when clicking radio button
        private void animationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PopulateList();
        }

        private void liveActionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PopulateList();
        }

        // Update information when changing selected classes in List Box
        private void classesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        // Handle about menu item click event
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the AboutForm form class
            AboutForm myAboutForm = new AboutForm();
            // Display AboutForm instance as a modal form
            myAboutForm.ShowDialog();
    }
    }
}
