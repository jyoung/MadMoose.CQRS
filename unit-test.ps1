ForEach ($folder in (Get-ChildItem -Path .\Source -Directory -Filter *.Tests)) { 
    dotnet test $folder.FullName
}