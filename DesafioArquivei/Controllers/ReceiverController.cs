using System;
using System.Text.RegularExpressions;
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
        // GET api/receiver/5
        [HttpGet]
        public ActionResult<string> index()
        {
            return "Desafio Arquivei - Index";
        }

        // GET api/receiver/5
        [HttpGet("{id}")]
        public ActionResult<string> GetNFeFromDataBase(string id)
        {
            try
            {
                DAOBase<NotaFiscal> dao = new DAOBase<NotaFiscal>();

                NotaFiscal nf = null;

                nf = dao.Get(w => w.access_key == id);
                if (nf != null)
                {
                    string sTotalValue = nf.total_nota.ToString();
                    return "Nota: \"" + id + "\"\nValor: " + sTotalValue;
                }
                else
                {
                    throw new Exception("Nota: \"" + id + "\" nao encontrada");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: {0}", e);
                return e.Message;
            }
        }
    }
}