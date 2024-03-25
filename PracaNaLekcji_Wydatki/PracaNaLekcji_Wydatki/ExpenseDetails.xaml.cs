using System;
using System.Collections.Generic;
using System.IO;
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
        Expense Expense = new Expense();
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expense4.txt");
        public ExpenseDetails(Expense expense)
        {
            InitializeComponent();

            this.date = expense.Date;
            DisplayTxt();
        }
        public void DisplayTxt()
        {
            List<string> strings = File.ReadAllLines(path).ToList();
            List<Expense> expenses = new List<Expense>();
            Expense expense = new Expense();
            Console.WriteLine("Amogus" + strings.Count);

            foreach (var item in strings)
            {
                string[] substring = item.Split(';');
                    expense.Id = int.Parse(substring[0]);
                    expense.Name = substring[1];
                    expense.Value = decimal.Parse(substring[2]);
                    expense.Date = DateTime.Parse(substring[3]);

                if (expense.Date == date)
                {
                    expenses.Add(expense);
                }
            }

            List.ItemsSource = expenses;
        }
        private void Delete(object sender, EventArgs e)
        {
            var btn = sender as Button;
            Expense expense = btn.BindingContext as Expense;
            App.Database.Remove(expense);

            DisplayTxt();
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
        }
    }
}