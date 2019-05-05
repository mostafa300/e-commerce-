using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IA_E_commerce_.Models;
namespace IA_E_commerce_.ModelView
{
    public class AllTeams
    {
        public List<Team> Team { get; set; }
        public ApplicationDbContext _context { get; set; }
        public AllTeams()
        {
            _context = new ApplicationDbContext();
            Team = new List<Team>();

            Team = (from d in _context.Team select d).ToList();
            
        }
    }
}