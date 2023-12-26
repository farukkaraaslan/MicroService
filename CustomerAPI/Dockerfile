FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
EXPOSE 8080
EXPOSE  5002
COPY . .
RUN dotnet restore
RUN dotnet publish CustomerAPI.csproj -c Release -o out
WORKDIR out
ENV ASPNETCORE_URLS="http://*:5002"
ENTRYPOINT ["dotnet", "CustomerAPI.dll"]