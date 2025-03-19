namespace DSS.Lab1.DecisionTrees.Nodes;

public sealed class ProbabilityNode : OperationNodeBase
{
    protected override Func<decimal, INode, decimal> Operation => (sum, node) => sum += node.ResultValue * node.Factor;

    public override void Validate()
    {
        var probabilitiesSum = Children.Sum(c => c.Factor);
        if (probabilitiesSum != 1m)
        {
            throw new InvalidOperationException(
                "Sum of probabilities (Factor properties) of ProbabilityNode's children nodes must be equal to 1.");
        }

        base.Validate();
    }
}
