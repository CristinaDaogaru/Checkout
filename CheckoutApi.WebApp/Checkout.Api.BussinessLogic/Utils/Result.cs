namespace Checkout.Api.BussinessLogic.Utils
{
    public class Result
    {
        public bool IsSuccessful { get; private set; }
        public string Error { get; private set; }
        public bool IsFailed => !IsSuccessful;
        protected internal Result(bool success, string error)
        {

            if (success && error != null)
            {
                throw new InvalidOperationException("A success result cannot have an error");
            }
            if (!success && error == null)
            {
                throw new InvalidOperationException("A failure result needs an error");
            }

            IsSuccessful = success;
            Error = error;
        }

        public static Result Ok()
        {
            return new Result(true, null);
        }

        public static Result<TValue> Ok<TValue>(TValue value)
        {
            return new Result<TValue>(value, true, null);
        }

        public static Result Fail(string error)
        {
            return new Result(false, error);
        }

        public static Result<TValue> Fail<TValue>(string error)
        {
            return new Result<TValue>(default, false, error);
        }
    }

    public class Result<TValue> : Result
    {
        private TValue _value;

        public TValue Value
        {
            get => !IsSuccessful
              ? throw new InvalidOperationException("Trying to access the value of a failure result")
              : _value;
            private set { _value = value; }
        }

        protected internal Result(TValue value, bool isSuccessful,
          string error)
            : base(isSuccessful, error)
        {
            Value = value;
        }
    }
}


