using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Theory
// https://tradingsim.com/blog/trading-days-in-a-year/

namespace Moon17
{
    public partial class Form1 : Form
    {
        private static double CONVERSION_FACTOR_DAYS = 1.45418;
        private static double STOCK_TRADING_HOURS_PER_DAY = 6.5; // currently stock hours are discounted 
        public Form1()
        {
            InitializeComponent();
        }

        private void rBtnYear_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            calculate();
        }


        private void calculate()
        {
            if (txtBoxInput.Text.Length > 0)
            {
                double cDays = getCDays();

                string Format = (radioButtonFull.Checked) ? "{0:0.00}" : "{0:0}";

                lblYears.Text = String.Format(Format, GetUpToNDigits(cDays / 365));
                lblMonths.Text = String.Format(Format, GetUpToNDigits(cDays / 365*12));
                lblWeeks.Text = String.Format(Format, GetUpToNDigits(cDays / 7));
                lblCDays.Text = String.Format(Format, GetUpToNDigits(cDays));
                lblHours.Text = String.Format(Format, GetUpToNDigits(cDays * 24));
                lblMinutes.Text = String.Format(Format, GetUpToNDigits(cDays * 24 * 60));
                lblSeconds.Text = String.Format(Format, GetUpToNDigits(cDays * 24 * 60 * 60));

                if (!rBtnTHour.Checked)
                {
                    lblTDays.Text = String.Format(Format, GetUpToNDigits(cDays / CONVERSION_FACTOR_DAYS));
                    lblTHours.Text = String.Format(Format, GetUpToNDigits((cDays / CONVERSION_FACTOR_DAYS) * 24));
                    lblTMinutes.Text = String.Format(Format, GetUpToNDigits((cDays / CONVERSION_FACTOR_DAYS) * 24 * 60));
                    lblTSeconds.Text = String.Format(Format, GetUpToNDigits((cDays / CONVERSION_FACTOR_DAYS) * 24 * 60 * 60));
                } 
                else
                {
                    lblTDays.Text = String.Format(Format, 0);
                    lblTHours.Text = String.Format(Format, 0);
                    lblTMinutes.Text = String.Format(Format, 0);
                    lblTSeconds.Text = String.Format(Format, 0);
                }
            }
        }

        private double GetUpToNDigits(double number)
        {
            double minSize = 0;
            double maxSize = 0;
            if (radioButton2D.Checked == true)
            {
                minSize = 10.0; maxSize = 100.0;
            } 
            else if (radioButton3D.Checked == true)
            {
                minSize = 100.0; maxSize = 1000.0;
            }
            else if (radioButton4D.Checked == true)
            {
                minSize = 1000.0; maxSize = 10000.0;
            }
            else if (radioButtonFull.Checked)
            {
                return number;
            }

            if ((number < maxSize) && (number >= minSize))
                return number;
            else if (number >= maxSize)
                while (number >= maxSize) number /= 10;
            else
                while (number < minSize) number *= 10;

            return number;
        }

        private double getCDays()
        {
            double number = Convert.ToDouble(txtBoxInput.Text);
            double cDays = 0.0F;
            if (rBtnYear.Checked)
            {
                cDays = number * 365 + leapDays(number);
            }
            else if (rBtnMonth.Checked)
            {
                double years = number / 12;
                cDays = years * 365 + leapDays(years);
            }
            else if (rBtnWeek.Checked)
            {
                cDays = number * 7;
            }
            else if (rBtnCDays.Checked)
            {
                cDays = number;
            }
            else if (rBtnTDays.Checked)
            {
                cDays = number * CONVERSION_FACTOR_DAYS;
            }
            else if (rBtnTHour.Checked)
            {
                cDays = number / 24;
            }

            return cDays;
        }


        private double leapDays(double years)
        {
            return years / 4;
        }

        private void rBtnMonth_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void rBtnWeek_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void rBtnCDays_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void rBtnTDays_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void rBtnHour_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void radioButton2D_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void radioButton3D_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void radioButton4D_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void radioButtonFull_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void bPlus_Click(object sender, EventArgs e)
        {
            long n = Convert.ToInt64(tHMSnumber.Text);
            if ( (n > 0) && (n*10 > 0)) // when overflow it becames negative, so if n*10 still positve it can be increased
                tHMSnumber.Text = "" + n*10;

            calculateHMS();

        }

        private void bMinus_Click(object sender, EventArgs e)
        {
            long n = Convert.ToInt64(tHMSnumber.Text);
            if ((n > 0) && (n / 10 > 0)) 
                // if dividing will lead to loosing digit then do not divide
                if ((n % 10) == 0)
                    tHMSnumber.Text = "" + n / 10;

            calculateHMS();

        }

        private void bHMScalculate_Click(object sender, EventArgs e)
        {
            calculateHMS();
        }

        private double getDays()
        {
            double result = 0;
            long n = Convert.ToInt64(tHMSnumber.Text);
            if (rHours.Checked) result = n / 24.0;
            else if (rMinutes.Checked) result = n / 24.0 / 60;
            else if (rSeconds.Checked) result = n / 24.0 / 60 / 60;
            return result;
        }

        private void calculateHMS()
        {
            double cDays = getDays();
            if (rbTrading.Checked) cDays *= CONVERSION_FACTOR_DAYS;

            string Format = "{0:0.00}";

            lblHMSdays.Text = String.Format(Format, cDays);
            lblHMSyears.Text = String.Format(Format, cDays/365.25);
            lblHMSmonths.Text = String.Format(Format, cDays / 30.44);
            lblHMSweeks.Text = String.Format(Format, cDays / 52.17);
            lblHMShours.Text = String.Format(Format, cDays * 24);
            lblHMSminutes.Text = String.Format(Format, cDays * 24 * 60);
            lblHMSseconds.Text = String.Format(Format, cDays * 24 * 60 * 60);
        }

        private void rbCalendar_CheckedChanged(object sender, EventArgs e)
        {
            calculateHMS();
        }

        private void rbTrading_CheckedChanged(object sender, EventArgs e)
        {
            calculateHMS();
        }

        private void rMinutes_CheckedChanged(object sender, EventArgs e)
        {
            calculateHMS();
        }

        private void rSeconds_CheckedChanged(object sender, EventArgs e)
        {
            calculateHMS();
        }

        private void rHours_CheckedChanged(object sender, EventArgs e)
        {
            calculateHMS();
        }
    }
}
