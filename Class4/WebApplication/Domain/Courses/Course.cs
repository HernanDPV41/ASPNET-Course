using Domain.Common;
using Domain.Courses.BusinessRules;

namespace Domain.Courses
{
    public class Course
        : Entity
    {

        public int Year { get; private set; }

        private List<Topic> _topics = [];
        public IReadOnlyCollection<Topic> Topics 
        {
            get => _topics; 
            private set
            {
                _topics = new(value);
            } 
        }

        private Course() { }

        private Course(
            int year,
            IEnumerable<Topic> topics)
            : base(Guid.NewGuid())
        {
            Year = year;
            _topics = new(topics);
        }

        public static Result<Course> Create(
            int year,
            bool isYearAlreadyUsed,
            IEnumerable<TopicRecord> topics)
        {
            var result = CheckRules(
                new CannotCreateCourseWithAlreadyUsedYearBusinessRule(isYearAlreadyUsed));

            if (result.IsFailure)
                return result;

            List<Topic> topicsList = new List<Topic>();

            foreach(var topic in topics)
            {
                var topicResult = Topic.Create(
                    topic.Name,
                    topic.TotalHours,
                    topic.HasFinalTest);

                if (topicResult.IsFailure)
                    return result;

            }


            return Result.Success(
                new Course(
                    year,
                    topicsList));
        }

        public Result Update(
            int year,
            bool isYearAlreadyUsed,
            IEnumerable<TopicRecord> topics)
        {
            var result = CheckRules(
               new CannotCreateCourseWithAlreadyUsedYearBusinessRule(isYearAlreadyUsed));

            if (result.IsFailure)
                return result;

            foreach (var topic in _topics.ToList())
            {
                if(!_topics.Any(x => x.Id == topic.Id))
                    _topics.Remove(topic);
            }

            foreach (var updatedTopic in topics)
            {
                var topic = _topics.FirstOrDefault(x => x.Id == updatedTopic.Id);

                if(topic is null)
                {
                    var topicCreationResult = Topic.Create(
                        updatedTopic.Name,
                        updatedTopic.TotalHours,
                        updatedTopic.HasFinalTest);

                    if (topicCreationResult.IsFailure)
                        return topicCreationResult.ToResult();

                    _topics.Add(topicCreationResult.Value!);
                    continue;
                }

                var topicResult = topic.Update(
                    updatedTopic.Name,
                    updatedTopic.TotalHours,
                    updatedTopic.HasFinalTest);

                if (topicResult.IsFailure)
                    return result;

            }

            
            return Result.Success();
        }



    }
}
