namespace DSS.Lab1.DecisionTrees;

public static class DecisionTreeStateHelper
{
    public static DecisionTreeState ToValidated(DecisionTreeState state)
    {
        return state < DecisionTreeState.Validated ? DecisionTreeState.Validated : state;
    }
    
    public static DecisionTreeState ToExecuted(DecisionTreeState state)
    {
        return state < DecisionTreeState.Executed ? DecisionTreeState.Executed : state;
    }
}
