namespace DSS.Lab1.DecisionTrees.Demo;

public static class ConsoleService
{
    public static void PrintHeader(string name)
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(name);
        Console.ResetColor();
    }
}
