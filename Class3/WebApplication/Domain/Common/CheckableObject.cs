namespace Domain.Common
{
    public abstract class CheckableObject
    {
        public static Result CheckRules(params IBusinessRule[] rules)
        {
            return Result
               .Merge(rules
               .Select(r =>
               r.Check())
               .ToArray());
        }
    }
}
