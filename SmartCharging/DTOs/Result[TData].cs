namespace SmartCharging.API.DTOs
{
    public class Result<TData> : Result where TData : class
    {
        public TData Data { get; private set; }
        public Result<TData> WithData(TData data)
        {
            Data = data;
            return this;
        }

        public new Result<TData> Ok(string message = null)
        {
            base.Ok(message);
            return this;
        }

        public new Result<TData> Fail(string message = null)
        {
            base.Fail(message);
            return this;
        }
    }
}
