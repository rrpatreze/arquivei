using DesafioArquivei.Controllers;
using System;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Desafio Arquivei");

            try
            {
                ReceiverController rc = new ReceiverController();
                Entity.ReceivedResponse respAPI = rc.GetNFeFromAPI();

                rc.InsertNFeDataBase(respAPI);
            }
            catch (Exception)
            {

            }
        }
    }
}
