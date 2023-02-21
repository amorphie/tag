using System.ComponentModel;

public enum Status
{
    [Description("Tüm başarılı işlemler için kullanılır")]
    Success = 1,
    [Description("Tüm hatalı ve uyarı işlemleri için kullanılır")]
    Error = 2,
}