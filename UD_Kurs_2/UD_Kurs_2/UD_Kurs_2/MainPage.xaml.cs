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
                    // IPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.103.219"), 33023);
                    tcpClient = new TcpClient("192.168.68.219", 33023);
                    //tcpClient = new TcpClient("192.168.1.3", 33023);
                    //Thread1 = new Thread(Work);
                    //   Thread1.Start();
                    NetworkStream networkStream = tcpClient.GetStream();
                    byte[] bytes = Encoding.Unicode.GetBytes("LP#"+Login.Text + "#" + Password.Text);//("LP#AAAAAA#000000");//
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
                               // Thread1.Abort();
                                break;
                            }
                            networkStream1.Flush();
                        }
                    }
                }
                catch(Exception r)
                {
                    string x = r.Message;
                    //tcpClient.Close();
                    // Thread1.Abort();
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
            All_Inf_About_User.Add("#" + Login.Text + "#" + Password.Text);//("#AAAAAA#000000");
            All_Inf_About_User.Add(Work_Name());
            All_Inf_About_User.Add(Work_Rasspis());
            All_Inf_About_User.Add(Work_Pokupok());
            All_Inf_About_User.Add(Work_Phone());
            All_Inf_About_User.Add(Work_Pochta());
            All_Inf_About_User.Add(Work_All_Meropriatie());
            All_Inf_About_User.Add(Work_Meropriatye_By_User());
            All_Inf_About_User.Add(Work_Groops());
            All_Inf_About_User.Add(Login.Text);
            All_Inf_About_User.Add(Password.Text);
        }
        void Zapolnenie_All_Inf_Tc()
        {
            All_Inf_About_User.Add("#" + Login.Text + "#" + Password.Text);//("#AAAAAA#000000");
            All_Inf_About_User.Add(Work_Name());
            All_Inf_About_User.Add(Work_Rasspis());
            All_Inf_About_User.Add(Work_Phone());
            All_Inf_About_User.Add(Work_Pochta());
            All_Inf_About_User.Add(Login.Text);
            All_Inf_About_User.Add(Password.Text);
            All_Inf_About_User.Add(Work_Groops());
            All_Inf_About_User.Add(Work_All_Meropriatie());
            All_Inf_About_User.Add(Work_FIO());
            //All_Inf_About_User.Add(Work_Pokupok());
            //All_Inf_About_User.Add(Work_Meropriatye_By_User());
            //
        }
        void Zapolnenie_All_Inf_Di()
        {
            All_Inf_About_User.Add("#" + Login.Text + "#" + Password.Text);//("#AAAAAA#000000");
            All_Inf_About_User.Add(Work_All_Meropriatie());
            All_Inf_About_User.Add(Work_Groops_Director());
            All_Inf_About_User.Add(Work_All_People("C"));
            All_Inf_About_User.Add(Work_All_People("T"));
            All_Inf_About_User.Add(Work_All_Usluga());
            All_Inf_About_User.Add(Work_Rasspis("B"));
            All_Inf_About_User.Add(Load_Chats());
        }
        void Zapolnenie_All_Inf_Admin()
        {
            All_Inf_About_User.Add("#" + Login.Text + "#" + Password.Text);//("#AAAAAA#000000");
            All_Inf_About_User.Add(Work_Name());
            All_Inf_About_User.Add(Work_Phone());
            All_Inf_About_User.Add(Work_Pochta());
            All_Inf_About_User.Add(Login.Text);
            All_Inf_About_User.Add(Password.Text);
            All_Inf_About_User.Add(Work_All_People("C"));
            //All_Inf_About_User.Add(Work_Groops_Director());
            All_Inf_About_User.Add(Work_All_Inf_Group());
            All_Inf_About_User.Add(Work_All_People("A"));
            All_Inf_About_User.Add(Work_All_Usluga());
            All_Inf_About_User.Add(Work_All_Rasspis());
            //All_Inf_About_User.Add(Load_Chats());
        }

        string Load_Chats()
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
                        byte[] bytes1 = new byte[32000];
                        NetworkStream networkStream1 = tcpClient.GetStream();
                        int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                        Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                        if (Msg[0] != '~')
                        {
                        networkStream1.Flush();
                            return Msg;
                        }
                    networkStream1.Flush();
                }
            }
        }
        string Work_All_Rasspis ()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("AO");//(Login.Text + "#" + Password.Text);
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
        string Work_Rasspis()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GT"+ All_Inf_About_User[0]);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[240000];
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
        string Work_Rasspis(string X)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GT" + X);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[2048];
                    NetworkStream networkStream1 = tcpClient.GetStream();
                    int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                    Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                    if (Msg[0] != '~')
                    {
                        networkStream1.Flush();
                        return Msg;
                    }
                    networkStream1.Flush();
                }
            }
        }
       
        string Work_All_People(string Str)
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("AP" + Str);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[64000];
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
        string Work_All_Inf_Group()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("AG");//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[64000];
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
        string Work_All_Usluga()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GZ");//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[64000];
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
        string Work_Groops_Director()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("DG");//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[64000];
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
        string Work_FIO()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GO" + All_Inf_About_User[0]);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[64000];
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
        string Work_Phone()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GF" + All_Inf_About_User[0]);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[64000];
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
        string Work_Pochta()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GE" + All_Inf_About_User[0]);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                Msg = "";
                if (tcpClient.Available > 0)
                {
                    byte[] bytes1 = new byte[64000];
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
        string Work_Meropriatye_By_User()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GU" + All_Inf_About_User[0]);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                Msg = "";
                if (tcpClient.Available > 0)
                {
                    byte[] bytes1 = new byte[64000];
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
        string Work_Name()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GN" + All_Inf_About_User[0]);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[64000];
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
        string Work_Groops()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GG" + All_Inf_About_User[0]);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                if (tcpClient.Available > 0)
                {
                    Msg = "";
                    byte[] bytes1 = new byte[64000];
                    NetworkStream networkStream1 = tcpClient.GetStream();
                    int size = networkStream1.Read(bytes1, 0, bytes1.Length);
                    Msg += Encoding.Unicode.GetString(bytes1, 0, size);
                    if (Msg[0] != '~')
                    {
                        networkStream1.Flush();
                        return Msg;
                    }
                }
            }
        }
        string Work_All_Meropriatie()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GM" + All_Inf_About_User[0]);//(Login.Text + "#" + Password.Text);
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
        string Work_Pokupok()
        {
            NetworkStream networkStream = tcpClient.GetStream();
            byte[] bytes = Encoding.Unicode.GetBytes("GP" + All_Inf_About_User[0]);//(Login.Text + "#" + Password.Text);
            networkStream.Write(bytes, 0, bytes.Length);
            networkStream.Flush();
            Msg = "";
            while (true)
            {
                Msg = "";
                if (tcpClient.Available > 0)
                {
                    byte[] bytes1 = new byte[64000];
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
            switch(Type)
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
