using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using System.Threading;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //создаем переменную типа Iwebdriver
        IWebDriver Browser;
        //Адрес проживаниия
        string Adr = "ул Премудрова, д. 10корп.1, кв.58";//потребуется для доп проверки по адресу
        public Form1()
        {
            InitializeComponent();
        }

         
        private void button2_Click(object sender, EventArgs e)
        {
            // Привет от Макса! 
            // проверка на ввод данных в поля
            if ((textBox1.Text != String.Empty) && (textBox2.Text != String.Empty) && (textBox3.Text != String.Empty))
            {
                //запускаем браузеа и заходим на страничку.
                Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
                Browser.Navigate().GoToUrl("https://nn.tns-e.ru");
                //ищем textbox по имени класса для ввода  и пихаем туда значение из нашего текстбокса
                IWebElement SearchElement = Browser.FindElement(By.ClassName("ls-input"));
                SearchElement.SendKeys(textBox1.Text);
                //ищем кнопульку через css выражение и жмем на нее 
                SearchElement = Browser.FindElement(By.CssSelector(".butt .btn"));
                SearchElement.Click();
                //проверка верности адреса проживания
                //Надо доделать проверку на правильный адрес. Не получилось найти и сравнить выводимый адрес на страничке
                Thread.Sleep(2000);
                SearchElement = Browser.FindElement(By.ClassName("s-desc light cust-class-service-3"));
                if (SearchElement.Text != Adr)
                {
                    MessageBox.Show("Адрес не верен");
                    Browser.Close();
                }
                //подтверждаем адрес по нажатия 
                Thread.Sleep(1000); //без задержки почему то не срабатывало
                SearchElement = Browser.FindElement(By.CssSelector(".butt .btn"));
                SearchElement.Click();

                SearchElement = Browser.FindElement(By.Name("peak"));
                SearchElement.SendKeys(textBox2.Text);
                SearchElement = Browser.FindElement(By.Name("night"));
                SearchElement.SendKeys(textBox3.Text);

            }
            else
            {
             MessageBox.Show("Необходимо ввести все данные!", "Ошибка ввода данных", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            //произвети обработку кнопки cancel
            }
        }
    }
}
