﻿using DAO;
using Entity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Receiver
{
    public class Receiver
    {
        private RestRequest InsertHeaders(RestRequest request)
        {
            try
            {
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("x-api-id", "f96ae22f7c5d74fa4d78e764563d52811570588e");
                request.AddHeader("x-api-key", "cc79ee9464257c9e1901703e04ac9f86b0f387c2");

                return request;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao criar request: {0}", e);
                throw;
            }
        }

        /*
         * Metodo que consome a API arquivei para retornar as notas fiscais eletronicas.
         */
        public Entity.ReceivedResponse GetNFeFromAPI()
        {
            string apiBaseURL = null;

            Entity.ReceivedResponse resp = null;

            try
            {
                Console.WriteLine("Buscando as notas na API da Arquivei...");


                apiBaseURL = "https://sandbox-api.arquivei.com.br/v1";

                var client = new RestClient(apiBaseURL);

                RestRequest request = new RestRequest("nfe/received");

                request = this.InsertHeaders(request);

                //Executa o request.
                var response = client.Get(request);

                //Transforma a resposta em um objeto chamado ReceivedResponse
                resp = JsonConvert.DeserializeObject<Entity.ReceivedResponse>(response.Content);

                Console.WriteLine("Notas recebidas com sucesso!");

                return resp;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao obter valores da nota fiscal eletronica: {0}", e);
                throw;
            }
        }

        public void InsertNFeDataBase(Entity.ReceivedResponse respAPI)
        {
            int i = 0;

            try
            {
                Console.WriteLine("Inserindo notas na base de dados...");

                //Para cada nota devolvida.
                //Obtem a chave de acesso e o valor total da nota.
                //Sava a nota no banco de dados.
                foreach (Entity.ReceivedResponse.Datum item in respAPI.data)
                {
                    i += 1;

                    //Obtem a chave de acesso.
                    string accessKey = item.access_key;
                    try
                    {
                        //Obtem o valor total da nota.
                        double valorNF = GetNFTotalValue(item.xml);
                        Console.WriteLine("AccessKey(" + i + "):" + accessKey);
                        Console.WriteLine("NFe Value: " + valorNF);

                        //Atribui os valores ao objeto nota fiscal.
                        NotaFiscal nf = new NotaFiscal();
                        nf.access_key = accessKey;
                        nf.total_nota = valorNF;

                        //Insere a nota(chave e valor) na base de dados.
                        DAOBase<NotaFiscal> dao = new DAOBase<NotaFiscal>();
                        dao.Insert(nf);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("AccessKey(" + i + "):" + accessKey);
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao inserir nota na base de dados: {0}", e);
                throw;
            }
        }

        private double GetNFTotalValue(string base64Enconded)
        {
            string xmlTag = "vNF";

            try
            {
                byte[] data = System.Convert.FromBase64String(base64Enconded);

                string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);

                Regex _Regex = new Regex(@"<" + xmlTag + @"\b[^>]*>(.*?)</" + xmlTag + ">");
                Match vl = _Regex.Match(base64Decoded);

                string resultado = vl.Value.Replace("</" + xmlTag + ">", string.Empty).Replace("<" + xmlTag + ">", string.Empty).Trim();
                return double.Parse(resultado);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao buscar valor total da nota fiscal: {0}", e);
                throw;
            }
        }
    }
}
