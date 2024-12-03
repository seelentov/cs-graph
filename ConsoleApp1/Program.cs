using ConsoleApp1;

internal class Program
{
    private static void Main(string[] args)
    {
        Graph graph = new();
        graph.Add(0);
        graph.Add(3, 0, 1, Graph.Direction.FromTo);
        graph.Add(6, 3, 1, Graph.Direction.FromTo);
        graph.Add(1, 0, 1, Graph.Direction.FromTo);
        graph.Add(4, 1, 1, Graph.Direction.FromTo);
        graph.Add(7, 4, 1, Graph.Direction.FromTo);
        graph.Add(2, 1, 1, Graph.Direction.FromTo);
        graph.Add(5, 2, 1, Graph.Direction.FromTo);
        graph.Add(8, 5, 1, Graph.Direction.FromTo);

        graph.Join(3, 4, 1, Graph.Direction.FromTo);
        graph.Join(4, 5, 1, Graph.Direction.FromTo);
        graph.Join(6, 7, 1, Graph.Direction.FromTo);
        graph.Join(7, 8, 1, Graph.Direction.FromTo);

        graph.DFS(8, (n, k) => Console.WriteLine(k + ": " + n));
        Console.WriteLine();
        graph.BFS(8, (n, k) => Console.WriteLine(k + ": " + n));
    }
}