# Home Health Medical Professional Locator (Web API)

[![pipeline status](https://gitlab.com/troyanderson.d/home-health-medical-professional-locator/badges/master/pipeline.svg)](https://gitlab.com/troyanderson.d/home-health-medical-professional-locator)
[![coverage report](https://gitlab.com/troyanderson.d/home-health-medical-professional-locator/badges/master/coverage.svg)](https://gitlab.com/troyanderson.d/home-health-medical-professional-locator/commits/master)

Repository for the Home Health Medical Professional Location Web App.
Built with [.NetCore 3.1](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-3.1).


## Required tools

- [Git](https://git-scm.com/downloads)
- [Dotnet EF](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)
- [Docker](https://www.docker.com/) (Optional)

## Getting Started

Once the required tools have been installed, you can finally clone the project and begin setting it up for local development.

- Clone the project using the `git` command:

```shell
git clone <url>
```

- Download Nuget Packages and dependencies by running the command:

```shell
Dotnet Restore
```

- Finally, to start the project:

```php
dotnet run
```

NB: Migrations are ran automatically

You can then access the app at http://localhost:5001

---

By default, the project uses an embedded database (SQLite), so no need to worry about setting up a database.

---

## F.A.Q

**Q:** 

**A:** 
