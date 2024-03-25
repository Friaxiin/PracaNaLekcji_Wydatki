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
        int id = 0;
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expense5.txt");
        List<Expense> expensesList = new List<Expense>();
        public MainPage()
        {
            InitializeComponent();

            DisplayTxt();
            Console.WriteLine("XD" + path);
        }
        public void ReadId()
        {
            if (File.Exists(path))
            {
                List<string> strings = new List<string>();
                strings = File.ReadAllLines(path).ToList();
                id = strings.Count();
                id++;
            }
        }        
        private void DetailsPage(object sender, ItemTappedEventArgs e)
        {
            Expense expense = (Expense)ExpenseList.SelectedItem;

            if(expense == null)
            {
                DisplayAlert("Błąd", "Wybierz element z listy", "OK");
            }
            Navigation.PushAsync(new ExpenseDetails((ExpenseList.SelectedItem as Expense)));
        }
        public void DisplayTxt()
        {
            if(File.Exists(path))
            {
                List<string> strings = File.ReadAllLines(path).ToList();
                List<Expense> expenses = new List<Expense>();
                Expense expense = new Expense();
                DateTime LastDate = DateTime.Now;

                Console.WriteLine("Amogus" + strings.Count);

                foreach (var item in strings)
                {
                    string[] substring = item.Split(';');
                    if (DateTime.Parse(substring[3]) != LastDate)
                    {
                        expense.Id = int.Parse(substring[0]);
                        expense.Name = substring[1];
                        expense.Value = decimal.Parse(substring[2]);
                        expense.Date = DateTime.Parse(substring[3]);

                        LastDate = DateTime.Parse(substring[3]);

                        expenses.Add(expense);
                    }
                }

                ExpenseList.ItemsSource = expenses;
            }
        }
        
        private void AddExpenseTxt(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                expensesList = Read();
                Expense expense = new Expense();
                ReadId();
                if (string.IsNullOrEmpty(nameEntry.Text))
                {
                    expense.Id = id;
                    expense.Date = datePicker.Date;
                    expense.Name = "Brak tytulu";
                    expense.Value = decimal.Parse(valueEntry.Text);
                }
                else
                {
                    expense.Id = id;
                    expense.Date = datePicker.Date;
                    expense.Name = nameEntry.Text;
                    expense.Value = decimal.Parse(valueEntry.Text);
                }

                expensesList.Add(expense);

                List<string> strings = new List<string>();
                foreach (var item in expensesList)
                {
                    strings.Add(item.Id + "; " + item.Name + "; " + item.Value + "; " + item.Date);
                }
                File.WriteAllLines(path, strings);

                DisplayAlert("Informacja", "Pomyślnie dodano wydatek", "OK");
                nameEntry.Text = valueEntry.Text = String.Empty;
                DisplayTxt();
            }    
            else
            {
                File.WriteAllText(path, "");
            }
        }
        public List<Expense> Read()
        {
            if (File.Exists(path))
            {
                List<string> strings = File.ReadAllLines(path).ToList();
                List<Expense> expenses = new List<Expense>();
                Expense expense = new Expense();

                foreach (var item in strings)
                {
                    string[] substring = item.Split(';');
                    expense.Id = int.Parse(substring[0]);
                    expense.Name = substring[1];
                    expense.Value = decimal.Parse(substring[2]);
                    expense.Date = DateTime.Parse(substring[3]);
                    expenses.Add(expense);
                }
                return expenses;
            }
            return null;
        }
    }
}
