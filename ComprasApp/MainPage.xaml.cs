using System.Collections.ObjectModel;
using ComprasApp.Models;

namespace ComprasApp
{
    public partial class MainPage : ContentPage
    {
        // ObservableCollection serve para notificar automaticamente quando ouver algum tipo de alteração na variavel lista_produtos
        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

        public MainPage()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista_produtos; // Adicionando a lista_produtos a fonte de items da ListView da main page
        }


        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e) // Criando uma função para somar o total de todos os valores dos produtos
        {
            double soma = lista_produtos.Sum(i => i.Total);
            string msg = $"O total dos produtos é {soma:C}";
            DisplayAlert("Resultado", msg, "Fechar");
        }

        private async void ToolbarItem_Clicked_Add(object sender, EventArgs e) // Função para navegar até a pagina de Adicionar produto
        {
            await Navigation.PushAsync(new Views.NovoProduto()); // Navegar até a pagina
        }

        // onAppearing é uma função que dispara a cada reload
        protected async override void OnAppearing() // Função para carregar a lista_produtos com os dados do banco
        {
            if (lista_produtos.Count == 0)
            {
                List<Produto> tmp = await App.Database.GetAll();
                foreach (Produto p in tmp)
                {
                    lista_produtos.Add(p);
                }
            }
        }

        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e) // Função para buscar produtos, disparada cada vez que o texto muda no input
        {
            // Usando try para o tratamento de erro
            try 
            {
                string q = e.NewTextValue;
                lista_produtos.Clear(); // Limpando a lista de produtos para carregar denovo apenas com o produtos filtrados

                List<Produto> tmp = await App.Database.Search(q); // Fazendo a busca no banco
                foreach (Produto p in tmp)
                {
                    lista_produtos.Add(p); // Adicionando os itens carregados na lista
                }
            } catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Fechar"); // Mensagem em caso de erro
            }
        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e) // Função para navegar até a página de Editar produto
        {
            Produto? p = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.EditarProduto 
            {
                BindingContext = p
            }); // Navegando até a pagina EditarProduto, enviando o item selecionado
        }

        private async void MenuItem_Clicked(object sender, EventArgs e) // Função para remover um item
        {
            try
            {
                MenuItem selecionado = sender as MenuItem; // 'as' é usado para deixar claro que o objeto que chegou é um MenuItem
                Produto p = selecionado.BindingContext as Produto; // Passando o objeto para a variavel avisando que é um Produto

                bool confirm = await DisplayAlert("Tem certeza?", "Remover Produto?", "Sim", "Não"); // Alert para confirma a acção
                if (confirm)
                {
                    await App.Database.Delete(p.Id); // Deletando o item do banco
                    await DisplayAlert("Sucesso!", "Produto removido", "Ok");
                    lista_produtos.Remove(p);
                }
            }
            catch (Exception ex) 
            {
                await DisplayAlert("Ops", ex.Message, "Fechar");
            }
        }
    }
}
