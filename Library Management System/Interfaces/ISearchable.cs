using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Management_System.Interfaces
{
    public interface ISearchable
    {
        public bool MatchesQuery(string query);
    }
}
