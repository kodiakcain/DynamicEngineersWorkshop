using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;

namespace DynamicEngineersWorkshop
{
    public partial class energyconversions : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;

        int questionIndex;
        double answer;
        double cal01A;

        //random number generator
        static Random numberGen = new Random();

        //declaring variable for question 1

        decimal var01A = numberGen.Next(10, 30);
       
        Properties.Settings.Default.var01A = var01A;


        //declaring variables for question 2
        double var01B = numberGen.Next(150, 300);
        double var02B = numberGen.Next(6, 8);
        double var03B = numberGen.Next(15, 20);
        double var04B = numberGen.Next(50, 75);


        //declaring variables for question 3
        double var01C = numberGen.Next(25, 50);
        double var02C = numberGen.Next(200, 250);
        double var03C = numberGen.Next(200, 250);

        //declaring variables for question 4 
        double var01D = numberGen.Next(75, 80);
        double var02D = numberGen.Next(30, 40);
        double var03D = numberGen.Next(10, 20);

        //declaring variables for question 5
        double var01E = numberGen.Next(2500, 3000);
        double var02E = numberGen.Next(60, 70);
        double var03E = numberGen.Next(5, 10);

        //declaring variables for question 6
        double var01F = numberGen.Next(2, 5);
        double var02F = numberGen.Next(1250, 1500);
        double var03F = numberGen.Next(5, 10);
        double var04F = numberGen.Next(30, 50);

        public static class randomVariables { 
        }
        public void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Visible = false;
            Label1.Visible = false;
            Submit.Visible = false;
            Label2.Visible = false;
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            makeVisible();
            calcAnswer();
            checkFunction();
           
        }

        protected void Start_Click(object sender, EventArgs e)
        {
            makeVisible();
            generateQuestion();
        }
        public void generateQuestion()
        {
        
            string[] questions =
            {
                $"Jack went up the {var01A} meter hill to fetch a pail of water. On the way back down he tripped and fell, rolling all the way to the bottom. If friction, bumps and bruises used up half of the gravitational energy that he had at the top of the hill, how fast was he going at the bottom?",
                $"Jill's slingshot has a spring constant of {var01B} N/m. If she pulls it back {var02B} inches, how fast will the {var03B} gram pebble fly? What if the slingshot is {var04B} efficient?",
                $"The hot gases in a cylinder push a piston enough to expand the volume trapped from {var01C} to {var02C} ml at an average pressure of {var03C} kPa. How much work is done by the gasses?",
                $"A {var01D}% efficient {var02D} kw electric motor runs for {var03D} seconds. How much energy does it produce?",
                $"A gallon of gasoline contains 120 MJ of chemical energy. Internal combustion engines run at roughly 25% efficiency. How much gasoline is required to accelerate a {var01E} kg car and driver from rest to {var02E} mph in {var03E} seconds?",
                $"A punkin' chunkin' air canon launches a {var01F} kg pumpkin {var02F}m horizontally in {var03F} seconds. How much work did the expanding gases have to do if the air canon is {var04F} efficient?"
            };

            Random displayQuestionGen = new Random();
            questionIndex = 0;
            // displayQuestionGen.Next(questions.Length);

            Label1.Text = Convert.ToString(questions[questionIndex]);
        }

        public void calcAnswer()
        {
           
            if (questionIndex == 0)
            {
                Properties.Settings.Default.var01A = var01A;

                cal01A = Math.Sqrt(9.81 * Convert.ToDouble(var01A));
                cal01A = Math.Round(cal01A, 8);
                answer = cal01A;
             
                Label3.Text = Convert.ToString(var01A);
            }
        }

        public void checkFunction()
        {
           double userAnswer = Convert.ToDouble(TextBox1.Text);
          

           if (questionIndex == 0)
            {
                if (Math.Abs(answer - userAnswer) < 0.1) {
                    Label2.Text = "Correct!";
                }
                else
                {
                    answer = Math.Round(answer, 8);
                    Label2.Text = Convert.ToString(answer);
                }
            }
        }

        public void makeVisible()
        {
            TextBox1.Visible = true;
            Label1.Visible = true;
            Submit.Visible = true;
            Label2.Visible = true;
            Start.Visible = false;
        }

        public void makeInvisible()
        {
            TextBox1.Visible = false;
            Label1.Visible = false;
            Submit.Visible = false;
            Label2.Visible = false; 
        }

        void generateXp()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("update member_master_tbl set user_xp=@user_xp WHERE " +
                    "user_name = '" + @Session["username"].ToString().Trim() + "'", con);
                int xp = 5;
                int current_xp = Convert.ToInt32(Session["user_xp"]);
                int new_xp = xp + current_xp;
                string new_xp2 = Convert.ToString(new_xp);
                cmd.Parameters.AddWithValue("@user_xp", new_xp2);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
        }
        



        }
} 
