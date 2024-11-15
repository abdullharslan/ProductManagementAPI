/*
 * ApiResponse sınıfı, API yanıtlarını tek bir yapı altında toplamak için oluşturulmuş genel bir yanıt modelidir.
 * Bu model, başarılı veya başarısız işlemler için standart bir veri yapısı sağlar.
 *
 * Genel Özellikler:
 * - Success: İşlemin başarılı olup olmadığını belirten boolean değer.
 * - Message: İşlem sonucu hakkında bilgi veren mesaj.
 * - Data: İşlem sonucunda döndürülen veri (T tipi).
 *
 * ValidationError sınıfı, doğrulama hataları durumunda hatalı olan özellikler ve hata mesajları hakkında bilgi sağlar.
 *
 * ErrorResponse sınıfı, başarısız işlemler için kullanılan yanıt modelidir. ValidationError nesnelerini içerir.
 *
 * SuccessResponse sınıfı, başarılı işlemler için kullanılan yanıt modelidir. Başarılı işlemler için özel bir
 * yapı sunarak başarı durumunu belirtir.
 */
namespace ProductManagementAPI.Core.Response;

/*
 * Genel API yanıt modeli
 * <typeparam name="T">Yanıt verisi tipi</typeparam>
 */
public class ApiResponse<T>
{
    /* İşlemin başarılı olup olmadığını belirler */
    public bool Success { get; set; }

    /* İşlem sonucu ile ilgili mesaj */
    public string Message { get; set; }

    /* İşlem sonucu dönen veri */
    public T Data { get; set; }
}

/*
 * Validation hatası detaylarını içeren model
 */
public class ValidationError
{
    /* Hatalı özelliğin adı */
    public string PropertyName { get; set; }

    /* Hata mesajı */
    public string ErrorMessage { get; set; }
}

/*
 * Hata yanıtı için kullanılan model
 */
public class ErrorResponse
{
    /* Başarısız işlemde false olarak ayarlanır */
    public bool Success { get; set; }

    /* Hata mesajı */
    public string Message { get; set; }

    /* Doğrulama hataları listesi */
    public List<ValidationError> Errors { get; set; }

    public ErrorResponse()
    {
        /* Hatalı işlemler için false olarak ayarlanır */
        Success = false;

        /* Hata mesajları listesi başlatılır */
        Errors = new List<ValidationError>();
    }
}

/*
 * Başarılı işlem yanıtı için kullanılan model
 * <typeparam name="T">Yanıt verisi tipi</typeparam>
 */
public class SuccessResponse<T> : ApiResponse<T>
{
    public SuccessResponse(T data, string message = null)
    {
        /* Başarılı işlemler için true olarak ayarlanır */
        Success = true;

        /* İşlem sonucu ile ilgili mesaj */
        Message = message;

        /* Başarı durumunda döndürülen veri */
        Data = data;
    }
}