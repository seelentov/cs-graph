using ConsoleApp1;

internal class Program
{
    private static void Main(string[] args)
    {
        Graph graph = new();
        graph.Add(0);
        graph.Add(1, 0, 10);
        graph.Add(2, 0, 6);
        graph.Add(3, 0, 5);
        graph.Join(1, 3, 15);
        graph.Join(2, 3, 4);

        int START = 1;
        int END = 3;

        Console.WriteLine(graph.ToString());

        Console.WriteLine("All");

        var path = graph.FindAllPaths(START, END);

        if (path != null)
            foreach (var item in path)
            {
                Console.WriteLine(string.Join("", item));
            }

        Console.WriteLine("");
        Console.WriteLine("Dijkstra");

        var path2 = graph.FindPathDijkstra(START, END);

        if (path2 != null)
            Console.WriteLine(string.Join("", path2));

        Console.WriteLine("");
        Console.WriteLine("Bellman");

        var path3 = graph.FindPathBellmanFord(START, END);

        if (path3 != null)
            Console.WriteLine(string.Join("", path3));
    }
}