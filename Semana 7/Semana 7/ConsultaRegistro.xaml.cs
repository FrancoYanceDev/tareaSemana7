using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using System.Collections.ObjectModel;
using Semana_7.Models;

namespace Semana_7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Estudiante> tableEstudiantes;
        public ConsultaRegistro()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            consulta();
        }


        public async void consulta()
        {
            var registros = await con.Table<Estudiante>()
                                     .ToListAsync();

            tableEstudiantes = new ObservableCollection<Estudiante>(registros);
            ListaUsuario.ItemsSource = tableEstudiantes;
        }

        private void ListaUsuario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Estudiante)e.SelectedItem;
            var item = obj.Id.ToString();
            int id = Convert.ToInt32(item);

            try
            {
                Navigation.PushAsync(new Elemento(id));
            }
            catch (Exception ex)
            {

            }
        }
    }
}