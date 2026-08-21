using Library_Management_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Models
{
    public class Member : ISearchable
    {
         public readonly int MaxBorrowLimit = 5;
         public readonly int LoanDays = 20;
 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public List<Books> BorrowedBooks { get; set; } = new List<Books>();

        public Member(int id, string name , string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public virtual void GetInfo()
        {
            Console.Write($"ID : {Id} , Name : {Name} , Email : {Email} , JoinDate : {JoinDate}");
        }

         public bool MatchesQuery(string query)
        {
            if(query == null || query.Length == 0)
                return false;
            else
                return (Name.Contains(query,StringComparison.OrdinalIgnoreCase));
        }
    }
}
