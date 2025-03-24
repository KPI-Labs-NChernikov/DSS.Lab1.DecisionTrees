namespace DSS.Lab1.DecisionTrees.Nodes;

public sealed class DecisionNode : Node
{
    private readonly List<INode> _bestChildren = [];
    
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

    public override List<INode> GetBestChildren()
    {
        return _bestChildren;
    }
}
