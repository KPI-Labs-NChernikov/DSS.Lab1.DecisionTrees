namespace DSS.Lab1.DecisionTrees;

public interface INode
{
    string? Name { get; }
    decimal ResultValue { get; }
    IReadOnlyList<INode> Children { get; }
    decimal InitialValue { get; }
    decimal Factor { get; }
    DecisionTreeState State { get; }

    void Validate();
    void Execute();
    List<INode> GetBestChildren();
}
