using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FunctionParseTest.Test;
using Com.Rzgshome.Common.Function;

namespace FunctionParseTest
{
    public partial class Form1 : Form
    {
        private readonly FunctionTestData testData = FunctionTestData.GetInstance();
        public Form1()
        {
            InitializeComponent();
            testData.StaticNumber = 5;
            testData.PropertyName = "A";
            testData.PropertyValue = "ABC";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String result = FunctionTest.GetResult();
            textBox1.Text = result;
        }

        private void staticField_TextChanged(object sender, EventArgs e)
        {
            testData.StaticNumber = decimal.Parse(staticField.Text);
        }

        private void fieldName_TextChanged(object sender, EventArgs e)
        {
            testData.PropertyName = fieldName.Text;
        }

        private void fieldValue_TextChanged(object sender, EventArgs e)
        {
            testData.PropertyValue = fieldValue.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FunctionParseTestMain main = new FunctionParseTestMain();
            if (main.Compile(parseString.Text))
            {
                Object result = main.RunFunction(testData);
                if (result != null)
                {
                    textBox1.Text = result.ToString();
                }
                else
                {
                    textBox1.Text = "";
                }
            }
            else
            {
                textBox1.Text = "Compile Error:" + parseString.Text;
            }
        }
    }
}
