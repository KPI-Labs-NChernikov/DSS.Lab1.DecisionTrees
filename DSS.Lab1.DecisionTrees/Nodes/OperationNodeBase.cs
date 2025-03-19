namespace DSS.Lab1.DecisionTrees.Nodes;

public abstract class OperationNodeBase : Node
{
    protected abstract Func<decimal, INode, decimal> Operation { get; }
    
    public override void Execute()
    {
        var aggregation = 0m;
        foreach (var node in Children)
        {
            node.Execute();
            aggregation = Operation(aggregation, node);
        }
        ResultValue = aggregation + InitialValue;
        
        State = DecisionTreeStateHelper.ToExecuted(State);
    }
    
    public override IEnumerable<IEnumerable<INode>> GetBestPaths(List<INode> currentPath)
    {
        currentPath.Add(this);
        var paths = new List<IEnumerable<INode>>();
        foreach (var child in Children)
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
