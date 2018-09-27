ForEach ($folder in (Get-ChildItem -Path .\ -Directory -Filter *.Tests)) { 
    dotnet test $folder.FullName
}