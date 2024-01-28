# TelechatSharp
TelechatSharp handles the deserialization of JSON exported from Telegram Desktop, making it easy to work with chat data in .NET applications via custom classes, properties, and extension methods. 

## Getting Started
Usage of the library's core functionality is through a `Chat` object which takes the file path of your exported JSON as a constructor parameter:

```csharp
Chat chat = new Chat("telegram.json");
```

`Chat` can then be used to extract meaningful data from your chat history:

```csharp
// Returns a collection of all messages in the chat. 
var messages = chat.Messages;

// Returns a collection of all messages in the chat containing "lol", case-insensitive.
var containsLol = messages.Where(m => m.Text.Contains("lol", StringComparison.OrdinalIgnoreCase));
```

## `Chat.cs`

`Chat` exposes the following public properties, notably a collection of custom `Message` objects representing all messages in the chat. The library provides some flexibility during deserialization, in that only a `messages` JSON array must be present in the JSON for successful conversion. 

```csharp
public string? Name { get; set; }
public string? Type { get; set; }
public long? Id { get; set; }

[JsonRequired]
public IEnumerable<Message> Messages { get; set; } = default!;
```

