using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            Display();
            //string path1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expenses.txt");
            //Console.WriteLine("XD" + path1);
        }
        
        private void DetailsPage(object sender, ItemTappedEventArgs e)
        {
            Expense expense = (Expense)ExpenseList.SelectedItem;

            if(expense == null)
            {
                DisplayAlert("Błąd", "Wybierz element z listy", "OK");
            }
            Navigation.PushAsync(new ExpenseDetails((ExpenseList.SelectedItem as Expense).Date));
        }
        public void Display()
        {
            List<Expense> tmp = new List<Expense>();
            for (int i = 0; i < App.Database.GetAll().Count; i++)
            {
                bool exists = false;
                for (int j = 0; j < tmp.Count; j++)
                {
                    if (App.Database.GetAll()[i].Date == tmp[j].Date)
                    {
                        exists = true;
                    }
                }
                if (!exists)
                {
                    tmp.Add(App.Database.GetAll()[i]);
                }
            }
            ExpenseList.ItemsSource = tmp;
        }
        private void AddExpense(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameEntry.Text))
            {
                App.Database.Add(new Expense() { Name = "Brak tytulu", Value = decimal.Parse(valueEntry.Text), Date = datePicker.Date});
            }
            else
            {
                App.Database.Add(new Expense() { Name = nameEntry.Text, Value = decimal.Parse(valueEntry.Text), Date = datePicker.Date });
            }
            Display();
            DisplayAlert("Informacja", "Pomyślnie dodano wydatek", "OK");
            nameEntry.Text = valueEntry.Text = String.Empty;
        }
    }
}
