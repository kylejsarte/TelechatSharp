
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

- **[Chat.cs](#chatcs)**
	- **[ChatExtensions.cs](#chatextensionscs)**
- **[Message.cs](#messagecs)**
 	- **[Message Types](#messagetypes)**
	- **[MessagesExtensions.cs](#messagesextensionscs)**
- **[Text.cs and TextEntity.cs](#textcs---textentitycs)**
  	- **[Text.cs](#textcs)**
 
_For readability, JSON samples in this README will omit most properties and only include those relevant to the topic being discussed._

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
Additional data about the chat is made available via extension methods:

```csharp
var dateCreated = chat.GetDateCreated();
var members = chat.GetMembers();
var originalMembers = chat.GetOriginalMembers();
```

Extension methods are used to simplify the base `Chat` model so that it contains only properties present in the original JSON.

## `Message.cs`
Data about individual messages can be accessed through properties of the custom `Message` object:

```json
{
   "messages": [
      {
         "type": "message",
	 "date": "2024-01-27T19:40:00",
	 "from": "Kyle Sarte",
         "text": "TelechatSharp is public!"
      }
   ]
}
```

```csharp
foreach (Message message in chat.Messages)
{
  Console.WriteLine($"On {message.Date}, {message.From} said '{message.Text}'");
}
```

Given the sample JSON, the output would be:
>`On 1/27/2024 7:40:00 PM, Kyle Sarte said 'TelechatSharp is public!'`

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
Use `Message.Text` to get a plain text string of a message's text content.

The `text` property of a message in the JSON is polymorphic—`text` can either be a plain text string or an array of plain text strings and _text entity_ objects. `Message.Text` is backed by a private field of custom type `Text` which handles any necessary string building to abstract these text entities away and simplify text retrieval.

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

### `TextEntity.cs`

Use `Message.TextEntities` for finer-grain access to text data.

`Message.TextEntities`, unlike `Message.Text`, preserves the structure of objects returned from the `text_entities` property of messages in the JSON. `Message.TextEntities` returns a collection of custom `TextEntity` types with `Type` and `Text` properties:

```json
{
   "messages": [
      {
         "id": 1,
         "type": "message",
         "text_entities": [
            {
               "type": "link",
               "text": "https://www.kylejsarte.com"
            }
         ]
      }
   ]
}
```

```csharp
// Get the message with ID "1".
var message = messages.Where(m => m.Id == 1).FirstOrDefault();

foreach (TextEntity textEntity in message.TextEntities)
{
    Console.WriteLine($"Type: {textEntity.Type}, Text: {textEntity.Text}");
}
```
Given the sample JSON, the output would be:
>`Type: link, Text: https://www.kylejsarte.com`
