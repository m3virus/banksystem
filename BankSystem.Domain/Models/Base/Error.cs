namespace BankSystem.Domain.Models.Base
{
    public record Error(string Name,  string Message)
    {
        public static Error NullVallue = new("Error.NullValue", "Null Value Provider");
        public static Error None = new(string.Empty, string.Empty);

        public static Error NoDestinatinoAccountFound = new("Error.NoDestinatinoAccountFound", "حسابی با این شماره برای واریز موجود نیست");
        public static Error NoOriginAccountFound = new("Error.NoOriginAccountFound", "حسابی با این شماره برای برداشت موجود نیست");
        public static Error SameAccountFound = new("Error.SameAccountFound", "شماره حساب مبدا و مقصد نباید یکی باشد");
        public static Error OriginAccountIsBlock = new("Error.OriginAccountIsBlock", "شماره حساب مبدا مسدود است");
        public static Error OriginAccountIsInActive = new("Error.OriginAccountIsInActive", "شماره حساب مبدا غیر فعال است");
        public static Error DestinationAccountIsBlock = new("Error.DestinationAccountIsBlock", "شماره حساب مقصد مسدود است");
        public static Error DestinationAccountIsInActive = new("Error.DestinationAccountIsInActive", "شماره حساب مقصد غیر فعال است");

        public static Error CreateFailed = new("Error.CreateFailed", "failed to create");
        public static Error UpdateFailed = new("Error.UpdateFailed", "failed to update");
        public static Error DeleteFailed = new("Error.DeleteFailed", "failed to delete");

        public static Error CustomerNotFound = new("Error.CustomerNotFound", "Customer is not find");
        public static Error AccountNotFound = new("Error.AccountNotFound", "Account is not find");
    }
}
