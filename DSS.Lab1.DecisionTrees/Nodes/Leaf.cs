namespace DSS.Lab1.DecisionTrees.Nodes;

public class Leaf : INode
{
    public string? Name { get; init; }
    
    public DecisionTreeState State { get; protected set; }
    
    public decimal ResultValue { get; private set; }
    public IReadOnlyList<INode> Children => Array.Empty<INode>();
    public required decimal InitialValue { get; init; }
    public decimal Factor { get; init; } = 1;

    public void Validate()
    {
        if (Factor < 0 || Factor > 1)
        {
            throw new InvalidOperationException("Factor must be between 0 and 1.");
        }

        State = DecisionTreeStateHelper.ToValidated(State);
    }

    public void Execute()
    {
        ResultValue = InitialValue;
        
        State = DecisionTreeStateHelper.ToExecuted(State);
    }
    
    public List<INode> GetBestChildren()
    {
        return [];
    }
}
