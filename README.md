# TelechatSharp

![NuGet Version](https://img.shields.io/nuget/v/TelechatSharp?style=flat-square)
![Static Badge](https://img.shields.io/badge/downloads-518-red?style=flat-square) <? Manually updated as shields.io sometimes fails to fetch ?>
![GitHub License](https://img.shields.io/github/license/kylejsarte/TelechatSharp?style=flat-square)

TelechatSharp handles the deserialization of JSON exported from Telegram Desktop into C# objects, making it easy to work with your chat data in .NET applications through custom classes, properties, and extension methods. 

TelechatSharp is [available on NuGet](https://www.nuget.org/packages/TelechatSharp).

```cmd
dotnet add package TelechatSharp
```

## Accessing Your Data  
Construct a new `Chat` object using the file path to your JSON:

```csharp
Chat chat = new Chat("telegram.json");
```

Easily extract data from your chat history:

```csharp
var messages = chat.Messages;

var messagesFromKyle = messages.FromMember("Kyle Sarte");

var messagesContainingLol = messages.ContainingText("lol");

var emailsSentInChat = messages.GetAllTextsOfTextEntityType("email");

var phoneNumbersSentInChat = messages.GetAllTextOfTextEntityType("phone");
```

## Advanced Use Cases

For advanced data analysis use cases, combine `TelechatSharp` with libraries such as `Microsoft.Data.Analysis`:

```csharp
using TelechatSharp.Core;
using Microsoft.Data.Analysis;

Chat chat = new Chat("telegram.json");

// From TelechatSharp.Core, get chat member names and their messages.
var from = chat.Messages.Select(m => m.From);
var text = chat.Messages.Select(m => m.Text);

// From Microsoft.Data.Analysis, create a new DataFrame using data from TelechatSharp.
DataFrameColumn[] columns = {
   new StringDataFrameColumn("From", from),
   new StringDataFrameColumn("Text", text)
};

DataFrame dataFrame = new(columns);
```

Code produces a DataFrame similar to the following:

| From   | Text                           |
|--------|--------------------------------|
| Céline | You are gonna miss that plane. |
| Jesse  | I know.                        |

## Further Reading
- **[Chat.cs](#chatcs)**
	- **[ChatExtensions.cs](#chatextensionscs)**
- **[Message.cs](#messagecs)**
 	- **[Message Types](#messagetypes)**
	- **[MessagesExtensions.cs](#messagesextensionscs)**
- **[Text.cs and TextEntity.cs](#textcs---textentitycs)**
  	- **[Text.cs](#textcs)**
 
_For readability, JSON samples in this README will omit most properties and only include those relevant to the topic being discussed._

## `Chat.cs`

While the library is most useful through a `Chat` object's `Messages` property, some derived data about a chat can be accessed via extension methods:

### `ChatExtensions.cs`

```csharp
var dateCreated = chat.GetDateCreated();

var members = chat.GetMembers();

var originalMembers = chat.GetOriginalMembers();
```

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

Output is:
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
var messageTypeMessages = chat.Messages.GetMessageTypeMessages();
var serviceTypeMessages = chat.Messages.GetServiceTypeMessages();
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
            },
            {
               "type": "mention",
               "text": "Kyle Sarte"
            }
         ]
      }
   ]
}
```

```csharp
// Get the message with ID "1".
var message = chat.Messages.Where(m => m.Id == 1).FirstOrDefault();

foreach (TextEntity textEntity in message.TextEntities)
{
    Console.WriteLine($"Type: {textEntity.Type}, Text: {textEntity.Text}");
}
```
Output is:
>`Type: link, Text: https://www.kylejsarte.com`
>
>`Type: mention, Text: Kyle Sarte`
