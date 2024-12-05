using ConsoleApp1;

internal class Program
{
    private static void Main(string[] args)
    {
        Graph graph = new();

        graph.Add(0);
        graph.Add(3, 0);
        graph.Add(6, 3);
        graph.Add(1, 0);
        graph.Add(4, 1);
        graph.Add(7, 4);
        graph.Add(2, 1);
        graph.Add(5, 2);
        graph.Add(8, 5);

        graph.Join(3, 4);
        graph.Join(4, 5);
        graph.Join(6, 7);
        graph.Join(7, 8);

        Console.WriteLine(graph);

        var paths = graph.FindAllPaths(0, 4);

        Console.WriteLine(paths.Count);

        foreach (List<int> path in paths)
        {
            foreach (int step in path)
            {
                Console.Write(step);
            }
            Console.Write('\n');
        }
    }
}