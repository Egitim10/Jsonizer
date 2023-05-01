# Jsonizer
Jsonizer is a library for practical usage for Dotnet  "System.Text.Jsonserializer"  


# USAGE OF EXTENTION METHODS
```
(Does not need  to be added to the services)
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
```

# USAGE OF METHODS OF CONCRETE CLASS
```
(Needs to be added to the services)
```
```csharp

builder.Services.AddDbContext<RedisApiDbContext>(ops => ops.UseSqlServer(builder.Configuration.GetConnectionString("ALO")));
builder.Services.AddSingleton<IJsonizer>(m =>
{
    return new MyJsonizer(false, JavaScriptEncoder.UnsafeRelaxedJsonEscaping, 
    JsonNamingPolicy.CamelCase, ReferenceHandler.IgnoreCycles);
});

var app = builder.Build();

List<Userx> users = new()
{
    new Userx{Name="u1"},
    new Userx{Name="u2"},
    new Userx{Name="u3"},
    new Userx{Name="u4"},
};

var jsonStrFromListOfObjects = users.JsonizerToString();
var objectListFromJsonString = jsonStrFromListOfObjects.JsonizertoObject<List<Userx>>();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RedisApiDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();


    db.Users.AddRange(users);
    db.SaveChanges();

    var jsonizer = scope.ServiceProvider.GetRequiredService<IJsonizer>();
    var jsonStr = jsonizer.JsonizerToString(users);
    var jsonObj = jsonizer.JsonizertoObject<List<Userx>>(jsonStr);

    //db.Database.Migrate();
    //var dbx = scope.ServiceProvider.GetRequiredService<UserxDBContetx>();
    //dbx.Database.Migrate();
}
```
