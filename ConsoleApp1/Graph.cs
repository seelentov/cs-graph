using System;
using System.Reflection;

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

    public void Add(int key, int? sublingKey = null, int weight = 1, Direction direction = Direction.FromTo)
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

    public void Join(int from, int to, int weight = 1, Direction direction = Direction.FromTo)
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
        if (!_nodes.ContainsKey(start))
        {
            return;
        }

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
        if (!_nodes.ContainsKey(start))
        {
            return;
        }

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

    public List<List<int>>? FindAllPaths(int start, int end)
    {
        if (!_nodes.ContainsKey(start))
        {
            return null;
        }

        List<int> path = [];
        HashSet<int> visited = [];
        List<List<int>> paths = [];

        FindAllPathsRecursive(start, end, path, visited, paths);

        return paths;
    }

    public void FindAllPathsRecursive(int current, int end, List<int> path, HashSet<int> visited, List<List<int>> paths)
    {
        path.Add(current);
        visited.Add(current);

        if (current == end)
        {
            paths.Add([.. path]);
        }

        foreach (var edge in _nodes[current].Edges)
        {
            if (!visited.Contains(edge.Adj))
            {
                FindAllPathsRecursive(edge.Adj, end, path, visited, paths);
            }
        }

        path.RemoveAt(path.Count - 1);
        visited.Remove(current);
    }

    public List<int>? FindPathDFS(int start, int end)
    {
        if (!_nodes.ContainsKey(start))
        {
            return null;
        }

        List<int> path = [];
        HashSet<int> visited = [];

        if (FindPathDFSRecursive(start, end, path, visited))
        {
            return path;
        }
        else
        {
            return [];
        }

    }

    public bool FindPathDFSRecursive(int current, int end, List<int> path, HashSet<int> visited)
    {
        path.Add(current);
        visited.Add(current);

        if (current == end)
        {
            return true;
        }

        foreach (var edge in _nodes[current].Edges)
        {
            if (!visited.Contains(edge.Adj))
            {
                if (FindPathDFSRecursive(edge.Adj, end, path, visited))
                {
                    return true;
                }
            }
        }

        path.RemoveAt(path.Count - 1);
        return false;
    }

    public List<int>? FindPathBFS(int start, int end)
    {
        if (!_nodes.ContainsKey(start))
        {
            return null;
        }

        Queue<int> queue = [];

        Dictionary<int, int> map = [];

        queue.Enqueue(start);
        map.Add(start, 0);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            if (current == end)
            {
                List<int> path = [];

                while (current != start)
                {
                    path.Insert(0, current);
                    current = map[current];
                }

                path.Insert(0, start);

                return path;
            }

            foreach (Edge edge in _nodes[current].Edges)
            {
                if (!map.ContainsKey(edge.Adj))
                {
                    queue.Enqueue(edge.Adj);
                    map.Add(edge.Adj, current);
                }
            }
        }

        return null;
    }

    public List<int>? FindPathBFSTwoSize(int start, int end)
    {
        if (!_nodes.ContainsKey(start))
        {
            return null;
        }

        Queue<int> queue = [];
        Queue<int> queue2 = [];

        Dictionary<int, int> map = [];
        Dictionary<int, int> map2 = [];

        queue.Enqueue(start);
        map.Add(start, 0);

        queue2.Enqueue(end);
        map2.Add(end, 0);

        int? contact = null;

        bool isBreak = false;

        while (queue.Count > 0 || queue2.Count > 0)
        {
            if (isBreak)
            {
                break;
            }

            int? current = queue.Count > 0 ? queue.Dequeue() : null;

            if (current != null)
            {
                int currentNotNull = (int)current;

                foreach (Edge edge in _nodes[currentNotNull].Edges)
                {
                    if (!map.ContainsKey(edge.Adj))
                    {
                        map.Add(edge.Adj, currentNotNull);

                        if (map2.ContainsKey(edge.Adj))
                        {
                            contact = edge.Adj;
                            isBreak = true;
                            break;
                        }

                        queue.Enqueue(edge.Adj);
                    }
                }
            }

            if (isBreak)
            {
                break;
            }

            int? current2 = queue2.Count > 0 ? queue2.Dequeue() : null;

            if (current2 != null)
            {
                int currentNotNull = (int)current2;

                foreach (Edge edge in _nodes[currentNotNull].Edges)
                {
                    if (!map2.ContainsKey(edge.Adj))
                    {
                        map2.Add(edge.Adj, currentNotNull);

                        if (map.ContainsKey(edge.Adj))
                        {
                            contact = edge.Adj;
                            isBreak = true;
                            break;
                        }

                        queue2.Enqueue(edge.Adj);
                    }
                }
            }
        }

        if (contact != null)
        {
            int current = (int)contact;

            while (current != start)
            {
                List<int> path = [];

                while (current != start)
                {
                    path.Insert(0, current);
                    current = map[current];
                }

                current = map2[(int)contact];

                while (current != end)
                {
                    path.Add(current);
                    current = map2[current];
                }

                path.Insert(0, start);
                path.Add(end);

                return path;
            }
        }

        return null;
    }

    public List<int>? FindPathDijkstra(int start, int end)
    {
        if (!_nodes.ContainsKey(start) || !_nodes.ContainsKey(end))
        {
            return null;
        }

        HashSet<int> unprocessed = [];
        Dictionary<int, int> weightToNode = [];

        foreach (int node in _nodes.Keys)
        {
            unprocessed.Add(node);
            weightToNode[node] = int.MaxValue; ;
        }

        weightToNode[start] = 0;

        while (unprocessed.Count > 0)
        {
            int? current = null;
            int minTime = int.MaxValue;

            foreach (int key in unprocessed)
            {
                int weight = weightToNode[key];

                if (weight < minTime)
                {
                    minTime = weight;
                    current = key;
                }
            }

            if (current == null)
            {
                break;
            }

            foreach (var edge in _nodes[(int)current].Edges)
            {
                if (unprocessed.Contains(edge.Adj))
                {
                    int weightToCheck = weightToNode[(int)current] + edge.Weight;
                    if (weightToCheck < weightToNode[edge.Adj])
                        weightToNode[edge.Adj] = weightToCheck;
                }
            }

            unprocessed.Remove((int)current);
        }

        if (weightToNode[end] != int.MaxValue)
        {
            List<int> path = [];

            int current = end;

            while (current != start)
            {
                int minWeight = weightToNode[current];
                path.Insert(0, current);

                foreach (var edge in _nodes[current].Edges)
                {
                    if (!weightToNode.ContainsKey(edge.Adj)) continue;
                    bool prevNodeFound = (edge.Weight + weightToNode[edge.Adj]) == minWeight;

                    if (prevNodeFound)
                    {
                        weightToNode.Remove(current);
                        current = edge.Adj;
                        break;
                    }
                }
            }

            path.Insert(0, start);

            return path;

        }

        return null;
    }

    public List<int>? FindPathBellmanFord(int start, int end)
    {
        if (!_nodes.ContainsKey(start) || !_nodes.ContainsKey(end))
        {
            return null;
        }

        Dictionary<int, int> distances = new Dictionary<int, int>(_nodes.Count);
        Dictionary<int, int?> previousNodes = new Dictionary<int, int?>(_nodes.Count); // Для реконструкции пути

        foreach (int node in _nodes.Keys)
        {
            distances[node] = int.MaxValue;
            previousNodes[node] = null;
        }
        distances[start] = 0;

        for (int i = 1; i < _nodes.Count; i++)
        {
            foreach (var node in _nodes)
            {
                foreach (var edge in _nodes[node.Key].Edges)
                {
                    if (distances[node.Key] + edge.Weight < distances[edge.Adj])
                    {
                        distances[edge.Adj] = distances[node.Key] + edge.Weight;
                        previousNodes[edge.Adj] = node.Key;
                    }
                }
            }
        }

        foreach (var node in _nodes)
        {
            foreach (var edge in _nodes[node.Key].Edges)
            {
                if (distances[node.Key] + edge.Weight < distances[edge.Adj])
                {
                    return null;
                }
            }
        }

        if (distances[end] == int.MaxValue) return null;

        List<int> path = [];
        int currentNode = end;
        while (currentNode != start)
        {
            path.Insert(0, currentNode);
            currentNode = previousNodes[currentNode] ?? throw new Exception("Несогласованный путь найден!");
        }
        path.Insert(0, start);
        return path;
    }
    public List<EdgeUndir> ToEdgeList()
    {
        List<EdgeUndir> edges = new();

        foreach (var node in _nodes)
        {
            foreach (var edge in _nodes[node.Key].Edges)
            {
                edges.Add(new EdgeUndir(edge, node.Key));
            }
        }

        return edges;
    }
}
