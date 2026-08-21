using Library_Management_System.Interfaces;
using Library_Management_System.Models;
using Library_Management_System.Services;
using System.Runtime.Intrinsics.X86;
namespace Library_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var library = new Library();
            library.SeedData();


            bool Exit = false;
            do
            {

                Console.WriteLine("=========================Welcome to the library=================================");
                Console.WriteLine("choose 1 to Add new book");
                Console.WriteLine("choose 2 to Add new member");
                Console.WriteLine("choose 3 to borrow a book");
                Console.WriteLine("choose 4 to return a book");
                Console.WriteLine("choose 5 to search");
                Console.WriteLine("choose 6 to Display Available books");
                Console.WriteLine("choose 7 to display the borrow record of a member");
                Console.WriteLine("choose 8 to display the late members");
                Console.WriteLine("choose 0 to Exit");
                Console.WriteLine("============================================================");


                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.Write("To add a book, Enter Book Title: ");
                            string title = Console.ReadLine();
                            Console.Write("author name: ");
                            string auther = Console.ReadLine();
                            Console.Write("Book Genre: ");
                            string genre = Console.ReadLine();
                            Console.Write("Year: ");
                            int year = int.Parse(Console.ReadLine());
                            library.AddBook(title, auther, genre, year);
                            break;
                        case 2:
                            Console.Write("To add a member, Enter your name: ");
                            string name = Console.ReadLine();
                            Console.Write("Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Premium or not [true / false]: ");
                            bool isPrieum = bool.Parse(Console.ReadLine());
                            library.AddMember(name, email, isPrieum);
                            break;
                        case 3:
                            Console.WriteLine("To Borrow a book, Enter book id: ");
                            int bookId = int.Parse(Console.ReadLine());
                            Console.Write("your id: ");
                            int memberId = int.Parse(Console.ReadLine());
                            library.BorrowBook(bookId, memberId);
                            break;
                        case 4:
                            Console.Write("To return a book, Enter book id: ");
                            int bookId_ = int.Parse(Console.ReadLine());
                            library.ReturnBook(bookId_);
                            break;
                        case 5:
                            Console.Write("To search by keyword, Enter a keyword: ");
                            string word = Console.ReadLine();
                            library.Search(word);
                            break;
                        case 6:
                            Console.WriteLine("Information about available books : ");
                            library.AvailableBooks();
                            break;
                        case 7:
                            Console.Write("To display a member that borrow a book, Enter his id: ");
                            int id = int.Parse(Console.ReadLine());
                            library.BorrowHistory(id);
                            break;
                        case 8:
                            Console.WriteLine("Members who are late to return books: ");
                            library.DelayReport();
                            break;
                        case 0:
                            Console.WriteLine("Finished!");
                            Exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid Operation, Please try again");
                            break;

                    }

                }
                catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }

            } while(!Exit);

        }
    }
}
