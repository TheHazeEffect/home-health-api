FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

# Copy project files
WORKDIR /source
COPY ["HomeHealth.csproj", "./src"]

#install dotnet dependencies
RUN dotnet restore "src/HomeHealth/HomeHealth.csproj"

#install node dependencies
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install -y nodejs
COPY . .
RUN npm --prefix ./source/src/ClientApp/src install -g react ./src

#goto directory and build application
WORKDIR /source/src
RUN dotnet publish -c Release -o /publish

FROM  mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "HomeHealth.dll"]