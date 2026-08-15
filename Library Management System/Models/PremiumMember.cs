using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Models
{
    public class PremiumMember : Member 
    {
        private const int MaxBorrowLimit = 10;
        private const int LoanDays = 30;

        public PremiumMember(int id , string name , string email , DateTime joinDate) : base(id , name , email , joinDate) { }
        public override void GetInfo()
        {
            base.GetInfo();
        }
    }
}
