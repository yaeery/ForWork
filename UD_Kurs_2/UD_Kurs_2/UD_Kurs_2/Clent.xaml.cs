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
    public partial class Clent : ContentPage
    {
        int Change_Izm = 1;
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
        List<string> All_Meropriatie_String_List = new List<string>();
        List<string> Prom_All_Meropriatie_String_List = new List<string>();
        List<string> User_Meropriatie_String_List = new List<string>();
        List<string> User_Chats_String_List = new List<string>();
        List<string> Id_Groups_List = new List<string>();
        List<string> Massege = new List<string>();
        StackLayout Vremenniy = new StackLayout();
        Dictionary<Button, string> Key_Pair = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_For_Meroproytie = new Dictionary<Button, string>();
        Dictionary<Button, string> Key_Pair_For_Meroproytie_Big_String_All = new Dictionary<Button, string>();
        public Clent(TcpClient tcpClient, List<string> Vsy_Infa)
        {
            Thread1 = new Thread(Persapusk);
            this.tcpClient = tcpClient;
            All_Inf_User = Vsy_Infa;
            Name = Vsy_Infa[1];
            First_Rasspis(Vsy_Infa[2]);
            Second_Pokupka(Vsy_Infa[3]);
            Third_All_Meropriatie(Vsy_Infa[6]);
            Third_Meropriatye_By_User(Vsy_Infa[7]);
            Fourth_Chats(Vsy_Infa[8]);
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected  override void OnAppearing()
        {
            First_Text_Appoaring(Name);
            Decoding_And_Criate_Children_For_Pokupka();
            Decoding_And_Criate_Choldren();
            First_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            First_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height *0.9  / DeviceDisplay.MainDisplayInfo.Density;

            Second_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            Second_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Third_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            Third_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Fouth_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            Fouth_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Fifth_page.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            Fifth_page.HeightRequest = DeviceDisplay.MainDisplayInfo.Height * 0.9 / DeviceDisplay.MainDisplayInfo.Density;

            Minus_Ugli_Na_First_Page.WidthRequest = First_page.WidthRequest;
            Box_View_On_First_Page.HeightRequest = First_page.HeightRequest*0.3;
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


            Decoding_And_Criate_Choldren_All_Meropriatie();
            Decoding_And_Criate_Choldren_All_Chats();
            Start_Text_On_Fifth_Page(All_Inf_User[4], All_Inf_User[5], All_Inf_User[1], All_Inf_User[9],All_Inf_User[10]);
        }

        private async void Data_ValueChanged(object sender, EventArgs e)
        {
            await DisplayAlert("", "", "Ок");
            
        }
        void First_Text_Appoaring(string Str)
        {
            Text_Hello_On_First_Page.Text = "Привет, ";
            Text_Hello_On_First_Page.Text += Str;
            Text_Hello_On_First_Page.Text += "!";
        }
        void Start_Text_On_Fifth_Page(string Phone, string Pochta, string Name,string _Login,string _Passsword)
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

        void Second_Pokupka(string Str)
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
                        Pokupka_Sting_Lst.Add(Prom);
                        Prom = "";
                    }
                }
                Pokupka_Sting_Lst.Add(Prom);
            }
        }

        void Fourth_Chats(string Str)
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
                        User_Chats_String_List.Add(Prom);
                        Prom = "";
                    }
                }
                User_Chats_String_List.Add(Prom);
            }
        }
        void Third_Meropriatye_By_User(string Str)
        {
            if (Str != "F")
            {
                List<string> L = new List<string>();
                string Prom = "";
                for (int i = 0; i < Str.Length; i++)
                {
                    if (Str[i] != '#')
                    {
                        Prom += Str[i];
                    }
                    else
                    {
                        L.Add(Prom);
                        Prom = "";
                    }
                }
                L.Add(Prom);
                for (int i = 0; i < L.Count - 1; i += 2)
                {
                    User_Meropriatie_String_List.Add(L[i] + "#" + L[i + 1]);
                }
            }
        }
        void Decoding_And_Criate_Children_For_Pokupka()
        {
            for (int i = 0; i < Pokupka_Sting_Lst.Count-1   ; i+=2)
            {
                StackLayout promezutocniy = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                };

                promezutocniy.Children.Add(new Label()
                {
                    Text = (Pokupka_Sting_Lst[i]),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center
                });
                promezutocniy.Children.Add(new Label()
                {
                    Text = (Pokupka_Sting_Lst[i + 1]),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.End
                });
                StackLayout_In_Scroll_Pokupok.Children.Add(new Frame()
                {
                    BorderColor = Color.Purple,
                    CornerRadius = 20,
                    Content = promezutocniy
                });
            }
            Pokupki_sckroll.Content = StackLayout_In_Scroll_Pokupok;
        }
        void Decoding_And_Criate_Choldren()
        {
           
            for (int i = 0; i < Rsspis_String_List.Count-2; i+=3)
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
                    Text = (Rsspis_String_List[i+1]),
                    TextColor = Color.Purple,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center
                });
                promezutocniy.Children.Add(new Label()
                {
                    Text = (Rsspis_String_List[i+2]),
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

        void Decoding_And_Criate_Choldren_All_Meropriatie()
        {
            StackLayout promezutocniy = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
            };
            int Count = 0;
            for (int i = 0; i < All_Meropriatie_String_List.Count-1; i+=2)
            {
                string[] Prom_List = new string[1];
                Prom_List[0] = All_Meropriatie_String_List[i] + "#" + All_Meropriatie_String_List[i + 1];
                var Result = Prom_List.Intersect(User_Meropriatie_String_List);

                Button bt = new Button()
                {
                    Text = All_Meropriatie_String_List[i] + "\n\n" + (DateTime.Parse(All_Meropriatie_String_List[i + 1]).ToString("dd MMM yyyy")),
                    TextColor = Color.Purple,
                    HeightRequest = Third_page.HeightRequest * 0.1,
                    WidthRequest = Third_page.WidthRequest,
                    CornerRadius = 20,
                    VerticalOptions = LayoutOptions.Center,
                    BorderColor = Color.Purple,
                    BorderWidth = 1
                };

                if (Result.Count() == 0)
                {
                    bt.BackgroundColor = Color.Gray;
                }
                else
                {
                    bt.BackgroundColor = Color.White;
                }
                Key_Pair_For_Meroproytie.Add(bt, All_Meropriatie_String_List[i + 1]);
                Key_Pair_For_Meroproytie_Big_String_All.Add(bt, Prom_All_Meropriatie_String_List[Count]);
                Count++;
                bt.Clicked += Buttons_On_Third_Page;
                promezutocniy.Children.Add(bt);
            }
            Meroptiatie_sckroll.Content = promezutocniy;
        }

        void Decoding_And_Criate_Choldren_All_Chats()
        {

            Id_Groups_List.Add("0");
            for (int i = 2; i < User_Chats_String_List.Count; i += 3)
            {
                Id_Groups_List.Add(User_Chats_String_List[i]);
            }
            Vremenniy.Orientation = StackOrientation.Vertical;
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
            Key_Pair.Add(Osn, Id_Groups_List[Count]);
            Osn.Clicked += Load_Chats;
            Vremenniy.Children.Add(Osn);
            for (int i = 0; i < User_Chats_String_List.Count/3*2; i+=3)
            {
                Count++;
                Button bt = new Button()
                {
                    Text = User_Chats_String_List[i] +" "+ User_Chats_String_List[i+1],
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
                Vremenniy.Children.Add(bt);
                Key_Pair.Add(bt, Id_Groups_List[Count]);
            }
            Chats_sckroll.Content = Vremenniy;
        }

        void Load_Chats(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                byte[] bytes = Encoding.Unicode.GetBytes("MMG" + Key_Pair[button]);//(Login.Text + "#" + Password.Text);
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
                            Strelka.Text = "<-";
                            Name_chats.IsVisible = false;
                            Criate_Dilog(Msg);
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
        void Criate_Dilog(string Msg)
        {
            if (Msg != "F")
            {
                Box_View_On_Fouth_Page.HeightRequest = Fouth_page.WidthRequest * 0.4;
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
        void Buttons_On_Third_Page(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            string[] Prom_List = new string[1];
            Prom_List[0] = Key_Pair_For_Meroproytie_Big_String_All[button];
            var Result = Prom_List.Intersect(User_Meropriatie_String_List);
            //string x = DateTime.Parse(Key_Pair_For_Meroproytie[button]).ToString();
            if (DateTime.Parse(Key_Pair_For_Meroproytie[button])>DateTime.Now)
            {
                if (Result.Count() != 0)
                {
                    Otkaz_Ot_Meropriatia(Prom_List[0], button);
                }
                else
                {
                    Vibor_Registrac(Prom_List[0], button);
                }
            }
            else
            {
                if (Result.Count() != 0)
                {
                   DisplayAlert("Мероприятие уже прошло", "Вы участвовали в данном мероприятии", "Ок");
                }
                else
                {
                    DisplayAlert("Мероприятие уже прошло", "Вы не участвовали в данном мероприятии", "Ок");
                }
            }
        }
        async void Otkaz_Ot_Meropriatia(string text,Button bt)
        {
            bool x = await DisplayAlert("Вы уже участник мероприятия", "Хотите отказаться?", "Да", "Нет");
            if (x == true)
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("DM" + All_Inf_User[0] + "#" + text);//(Login.Text + "#" + Password.Text);
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
                                User_Meropriatie_String_List.Remove(text);
                                bt.BackgroundColor = Color.Gray;
                                DisplayAlert("Теперь вы не участвуете в мероприятии", "", "Ок");
                                break;
                            }
                            else if (Msg[0] == 'F')
                            {
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
        }
        async void Vibor_Registrac(string text,Button bt)
        {
            bool x =  await DisplayAlert("Хотите принять участие в мероприятии?", "", "Да", "Нет");
            if(x==true)
            {
                try
                {
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("SM" + All_Inf_User[0] + "#" + text);//(Login.Text + "#" + Password.Text);
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
                                User_Meropriatie_String_List.Add(text);
                                bt.BackgroundColor = Color.White;
                                DisplayAlert("Теперь вы участник мероприятия", "", "Ок");
                                break;
                            }
                            else if (Msg[0] == 'F')
                            {
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
                    Is_Connect = false;
                    //if (!Thread1.IsAlive)
                    //{
                    //    Thread1.Start();
                    //}
                }
            }
        }
        private  void Button_One_Clicked(object sender, EventArgs e)
        {
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density)*0, 0, true);
        }

        private void Button_Two_Clicked(object sender, EventArgs e)
        {
            //if (Proverka_Na_Zagruzku[1] == false)
            {
                Proverka_Na_Zagruzku[1] = true;
            }
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density)*1.014, 0, true);
        }

        private void Button_Three_Clicked(object sender, EventArgs e)
        { 
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width *2.03 / DeviceDisplay.MainDisplayInfo.Density), 0, true);
        }

        private void Button_Four_Clicked(object sender, EventArgs e)
        { 
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width *3.04 / DeviceDisplay.MainDisplayInfo.Density), 0, true);
        }

        private void Button_Five_Clicked(object sender, EventArgs e)
        {
            Scroll_View.ScrollToAsync((DeviceDisplay.MainDisplayInfo.Width*4.06 / DeviceDisplay.MainDisplayInfo.Density), 0, true);
        }

        private void Save_Change_In_Profil_Clicked(object sender, EventArgs e)
        {
            bool Is_No_Ok = false;
            string Prom = "";
            if((Phon_Number.Text!=null)&&((Phon_Number.Text.Length!=11) || (!Int64.TryParse(Phon_Number.Text, out Int64 num))))
            {
                Is_No_Ok = true;
                Prom += "Некоректный номер";
                Prom += "\n";
                Phon_Number.Text = null;
                Phon_Number.Placeholder = All_Inf_User[4];
            }
            if((Login.Text!=null))
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

            if ((Poshta.Text != null) &&(!Poshta.Text.Contains('@') &&(!Poshta.Text.Contains('.'))))
            {
                Is_No_Ok = true;
                Prom += "Некоректная почта";
                Prom += "\n";
                Poshta.Text = null;
                Poshta.Placeholder = All_Inf_User[5];
            }
            if ((Psewdonim.Text != null) &&(Psewdonim.Text.Length > 0))
            {
                string x = Psewdonim.Text;
                string v= x.Trim();
                if (v=="")
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
                    Send_Changes(Prom_Str_LP + "#CL");
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
                    Send_Changes(Obzh_Na_Otpravka + "#CL");
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

        private void Strelka_Clicked(object sender, EventArgs e)
        {
            Chats_And_Strelka.IsVisible = false;
            Name_chats.IsVisible = true;
            Chats_sckroll.Content = Vremenniy;
            StackLayout_In_Scroll_Groops_sckroll.Children.Clear();
            Box_View_On_Fouth_Page.HeightRequest = Fouth_page.WidthRequest * 0.2;
        }
        void Persapusk()
        {
            while (Is_Connect)
            {
                try
                {
                    tcpClient = new TcpClient("192.168.62.219", 33023);
                    Is_Connect = true;
                    break;
                }
                catch
                {
                }
            }
            Thread1.Abort();
        }
    }
}