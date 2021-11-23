using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Semana_7.Models;
using System.IO;

namespace Semana_7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private SQLiteAsyncConnection con;
        public Login()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>()
                                    .GetConnection();
        }

        public static IEnumerable<Estudiante> SELECT_WHERE(SQLiteConnection db
                                                          , string usuario
                                                          , string contrasenia)
        {
            return db.Query<Estudiante>("Select * from Estudiante where Usuario = ? and Contrasenia = ?", usuario, contrasenia);
        }
        private void btnIniciar_Clicked(object sender, EventArgs e)
        {
            try {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                db.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = SELECT_WHERE(db, txtNombre.Text, txtContrasenia.Text);

                if (resultado.Count() > 0) {

                    Navigation.PushAsync(new ConsultaRegistro());
                }
            }catch(Exception ex) {
                DisplayAlert("Alterta", "Usuario no existe, por favor registrese", "Ok");
            }
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}