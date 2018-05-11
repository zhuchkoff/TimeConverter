using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeConvertingLibrary;

// Theory
// https://tradingsim.com/blog/trading-days-in-a-year/

namespace Moon17
{
    public partial class Form1 : Form
    {
        private TimeConverter timeConverter = new TimeConverter();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            try
            {
                double number = Convert.ToDouble(txtBoxInput.Text);
                if (number > 0)
                {                    
                    TimeBean timeBean = new TimeBean();
                    SetupTimeBean(timeBean, number);
                    timeConverter.MakeConversion(timeBean);
                    DisplayResults(timeBean);
                }
            } catch (Exception e)
            {
                throw e;
            }
        }
       
        private void DisplayResults(TimeBean timeBean)
        {
            string Format = (radioButtonFull.Checked) ? "{0:0.00}" : "{0:0}";
            double cells, degrees;

            // Years & Months & Weeks
            cells = GetUpToNDigits(timeBean.years); degrees = GetUpToNDigits(timeBean.years * 360);
            lblYears.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.months); degrees = GetUpToNDigits(timeBean.months * 360);
            lblMonths.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.weeks); degrees = GetUpToNDigits(timeBean.weeks * 360);
            lblWeeks.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            // Calendar Days & Hours & Minutes & Seconds
            cells = GetUpToNDigits(timeBean.calendarDays); degrees = GetUpToNDigits(timeBean.calendarDays * 360);
            lblCDays.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.calendarHours); degrees = GetUpToNDigits(timeBean.calendarHours * 360);
            lblHours.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.calendarMinutes); degrees = GetUpToNDigits(timeBean.calendarMinutes * 360);
            lblMinutes.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.calendarSeconds); degrees = GetUpToNDigits(timeBean.calendarSeconds * 360);
            lblSeconds.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            // Trading Days & Hours & Minutes & Seconds
            cells = GetUpToNDigits(timeBean.tradingDays); degrees = GetUpToNDigits(timeBean.tradingDays * 360);
            lblTDays.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.tradingHours); degrees = GetUpToNDigits(timeBean.tradingHours * 360);
            lblTHours.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.tradingMinutes); degrees = GetUpToNDigits(timeBean.tradingMinutes * 360);
            lblTMinutes.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.tradingSeconds); degrees = GetUpToNDigits(timeBean.tradingSeconds * 360);
            lblTSeconds.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";
        }

        private void SetupTimeBean(TimeBean timeBean, double number)
        {
            if (stockMarketToolStripMenuItem.Checked)
                timeBean.SetStockMarket();
            else
                timeBean.SetForexMarket(); 

            if (rBtnYear.Checked) timeBean.years = number;
            if (rBtnMonth.Checked) timeBean.months = number;
            if (rBtnWeek.Checked) timeBean.weeks = number;
            if (rBtnCDays.Checked) timeBean.calendarDays = number;
            if (rBtnTDays.Checked) timeBean.tradingDays = number;
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

        private void rBtnYear_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void rBtnMonth_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void rBtnWeek_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void rBtnCDays_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void rBtnTDays_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void rBtnHour_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void radioButton2D_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
            CalculateHMS();
        }

        private void radioButton3D_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
            CalculateHMS();
        }

        private void radioButton4D_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
            CalculateHMS();
        }

        private void radioButtonFull_CheckedChanged(object sender, EventArgs e)
        {
            Calculate();
            CalculateHMS();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            try
            {
                double number = Convert.ToDouble(txtBoxInput.Text);
                if (number - 1 > 0) txtBoxInput.Text = "" + --number;
                Calculate();
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            try
            {
                double number = Convert.ToDouble(txtBoxInput.Text);
                if (number + 1 < Double.MaxValue) txtBoxInput.Text = "" + ++number;
                Calculate();
            }
            catch (Exception e1)
            {
                throw e1;
            }

        }
        
        //
        // HMS section
        //


        private void bHMScalculate_Click(object sender, EventArgs e)
        {
            CalculateHMS();
        }

        private void CalculateHMS()
        {
            try
            {
                long number = Convert.ToInt64(tHMSnumber.Text);
                if (number > 0)
                {
                    TimeBean timeBean = new TimeBean();
                    SetupTimeBeanHMS(timeBean, number);
                    timeConverter.MakeConversion(timeBean);
                    DisplayResultsHMS(timeBean);
                }
            } catch (Exception e)
            {
                throw e;
            }
        }

        private void SetupTimeBeanHMS(TimeBean timeBean, long number)
        {
            if (stockMarketToolStripMenuItem.Checked)
                timeBean.SetStockMarket();
            else
                timeBean.SetForexMarket();

            if (rHours.Checked) timeBean.calendarHours = number;
            if (rMinutes.Checked) timeBean.calendarMinutes = number;
            if (rSeconds.Checked) timeBean.calendarSeconds = number;
        }

        private void DisplayResultsHMS(TimeBean timeBean)
        {
            string Format = (radioButtonFull.Checked) ? "{0:0.00}" : "{0:0}";
            double cells, degrees;
            cells = GetUpToNDigits(timeBean.years); degrees = GetUpToNDigits(timeBean.years * 360);
            lblHMSyears.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.months); degrees = GetUpToNDigits(timeBean.months * 360);
            lblHMSmonths.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.weeks); degrees = GetUpToNDigits(timeBean.weeks * 360);
            lblHMSweeks.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.calendarDays); degrees = GetUpToNDigits(timeBean.calendarDays * 360);
            lblHMSdays.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.calendarHours); degrees = GetUpToNDigits(timeBean.calendarHours * 360);
            lblHMShours.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.calendarMinutes); degrees = GetUpToNDigits(timeBean.calendarMinutes * 360);
            lblHMSminutes.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";

            cells = GetUpToNDigits(timeBean.calendarSeconds); degrees = GetUpToNDigits(timeBean.calendarSeconds * 360);
            lblHMSseconds.Text = String.Format(Format, cells) + " [" + String.Format(Format, degrees) + "]";
        }

        private void rbCalendar_CheckedChanged(object sender, EventArgs e)
        {
            CalculateHMS();
        }

        private void rbTrading_CheckedChanged(object sender, EventArgs e)
        {
            CalculateHMS();
        }

        private void rMinutes_CheckedChanged(object sender, EventArgs e)
        {
            CalculateHMS();
        }

        private void rSeconds_CheckedChanged(object sender, EventArgs e)
        {
            CalculateHMS();
        }

        private void rHours_CheckedChanged(object sender, EventArgs e)
        {
            CalculateHMS();
        }

        // T <-> C section
        private void buttonTCHoursCalculate_Click(object sender, EventArgs e)
        {
            int Hours = 0;
            bool IsValid = true;
            try
            {
                Hours = Convert.ToInt32(textBoxTradingHours.Text);
            } 
            catch
            {
                IsValid = false;
            }

            if (IsValid)
            {
                int CalendarDays = (Hours / 24);
                labelCalendarTime.Text = CalendarDays + "d ";

                double CalendarHours = (Hours / 24.0 - CalendarDays) * 24;
                labelCalendarTime.Text += String.Format("{0:0}", CalendarHours) + "h";

                double CalendarHoursPart = CalendarDays + CalendarHours / 24;
                labelCalendarDaysPart.Text = String.Format("{0:0.00}", CalendarHoursPart);
            }

        }

 

        private void btnUpHMS_Click(object sender, EventArgs e)
        {
            long n = Convert.ToInt64(tHMSnumber.Text);
            if ((n > 0) && (n * 10 > 0)) // when overflow it becames negative, so if n*10 still positve it can be increased
                tHMSnumber.Text = "" + n * 10;

            CalculateHMS();
        }

        private void btnDownHMS_Click(object sender, EventArgs e)
        {
            long n = Convert.ToInt64(tHMSnumber.Text);
            if ((n > 0) && (n / 10 > 0))
                // if dividing will lead to loosing digit then do not divide
                if ((n % 10) == 0)
                    tHMSnumber.Text = "" + n / 10;

            CalculateHMS();
        }

        private void btnMinusHMS_Click(object sender, EventArgs e)
        {
            try
            {
                double number = Convert.ToDouble(tHMSnumber.Text);
                if (number - 1 > 0) tHMSnumber.Text = "" + --number;
                CalculateHMS();
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }

        private void btnPlusHMS_Click(object sender, EventArgs e)
        {
            try
            {
                double number = Convert.ToDouble(tHMSnumber.Text);
                if (number + 1 < Double.MaxValue) tHMSnumber.Text = "" + ++number;
                CalculateHMS();
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }

        private void forexAndFuturesMarketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!forexAndFuturesMarketToolStripMenuItem.Checked)
            {
                forexAndFuturesMarketToolStripMenuItem.Checked = true;
                stockMarketToolStripMenuItem.Checked = false;
            }

        }

        private void stockMarketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!stockMarketToolStripMenuItem.Checked)
            {
                stockMarketToolStripMenuItem.Checked = true;
                forexAndFuturesMarketToolStripMenuItem.Checked = false;
            }
        }
    }
}
