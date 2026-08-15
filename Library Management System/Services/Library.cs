using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Services
{
    public class Library
    {
        private Books[] books = new Books[50];
        private Member[] members = new Member[50];
        private BorrowRecord[] borrowRecords = new BorrowRecord[50];

        private int BookId = 0;
        private int BorrowId = 0;
        private int MemberId = 0;


        // Adding new book
        public void AddBook(string title , string author , string genre , int year)
        {
            if(string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(genre))
            {
                throw new ArgumentException("Please Fill the book title ,author name ,and book genre!");
            }
            var newBook = new Books(BookId, title , DateTime.Now , author, year, genre);
            books[BookId] = newBook;
            BookId++;
            Console.WriteLine("Added new book!");
        }


        // Adding new member
        public void AddMember(string name , string email , bool isPremium)
        {
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email)){
                Console.WriteLine("Please enter your email and your name");
            }
            else
            {
                if (isPremium)
                {
                    var newPreMember = new PremiumMember(MemberId, name , email , DateTime.Now);
                    Console.WriteLine("Added new Premium Member!");
                }
                else
                {
                    var newMember = new Member(MemberId, name, email, DateTime.Now);
                    Console.WriteLine("Added new Regular Member!");
                    members[MemberId] = newMember;
                    MemberId++;
                }
                
            }
        }

        // Borrow a book
        public void BorrowBook(int bookId , int memberId)
        {
            var book = FindBook(bookId);
            var member = FindMember(memberId);
            if (book == null || member == null)
            {
                throw new Exception("Can't Borrow the book!");
            }

            if (book.IsAvailable == false)
                {
                    throw new Exception("Book is't available!");
            }
                
              var newBorrowRecord = new BorrowRecord(BorrowId, book, member, DateTime.Now);
                        book.IsAvailable = false;
                        Console.WriteLine($"New book is Borrowed by {member.Name}");
                        borrowRecords[BorrowId] = newBorrowRecord;
                        BorrowId++;      
        }
            
        


        // Return a book
        public void ReturnBook(int bookId)
        {
            var book = FindBook(bookId);
            if (book == null) throw new Exception("Wrong Id");
            book.IsAvailable = true;
            foreach(var record in borrowRecords)
            {
                if (record != null && record.Book.Id == bookId)
                {
                    record.ReturnDate = DateTime.Now;
                }
            }
        }

        // Search for a book or member
        public void Search(string query)
        {
            foreach(var book in books )
            {
                if (book != null && book.MatchesQuery(query))
                {
                    Console.WriteLine($"Book Title: {book.Title} , Book Genre: {book.Genre} , Author Name: {book.Author}");
                }
            }
            foreach(var member in members )
            {
                if (member != null && member.MatchesQuery(query))
                {
                    Console.WriteLine($"Member ID: {member.Id} , JoinDate: {member.JoinDate} , Email: {member.Email}");
                    
                }
                return;
            }
            Console.WriteLine("Not Found");
        }

        // Get Info For Available Books
        public void AvailableBooks()
        {
            foreach(var book in books)
            {
                if(book != null && book.IsAvailable)
                {
                    book.GetInfo();
                }
            }
        }

        // Display a member that borrow a book using id
        public void BorrowHistory(int memberId)
        {
            Member member = FindMember(memberId);
            if (member == null) throw new Exception("MemberId not found!");
            foreach(var records in borrowRecords)
            {
                if(records != null && memberId == records.Member.Id)
                {
                    member.GetInfo();
                }
            }
        }

        // Report of Delay
        public void DelayReport()
        {
            foreach(var record in borrowRecords)
            {
                if (record != null && record.IsLate())
                {
                    Console.WriteLine($"Name : {record.Member.Name} , Title of book : {record.Book.Title} , Total Days of late : {DateTime.Now - record.BorrowDate}");
                }
            }
        }

        // Find book by id
        public Books FindBook(int bookId)
        {
            foreach (var book in books)
            {
                if (book != null && bookId == book.Id)
                {
                    return book;
                }
            }
            return null;
        }

        // find member by id
        public Member FindMember(int memberId)
        {
            foreach(var member in members)
            {
                if (member != null && memberId == member.Id)
                    return member;
            }
            return null;
        }


        // Seeding Data
        public void SeedData()
        {
            AddBook("Harry Potter", "J.K. Rowling", "Fantasy", 1997);
            AddBook("Clean Code", "Robert C. Martin", "Programming" , 2008);

            AddMember("Sara Hassan", "sara@gmail.com" ,isPremium: true);
            AddMember("Ahmed Ali", "ahmed@gmail.com", isPremium:false);

            BorrowBook(0,0);
        }
    }
}
