using ConsoleApp1;

internal class Program
{
    private static void Main(string[] args)
    {
        Graph graph = new();
        graph.Add(1);
        graph.Add(2, 1, 3);
        graph.Add(3, 1, 7);
        graph.Add(4, 2, 8);
        graph.Add(5, 2, 1);
        graph.Add(6, 5, 5);
        graph.Join(3, 4, 5);
        graph.Join(6, 4, 1);

        Console.WriteLine(graph.ToString());

        var path = graph.FindPathDijkstra(1, 4);

        if (path != null)
            foreach (var item in path)
            {
                Console.WriteLine(item);
            }

    }
}