using System;

namespace ConsoleApp1;

public class Graph
{
    public enum Direction
    {
        From,
        To,
        FromTo
    }
    private Dictionary<int, Node> _nodes = new();

    public override string ToString()
    {
        string res = "";

        foreach (var node in _nodes)
        {
            res += $"{node.Key}: {node.Value}\n";
        }

        return res;
    }

    public Node Get(int key)
    {
        Node node = _nodes[key];
        return node;
    }

    public void Add(int key, int? sublingKey = null, int weight = 1, Direction direction = Direction.To)
    {
        if (sublingKey != null && !_nodes.ContainsKey((int)sublingKey))
        {
            return;
        }

        Node newNode = new Node();

        _nodes[key] = newNode;

        if (sublingKey != null)
        {
            Join((int)sublingKey, key, weight, direction);
        }
    }

    public void Remove(int key)
    {
        Node nodeToRemove = Get(key);

        foreach (var node in _nodes)
        {
            Edge? edgeToRemove = node.Value.Edges.Where(edge => edge.Adj == key).FirstOrDefault();
            if (edgeToRemove != null)
                node.Value.Edges.Remove(edgeToRemove);
        }

        _nodes.Remove(key);
    }

    public void Join(int from, int to, int weight = 1, Direction direction = Direction.To)
    {
        Node fromNode = Get(from);
        Node toNode = Get(to);

        if (direction == Direction.To)
        {
            fromNode.Edges.Add(new Edge(to, weight));
        }
        else if (direction == Direction.From)
        {
            toNode.Edges.Add(new Edge(from, weight));
        }
        else
        {
            toNode.Edges.Add(new Edge(from, weight));
            fromNode.Edges.Add(new Edge(to, weight));
        }
    }

    public void BFS(int start, Action<Node, int> action)
    {
        Queue<int> queue = [];
        HashSet<int> visited = [];
        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            int key = queue.Dequeue();
            Node node = _nodes[key];
            action(node, key);
            foreach (var edge in node.Edges)
            {
                if (!visited.Contains(edge.Adj))
                {
                    visited.Add(edge.Adj);
                    queue.Enqueue(edge.Adj);
                }
            }
        }
    }

    public void DFS(int start, Action<Node, int> action)
    {
        Stack<int> stack = [];
        HashSet<int> visited = [];
        stack.Push(start);
        visited.Add(start);

        while (stack.Count > 0)
        {
            int key = stack.Pop();
            Node node = _nodes[key];
            action(node, key);
            foreach (var edge in node.Edges)
            {
                if (!visited.Contains(edge.Adj))
                {
                    visited.Add(edge.Adj);
                    stack.Push(edge.Adj);
                }
            }
        }
    }
}
