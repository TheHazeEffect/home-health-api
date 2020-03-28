#!/bin/bash
dotnet ef dbcontext scaffold "Server=localhost;Database=HomeHealthdb;Trusted_Connection=True;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer --context-dir data -o data/tables -f
