# ENSEK_Test

.Net Core 7.

The create_projects.ps1 is something I used to set up the solution, and apologies in advance for the state of the UI.

Calling API:

```powershell
curl --location 'http://localhost:5087/api/meter-reading-uploads' --form 'source=@"<path to input file>"'
```

