# Base image olarak .NET SDK kullanıyoruz
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build


# Çalışma dizini oluşturuyoruz
WORKDIR /app

# Proje dosyasını kopyalayıp bağımlılıkları yüklüyoruz
COPY .Api_Project/Api/Api.csproj .
RUN dotnet restore

# Kodları kopyalayıp uygulamayı derliyoruz
COPY . .
RUN dotnet publish -c Release -o out

# .NET runtime imajını kullanarak çalıştırma ortamı oluşturuyoruz
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Uygulamayı çalıştırma komutu
CMD ["dotnet", "Api.dll", "--urls", "http://*:8090"]
