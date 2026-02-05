# שלב הבנייה
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# העתקת קבצי הפרויקט (csproj) כדי לבצע Restore
COPY ["Proyect/Proyect.csproj", "Proyect/"]
COPY ["Proyect.CORE/Proyect.CORE.csproj", "Proyect.CORE/"]
COPY ["Proyect.DATA/Proyect.DATA.csproj", "Proyect.DATA/"]
COPY ["Proyect.Service/Proyect.Service.csproj", "Proyect.Service/"]

RUN dotnet restore "Proyect/Proyect.csproj"

# העתקת כל שאר הקבצים ובנייה
COPY . .
WORKDIR "/src/Proyect"
RUN dotnet build "Proyect.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Proyect.csproj" -c Release -o /app/publish

# שלב הסופי - הרצה
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Proyect.dll"]
