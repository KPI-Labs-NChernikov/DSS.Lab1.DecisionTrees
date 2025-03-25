namespace DSS.Lab1.DecisionTrees.Nodes;

public sealed class DecisionNode : Node
{
    private readonly List<INode> _bestChildren = [];
    public DecisionNodeType DecisionNodeType { get; init; }
    
    public override void Execute()
    {
        var bestResultValue = DecisionNodeType == DecisionNodeType.Maximize ? decimal.MinValue  : decimal.MaxValue;
        foreach (var node in Children)
        {
            node.Execute();
            if ((DecisionNodeType == DecisionNodeType.Maximize && node.ResultValue > bestResultValue)
                || (DecisionNodeType == DecisionNodeType.Minimize && node.ResultValue < bestResultValue))
            {
                bestResultValue = node.ResultValue;
            }
        }

        foreach (var node in Children)
        {
            if (node.ResultValue == bestResultValue)
            {
                _bestChildren.Add(node);
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

    public override IReadOnlyList<INode> GetBestChildren()
    {
        return [.._bestChildren];
    }
}
