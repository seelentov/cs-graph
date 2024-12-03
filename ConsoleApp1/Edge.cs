using System;

namespace ConsoleApp1;

public class Edge
{
    public int Weight = 1;
    public int Adj { get; private set; }

    public override string ToString()
    {
        return $"{Adj}(w: {Weight})";
    }
    public Edge(int adj, int weight = 1)
    {
        Weight = weight;
        Adj = adj;
    }
}
