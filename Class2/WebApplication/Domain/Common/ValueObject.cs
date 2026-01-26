namespace Domain.Common
{
    public abstract class ValueObject
    {
        public bool Equals(ValueObject? other)
        {
            if (other is null)
                return false;

            return GetEqualityComponents()
                .SequenceEqual(
                other.GetEqualityComponents());
        }

        public abstract IEnumerable<object> GetEqualityComponents();
    }
}
