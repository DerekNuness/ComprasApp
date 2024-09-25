using ComprasApp.Models;

namespace ComprasApp.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e) // Função para editar o produto
    {
		try
		{
			Produto produto_anexado = BindingContext as Produto; 

			Produto p = new Produto()
			{
				Id = produto_anexado.Id,
				Descricao = txt_descricao.Text,
				Preco = Convert.ToDouble(txt_preco.Text),
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
			}; // Instanciando um novo produto com as informações contidas no XAML

			await App.Database.Update(p); // Atualizando os dados no banco de dados
			await DisplayAlert("Sucesso", "Atualizado", "Ok");
			await Navigation.PushAsync(new MainPage()); // Navegando de volta a MainPage
		}
		catch (Exception ex) 
		{
			await DisplayAlert("ops", ex.Message, "Fechar");
		}
    }
}