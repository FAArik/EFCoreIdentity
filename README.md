# EFCoreIdentity

Bu proje, [Entity Framework Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity) kütüphanesini öğrenmek amacıyla oluşturulmuştur. Proje, kimlik doğrulama ve yetkilendirme işlemlerinde Entity Framework Identity'nin temel işlevlerini keşfetmeye yönelik basit bir uygulamadır.

## Özellikler

- **Kullanıcı Yönetimi**: Kayıt, giriş ve profil yönetimi.
- **Rol Yönetimi**: Kullanıcı rollerinin atanması ve yönetimi.
- **Kimlik Doğrulama ve Yetkilendirme**: Kullanıcı oturum açma, rol bazlı erişim kontrolü.

## Kurulum

Projeyi kendi makinenize klonlayarak başlayabilirsiniz:

```bash
git clone https://github.com/FAArik/EFCoreIdentity.git
cd EFCoreIdentity
```

### Gereksinimler

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Entity Framework Core
- SQL Server veya başka bir EF Core uyumlu veritabanı

### Çalıştırma

Projeyi çalıştırmadan önce veritabanı yapılandırmasını ve migration işlemlerini tamamlamanız gerekmektedir:

1. **Veritabanı Bağlantısı**: `appsettings.json` dosyasındaki `ConnectionStrings` bölümünden veritabanı bağlantınızı ayarlayın.
2. **Migration İşlemi**: Aşağıdaki komutu çalıştırarak veritabanınızı oluşturun ve güncelleyin.

   ```bash
   dotnet ef database update
   ```

3. **Uygulamayı Başlatma**:

   ```bash
   dotnet run
   ```

## Kullanım

1. Uygulama çalıştırıldığında, kayıt ekranından yeni bir kullanıcı oluşturabilirsiniz.
2. Giriş yaptıktan sonra kullanıcı bilgilerinizi görüntüleyebilir ve rol yönetimi işlemlerini gerçekleştirebilirsiniz.
