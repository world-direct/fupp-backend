## Build

To build and package this, following steps are needed. (This assumes you have the dotnet sdk installed.)


```
dotnet restore -r linux-x64
dotnet publish -c Release -r win7-x64
```

then docker can grab the publish folder.

# Run

to just run the program locally, just use

```
dotnet run
```