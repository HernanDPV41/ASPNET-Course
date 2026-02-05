using Domain.Common;

namespace Domain.Courses
{
    public class Topic
        : Entity
    {

        public string Name { get; private set; } = string.Empty;

        public int TotalHours { get; private set; }

        public bool HasFinalTest { get; private set; }

        private Topic() { }

        private Topic(
            string name,
            int totalHours,
            bool hasFinalTest)
            : base(Guid.NewGuid())
        {
            Name = name;
            TotalHours = totalHours;
            HasFinalTest = hasFinalTest;
        }

        public static Result<Topic> Create(
            string name,
            int totalHours,
            bool hasFinalTest)
        {
            return Result.Success(
                new Topic(
                name,
                totalHours,
                hasFinalTest));
        }

        public Result Update(
            string name,
            int totalHours,
            bool hasFinalTest)
        {
            Name = name;
            TotalHours = totalHours;
            HasFinalTest = hasFinalTest;

            return Result.Success();
        }
    }

    public sealed record TopicRecord(
            Guid Id,
            string Name,
            int TotalHours,
            bool HasFinalTest);


}
