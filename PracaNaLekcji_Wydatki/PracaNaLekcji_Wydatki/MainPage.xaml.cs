using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PracaNaLekcji_Wydatki
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void DetailsPage(object sender, ItemTappedEventArgs e)
        {
            Expense expense = (Expense)ExpenseList.SelectedItem;

            if(expense == null )
            {
                DisplayAlert("Błąd", "Wybierz element z listy", "OK");
            }
            Navigation.PushAsync(new ExpenseDetails());
        }
    }
}
