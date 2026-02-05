# שלב הבנייה
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# העתקת קבצי הפרויקט עם שמות מדויקים לפי התמונות שלך
COPY ["Proyect/Proyect.csproj", "Proyect/"]
COPY ["Proyect.CORE/Proyect.Core.csproj", "Proyect.CORE/"]
COPY ["Proyect.DATA/Proyect.Data.csproj", "Proyect.DATA/"]
COPY ["Proyect.Service/Proyect.Service.csproj", "Proyect.Service/"]

# ביצוע Restore
RUN dotnet restore "Proyect/Proyect.csproj"

# העתקת כל שאר הקבצים
COPY . .
WORKDIR "/src/Proyect"

# בנייה (Build)
RUN dotnet build "Proyect.csproj" -c Release -o /app/build

# פרסום (Publish)
FROM build AS publish
RUN dotnet publish "Proyect.csproj" -c Release -o /app/publish /p:UseAppHost=false

# שלב ההרצה הסופי
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Proyect.dll"]
