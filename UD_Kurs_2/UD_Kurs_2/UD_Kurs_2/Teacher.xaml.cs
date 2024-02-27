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

namespace UD_Kurs_2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Teacher : ContentPage
    {
        string FIO_Sotr = "";
        public bool Is_Connect = false;
        TcpClient tcpClient;
        Thread Thread1;
        string Msg = "";
        string Name = "";
        List<string> Rsspis_String_List = new List<string>();
        List<string> Pokupka_Sting_Lst = new List<string>();
        List<bool> Proverka_Na_Zagruzku = new List<bool>()
        {true,false,false,false,false};
        List<string> All_Inf_User;
        List<string> Prom_All_Meropriatie_String_List = new List<string>();
        List<string> All_Meropriatie_String_List = new List<string>();
        List<string> User_Meropriatie_String_List = new List<string>();
        List<string> User_Groups_String_List = new List<string>();
        List<string> Id_Groups_List = new List<string>();
       // List<string> Id_Chats_List = new List<string>();
        List<string> Massege = new List<string>();
        StackLayout Vremenniy_Group = new StackLayout();
        StackLayout Vremenniy_User_Inf = new StackLayout();
        StackLayout Vremenniy_Meropriatie = new StackLayout();
        StackLayout Vremenniy_Chats = new StackLayout();
        Dictionary<Button, string> Key_Pair_Group = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_Chats = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_Fio_Button = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_For_Meroproytie_Big_String_All = new Dictionary<Button, string>();
        string Type_Send_Chat = "";
        int Type_Strelka_Group = 0;
        int Change_Izm = 1;
        public Teacher(TcpClient tcpClient, List<string> Vsy_Infa)
        {
            this.tcpClient = tcpClient;
            All_Inf_User = Vsy_Infa;
            Name = Vsy_Infa[1];
            First_Rasspis(Vsy_Infa[2]);
            Seconds_Groups(Vsy_Infa[7]);
            Third_All_Meropriatie(Vsy_Infa[8]);
            FIO_Sotr = Vsy_Infa[9];
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected override void OnAppearing()
        {
            First_Text_Appoaring(Name);
            Decoding_And_Criate_Choldren();

            Send.WidthRequest = Third_page.WidthRequest * 0.2;
            Polle_Vvoda.WidthRequest = Third_page.WidthRequest * 0.8;

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

            Minus_Ugli_Na_First_Page.WidthRequest = First_page.WidthRequest;
            Box_View_On_First_Page.HeightRequest = First_page.HeightRequest * 0.3;
            Minus_Ugli_Na_Fifth_Page.WidthRequest = First_page.WidthRequest;
            Box_View_On_Fifth_Page.HeightRequest = Fifth_page.HeightRequest * 0.1;
            Text_Hello_On_First_Page.FontSize = 40;
            Minus_Ugli_Na_Second_Page.WidthRequest = Second_page.WidthRequest;
            Box_View_On_Third_Page.HeightRequest = Third_page.HeightRequest * 0.1;
            Minus_Ugli_Na_Third_Page.WidthRequest = First_page.WidthRequest;
            Minus_Ugli_Na_Second_Page.WidthRequest = First_page.WidthRequest;
            Box_View_On_Second_Page.HeightRequest = First_page.HeightRequest * 0.1;

            Minus_Ugli_Na_Fouth_Page.WidthRequest = Fouth_page.WidthRequest;
            Box_View_On_Fouth_Page.HeightRequest = Fouth_page.HeightRequest * 0.1;

            Send.WidthRequest = Fifth_page.WidthRequest * 0.2;
            Polle_Vvoda.WidthRequest = Fifth_page.WidthRequest * 0.8;


            Decoding_And_Criate_Choldren_All_Meropriatie();
            Decoding_And_Criate_Choldren_All_Chats();
            Decoding_And_Criate_Choldren_All_Groups();
            Start_Text_On_Fifth_Page(All_Inf_User[3], All_Inf_User[4], All_Inf_User[1], All_Inf_User[5], All_Inf_User[6]);
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
        void Buttons_On_Third_Page(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("GA#" + Key_Pair_For_Meroproytie_Big_String_All[button]);//(Login.Text + "#" + Password.Text);
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
                        User_Groups_String_List.Add(Prom);
                        Prom = "";
                    }
                }
                User_Groups_String_List.Add(Prom);
            }
        }
        void First_Text_Appoaring(string Str)
        {
            Text_Hello_On_First_Page.Text = "Привет, ";
            Text_Hello_On_First_Page.Text += Str;
            Text_Hello_On_First_Page.Text += "!";
        }
        void Decoding_And_Criate_Choldren()
        {

            for (int i = 0; i < Rsspis_String_List.Count - 2; i += 3)
            {
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                };

                promezutocniy.Children.Add(new Label()
                {
                    Text = (Rsspis_String_List[i]),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center
                });
                promezutocniy.Children.Add(new Label()
                {
                    Text = (Rsspis_String_List[i + 1]),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center
                });
                promezutocniy.Children.Add(new Label()
                {
                    Text = (Rsspis_String_List[i + 2]),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand

                });

                StackLayout_In_Scroll.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,
                    Content = promezutocniy
                });
            }
            Rasspis_sckroll.Content = StackLayout_In_Scroll;
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
        void First_Rasspis(string Str)
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
                        Rsspis_String_List.Add(Prom);
                        Prom = "";
                    }
                }
                Rsspis_String_List.Add(Prom);
            }
        }

        void Decoding_And_Criate_Choldren_All_Groups()
        {
            Vremenniy_Group.Orientation = StackOrientation.Vertical;
            for (int i = 0; i < User_Groups_String_List.Count -1; i += 2)
            {
                Button bt = new Button()
                {
                    Text = "Группа №"+User_Groups_String_List[i] + " " + User_Groups_String_List[i + 1],
                    TextColor = Color.Purple,
                    HeightRequest = Second_page.HeightRequest * 0.1,
                    WidthRequest = Second_page.WidthRequest,
                    CornerRadius = 20,
                    VerticalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White,
                    BorderColor = Color.Purple,
                    BorderWidth = 1
                };
                bt.Clicked += Load_SpisGroup;
                Vremenniy_Group.Children.Add(bt);
                Key_Pair_Group.Add(bt, User_Groups_String_List[i]);
            }
            Groups_sckroll.Content = Vremenniy_Group;
        }

        private void Load_SpisGroup(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("GS#" + Key_Pair_Group[button]);//(Login.Text + "#" + Password.Text);
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
                        Massege.Add(Msg);
                        if (Msg[0] != '~')
                        {
                            Groups_And_Strelka.IsVisible = true;
                            Name_Groups_In_Grid.Text = button.Text;
                            Strelka_Group.Text = "<-";
                            Name_Group.IsVisible = false;
                            Name_Meropriatia_In_Grid.Text = "Участники";
                            Create_Spisok_Group(Msg);
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
        private void Strelka_Group_Clicked(object sender, EventArgs e)
        {
            if (Type_Strelka_Group == 1)
            {
                Groups_And_Strelka.IsVisible = false;
                Name_Group.IsVisible = true;
                Groups_sckroll.Content = Vremenniy_Group;
                StackLayout_In_Scroll_Groups.Children.Clear();
                Vremenniy_User_Inf.Children.Clear();
            }
            else if(Type_Strelka_Group==2)
            {
                Type_Strelka_Group = 1;
                StackLayout_In_Scroll_Groups.Children.Clear();
                Groups_sckroll.Content = Vremenniy_User_Inf;
            }
        }
        void  Create_Spisok_Group(string Msg)
        {
            Type_Strelka_Group = 1;
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
                    Key_Pair_Fio_Button.Add(button, Massege[i]);
                    Vremenniy_User_Inf.Children.Add(new Frame()
                    {
                        BorderColor = Color.Purple,
                        CornerRadius = 20,
                        Content = promezutocniy,

                    });
                }
            }
            Groups_sckroll.Content = Vremenniy_User_Inf;
            //Vremenniy_User_Inf = StackLayout_In_Scroll_Groups;
       }

        private void Inf_Of_User_By_Knopka(object sender, EventArgs e)
        {
            Type_Strelka_Group = 2;
            Button button = (Button)sender;
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("GI#" + Key_Pair_Fio_Button[button]);//(Login.Text + "#" + Password.Text);
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
                        Massege.Add(Msg);
                        if (Msg[0] != '~')
                        {
                            Groups_And_Strelka.IsVisible = true;
                            Strelka_Group.Text = "<-";
                            Name_Group.IsVisible = false;
                            Create_Page_About_Student(Msg, Key_Pair_Fio_Button[button]);
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
            Groups_sckroll.Content = StackLayout_In_Scroll_Groups;
        }
        void Create_Page_About_Student(string Msg,string Name)
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

                StackLayout_In_Scroll_Groups.Children.Add(new Frame()
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
                for (int i = 2; i < Massege.Count - 1; i += 2)
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
                StackLayout_In_Scroll_Groups.Children.Add(new ScrollView()
                {
                    Content = promezutocniy
                });
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

        private void Save_Change_In_Profil_Clicked(object sender, EventArgs e)
        {
            bool Is_No_Ok = false;
            string Prom = "";
            if((Phon_Number.Text != null) && ((Phon_Number.Text.Length != 11) || (!Int64.TryParse(Phon_Number.Text, out Int64 num))))
                {
                Is_No_Ok = true;
                Prom += "Некоректный номер";
                Prom += "\n";
                Phon_Number.Text = null;
                Phon_Number.Placeholder = All_Inf_User[4];
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
                    Login.Placeholder = All_Inf_User[9];
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
                    Password.Placeholder = All_Inf_User[10];
                }
            }

            if ((Poshta.Text != null) && (!Poshta.Text.Contains('@') && (!Poshta.Text.Contains('.'))))
            {
                Is_No_Ok = true;
                Prom += "Некоректная почта";
                Prom += "\n";
                Poshta.Text = null;
                Poshta.Placeholder = All_Inf_User[5];
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
                    Send_Changes(Obzh_Na_Otpravka+"#TE");
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
                        Massege.Add(Msg);
                        if (Msg[0] == 'S')
                        {
                            if (Change_Izm % 5 == 0)
                            {
                                First_Text_Appoaring(Psewdonim.Text);
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
                            Password.Text = null;
                            DisplayAlert("Ошибка", "Логин уже используется", "Ок");
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
            Name = "";
            Rsspis_String_List.Clear();
            Pokupka_Sting_Lst.Clear();
            Proverka_Na_Zagruzku.Clear();
            All_Inf_User.Clear();
            All_Meropriatie_String_List.Clear();
            User_Meropriatie_String_List.Clear();
            Navigation.PopModalAsync();
        }

        private void Obsh_TextChanged(object sender, TextChangedEventArgs e)
        {
            Save_Change_In_Profil.BackgroundColor = Color.Purple;
            Save_Change_In_Profil.IsEnabled = true;
        }
        void Decoding_And_Criate_Choldren_All_Chats()
        {

            Id_Groups_List.Add("0");
            for (int i = 0; i < User_Groups_String_List.Count; i += 2)
            {
                Id_Groups_List.Add(User_Groups_String_List[i]);
            }
            Vremenniy_Chats.Orientation = StackOrientation.Vertical;
            Button Osn = new Button()
            {
                Text = "Основной",
                TextColor = Color.Purple,
                HeightRequest = Third_page.HeightRequest * 0.1,
                WidthRequest = Third_page.WidthRequest,
                CornerRadius = 20,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                BorderColor = Color.Purple,
                BorderWidth = 1
            };
            int Count = 0;
            Key_Pair_Chats.Add(Osn, Id_Groups_List[Count]);
            Osn.Clicked += Load_Chats;
            Vremenniy_Chats.Children.Add(Osn);
            for (int i = 0; i < User_Groups_String_List.Count; i += 2)
            {
                Count++;
                Button bt = new Button()
                {
                    Text ="Группа № "+ User_Groups_String_List[i] + " " + User_Groups_String_List[i + 1],
                    TextColor = Color.Purple,
                    HeightRequest = Third_page.HeightRequest * 0.1,
                    WidthRequest = Third_page.WidthRequest,
                    CornerRadius = 20,
                    VerticalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White,
                    BorderColor = Color.Purple,
                    BorderWidth = 1
                };
                bt.Clicked += Load_Chats;
                Vremenniy_Chats.Children.Add(bt);
                Key_Pair_Chats.Add(bt, Id_Groups_List[Count]);
            }
            Chats_sckroll.Content = Vremenniy_Chats;
        }

        void Load_Chats(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            Type_Send_Chat = Key_Pair_Chats[button];
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("MMG" + Key_Pair_Chats[button]);//(Login.Text + "#" + Password.Text);
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
                        Massege.Add(Msg);
                        if (Msg[0] != '~')
                        {
                            Chats_And_Strelka.IsVisible = true;
                            Name_chats_In_Grid.Text = button.Text;
                            Strelka_Chats.Text = "<-";
                            Name_chats.IsVisible = false;
                            Criate_Dilog(Msg, Key_Pair_Chats[button]);
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
        void Criate_Dilog(string Msg,string Type)
        {
            if (Type != "0")
            {
                Box_View_On_Fouth_Page.HeightRequest = Fouth_page.HeightRequest * 0.15;
                Pole_Vvoda_And_Send.IsVisible = true;
            }
            else
            {
                Box_View_On_Fouth_Page.HeightRequest = Fouth_page.HeightRequest * 0.2;
            }
            StackLayout_In_Scroll_Groops_sckroll.Children.Clear();
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
                    StackLayout promezutocniy = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                    };

                    promezutocniy.Children.Add(new Label()
                    {
                        Text = (Massege[i]),
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
        private void Strelka_Chats_Clicked(object sender, EventArgs e)
        {
            Box_View_On_Fouth_Page.HeightRequest = Fouth_page.HeightRequest * 0.1;
            Chats_And_Strelka.IsVisible = false;
            Name_chats.IsVisible = true;
            Chats_sckroll.Content = Vremenniy_Chats;
            StackLayout_In_Scroll_Groops_sckroll.Children.Clear();
            Polle_Vvoda.Text = null;
            Pole_Vvoda_And_Send.IsVisible = false;
            Type_Send_Chat = "";
        }

        private void Send_Clicked(object sender, EventArgs e)
        {
            if((Polle_Vvoda.Text!=null)&&(Polle_Vvoda.Text.Length>0))
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("MMS" +Type_Send_Chat+"#"+ Polle_Vvoda.Text);//(Login.Text + "#" + Password.Text);
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
                                        Text = FIO_Sotr,
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
        }
    }
