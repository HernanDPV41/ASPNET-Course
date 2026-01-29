namespace Domain.Common
{
    public class Result
    {
        private List<Error> _errors = [];

        public IReadOnlyCollection<Error> Errors => _errors;


        public bool IsFailure => _errors.Count > 0;

        public bool IsSuccess => _errors.Count == 0;

        internal Result() { }

        public static Result Success()
        {
            var result = new Result();
            return result;
        }

        public static Result<T> Success<T>(T value)
        {
            var result = new Result<T>();
            result.Value = value;
            return result;
        }

        public static Result WithErrors(IEnumerable<Error> errors)
        {
            var result = new Result();
            result._errors.AddRange(errors);
            return result;
        }

        public static Result WithError(Error error)
        {
            var result = new Result();
            result._errors.Add(error);
            return result;
        }

        public static Result Merge(params Result[] results)
        {
            var mergedResukt = Success();
            foreach (var result in results)
            {
                mergedResukt._errors.AddRange(result.Errors);
            }
            return mergedResukt;
        }

    }

    public class Result<T>
    {

        private List<Error> _errors = [];

        public IReadOnlyCollection<Error> Errors => _errors;


        public bool IsFailure => _errors.Count > 0;

        public bool IsSuccess => _errors.Count == 0;

        public T? Value { get; internal set; } = default;

        public static Result<T> WithErrors(IEnumerable<Error> errors)
        {
            var result = new Result<T>();
            result._errors.AddRange(errors);
            return result;
        }

        public static Result<T> WithError(Error error)
        {
            var result = new Result<T>();
            result._errors.Add(error);
            return result;
        }

        public static implicit operator Result<T>(Result result)
        {
            return Result.WithErrors(result.Errors);
        }

        public Result ToResult()
        {
            return Result.WithErrors(_errors);
        }
    }
}
