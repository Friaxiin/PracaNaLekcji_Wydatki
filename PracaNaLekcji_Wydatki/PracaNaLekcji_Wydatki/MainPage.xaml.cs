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
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expense.txt");
        List<Expense> expensesList = new List<Expense>();
        public MainPage()
        {
            InitializeComponent();
            //Display();
            DisplayTxt();
            Console.WriteLine("XD" + path);
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

        public void DisplayTxt()
        {
            List<string> strings = File.ReadAllLines(path).ToList();
            List<Expense> expenses = new List<Expense>();
            Expense expense = new Expense();
            Console.WriteLine("Amogus" + strings.Count);

            foreach (var item in strings)
            {
                string[] substring = item.Split(';');
                expense.Name = substring[0];
                expense.Value = decimal.Parse(substring[1]);
                expense.Date  = DateTime.Parse(substring[2]);
                expenses.Add(expense);
            }

            ExpenseList.ItemsSource = expenses;
        }
        private void AddExpenseTxt(object sender, EventArgs e)
        {
            Expense expense = new Expense();
            if (string.IsNullOrEmpty(nameEntry.Text))
            {
                expense.Date = datePicker.Date;
                expense.Name = "Brak tytulu";
                expense.Value = decimal.Parse(valueEntry.Text);
            }
            else
            {
                expense.Date = datePicker.Date;
                expense.Name = nameEntry.Text;
                expense.Value = decimal.Parse(valueEntry.Text);
            }

            expensesList.Add(expense);

            List<string> strings = new List<string>();
            foreach(var item in expensesList)
            {
                strings.Add(item.Name + "; " + item.Value + "; " + item.Date);
            }
            File.WriteAllLines(path, strings);

            Display();
            DisplayAlert("Informacja", "Pomyślnie dodano wydatek", "OK");
            nameEntry.Text = valueEntry.Text = String.Empty;
        }
    }
}
