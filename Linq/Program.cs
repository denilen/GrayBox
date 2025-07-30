using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq;

internal static class Program
{
    private static void Main(string[] args)
    {
        var teams = new List<Team>()
        {
            new() { Name = "Бавария", Country = "Германия" },
            new() { Name = "Барселона", Country = "Испания" }
        };

        var players = new List<Player>()
        {
            new() { Name = "Месси", TeamName = "Франция" },
            new() { Name = "Неймар", TeamName = "Барселона" },
            new() { Name = "Роббен", TeamName = "Бавария" },
            new() { Name = "Роббен", TeamName = "Бавария" }
        };

        var playerTeam = new List<PlayerTeam>
        {
            new() { Name = "Неймар", TeamName = "Барселона", Country = "Испания" },
            new() { Name = "Роббен", TeamName = "Бавария", Country = "Германия"  },
            new() { Name = "Роббен", TeamName = "Бавария", Country = "Германия"  }
        };

        var playerTeamTwo = playerTeam.ToArray();

        // example #1
        var resultOne = from pl in players
                        join t in teams on pl.TeamName equals t.Name
                        select new { Name = pl.Name, Team = pl.TeamName, Country = t.Country };

        // example #2
        var resultTwo = players.Join(teams, // 1 collection
            p => p.TeamName, // object selector property from the first collection
            t => t.Name, // object selector property from the two collection
            (p, t) => new { Name = p.Name, Team = p.TeamName, Country = t.Country }); // result

        var resultThree = playerTeam.GroupBy(x => x.Name)
            .Select(y => y.First());

        var resultFour = playerTeam.Distinct();

        var resultFive = playerTeamTwo.GroupBy(x => x.Name)
            .Select(y => y.First());

        foreach (var item in resultOne)
            Console.WriteLine($"{item.Name} - {item.Team} ({item.Country})");

        Console.WriteLine();

        foreach (var item in resultTwo)
            Console.WriteLine($"{item.Name} - {item.Team} ({item.Country})");

        Console.WriteLine();

        foreach (var item in resultThree)
            Console.WriteLine($"{item.Name} - {item.TeamName} ({item.Country})");

        Console.WriteLine();

        foreach (var item in resultFour)
            Console.WriteLine($"{item.Name} - {item.TeamName} ({item.Country})");

        Console.WriteLine();

        foreach (var item in resultFive)
            Console.WriteLine($"{item.Name} - {item.TeamName} ({item.Country})");
    }

    private class PlayerTeam
    {
        public string Name { get; init; } = null!;
        public string TeamName { get; init; } = null!;
        public string Country { get; init; } = null!;
    }

    private class Player
    {
        public string Name { get; init; } = null!;
        public string TeamName { get; init; } = null!;
    }

    private class Team
    {
        public string Name { get; init; } = null!;
        public string Country { get; init; } = null!;
    }
}
