using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace personal_week8_9
{
    public partial class Form : System.Windows.Forms.Form
    {
        private Dictionary<String, double> dic = new Dictionary<String, double>();
        private double allWallSize; 
        private double allFloorSize; 
        private String resultText = "";

        public Form()
        {
            InitializeComponent();
            LoadDataValues();
        }
        private void LoadAccessibleSizeOfRepair()
        {
            if (isValid(textBox1.Text) && textBox1.Text != "" &&
                isValid(textBox2.Text) && textBox2.Text != "" &&
                isValid(textBox11.Text) && textBox11.Text != "" &&
                isValid(textBox4.Text) && textBox4.Text != "" &&
                isValid(textBox6.Text) && textBox6.Text != "" &&
                isValid(textBox3.Text) && textBox3.Text != "" &&
                dic.ContainsKey(comboBox12.Text) && dic.ContainsKey(comboBox13.Text))
            {
                double doorSize;
                double windowSize;
                double wallWidth = Convert.ToDouble(textBox2.Text);
                double wallHeight = Convert.ToDouble(textBox11.Text);
                dic.TryGetValue(comboBox12.Text, out doorSize);
                dic.TryGetValue(comboBox13.Text, out windowSize);
                allWallSize = wallWidth * wallHeight * Convert.ToDouble(textBox1.Text)
                    - Convert.ToDouble(textBox4.Text) * doorSize - Convert.ToDouble(textBox6.Text) * windowSize;
                allFloorSize = Convert.ToDouble(textBox3.Text);
            }
            else
            {
                MessageBox.Show( "Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
        }
        private bool isValid(String str)
        {
            char[] chArr = str.ToCharArray();
            for (int i = 0; i < chArr.Length; i++)
            {
                if (!(chArr[i] >= '0' && chArr[i] <= '9') && chArr[i] != ',')
                {
                    return false;
                }
            }
            return true;
        }

        private void LoadDataValues()
        {
            //размер плитки
            dic.Add("200х250 мм", 0.05);
            dic.Add("200х300 мм", 0.06);
            dic.Add("120х240 мм", 0.028);
            dic.Add("150х150 мм", 0.0225);


            //ширина линолиума
            dic.Add("2*18 м", 36);
            dic.Add("2,5*18 м", 45);
            dic.Add("3*18 м", 54);
            dic.Add("3,5*18 м", 63);
            dic.Add("4*18 м", 72);
            
            //размер обоев
            dic.Add("0,53х10,05 м", 5.32);
            dic.Add("1,06х10,05 м", 10.653);

            //размер ламината
            dic.Add("100*1200 мм", 0.12);
            dic.Add("120*1300 мм", 0.156);
            dic.Add("300*1380 мм", 0.414);
            
            //ламинат - укладка
            dic.Add("Прямой способ", 1);
            dic.Add("Диагональная укладка", 1.2);
            dic.Add("Беспороговая укладка", 1.4);

            
            //размер паркета
            dic.Add("200х50 мм", 0.01);
            dic.Add("210х70 мм", 0.014);
            dic.Add("900х90 мм", 0.081);
            //паркет - укладка
            dic.Add("Елочка", 1.4);
            dic.Add("Палуба", 1);
            dic.Add("Квадраты", 1.1);
            dic.Add("Ромбы", 1.2);

            //размер дверных проемов
            dic.Add("2,071*0,67 м", 1.38);
            dic.Add("2,071*0,77 м", 1.48); 
            dic.Add("2,071*0,97 м", 1.52); 
            dic.Add("2,071*0,87 м", 1.49);

            //размер оконных проемов
            dic.Add("500*500 мм", 0.25);
            dic.Add("600*600 мм", 0.36);
            dic.Add("600*900 мм", 0.54);
            dic.Add("1200*600 мм", 0.72);
            dic.Add("1200*900 мм", 1.08);
            dic.Add("1350*600 мм", 0.81);
            dic.Add("1350*900 мм", 1.251);
            dic.Add("1500*900 мм", 1.354);

        }

        private void countButton(object sender, EventArgs e)
        {
            allFloorSize = 0;
            allWallSize = 0;
            resultLabel.Text = "";
            resultText = "";
            LoadAccessibleSizeOfRepair();
            showResult();
        }
        private void showResult()
        {
            if(dic.ContainsKey(comboBox2.Text) &&
                dic.ContainsKey(comboBox3.Text) &&
                dic.ContainsKey(comboBox9.Text) &&
                dic.ContainsKey(comboBox5.Text) &&
                dic.ContainsKey(comboBox14.Text) &&
                dic.ContainsKey(comboBox7.Text) &&
                dic.ContainsKey(comboBox11.Text))
            {
               
                if(textBox8.Text != "" && isValid(textBox8.Text) && allWallSize != 0)
                {
                    if (allWallSize < Convert.ToDouble(textBox8.Text))
                    {
                        MessageBox.Show(
                    "Размер заданной отделки для обоев больше размера площади для ремонта стен",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                        return;
                    }
                    double unit;
                    dic.TryGetValue(comboBox9.Text, out unit);
                    double result = Math.Round(allWallSize / unit);
                    if (allWallSize / unit > result) result++;
                    resultText += "Количество рулонов обоев на стену " +  result + Environment.NewLine; 
                    
                }
            
                if (allFloorSize != 0)
                {    
                    if(textBox5.Text != "" && isValid(textBox5.Text) &&  Convert.ToDouble(textBox5.Text) != 0.0 )
                    {
                        double unit;
                        double currentSize = Convert.ToDouble(textBox5.Text);
                        dic.TryGetValue(comboBox2.Text, out unit);
                        double result = Math.Round(currentSize / unit);
                        if (currentSize / unit > result) result++;
                        resultText += "Количество плиток на пол " + result + Environment.NewLine;
                        allFloorSize -= currentSize;
                    }
                    if (textBox7.Text != "" && isValid(textBox7.Text)   && Convert.ToDouble(textBox7.Text) != 0.0 )
                    {
                        double unit;
                        double currentSize = Convert.ToDouble(textBox7.Text);
                        dic.TryGetValue(comboBox3.Text, out unit);
                        double result = Math.Round(currentSize / unit);
                        if (currentSize / unit > result) result++;
                        resultText += "Количество рулонов линолиума на пол " + result + Environment.NewLine;
                        allFloorSize -= currentSize;
                    }
                    if (textBox9.Text != "" && isValid(textBox9.Text) &&  Convert.ToDouble(textBox9.Text) != 0.0 )
                    {
                        double unit;
                        double excess;
                        double currentSize = Convert.ToDouble(textBox9.Text);
                        dic.TryGetValue(comboBox5.Text, out unit);
                        dic.TryGetValue(comboBox14.Text, out excess);
                        double result = Math.Round(currentSize / unit * excess);
                        if (currentSize / unit > result) result++;
                        resultText += "Количество штук ламината на пол " + result + Environment.NewLine;
                        allFloorSize -= currentSize;
                    }
                    if (textBox10.Text != "" && isValid(textBox10.Text)  && Convert.ToDouble(textBox10.Text) != 0.0 )
                    {
                        double unit;
                        double excess;
                        double currentSize = Convert.ToDouble(textBox10.Text);
                        dic.TryGetValue(comboBox7.Text, out unit);
                        dic.TryGetValue(comboBox11.Text, out excess);
                        double result = Math.Round(currentSize / unit * excess);
                        if (currentSize / unit > result) result++;
                        resultText += "Количество штук паркета на пол " + result + Environment.NewLine;
                        allFloorSize -= currentSize;
                    }
                    if(allFloorSize < 0)
                    {
                        
                            MessageBox.Show(
                     "Размер заданной отделки для пола больше размера площади для ремонта пола",
                     "Ошибка",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1,
                     MessageBoxOptions.DefaultDesktopOnly);
                            return;
                        
                    }
                    textBox12.Text = Convert.ToString(allFloorSize);
                    resultLabel.Text = resultText;
                    resultLabel.Visible = true;

                }
            } else
            {
                MessageBox.Show(
                                     "Неверные данные",
                                     "Ошибка",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error,
                                     MessageBoxDefaultButton.Button1,
                                     MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
        }

        private void resetButton(object sender, EventArgs e)
        {
            resultLabel.Visible = false;
            resultText = "";
            resultLabel.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
