namespace SmartCharging.API.DTOs
{
    public class Result
    {
        public string Message { get; protected set; }
        public bool IsOk { get; protected set; }

        public Result Ok(string message = null) 
        { 
            Message = string.IsNullOrEmpty(message) ? string.Empty : message;
            IsOk = true;
            return this;
        }

        public Result Fail(string message = null)
        {
            Message = string.IsNullOrEmpty(message) ? string.Empty : message;
            IsOk = false;
            return this;
        }
    }
}
