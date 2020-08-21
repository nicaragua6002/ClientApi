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
           
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respuesta.tokenDelUsuario);
            var response = client.GetAsync("https://sigi.unan.edu.ni/RAServices/Api/PortalEstudiante/GetPortal");

            //txttoken.Text = response.Result.Content.ReadAsStringAsync().Result;

            List<Perfil> lista = new List<Perfil>();
            lista.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<Perfil>(response.Result.Content.ReadAsStringAsync().Result));

           dataGridView1.DataSource= lista;
        }

        public void GetMarcas()
        {

            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respuesta.tokenDelUsuario);
            var response = client.GetAsync("https://sigi.unan.edu.ni/RAServices/Api/PortalEstudiante/GetMarcas");

           txttoken.Text= response.Result.Content.ReadAsStringAsync().Result;
        }

        public void GetEstadoCuenta()
        {

            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respuesta.tokenDelUsuario);
            var response = client.GetAsync("https://sigi.unan.edu.ni/RAServices/Api/PortalEstudiante/GetEstadoDeCuenta");

            
            List<Cuenta> lista= Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cuenta>>(response.Result.Content.ReadAsStringAsync().Result);

            dataGridView2.DataSource = lista;
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

    class Perfil
    {
        public string Carnet { get; set; }
        public string Facultad { get; set; }
        public string Carrera { get; set; }
        public string PlanDeEstudio { get; set; }
        public string Modalidad { get; set; }
        public string Turno { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string SituacionLaboral { get; set; }
        public string Direccion { get; set; }
        public string CentroDeTrabajo { get; set; }
        public string FechaDeNacimiento { get; set; }
    }

    public class Cuenta
    {
        public string CodigoDelTipoDeMovimiento { get; set; }
        public string TipoDeMovimiento { get; set; }
        public string Periodo { get; set; }
        public string Fecha { get; set; }
        public string Recibo { get; set; }
        public string Concepto { get; set; }
        public string Monto { get; set; }

    }
}
