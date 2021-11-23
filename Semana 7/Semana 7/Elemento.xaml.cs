using Semana_7.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Semana_7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        private SQLiteAsyncConnection con;
        private int IdSeleccionado  = 0;
        public Elemento(int id)
        {
            InitializeComponent();
            IdSeleccionado = id;
            con = DependencyService.Get<Database>()
                        .GetConnection();

            consulta();
        }
        public async void consulta()
        {
            var registros = await con.Table<Estudiante>()
                                     .ToListAsync();

            if (registros != null)
            {
                txtNombre.Text = registros[0].Nombre;
                txtUsuario.Text = registros[0].Usuario;
                txtContrasenia.Text = registros[0].Contrasenia;
            }
           
        }
        public static IEnumerable<Estudiante> Delete(SQLiteConnection db
                                                    , int id)
        {
            return db.Query<Estudiante>("DELETE  from Estudiante where Id = ?", id);
        }

        public static IEnumerable<Estudiante> Update(SQLiteConnection db
                                                    , string nombre
                                                    , string usuario
                                                    ,string contrasenia
                                                    , int id)
        {
            return db.Query<Estudiante>("UPDATE  Estudiante SET Nombre = ? , Usuario = ?, Contrasenia = ?  WHERE Id = ?", nombre, usuario, contrasenia, id);
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                db.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = Update(db, txtNombre.Text, txtUsuario.Text, txtContrasenia.Text, IdSeleccionado);

                DisplayAlert("Alterta", "Se edito correctamente", "Ok");
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception ex)
            {
                DisplayAlert("Alterta", ex.Message, "Ok");
            }
        }

        private void tbnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                db.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = Delete(db, IdSeleccionado);

                DisplayAlert("Alterta", "Se elimino correctamente", "Ok");
                Navigation.PushAsync(new ConsultaRegistro());
             }
            catch (Exception ex)
            {
                DisplayAlert("Alterta", ex.Message, "Ok");
            }
        }

    }
}