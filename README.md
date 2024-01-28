# TelechatSharp
TelechatSharp handles the deserialization of JSON exported from Telegram Desktop, making it easy to work with chat data in .NET applications through a public interface of custom classes, properties, and extension methods. 

## Getting Started
Usage of the library's core functionality is through a `Chat` object which takes your exported JSON as its only public constructor's parameter:

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

`Chat` exposes the following public properties which are modeled after the Telegram JSON schema, notably a collection of custom `Message` objects which contains object representations of all messages in the chat. The library provides some flexibility during deserialization, in that only the `messages` property—a JSON array—a must be present in the JSON for successful conversion. 

```csharp
public string? Name { get; set; }
public string? Type { get; set; }
public long? Id { get; set; }

[JsonRequired]
public IEnumerable<Message> Messages { get; set; } = default!;
```
