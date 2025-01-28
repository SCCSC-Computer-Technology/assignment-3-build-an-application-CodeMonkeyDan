//Daniel Schiefer aka CodeMonkeyDan
//CPT-206-A80S-2025SP
//Lab #3: State Information Database


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSchiefer_Lab_3_State_Database
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();
        }

        private void stateBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.stateBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.stateDataDataSet);

        }

        private void mainFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'stateDataDataSet.State' table. You can move, or remove it, as needed.
            this.stateTableAdapter.Fill(this.stateDataDataSet.State);

            //create a dataview to sort the data by the state name column
            DataView sortedByState = new DataView(this.stateDataDataSet.State)
            {
                Sort = "State ASC" //sort by state name in ascending order
            };

            //binds the dataview to the combobox and displays only the state column
            stateNameCmbBx.DataSource = sortedByState;
            stateNameCmbBx.DisplayMember = "State";
        }


        //display single state data on a seperate form
        private void displayBtn_Click(object sender, EventArgs e)
        {
            //retrieve the user selected state
            string selectedState = stateNameCmbBx.Text;

            //search the dataset for the selected state
            DataRow[] selectedStateData = stateDataDataSet.State.Select("State = '" + selectedState + "'");

            //checks if any rows were found
            if (selectedStateData.Length > 0)
            {
                //creates a new state object
                State state = new State
                    (
                    id: Convert.ToByte(selectedStateData[0]["StateID"]),
                    name: selectedStateData[0]["State"].ToString(),
                    population: Convert.ToInt32(selectedStateData[0]["Population"]),
                    flagDescription: selectedStateData[0]["Flag Description"].ToString(),
                    flower: selectedStateData[0]["Flower"].ToString(),
                    bird: selectedStateData[0]["Bird"].ToString(),
                    colors: selectedStateData[0]["Colors"].ToString(),
                    threeLargestCities: selectedStateData[0]["Largest Cities"].ToString(),
                    capital: selectedStateData[0]["Capital"].ToString(),
                    medianIncome: Convert.ToInt32(selectedStateData[0]["Median Income"]),
                    percentComputerJobs: Convert.ToByte(selectedStateData[0]["Computer Jobs (%)"])
                    );

                //create and show the detail form, passing the State object to it
                detailFrm newdetailFrm = new detailFrm(state);
                newdetailFrm.ShowDialog();
            }
            else
            {
                //displays error message if no matching state was found
                MessageBox.Show("State was not found. Please select a valid state.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //search database for user input values
        private void searchBtn_Click(object sender, EventArgs e)
        {
            ResetDataGridView();

            if (idRdBtn.Checked) //search by StateID
            {
                int userSearch = 0;
                if(int.TryParse(searchTxtBx.Text, out userSearch)) //parses user input to an int
                {
                    if (userSearch > 0 && userSearch < 51) //verifies user input is between 1-50
                    {
                        this.stateTableAdapter.SearchStateID(this.stateDataDataSet.State, userSearch);
                    }
                    else //shows error message if user input an integer that was not between 1-50
                    {
                        MessageBox.Show("Please enter a valid StateID (1-50)", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else //shows error message if user input was not an integer
                {
                    MessageBox.Show("Please enter a valid StateID (1-50)", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else if (stateRdBtn.Checked) //search by State
            {
                this.stateTableAdapter.SearchState(this.stateDataDataSet.State, searchTxtBx.Text);
            }
            else if (flowerRdBtn.Checked) //search by Flower
            {
                this.stateTableAdapter.SearchFlower(this.stateDataDataSet.State, searchTxtBx.Text);
            }
            else if (birdRdBtn.Checked) //search by Bird
            {
                this.stateTableAdapter.SearchBird(this.stateDataDataSet.State, searchTxtBx.Text);
            }
            else if (colorsRdBtn.Checked) //search by Colors
            {
                this.stateTableAdapter.SearchColors(this.stateDataDataSet.State, searchTxtBx.Text);
            }
            else if (cityRdBtn.Checked) //search by three largest cities OR capital
            {
                this.stateTableAdapter.SearchCity(this.stateDataDataSet.State, searchTxtBx.Text);
            }
            else //displays error message if user did not select a field to look in
            {
                MessageBox.Show("Please select a field to search", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        //filter database by user input
        private void filterBtn_Click(object sender, EventArgs e)
        {
            ResetDataGridView();

            //checks if a field and an operator were selected
            bool fieldSelected = fieldGrpBx.Controls.OfType<RadioButton>().Any(r => r.Checked);
            bool operatorSelected = operatorsGrpBx.Controls.OfType<RadioButton>().Any(r => r.Checked);

            if (fieldSelected && operatorSelected)
            {
                //assigns the operator
                string filterOperator = "=";
                if (lessThanRdBtn.Checked)
                {
                    filterOperator = "<";
                }
                else if (greaterThanRdBtn.Checked)
                {
                    filterOperator = ">";
                }

                //assigns the field
                string filterField = "Population";
                if (medianIncomeRdBtn.Checked)
                {
                    filterField = "Median Income";
                }
                else if (computerJobsRdBtn.Checked)
                {
                    filterField = "Computer Jobs (%)";
                }

                //assigns the value
                int filterValue;
                if (int.TryParse(filterTxtBx.Text, out filterValue))
                {
                    //concatenates the filter condition
                    string filterCondition = "[" + filterField + "] " + filterOperator + " " + filterValue;

                    //applies the filter condition to the BindingSource
                    try
                    {
                        stateBindingSource.Filter = filterCondition;
                    }
                    catch (Exception ex) //handles potential errors
                    {
                        MessageBox.Show("Error applying filter", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //displays an error message if the user input was not a valid integer
                    MessageBox.Show("Please select a valid integer to filter", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //displays an error message if a field or operator were not selected
                MessageBox.Show("Please select a field and operator to filter results", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //clear and reset form
        private void resetBtn_Click(object sender, EventArgs e)
        {
            ResetDataGridView();
            idRdBtn.Checked = false;
            stateRdBtn.Checked = false;
            flowerRdBtn.Checked = false;
            birdRdBtn.Checked = false;
            colorsRdBtn.Checked = false;
            cityRdBtn.Checked = false;
            searchTxtBx.Text = string.Empty;
            populationRdBtn.Checked = false;
            medianIncomeRdBtn.Checked = false;
            computerJobsRdBtn.Checked = false;
            equalsToRdBtn.Checked = false;
            lessThanRdBtn.Checked = false;
            greaterThanRdBtn.Checked = false;
            filterTxtBx.Text = string.Empty;
        }


        //reset data grid view
        private void ResetDataGridView()
        {
            stateBindingSource.RemoveFilter();
            this.stateTableAdapter.Fill(this.stateDataDataSet.State);
        }


        //thank user and exit applicationk
        private void exitBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thank you for using my State Information Database." +
                "\n\ncreated by: CodeMonkeyDan");
            this.Close();
        }
    }
}
