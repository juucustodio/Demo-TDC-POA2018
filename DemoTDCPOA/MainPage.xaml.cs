using System.Linq;
using LiteDB;
using Xamarin.Forms;

namespace DemoTDCPOA
{
    public partial class MainPage : ContentPage
    {
        LiteDatabase _dataBase;
        LiteCollection<Pessoa> Pessoas;
        public MainPage()
        {
            InitializeComponent();

            _dataBase = new LiteDatabase(DependencyService.Get<IHelper>().GetFilePath("Banco.db"));
            Pessoas = _dataBase.GetCollection<Pessoa>();
            ListPessoas.ItemsSource = Pessoas.FindAll();
            BindingContext = this;
        }

        private void Insert(object sender, System.EventArgs e)
        {

            int idCustomer = Pessoas.Count() == 0 ? 1 : (int)(Pessoas.Max(x => x.Id) + 1);
            Pessoa pessoa = new Pessoa
            {
                Id = idCustomer,
                Name = EntryName.Text,
            };

            Pessoas.Insert(pessoa);

            ListPessoas.ItemsSource = Pessoas.FindAll();

        }



        private void Get(object sender, System.EventArgs e)
        {
            Pessoas = _dataBase.GetCollection<Pessoa>();

            if (Pessoas.Count() > 0)
            {
                var customer = Pessoas.FindAll().FirstOrDefault(x => x.Name == EntryName.Text);
                DisplayAlert("id: " + customer?.Id, "Name: " + customer?.Name, "ok");
            }
        }

        private async void List_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var action = await DisplayAlert("Atenção", "Deletar ?", "Sim", "Não");

            if (action)
            {
                var pessoa = e.SelectedItem as Pessoa;

                Pessoas.Delete(pessoa?.Id);

                ListPessoas.ItemsSource = Pessoas.FindAll();
            }
        }
    }
}
