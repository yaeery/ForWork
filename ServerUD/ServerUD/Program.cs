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
    class Program   
    {
        
        static void Main(string[] args)
        {
            Hosting hosting_first = new Hosting(33023);
            Thread first = new Thread(hosting_first.Main_Vhod);
            first.Start();
        }
    }
      
}
