using DesafioArquivei.Controllers;
using System;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ReceiverController rc = new ReceiverController();
            bool b = rc.getNFe();
        }
    }
}
