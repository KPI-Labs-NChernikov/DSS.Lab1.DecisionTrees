using DSS.Lab1.DecisionTrees;
using DSS.Lab1.DecisionTrees.Nodes;

// var decisionTree = new DecisionTree(
//     new DecisionNode
//     {
//         Children =
//         [
//             new ProbabilityNode()
//             {
//                 Name = "Aquadrome",
//                 Children = 
//                 [
//                     new Leaf()
//                     {
//                         Name = "Сприятливий стан",
//                         InitialValue = 200000,
//                         Factor = 0.5m
//                     },
//                     new Leaf()
//                     {
//                         Name = "Несприятливий стан",
//                         InitialValue = -180000,
//                         Factor = 0.5m
//                     },
//                 ]
//             },
//             new ProbabilityNode()
//             {
//                 Name = "Garage",
//                 Children = 
//                 [
//                     new Leaf()
//                     {
//                         Name = "Сприятливий стан",
//                         InitialValue = 100000,
//                         Factor = 0.5m
//                     },
//                     new Leaf()
//                     {
//                         Name = "Несприятливий стан",
//                         InitialValue = -20000,
//                         Factor = 0.5m
//                     },
//                 ]
//             },
//             new Leaf()
//             {
//                 Name = "Deposit",
//                 InitialValue = 10000
//             }
//         ]
//     });
var decisionTree = new DecisionTree(
    new DecisionNode()
    {
        Children = 
        [
            new DecisionNode
            {
                Name = "Without audit",
                InitialValue = 0,
                 Children =
                 [
                     new ProbabilityNode()
                     {
                         Name = "Aquadrome",
                         Children = 
                         [
                             new Leaf()
                             {
                                 Name = "Сприятливий стан",
                                 InitialValue = 200000,
                                 Factor = 0.5m
                             },
                             new Leaf()
                             {
                                 Name = "Несприятливий стан",
                                 InitialValue = -180000,
                                 Factor = 0.5m
                             },
                         ]
                     },
                     new ProbabilityNode()
                     {
                         Name = "Garage",
                         Children = 
                         [
                             new Leaf()
                             {
                                 Name = "Сприятливий стан",
                                 InitialValue = 100000,
                                 Factor = 0.5m
                             },
                             new Leaf()
                             {
                                 Name = "Несприятливий стан",
                                 InitialValue = -20000,
                                 Factor = 0.5m
                             },
                         ]
                     },
                     new Leaf()
                     {
                         Name = "Deposit",
                         InitialValue = 10000
                     }
                ]
            },
            new ProbabilityNode()
            {
                Name = "With audit",
                InitialValue = -10000,
                Children = 
                [
                    new DecisionNode
                    {
                        Name = "Сприятливий",
                        Factor = 0.45m,
                        Children =
                        [
                            new ProbabilityNode()
                            {
                                Name = "Aquadrome",
                                Children = 
                                [
                                    new Leaf()
                                    {
                                        Name = "Сприятливий стан",
                                        InitialValue = 200000,
                                        Factor = 0.78m
                                    },
                                    new Leaf()
                                    {
                                        Name = "Несприятливий стан",
                                        InitialValue = -180000,
                                        Factor = 0.22m
                                    },
                                ]
                            },
                            new ProbabilityNode()
                            {
                                Name = "Garage",
                                Children = 
                                [
                                    new Leaf()
                                    {
                                        Name = "Сприятливий стан",
                                        InitialValue = 100000,
                                        Factor = 0.78m
                                    },
                                    new Leaf()
                                    {
                                        Name = "Несприятливий стан",
                                        InitialValue = -20000,
                                        Factor = 0.22m
                                    },
                                ]
                            },
                            new Leaf()
                            {
                                Name = "Deposit",
                                InitialValue = 10000
                            }
                        ]
                    },
                    new DecisionNode
                    {
                        Name = "Несприятливий",
                        Factor = 0.55m,
                        Children =
                        [
                            new ProbabilityNode()
                            {
                                Name = "Aquadrome",
                                Children = 
                                [
                                    new Leaf()
                                    {
                                        Name = "Сприятливий стан",
                                        InitialValue = 200000,
                                        Factor = 0.27m
                                    },
                                    new Leaf()
                                    {
                                        Name = "Несприятливий стан",
                                        InitialValue = -180000,
                                        Factor = 0.73m
                                    },
                                ]
                            },
                            new ProbabilityNode()
                            {
                                Name = "Garage",
                                Children = 
                                [
                                    new Leaf()
                                    {
                                        Name = "Сприятливий стан",
                                        InitialValue = 100000,
                                        Factor = 0.27m
                                    },
                                    new Leaf()
                                    {
                                        Name = "Несприятливий стан",
                                        InitialValue = -20000,
                                        Factor = 0.73m
                                    },
                                ]
                            },
                            new Leaf()
                            {
                                Name = "Deposit",
                                InitialValue = 10000
                            }
                        ]
                    }
                ]
            }
        ]
    });
decisionTree.Validate();
decisionTree.Execute();
var paths = decisionTree.GetBestPaths();

var bestPathsWithGoodPrediction = decisionTree.Root.Children[1].Children[0].GetBestPaths([]);
var bestProjectsWithGoodPrediction = bestPathsWithGoodPrediction.Select(p => p.ElementAt(1)).Distinct();

Console.WriteLine(string.Join(Environment.NewLine, bestProjectsWithGoodPrediction.Select(p => p.Name)));
