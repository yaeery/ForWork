using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Data.SqlClient;

namespace ServerUD
{
    class Hosting
    {
        IPEndPoint ipPoint_first_Chat;// = new IPEndPoint(IPAddress.Any, 33022);
        List<User> list__first_Chat;// = new List<User>();
                                    // static byte[] bytes = new byte[256];
        Thread thread_listning;// = new Thread(Listning);
        string Msg;// = "";
        TcpListener tcpListener;// = new TcpListener(ipPoint_first_Chat);   
        BD bd;
        string Type_Of_Zapros = "";
        bool Udalenie = false;

        public Hosting(int Port)    
        {
            ipPoint_first_Chat = new IPEndPoint(IPAddress.Parse("192.168.68.219"), Port);
            //ipPoint_first_Chat = new IPEndPoint(IPAddress.Parse("192.168.1.3"), Port);
            list__first_Chat = new List<User>();
            thread_listning = new Thread(Listning);
            Msg = "";
            bd = new BD(@"data source=HPLAPTOP\SQLEXPRESS; Initial Catalog=4Glava;Integrated Security = True;");
            tcpListener = new TcpListener(ipPoint_first_Chat);
            bd.Open_Conection();
        }
        public void ReceiveCallback(IAsyncResult AsyncCall)
        {
            TcpClient tcpClient = ((TcpListener)AsyncCall.AsyncState).EndAcceptTcpClient(AsyncCall);
            //StreamReader reader = new StreamReader("../txt.txt");
            //byte[] message = Encoding.Unicode.GetBytes(reader.ReadToEnd());
           // reader.Close();
            //NetworkStream networkStream = tcpClient.GetStream();
            //networkStream.Write(message, 0, message.Length);
           // networkStream.Flush();
            list__first_Chat.Add(new ServerUD.User((TcpListener)AsyncCall.AsyncState, tcpClient));
            ((TcpListener)AsyncCall.AsyncState).BeginAcceptTcpClient(new AsyncCallback(ReceiveCallback), ((TcpListener)AsyncCall.AsyncState));
        }
        void Listning()
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i < list__first_Chat.Count; i++)
                    {
                        if (Udalenie == true)
                        {
                            Console.WriteLine("Прослушивание приостановлено");
                            Thread.Sleep(500);
                        }
                        if ((list__first_Chat[i].client != null))
                        {
                            if (list__first_Chat[i].client.Available > 0) 
                            {
                                Msg = list__first_Chat[i].Get_Msg();
                                Console.WriteLine(Msg);
                                Type_Of_Zapros += Msg[0];
                                Type_Of_Zapros += Msg[1];
                                string x = "";
                                switch (Type_Of_Zapros)
                                {
                                    case "LP":
                                        x = bd.Get_Type_of(Msg);
                                        break;
                                    case "GN":
                                        x = bd.Get_Psevdonim_of(Msg);
                                        break;
                                    case "GT":
                                        x = bd.Get_Passpis_of(Msg);
                                        break;
                                    case "GP":
                                        x = bd.Get_Pokupka_of(Msg);
                                        break;
                                    case "GF":
                                        x = bd.Get_Phone_of(Msg);
                                        break;
                                    case "GI":
                                        x = bd.Get_Inf_About_Studen_For_Teacher(Msg);
                                        break;
                                    case "GE":
                                        x = bd.Get_Pochta_of(Msg);
                                        break;
                                    case "GM":
                                        x = bd.Get_All_Meropriatie_of(Msg);
                                        break;
                                    case "GU":
                                        x = bd.Get_Meropriatie_User_of(Msg);
                                        break;
                                    case "GG":
                                        x = bd.Get_Groops_of(Msg);
                                        break;
                                    case "GS":
                                        x = bd.Get_Spisok_Groups(Msg);
                                        break;
                                    case "GO":
                                        x = bd.Get_FIO_of(Msg);
                                        break;
                                    case "GA":
                                        x = bd.Get_Spisok_Uchactnikov_Mer_of(Msg);
                                        break;
                                    case "ST":
                                        x = bd.Set_Name_Pochta_Phone_of(Msg);
                                        break;
                                    case "SM":
                                        x = bd.Set_eropriatye_User_of(Msg);
                                        break;
                                    case "MM":
                                        x = bd.All_About_Massage(Msg);
                                        break;
                                    case "DM":
                                        x = bd.Delete_eropriatye_User_of(Msg);
                                        break;
                                    case "DG":
                                        x = bd.Get_Groops_For_Dir_of(Msg);
                                        break;
                                    case "NM":
                                        x = bd.Set_New_Meropriatie_of(Msg);
                                        break;
                                    case "NR":
                                        x = bd.Set_New_Rasspis_of(Msg);
                                        break;
                                    case "AP":
                                        x = bd.Get_All_People_of(Msg);
                                        break;
                                    case "GR":
                                        x = bd.Get_Sotr_Info_of(Msg);
                                        break;
                                    case "NZ":
                                        x = bd.Set_Zp_Sotr_of(Msg);
                                        break;
                                    case "DD":
                                        x = bd.Delete_Sotr_Or_Client_of(Msg);
                                        break;
                                    case "DI":
                                        x = bd.Get_Inf_About_Studen_For_Director(Msg);
                                        break;
                                    case "GZ":
                                        x = bd.Get_All_Usluga_of(Msg);
                                        break;
                                    case "NC":
                                        x = bd.New_Coast_Usluga_of(Msg);
                                        break;
                                    case "NU":
                                        x = bd.Set_New_Usluga(Msg);
                                        break;
                                    case "NS":
                                        x = bd.Set_New_Sotr_of(Msg);
                                        break;
                                    case "AG":
                                         x = bd.Get_All_Inf_Groops_of(Msg);
                                        break;
                                    case "SG":
                                        x = bd.Set_New_Group_User_of(Msg);
                                        break;
                                    case "NK":
                                        x = bd.Set_New_Client_of(Msg);
                                        break;
                                    case "TG":
                                        x = bd.Set_New_Teacher_of_Group_of(Msg);
                                        break;
                                    case "NG":
                                        x = bd.Set_New_Group(Msg);
                                        break;
                                    case "CT":
                                        x = bd.Get_Tren_by_Client_in_Rasspis_of(Msg);
                                        break;
                                    case "NO":
                                        x = bd.Set_New_Pokupka(Msg);
                                        break;
                                    case "AO":
                                        x = bd.Get_All_Pokupki_of(Msg);
                                        break;
                                    case "PC":
                                        x = bd.Get_Spisok_Clientov_Poisk(Msg);
                                        break;
                                    case "PS":
                                        x = bd.Get_Spisok_Sotr_Poisk(Msg);
                                        break;
                                    case "PM":
                                        x = bd.Get_Spisok_Mer_Poisk(Msg);
                                        break;
                                }
                                Type_Of_Zapros = "";
                                Console.WriteLine(x);
                                list__first_Chat[i].Send_Msg(x);
                                //NetworkStream networkStream = list__first_Chat[i].client.GetStream();
                                //byte[] bytes = Encoding.Unicode.GetBytes(x);
                                //networkStream.Write(bytes, 0, bytes.Length);
                                //networkStream.Flush();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка");
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }
       
        public void Main_Vhod()
        {
            try
            {
                tcpListener.Start();
                Console.WriteLine("Сервер " + ipPoint_first_Chat.Port + " запущен");
                thread_listning.Start();
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(ReceiveCallback), tcpListener);

                while (true)
                {
                    Console.WriteLine("Подключено " + list__first_Chat.Count.ToString() + " пользователей к " + ipPoint_first_Chat.Port);
                    for (int i = 0; i < list__first_Chat.Count; i++)
                    {
                        //if (list__first_Chat[i].Get_Is_Sending() == false)
                        //{
                            list__first_Chat[i].Send_Proverka();
                        //}
                        if (list__first_Chat[i].Get_Is_Connect() == false)
                        {
                            //list__first_Chat[i].client.Close();
                            Udalenie = true;
                            list__first_Chat.Remove(list__first_Chat[i]);
                            i--;
                            Console.WriteLine("Пользователь отключился от " + ipPoint_first_Chat.Port);
                            Console.WriteLine("Подключено " + list__first_Chat.Count.ToString() + " пользователей к " + ipPoint_first_Chat.Port);
                            Udalenie = false;
                        }
                    }
                    Thread.Sleep(1000);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Ошибка");
                Console.WriteLine(e.Message);
            }
        }
    }
}
/*
LP - вход
GN - Запросимени
GT - запрос тренировок
GP - запрос оплат
GF - запрос номера
GE - запрос почты
GS - список групп
GA  - спмсок фамилий в мероприятии
GI - информация о ученике
GM - запрос всех мероприятий
GU - запрос о участие в мероприятии клиента
GG - запрос на группы
GO - запрос на ФИО
SN - изменить имя
ST - запрос на замену трех параметров с пятой страницы
SP - изменить почту
SF - изменить номер
SL - сменить логин
SK - сменить код
SM - Добавить участие в мероприятии
DG - группы для директора
DM - удалить мероприятие
NM - новое мероприятие
NR - новое рассписание
AP - Сотр и клиенты для Директора
GR - информаия о сотрдние
NZ - новая зарплата
DD - удаление сотрдника или клиента
DI - аналог GI, но для директора
GZ - запрос услуг
NU - новая услуга
NC - новая цена
NS - новый сотрудник
AG - вся инфа о группе
SG - смена групп у клиента
NK - новый клиент
TG - новый преподаватель группы
NG - новая группа
CT - тренировки клиента
NO - новая оплата
AO - все покупки
PC - Поиск клиентов
PS - поиск сотрудников
PM - поиск мероприятия
 */
