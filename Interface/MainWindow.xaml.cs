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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool in_log = false;

        private string users_file = "./users.txt";

        private string goods_file = "./goods.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        private Dictionary<string, int> goods_parse()
        {
        
            List<string> name =
                new List<string>(File.ReadAllLines(users_file).Select((s) => s.Split(' ')[0]));

            List<int> price =
                new List<int>(File.ReadAllLines(users_file).Select<string, int>((s) => int.Parse(s.Split(' ')[1])));

            Dictionary<string, int> res = new Dictionary<string, int>();

            for (int i = 0; i < name.Count; i++)
            {
                res.Add(name[i], price[i]);
            }
            return res;
        }

        private Dictionary<string, Tuple<string, string, int>> users_parce()
        {

            List<string> users =
                            new List<string>(File.ReadAllLines(users_file).Select((s) => s.Split(' ')[0]));

            List<string> pass =
                           new List<string>(File.ReadAllLines(users_file).Select((s) => s.Split(' ')[0]));

            List<string> names =
                           new List<string>(File.ReadAllLines(users_file).Select((s) => s.Split(' ')[0]));

            List<int> acc =
                new List<int>(File.ReadAllLines(users_file).Select<string, int>((s) => int.Parse(s.Split(' ')[1])));



            Dictionary<string, Tuple<string, string, int>> res = new Dictionary<string, Tuple<string, string, int>>();

            for (int i = 0; i < users.Count; i++)
            {
                res.Add(users[i], new Tuple<string, string, int>(pass[i], names[i], acc[i]));
            }
            return res;
        }



        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void log_in_Click(object sender, RoutedEventArgs e)
        {
            nick.IsEnabled = true;
            pass.IsEnabled = true;
            nick.Clear();
            pass.Clear();
            ok.IsEnabled = true;
            clear.IsEnabled = true;

        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if(!in_log)
            {
                string name = nick.Text;
                string password = pass.Text;

            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
