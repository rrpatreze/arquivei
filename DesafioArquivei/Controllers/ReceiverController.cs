using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAO;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace DesafioArquivei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiverController : Controller
    {
        private RestRequest InsertHeaders(RestRequest request)
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-api-id", "f96ae22f7c5d74fa4d78e764563d52811570588e");
            request.AddHeader("x-api-key", "cc79ee9464257c9e1901703e04ac9f86b0f387c2");
            return request;
        }

        // GET: Receiver
        public Boolean getNFe()
        {
            string apiBaseURL = "https://sandbox-api.arquivei.com.br/v1";
            var client = new RestClient(apiBaseURL);
            RestRequest request = new RestRequest("nfe/received");
            Entity.ReceivedResponse resp = null;

            request = this.InsertHeaders(request);

            // execute the request
            var response = client.Get(request);

            resp = JsonConvert.DeserializeObject<Entity.ReceivedResponse>(response.Content);

            string accessKey = null;
            double valorNF = 0d;
            int i = 0;

            foreach (Entity.ReceivedResponse.Datum item in resp.data)
            {
                i = i + 1;
                accessKey = item.access_key;
                try
                {
                    valorNF = this.getNFTotalValue(item.xml);
                    Console.WriteLine("AccessKey(" + i + "):" + accessKey);
                    Console.WriteLine("NFe Value: " + valorNF);

                    nota_fiscal nf = new nota_fiscal();
                    nf.access_key = accessKey;
                    nf.total_nota = valorNF;

                    DAOBase<nota_fiscal> dao = new DAOBase<nota_fiscal>();

                    dao.Insert(nf);
                }
                catch (Exception e)
                {
                    Console.WriteLine("AccessKey(" + i + "):" + accessKey);
                    Console.WriteLine("Error: " + e.Message);
                }

            }

            return true;
        }

        // GET api/receiver/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            DAOBase<nota_fiscal> dao = new DAOBase<nota_fiscal>();

            nota_fiscal nf = null;

            nf = dao.Get(w => w.access_key == id);
            if(nf!= null)
            {
                return nf.total_nota.ToString();
            }
            else
            {
                return null;
            }
        }

        /*
        private double getNFTotalValue(string base64Enconded)
        {
            string base64Decoded;
            double ret = 0d;

            byte[] data = System.Convert.FromBase64String(base64Enconded);
            base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);

            //base64Decoded = base64Decoded.Replace("<nfeProc xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"3.10\">", "");
            //base64Decoded = base64Decoded.Replace("</nfeProc>", "");
            //base64Decoded = base64Decoded.Replace("<NFe>", "<NFe xmlns = \"http://www.portalfiscal.inf.br/nfe\" >");

            XmlSerializer serializerPrinc = new XmlSerializer(typeof(Entity.NFePrincipal.nfeProc));
            XmlSerializer serializerSec = new XmlSerializer(typeof(Entity.NFeSecundaria.NFe));

            using (TextReader reader = new StringReader(base64Decoded))
            {
                try
                {
                    var nfePrinc = (Entity.NFePrincipal.nfeProc)serializerPrinc.Deserialize(reader);
                    ret = double.Parse(nfePrinc.NFe.infNFe.total.ICMSTot.vNF.ToString());
                    return ret;
                }
                catch (Exception e)
                {
                    using (TextReader reader2 = new StringReader(base64Decoded))
                    {
                        try
                        {
                            var nfeSec = (Entity.NFeSecundaria.NFe)serializerSec.Deserialize(reader2);
                            ret = double.Parse(nfeSec.infNFe.total.ICMSTot.vNF.ToString());
                            return ret;
                        }
                        catch (Exception e2)
                        {
                            throw new Exception(e2.Message);
                        }
                    }
                }
            }
            return -1d;
        }
    }
    */

        private double getNFTotalValue(string base64Enconded)
        {
            string base64Decoded;
            double ret = 0d;

            byte[] data = System.Convert.FromBase64String(base64Enconded);
            base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);


            string resultado = RetornaValorTag(base64Decoded, "vNF");

            return double.Parse(resultado);


        }

        private string RetornaValorTag(string xml, string tag)
        {
            Regex _Regex = new Regex(@"<" + tag + @"\b[^>]*>(.*?)</" + tag + ">");
            Match vl = _Regex.Match(xml);

            return vl.Value.Replace("</" + tag + ">", string.Empty).Replace("<" + tag + ">", string.Empty).Trim();
        }
    }
}