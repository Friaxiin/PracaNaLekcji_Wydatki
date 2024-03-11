using System;
using System.Collections.Generic;
using System.Text;

namespace PracaNaLekcji_Wydatki
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
