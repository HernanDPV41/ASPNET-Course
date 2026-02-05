using Domain.Common;
using Domain.Students.Errors;

namespace Domain.Students.BusinessRules
{
    public class CannotUpdateNotMatriculatedEvaluation(
        IEnumerable<Evaluation> currentEvaluations,
        IEnumerable<EvaluationRecord> requestedEvaluations)
        : IBusinessRule
    {
        public Result Check()
        {
            if (requestedEvaluations
                .Select(x => x.Id)
                .Except(currentEvaluations
                .Select(x => x.Id))
                .Any())
                return Result.WithError(
                    StudentErrors.UpdateNotMatriculatedEvaluation);
            return Result.Success();
        }
    }
}
