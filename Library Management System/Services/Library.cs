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

        private int BookId = 1;
        private int BorrowId = 1;
        private int MemberId = 1;


        // Adding new book
        public void AddBook(string title , string author , string genre , int year)
        {
            if(string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(genre))
            {
                throw new ArgumentException("Please Fill the book title ,author name ,and book genre!");
            }
            var newBook = new Books(BookId, title , DateTime.Now , author, year, genre);
            EnsureCapacity(ref books, BookId);
            books[BookId] = newBook;
            BookId++;
            Console.WriteLine("Added new book!");
        }


        // Adding new member
        public void AddMember(string name , string email , bool isPremium)
        {
            if(string.IsNullOrWhiteSpace(name)){
                Console.WriteLine("Please enter your name");
                return;
            }
            else if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Please enter your Email");
                return;
            }
            else
            {
                EnsureCapacity(ref members, MemberId);
                if (isPremium)
                {
                    var newPreMember = new PremiumMember(MemberId, name , email);
                    Console.WriteLine("Added new Premium Member!");
                    members[MemberId] = newPreMember;
                    MemberId++;
                }
                else
                {
                    var newMember = new Member(MemberId, name, email);
                    Console.WriteLine("Added new Regular Member!");
                    members[MemberId] = newMember;
                    MemberId++;
                }
                
            }
        }

        // Borrow a book
        public void BorrowBook(int bookId , int memberId)
        {
            var book = FindBook(bookId) ?? throw new KeyNotFoundException("Book not found");
            var member = FindMember(memberId) ?? throw new KeyNotFoundException($"Member with id {memberId} not found");

            if (book.IsAvailable == false)
                {
                    throw new InvalidOperationException("Book is't available!");
            }
                
            if(member.BorrowedBooks.Count >= member.MaxBorrowLimit)
            {
                throw new InvalidOperationException($"Member with id {memberId} has reach the max borrow limit of books");
            }
              var newBorrowRecord = new BorrowRecord(BorrowId, book, member);
            member.BorrowedBooks.Add(book);
                        book.IsAvailable = false;
                        Console.WriteLine($"New book is Borrowed by {member.Name}");
            EnsureCapacity(ref borrowRecords, BorrowId);
                        borrowRecords[BorrowId] = newBorrowRecord;
                        BorrowId++;      
        }
            
        


        // Return a book
        public void ReturnBook(int bookId)
        {
            var book = FindBook(bookId);
            if (book == null) throw new InvalidOperationException("Book not found");
            book.IsAvailable = true;
            foreach(var record in borrowRecords)
            {
                if (record != null && record.Book.Id == bookId && record.ReturnDate == null)
                {
                    record.ReturnDate = DateTime.Now;
                    record.Member.BorrowedBooks.Remove(book);
                    break;
                }
            }
        }

        // Search for a book or member
        public void Search(string query)
        {
            bool isfound = false;
            foreach(var book in books )
            {
                if (book != null && book.MatchesQuery(query))
                {
                    Console.WriteLine($"Book Title: {book.Title} , Book Genre: {book.Genre} , Author Name: {book.Author}");
                    isfound = true;
                }
            }
            foreach(var member in members )
            {
                if (member != null && member.MatchesQuery(query))
                {
                    Console.WriteLine($"Member ID: {member.Id} , JoinDate: {member.JoinDate} , Email: {member.Email}");
                    isfound=true; 
                }
            }
            if (!isfound)
            {
                Console.WriteLine("Not Found");
            }
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
                    Console.WriteLine($"Borrow record Id: {record.Id}");
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


        public void EnsureCapacity<T>(ref T[] arr,int currentCount)
        {
            if(currentCount >= arr.Length)
            {
                Array.Resize(ref arr, arr.Length*2);
            }
        }

        // Seeding Data
        public void SeedData()
        {
            AddBook("Harry Potter", "J.K. Rowling", "Fantasy", 1997);
            AddBook("Clean Code", "Robert C. Martin", "Programming" , 2008);

            AddMember("Sara Hassan", "sara@gmail.com" ,isPremium: true);
            AddMember("Ahmed Ali", "ahmed@gmail.com", isPremium:false);

            BorrowBook(1,1);
        }
    }
}
