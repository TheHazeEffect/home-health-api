FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /src
# Copy project files
COPY ["HomeHealth.Web.csproj", "."]
# RUN ls -l
#install dotnet dependencies
RUN dotnet restore "HomeHealth.Web.csproj"
# RUN dotnet restore 
COPY . .

RUN dotnet publish -c release -o /publish

#build runtimeimage
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
# ENTRYPOINT ["dotnet", "HomeHealth.Web.dll"]
# CMD ["dotnet", "HomeHealth.Web.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet HomeHealth.Web.dll