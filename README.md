for more complex applications, the chat can be dependency injected: 

```csharp
services.AddScoped<Chat>(provider =>
{
    var jsonFilePath = "result.json"; 
    return new Chat(jsonFilePath);
});
```
Distinct() was faster than ToHashSet() in LINQ calls to get specific entries
