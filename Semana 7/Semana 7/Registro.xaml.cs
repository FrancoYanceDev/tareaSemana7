using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Semana_7.Models;

namespace Semana_7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {

        private SQLiteAsyncConnection con;
        public Registro()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>()
                                   .GetConnection();
        }

        private void tbnAgregar_Clicked(object sender, EventArgs e)
        {
            try {
                var Registros = new Estudiante()
                {
                    Nombre = txtNombre.Text,
                    Usuario = txtUsuario.Text,
                    Contrasenia = txtContrasenia.Text
                };
                DisplayAlert("Alterta", "Dato ingresado", "Ok");
                con.InsertAsync(Registros);
                txtNombre.Text = "";
                txtUsuario.Text = "";
                txtContrasenia.Text = "";
            } catch (Exception ex) {
                DisplayAlert("Alterta", ex.Message, "Ok");
            }
        }
    }
}