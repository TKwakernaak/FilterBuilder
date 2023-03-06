using System.Linq.Expressions;

internal class RebindParameterVisitor : ExpressionVisitor
{
    private readonly ParameterExpression _oldParameter;
    private readonly ParameterExpression _newParameter;

    public RebindParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
    {
        _oldParameter = oldParameter;
        _newParameter = newParameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (node == _oldParameter)
        {
            return _newParameter;
        }

        return base.VisitParameter(node);
    }
}

