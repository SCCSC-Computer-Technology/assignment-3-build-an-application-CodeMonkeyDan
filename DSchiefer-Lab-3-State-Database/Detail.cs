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
    public partial class detailFrm : Form
    {
        public detailFrm(State state)
        {
            InitializeComponent();

            //display state information
            detailTitleLbl.Text = state.name.ToString(); //display state name

            stateIDLbl.Text = state.name.ToString() + " was state #" + state.id.ToString() +
                " to join the union."; //displays state id (when the state joined the union)

            capitalLbl.Text = "The capital is " + state.capital.ToString(); //displays the state capital

            largestCitiesLbl.Text = "The three largest cities are " +
                state.threeLargestCities.ToString(); //displays the three largest cities in the state

            populationLbl.Text = "The state population is " + state.population.ToString("N0"); //displays the states population

            medianIncomeLbl.Text = "The median income is " + state.medianIncome.ToString("C0"); //display the median income for the state

            computerJobsLbl.Text = state.percentComputerJobs.ToString() + "% of jobs in the state " +
                "are computer related"; //displays the percentage of computer jobs in the state

            flowerLbl.Text = "State Flower: " + state.flower.ToString(); //displays the state flower

            birdLbl.Text = "State Bird: " + state.bird.ToString(); //displays the state bird

            //displays the state colors
            if (state.colors == "") //many states do not have an official color
            {
                colorLbl.Text = state.name.ToString() + " does not have any official state colors";
            }
            else
            {
                colorLbl.Text = "State Color(s): " + state.colors.ToString();
            }

            //displays information about the states flag
            flagDescriptionLbl.Text = "About " + state.name.ToString() + "'s Flag";
            flagDescriptionTxtBx.Text = state.flagDescription.ToString();
        }


        //close the details form
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
