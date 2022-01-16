using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_Game
{
    public partial class Matching_Game : Form
    {
        Random random = new Random();
        Label firstClicked = null;
        Label secondClicked = null;
        int timeLeft;
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        private void AssignIconsToSquares()
        {
            
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        public Matching_Game()
        {
            InitializeComponent();
            AssignIconsToSquares();
            MessageBox.Show("You have 1 min to match all icons");
            timeLeft = 60;
            timer.Text = "60 sec";
            timer2.Start();
            
        }

        private void label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
                else
                {
                    if (secondClicked == null)
                    {
                        secondClicked = clickedLabel;
                        secondClicked.ForeColor = Color.Black;
                        timer1.Start();
                        if (firstClicked.Text == secondClicked.Text)
                        {
                            firstClicked.BackColor = Color.Green;
                            secondClicked.BackColor = Color.Green;
                            firstClicked = null;
                            secondClicked = null;
                        }
                        CheckForWinner();
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (firstClicked != null && secondClicked != null) 
            {
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;
                firstClicked = null;
                secondClicked = null;
            }
        }
        private void CheckForWinner()
        {
            // Go through all of the labels in the TableLayoutPanel, 
            // checking each one to see if its icon is matched
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // If the loop didn’t return, it didn't find
            // any unmatched icons
            // That means the user won. Show a message and close the form
            timer2.Stop();
            MessageBox.Show("You matched all the icons!, Congratulations");
            //Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            if (timeLeft > 0)
            {

                timeLeft = timeLeft - 1;
                if (timeLeft == 5) timer.ForeColor = Color.Red;
                timer.Text = timeLeft + " sec";
            }
            else
            {
                timer2.Stop();
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                tableLayoutPanel1.Enabled = false;
                //Close();
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
