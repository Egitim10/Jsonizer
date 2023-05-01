# Jsonizer
Jsonizer is a library for practical usage for Dotnet  "System.Text.Jsonserializer"  


# USAGE OF EXTENTION METHODS
```
```csharp
List<Userx> users = new()
{
    new Userx{Name="u1"},
    new Userx{Name="u2"},
    new Userx{Name="u3"},
    new Userx{Name="u4"},
};

var jsonStrFromListOfObjects = users.JsonizerToString();
var objectListFromJsonString = jsonStrFromListOfObjects.JsonizertoObject<List<Userx>>();



