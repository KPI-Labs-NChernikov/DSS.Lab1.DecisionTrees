namespace DSS.Lab1.DecisionTrees.Nodes;

public abstract class Node : INode
{
    public string? Name { get; init; }
    
    public decimal ResultValue { get; protected set; }

    public DecisionTreeState State { get; protected set; }
    
    public IReadOnlyList<INode> Children
    {
        get => _children;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _children = value;

            State = DecisionTreeState.Created;
        }
    }
    private IReadOnlyList<INode> _children = [];
    
    public decimal InitialValue { get; init; }
    public decimal Factor { get; init; } = 1m;

    public virtual void Validate()
    {
        if (Factor < 0 || Factor > 1)
        {
            throw new InvalidOperationException("Factor must be between 0 and 1.");
        }
        
        if (!Children.Any())
        {
            throw new InvalidOperationException("Only Leaf node can not have children.");
        }
        
        foreach (var node in Children)
        {
            node.Validate();
        }
        
        State = DecisionTreeStateHelper.ToValidated(State);
    }

    public abstract void Execute();
    public abstract IEnumerable<IEnumerable<INode>> GetBestPaths(List<INode> currentPath);
}
