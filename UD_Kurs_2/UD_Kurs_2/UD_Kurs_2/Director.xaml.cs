using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Globalization;

namespace UD_Kurs_2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Director : ContentPage
    {
        TcpClient tcpClient;
        Thread Thread1;
        string Chat = "";
        List<string> Prom_All_Meropriatie_String_List = new List<string>();
        List<string> All_Meropriatie_String_List = new List<string>();
        List<string> Massege = new List<string>();
        List<string> All_Client = new List<string>();
        List<string> All_Sotr = new List<string>();
        List<string> All_Group = new List<string>();
        StackLayout Vremenniy_Meropriatie = new StackLayout();
        StackLayout Dobavlenie_Meropriatia = new StackLayout();
        StackLayout Poisk_Meropriatia = new StackLayout();

        List<string> Groops_Id_List = new List<string>();
        List<string> Groops_Date_List = new List<string>();
        List<string> Groops_Time_List = new List<string>();
        List<string> All_Rasspis = new List<string>();
        StackLayout Vremenniy_Group = new StackLayout();
        StackLayout Vremenniy_Clnts = new StackLayout();
        StackLayout Vremenniy_Clnts_Pustoy = new StackLayout();
        StackLayout Vremenniy_Sotr = new StackLayout();
        Dictionary<Button, string> Key_Pair_Fio_Client_Button = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_Fio_Sotr_Button = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_Usluga_Button = new Dictionary<Button, string>();
        Dictionary<Entry, Button> Key_Pair_BT_EN_Sotr = new Dictionary<Entry, Button>();
        Dictionary<Button, Entry> Key_Pair_BT_EN_Sotr_Inv = new Dictionary<Button, Entry>();
        Dictionary<Button, Entry> Key_Pair_BT_EN_Usluga = new Dictionary<Button, Entry>();
        Dictionary<Button, string> Key_Pair_For_Meroproytie_Big_String_All = new Dictionary<Button, string>();
        Dictionary<string, Frame> Key_Pair_Klient_Fio_And_Frame = new Dictionary<string, Frame>();
        Dictionary<string, Button> Key_Pair_Klient_Fio_And_bt = new Dictionary<string, Button>();
        Dictionary<string, Frame> Key_Pair_Sotr_Fio_And_Frame = new Dictionary<string, Frame>();
        Dictionary<string, Button> Key_Pair_Sotr_Fio_And_bt = new Dictionary<string, Button>();
        string Fio_Sotr_For_Otpr = "";
        string Fio_Clnient_For_Otpr = "";
        string Usluga_For_Otpr = "";
        public Director(TcpClient tcpClient, List<string> Vsy_Infa)
        {
            this.tcpClient = tcpClient;
            Third_All_Meropriatie(Vsy_Infa[1]);
            Seconds_Groups(Vsy_Infa[2]);
            All_Client_Dec(Vsy_Infa[3]);
            All_Sotr_Dec(Vsy_Infa[4]);
            All_Usluga_Dec(Vsy_Infa[5]);
            All_Rasspis_Dec(Vsy_Infa[6]);
            Chat = Vsy_Infa[7];
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected override void OnAppearing()
        {
            First_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            First_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Second_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            Second_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Third_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            Third_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Fouth_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            Fouth_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Fifth_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            Fifth_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Sixth_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            Sixth_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Minus_Ugli_Na_First_Page.WidthRequest = First_page.WidthRequest;
            Box_View_On_First_Page.HeightRequest = First_page.HeightRequest * 0.1;
            Minus_Ugli_Na_Fifth_Page.WidthRequest = Fifth_page.WidthRequest;
            Box_View_On_Fifth_Page.HeightRequest = Fifth_page.HeightRequest * 0.1;
            Minus_Ugli_Na_Second_Page.WidthRequest = Second_page.WidthRequest;
            Box_View_On_Second_Page.HeightRequest = Second_page.HeightRequest * 0.1;
            Box_View_On_Third_Page.HeightRequest = Third_page.HeightRequest * 0.1;
            Minus_Ugli_Na_Third_Page.WidthRequest = Third_page.WidthRequest;
            Minus_Ugli_Na_Sixth_Page.WidthRequest = First_page.WidthRequest;
            Box_View_On_Sixth_Page.HeightRequest = First_page.HeightRequest * 0.1;

            Minus_Ugli_Na_Fouth_Page.WidthRequest = Fouth_page.WidthRequest;
            Box_View_On_Fouth_Page.HeightRequest = Fouth_page.HeightRequest * 0.1;


            //Button_Cl_Nayti_In_Stac.WidthRequest = First_page.WidthRequest*0.1;
            //Button_Cl_Nayti_In_Stac.HeightRequest = First_page.WidthRequest * 0.1;
            Decoding_And_Criate_Choldren_All_Meropriatie();
            Zapolnenie_SL_Na_Dobavlenie();
            Decoding_And_Criate_Children_Rasspis();
            Dobav_Mer_Bt.WidthRequest = Third_page.WidthRequest * 0.1;
            Dobav_Mer_Bt.HeightRequest = Third_page.WidthRequest * 0.1;
            Poisk_Mer_Bt.WidthRequest = Third_page.WidthRequest * 0.1;
            Poisk_Mer_Bt.HeightRequest = Third_page.WidthRequest * 0.1;

            Poisk_Cl_Bt.WidthRequest = Third_page.WidthRequest * 0.1;
            Poisk_Cl_Bt.HeightRequest = Third_page.WidthRequest * 0.1;

            Poisk_Sotr_Bt.WidthRequest = Third_page.WidthRequest * 0.1;
            Poisk_Sotr_Bt.HeightRequest = Third_page.WidthRequest * 0.1;

            Name_Meropr.WidthRequest = Third_page.WidthRequest * 0.8;
            Mesto_Meropr.WidthRequest = Third_page.WidthRequest * 0.8;
            Type_Meropr.WidthRequest = Third_page.WidthRequest * 0.8;
            Date_Meropr.WidthRequest = Third_page.WidthRequest * 0.4;
            Send.WidthRequest = Third_page.WidthRequest * 0.2;
            Polle_Vvoda.WidthRequest = Third_page.WidthRequest * 0.8;

            Three_Scrols_In_Rasspis.WidthRequest = Fifth_page.WidthRequest * 0.5;
            FIO_sotr.WidthRequest = Second_page.WidthRequest * 0.7;
            Type_Sotr.WidthRequest = Second_page.WidthRequest * 0.3;
            ZP_Sotr.WidthRequest = Second_page.WidthRequest * 0.4;
            Data_Rozh.WidthRequest = Second_page.WidthRequest * 0.4;
            Stazh.WidthRequest = Second_page.WidthRequest * 0.1;
            Pochta_Sotr.WidthRequest = Second_page.WidthRequest * 0.7;
            Phone_Number_Sotr.WidthRequest = Second_page.WidthRequest * 0.35;
            Login_Sotr.WidthRequest = Second_page.WidthRequest * 0.3;
            Password_Sotr.WidthRequest = Second_page.WidthRequest * 0.3;
            Psevldonim_Sotr.WidthRequest = Second_page.WidthRequest * 0.3;

            Name_Mer_Poisk.WidthRequest = Second_page.WidthRequest * 0.7;
            Min_Chislo_Uchastnikov.WidthRequest = Second_page.WidthRequest * 0.2;
            Max_Chislo_Uchastnikov.WidthRequest = Second_page.WidthRequest * 0.2;
            Min_Date.WidthRequest = Second_page.WidthRequest * 0.4;
            Max_Date.WidthRequest = Second_page.WidthRequest * 0.4;

            Min_Date.Date = DateTime.Now.AddYears(-5);
            Min_Date.TextColor = Color.Gray;
            Max_Date.Date = DateTime.Now.AddYears(5);
            Max_Date.TextColor = Color.Gray;

            Zapolnenie_Passpis();
            Create_Spisok_KLient();
            Create_Spisok_Sotr();
            Create_Spisok_Usluga();

            Criate_Dilog(Chat);
        }
        void All_Client_Dec(string Msg)
        {
            if (Msg != "F")
            {
                string Prom = "";
                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        All_Client.Add(Prom);
                        Prom = "";
                    }
                }
                All_Client.Add(Prom);
            }
        }
        void All_Usluga_Dec(string Msg)
        {
            if (Msg != "F")
            {
                string Prom = "";
                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        All_Group.Add(Prom);
                        Prom = "";
                    }
                }
                All_Group.Add(Prom);
            }
        }
        void Create_Spisok_Usluga()
        {

            for (int i = 0; i < All_Group.Count; i += 2)
            {
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };

                promezutocniy.Children.Add(new Label()
                {
                    Text = (All_Group[i]),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                });
                Button button = new Button()
                {
                    Text = "Просмотреть информацию",
                    BackgroundColor = Color.White,
                    TextColor = Color.Purple,
                    FontSize = 10,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };
                Key_Pair_Usluga_Button.Add(button, All_Group[i]);
                button.Clicked += Inf_Of_Usluga_By_Knopka;
                promezutocniy.Children.Add(button);
                Vremenniy_Group.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,
                    Content = promezutocniy,

                });
            }
            Usluga_Scroll.Content = Vremenniy_Group;
            //Vremenniy_User_Inf = StackLayout_In_Scroll_Groups;
        }

        private void Add_New_Usluga_Clicked(object sender, EventArgs e)
        {
            Stacklayout_Dobav_Usluga.IsVisible = true;
            StackLayout_In_Scroll_Usluga.IsVisible = false;
            // Vremenniy_Group.IsVisible = false;
            Name_Uslug_Dobav.WidthRequest = Sixth_page.WidthRequest * 0.6;
            Coast_Uslug_Dobav.WidthRequest = Sixth_page.WidthRequest * 0.3;
            Usluga_Scroll.Content = Stacklayout_Dobav_Usluga;
            Save_Change_Usluuga.IsVisible = true;
            Usluga_And_Strelka.IsVisible = true;
            Strelka_Usluga.Text = "<-";
            Name_Usluga.IsVisible = false;
            Add_New_Usluuga.IsVisible = false;
            Save_Change_Usluuga.IsVisible = false;
            Name_Usluga_In_Grid.Text = "Создание";
        }

        private void Name_Coast_Uslug_Dobav_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Name_Uslug_Dobav.Text != null)
            {
                Soxran_New_Usluga.IsEnabled = true;
                Soxran_New_Usluga.BackgroundColor = Color.Purple;
            }
            if (Coast_Uslug_Dobav.Text != null)
            {
                Soxran_New_Usluga.IsEnabled = true;
                Soxran_New_Usluga.BackgroundColor = Color.Purple;
            }
        }

        private void Soxran_New_Usluga_Clicked(object sender, EventArgs e)
        {
            if ((Name_Uslug_Dobav.Text != null) && ((Coast_Uslug_Dobav.Text != null) && (Int64.TryParse(Coast_Uslug_Dobav.Text, out Int64 var))))
            {
                try
                {
                    string Msg = "";
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("NU#" + Name_Uslug_Dobav.Text + "#" + Coast_Uslug_Dobav.Text);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] == 'S')
                            {
                                All_Group.Add(Name_Uslug_Dobav.Text);
                                All_Group.Add(Coast_Uslug_Dobav.Text);
                                DisplayAlert("Услуга успешно добавлена", "", "Ок");

                                StackLayout promezutocniy = new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical,
                                };

                                promezutocniy.Children.Add(new Label()
                                {
                                    Text = (All_Group[All_Group.Count - 2]),
                                    TextColor = Color.Purple,
                                    FontSize = 20,
                                    VerticalOptions = LayoutOptions.StartAndExpand,
                                    HorizontalOptions = LayoutOptions.StartAndExpand
                                });
                                Button button = new Button()
                                {
                                    Text = "Просмотреть информацию",
                                    BackgroundColor = Color.White,
                                    TextColor = Color.Purple,
                                    FontSize = 10,
                                    VerticalOptions = LayoutOptions.StartAndExpand,
                                    HorizontalOptions = LayoutOptions.EndAndExpand
                                };
                                Key_Pair_Usluga_Button.Add(button, All_Group[All_Group.Count - 2]);
                                button.Clicked += Inf_Of_Usluga_By_Knopka;
                                promezutocniy.Children.Add(button);
                                Vremenniy_Group.Children.Add(new Frame()
                                {
                                    BorderColor = Color.Purple,
                                    CornerRadius = 20,
                                    Content = promezutocniy,

                                });
                                break;
                            }
                            else if (Msg[0] == 'F')
                            {

                                DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                                break;
                            }
                            networkStream1.Flush();
                        }
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                    //if (!Thread1.IsAlive)
                    //{
                    //    Thread1.Start();
                    //}
                }
            }
            else
            {
                DisplayAlert("Ошибка", "Неверные данные", "Ок");
            }
            Coast_Uslug_Dobav.Text = null;
            Name_Uslug_Dobav.Text = null;
            Soxran_New_Usluga.IsEnabled = false;
            Soxran_New_Usluga.BackgroundColor = Color.Gray;

        }

        private void Save_Change_Usluuga_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (Int64.TryParse(Key_Pair_BT_EN_Usluga[button].Text, out Int64 var))
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("NC#" + Usluga_For_Otpr + "#" + Key_Pair_BT_EN_Usluga[button].Text);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] == 'S')
                            {
                                Key_Pair_BT_EN_Usluga[button].Placeholder = Key_Pair_BT_EN_Usluga[button].Text;
                                All_Group[All_Group.IndexOf(Usluga_For_Otpr) + 1] = Key_Pair_BT_EN_Usluga[button].Text;
                                Key_Pair_BT_EN_Usluga[button].Text = null;
                                DisplayAlert("Услуга успешно добавлено", "", "Ок");
                                Save_Change_Usluuga.BackgroundColor = Color.Gray;
                                Save_Change_Usluuga.IsEnabled = false;
                                break;
                            }
                            else if (Msg[0] == 'F')
                            {
                                Key_Pair_BT_EN_Usluga[button].Text = null;
                                Save_Change_Usluuga.BackgroundColor = Color.Gray;
                                Save_Change_Usluuga.IsEnabled = false;
                                DisplayAlert("Ошибка", "Попробуйте снова", "Ок");
                                break;
                            }
                            networkStream1.Flush();
                        }
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                    //if (!Thread1.IsAlive)
                    //{
                    //    Thread1.Start();
                    //}
                }
            }
            else
            {
                DisplayAlert("Ошибка", "Некоректные данные", "Ок");
                Key_Pair_BT_EN_Usluga[button].Text = null;
                button.IsEnabled = false;
                button.BackgroundColor = Color.Purple;
            }
        }
        private void Strelka_Usluga_Clicked(object sender, EventArgs e)
        {
            Add_New_Usluuga.IsVisible = true;
            Save_Change_Usluuga.IsVisible = false;
            Usluga_And_Strelka.IsVisible = false;
            Name_Usluga.IsVisible = true;
            Stacklayout_Dobav_Usluga.IsVisible = false;
            Usluga_Scroll.Content = Vremenniy_Group;
            StackLayout_In_Scroll_Usluga.Children.Clear();
            Key_Pair_BT_EN_Usluga.Clear();
        }


        void Create_Spisok_KLient()
        {

            for (int i = 0; i < All_Client.Count; i++)
            {
                Frame frame = new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,

                };
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };
                promezutocniy.Children.Add(new Label()
                {
                    Text = All_Client[i].Remove(All_Client[i].Length - 1),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                });
                Button button = new Button()
                {
                    Text = "Просмотреть информацию",
                    TextColor = Color.Purple,
                    BackgroundColor = Color.White,
                    FontSize = 10,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };
                if (All_Client[i][All_Client[i].Length - 1] == 'F')
                {
                    frame.BackgroundColor = Color.Gray;
                    button.BackgroundColor = Color.Gray;
                }
                All_Client[i] = All_Client[i].Remove(All_Client[i].Length - 1);
                button.Clicked += Inf_Of_User_By_Knopka;
                promezutocniy.Children.Add(button);
                frame.Content = promezutocniy;
                Key_Pair_Fio_Client_Button.Add(button, All_Client[i]);
                Key_Pair_Klient_Fio_And_Frame.Add(All_Client[i],frame);
                Key_Pair_Klient_Fio_And_bt.Add(All_Client[i], button);
                Vremenniy_Clnts.Children.Add(frame);
            }
            Client_sckroll.Content = Vremenniy_Clnts;
            //Vremenniy_User_Inf = StackLayout_In_Scroll_Groups;
        }
        private void Inf_Of_Usluga_By_Knopka(object sender, EventArgs e)
        {
            StackLayout_In_Scroll_Usluga.IsVisible = true;
            Save_Change_Usluuga.BackgroundColor = Color.Gray;
            Button button = (Button)sender;
            Usluga_For_Otpr = Key_Pair_Usluga_Button[button];
            Save_Change_Usluuga.IsVisible = true;
            Usluga_And_Strelka.IsVisible = true;
            Strelka_Usluga.Text = "<-";
            Name_Usluga.IsVisible = false;
            Add_New_Usluuga.IsVisible = false;
            Name_Usluga_In_Grid.Text = "Изменение";
            StackLayout promezutocniy = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
            };
            promezutocniy.Children.Add(new Label()
            {
                Text = Key_Pair_Usluga_Button[button],
                TextColor = Color.Purple,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.StartAndExpand
            });
            Entry entry = new Entry()
            {
                Text = null,
                Placeholder = All_Group[All_Group.IndexOf(Key_Pair_Usluga_Button[button]) + 1],
                FontSize = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            entry.TextChanged += Ch_SU;
            promezutocniy.Children.Add(entry);
            Key_Pair_BT_EN_Usluga.Add(Save_Change_Usluuga, entry);
            StackLayout_In_Scroll_Usluga.Children.Add(new Frame()
            {
                BorderColor = Color.Purple,
                CornerRadius = 20,
                Content = promezutocniy
            });

            Usluga_Scroll.Content = StackLayout_In_Scroll_Usluga;
        }

        void Ch_SU(object sender, System.EventArgs e)
        {
            Entry entry = (Entry)sender;
            if (entry.Text != null)
            {
                Save_Change_Usluuga.IsEnabled = true;
                Save_Change_Usluuga.BackgroundColor = Color.Purple;
            }
        }
        private void Inf_Of_User_By_Knopka(object sender, EventArgs e)
        {
            Sl_Knopok_Cl.IsVisible = false;
            Box_View_On_First_Page.HeightRequest = First_page.HeightRequest * 0.05;
            Button button = (Button)sender;
            try
            {
                Fio_Clnient_For_Otpr = Key_Pair_Fio_Client_Button[button];
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("DI#" + Key_Pair_Fio_Client_Button[button]);//(Login.Text + "#" + Password.Text);
                networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
                string Msg = "";
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[64000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        Massege.Add(Msg);
                        if (Msg[0] != '~')
                        {
                            Box_View_On_First_Page.HeightRequest = First_page.HeightRequest * 0.15;
                            Del_Client.IsVisible = true;
                            Cliets_And_Strelka.IsVisible = true;
                            Name_Client_In_Grid.Text = "Просмотр информации";
                            Strelka_Client.Text = "<-";
                            Name_Client.IsVisible = false;
                            Create_Page_About_Student(Msg, Key_Pair_Fio_Client_Button[button]);
                            break;
                        }
                        networkStream1.Flush();
                    }
                }
            }
            catch(Exception ee)
            {
                string x = ee.Message;
                DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                //if (!Thread1.IsAlive)
                //{
                //    Thread1.Start();
                //}
            }
            // StackLayout_In_Scroll_Groups.Children.Clear();
        }
        private void Strelka_Client_Clicked(object sender, EventArgs e)
        {
            Del_Client.IsVisible = false;
            Cliets_And_Strelka.IsVisible = false;
            Name_Client.IsVisible = true;
            Client_sckroll.Content = Vremenniy_Clnts;
            StackLayout_In_Scroll.Children.Clear();
            for (int i = 0; i < Vremenniy_Clnts.Children.Count; i++)
            {
                Vremenniy_Clnts.Children[i].IsVisible = true;
            }
            Poisk_Cl_Bt.IsVisible = true;
            Box_View_On_First_Page.HeightRequest = First_page.HeightRequest * 0.1;
            Sl_Knopok_Cl.IsVisible = true;
        }

        void Create_Page_About_Student(string Msg, string Name)
        {
            Box_View_On_First_Page.HeightRequest = Second_page.HeightRequest * 0.1;
            if (Msg != "F")
            {
                List<string> Massege = new List<string>();
                string Prom = "";
                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        Massege.Add(Prom);
                        Prom = "";
                    }
                }
                Massege.Add(Prom);

                StackLayout_In_Scroll.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,
                    Content = new Label()
                    {
                        Text = "ФИО: " + Name + "\n\n" + "Телефон: " + Massege[0] + "\n\n" + "Почта:" + Massege[1],
                        TextColor = Color.Purple,
                        FontSize = 20,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    }

                });
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };
                if (Massege[2] == "Y")
                {
                    Del_Client.BackgroundColor = Color.Purple;
                    Del_Client.IsEnabled = true;
                }
                else if (Massege[2] == "F")
                {
                    Del_Client.BackgroundColor = Color.Gray;
                    Del_Client.IsEnabled = false;
                }
                for (int i = 3; i < Massege.Count - 1; i += 2)
                {
                    promezutocniy.Children.Add(new Frame()
                    {
                        BorderColor = Color.Purple,
                        CornerRadius = 20,
                        Content = new Label()
                        {
                            Text = "Группа №" + Massege[i] + " " + Massege[i + 1],
                            TextColor = Color.Purple,
                            FontSize = 20,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        }
                    });
                }
                StackLayout_In_Scroll.Children.Add(new ScrollView()
                {
                    Content = promezutocniy
                });
                if (Massege.Count >= 11)
                    Box_View_On_First_Page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density * 0.2;

            }
            Client_sckroll.Content = StackLayout_In_Scroll;
        }


        void All_Sotr_Dec(string Msg)
        {
            if (Msg != "F")
            {
                string Prom = "";
                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        All_Sotr.Add(Prom);
                        Prom = "";
                    }
                }
                All_Sotr.Add(Prom);
            }
        }
        void Create_Spisok_Sotr()
        {
            for (int i = 0; i < All_Sotr.Count; i++)
            {
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };

                promezutocniy.Children.Add(new Label()
                {
                    Text = All_Sotr[i].Remove(All_Sotr[i].Length - 1),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                });
                Button button = new Button()
                {
                    Text = "Просмотреть информацию",
                    BackgroundColor = Color.White,
                    TextColor = Color.Purple,
                    FontSize = 10,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };
                button.Clicked += Inf_Of_Sotr_By_Knopka;
                promezutocniy.Children.Add(button);
                Frame frame = new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20

                };
                if (All_Sotr[i][All_Sotr[i].Length - 1] == 'F')
                {
                    frame.BackgroundColor = Color.Gray;
                    button.BackgroundColor = Color.Gray;
                }
                frame.Content =  promezutocniy;
                All_Sotr[i] = All_Sotr[i].Remove(All_Sotr[i].Length - 1);
                Key_Pair_Fio_Sotr_Button.Add(button, All_Sotr[i]);
                Vremenniy_Sotr.Children.Add(frame);
                Key_Pair_Sotr_Fio_And_Frame.Add(All_Sotr[i], frame);
                Key_Pair_Sotr_Fio_And_bt.Add(All_Sotr[i], button);
            }
            Sotr_sckroll.Content = Vremenniy_Sotr;
            //Vremenniy_User_Inf = StackLayout_In_Scroll_Groups;
        }

        private void Inf_Of_Sotr_By_Knopka(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            New_Sotr.IsVisible = false;
            try
            {
                Fio_Sotr_For_Otpr = Key_Pair_Fio_Sotr_Button[button];
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("GR#" + Key_Pair_Fio_Sotr_Button[button]);//(Login.Text + "#" + Password.Text);
                networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
                string Msg = "";
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[64000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        Massege.Add(Msg);
                        if (Msg[0] != '~')
                        {
                            // Change_Sotr_Zp.IsVisible = true;
                            Name_Sotr_In_Grid.Text = "Просмотр информации";
                            Del_Sotr.IsVisible = true;
                            Sotr_And_Strelka.IsVisible = true;
                            //Name_Sotr_In_Grid.Text = button.Text;
                            Strelka_Sotr.Text = "<-";
                            Name_Sotr.IsVisible = false;
                            Create_Page_About_Sotr(Msg, Key_Pair_Fio_Sotr_Button[button]);
                            break;
                        }
                        networkStream1.Flush();
                    }
                }
            }
            catch
            {
                DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                //if (!Thread1.IsAlive)
                //{
                //    Thread1.Start();
                //}
            }
            // StackLayout_In_Scroll_Groups.Children.Clear();
        }
        private void Strelka_Sotr_Clicked(object sender, EventArgs e)
        {
            Del_Sotr.IsVisible = false;
            Soxr_Inf_Ab_Sotr.IsVisible = false;
            New_Sotr.IsVisible = true;
            // Change_Sotr_Zp.IsVisible = false;
            Sotr_And_Strelka.IsVisible = false;
            Stac_Knopok_In_Sotr.IsVisible = true;
            Name_Sotr.IsVisible = true;
            New_Sotr_StackLayout.IsVisible = false;
            Sotr_sckroll.Content = Vremenniy_Sotr;
            Name_Sotr_In_Grid.Text = "Просмотр информации";
            StackLayout_In_Scroll_Sotr.Children.Clear();
            Box_View_On_Second_Page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density * 0.1;
            for (int i = 0; i < Vremenniy_Sotr.Children.Count; i++)
            {
                Vremenniy_Sotr.Children[i].IsVisible = true;
            }
            Poisk_Sotr_Bt.IsVisible = true;
        }

        void Create_Page_About_Sotr(string Msg, string Name)
        {
            //Name_Sotr_In_Grid.Text = "Добавление сотрудника";
            Stac_Knopok_In_Sotr.IsVisible = false;
            if (Msg != "F")
            {
                List<string> Massege = new List<string>();
                string Prom = "";
                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        Massege.Add(Prom);
                        Prom = "";
                    }
                }
                Massege.Add(Prom);


                StackLayout_In_Scroll_Sotr.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,
                    Content = new Label()
                    {
                        Text = "ФИО: " + Name + "\n\n" + "Должность: " + Massege[0] + "\n\n" + "Дата Рождения: " + DateTime.Parse(Massege[1]).ToString("d") + "\n\n" + "Стаж: " + Massege[2] + "\n\n" + "Телефон: " + Massege[3] + "\n\n" + "Почта: " + Massege[4],
                        TextColor = Color.Purple,
                        FontSize = 15,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    }

                });
                Entry en = new Entry()
                {
                    Placeholder = Massege[5],
                    MaxLength = 10,
                    TextColor = Color.Purple,
                    FontSize = 15,
                    WidthRequest = Second_page.WidthRequest * 0.2,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                };
                en.TextChanged += Ch_Zp;
                StackLayout mini = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };
                if (Massege[6] == "Y")
                {
                    Del_Sotr.BackgroundColor = Color.Purple;
                    en.IsEnabled = true;
                    Del_Sotr.IsEnabled = true;
                }
                else if (Massege[6] == "F")
                {
                    Del_Sotr.BackgroundColor = Color.Gray;
                    en.IsEnabled = false;
                    Del_Sotr.IsEnabled = false;
                }
                mini.Children.Add(new Label()
                {
                    Text = "Зарплата: ",
                    FontSize = 15,
                    TextColor = Color.Purple,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                });
                mini.Children.Add(en);
                Button button = new Button()
                {
                    BackgroundColor = Color.Gray,
                    CornerRadius = 20,
                    TextColor = Color.White,
                    FontSize = 10,
                    Text = "Применить",
                    IsEnabled = false,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                };
                Key_Pair_BT_EN_Sotr.Add(en, button);
                Key_Pair_BT_EN_Sotr_Inv.Add(button, en);
                button.Clicked += Change_Sotr_Zp_Clicked;
                mini.Children.Add(button);
                StackLayout_In_Scroll_Sotr.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 15,
                    Content = mini
                });
                // <Button x:Name="Change_Sotr_Zp" Text="Применить изменения"  BackgroundColor="Purple" CornerRadius="40" IsVisible="false"  TextColor="White" VerticalOptions="EndAndExpand" Clicked = "Change_Sotr_Zp_Clicked" ></ Button >
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };

                for (int i = 7; i < Massege.Count - 1; i += 2)
                {
                    promezutocniy.Children.Add(new Frame()
                    {
                        BorderColor = Color.Purple,
                        CornerRadius = 20,
                        Content = new Label()
                        {
                            Text = "Группа №" + Massege[i] + " " + Massege[i + 1],
                            TextColor = Color.Purple,
                            FontSize = 15,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        }
                    });
                }

                StackLayout_In_Scroll_Sotr.Children.Add(new ScrollView()
                {
                    Content = promezutocniy
                });
                if (Massege.Count >= 15)
                    Box_View_On_Second_Page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density * 0.2;
            }
            Sotr_sckroll.Content = StackLayout_In_Scroll_Sotr;
        }
        void Ch_Zp(object sender, System.EventArgs e)
        {
            Entry entry = (Entry)sender;
            if (Del_Sotr.IsEnabled == true)
            {
                Key_Pair_BT_EN_Sotr[entry].IsEnabled = true;
                Key_Pair_BT_EN_Sotr[entry].BackgroundColor = Color.Purple;
            }

        }
        void Seconds_Groups(string Str)
        {
            if (Str != "F")
            {
                string Prom = "";
                for (int i = 0; i < Str.Length; i++)
                {
                    if (Str[i] != '#')
                    {
                        Prom += Str[i];
                    }
                    else
                    {
                        Groops_Id_List.Add(Prom);
                        Prom = "";
                    }
                }
                Groops_Id_List.Add(Prom);
            }
        }


        void Zapolnenie_Passpis()
        {
            Group_Id_Piccker.Items.Clear();
            Vremy_Picker.Items.Clear();
            Data_Picker.Items.Clear();
            for (int i = 0; i < Groops_Id_List.Count; i++)
            {
                Group_Id_Piccker.Items.Add("№ " + Groops_Id_List[i]);
            }
            Groops_Date_List.Clear();
            for (int i = 1; i < 30; i++)
            {
                Groops_Date_List.Add(DateTime.Now.AddDays(i).ToString("d"));
                Data_Picker.Items.Add(Groops_Date_List[i-1]);
            }
            Groops_Time_List.Add("16:30 - 18:00");
            Groops_Time_List.Add("18:00 - 19:30");
            Groops_Time_List.Add("19:30 - 21:00");
            Vremy_Picker.Items.Add("16:30 - 18:00");
            Vremy_Picker.Items.Add("18:00 - 19:30");
            Vremy_Picker.Items.Add("19:30 - 21:00");
        }
        void Third_All_Meropriatie(string Str)
        {
            if (Str != "F")
            {
                string Prom = "";
                for (int i = 0; i < Str.Length; i++)
                {
                    if (Str[i] != '#')
                    {
                        Prom += Str[i];
                    }
                    else
                    {
                        All_Meropriatie_String_List.Add(Prom);
                        Prom = "";
                    }
                }
                All_Meropriatie_String_List.Add(Prom);
                for (int i = 0; i < All_Meropriatie_String_List.Count - 1; i += 2)
                {
                    Prom_All_Meropriatie_String_List.Add(All_Meropriatie_String_List[i] + "#" + All_Meropriatie_String_List[i + 1]);
                }
            }
        }
        void Zapolnenie_SL_Na_Dobavlenie()
        {

        }
        void Buttons_On_Third_Page(object sender, System.EventArgs e)
        {
            Sl_Knopok_Mer.IsVisible = false;
            Button button = (Button)sender;
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("GA#" + Key_Pair_For_Meroproytie_Big_String_All[button]);//(Login.Text + "#" + Password.Text);
                networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
                string Msg = "";
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[64000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        Massege.Add(Msg);
                        if (Msg[0] != '~')
                        {
                            Meropriatia_And_Strelka.IsVisible = true;
                            Name_Meropriatia_In_Grid.Text = button.Text;
                            Strelka_Meropriatia.Text = "<-";
                            Name_Meropriatia.IsVisible = false;
                            Create_Spisok_Meropriatia(Msg);
                            break;
                        }
                        networkStream1.Flush();
                    }
                }
            }
            catch
            {
                DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                //if (!Thread1.IsAlive)
                //{
                //    Thread1.Start();
                //}
            }
        }
        void Decoding_And_Criate_Choldren_All_Meropriatie()
        {
            int Count = 0;
            for (int i = 0; i < All_Meropriatie_String_List.Count - 1; i += 2)
            {
                Button bt = new Button()
                {
                    Text = All_Meropriatie_String_List[i] + "\n\n" + DateTime.Parse(All_Meropriatie_String_List[i + 1]).ToString("dd MMM yyyy"),
                    TextColor = Color.Purple,
                    HeightRequest = Third_page.HeightRequest * 0.1,
                    WidthRequest = Third_page.WidthRequest,
                    CornerRadius = 20,
                    VerticalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White,
                    BorderColor = Color.Purple,
                    BorderWidth = 1
                };
                Key_Pair_For_Meroproytie_Big_String_All.Add(bt, Prom_All_Meropriatie_String_List[Count]);
                Count++;
                bt.Clicked += Buttons_On_Third_Page;
                Vremenniy_Meropriatie.Children.Add(bt);
            }
            Meroptiatie_sckroll.Content = Vremenniy_Meropriatie;
        }
        void Create_Spisok_Meropriatia(string Msg)
        {
            if (Msg != "F")
            {
                List<string> Massege = new List<string>();
                string Prom = "";
                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        Massege.Add(Prom);
                        Prom = "";
                    }
                }
                Massege.Add(Prom);
                for (int i = 0; i < Massege.Count; i++)
                {
                    StackLayout promezutocniy = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                    };

                    promezutocniy.Children.Add(new Label()
                    {
                        Text = (Massege[i]),
                        TextColor = Color.Purple,
                        FontSize = 20,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    });
                    StackLayout_In_Scroll_Meroptiatie_sckroll.Children.Add(new Frame()
                    {
                        BorderColor = Color.Purple,
                        CornerRadius = 20,
                        Content = promezutocniy,
                    });
                }
            }
            Meroptiatie_sckroll.Content = StackLayout_In_Scroll_Meroptiatie_sckroll;
        }
        private void Strelka_Meropriatia_Clicked(object sender, EventArgs e)
        {
            Meropriatia_And_Strelka.IsVisible = false;
            Name_Meropriatia.IsVisible = true;
            Meroptiatie_sckroll.Content = Vremenniy_Meropriatie;
            StackLayout_In_Scroll_Meroptiatie_sckroll.Children.Clear();
            Sl_Knopok_Mer.IsVisible = true;
            StackLayout_In_Scroll_Meroptiatie_sckroll.IsVisible = true;
            New_Dannye_in_Mer.IsVisible = false;
            Meroptiatie_sckroll.Content = Vremenniy_Meropriatie;
            SoXr_Ism_New_Mer.IsVisible = false;
            Sl_Knopok_Mer.IsVisible = true;
            for (int i = 0; i < Vremenniy_Meropriatie.Children.Count; i++)
            {
                Vremenniy_Meropriatie.Children[i].IsVisible = true;
            }
        }


        private void Button_One_Clicked(object sender, EventArgs e)
        {
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) * 0, 0, true);
        }

        private void Button_Two_Clicked(object sender, EventArgs e)
        {
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) * 1.014, 0, true);
        }

        private void Button_Three_Clicked(object sender, EventArgs e)
        {
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width * 2.03 / DeviceDisplay.MainDisplayInfo.Density), 0, true);
        }

        private void Button_Four_Clicked(object sender, EventArgs e)
        {
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width * 3.04 / DeviceDisplay.MainDisplayInfo.Density), 0, true);
        }

        private void Button_Five_Clicked(object sender, EventArgs e)
        {
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width * 4.06 / DeviceDisplay.MainDisplayInfo.Density), 0, true);
        }

        private void Button_Six_Clicked(object sender, EventArgs e)
        {
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width * 5.08 / DeviceDisplay.MainDisplayInfo.Density), 0, true);
        }


        private void Dobav_Mer_Bt_Clicked(object sender, EventArgs e)
        {
            SoXr_Ism_New_Mer.IsVisible = true;
            Name_Meropriatia_In_Grid.Text = "Добавление мероприятия";
            Strelka_Meropriatia.Text = "<-";
            StackLayout_In_Scroll_Meroptiatie_sckroll.IsVisible = false;
            New_Dannye_in_Mer.IsVisible = true;
            Meropriatia_And_Strelka.IsVisible = true;
            Sl_Knopok_Mer.IsVisible = false;
            Name_Meropriatia.IsVisible = false;
            Meroptiatie_sckroll.Content = New_Dannye_in_Mer;
        }

        private void Poisk_Mer_Bt_Clicked(object sender, EventArgs e)
        {
            Poisk_Scroll.IsVisible = true;
            StackLayout_In_Scroll_Meroptiatie_sckroll.IsVisible = false;
            New_Dannye_in_Mer.IsVisible = false;
            Meroptiatie_sckroll.Content = Poisk_Scroll;
            Sl_Knopok_Mer.IsVisible = false;

            Meropriatia_And_Strelka.IsVisible = true;
            Name_Meropriatia_In_Grid.Text = "Поиск";
            Strelka_Meropriatia.Text = "<-";
            Name_Meropriatia.IsVisible = false;
        }
        private void Obsh_TextChanged(object sender, TextChangedEventArgs e)
        {
            SoXr_Ism_New_Mer.BackgroundColor = Color.Purple;
            SoXr_Ism_New_Mer.IsEnabled = true;
        }

        private void SoXr_Ism_New_Mer_Clicked(object sender, EventArgs e)
        {
            bool Is_No_Ok = false;
            string Prom = "";
            if ((Name_Meropr.Text != null) && (Name_Meropr.Text.Length < 4))
            {
                string Proverka1 = Name_Meropr.Text;
                string Proverka2 = Proverka1.Trim();
                if (Proverka2.Length < 4)
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный название";
                    Prom += "\n";
                    Name_Meropr.Text = null;
                }
            }
            else if(Name_Meropr.Text == null)
            {
                Is_No_Ok = true;
                Prom += "Некоректный название";
                Prom += "\n";
            }
            if ((Mesto_Meropr.Text != null) && (Mesto_Meropr.Text.Length < 4))
            {
                string Proverka1 = Mesto_Meropr.Text;
                string Proverka2 = Proverka1.Trim();
                if (Proverka2.Length < 4)
                {
                    Is_No_Ok = true;
                    Prom += "Некоректное место";
                    Prom += "\n";
                    Mesto_Meropr.Text = null;
                }
            }
            else if(Mesto_Meropr.Text == null)
            {
                Is_No_Ok = true;
                Prom += "Некоректное место";
                Prom += "\n";
            }
            if ((Type_Meropr.Text != null) && (Type_Meropr.Text.Length < 4))
            {
                string Proverka1 = Type_Meropr.Text;
                string Proverka2 = Proverka1.Trim();
                if (Proverka2.Length < 4)
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный тип";
                    Prom += "\n";
                    Type_Meropr.Text = null;
                }
            }
            else if(Type_Meropr.Text == null)
            {
                Is_No_Ok = true;
                Prom += "Некоректный тип";
                Prom += "\n";
            }
            if ((Date_Meropr.Text != null) && (!DateTime.TryParseExact(Date_Meropr.Text, "d", new CultureInfo("en-US"), DateTimeStyles.AllowLeadingWhite, out DateTime result)))
            {
                Is_No_Ok = true;
                Prom += "Некоректная дата";
                Date_Meropr.Text = null;
            }
            else if (Date_Meropr.Text == null)
            {
                Is_No_Ok = true;
                Prom += "Некоректная дата";
                Prom += "\n";
            }


            SoXr_Ism_New_Mer.BackgroundColor = Color.Gray;
            SoXr_Ism_New_Mer.TextColor = Color.White;
            SoXr_Ism_New_Mer.IsEnabled = false;

            if (Is_No_Ok == true)
            {
                DisplayAlert("Внимание", Prom, "Ок");
            }
            else
            {
                string Prom_Str_LP = "NM#" + Name_Meropr.Text + "#" + Mesto_Meropr.Text + "#" + Type_Meropr.Text + "#" + Date_Meropr.Text;
                Send_Changes(Prom_Str_LP);
            }
        }

        private void Send_Changes(string Str)
        {
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes(Str);//(Login.Text + "#" + Password.Text);
                networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
                string Msg = "";
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[64000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        Massege.Add(Msg);
                        if (Msg[0] == 'S')
                        {
                            All_Meropriatie_String_List.Add(Name_Meropr.Text);
                            All_Meropriatie_String_List.Add(DateTime.Parse(Date_Meropr.Text).ToString());
                            Button bt = new Button()
                            {
                                Text = Name_Meropr.Text + "\n\n" + DateTime.Parse(Date_Meropr.Text).ToString("dd MMM yyyy"),
                                TextColor = Color.Purple,
                                HeightRequest = Third_page.HeightRequest * 0.1,
                                WidthRequest = Third_page.WidthRequest,
                                CornerRadius = 20,
                                VerticalOptions = LayoutOptions.Center,
                                BackgroundColor = Color.White,
                                BorderColor = Color.Purple,
                                BorderWidth = 1
                            };
                            Prom_All_Meropriatie_String_List.Add(Name_Meropr.Text + "#" + DateTime.Parse(Date_Meropr.Text).ToString());
                            Key_Pair_For_Meroproytie_Big_String_All.Add(bt, Prom_All_Meropriatie_String_List[Prom_All_Meropriatie_String_List.Count - 1]);
                            bt.Clicked += Buttons_On_Third_Page;
                            Vremenniy_Meropriatie.Children.Add(bt);
                            Name_Meropr.Text = null;
                            Mesto_Meropr.Text = null;
                            Type_Meropr.Text = null;
                            Date_Meropr.Text = null;
                            DisplayAlert("Мероприятие успешно добавлено", "", "Ок");
                            break;
                        }
                        else if (Msg[0] == 'F')
                        {
                            Name_Meropr.Text = null;
                            Mesto_Meropr.Text = null;
                            Type_Meropr.Text = null;
                            Date_Meropr.Text = null;
                            DisplayAlert("Ошибка", "Попробуйте снова", "Ок");
                            break;
                        }
                        networkStream1.Flush();
                    }
                }
            }
            catch
            {
                DisplayAlert("Ошибка", "Сервер прилег, поднимаем", "Ок");
                //if (!Thread1.IsAlive)
                //{
                //    Thread1.Start();
                //}
            }
        }

        void Criate_Dilog(string Msg)
        {
            if (Msg != "F")
            {
                List<string> Massege = new List<string>();
                string Prom = "";

                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        Massege.Add(Prom);
                        Prom = "";
                    }
                }
                Massege.Add(Prom);

                for (int i = 0; i < Massege.Count - 2; i += 3)
                {
                    string Who = "";
                    if (Massege[i][0] == 'Д')
                    {
                        Who = "Вы";
                    }
                    else
                    {
                        Who = Massege[i];
                    }
                    StackLayout promezutocniy = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                    };

                    promezutocniy.Children.Add(new Label()
                    {
                        Text = Who,
                        TextColor = Color.Red,
                        FontSize = 15,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    });
                    promezutocniy.Children.Add(new Label()
                    {
                        Text = (Massege[i + 1]),
                        TextColor = Color.Purple,
                        FontSize = 20,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    });
                    promezutocniy.Children.Add(new Label()
                    {
                        Text = (Massege[i + 2]),
                        TextColor = Color.Purple,
                        FontSize = 10,
                        VerticalOptions = LayoutOptions.EndAndExpand,
                        HorizontalOptions = LayoutOptions.EndAndExpand

                    });

                    StackLayout_In_Scroll_Groops_sckroll.Children.Add(new Frame()
                    {
                        BorderColor = Color.Purple,
                        CornerRadius = 20,
                        Content = promezutocniy,

                    });
                }
            }
            Chats_sckroll.Content = StackLayout_In_Scroll_Groops_sckroll;
        }
        private void Send_Clicked(object sender, EventArgs e)
        {
            if ((Polle_Vvoda.Text != null) && (Polle_Vvoda.Text.Length > 0))
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("MMS0#D" + "#" + Polle_Vvoda.Text);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                if (Msg[0] == 'S')
                                {
                                    StackLayout promezutocniy = new StackLayout()
                                    {
                                        Orientation = StackOrientation.Vertical,
                                    };

                                    promezutocniy.Children.Add(new Label()
                                    {
                                        Text = "Вы",
                                        TextColor = Color.Red,
                                        FontSize = 15,
                                        VerticalOptions = LayoutOptions.StartAndExpand,
                                        HorizontalOptions = LayoutOptions.StartAndExpand
                                    });
                                    promezutocniy.Children.Add(new Label()
                                    {
                                        Text = Polle_Vvoda.Text,
                                        TextColor = Color.Purple,
                                        FontSize = 20,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.StartAndExpand
                                    });
                                    promezutocniy.Children.Add(new Label()
                                    {
                                        Text = DateTime.Now.ToString("HH:mm dd MMMM yyyy"),
                                        TextColor = Color.Purple,
                                        FontSize = 10,
                                        VerticalOptions = LayoutOptions.EndAndExpand,
                                        HorizontalOptions = LayoutOptions.EndAndExpand

                                    });

                                    StackLayout_In_Scroll_Groops_sckroll.Children.Add(new Frame()
                                    {
                                        BorderColor = Color.Purple,
                                        CornerRadius = 20,
                                        Content = promezutocniy,

                                    });
                                    Polle_Vvoda.Text = null;
                                    break;
                                }
                                else if (Msg[0] == 'F')
                                {
                                    Polle_Vvoda.Text = null;
                                    DisplayAlert("Ошибка", "Попробуйте снова", "Ок");
                                    break;
                                }
                            }
                            networkStream1.Flush();
                        }
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                    //if (!Thread1.IsAlive)
                    //{
                    //    Thread1.Start();
                    //}
                }
            }
        }


        private async void Save_Change_Rasspis_Clicked(object sender, EventArgs e)
        {
            //bool x = await DisplayAlert("Данные верны?", "№ " + Groops_Id_List[(Convert.ToInt32(Group_Id_Scroll_Scroll.ScrollY / 35.3))].ToString() + '\n' + Groops_Time_List[(Convert.ToInt32(Group_Time_Scroll_Scroll.ScrollY / 35.3))].ToString() + '\n' + Groops_Date_List[(Convert.ToInt32(Group_Date_Scroll_Scroll.ScrollY / 35.3))].ToString(), "Да", "Нет");
            //if (x == true)
            //{
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("NR" + "#" +Groops_Id_List[Group_Id_Piccker.SelectedIndex] + "#" + Data_Picker.SelectedItem+ "#" + Vremy_Picker.SelectedItem);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                if (Msg[0] == 'S')
                                {
                                    StackLayout promezutocniy = new StackLayout()
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                    };

                                    promezutocniy.Children.Add(new Label()
                                    {
                                        Text =  Group_Id_Piccker.SelectedItem.ToString(),
                                        TextColor = Color.Purple,
                                        FontSize = 20,
                                        VerticalOptions = LayoutOptions.CenterAndExpand
                                    });
                                string j = Data_Picker.SelectedItem.ToString();
                                string h = "";
                                h+= (((DateTime)(Convert.ToDateTime(j))).ToString("dd MMMM yyyy ")).ToString();
                                promezutocniy.Children.Add(new Label()
                                    {
                                        Text = Vremy_Picker.SelectedItem.ToString() + "      " + h,
                                TextColor = Color.Purple,
                                        FontSize = 20,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.EndAndExpand

                                    });

                                    StackLayout_In_Scroll_Rasspis.Children.Add(new Frame()
                                    {
                                        BorderColor = Color.Purple,
                                        CornerRadius = 20,
                                        Content = promezutocniy
                                    });
                                    DisplayAlert("Данные успешно добавлены", "", "Ок");
                                    break;
                                }
                                else if (Msg[0] == 'F')
                                {
                                    DisplayAlert("Ошибка", "Для данной группы на это время уже есть занятие", "Ок");
                                    break;
                                }
                            }
                        }
                    }
                }
                catch(Exception ee)
                {
                string h = ee.Message;
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                }
            Save_Change_Rasspis.IsEnabled = false;
            Save_Change_Rasspis.BackgroundColor = Color.Gray;
            Group_Id_Piccker.SelectedItem = null;
            Vremy_Picker.SelectedItem = null;
            Data_Picker.SelectedItem = null;
           // }
        }

        private async void Del_Client_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            bool x = await DisplayAlert("Потдвердите действие", "Удалить клиента?", "Да", "Нет");
            if (x == true)
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("DDK" + Fio_Clnient_For_Otpr);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                if (Msg[0] == 'S')
                                {
                                    button.IsEnabled = false;
                                    button.BackgroundColor = Color.Gray;
                                    DisplayAlert("Клиент удален", "", "Ок");
                                    Key_Pair_Klient_Fio_And_bt[Fio_Clnient_For_Otpr].BackgroundColor = Color.Gray;
                                    Key_Pair_Klient_Fio_And_Frame[Fio_Clnient_For_Otpr].BackgroundColor = Color.Gray;
                                    break;
                                }
                                else if (Msg[0] == 'F')
                                {
                                    DisplayAlert("Ошибка", "Попробуйте снова", "Ок");
                                    break;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                }
            }
        }

        private async void Del_Sotr_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            bool x = await DisplayAlert("Потдвердите действие", "Удалить сотрудника?", "Да", "Нет");
            if (x == true)
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("DDS" + Fio_Sotr_For_Otpr);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                if (Msg[0] == 'S')
                                {
                                    Key_Pair_Sotr_Fio_And_bt[Fio_Sotr_For_Otpr].BackgroundColor = Color.Gray;
                                    Key_Pair_Sotr_Fio_And_Frame[Fio_Sotr_For_Otpr].BackgroundColor = Color.Gray;
                                    button.IsEnabled = false;
                                    button.BackgroundColor = Color.Gray;
                                    DisplayAlert("Сотрдник удален", "", "Ок");
                                    break;
                                }
                                else if (Msg[0] == 'F')
                                {
                                    DisplayAlert("Ошибка", "Попробуйте снова", "Ок");
                                    break;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                }
            }
        }

        private void Change_Sotr_Zp_Clicked(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            bt.IsEnabled = false;
            bt.BackgroundColor = Color.Gray;
            bt.TextColor = Color.White;
            if (Int64.TryParse(Key_Pair_BT_EN_Sotr_Inv[bt].Text, out Int64 num) && (num > 0))
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("NZ" + "#" + Key_Pair_BT_EN_Sotr_Inv[bt].Text + "#" + Fio_Sotr_For_Otpr);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                if (Msg[0] == 'S')
                                {
                                    Key_Pair_BT_EN_Sotr_Inv[bt].Placeholder = Key_Pair_BT_EN_Sotr_Inv[bt].Text;
                                    DisplayAlert("Данные успешно добавлены", "", "Ок");
                                    break;
                                }
                                else if (Msg[0] == 'F')
                                {
                                    DisplayAlert("Ошибка", "Попробте снова", "Ок");
                                    break;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                }
            }
            else
            {
                DisplayAlert("Ошибка", "Нееоректные данные", "Ок");
            }
            Key_Pair_BT_EN_Sotr_Inv[bt].Text = null;
            bt.IsEnabled = false;
            bt.BackgroundColor = Color.Gray;
        }

        private void New_Rasspis_Clicked(object sender, EventArgs e)
        {
            Three_Scrols_In_Rasspis.IsVisible = true;
            StackLayout_In_Scroll_Rasspis.IsVisible = false;

            Rasspis_sckroll.Content = Three_Scrols_In_Rasspis;
            Rasspis_And_Strelka.IsVisible = true;
            Strelka_Rasspis.Text = "<-";
            Name_Rasspis.IsVisible = false;

            New_Rasspis.IsVisible = false;
        }
        void All_Rasspis_Dec(string Str)
        {
            if (Str != "F")
            {
                string Prom = "";
                for (int i = 0; i < Str.Length; i++)
                {
                    if (Str[i] != '#')
                    {
                        Prom += Str[i];
                    }
                    else
                    {
                        All_Rasspis.Add(Prom);
                        Prom = "";
                    }
                }
                All_Rasspis.Add(Prom);
            }
        }
        void Decoding_And_Criate_Children_Rasspis()
        {

            for (int i = 0; i < All_Rasspis.Count - 2; i += 3) //группа - дата - время
            {
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                };

                promezutocniy.Children.Add(new Label()
                {
                    Text = "№" + (All_Rasspis[i]),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                });
                promezutocniy.Children.Add(new Label()
                {
                    Text = All_Rasspis[i + 2] + "   " + (All_Rasspis[i + 1]),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand

                });

                StackLayout_In_Scroll_Rasspis.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,
                    Content = promezutocniy
                });
            }
            Rasspis_sckroll.Content = StackLayout_In_Scroll_Rasspis;
        }
        private void Strelka_Rasspis_Clicked(object sender, EventArgs e)
        {
            New_Rasspis.IsVisible = true;
            Rasspis_And_Strelka.IsVisible = false;
            Name_Rasspis.IsVisible = true;
            Three_Scrols_In_Rasspis.IsVisible = false;
            StackLayout_In_Scroll_Rasspis.IsVisible = true;
            Rasspis_sckroll.Content = StackLayout_In_Scroll_Rasspis;
        }

        private void New_Sotr_Clicked(object sender, EventArgs e)
        {
            Name_Sotr_In_Grid.Text = "Добавление сотрудника";
            Sotr_And_Strelka.IsVisible = true;
            Strelka_Sotr.Text = "<-";
            Name_Sotr.IsVisible = false;
            New_Sotr_StackLayout.IsVisible = true;
            Stac_Knopok_In_Sotr.IsVisible = false;
            Sotr_sckroll.Content = New_Sotr_StackLayout;
            Soxr_Inf_Ab_Sotr.IsVisible = true;
            New_Sotr.IsVisible = false;
            Box_View_On_Second_Page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density * 0.3;
        }

        private void Type_Sotr_SelectedIndexChanged(object sender, EventArgs e)
        {
            Soxr_Inf_Ab_Sotr.IsEnabled = true;
            Soxr_Inf_Ab_Sotr.BackgroundColor = Color.Purple;
        }

        private void Name_Sotr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((FIO_sotr.Text != null)||(ZP_Sotr.Text != null) ||(Data_Rozh.Text != null) ||(Stazh.Text != null) ||(Pochta_Sotr.Text != null) ||
                (Phone_Number_Sotr.Text != null) ||(Login_Sotr.Text != null) ||(Password_Sotr.Text != null) ||(Psevldonim_Sotr.Text != null))
            {
                Soxr_Inf_Ab_Sotr.IsEnabled = true;
                Soxr_Inf_Ab_Sotr.BackgroundColor = Color.Purple;
            }
            
        }

        private void Soxr_Inf_Ab_Sotr_Clicked(object sender, EventArgs e)
        {
            bool Is_No_Ok = false;
            string Prom = "";
            if (FIO_sotr.Text == null)
            {
                 Is_No_Ok = true;
                 Prom += "Некоректные ФИО";
                Prom += "\n";
            }
            else if ((FIO_sotr.Text != null) && (FIO_sotr.Text.Length > 0))
            {
                string x = FIO_sotr.Text;
                string v = x.Trim();
                if (v == "")
                {
                    Is_No_Ok = true;
                    Prom += "Некоректные ФИО";
                    Prom += "\n";
                    FIO_sotr.Text = null;
                }
            }

            if(Type_Sotr.SelectedItem==null)
            {
                Prom += "Некоректная должность";
                Prom += "\n";
            }

                if ((ZP_Sotr.Text == null) || ((ZP_Sotr.Text != null) && (!Int64.TryParse(ZP_Sotr.Text, out Int64 num))))
            {
                Is_No_Ok = true;
                Prom += "Некоректная зп";
                Prom += "\n";
                ZP_Sotr.Text = null;
            }
            if ((Data_Rozh.Text == null) || ((Data_Rozh.Text != null) && (!DateTime.TryParseExact(Data_Rozh.Text, "d", new CultureInfo("en-US"), DateTimeStyles.AllowLeadingWhite, out DateTime result))))
            {
                Is_No_Ok = true;
                Prom += "Некоректная дата";
                Prom += "\n";
                Data_Rozh.Text = null;
            }
            if ((Stazh.Text == null) || ((Stazh.Text != null) && (!Int64.TryParse(Stazh.Text, out Int64 num1))))
            {
                Is_No_Ok = true;
                Prom += "Некоректный стаж";
                Prom += "\n";
                Stazh.Text = null;
            }
            if ((Pochta_Sotr.Text == null) || ((Pochta_Sotr.Text != null) && (!Pochta_Sotr.Text.Contains('@') || (!Pochta_Sotr.Text.Contains('.')))))
            {
                Is_No_Ok = true;
                Prom += "Некоректная почта";
                Prom += "\n";
                Pochta_Sotr.Text = null;
            }
            if ((Phone_Number_Sotr.Text == null) || ((Phone_Number_Sotr.Text != null) && ((Phone_Number_Sotr.Text.Length != 11) || (!Int64.TryParse(Phone_Number_Sotr.Text, out Int64 num2)))))
            {
                Is_No_Ok = true;
                Prom += "Некоректный номер";
                Prom += "\n";
                Phone_Number_Sotr.Text = null;
            }
            if (Login_Sotr.Text == null)
            {
                Is_No_Ok = true;
                Prom += "Некоректный логин";
                Prom += "\n";
            }
            else
            {
                string Proverka1 = Login_Sotr.Text;
                string Proverka2 = Proverka1.Trim();
                if (Proverka2.Length != 6)
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный логин";
                    Prom += "\n";
                    Login_Sotr.Text = null;
                }
            }
            if (Password_Sotr.Text == null)
            {
                    Is_No_Ok = true;
                    Prom += "Некоректный пароль";
                    Prom += "\n";
            }
            else
            {
                string Proverka1 = Password_Sotr.Text;
                string Proverka2 = Proverka1.Trim();
                if (Proverka2.Length != 6)
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный пароль";
                    Prom += "\n";
                    Password_Sotr.Text = null;
                }
            }

            if (Psevldonim_Sotr.Text == null)
            {
                    Is_No_Ok = true;
                    Prom += "Некоректный псевдоним";
            }
            else if ((Psevldonim_Sotr.Text != null) && (Psevldonim_Sotr.Text.Length > 0))
            {
                string x = Psevldonim_Sotr.Text;
                string v = x.Trim();
                if (v == "")
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный псевдоним";
                    Psevldonim_Sotr.Text = null;
                }
            }


            Soxr_Inf_Ab_Sotr.BackgroundColor = Color.Gray;
            Soxr_Inf_Ab_Sotr.TextColor = Color.White;
            Soxr_Inf_Ab_Sotr.IsEnabled = false;

            if (Is_No_Ok == true)
            {
                DisplayAlert("Внимание", Prom, "Ок");
            }
            else
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("NS" + "#" + FIO_sotr.Text + "#"+ Type_Sotr.SelectedItem.ToString()+ "#"+ ZP_Sotr.Text+ "#"+ Data_Rozh.Text + "#"+ Stazh.Text + "#" + Phone_Number_Sotr.Text + "#" + Pochta_Sotr.Text + "#"+ Login_Sotr.Text+ "#"+ Password_Sotr.Text + "#"+ Psevldonim_Sotr.Text+"#"+(Type_Sotr.SelectedIndex+2).ToString());//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                if (Msg[0] == 'S')
                                {
                                    DisplayAlert("Цена успешно измена", "", "Ок");
                                    StackLayout promezutocniy = new StackLayout()
                                    {
                                        Orientation = StackOrientation.Vertical,
                                    };

                                    promezutocniy.Children.Add(new Label()
                                    {
                                        Text = (FIO_sotr.Text),
                                        TextColor = Color.Purple,
                                        FontSize = 20,
                                        VerticalOptions = LayoutOptions.StartAndExpand,
                                        HorizontalOptions = LayoutOptions.StartAndExpand
                                    });
                                    Button button = new Button()
                                    {
                                        Text = "Просмотреть информацию",
                                        BackgroundColor = Color.White,
                                        TextColor = Color.Purple,
                                        FontSize = 10,
                                        VerticalOptions = LayoutOptions.StartAndExpand,
                                        HorizontalOptions = LayoutOptions.EndAndExpand
                                    };
                                    button.Clicked += Inf_Of_Sotr_By_Knopka;
                                    promezutocniy.Children.Add(button);
                                    Key_Pair_Fio_Sotr_Button.Add(button, FIO_sotr.Text);
                                    Vremenniy_Sotr.Children.Add(new Frame()
                                    {
                                        BorderColor = Color.Purple,
                                        CornerRadius = 20,
                                        Content = promezutocniy,

                                    });
                                    FIO_sotr.Text = null;
                                    ZP_Sotr.Text = null;
                                    Data_Rozh.Text = null;
                                    Stazh.Text = null;
                                    Pochta_Sotr.Text = null;
                                    Phone_Number_Sotr.Text = null;
                                    Login_Sotr.Text = null;
                                    Password_Sotr.Text = null;
                                    Psevldonim_Sotr.Text = null;
                                    Type_Sotr.SelectedItem = null;
                                    break;
                                }
                                else if (Msg[0] == 'F')
                                {
                                    DisplayAlert("Ошибка", "Логин занят", "Ок");
                                    Login_Sotr.Text = null;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ee)
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                }
            }
        }

        private void Obsh_Picker_Rasspis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if((Group_Id_Piccker.SelectedItem!=null)&&(Vremy_Picker.SelectedItem!=null)&&(Data_Picker.SelectedItem!=null))
            {
                Save_Change_Rasspis.IsEnabled = true;
                Save_Change_Rasspis.BackgroundColor = Color.Purple;
            }
        }

        private async  void Poisk_Cl_Bt_Clicked(object sender, EventArgs e)
        {
            //Box_View_On_First_Page.HeightRequest = First_page.HeightRequest * 0.2;
            string Result = await DisplayPromptAsync("Поиск", "", "Ok", "", "", 40, Keyboard.Chat, "");
            if (Result.Length > 0)
            {
                Poisk_Cl_Bt.IsVisible = false;
                Cliets_And_Strelka.IsVisible = true;
                Name_Client_In_Grid.Text = "Поиск";
                Strelka_Client.Text = "<-";
                Name_Client.IsVisible = false;
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("PC" + "#" + Result);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                Decoding_And_Create_Poick_Client(Msg);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ee)
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                }
            }

        }
       void Decoding_And_Create_Poick_Client(string Msg)
        {
            List<string> Naidennie_Clienti = new List<string>();
            if (Msg != "F")
            {
                string Prom = "";
                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        Naidennie_Clienti.Add(Prom);
                        Prom = "";
                    }
                }
                Naidennie_Clienti.Add(Prom);

                for (int i = 0; i < All_Client.Count; i++)
                {
                    string[] Prom_List = new string[1];
                    Prom_List[0] = All_Client[i];
                    var Result = Prom_List.Intersect(Naidennie_Clienti);
                    if(Result.Count() == 0)
                    {
                        Vremenniy_Clnts.Children[i].IsVisible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Vremenniy_Clnts.Children.Count; i++)
                {
                    Vremenniy_Clnts.Children[i].IsVisible = false;
                }
            }
        }

        private async void Poisk_Sotr_Bt_Clicked(object sender, EventArgs e)
        {
            string Result = await DisplayPromptAsync("Поиск", "", "Ok", "", "", 40, Keyboard.Chat, "");
            if(Result.Length>0)
            {
                Poisk_Sotr_Bt.IsVisible = false;
                Sotr_And_Strelka.IsVisible = true;
                Name_Sotr_In_Grid.Text = "Поиск";
                Strelka_Sotr.Text = "<-";
                Name_Sotr.IsVisible = false;
                New_Sotr.IsVisible = false;
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("PS" + "#" + Result);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                Decoding_And_Criate_Sotr_Poisk(Msg);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ee)
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                }
            }
        }
        void Decoding_And_Criate_Sotr_Poisk(string Msg)
        {
            List<string> Naidennie_Sotr = new List<string>();
            if (Msg != "F")
            {
                string Prom = "";
                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        Naidennie_Sotr.Add(Prom);
                        Prom = "";
                    }
                }
                Naidennie_Sotr.Add(Prom);

                for (int i = 0; i < All_Sotr.Count; i++)
                {
                    string[] Prom_List = new string[1];
                    Prom_List[0] = All_Sotr[i];
                    var Result = Prom_List.Intersect(Naidennie_Sotr);
                    if (Result.Count() == 0)
                    {
                        Vremenniy_Sotr.Children[i].IsVisible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Vremenniy_Sotr.Children.Count; i++)
                {
                    Vremenniy_Sotr.Children[i].IsVisible = false;
                }
            }
        }

        private void Naity_mer_but_Clicked(object sender, EventArgs e)
        {
            string Otpravka = "";
            bool Is_No_Ok = false;
            string Prom = "";
            if (Min_Chislo_Uchastnikov.Text == null)
            {
                Otpravka += Min_Chislo_Uchastnikov.Placeholder;
            }
            else
            {
                if ((Int64.TryParse(Min_Chislo_Uchastnikov.Text, out Int64 num)))
                {
                    Otpravka += Min_Chislo_Uchastnikov.Text;
                }
                else
                {
                    Is_No_Ok = true;
                    Prom += "Некоректная минимальная дата";
                    Prom += "\n";
                    Min_Chislo_Uchastnikov.Text = null;
                    Min_Chislo_Uchastnikov.Placeholder = "0";
                }
            }
            Otpravka += "#";
            if (Max_Chislo_Uchastnikov.Text == null)
            {
                Otpravka += Max_Chislo_Uchastnikov.Placeholder;
            }
            else
            {
                if ((Int64.TryParse(Max_Chislo_Uchastnikov.Text, out Int64 num)))
                {
                    Otpravka += Max_Chislo_Uchastnikov.Text;
                }
                else
                {
                    Is_No_Ok = true;
                    Prom += "Некоректная максимальная дата";
                    Prom += "\n";
                    Max_Chislo_Uchastnikov.Text = null;
                    Max_Chislo_Uchastnikov.Placeholder = "100";
                }
            }
            Otpravka += "#";
            Otpravka += (Min_Date.Date).ToString("d.MM.yyyy");
            Otpravka += "#";
            Otpravka += Max_Date.Date.ToString("d.MM.yyyy");
            Otpravka += "#";
            if (Name_Mer_Poisk.Text == null)
            {
                Otpravka += "#";
            }
            else
            {
                Otpravka += Name_Mer_Poisk.Text;
            }
            if (Is_No_Ok==false)
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("PM" + "#" + Otpravka);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                Decoding_And_Criate_Mer_Poisk(Msg);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ee)
                {
                    DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
                }
            }
            else
            {
                DisplayAlert("Внимание",Prom,"ОК");
            }
            Name_Mer_Poisk.Text = null;
            Min_Chislo_Uchastnikov.Text = null;
            Max_Chislo_Uchastnikov.Text = null;
            Min_Date.Date = DateTime.Now.AddYears(-5);
            Min_Date.TextColor = Color.Gray;
            Max_Date.Date = DateTime.Now.AddYears(5);
            Max_Date.TextColor = Color.Gray;
        }
        void Decoding_And_Criate_Mer_Poisk(string Msg)
        {
            List<string> Naidennie_Sotr = new List<string>();
            List<string> Naidennie_Group = new List<string>();
            if (Msg != "F")
            {
                string Prom = "";
                for (int i = 0; i < Msg.Length; i++)
                {
                    if (Msg[i] != '#')
                    {
                        Prom += Msg[i];
                    }
                    else
                    {
                        Naidennie_Sotr.Add(Prom);
                        Prom = "";
                    }
                }
                Naidennie_Sotr.Add(Prom);

                for (int i = 0; i < Naidennie_Sotr.Count-1; i+=2)
                {
                    Naidennie_Group.Add(Naidennie_Sotr[i] + "#" + Naidennie_Sotr[i+1]);
                }

                for (int i = 0; i < Prom_All_Meropriatie_String_List.Count; i++)
                {
                    string[] Prom_List = new string[1];
                    Prom_List[0] = Prom_All_Meropriatie_String_List[i];
                    var Result = Prom_List.Intersect(Naidennie_Group);
                    if (Result.Count() == 0)
                    {
                        Vremenniy_Meropriatie.Children[i].IsVisible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Vremenniy_Sotr.Children.Count; i++)
                {
                    Vremenniy_Sotr.Children[i].IsVisible = false;
                }
            }
            New_Dannye_in_Mer.IsVisible = false;
            Meroptiatie_sckroll.Content = Vremenniy_Meropriatie;
        }
        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Min_Date_DateSelected(object sender, DateChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            datePicker.TextColor = Color.Purple;
        }
    }

}

