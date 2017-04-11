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
using System.Windows.Resources;
using System.Windows.Shapes;
using ComputerStoreCore;
using CSCVoid = ComputerStoreCore.Void;
using Microsoft.Win32;

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool in_log = false;

        private string curr_command;
  
        private string users_file = "./users.txt";

        private string goods_file = "./goods.txt";

        private Store store;

        private Customer customer;

        private CSCVoid voidOut;

        private Random r = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Uri resourceUri = new Uri("img/shoppingcart_2.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;
            shopingcart.Background = brush;
            scviewer.ScrollToBottom();
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

            store.Goods = goods_parse();
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

        private void console_print(Basket basket)
        {
            for (int i = 0; i < basket.Count; ++i)
                interpreter.Text += Environment.NewLine + basket[i].Item1;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            store.OnQuit(customer, out voidOut);
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "Text (*.TXT)|*.TXT" + "|All files (*.*)|*.* ";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            if (myDialog.ShowDialog() == true)
            {
                path = myDialog.FileName;
            }
            if (path != "")
            {
                test_interpreter(path);
            }
            else
            {
                console_print("Bad test!");
            }
        }

        private void test_interpreter(string path)
        {
            List<List<string>> commands =
                    new List<List<string>>(File.ReadAllLines(path).Select((s) => s.Split(' ').ToList()));
            foreach(var command in commands)
            {
                switch(command[0])
                {
                    case "log_in":
                        log_in_Click(null, null);
                        nick.Text = command[1];
                        pass.Password = command[2];
                        ok_Click(null, null);
                        break;

                    case "add":
                        if (!in_log)
                        {
                            console_print("Not in log!");
                        }
                        else
                        {
                            goods.SelectedIndex = r.Next(0, goods.Items.Count);
                            add_Click(null, null);
                        }
                        break;

                    case "buy":
                        if (!in_log)
                        {
                            console_print("Not in log!");
                        }
                        else
                        {
                            buy_Click(null, null);
                        }
                        break;

                    case "log_out":
                        if (!in_log)
                        {
                            console_print("Not in log!");
                        }
                        else
                        {
                            log_out_Click(null, null);
                        }
                        break;

                }
            }

            name.IsEnabled = false;
            nick.IsEnabled = false;
            pass.IsEnabled = false;
            ok.IsEnabled = false;
            clear.IsEnabled = false;
        }

        private void log_in_Click(object sender, RoutedEventArgs e)
        {
            curr_command = "log_in";
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
            var users = users_parce();
            if (curr_command == "register")
            {
                if (!users.ContainsKey(curr_nick))
                {
                    curr_name = name.Text;
                    add_user(curr_name, curr_nick, curr_pass);
                    in_log = true;
                }
            }
            else
            {
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
                shopingcart.IsEnabled = true;
                console_print("Hi, " + curr_name);
                name.Text = curr_name;
                goods.ItemsSource = new List<string>(File.ReadAllLines(goods_file));

                if (curr_command == "log_in")
                    store.OnAuthorization(Tuple.Create(curr_name, curr_nick), out customer);
                else
                    store.OnRegistration(Tuple.Create(curr_name, curr_nick), out customer);
            }
            else
            {
                name.Text = "Name";
                if (curr_command == "register")
                {
                    name.Clear();
                }
                nick.Clear();
                pass.Clear();
                console_print("Try Again!");
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
            curr_command = "register";
            name.IsEnabled = true;
            nick.IsEnabled = true;
            pass.IsEnabled = true;
            name.Clear();
            nick.Clear();
            pass.Clear();
            ok.IsEnabled = true;
            clear.IsEnabled = true;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            int ind = goods.SelectedIndex;
            if (ind != -1)
            {
                var pair = store.Goods.ElementAt(ind);
                store.OnAddToBasket(Tuple.Create(Tuple.Create(pair.Key, pair.Value),
                            customer.GoodsBasket), out voidOut);
                console_print(goods.Items[ind] + " Added!");
            }
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            shopingcart.IsEnabled = false;
            store.OnPurchase(customer.GoodsBasketReadOnly, out voidOut);
            int summ = 0;
            for (var i = 0;  i < customer.GoodsBasketReadOnly.Count; i++)
            {
                summ += customer.GoodsBasketReadOnly[i].Item2;
            }
            console_print("Purchase for " + summ + " rubles is committed!");
            customer.GoodsBasket.Clear();
        }

        private void log_out_Click(object sender, RoutedEventArgs e)
        {
            in_log = false;
            console_print("Log out!");
            console_print("Welcome to shop!");
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
            shopingcart.IsEnabled = false;
            log_out.IsEnabled = false;
            update_user(customer.Username, store.CurrentAccumulation);
            store.OnLogOut(customer, out voidOut);
        }        

        private void shopingcart_Click(object sender, RoutedEventArgs e)
        {
            if(customer.GoodsBasket.Count != 0)
                console_print(customer.GoodsBasket);
        }

        private void update_user(string nick, int acc)
        { 
            var f = File.ReadAllLines(users_file);
            for (var i = 0; i < f.Length; i++) 
            {
                if(f[i].Split(' ')[1] == nick)
                {
                    f[i] = f[i].Split(' ')[0] + " " + f[i].Split(' ')[1] + " " + f[i].Split(' ')[2] + " " + (int.Parse(f[i].Split(' ')[3]) + store.CurrentAccumulation).ToString();
                }
            }
            File.WriteAllLines(users_file, f);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
