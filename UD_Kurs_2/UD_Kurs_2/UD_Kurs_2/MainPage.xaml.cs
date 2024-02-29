using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace UD_Kurs_2
{
    public partial class MainPage : ContentPage
    {
        static TcpClient tcpClient;
        Thread Thread1;
        static string Msg = "";
        static List<string> All_Inf_About_User = new List<string>();
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            Login.Text = null;
            Password.Text = null;
        }
        protected override void OnDisappearing()
        { 
            base.OnDisappearing();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            if((Login.Text!=null) &&(Password.Text != null) &&(Login.Text.ToString().Length == 6) && (Password.Text.ToString().Length == 6))
            {
                try
                {
                    tcpClient = new TcpClient("192.168.68.219", 33023);
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("LP#"+Login.Text + "#" + Password.Text);
                    networkStream.Write(bytes, 0, bytes.Length);
                    networkStream.Flush();
                    while (true)
                    {
                        if (tcpClient.Available > 0)
                        {
                            byte[] bytes1 = new byte[64000];
                            NetworkStream networkStream1 = tcpClient.GetStream();
                            int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                            Msg = Encoding.Unicode.GetString(bytes1, 0, size);
                            if (Msg[0] != '~')
                            {
                                Console.WriteLine(Msg);
                                Create_Page(Msg);
                                break;
                            }
                            networkStream1.Flush();
                        }
                    }
                }
                catch(Exception r)
                {
                    string x = r.Message;
                    Massag.Text = "Нет подлючения попробуйте снова";
                }
            }
            else
            {
                Massag.Text = "Неверные данные";
                Login.Text = "";
                Password.Text = "";
            }
        }
        void Zapolnenie_All_Inf_Cl()
        {
            All_Inf_About_User.Add("#" + Login.Text + "#" + Password.Text);
            All_Inf_About_User.Add(Work("GN" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GT" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GP" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GF" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GE" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GM" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GU" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GG" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Login.Text);
            All_Inf_About_User.Add(Password.Text);
        }
        void Zapolnenie_All_Inf_Tc()
        {
            All_Inf_About_User.Add("#" + Login.Text + "#" + Password.Text);
            All_Inf_About_User.Add(Work("GN" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GT" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GF" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GE" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Login.Text);
            All_Inf_About_User.Add(Password.Text);
            All_Inf_About_User.Add(Work("GG" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GM" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GO" + All_Inf_About_User[0]));
        }
        void Zapolnenie_All_Inf_Di()
        {
            All_Inf_About_User.Add("#" + Login.Text + "#" + Password.Text);
            All_Inf_About_User.Add(Work("GM" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("DG"));
            All_Inf_About_User.Add(Work("APC"));
            All_Inf_About_User.Add(Work("APT"));
            All_Inf_About_User.Add(Work("GZ"));
            All_Inf_About_User.Add(Work("GTB"));
            All_Inf_About_User.Add(Work("MMG0"));
        }
        void Zapolnenie_All_Inf_Admin()
        {
            All_Inf_About_User.Add("#" + Login.Text + "#" + Password.Text);
            All_Inf_About_User.Add(Work("GN" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GF" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Work("GE" + All_Inf_About_User[0]));
            All_Inf_About_User.Add(Login.Text);
            All_Inf_About_User.Add(Password.Text);
            All_Inf_About_User.Add(Work("APC"));
            All_Inf_About_User.Add(Work("AG"));
            All_Inf_About_User.Add(Work("APA"));
            All_Inf_About_User.Add(Work("GZ"));
            All_Inf_About_User.Add(Work("AO"));
        }
        string Work(string Input)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes(Input);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[8000];
                    NetworkStream networkStream1 = tcpClient.GetStream();
                    int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                    Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                    if (Msg[0] != '~')
                    {
                        return Msg;
                    }
                    networkStream1.Flush();
                }
            }
        }
        void Create_Page(string Type)
        {
            switch (Type)
            {
                case "1":
                    Zapolnenie_All_Inf_Cl();
                    Navigation.PushModalAsync(new Clent(tcpClient, All_Inf_About_User));
                    break;
                case "2":
                    Zapolnenie_All_Inf_Tc();
                    Navigation.PushModalAsync(new Teacher(tcpClient, All_Inf_About_User));
                    break;
                case "3":
                    Zapolnenie_All_Inf_Admin();
                    Navigation.PushModalAsync(new Admin(tcpClient, All_Inf_About_User));
                    break;
                case "4":
                    Zapolnenie_All_Inf_Di();
                    Navigation.PushModalAsync(new Director(tcpClient, All_Inf_About_User));
                    break;
                default:
                    tcpClient.Close();
                    Massag.Text = "Неверные данные";
                    Login.Text = "";
                    Password.Text = "";
                    break;
            }
        }
    }
}
