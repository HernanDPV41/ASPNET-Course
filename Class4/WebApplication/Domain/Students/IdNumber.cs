using Domain.Common;

namespace Domain.Students
{
    public class IdNumber
        : ValueObject
    {
        public long Value { get; private set; }

        private IdNumber() { }

        public IdNumber(long idNumber)
        {
            Value = idNumber;
        }

        public long GetAge()
        {
            long studentBirthYear = Value / 1000000000; // Obteniendo las primeras dos cifras del carnet
            studentBirthYear = studentBirthYear > 50 ?
                1900 + studentBirthYear :
                2000 + studentBirthYear; // Convirtiendo esas dos cifras al año de nacimiento
            return DateTime.Now.Year - studentBirthYear; // calculando edad
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
