using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Models
{
    public abstract class LibraryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime AddedDate { get; set; }

        public LibraryItem(int id , string title, DateTime addedDate)
        {
            Id = id ;
            Title = title ;
            AddedDate = addedDate ;
        }
        public abstract void GetInfo();
    }
}
