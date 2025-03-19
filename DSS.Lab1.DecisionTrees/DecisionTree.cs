namespace DSS.Lab1.DecisionTrees;

public class DecisionTree
{
    public INode Root { get; }

    public DecisionTreeState State { get; private set; } = DecisionTreeState.Created;

    public DecisionTree(INode root)
    {
        ArgumentNullException.ThrowIfNull(root);
        Root = root;
    }

    public void Validate()
    {
        if (Root.Factor != 1m)
        {
            throw new InvalidOperationException("Root node's factor must be 1");
        }
        Root.Validate();
        State = DecisionTreeStateHelper.ToValidated(State);
    }

    public void Execute()
    {
        Root.Execute();
        State = DecisionTreeStateHelper.ToExecuted(State);
    }

    public IReadOnlyList<IReadOnlyList<INode>> GetBestPaths()
    {
        if (State < DecisionTreeState.Executed)
        {
            throw new InvalidOperationException("Decision tree must be executed first.");
        }
        
        return Root.GetBestPaths([])
            .Select(p => p.ToList())
            .ToList();
    }
}
