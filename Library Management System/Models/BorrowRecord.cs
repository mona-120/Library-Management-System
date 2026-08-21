using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public Books Book { get; set; }
        public Member Member { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; } = null;

        public BorrowRecord(int id, Books book, Member member)
        {
            Id = id;
            Book = book;
            Member = member;
        }
        public bool IsLate()
        {
            if((ReturnDate == null) && ((DateTime.Now - BorrowDate).TotalDays > 14))
                return true;
            else return false;
        }
    }
}
