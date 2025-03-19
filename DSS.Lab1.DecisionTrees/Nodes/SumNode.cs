namespace DSS.Lab1.DecisionTrees.Nodes;

public sealed class SumNode : OperationNodeBase
{
    protected override Func<decimal, INode, decimal> Operation  => (sum, node) => sum += node.ResultValue;

    public override void Validate()
    {
        if (Children.Any(c => c.Factor != 1m))
        {
            throw new InvalidOperationException("You shouldn't change the factor of SumNode's Children.");
        }
        
        base.Validate();
    }
}
