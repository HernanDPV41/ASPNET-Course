namespace Domain.Common
{
    public class Error
    {
        public string Code { get; }
        public ErrorType ErrorType { get; }
        public string Message { get; init; }

        public Error(
            string code,
            ErrorType errorType,
            string message)
        {
            Code = code;
            ErrorType = errorType;
            Message = message;
        }

    }
}
