using DSS.Lab1.DecisionTrees.Nodes;

namespace DSS.Lab1.DecisionTrees.Demo;

public sealed class LabTask3 : ILabTask
{
    private readonly decimal[,] _data;
    private readonly decimal _k;
    private readonly decimal _n;
    private readonly decimal _l;

    private const int ProductTypesCount = 5;
    private const int SuppliersCount = 2;
    
    public LabTask3(decimal[,] data, decimal k, decimal n, decimal l)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentOutOfRangeException.ThrowIfNotEqual(data.GetLength(0), ProductTypesCount);
        ArgumentOutOfRangeException.ThrowIfNotEqual(data.GetLength(1), SuppliersCount);
        _data = data;
        
        _k = k;
        _n = n;
        _l = l;
    }

    public void Execute()
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Завдання 3");
        Console.ResetColor();

        var supplierNodes = new List<INode>[SuppliersCount];
        for (var i = 0; i < supplierNodes.Length; i++)
        {
            supplierNodes[i] = [];
            for (var j = 0; j < _data.GetLength(0); j++)
            {
                supplierNodes[i].Add(new ProbabilityNode()
                {
                    Name = $"Виріб {j + 1}",
                    Children = [
                        new Leaf()
                        {
                            Name = "Вибір бракований",
                            Factor = _data[j, i],
                            InitialValue = _k * _n
                        },
                        new Leaf()
                        {
                            Name = "Вибір небракований",
                            Factor = decimal.One - _data[j, i],
                            InitialValue = 0
                        }
                    ]
                });
            }
        }

        var decisionTree = new DecisionTree(
            new DecisionNode()
            {
                DecisionNodeType = DecisionNodeType.Minimize,
                Children = [
                    new SumNode()
                    {
                        Name = "Постачальник A",
                        Children = supplierNodes[0]
                    },
                    new SumNode()
                    {
                        Name = "Постачальник B",
                        InitialValue = -_l,
                        Children = supplierNodes[1]
                    }
                ]
            });
        
        decisionTree.Validate();
        decisionTree.Execute();
        
        var bestSuppliers = decisionTree.GetBestChildren();
        if (bestSuppliers.Count == 1)
        {
            Console.WriteLine($"Краще обрати: [{bestSuppliers[0].Name}], результат: {decisionTree.ResultValue:0.00} г.о.");
        }
        else
        {
            Console.WriteLine($"Обрання будь-якого з виробників є однаково вигідним, результат: {decisionTree.ResultValue:0.00} г.о.");
        }
        Console.WriteLine();

        foreach (var supplier in decisionTree.Root.Children)
        {
            Console.WriteLine($"{supplier.Name}, результат: {supplier.ResultValue:0.00} г.о.");
            if (supplier.InitialValue != decimal.One)
            {
                Console.WriteLine($"Знижка: {-supplier.InitialValue:0.00} г.о.");
            }
            foreach (var product in supplier.Children)
            {
                Console.WriteLine($"{product.Name}, очікувана вартість ремонту: {product.ResultValue:0.00} г.о.");
            }
        }
    }
}
