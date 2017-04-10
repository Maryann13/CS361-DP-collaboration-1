﻿using System;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using ComputerStoreCore;
using CSCVoid = ComputerStoreCore.Void;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool in_log = false;

        private string curr_comand;
  
        private string users_file = "./users.txt";

        private string goods_file = "./goods.txt";

        private Store store;

        private Customer customer;

        private CSCVoid voidOut;

        public MainWindow()
        {
            InitializeComponent();
            Uri resourceUri = new Uri("img/shoppingcart_2.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;
            shopingcart.Background = brush;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StoreSystem storeSystem = new StoreSystem();

            StoreFactory sFactory = new StoreFactory();
            DiscountCardsFactory dcFactory = new DiscountCardsFactory();
            CustomerFactory cFactory = new CustomerFactory();

            StandardStoreBuilder storeBuilder = new StandardStoreBuilder();

            store = storeSystem.CreateStore
                    (storeBuilder, sFactory, dcFactory, cFactory);
        }

        private Dictionary<string, int> goods_parse()
        {

            List<string> names =
                new List<string>(File.ReadAllLines(goods_file).Select((s) => s.Split(' ')[0])).ToList();

            List<int> prices =
                new List<int>(File.ReadAllLines(goods_file).Select<string, int>
                                        ((s) => int.Parse(s.Split(' ')[1]))).ToList();

            Dictionary<string, int> res = new Dictionary<string, int>();

            for (int i = 0; i < names.Count; i++)
            {
                res.Add(names[i], prices[i]);
            }
            return res;
        }

        private Dictionary<string, Tuple<string, string, int>> users_parce()
        {

            List<string> names =
                    new List<string>(File.ReadAllLines(users_file).Select
                            ((s) => s.Split(' ')[0])).ToList();

            List<string> nicks =
                    new List<string>(File.ReadAllLines(users_file).Select
                            ((s) => s.Split(' ')[1])).ToList();

            List<string> pass =
                    new List<string>(File.ReadAllLines(users_file).Select
                            ((s) => s.Split(' ')[2])).ToList();

            List<int> acc = 
                    new List<int>(File.ReadAllLines(users_file).Select<string, int>
                            ((s) => int.Parse(s.Split(' ')[3]))).ToList();

            Dictionary<string, Tuple<string, string, int>> res =
                        new Dictionary<string, Tuple<string, string, int>>();

            for (int i = 0; i < nicks.Count; i++)
            {
                res.Add(nicks[i], new Tuple<string, string, int>(names[i], pass[i], acc[i]));
            }
            return res;
        }

        private void add_user(string name, string nick, string pass)
        {
            using (StreamWriter sw = File.AppendText(users_file))
            {
                sw.Write(Environment.NewLine + name + " " + nick + " " + pass + " 0");
            }
        }

        private void add_good(string name, int price)
        {
            using (StreamWriter sw = File.AppendText(goods_file))
            {
                sw.Write(Environment.NewLine + name + " " +price.ToString());
            }
        }

        private void console_print(string s)
        {
            interpreter.Text += Environment.NewLine + s;
        }

        private void console_print(IEnumerable<string> ss)
        {
            foreach(string s in ss)
                interpreter.Text += Environment.NewLine + s;
        }


        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }

        private void log_in_Click(object sender, RoutedEventArgs e)
        {
            curr_comand = "log_in";
            nick.IsEnabled = true;
            pass.IsEnabled = true;
            nick.Clear();
            pass.Clear();
            ok.IsEnabled = true;
            clear.IsEnabled = true;

        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            string curr_nick = nick.Text;
            string curr_pass = pass.Password;
            string curr_name = "";
            if (curr_comand == "register")
            {
                curr_name = name.Text;
                add_user(curr_name, curr_nick, curr_pass);
                in_log = true;
            }
            else
            {
                var users = users_parce();
                if (users.ContainsKey(curr_nick))
                    if (users[curr_nick].Item2 == curr_pass)
                    {
                        in_log = true;
                        curr_name = users[curr_nick].Item1;
                    }
            }
            if (in_log)
            {
                name.IsEnabled = false;
                nick.IsEnabled = false;
                pass.IsEnabled = false;
                ok.IsEnabled = false;
                clear.IsEnabled = false;
                register.IsEnabled = false;
                log_in.IsEnabled = false;
                add.IsEnabled = true;
                buy.IsEnabled = true;
                log_out.IsEnabled = true;
                console_print("Hi, " + curr_name);
                name.Text = curr_name;
                goods.ItemsSource = new List<string>(goods_parse().Keys);
                //todo
            }
            else
            {
                name.Text = "Name";
                nick.Clear();
                pass.Clear();
                console_print("Try Agane!");
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            name.Clear();
            nick.Clear();
            pass.Clear();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            curr_comand = "register";
            name.IsEnabled = true;
            nick.IsEnabled = true;
            pass.IsEnabled = true;
            name.Clear();
            nick.Clear();
            pass.Clear();
            ok.IsEnabled = true;
            clear.IsEnabled = true;
            //todo
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(goods.SelectedItem != null)
            {
                //todo
            }
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }

        private void log_out_Click(object sender, RoutedEventArgs e)
        {
            in_log = false;
            interpreter.Text = "Welcome to shop!";
            name.Text = "Name";
            nick.Text = "Nickname";
            pass.Clear();
            goods.ItemsSource = "";
            name.IsEnabled = false;
            nick.IsEnabled = false;
            pass.IsEnabled = false;
            ok.IsEnabled = false;
            clear.IsEnabled = false;
            register.IsEnabled = true;
            log_in.IsEnabled = true;
            add.IsEnabled = false;
            buy.IsEnabled = false;
            log_out.IsEnabled = false;

            store.OnLogOut(customer, out voidOut);
        }        

        private void shopingcart_Click(object sender, RoutedEventArgs e)
        {
            List<string> basket = new List<string>();
            basket.Add("lol");
            basket.Add("kek");
            basket.Add("azaza");
            console_print(basket);
        }
    }
}
