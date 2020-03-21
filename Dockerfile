FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /source
# Copy project files
COPY ["HomeHealth.csproj", "./HomeHealth"]
RUN ls Homehealth -l
#install dotnet dependencies
RUN dotnet restore HomeHealth/Homehealth.csproj 

#install node dependencies
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install -y nodejs
COPY . .
RUN npm --prefix ./src/ClientApp/src install -g react ./src/ClientApp/src

#goto directory and build application
WORKDIR /source/src
RUN dotnet publish -c Release -o /publish

FROM  mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "HomeHealth.dll"]