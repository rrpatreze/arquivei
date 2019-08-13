using DAO;
using DesafioArquivei.Controllers;
using Entity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Text.RegularExpressions;

namespace Receiver
{
    class DesafioArquiveiMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Desafio Arquivei");

            try
            {
                Receiver reciever = new Receiver();

                Entity.ReceivedResponse resp = reciever.GetNFeFromAPI();

                reciever.InsertNFeDataBase(resp);

            }
            catch (Exception)
            {

            }
        }
    }
}
