## Proje Kurulumu
projeyi kopyaladiktan sonra kendi `appsettings.json` dosyani eklemeyi unutma 
asagida bir ornek birakiyorum 
bu dosyanin icerisinde senin veritabani baglanti bilgilerin 
yer alacak dosyayi gitignore icerisine ekledim senin veritabani stringin ile 
benimki karismayacak 

``{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=StajTakipDb;User Id=sa;Password=mssql2026!;TrustServerCertificate=True;"
  },
  "AllowedHosts": "*"
}
``