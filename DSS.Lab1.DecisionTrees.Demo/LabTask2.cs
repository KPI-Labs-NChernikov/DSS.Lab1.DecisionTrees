using DSS.Lab1.DecisionTrees.Nodes;

namespace DSS.Lab1.DecisionTrees.Demo;

public sealed class LabTask2 : ILabTask
{
    private readonly decimal[,] _data;
    private readonly decimal[,] _predictionComingTrueProbabilities;
    private readonly decimal _q;
    private const decimal FavourableSituationProbability = 0.75m;
    private const decimal UnfavourableSituationProbability = decimal.One - FavourableSituationProbability;

    public LabTask2(decimal[,] data, decimal[,] predictionComingTrueProbabilities, decimal q)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentOutOfRangeException.ThrowIfNotEqual(data.GetLength(0), 3);
        ArgumentOutOfRangeException.ThrowIfNotEqual(data.GetLength(1), 2);
        if (data[2, 0] != data[2, 1])
        {
            throw new ArgumentException("data[2, 0] must be equal to data[2, 1]");
        }
        _data = data;
        
        ArgumentNullException.ThrowIfNull(predictionComingTrueProbabilities);
        ArgumentOutOfRangeException.ThrowIfNotEqual(predictionComingTrueProbabilities.GetLength(0), 2);
        ArgumentOutOfRangeException.ThrowIfNotEqual(predictionComingTrueProbabilities.GetLength(1), 2);
        if (predictionComingTrueProbabilities[0, 0] + predictionComingTrueProbabilities[1, 0] != decimal.One
            || predictionComingTrueProbabilities[0, 1] + predictionComingTrueProbabilities[1, 1] != decimal.One)
        {
            throw new ArgumentException("predictionComingTrueProbabilities must add up to 1 vertically");
        }
        
        _predictionComingTrueProbabilities = predictionComingTrueProbabilities;
        
        _q = q;
    }

    public void Execute()
    {
        ConsoleService.PrintHeader("Завдання 2");
        
        var decisionTree = new DecisionTree(
            new DecisionNode()
            {
                Children = 
                [
                    new DecisionNode
                    {
                        Name = "Не проводити огляд",
                        InitialValue = 0,
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
                                         Factor = 0.5m
                                     },
                                     new Leaf()
                                     {
                                         Name = "Несприятливий стан",
                                         InitialValue = _data[0, 1],
                                         Factor = 0.5m
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
                                         Factor = 0.5m
                                     },
                                     new Leaf()
                                     {
                                         Name = "Несприятливий стан",
                                         InitialValue = _data[1, 1],
                                         Factor = 0.5m
                                     },
                                 ]
                             },
                             new Leaf()
                             {
                                 Name = "Продаж патенту",
                                 InitialValue =_data[2, 0]
                             }
                        ]
                    },
                    new ProbabilityNode()
                    {
                        Name = "Проводити огляд",
                        InitialValue = -_q,
                        Children = 
                        [
                            new DecisionNode
                            {
                                Name = "Сприятливий",
                                Factor = FavourableSituationProbability,
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
                                                Factor = _predictionComingTrueProbabilities[0, 0]
                                            },
                                            new Leaf()
                                            {
                                                Name = "Несприятливий стан",
                                                InitialValue = _data[0, 1],
                                                Factor = _predictionComingTrueProbabilities[1, 0]
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
                                                Factor = _predictionComingTrueProbabilities[0, 0]
                                            },
                                            new Leaf()
                                            {
                                                Name = "Несприятливий стан",
                                                InitialValue = _data[1, 1],
                                                Factor = _predictionComingTrueProbabilities[1, 0]
                                            },
                                        ]
                                    },
                                    new Leaf()
                                    {
                                        Name = "Продаж патенту",
                                        InitialValue =_data[2, 0]
                                    }
                                ]
                            },
                            new DecisionNode
                            {
                                Name = "Несприятливий",
                                Factor = UnfavourableSituationProbability,
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
                                                Factor = _predictionComingTrueProbabilities[0, 1]
                                            },
                                            new Leaf()
                                            {
                                                Name = "Несприятливий стан",
                                                InitialValue = _data[0, 1],
                                                Factor = _predictionComingTrueProbabilities[1, 1]
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
                                                Factor = _predictionComingTrueProbabilities[0, 1]
                                            },
                                            new Leaf()
                                            {
                                                Name = "Несприятливий стан",
                                                InitialValue = _data[1, 1],
                                                Factor = _predictionComingTrueProbabilities[1, 1]
                                            },
                                        ]
                                    },
                                    new Leaf()
                                    {
                                        Name = "Продаж патенту",
                                        InitialValue = _data[2, 0]
                                    }
                                ]
                            }
                        ]
                    }
                ]
            });
        
        decisionTree.Validate();
        decisionTree.Execute();
        
        var auditNodes = decisionTree.Root.Children;
        var bestAuditNodes = decisionTree.Root.GetBestChildren();
        if (bestAuditNodes.Count == 1)
        {
            Console.WriteLine($"Краще за все буде: {bestAuditNodes[0].Name!.ToLower()}, результат: {decisionTree.ResultValue:0.00} г.о.");
            var reverseNode = auditNodes.Except(bestAuditNodes).First();
            Console.WriteLine($"Інакше, результат складатиме: {reverseNode.ResultValue:0.00} г.о.");
        }
        else
        {
            Console.WriteLine($"Проведення і непроведення огляду є однаково вигідним, результат: {decisionTree.ResultValue:0.00} г.о.");
        }
        Console.WriteLine();
        
        foreach (var auditNode in bestAuditNodes)
        {
            Console.WriteLine($"Якщо {auditNode.Name!.ToLower()}:");
            
            var situations = auditNode.Children;
            foreach (var situation in situations)
            {
                Console.WriteLine($"Якщо прогноз - {situation.Name!.ToLower()}, найкращий проект: ");
                foreach (var project in situation.GetBestChildren())
                {
                    Console.WriteLine($"[{project.Name}], очікуваний максимальний прибуток: {project.ResultValue:0.00} г.о.");
                }
            }
        }
    }
}
