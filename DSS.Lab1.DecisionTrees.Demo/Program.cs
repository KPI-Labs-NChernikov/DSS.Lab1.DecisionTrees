using System.Globalization;
using System.Text;
using DSS.Lab1.DecisionTrees.Demo;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var labTask1Data = new decimal[3,2]
{
    { 550_000, -250_000 },
    { 300_000, -75_000 },
    { 55000, 55000 },
};
var labTasks = new ILabTask[]
{
    new LabTask1(labTask1Data),
    new LabTask2(
        labTask1Data,
        new decimal[,]
        {
            { 0.75m, 0.3m},
            { 0.25m, 0.7m}
        },
        10_000)
};

for (var i = 0; i < labTasks.Length; i++)
{
    labTasks[i].Execute();
    if (i != labTasks.Length - 1)
    {
        Console.WriteLine();
    }
}
