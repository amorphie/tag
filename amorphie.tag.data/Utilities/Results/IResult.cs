namespace Core.Utilities.Results
{
    //Voidler yerine kullanılacak.İşlemin başarılı olup olmadığı
    // ve başarı başarısızlık mesajı döndürecek.
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
