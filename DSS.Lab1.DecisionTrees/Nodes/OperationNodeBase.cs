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
}
