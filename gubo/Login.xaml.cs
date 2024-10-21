using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KretaKlon.gubo
{
    public partial class Login
    {
        List<List<string>> lista = new List<List<string>>();
        public string Tanulo { get; set; }
        public Login()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            
            var lines = File.ReadAllLines("studentsInformation.csv");
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                List<string> list = new List<string> { parts[0], parts[2] };
                lista.Add(list);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in lista)
            {
                if (Tanulo == item[0])
                {
                    if (item[1] == textBox.Text)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hibás jelszó");
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Close();

        }
    }
}
