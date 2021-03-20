using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Investment
{
    public partial class Investment : Form
    {
        public Investment()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidData())
                {
                    decimal monthlyInvestment = Convert.ToDecimal(txtMonthlyInvestment.Text);
                    decimal yearlyInterestRate = Convert.ToDecimal(txtInterestRate.Text);
                    int years = Convert.ToInt32(txtYears.Text);

                    int months = years * 12;
                    decimal monthlyInterestRate = yearlyInterestRate / 12 / 100;

                    decimal futureValue = this.CalculateFutureValue(monthlyInvestment, monthlyInterestRate, months);
                    txtFutureValue.Text = futureValue.ToString("c");
                    txtMonthlyInvestment.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                    ex.GetType().ToString() + "\n" +
                    ex.StackTrace, "Exception");
            }
        }

        public bool ValidData()
        {
            return
               RequiredField(txtMonthlyInvestment, "Monthly Investment") &&
               IsDecimal(txtMonthlyInvestment, "Monthly Investment") &&
               MinMax(txtMonthlyInvestment, "Montly Investment", 1, 1000) &&

               RequiredField(txtInterestRate, "Interest Rate") &&
               IsDecimal(txtInterestRate, "Interest Rate") &&
               MinMax(txtInterestRate, "Interest Rate", 1, 20) &&

               RequiredField(txtYears, "Years") &&
               IsInt32(txtYears, "Years") &&
               MinMax(txtYears, "Years", 1, 40);
        }

        public bool RequiredField(TextBox textBox, string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field", "Entry Error");
                textBox.Focus();
                return false;


            }
            return true;
        }

        public bool IsDecimal(TextBox textBox, string name)
        {
            decimal number = 0m;
            if (Decimal.TryParse(textBox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(name + " must be a decimal value.", "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        public bool IsInt32(TextBox textBox, string name)
        {
            int number = 0;
            if (Int32.TryParse(textBox.Text, out number))
            {
                return true;

            }
            else
            {
                MessageBox.Show(name + " must be an integer.", "Entry Error");
                textBox.Focus();
                return false;

            }
        }

        public bool MinMax(TextBox textBox, string name,
            decimal min, decimal max)
        {
            decimal number = Convert.ToDecimal(textBox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show(name + " must be between" + min
                    + " and " + max + ".", "Entry Error");
                textBox.Focus();
                return false;

            }
            return true;
        }

        private decimal CalculateFutureValue(decimal monthlyInvestment,
            decimal monthlyInterestRate, int months)
        {
            decimal futureValue = 0m;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + monthlyInvestment)
                            * (1 + monthlyInterestRate);
            }
            return futureValue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearFutureValue(object sender, EventArgs e)
        {
            txtFutureValue.Text = "";
        }
        private void ClearMonthlyInvestment(object sender, EventArgs e)
        {
            txtMonthlyInvestment.Text = "";
        }
        private void ClearInterestRate(object sender, EventArgs e)
        {
            txtInterestRate.Text = "";
        }
        private void ClearYears(object sender, EventArgs e)
        {
            txtYears.Text = "";
        }
        private void Investment_DoubleClick(object sender, EventArgs e)
        {
            txtMonthlyInvestment.Text = "";
            txtInterestRate.Text = "";
            txtYears.Text = "";
            txtFutureValue.Text = "";
        }

        private void txtInterestRate_DoubleClick(object sender, EventArgs e)
        {
            txtInterestRate.Text = "Enter the Interest rate";
        }
    }
}
