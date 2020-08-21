using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;


namespace ClienteApiRest
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }
     
        private void Form1_Load(object sender, EventArgs e)
        {
            var values = new Dictionary<string, string>
                {
                      { "value", "rsolis1" },
                 { "id", "1" }
                      //{ "Password", "Managua.2020" }
                };

            var content = new FormUrlEncodedContent(values);

            var response =  client.PostAsync("http://localhost:53416/Api/Values", content);

        


            MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result);


           

        }
    }
}
