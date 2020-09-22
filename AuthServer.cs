using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer
{
    class AuthServer
    {
        List<List<String>> listOfChunks = new List<List<string>>();

        public FileStream Listen(int port)
        {
            //TcpListener serverSocket = new TcpListener(port);
            TcpListener server = new TcpListener(IPAddress.Loopback, port);
            server.Start();
            Console.WriteLine("Server listning on port: " + port);

            TcpClient connectionSocket = server.AcceptTcpClient();
            Console.WriteLine("Server activated");

            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string request = sr.ReadLine();
           
            while (!string.IsNullOrEmpty(request))
            {
                if (request == "Jeg er fri")
                {
                    using (FileStream fs = new FileStream(@"C:\Zealand\4. semester\IT Security\Projekter\PasswordCrackerCentralized - Kopi\PasswordCrackerCentralized\PasswordCrackerCentralized\bin\Debug\webster-dictionary.txt", FileMode.Open, FileAccess.Read))
                    using (StreamReader dictionary = new StreamReader(fs))
                    {
                        List<string> chunk = new List<string>();
                        int counter = 1;

                        while (!dictionary.EndOfStream) //gennemløber vores liste af ord fra streamet indtil der ikke er flere.
                        {
                           chunk.Add(dictionary.ReadLine()); 
                            if(counter++ % 10000 == 0) // tager 10000 af gangen via modulos
                            {
                                listOfChunks.Add(chunk); // Tilføjer en chunk på 10000 ord til listen listOfChunks
                                chunk = new List<string>(); // laver en ny, tom chunk-liste til at fylde ord i.
                            }

                        }
                        listOfChunks.Add(chunk); // tilføjer de resterende ord på listen, som ikke går op i 10000, i et chunk
                    }
                 
                }
                else
                {
                    //Returnerer password og sende en liste
                }
                try
                {
                    request = sr.ReadLine();
                }
                catch (Exception e)
                {
                    request = null;
                    Console.WriteLine("Client got crazy");
                }


            }
            Console.WriteLine("Press Enter to Close");
            Console.ReadKey();
            ns.Close();
            connectionSocket.Close();
            server.Stop();
            return null;
        }


    }


}


