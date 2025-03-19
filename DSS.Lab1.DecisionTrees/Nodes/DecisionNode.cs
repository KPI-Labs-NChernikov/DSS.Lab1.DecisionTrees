namespace DSS.Lab1.DecisionTrees.Nodes;

public sealed class DecisionNode : Node
{
    public List<INode> BestChildren { get; } = [];
    
    public override void Execute()
    {
        var bestResultValue = decimal.MinValue;
        foreach (var node in Children)
        {
            node.Execute();
            if (node.ResultValue > bestResultValue)
            {
                bestResultValue = node.ResultValue;
            }
        }

        foreach (var node in Children)
        {
            if (node.ResultValue == bestResultValue)
            {
                BestChildren.Add(node);
            }
        }
        
        ResultValue = bestResultValue + InitialValue;
    }
    
    public override void Validate()
    {
        if (Children.Any(c => c.Factor != 1m))
        {
            throw new InvalidOperationException("You shouldn't change the factor of DecisionNode's Children.");
        }
        
        base.Validate();
    }
    
    public override IEnumerable<IEnumerable<INode>> GetBestPaths(List<INode> currentPath)
    {
        currentPath.Add(this);
        var paths = new List<IEnumerable<INode>>();
        foreach (var child in BestChildren)
        {
            var childPath = new List<INode>(currentPath);
            var nextBestPaths = child.GetBestPaths(childPath);
            foreach (var path in nextBestPaths)
            {
                paths.Add(path);
            }
        }
        return paths;
    }
}
