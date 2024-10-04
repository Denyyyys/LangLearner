namespace LangLearner.Exceptions
{
    public class BadRequestException : GeneralAPIException
    {
        public BadRequestException(string message) : base(message)
        {
            StatusCode = 400;
        }
    }
}
