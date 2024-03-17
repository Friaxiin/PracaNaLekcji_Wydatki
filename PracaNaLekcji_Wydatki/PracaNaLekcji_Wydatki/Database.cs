using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PracaNaLekcji_Wydatki
{
    public class Database
    {
        static string path;

        public Database(string dbPath)
        {
            path = dbPath;
        }
        public List<Expense> GetAll()
        {
            if(File.Exists(path))
            {
                List<string> results = File.ReadAllLines(path).ToList();
                List<Expense> expenses = new List<Expense>();

                foreach (var item in results)
                {
                    string[] entries = item.Split(';');

                    if (entries.Length == 3)
                    {
                        Expense tmp = new Expense();
                        tmp.Name = entries[0];
                        tmp.Value = decimal.Parse(entries[1]);
                        tmp.Date = DateTime.Parse(entries[2]);

                        expenses.Add(tmp);
                    }
                }
                return expenses;
            }
            else
            {
                File.WriteAllLines(path, new List<string>());
                return new List<Expense>();
            }
        }
        public void Add(Expense expense)
        {
            List<Expense> tmp = GetAll();
            tmp.Add(expense);
            WriteToFile(tmp);
        }
        public static void WriteToFile(List<Expense> expenses)
        {
            List<string> output = new List<string>();
            foreach (var item in expenses)
            {
                string line = $"{item.Name};{item.Value};{item.Date}";
                output.Add(line);
            }
            File.WriteAllLines(path, output);
        }
        public void Remove(Expense expense)
        {
            List<Expense> tmp = GetAll();
            for(int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].Name == expense.Name)
                {
                    tmp.RemoveAt(i);
                }
            }
            WriteToFile(tmp);
        }
    }
}
