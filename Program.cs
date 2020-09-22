using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program

    {
        static void Main(string[] args)
        {
            TcpClient clientSocket = new TcpClient("localhost", 6789);

            Stream ns = clientSocket.GetStream();  //provides a NetworkStream
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing
            //read user inputs
            string userInput = Console.ReadLine();
            

            while (!string.IsNullOrEmpty(userInput))
            {
                //send the user input to the server
                sw.WriteLine(userInput);
                //read server response
                string response = sr.ReadLine();
                //if we can use Jwt token handler to read the response then it must be a token
                if (true)
                {
                   
                    Console.WriteLine("...");
                }
                else
                {
                    Console.WriteLine("Server Says: " + response);
                }

                userInput = Console.ReadLine();
            }
            ns.Close();
            clientSocket.Close();
        }
    }
}
