using System;

namespace ConsoleApp1;

public class Node
{
    public List<Edge> Edges { get; set; } = new();
    public override string ToString()
    {
        return string.Join(", ", Edges.Select(e => e.ToString()));
    }
}
