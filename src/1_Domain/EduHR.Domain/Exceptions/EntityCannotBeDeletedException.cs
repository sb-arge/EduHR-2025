namespace EduHR.Application.Features.Plans.Handlers
{
    public class EntityCannotBeDeletedException : Exception
    {
        public EntityCannotBeDeletedException(string message)
            : base(message)
        {
        }
    }
}
