
# TelechatSharp
TelechatSharp handles the deserialization of JSON exported from Telegram Desktop, making it easy to work with your chat data in .NET applications through custom classes, properties, and extension methods. 

## Quick Start  
Construct a new `Chat` object using the file path to your JSON:

```csharp
Chat chat = new Chat("telegram.json");
```

Extract meaningful data from your chat history:

```csharp
// Returns a collection of every message in the chat. 
var messages = chat.Messages;

// Returns a collection of all messages in the chat containing "lol", case-insensitive.
var containsLol = messages.Where(m => m.Text.Contains("lol", StringComparison.OrdinalIgnoreCase));

// Returns a collection of all emails sent in the chat.
var emails = messages.GetAllTextsOfTextEntityType("email");
```

## Contents

*Full API documentation coming soon.*

- **[Chat.cs](#chatcs)**
	- **[ChatExtensions.cs](#chatextensionscs)**
- **[Message.cs](#messagecs)**
	- **[MessagesExtensions.cs](#messagesextensionscs)**
- **[Text.cs and TextEntity.cs](#textcs---textentitycs)**
  	- **[Text.cs](#textcs)**

For readability, all JSON samples in this README will omit most properties and only include ones relevant to the toppic being discussed.

## `Chat.cs`

`Chat` provides some flexibility during deserialization, in that only a `messages` JSON array must be present in the JSON for a successful conversion. Once deserialized, `Chat.Messages` will return a collection of custom `Message` objects containing every message in the chat:

```csharp
public string? Name { get; set; }

public string? Type { get; set; }

public long? Id { get; set; }

[JsonRequired]
public IEnumerable<Message> Messages { get; set; } = default!;
```

### `ChatExtensions.cs`
To simplify the base `Chat` model to contain only properties present in the original JSON, additional data about the chat is made available via extension methods:

```csharp
var dateCreated = chat.GetDateCreated();
var members = chat.GetMembers();
var originalMembers = chat.GetOriginalMembers();
```

## `Message.cs`
Data about individual messages can be accessed through properties of the custom `Message` object:

```csharp
var messages = chat.Messages;

foreach (var message in messages)
{
  Console.WriteLine($"On {message.Date}, {message.From} said '{message.Text}'.");
}
```

### Message Types
In the schema, Telegram messages are either  `Message` types or `Service` types. A `Message` is any basic text message. A `Service` is any action performed on the chat such as the pinning of a message or the invitation of a new member:

```json
{
   "messages": [
      {
         "id": 1,
         "type": "message",
         "text": "Someone please change the group photo."
      },
      {
         "id": 2,
         "type": "service",
         "action": "edit_group_photo"
      }
   ]
}
```


Messages can be filtered by type through extension methods:

```csharp
var messageTypeMessages = messages.GetMessageTypeMessages();
var serviceTypeMessages = messages.GetServiceTypeMessages();
```

### `MessagesExtensions.cs`
For a full list of available extension methods on `Message` collections, refer to [`MessagesExtensions.cs`](https://github.com/kylejsarte/TelechatSharp/blob/main/TelechatSharp.Core/Extensions/MesssagesExtensions.cs).

*Full API documentation coming soon.*

## `Text.cs` &  `TextEntity.cs`

`Message.Text` and `Message.TextEntities` can be used to work with text data.

### `Text.cs`
`Message.Text` returns a string of a message's text content. It is backed by a private field of custom type `Text` which handles building a raw string since the `text` property of a message object in the Telegram JSON schema is polymorphic—`text` can be either a plain text string or an array of plain text strings and *text entity* objects:
```json
{
   "messages": [
      {
         "type": "message",
         "text": "This is a message with only plain text."
      },
      {
         "type": "message",
         "text": [
            "This is a message with plain text and ",
            {
               "type": "bold",
               "text": "bold text."
            },
            " The bold text appears in the JSON as a text entity. Links, such as ",
            {
               "type": "link",
               "text": "https://www.kylejsarte.com"
            },
            " will also appear as text entities."
         ]
      }
   ]
}
```
A call to `Message.Text` for the second message would build and return the following plain text string:

>`This is a message with plain text and bold text. The bold text appears in the JSON as a text entity. Links such as https://www.kylejsarte.com will also appear as text entities.`
