using DSS.Lab1.DecisionTrees.Nodes;

namespace DSS.Lab1.DecisionTrees.Demo.LabTask3;

public sealed class LabTask3 : ILabTask
{
    private readonly decimal[,] _data;
    private readonly decimal _supplierBDiscount;
    private readonly int _productBatchSize;
    private readonly decimal _fullRepairPrice;

    private const int ProductTypesCount = 5;
    private const int SuppliersCount = 2;

    public LabTask3(decimal[,] data, decimal k, int n, decimal l)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentOutOfRangeException.ThrowIfNotEqual(data.GetLength(0), ProductTypesCount);
        ArgumentOutOfRangeException.ThrowIfNotEqual(data.GetLength(1), SuppliersCount);
        _data = data;

        _supplierBDiscount = l;
        _productBatchSize = n;
        _fullRepairPrice = k;
    }

    public void Execute()
    {
        ConsoleService.PrintHeader("Завдання 3");

        var products = CreateProductsFromData();

        var decisionTree = CreateDecisionTree(products);
        decisionTree.Validate();
        decisionTree.Execute();
        
        PrintResults(decisionTree);
    }
    
    private Product[] CreateProductsFromData()
    {
        var suppliers = new[]
        {
            new Supplier("Постачальник A", decimal.Zero),
            new Supplier("Постачальник B", _supplierBDiscount)
        };
        var products = new Product[_data.GetLength(0)];
        for (var i = 0; i < ProductTypesCount; i++)
        {
            var productSuppliers = new ProductSupplier[SuppliersCount];
            for (var j = 0; j < SuppliersCount; j++)
            {
                productSuppliers[j] = new ProductSupplier(_data[i, j], suppliers[j]);
            }
            products[i] = new Product($"Виріб {i + 1}", productSuppliers);
        }

        return products;
    }
    
    private DecisionTree CreateDecisionTree(Product[] products)
    {
        return new DecisionTree(
            new SumNode
            {
                Children = products
                    .Select(p => new DecisionNode
                    {
                        Name = p.Name,
                        DecisionNodeType = DecisionNodeType.Minimize,
                        Children = p.Suppliers
                            .Select(s => new ProbabilityNode
                            {
                                Name = s.Supplier.Name,
                                InitialValue = -s.Supplier.Discount,
                                Children =
                                [
                                    new Leaf()
                                    {
                                        Name = "Вибір бракований",
                                        Factor = s.FailureProbability,
                                        InitialValue = _fullRepairPrice * _productBatchSize
                                    },
                                    new Leaf()
                                    {
                                        Name = "Вибір небракований",
                                        Factor = decimal.One - s.FailureProbability,
                                        InitialValue = 0
                                    }
                                ]
                            })
                            .ToList()
                    })
                    .ToList()
            });
    }
    
    private static void PrintResults(DecisionTree decisionTree)
    {
        Console.WriteLine($"Загальна очікувана вартість ремонту з врахуванням знижок: {decisionTree.ResultValue:0.00} г.о.");
        foreach (var productNode in decisionTree.Root.Children)
        {
            var bestSupplierNodes = productNode.GetBestChildren();
            Console.Write($"{productNode.Name} краще брати у {string.Join(" або ", bestSupplierNodes.Select(s => $"[{s.Name}]"))}. ");
            Console.Write($"Очікувана вартість ремонту: {productNode.ResultValue:0.00} г.о.");
            if (bestSupplierNodes.Count == 1 && bestSupplierNodes[0].InitialValue != decimal.Zero)
            {
                Console.Write($" З неї знижка за партію: {-bestSupplierNodes[0].InitialValue:0.00} г.о.");
            }
            
            var otherSupplierNodes = productNode.Children.Except(bestSupplierNodes);
            foreach (var otherSupplierNode in otherSupplierNodes)
            {
                Console.Write($" (Вартість у [{otherSupplierNode.Name}]: {otherSupplierNode.ResultValue:0.00} г.о.)");
            }

            Console.WriteLine();
        }
    }
}
