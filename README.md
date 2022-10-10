
# Tags

Kayıtların kaynaklarını ve kaynakların sağlayabildiği ek bilgileri içerir.

:::info
Tam bağımsız bir mikro servis uygulama olarak tasarlanıp hizmete alınacaktır.
:::



## Tag Tanımı

Tag tanımı diğer kaytılarla ilikilendirebilicek dış servis kayıtlarını ve görselleştirme için gereken tanımları içerir.

* **Code:** Her bir kaynak kod içerir. Bu kod aynı zamanda kaynağın atandığı kaydın bir özelliği haline gelir. Yetkilendirme ve izleme süreçlerinde bu kod kısıt olarak kullanılabilir.
* **Url:** Kaynak bilginin tedarik edileceği Url. Url üzerinde parametre bilgileri tanımlanabilir.URL tanımı sadece HTTP GET olarak tanımlanır.Method her zaman JSON veri döner. Parametreler Tag servisinden veri istendiğinde sağlanan parametredir. Parametreler URL üzerinden gönderilir
* **Retention:** Kullanılacak verinin önbellekleme süresini belirler. Çağrılar için parametre özelinde cache uygulanır.
* **Template:** Kaynak verisinin kullanıcı arabirimine dönüştürüşebilmesi için gereken template kodunu belirtir. Template tanımları ve render süreci için  **Template Engine** kullanılır.

| Tag                | Url                                           | Retention | Template              |
| ------------------ | --------------------------------------------- | --------- | --------------------- |
| corporate-customer | cb.burgan.com.tr/corporate-customer/{param1} | 1m        | ui-corporate-customer |
| loan-partner       | cb.burgan.com.tr/partner/{param1}            | 1h        | ui-partner-info       |
| burgan-bank        | cb.burgan.com.tr/bank-info                    | 1h        | ui-burgan-splash      |
| retail-customer    | cb.burgan.com.tr/retail-customer/{param1}    | 1m        | ui-retail-customer    |
| potential-customer | cb.burgan.com.tr/application/{param1}        | 1m        | ui-retail-customer    |
| bank-staff         | cb.burgan.com.tr/staff/{param1}              | 1m        | ui-staff-info         |
| customer-360       | cb.burgan.com.tr/360/{param1}                | 1m        | ui-user-360           |


:::warning
* Dönen veriler için **şema validasyonu** veya **mapping çalışması** planlanmıyor !
* Birden fazla template tipi değerlendirilebilir. (Mobil, HTML, Text gibi. )
:::


## Servisler

### Definition
#### GET /tag?keyword=customer
**Tag** kayıtlarını döner. Sayfalama ve free text arama özellilkleri sağlar.

#### POST /tag
Tag adına göre varolan bir **Tag** kaydını günceller veya yenisini oluşturur.

#### DELETE /tag/{tag}
**Tag** kaydını siler

#### GET /tag/{tag}
İstenen **Tag** ile ilgili detaylı kayıt bilgilerini döner.


### Data
#### GET /tag/{tag}/data?param1=xxx&param2=yyy
Tag kaydının bağımlı oldu veri kümesini döner.

#### GET /tag/{tag}/template?param1=xxx&param2=yyy
Tag kaydının bağımlı oldu veri kümesini kullanarak template render sonucunu döner.


