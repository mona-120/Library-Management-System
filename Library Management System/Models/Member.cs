using Library_Management_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Models
{
    public class Member : ISearchable
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public Books[] BorrowedBooks { get; set; }

        public Member(int id, string name , string email , DateTime joinDate)
        {
            Id = id;
            Name = name;
            Email = email;
            JoinDate = DateTime.Now;
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
