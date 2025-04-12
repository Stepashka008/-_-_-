using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_2._0
{
    public partial class Form1 : Form
    {
        double pred;
        Programm programm;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "50000";
            textBox2.Text = "15";
            textBox3.Text = "12";
            textBox4.Text = "9";
            textBox5.Text = "11";
            textBox6.Text = "8";
            textBox7.Text = "6";
            comboBox2.Text = "80000";
        }
        private void button1_Click(object sender, EventArgs e) // Расчёт инвестирования
        {
            List<double> percents = new List<double>(); // Список процентов
            List<TextBox> textBoxList = this.Controls.OfType<TextBox>().ToList(); // Собираем все checkbox
            double sum = double.Parse(textBoxList[textBoxList.Count - 1].Text); // Берем сумму
            textBoxList.RemoveAt(textBoxList.Count - 1); // Удаляем сумму из процентов
            foreach (var textBox in textBoxList) { percents.Add(int.Parse(textBox.Text) / 100.0); } // Перевод из списка textbox в список процентов
            percents.Reverse();
            programm = new Programm();
            comboBox1.Text = $"{programm.main(sum, percents)}";
            pred = double.Parse(comboBox1.Text);
            Watching();
        }
        private void button2_Click(object sender, EventArgs e) // Расчёт дополнительных инвестированных средств
        {
            comboBox1.Text = $"{programm.dopInvesting(double.Parse(comboBox2.Text))}";
            double nast = double.Parse(comboBox1.Text);
            comboBox3.Text = $"{nast - pred}";
            Watching();
        }
        private void Watching() // Показ куда и сколько нужно инвестировать
        {
            List<MaskedTextBox> ctb = this.Controls.OfType<MaskedTextBox>().ToList();
            ctb.Reverse();
            List<string> vlog = new List<string>();
            foreach (var bum in programm.B)
            {
                vlog.Add(bum.SUM.ToString());
            }
            foreach (var bum in programm.B)
            {
                vlog.Add((bum.SUM * bum.PERCENT).ToString());
            }

            for (int i = 0; i < vlog.Count / 2; i++)
            {
                ctb[i].Text = vlog[i];
            }

            for (int i = vlog.Count / 2; i < vlog.Count; i++)
            {
                ctb[i].Text = vlog[i];
            }
        }
    }
}
