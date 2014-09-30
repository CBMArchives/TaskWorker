nuget spec 
nuget pack TaskWorker_BLL.vbproj -IncludeReferencedProjects -Verbosity Detailed
nuget push TaskWorker_BLL.1.0.0.13.nupkg -s http://tfs.cbmarchives.com/nuget e1602adb-6e8d-4434-8050-d844e32d19a8
pause 
