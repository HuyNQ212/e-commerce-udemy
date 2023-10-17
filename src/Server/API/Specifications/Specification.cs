using System.Linq.Expressions;

namespace API.Specifications;

public class Specification<T> : ISpecification<T>
{
    public Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Specification()
    {
        
    }

    public Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

    protected void AddInclude(Expression<Func<T, object>> include) { Includes.Add(include); }
}