using System;

namespace ConsoleApp1;

public class EdgeUndir : Edge
{
    public int From { get; private set; }
    public EdgeUndir(Edge edge, int from) : base(edge.Adj, edge.Weight)
    {
        this.From = from;
    }
    public EdgeUndir(int from, int to, int weight = 1) : base(to, weight)
    {
        this.From = from;
    }
}
