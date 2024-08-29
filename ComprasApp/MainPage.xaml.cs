using System.Collections.ObjectModel;
using ComprasApp.Models;

namespace ComprasApp
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto> ();

        public MainPage()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista_produtos;
        }


        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => i.Total);
            string msg = $"O total dos produtos é {soma:C}";
            DisplayAlert("Resultado", msg, "Fechar");
        }

        private async void ToolbarItem_Clicked_Add(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.NovoProduto());
        }

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }

}
