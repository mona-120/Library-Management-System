using Library_Management_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Models
{
    public class Books : LibraryItem , ISearchable
    {
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; } = true;

        public Books(int id , string title , DateTime addedDate, string author , int year , string genre ) : base(id, title, addedDate)
        {
            Author = author;
            Year = year;
            Genre = genre;
        }

        public override void GetInfo()
        {
            if(IsAvailable == false)
                Console.WriteLine("Book isn't available!");
            else
            Console.WriteLine($"ID : {Id} , Title : {Title} , Auther : {Author} , Genre : {Genre}"); 
        }
        public bool MatchesQuery(string query)
        {
            if(query == null || query.Length == 0)
                return false;
            else
               return(Title.Contains(query,StringComparison.OrdinalIgnoreCase)
                      || Author.Contains(query,StringComparison.OrdinalIgnoreCase));
        }
    }
}
