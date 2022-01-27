using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var teams = new List<Team>()
            {
                new Team { Name = "Бавария", Country ="Германия" },
                new Team { Name = "Барселона", Country ="Испания" }
            };
            
            var players = new List<Player>()
            {
                new Player {Name="Месси", Team="Франция"},
                new Player {Name="Неймар", Team="Барселона"},
                new Player {Name="Роббен", Team="Бавария"}
            };
 
            // example #1
            var resultOne = from pl in players
            join t in teams on pl.Team equals t.Name
            select new { Name = pl.Name, Team = pl.Team, Country = t.Country };
 
            // example #2
            var resultTwo = players.Join(teams, // 1 collection
                p => p.Team, // object selector property from the first collection
                t => t.Name, // object selector property from the two collection
                (p, t) => new { Name = p.Name, Team = p.Team, Country = t.Country }); // result
            
            foreach (var item in resultOne)
                Console.WriteLine($"{item.Name} - {item.Team} ({item.Country})");
            
            Console.WriteLine();
            
            foreach (var item in resultTwo)
                Console.WriteLine($"{item.Name} - {item.Team} ({item.Country})");
        }

        private class Player
        {
            public string Name { get; init; } = null!;
            public string Team { get; init; } = null!;
        }

        private class Team
        {
            public string Name { get; init; } = null!;
            public string Country { get; init; } = null!;
        }
    }
}