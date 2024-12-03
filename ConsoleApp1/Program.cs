using ConsoleApp1;

internal class Program
{
    private static void Main(string[] args)
    {
        Graph graph = new();

        graph.Add(0);
        graph.Add(1);
        graph.Add(2);
        graph.Add(3);
        graph.Add(4);
        graph.Add(5);
        graph.Add(6);
        graph.Add(7);
        graph.Add(8);

        graph.Join(0, 1);
        graph.Join(0, 3);
        graph.Join(1, 2);
        graph.Join(1, 4);
        graph.Join(2, 3);
        graph.Join(2, 5);
        graph.Join(3, 4);
        graph.Join(3, 6);
        graph.Join(4, 5);
        graph.Join(4, 7);
        graph.Join(5, 8);
        graph.Join(6, 7);
        graph.Join(7, 8);

        Console.WriteLine(graph);

        graph.BFS(0, (node, key) => Console.WriteLine($"{key}: ({node})\n"));
        graph.DFS(0, (node, key) => Console.WriteLine($"{key}: ({node})\n"));
    }
}