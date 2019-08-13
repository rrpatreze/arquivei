using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtCarregar_Click(object sender, EventArgs e)
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
