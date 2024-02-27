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
    public partial class Admin : ContentPage
    {
        TcpClient tcpClient;
        Thread Thread1;
        List<string> All_Inf_User;
        string Msg = "";
        string Name = "";
        int Change_Izm = 1;
        StackLayout Vremenniy_Clnts = new StackLayout();
        StackLayout Vremenniy_Pokupka = new StackLayout();
        StackLayout Vremenniy_Groups = new StackLayout();
        List<string> All_Client = new List<string>();
        Dictionary<Button, string> Key_Pair_Fio_Client_Button = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_Name_Group_Button = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_Name_Group_for_Nadpis_Button = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_Id_Group_for_Nadpis_Button = new Dictionary<Button, string>();
        Dictionary<string, string> Key_Pair_Id_Prepoda_Fio = new Dictionary<string, string>();
        string Fio_Clnient_For_Otpr = "";
        string Group_Id_For_Group_For_Otpr = "";
        List<string> All_Group = new List<string>();
        List<string> All_Prepod = new List<string>();
        List<string> All_Usluga = new List<string>();
        List<string> All_Pokupka = new List<string>();
        List<CheckBox> All_Group_Checkbx = new List<CheckBox>();
        Picker Picer_All_Fio = new Picker();
        public Admin(TcpClient tcpClient, List<string> Vsy_Infa)
        {
            All_Inf_User = Vsy_Infa;
            Name = Vsy_Infa[1];
            this.tcpClient = tcpClient;
            All_Client_Dec(Vsy_Infa[6]);
            Seconds_Groups(Vsy_Infa[7]);
            Prepods_fo_Groups(Vsy_Infa[8]);
            All_Uslug_Dec(Vsy_Infa[9]);
            All_Pokupka_Dec(Vsy_Infa[10]);
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        { 
            return true;
        }
        protected override void OnAppearing()
        {
            Load_Chats();
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


            Login.WidthRequest = Fifth_page.WidthRequest * 0.3;
            Password.WidthRequest = Fifth_page.WidthRequest * 0.3;
            Phon_Number.WidthRequest = Fifth_page.WidthRequest * 0.45;
            Poshta.WidthRequest = Fifth_page.WidthRequest * 0.7;
            Psewdonim.WidthRequest = Fifth_page.WidthRequest * 0.7;

            Send.WidthRequest = Third_page.WidthRequest * 0.2;
            Polle_Vvoda.WidthRequest = Third_page.WidthRequest * 0.8;

            Minus_Ugli_Na_First_Page.WidthRequest = First_page.WidthRequest;
            Box_View_On_First_Page.HeightRequest = First_page.HeightRequest * 0.1;
            Minus_Ugli_Na_Fifth_Page.WidthRequest = Fifth_page.WidthRequest;
            Box_View_On_Fifth_Page.HeightRequest = Fifth_page.HeightRequest * 0.1;
            Minus_Ugli_Na_Second_Page.WidthRequest = Second_page.WidthRequest;
            Box_View_On_Second_Page.HeightRequest = Second_page.HeightRequest * 0.1;
            Box_View_On_Third_Page.HeightRequest = Third_page.HeightRequest * 0.1;
            Minus_Ugli_Na_Third_Page.WidthRequest = Third_page.WidthRequest;


            Minus_Ugli_Na_Fouth_Page.WidthRequest = Fouth_page.WidthRequest;
            Box_View_On_Fouth_Page.HeightRequest = Fouth_page.HeightRequest * 0.1;

            FIO_Client.WidthRequest = Second_page.WidthRequest * 0.7;
            Data_Rozh.WidthRequest = Second_page.WidthRequest * 0.4;
            Pochta_Client.WidthRequest = Second_page.WidthRequest * 0.7;
            Phone_Number_Client.WidthRequest = Second_page.WidthRequest * 0.35;
            Login_Client.WidthRequest = Second_page.WidthRequest * 0.3;
            Password_Client.WidthRequest = Second_page.WidthRequest * 0.3;
            Psevldonim_Client.WidthRequest = Second_page.WidthRequest * 0.3;
            Create_Spisok_KLient();
            Create_Spisok_Group("Y");
            Decoding_And_Create_Pokupka();
            Start_Text_On_Fifth_Page(All_Inf_User[2], All_Inf_User[3], All_Inf_User[1], All_Inf_User[4], All_Inf_User[5]);

        }
        void Start_Text_On_Fifth_Page(string Phone, string Pochta, string Name, string _Login, string _Passsword)
        {
            Phon_Number.Placeholder = Phone;
            Poshta.Placeholder = Pochta;
            Psewdonim.Placeholder = Name;
            Login.Placeholder = _Login;
            Password.Placeholder = _Passsword;
            Login.WidthRequest = Fifth_page.WidthRequest * 0.3;
            Password.WidthRequest = Fifth_page.WidthRequest * 0.3;
            Phon_Number.WidthRequest = Fifth_page.WidthRequest * 0.45;
            Poshta.WidthRequest = Fifth_page.WidthRequest * 0.7;
            Psewdonim.WidthRequest = Fifth_page.WidthRequest * 0.7;
        }
        void All_Pokupka_Dec(string Str)
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
                        All_Pokupka.Add(Prom);
                        Prom = "";
                    }
                }
                All_Pokupka.Add(Prom);
            }
        }
        void Decoding_And_Create_Pokupka()
        {
            for (int i = 0; i < All_Pokupka.Count - 2; i += 3) //группа - дата - время
            {
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                };

                promezutocniy.Children.Add(new Label()
                {
                    Text = (All_Pokupka[i])+"    "+ All_Pokupka[i+1],
                    TextColor = Color.Purple,
                    FontSize = 15,
                    VerticalOptions = LayoutOptions.StartAndExpand
                });
                promezutocniy.Children.Add(new Label()
                {
                    Text = All_Pokupka[i + 2],
                    TextColor = Color.Purple,
                    FontSize = 15,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand

                });

                Vremenniy_Pokupka.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,
                    Content = promezutocniy
                });
            }
            Pokupki_sckroll.Content = Vremenniy_Pokupka;
        }
        private void Strelka_Pokupka_Clicked(object sender, EventArgs e)
        {
            Pokupka_And_Strelka.IsVisible = false;
            Name_Pokupka.IsVisible = true;
            Pokupki_sckroll.Content = Vremenniy_Pokupka ;//Vremenniy_Groups;
            StackLayout_In_Scroll_Pokupok.Children.Clear();
            Name_Pokupka.IsVisible = true;
            Soxr_Inf_Ab_Pokupka.IsVisible = false;
            New_Pokupka.IsVisible = true;
            Fio_Client_In_Dobav.Items.Clear();
        }
        private void Soxr_Inf_Ab_Pokupka_Clicked(object sender, EventArgs e)
        {
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("NO#" + Fio_Client_In_Dobav.SelectedItem+"#"+ Name_Usluga_In_Dobav_Oplata.SelectedItem);
                networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
                string Msg = "";
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[32000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        if (Msg[0] != '~')
                        {
                            if (Msg[0] == 'F')
                            {
                                DisplayAlert("Ошибка", "Клиент уже оплатил тренировку", "Ок");
                                break;
                            }
                            else if(Msg[0] == 'S')
                            {
                                StackLayout promezutocniy = new StackLayout()
                                {
                                    Orientation = StackOrientation.Horizontal,
                                };

                                promezutocniy.Children.Add(new Label()
                                {
                                    Text = Fio_Client_In_Dobav.SelectedItem + "    " + Name_Usluga_In_Dobav_Oplata.SelectedItem,
                                    TextColor = Color.Purple,
                                    FontSize = 15,
                                    VerticalOptions = LayoutOptions.StartAndExpand
                                });
                                promezutocniy.Children.Add(new Label()
                                {
                                    Text = DateTime.Now.ToString("d MMMM yyyy"),
                                    TextColor = Color.Purple,
                                    FontSize = 15,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.EndAndExpand

                                });

                                Vremenniy_Pokupka.Children.Add(new Frame()
                                {
                                    BorderColor = Color.Purple,
                                    CornerRadius = 20,
                                    Content = promezutocniy
                                });
                                DisplayAlert("Данные успешно добавлены", "", "Ок");
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
            Name_Usluga_In_Dobav_Oplata.SelectedItem = null;
            Name_Usluga_In_Dobav_Oplata.Items.Clear();
            Name_Usluga_In_Dobav_Oplata.IsEnabled = false;
            Fio_Client_In_Dobav.SelectedItem = null;
            Soxr_Inf_Ab_Pokupka.IsEnabled = false;
            Soxr_Inf_Ab_Pokupka.BackgroundColor = Color.Gray;
            Name_Usluga_In_Dobav_Oplata.Items.Clear();
        }

        private void New_Pokupka_Clicked(object sender, EventArgs e)
        {
            Box_View_On_Second_Page.HeightRequest = Second_page.HeightRequest * 0.1;
            Pokupka_And_Strelka.IsVisible = true;
            Strelka_Pokupka.Text = "<-";
            Name_Pokupka.IsVisible = false;
            New_In_Scroll_Pokupok.IsVisible = true;
            Pokupki_sckroll.Content = New_In_Scroll_Pokupok;
            //Name_Pokupka_In_Grid.Text = "Добавление";
            Soxr_Inf_Ab_Pokupka.IsVisible = true;
            New_Pokupka.IsVisible = false;
            for (int i = 0; i < All_Client.Count ; i ++)
            {
                Fio_Client_In_Dobav.Items.Add(All_Client[i]);
            }
        }
        private void Name_Usluga_In_Dobav_Oplata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Name_Usluga_In_Dobav_Oplata.SelectedItem != null)
            {
                Soxr_Inf_Ab_Pokupka.IsEnabled = true;
                Soxr_Inf_Ab_Pokupka.BackgroundColor = Color.Purple;
            }
        }

        private void Fio_Client_In_Dobav_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Fio_Client_In_Dobav.SelectedItem!=null)
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("CT#"+ Fio_Client_In_Dobav.SelectedItem);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[32000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            if (Msg[0] != '~')
                            {  
                                if (Msg[0] == 'F')
                                {
                                    DisplayAlert("Внимание", "У данного клиента сегодня нет занятий", "Ок");
                                    Fio_Client_In_Dobav.SelectedItem = null;
                                    Name_Usluga_In_Dobav.Items.Clear();
                                    break;
                                }
                                else
                                {
                                    Name_Usluga_In_Dobav_Oplata.Items.Clear();
                                    Name_Usluga_In_Dobav_Oplata.IsEnabled = true;
                                    if (Msg != "F")
                                    {
                                        string Prom = "";
                                        for (int i = 0; i < Msg.Length; i++)
                                        {
                                            if (Msg[i] == '~')
                                                break;
                                            if (Msg[i] != '#')
                                            {
                                                Prom += Msg[i];
                                            }
                                            else
                                            {
                                                Name_Usluga_In_Dobav_Oplata.Items.Add(Prom);
                                                Prom = "";
                                            }
                                        }
                                        Name_Usluga_In_Dobav_Oplata.Items.Add(Prom);
                                    }
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
        void All_Uslug_Dec(string Str)
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
                        All_Usluga.Add(Prom);
                        Prom = "";
                    }
                }
                All_Usluga.Add(Prom);
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
                        All_Group.Add(Prom);
                        Prom = "";
                    }
                }
                All_Group.Add(Prom);
            }
        }
        void Prepods_fo_Groups(string Str)
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
                        All_Prepod.Add(Prom);
                        Prom = "";
                    }
                }
                All_Prepod.Add(Prom);
            }
        }
        void Create_Spisok_Group(string X)
        {
            Key_Pair_Name_Group_for_Nadpis_Button.Clear();
            Key_Pair_Name_Group_Button.Clear();
            Key_Pair_Id_Group_for_Nadpis_Button.Clear();
            Vremenniy_Groups.Children.Clear();
            for (int i = 0; i < All_Group.Count - 2; i += 3)
            {
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };

                promezutocniy.Children.Add(new Label()
                {
                    Text = "Группа №" + All_Group[i] + " " + All_Group[i + 2],
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                });
                Button button = new Button()
                {
                    Text = "Изменить преподавателя",
                    BackgroundColor = Color.White,
                    TextColor = Color.Purple,
                    FontSize = 10,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };
                button.Clicked += Inf_Of_Group_By_Knopka;
                promezutocniy.Children.Add(button);
                Key_Pair_Name_Group_Button.Add(button, All_Group[i + 1]);
                Key_Pair_Name_Group_for_Nadpis_Button.Add(button, "Группа №" + All_Group[i] + " " + All_Group[i + 2]);
                Key_Pair_Id_Group_for_Nadpis_Button.Add(button, All_Group[i]);
                Vremenniy_Groups.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,
                    Content = promezutocniy,

                });
            }
            if (X == "Y")
            {
                Group_sckroll.Content = Vremenniy_Groups;
            }
            Fio_Teachers_In_Dobav.Items.Clear();
            Key_Pair_Id_Prepoda_Fio.Clear();
            for (int i = 0; i < All_Prepod.Count - 1; i += 2)
            {
                Fio_Teachers_In_Dobav.Items.Add(All_Prepod[i]);
                Key_Pair_Id_Prepoda_Fio.Add(All_Prepod[i], All_Prepod[i + 1]);
            }
            //Vremenniy_User_Inf = StackLayout_In_Scroll_Groups;
        }
        private void Strelka_Group_Clicked(object sender, EventArgs e)
        {
            Group_And_Strelka.IsVisible = false;
            Name_Group.IsVisible = true;
            Group_sckroll.Content = Vremenniy_Groups;
            StackLayout_In_Scroll_Group_sckroll.Children.Clear();
            New_Group.IsVisible = true;
            Soxr_Ism_Ab_Group.IsVisible = false;
            Soxr_Inf_Ab_Group.IsVisible = false;
            Picer_All_Fio.Items.Clear();
        }

        private void New_Group_Clicked(object sender, EventArgs e)
        {
            Box_View_On_Third_Page.HeightRequest = Second_page.HeightRequest * 0.1;
            Group_And_Strelka.IsVisible = true;
            Strelka_Group.Text = "<-";
            Name_Group.IsVisible = false;
            New_Group_Stacklayout.IsVisible = true;
            Group_sckroll.Content = New_Group_Stacklayout;
            Name_Group_In_Grid.Text = "Добавление группы";
            Soxr_Inf_Ab_Group.IsVisible = true;
            New_Group.IsVisible = false;
            //for (int i = 0; i < All_Usluga.Count-1; i+=2)
            //{
            //    Name_Usluga_In_Dobav.Items.Add(All_Usluga[i]);
            //}
            //for (int i = 0; i < All_Prepod.Count-1; i +=2)
            //{
            //    Fio_Teachers_In_Dobav.Items.Add(All_Prepod[i]);
            //    Key_Pair_Id_Prepoda_Fio.Add(All_Prepod[i], All_Prepod[i + 1]);
            //}
        }
        private void Fio_Teachers_and_Usluga_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Fio_Teachers_In_Dobav.SelectedItem != null)&&(Name_Usluga_In_Dobav.SelectedItem != null))
            {
                Soxr_Inf_Ab_Group.IsEnabled = true;
                Soxr_Inf_Ab_Group.BackgroundColor = Color.Purple;
            }

        }

        private void Soxr_Ism_Ab_Group_Clicked(object sender, EventArgs e)
        {
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("TG#"+Group_Id_For_Group_For_Otpr+"#"+(Picer_All_Fio.SelectedIndex+1).ToString());
                networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
                string Msg = "";
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[32000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        if (Msg[0] != '~')
                        {
                            if (Msg[0] == 'S')
                            {
                                All_Group[3 * Convert.ToInt32(Group_Id_For_Group_For_Otpr) - 2] = Key_Pair_Id_Prepoda_Fio[(Picer_All_Fio.SelectedItem).ToString()];
                                Create_Spisok_Group("F");
                                DisplayAlert("Данные успешно добавлены", "", "Ок");//г/п/ну 0 1 2
                                                                                          //1 1 dfdf
                                break;
                            }
                            else if (Msg[0] == 'F')
                            {
                                DisplayAlert("Ошибка", "", "Ок");
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
            Soxr_Ism_Ab_Group.IsEnabled = false;
            Soxr_Ism_Ab_Group.BackgroundColor = Color.Gray;
        }

        private void Soxr_Inf_Ab_Group_Clicked(object sender, EventArgs e)
        {
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("NG#" + Fio_Teachers_In_Dobav.SelectedItem + "#" + Name_Usluga_In_Dobav.SelectedItem);
                networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
                string Msg = "";
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[32000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        if (Msg[0] != '~')
                        {
                            if (Msg[0] == 'S')
                            {
                                All_Group.Add((All_Group.Count/3+1).ToString());
                                All_Group.Add((Key_Pair_Id_Prepoda_Fio[Fio_Teachers_In_Dobav.SelectedItem.ToString()]).ToString());
                                All_Group.Add(Name_Usluga_In_Dobav.SelectedItem.ToString());
                                StackLayout promezutocniy = new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical,
                                };

                                promezutocniy.Children.Add(new Label()
                                {
                                    Text = "Группа №" + All_Group[All_Group.Count - 3] + " " + All_Group[All_Group.Count - 1],
                                    TextColor = Color.Purple,
                                    FontSize = 20,
                                    VerticalOptions = LayoutOptions.StartAndExpand,
                                    HorizontalOptions = LayoutOptions.StartAndExpand
                                });
                                Button button = new Button()
                                {
                                    Text = "Изменить преподавателя",
                                    BackgroundColor = Color.White,
                                    TextColor = Color.Purple,
                                    FontSize = 10,
                                    VerticalOptions = LayoutOptions.StartAndExpand,
                                    HorizontalOptions = LayoutOptions.EndAndExpand
                                };
                                button.Clicked += Inf_Of_Group_By_Knopka;
                                promezutocniy.Children.Add(button);
                                Key_Pair_Name_Group_Button.Add(button, All_Group[All_Group.Count - 2]);
                                Key_Pair_Name_Group_for_Nadpis_Button.Add(button, "Группа №" + All_Group[All_Group.Count - 3] + " " + All_Group[All_Group.Count - 1]);
                                Key_Pair_Id_Group_for_Nadpis_Button.Add(button, All_Group[All_Group.Count - 3]);
                                Vremenniy_Groups.Children.Add(new Frame()
                                {
                                    BorderColor = Color.Purple,
                                    CornerRadius = 20,
                                    Content = promezutocniy,

                                });
                                DisplayAlert("Данные успешно добавлены", "", "Ок");
                                break;
                            }
                            else if (Msg[0] == 'F')
                            {
                                DisplayAlert("Ошибка", "", "Ок");
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
            Soxr_Inf_Ab_Group.IsEnabled = false;
            Soxr_Inf_Ab_Group.BackgroundColor = Color.Gray;
            Fio_Teachers_In_Dobav.SelectedItem = null;
            Name_Usluga_In_Dobav.SelectedItem = null;
        }

        private void Inf_Of_Group_By_Knopka(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Group_And_Strelka.IsVisible = true;
            Name_Group_In_Grid.Text = Key_Pair_Name_Group_for_Nadpis_Button[button];
            //StackLayout_In_Scroll_Group_sckroll.IsVisible = true;
            Strelka_Group.Text = "<-";
            Name_Group.IsVisible = false;
            New_Group.IsVisible = false;
            Box_View_On_Third_Page.HeightRequest = Second_page.HeightRequest * 0.1;
            Soxr_Ism_Ab_Group.IsVisible = true;
            Group_Id_For_Group_For_Otpr = Key_Pair_Id_Group_for_Nadpis_Button[button];
            StackLayout promezutocniy = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
            };
            promezutocniy.Children.Add(new Label()
            {
                Text = "Преподаватель",
                TextColor = Color.Purple,
                FontSize = 20,
                VerticalOptions = LayoutOptions.CenterAndExpand
            });
            Picer_All_Fio.TextColor = Color.Purple;
            Picer_All_Fio.HorizontalOptions = LayoutOptions.EndAndExpand;
            Picer_All_Fio.HorizontalTextAlignment = TextAlignment.Center;
            Picer_All_Fio.WidthRequest = Third_page.WidthRequest * 0.4;
            Picer_All_Fio.SelectedIndexChanged += Picer_All_Fio_SelectedIndexChanged;
            for (int i = 0; i <All_Prepod.Count-1; i+=2)
            {
                Picer_All_Fio.Items.Add(All_Prepod[i]);
            }
            string x = Key_Pair_Name_Group_Button[button];
            Picer_All_Fio.SelectedIndex = (All_Prepod.IndexOf(Key_Pair_Name_Group_Button[button])-1)/2;
            Soxr_Ism_Ab_Group.IsEnabled = false;
            Soxr_Ism_Ab_Group.BackgroundColor = Color.Gray;
            promezutocniy.Children.Add(Picer_All_Fio);
            StackLayout_In_Scroll_Group_sckroll.Children.Add(new Frame()
            {
                BorderColor = Color.Purple,
                CornerRadius = 20,
                Content = promezutocniy
            });
            Group_sckroll.Content = StackLayout_In_Scroll_Group_sckroll;
        }

        private void Picer_All_Fio_SelectedIndexChanged(object sender, EventArgs e)
        {
            Soxr_Ism_Ab_Group.IsEnabled = true;
            Soxr_Ism_Ab_Group.BackgroundColor = Color.Purple;
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
        void Create_Spisok_KLient()
        {

            for (int i = 0; i < All_Client.Count; i++)
            {
                All_Client[i] = All_Client[i].Remove(All_Client[i].Length - 1);
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };

                promezutocniy.Children.Add(new Label()
                {
                    Text = (All_Client[i]),
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
                button.Clicked += Inf_Of_User_By_Knopka;
                promezutocniy.Children.Add(button);
                Key_Pair_Fio_Client_Button.Add(button, All_Client[i]);
                Vremenniy_Clnts.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,
                    Content = promezutocniy,

                });
            }
            Client_sckroll.Content = Vremenniy_Clnts;
            //Vremenniy_User_Inf = StackLayout_In_Scroll_Groups;
        }
        private void Inf_Of_User_By_Knopka(object sender, EventArgs e)
        {
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
                        byte[] bytes1 = new byte[32000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        //Massege.Add(Msg);
                        if (Msg[0] != '~')
                        {
                            //Del_Client.IsVisible = true;
                            Box_View_On_First_Page.HeightRequest = First_page.HeightRequest * 0.15;
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
        private void Strelka_Client_Clicked(object sender, EventArgs e)
        {
            Cliets_And_Strelka.IsVisible = false;
            Name_Client.IsVisible = true;
            Client_sckroll.Content = Vremenniy_Clnts;
            StackLayout_In_Scroll.Children.Clear();
            New_Client.IsVisible = true;
            All_Group_Checkbx.Clear();
            Soxr_Ism_Ab_Clint.IsVisible = false;
            Soxr_Inf_Ab_Clint.IsVisible = false;
            Box_View_On_First_Page.HeightRequest = First_page.HeightRequest * 0.1;
            Name_Client_In_Grid.Text = "Просмотр информации";
        }

        void Create_Page_About_Student(string Msg, string Name)
        {
            Box_View_On_First_Page.HeightRequest = Second_page.HeightRequest * 0.35;
            New_Client.IsVisible = false;
            Soxr_Ism_Ab_Clint.IsVisible = true;
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
                for (int i = 0; i < All_Group.Count - 2; i += 3)
                {
                    StackLayout promezutocniy_mini = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                    };
                    promezutocniy_mini.Children.Add(new Label()
                    {
                      Text = "Группа №" + All_Group[i] + " " + All_Group[i + 2],
                      TextColor = Color.Purple,
                      FontSize = 20,
                      VerticalOptions = LayoutOptions.StartAndExpand,
                      HorizontalOptions = LayoutOptions.StartAndExpand
                    });
                    CheckBox check = new CheckBox()
                    {
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        HorizontalOptions = LayoutOptions.EndAndExpand
                    };
                    if(Massege.Contains(All_Group[i]))
                    {
                        check.IsChecked = true;
                    }
                    if (Massege[2] == "F")
                    {
                        check.IsEnabled = false;
                    }
                    check.CheckedChanged += OnCheckBoxCheckedChanged;
                    All_Group_Checkbx.Add(check);
                    promezutocniy_mini.Children.Add(check);
                    promezutocniy.Children.Add(new Frame()
                    {
                        BorderColor = Color.Purple,
                        CornerRadius = 20,
                        Content = promezutocniy_mini
                    });
                }
                StackLayout_In_Scroll.Children.Add(new ScrollView()
                {
                    Content = promezutocniy
                });
            }
            Client_sckroll.Content = StackLayout_In_Scroll;

        }
        private void New_Client_Clicked(object sender, EventArgs e)
        {
            Name_Client_In_Grid.Text = "Добавление клиента";
            Box_View_On_First_Page.HeightRequest = Second_page.HeightRequest * 0.2;
            //Box_View_On_First_Page.HeightRequest = Second_page.HeightRequest * 0.2;
            Cliets_And_Strelka.IsVisible = true;
            Strelka_Client.Text = "<-";
            Name_Client.IsVisible = false;
            New_Client_StackLayout.IsVisible = true;
            Client_sckroll.Content = New_Client_StackLayout;
            Soxr_Inf_Ab_Clint.IsVisible = true;
            New_Client.IsVisible = false;
        }

        private void Name_Client_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((FIO_Client.Text != null)  || (Data_Rozh.Text != null) ||  (Pochta_Client.Text != null) ||
                (Phone_Number_Client.Text != null) || (Login_Client.Text != null) || (Password_Client.Text != null) || (Psevldonim_Client.Text != null))
            {
                Soxr_Inf_Ab_Clint.IsEnabled = true;
                Soxr_Inf_Ab_Clint.BackgroundColor = Color.Purple;
            }
        }

        private void Soxr_Inf_Ab_Clint_Clicked(object sender, EventArgs e)
        {
            bool Is_No_Ok = false;
            string Prom = "";
            if (FIO_Client.Text == null)
            {
                Is_No_Ok = true;
                Prom += "Некоректные ФИО";
                Prom += "\n";
            }
            else if ((FIO_Client.Text != null) && (FIO_Client.Text.Length > 0))
            {
                string x = FIO_Client.Text;
                string v = x.Trim();
                if (v == "")
                {
                    Is_No_Ok = true;
                    Prom += "Некоректные ФИО";
                    Prom += "\n";
                    FIO_Client.Text = null;
                }
            }
            if ((Data_Rozh.Text == null) || ((Data_Rozh.Text != null) && (!DateTime.TryParseExact(Data_Rozh.Text, "d", new CultureInfo("en-US"), DateTimeStyles.AllowLeadingWhite, out DateTime result))))
            {
                Is_No_Ok = true;
                Prom += "Некоректная дата";
                Prom += "\n";
                Data_Rozh.Text = null;
            }
            if ((Pochta_Client.Text == null) || ((Pochta_Client.Text != null) && (!Pochta_Client.Text.Contains('@') || (!Pochta_Client.Text.Contains('.')))))
            {
                Is_No_Ok = true;
                Prom += "Некоректная почта";
                Prom += "\n";
                Pochta_Client.Text = null;
            }
            if ((Phone_Number_Client.Text == null) || ((Phone_Number_Client.Text != null) && ((Phone_Number_Client.Text.Length != 11) || (!Int64.TryParse(Phone_Number_Client.Text, out Int64 num2)))))
            {
                Is_No_Ok = true;
                Prom += "Некоректный номер";
                Prom += "\n";
                Phone_Number_Client.Text = null;
            }
            if (Login_Client.Text == null)
            {
                Is_No_Ok = true;
                Prom += "Некоректный логин";
                Prom += "\n";
            }
            else
            {
                string Proverka1 = Login_Client.Text;
                string Proverka2 = Proverka1.Trim();
                if (Proverka2.Length != 6)
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный логин";
                    Prom += "\n";
                    Login_Client.Text = null;
                }
            }
            if (Password_Client.Text == null)
            {
                Is_No_Ok = true;
                Prom += "Некоректный пароль";
                Prom += "\n";
            }
            else
            {
                string Proverka1 = Password_Client.Text;
                string Proverka2 = Proverka1.Trim();
                if (Proverka2.Length != 6)
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный пароль";
                    Prom += "\n";
                    Password_Client.Text = null;
                }
            }

            if (Psevldonim_Client.Text == null)
            {
                Is_No_Ok = true;
                Prom += "Некоректный псевдоним";
            }
            else if ((Psevldonim_Client.Text != null) && (Psevldonim_Client.Text.Length > 0))
            {
                string x = Psevldonim_Client.Text;
                string v = x.Trim();
                if (v == "")
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный псевдоним";
                    Psevldonim_Client.Text = null;
                }
            }


            Soxr_Inf_Ab_Clint.BackgroundColor = Color.Gray;
            Soxr_Inf_Ab_Clint.TextColor = Color.White;
            Soxr_Inf_Ab_Clint.IsEnabled = false;

            if (Is_No_Ok == true)
            {
                DisplayAlert("Внимание", Prom, "Ок");
            }
            else
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("NK" + "#" + FIO_Client.Text + "#" + Data_Rozh.Text + "#"  + Phone_Number_Client.Text + "#" + Pochta_Client.Text + "#" + Login_Client.Text + "#" + Password_Client.Text + "#" + Psevldonim_Client.Text);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[32000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            //Massege.Add(Msg);
                            if (Msg[0] != '~')
                            {
                                if (Msg[0] == 'S')
                                {
                                    DisplayAlert("Данные успешно добавлены", "", "Ок");
                                    StackLayout promezutocniy = new StackLayout()
                                    {
                                        Orientation = StackOrientation.Vertical,
                                    };

                                    promezutocniy.Children.Add(new Label()
                                    {
                                        Text = (FIO_Client.Text),
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
                                    button.Clicked += Inf_Of_User_By_Knopka;
                                    promezutocniy.Children.Add(button);
                                    Key_Pair_Fio_Client_Button.Add(button, FIO_Client.Text);
                                    Vremenniy_Clnts.Children.Add(new Frame()
                                    {
                                        BorderColor = Color.Purple,
                                        CornerRadius = 20,
                                        Content = promezutocniy,

                                    });
                                    FIO_Client.Text = null;
                                    Data_Rozh.Text = null;
                                    Pochta_Client.Text = null;
                                    Phone_Number_Client.Text = null;
                                    Login_Client.Text = null;
                                    Password_Client.Text = null;
                                    Psevldonim_Client.Text = null;
                                    break;
                                }
                                else if (Msg[0] == 'F')
                                {
                                    DisplayAlert("Ошибка", "Логин занят", "Ок");
                                    Login_Client.Text = null;
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

        private void Soxr_Ism_Ab_Clint_Clicked(object sender, EventArgs e)
        {
            try
            {
                string Groups_For_Otpravka = "";
                for (int i = 0; i < All_Group_Checkbx.Count; i++)
                {
                    if(All_Group_Checkbx[i].IsChecked==true)
                    {
                        Groups_For_Otpravka += (i + 1).ToString();
                        Groups_For_Otpravka += "#";
                    }
                }
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes;
                if (Groups_For_Otpravka.Length>0)
                {
                    Groups_For_Otpravka = Groups_For_Otpravka.Remove(Groups_For_Otpravka.Length - 1);
                    bytes = Encoding.Unicode.GetBytes("SG#" + Fio_Clnient_For_Otpr + "#" + Groups_For_Otpravka);
                    networkStream.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    bytes = Encoding.Unicode.GetBytes("SG#" + Fio_Clnient_For_Otpr);
                    networkStream.Write(bytes, 0, bytes.Length);
                }
                 //(Login.Text + "#" + Password.Text);
                networkStream.Flush();
                string Msg = "";
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[32000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        if (Msg[0] != '~')
                        {
                            if (Msg[0] == 'S')
                            {
                                DisplayAlert("Данные успешно добавлены", "", "Ок");
                                break;
                            }
                            else if(Msg[0] == 'F')
                            {
                                DisplayAlert("Ошибка", "", "Ок");
                                break;
                            }
                        }
                        networkStream1.Flush();
                    }
                }
                Soxr_Ism_Ab_Clint.IsEnabled = false;
                Soxr_Ism_Ab_Clint.BackgroundColor = Color.Gray;
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
        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Soxr_Ism_Ab_Clint.IsEnabled = true;
            Soxr_Ism_Ab_Clint.IsVisible = true;
            Soxr_Ism_Ab_Clint.BackgroundColor = Color.Purple;
        }
        void Load_Chats()
        {
            try
            {
                string Msg = "";
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("MMG0");//(Login.Text + "#" + Password.Text);
                networkStream.Write(bytes, 0, bytes.Length);
                networkStream.Flush();
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[2048];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        //Massege.Add(Msg);
                        if (Msg[0] != '~')
                        {
                            Criate_Dilog(Msg);
                            networkStream1.Flush();
                            break;
                        }
                    }
                }
            }
            catch
            {
                DisplayAlert("Ошибка", "Попробуйте позже", "Ок");
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
                    if(Massege[i][0]=='А')
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
                    byte[] bytes = Encoding.Unicode.GetBytes("MMS0#A" + "#" + Polle_Vvoda.Text);//(Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    string Msg = "";
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            Msg = "";
                            byte[] bytes1 = new byte[32000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                            //Massege.Add(Msg);
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
        }//смена отправителя

        private void Save_Change_In_Profil_Clicked(object sender, EventArgs e)
        {
            bool Is_No_Ok = false;
            string Prom = "";
            if ((Phon_Number.Text != null) && ((Phon_Number.Text.Length != 11) || (!Int64.TryParse(Phon_Number.Text, out Int64 num))))
            {
                Is_No_Ok = true;
                Prom += "Некоректный номер";
                Prom += "\n";
                Phon_Number.Text = null;
                Phon_Number.Placeholder = All_Inf_User[2];
            }
            if ((Login.Text != null))
            {
                string Proverka1 = Login.Text;
                string Proverka2 = Proverka1.Trim();
                if (Proverka2.Length != 6)
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный логин";
                    Prom += "\n";
                    Login.Text = null;
                    Login.Placeholder = All_Inf_User[4];
                }
            }
            if ((Password.Text != null))
            {
                string Proverka1 = Password.Text;
                string Proverka2 = Proverka1.Trim();
                if (Proverka2.Length != 6)
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный пароль";
                    Prom += "\n";
                    Password.Text = null;
                    Password.Placeholder = All_Inf_User[5];
                }
            }

            if ((Poshta.Text != null) && (!Poshta.Text.Contains('@') && (!Poshta.Text.Contains('.'))))
            {
                Is_No_Ok = true;
                Prom += "Некоректная почта";
                Prom += "\n";
                Poshta.Text = null;
                Poshta.Placeholder = All_Inf_User[3];
            }
            if ((Psewdonim.Text != null) && (Psewdonim.Text.Length > 0))
            {
                string x = Psewdonim.Text;
                string v = x.Trim();
                if (v == "")
                {
                    Is_No_Ok = true;
                    Prom += "Некоректный псевдоним";
                    Psewdonim.Text = null;
                    Psewdonim.Placeholder = All_Inf_User[1];
                }
            }


            Save_Change_In_Profil.BackgroundColor = Color.Gray;
            Save_Change_In_Profil.TextColor = Color.White;
            Save_Change_In_Profil.IsEnabled = false;

            if (Is_No_Ok == true)
            {
                DisplayAlert("Внимание", Prom, "Ок");
            }
            else
            {
                string Prom_Str_LP = "ST" + All_Inf_User[0];
                if (Login.Text != null)
                {
                    Change_Izm = Change_Izm * 2;
                    Prom_Str_LP += "#SL#" + Login.Text;
                }
                if (Password.Text != null)
                {
                    Change_Izm = Change_Izm * 3;
                    Prom_Str_LP += "#SK#" + Password.Text;
                }
                if (Prom_Str_LP.Length > 16)
                {
                    Send_Changes(Prom_Str_LP + "#TE");
                }

                string Obzh_Na_Otpravka = "ST" + All_Inf_User[0];
                if (Psewdonim.Text != null)
                {
                    Change_Izm = Change_Izm * 5;
                    Obzh_Na_Otpravka += "#SN#" + Psewdonim.Text;
                }
                if (Phon_Number.Text != null)
                {
                    Change_Izm = Change_Izm * 7;
                    Obzh_Na_Otpravka += "#SF#" + Phon_Number.Text;
                }
                if (Poshta.Text != null)
                {
                    Change_Izm = Change_Izm * 11;
                    Obzh_Na_Otpravka += "#SP#" + Poshta.Text;
                }
                if (Obzh_Na_Otpravka.Length > 16)
                {
                    Send_Changes(Obzh_Na_Otpravka + "#TE");
                }
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
                Msg = "";
                while (true)
                {
                    if (tcpClient.Available > 0)
                    {
                        Msg = "";
                        byte[] bytes1 = new byte[32000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        //Massege.Add(Msg);
                        if (Msg[0] == 'S')
                        {
                            if (Change_Izm % 5 == 0)
                            {
                                Psewdonim.Placeholder = Psewdonim.Text;
                                Psewdonim.Text = null;
                            }
                            if (Change_Izm % 7 == 0)
                            {
                                Phon_Number.Placeholder = Phon_Number.Text;
                                Phon_Number.Text = null;
                            }
                            if (Change_Izm % 11 == 0)
                            {
                                Poshta.Placeholder = Poshta.Text;
                                Poshta.Text = null;
                            }
                            DisplayAlert("Данные успешно изменены", "", "Ок");
                            break;
                        }
                        else if (Msg[0] == 'F')
                        {
                            Poshta.Text = null;
                            Phon_Number.Text = null;
                            Psewdonim.Text = null;
                            DisplayAlert("Ошибка", "Попробуйте снова", "Ок");
                            break;
                        }
                        else if (Msg[0] == 'E')
                        {
                            Login.Text = null;
                            DisplayAlert("Ошибка", "Логин уже используются", "Ок");
                            break;
                        }
                        else if (Msg[0] == 'G')
                        {
                            if ((Change_Izm % 2 == 0) && (Change_Izm % 3 == 0))
                            {
                                DisplayAlert("Логин и пароль успешно изменены", "", "Ок");
                                Password.Placeholder = Password.Text;
                                Password.Text = null;
                                Login.Placeholder = Login.Text;
                                Login.Text = null;
                            }
                            else
                            {
                                if (Change_Izm % 2 == 0)
                                {
                                    DisplayAlert("Логин успешно изменен", "", "Ок");
                                    Login.Placeholder = Login.Text;
                                    Login.Text = null;
                                }
                                if (Change_Izm % 3 == 0)
                                {
                                    DisplayAlert("Пароль успешно изменен", "", "Ок");
                                    Password.Placeholder = Password.Text;
                                    Password.Text = null;
                                }
                            }
                            break;
                        }
                        networkStream1.Flush();
                    }
                }
            }
            catch
            {
                DisplayAlert("Ошибка", "Сервер прилег, поднимаем", "Ок");
                if (!Thread1.IsAlive)
                {
                    Thread1.Start();
                }
            }
            Save_Change_In_Profil.IsEnabled = false;
            Save_Change_In_Profil.BackgroundColor = Color.Gray;
            All_Inf_User[0] = "#" + Login.Placeholder + "#" + Password.Placeholder;
            Change_Izm = 1;
        }
        private void Exit_From_Prilozh_Clicked(object sender, EventArgs e)
        {
            // tcpClient.Close();
            System.Environment.Exit(0);
        }

        private void Exit_From_Profil_Clicked(object sender, EventArgs e)
        {
            tcpClient.Close();
            //Name = "";
            //Rsspis_String_List.Clear();
            //Pokupka_Sting_Lst.Clear();
            //Proverka_Na_Zagruzku.Clear();
            //All_Inf_User.Clear();
            //All_Meropriatie_String_List.Clear();
            //User_Meropriatie_String_List.Clear();
            Navigation.PopModalAsync();
        }

        private void Obsh_TextChanged(object sender, TextChangedEventArgs e)
        {
            Save_Change_In_Profil.BackgroundColor = Color.Purple;
            Save_Change_In_Profil.IsEnabled = true;
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

    }
}
