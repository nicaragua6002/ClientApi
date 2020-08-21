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
using System.Net.Http.Headers;

namespace ClienteApiRest
{
    public partial class Form1 : Form
    {
        EData respuesta;
        
        private static readonly HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }
     
        private void Form1_Load(object sender, EventArgs e)
        {

            PostToken();
            GetPerfil();
            GetMarcas();
            GetEstadoCuenta();
        }

        public void GetPerfil()
        {
           
            txttoken.Text = respuesta.tokenDelUsuario;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respuesta.tokenDelUsuario);
            var response = client.GetAsync("https://sigi.unan.edu.ni/RAServices/Api/PortalEstudiante/GetPortal");

            MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result);
        }

        public void GetMarcas()
        {

            txttoken.Text = respuesta.tokenDelUsuario;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respuesta.tokenDelUsuario);
            var response = client.GetAsync("https://sigi.unan.edu.ni/RAServices/Api/PortalEstudiante/GetMarcas");

            MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result);
        }

        public void GetEstadoCuenta()
        {

            txttoken.Text = respuesta.tokenDelUsuario;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respuesta.tokenDelUsuario);
            var response = client.GetAsync("https://sigi.unan.edu.ni/RAServices/Api/PortalEstudiante/GetEstadoDeCuenta");

            MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result);
        }




        //conectarse

        public void PostToken()
        {
            var values = new Dictionary<string, string>
                {
                      { "UserName", "18000002" },
                      { "Password", "15061995%" }
                      //{ "Password", "Managua.2020" }
                };

            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync("https://sigi.unan.edu.ni/RAServices/Api/Autorizacion/Login", content);


            MessageBox.Show(response.Result.ToString());

            //MessageBox.Show(response.Result.Content.ReadAsStringAsync().Result);


            respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<EData>(response.Result.Content.ReadAsStringAsync().Result);
            
        }
    }

    class EData
    {

        public string cuentaDeLUsuario { get; set; }
        public string tokenDelUsuario { get; set; }
    }
}
