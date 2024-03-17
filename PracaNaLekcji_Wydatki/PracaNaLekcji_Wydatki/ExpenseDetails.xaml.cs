using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracaNaLekcji_Wydatki
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseDetails : ContentPage
    {
        DateTime date;
        public ExpenseDetails(DateTime date)
        {
            InitializeComponent();

            this.date = date;
            Display();
        }
        public void Display(List<Expense> filter = null)
        {
            List<Expense> tmp = new List<Expense>();
            List<Expense> expenses = App.Database.GetAll();
            if(filter != null)
            {
                expenses = filter;
            }
            for (int i = 0; i < expenses.Count; i++)
            {
                if (expenses[i].Date == date)
                {
                    tmp.Add(expenses[i]);
                }
            }
            List.ItemsSource = tmp;

        }
        private void Delete(object sender, EventArgs e)
        {
            var btn = sender as Button;
            Expense expense = btn.BindingContext as Expense;
            App.Database.Remove(expense);

            Display();
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            List<Expense> tmp = new List<Expense>();
            for(int i = 0; i < App.Database.GetAll().Count; i++)
            {
                if (App.Database.GetAll()[i].Name.Contains(searchbar.Text))
                {
                    tmp.Add(App.Database.GetAll()[i]);
                }
            }
            Display(tmp);
        }
    }
}