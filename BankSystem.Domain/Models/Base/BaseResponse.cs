namespace BankSystem.Domain.Models.Base
{
    public class BaseResponse
    {
        protected internal BaseResponse(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }
            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            Error = error;
            IsSuccess = isSuccess;

            var x = new BaseEntity();
            
        }
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; set; }

        public static BaseResponse Success() => new (true, Error.None);
        public static BaseResponse Failure(Error error) => new (false, error);
        public static BaseResponse<T> Success<T>(T data) => new (data,true, Error.None);
        public static BaseResponse<T> Failure<T>(T data, Error error) => new (default!,false, error);

    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        protected internal BaseResponse(T data,bool isSuccess, Error error) : base(isSuccess, error)
        {
            Data = data;
        }
    }
}
