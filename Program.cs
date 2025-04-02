using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        Generic<Robots> game = new Generic<Robots>();
        int turnCount = 0;
        string playerName;

        Console.WriteLine("Bienvenido al juego de Slots de Robots!");
        Console.Write("Introduce tu nombre: ");
        playerName = Console.ReadLine() ?? "Player";

        while (turnCount < 10)
        {
            Console.WriteLine($"\nTirada {turnCount + 1}/10 - Presiona ENTER para tirar");
            Console.ReadLine();

            // Obtener números aleatorios
            int[] numbers = GetRandomNumbers();
            var robots = numbers.Select(n => n switch
            {
                1 => "R2D2",
                2 => "C3PO",
                _ => "BBB"
            }).ToArray();

            // Mostrar tirada
            Console.WriteLine($"[ {robots[0]} ] [ {robots[1]} ] [ {robots[2]} ]");

            // Crear robots según la tirada
            foreach (var robotType in robots)
            {
                DateTime createDate = DateTime.Now;
                int id = game.Robots.Count + 1;

                switch (robotType)
                {
                    case "R2D2":
                        game.AddRobots(new R2D2(id, createDate, 1));
                        break;
                    case "C3PO":
                        game.AddRobots(new C3PO(id, createDate));
                        break;
                    case "BBB":
                        game.AddRobots(new BBB(id, createDate, 1.0f));
                        break;
                }
            }

            // Calcular bonus por combinaciones
            var groupedRobots = robots.GroupBy(r => r).OrderByDescending(g => g.Count());
            int maxCount = groupedRobots.First().Count();

            if (maxCount == 3)
            {
                game.AddBonusPoints(10);
                Console.WriteLine("¡3 iguales! +10 puntos");
            }
            else if (maxCount == 2)
            {
                game.AddBonusPoints(5);
                Console.WriteLine("¡2 iguales! +5 puntos");
            }

            Thread.Sleep(1000);
            turnCount++;
        }

        // Mostrar resultados finales
        Console.WriteLine("\n=== Resultado Final ===");
        Console.WriteLine($"Puntos totales: {game.GetBonusPoints()}");
        Console.WriteLine("\nRobots creados:");
        game.ShowAllRobots();

        // Guardar puntuación
        SaveScore(playerName, game.GetBonusPoints());
        ShowTop3Scores();

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }

    static int[] GetRandomNumbers()
    {
        try
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync("https://www.randomnumberapi.com/api/v1.0/random?min=1&max=3&count=3").Result;
                return JsonSerializer.Deserialize<int[]>(response) ?? new int[] { 1, 1, 1 };
            }
        }
        catch
        {
            return new int[] {
                new Random().Next(1, 4),
                new Random().Next(1, 4),
                new Random().Next(1, 4)
            };
        }
    }

    static void SaveScore(string playerName, int score)
    {
        var scoreEntry = $"{playerName},{score},{DateTime.Now}\n";
        File.AppendAllText("scores.txt", scoreEntry);
    }

    static void ShowTop3Scores()
    {
        Console.WriteLine("\n=== Top 3 Puntuaciones ===");
        try
        {
            if (File.Exists("scores.txt"))
            {
                var scores = File.ReadAllLines("scores.txt")
                    .Select(line =>
                    {
                        var parts = line.Split(',');
                        return new { Name = parts[0], Score = int.Parse(parts[1]) };
                    })
                    .OrderByDescending(s => s.Score)
                    .Take(3);

                foreach (var score in scores)
                {
                    Console.WriteLine($"{score.Name}: {score.Score} puntos");
                }
            }
            else
            {
                Console.WriteLine("No hay puntuaciones guardadas todavía.");
            }
        }
        catch
        {
            Console.WriteLine("Error al leer las puntuaciones.");
        }
    }
}
