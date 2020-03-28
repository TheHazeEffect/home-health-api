 Get-ChildItem "C:\Users\Sapna-USER\Desktop\Troy\School\Yr4 Sem2\Ebusiness\HomeHealth\Data\tables" -Filter *.cs |
 Foreach-Object {
     $scaffoldCmd =
     'dotnet aspnet-codegenerator ' +
     '-p "C:\Users\Sapna-USER\Desktop\Troy\School\Yr4 Sem2\Ebusiness\HomeHealth\HomeHealth.csproj" ' +
     'controller ' +
     '-name ' + $_.BaseName + 'Controller ' +
     '-api ' +
     '-m HomeHealth.Models.' + $_.BaseName + ' ' +
     '-dc HomeHealthDbContext ' +
     '-outDir Controllers ' +
     '-namespace HomeHealth.Controllers'

     # List commands for testing:
     $scaffoldCmd 

	# Excute commands (uncomment this line):
    # iex $scaffoldCmd
}
