FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /source
# Copy project files
COPY ["HomeHealth.csproj", "."]
RUN ls -l
#install dotnet dependencies
RUN dotnet restore "HomeHealth.csproj"

#install node dependencies
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install -y nodejs
COPY . .
RUN npm --prefix ./ClientApp/src install -g react ./ClientApp/src

RUN dotnet publish -c Release -o /publish

#build runtimeimage
FROM  mcr.microsoft.com/dotnet/core/aspnet:3.1.0
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "HomeHealth.dll"]