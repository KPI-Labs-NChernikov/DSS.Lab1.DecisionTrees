using DSS.Lab1.DecisionTrees.Nodes;

namespace DSS.Lab1.DecisionTrees.Demo;

public sealed class LabTask1 : ILabTask
{
    private readonly decimal[,] _data;
    private const decimal Probability = 0.5m;

    public LabTask1(decimal[,] data)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentOutOfRangeException.ThrowIfNotEqual(data.GetLength(0), 3);
        ArgumentOutOfRangeException.ThrowIfNotEqual(data.GetLength(1), 2);
        if (data[2, 0] != data[2, 1])
        {
            throw new ArgumentException("data[2, 0] must be equal to data[2, 1]");
        }
        
        _data = data;
    }
    
    public void Execute()
    {
        ConsoleService.PrintHeader("Завдання 1");
        
        var decisionTree = new DecisionTree(
            new DecisionNode
            {
                Children =
                [
                    new ProbabilityNode()
                    {
                        Name = "Створення великого виробництва",
                        Children = 
                        [
                            new Leaf()
                            {
                                Name = "Сприятливий стан",
                                InitialValue = _data[0, 0],
                                Factor = Probability
                            },
                            new Leaf()
                            {
                                Name = "Несприятливий стан",
                                InitialValue = _data[0, 1],
                                Factor = Probability
                            },
                        ]
                    },
                    new ProbabilityNode()
                    {
                        Name = "Створення малого підприємства",
                        Children = 
                        [
                            new Leaf()
                            {
                                Name = "Сприятливий стан",
                                InitialValue = _data[1, 0],
                                Factor = Probability
                            },
                            new Leaf()
                            {
                                Name = "Несприятливий стан",
                                InitialValue = _data[1, 1],
                                Factor = Probability
                            },
                        ]
                    },
                    new Leaf()
                    {
                        Name = "Продаж патенту",
                        InitialValue = _data[2, 0]
                    }
                ]
            });
        decisionTree.Validate();
        decisionTree.Execute();

        var bestDecisions = decisionTree.GetBestChildren();
        if (bestDecisions.Count == 1)
        {
            Console.WriteLine($"Найкращим рішенням є [{bestDecisions[0].Name}], результат: {decisionTree.ResultValue:0.00} г.о.");
        }
        else
        {
            Console.WriteLine("Найкращі рішення:");
            foreach (var node in bestDecisions)
            {
                Console.WriteLine($"[{node.Name}], результат: {decisionTree.ResultValue:0.00} г.о.");
            }
            Console.WriteLine();
        }

        Console.WriteLine($"{"Вершина",-35}{"Виграш, г.о.",15}");
        foreach (var node in decisionTree.Root.Children)
        {
            Console.WriteLine($"{node.Name,-35}{node.ResultValue,15:0.00}");
        }
    }
}
