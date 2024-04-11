# OpenWikiApi

[![created by](https://img.shields.io/badge/created%20by-KandaTeach-blue.svg?longCache=true&style=flat-square)](https://github.com/KandaTeach) [![License](https://img.shields.io/github/license/KandaTeach/OpenWikiApi.svg?style=flat-square)](https://github.com/KandaTeach/OpenWikiApi/blob/main/LICENSE)

OpenWikiApi is a straightforward open-source wiki API project written in asp dotnet core. This project was constructed following the Clean Architecture Design principles, incorporating Domain-Driven Design.

**DISCLAIMER:**
OpenWikiApi is a personal project, developed to apply the creator's learning and expertise. It was not commissioned by a corporation or any client.

## Architectural Design

<p align="center">
  <img src="images/OpenWikiApi - Architectural Design.png" width="4000">
</p>

OpenWikiApi was built following the Clean Architecture Design. The design consists of four layers: presentation, infrastructure, application, and domain.

**Presentation Layer**
Under the presentation layer lies the API itself, which includes the controllers and response mappings.

| Libraries | Concepts |
|:----------|:---------|
| [Mapster](https://github.com/MapsterMapper/Mapster) | Mediator Pattern |

**Infrastructure Layer**
Under the infrastructure layer lies persistence, which implements communication with the database. Additionally, this layer implements the generation of JWT token.

| Libraries | Concepts |
|:----------|:---------|
| [Entity Framework Core](https://github.com/dotnet/efcore)   | ORM |
| [IdentityModel.Token.Jwt](https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet) ||

**Application Layer**
The application layer communicates with the infrastructure layer to execute queries and commands. Additionally, it validates requests before they are sent as queries or commands.

| Libraries | Concepts |
|:----------|:---------|
| [MediatR](https://github.com/jbogard/MediatR) | CQRS (Command and Query Responsibility Segregation)|
| [FluentValidation](https://github.com/FluentValidation/FluentValidation) | Repository Pattern |

**Domain Layer**
The domain layer is the centerpiece of this architecture. It contains the aggregates and entities that define the behavior, rules, and functionalities. In this layer, it follows the Domain Driven Design (DDD).

| Libraries | Concepts |
|:----------|:---------|
| [ErrorOr](https://github.com/amantinband/error-or) | Aggregates |
|| Aggregate Roots |
|| Value Objects |
|| Entities |
|| Domain Errors |

## Installation

1. Fork and clone the project repository.
2. To set up your database in Docker, download the SQL Server Docker image. [here](https://hub.docker.com/_/microsoft-mssql-server).
3. Run the image as container by executing this command `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=[YOUR STRONG PASSWORD!]" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`.
4. Open the JSON file located at `src/OpenWikiApi.Api/appsettings.sample.json`. Change the `ConnectionString` value to `Server=localhost;Database=OpenWikiApiDb;User Id=sa;Password=[YOUR STRONG PASSWORD];Encrypt=false`. Also modify the `JwtSettings` if neccessary.
5. Apply the migrations to the database by executing this command `dotnet ef database update -p .\src\OpenWikiApi.Infrastructure\ -s .\src\OpenWikiApi.Api\ --connection "Server=localhost;Database=OpenWikiApiDb;User Id=sa;Password=[YOUR STRONG PASSWORD];Encrypt=false"`.
6. Run the web API by executing this command `dotnet run --project .\src\OpenWikiApi.Api\ --environment Production`, then navigate to SwaggerUI at `http://localhost:5138/swagger`.
7. If you prefer not to use SwaggerUI, navigate to the folder named [HttpRequest](HttpRequest) and run those http requests files.

## Usage

These are the few sample usages of this web API project. Additionally, there are endpoints that haven't been included here that can be accessed with an admin role. Feel free to try them out for yourself.

**Register User**
You can add a user with either a member or an admin role by simply modifying the endpoint's suffix.

*Request*

```http
@host=http://localhost:5138

POST {{host}}/auth/register/member <- Change to admin if you want an admin user.
Content-Type: application/json

{
  "nickname": "Sample Member 1",
  "credential": {
    "username": "sampleMember1",
    "password": "12345"
  },
  "profile": {
    "name": "Sample 1. Member",
    "age": 25,
    "email": "sampleMember1@gmail.com"
  }
}
```

*Response*

```json
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Thu, 11 Apr 2024 16:38:26 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
  "id": "9ebfa4de-9527-48b8-ba05-44b4177184f3",
  "nickname": "Sample Member 1",
  "name": "Sample 1. Member",
  "age": 25,
  "email": "sampleMember1@gmail.com"
}
```

**Login User**

*Request*

```http
@host=http://localhost:5138

POST {{host}}/auth/login
Content-Type: application/json

{
  "username": "sampleMember1",
  "password": "12345"
}
```

*Response*

```json
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Thu, 11 Apr 2024 16:46:18 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
  "id": "9ebfa4de-9527-48b8-ba05-44b4177184f3",
  "nickname": "Sample Member 1",
  "name": "Sample 1. Member",
  "age": 25,
  "email": "sampleMember1@gmail.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI5ZWJmYTRkZS05NTI3LTQ4YjgtYmEwNS00NGI0MTc3MTg0ZjMiLCJqdGkiOiIwMTdjYjU5OS04MWE3LTRjNzYtYjI3NC0yMjMyYTY2NjM5ZDYiLCJyb2xlcyI6Ik1lbWJlciIsInBlcm1pc3Npb25zIjpbIlJlYWQiLCJVcGRhdGUiLCJEZWxldGUiLCJJbnNlcnQiXSwiZXhwIjoxNzEyODU3NTc4LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUxMzgvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTM4LyJ9.JEBPYVXvHs5xTtUCgiDHEPGtBeTWc0NzYmqTTZ6GKGI"
}
```

**Create an Article**

Make sure that all text are escape.

*Request*

```http
@host=http://localhost:5138
@token=Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI5ZWJmYTRkZS05NTI3LTQ4YjgtYmEwNS00NGI0MTc3MTg0ZjMiLCJqdGkiOiIwMTdjYjU5OS04MWE3LTRjNzYtYjI3NC0yMjMyYTY2NjM5ZDYiLCJyb2xlcyI6Ik1lbWJlciIsInBlcm1pc3Npb25zIjpbIlJlYWQiLCJVcGRhdGUiLCJEZWxldGUiLCJJbnNlcnQiXSwiZXhwIjoxNzEyODU3NTc4LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUxMzgvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTM4LyJ9.JEBPYVXvHs5xTtUCgiDHEPGtBeTWc0NzYmqTTZ6GKGI

POST {{host}}/article/new
Content-Type: application/json
Authorization: {{token}}

{
  "title": "Osu!",
  "content": "Osu![a] (stylized as osu!) is a free-to-play rhythm game primarily developed, published, and created by Dean \"peppy\" Herbert. Inspired by iNiS' rhythm game Osu! Tatakae! Ouendan, it was written in C# on the .NET Framework,[4] and was released for Microsoft Windows on 16 September 2007. The game has throughout the years been ported to macOS, Linux, Android and iOS.\r\n\r\nAside from Osu! Tatakae! Ouendan, the game has been inspired by titles such as Taiko no Tatsujin, Beatmania IIDX,[5] EZ2DJ (EZ2CATCH), Elite Beat Agents, O2Jam, StepMania, and DJMax. All \"beatmaps\" in the game are community-made through the in-game map editor or through external tools. Four different game modes exist, offering various ways to play a beatmap. These modes can also be combined with optional modifiers, which can increase or decrease the difficulty.\r\n\r\nGameplay and features\r\nThere are four official game modes: \"osu!\" (called \"osu! standard\"), \"osu!taiko\", \"osu!catch\", and \"osu!mania\". With the addition of osu!lazer, players can now add custom gamemodes to the osu! client.[6][7] The original osu!standard mode remains the most popular to date and as of January 2023, the game has over 19.3 million monthly active users according to the game's global country leaderboards.[8]\r\n\r\n\"osu!standard\" takes direct inspiration from Osu! Tatakae! Ouendan games. In this gamemode, the player clicks circles to the beat of a song. This is the flagship gamemode featured on the osu! website.[9] \"osu!mania\" is a vertical scrolling rhythm game that mostly takes inspiration from Beatmania and Stepmania. The gamemode consists of notes that fall vertically in different lanes, with one key used to tap for each lane.[10] \"osu!taiko\" simulates playing on taiko and is based on Taiko no Tatsujin.[11] The final gamemode, osu!catch, is based on \"EZ2CATCH\", which was part of the EZ2DJ cabinet. In this gamemode, the player moves a catcher left or right in order to catch fruits falling from the top of the screen.[9][better source needed]\r\n\r\nEach mode offers a variety of \"beatmaps\", which are game levels that are played to songs of different lengths, ranging from \"TV size\" anime openings to \"marathons\" surpassing 7 minutes. In osu!standard, beatmaps consist of three items \u2013 hit circles, sliders, and spinners. The objective of the game is for the player to click on these items in time to the music. These items are collectively known as \"hit objects\" or \"circles\" and are arranged in different positions on the screen (except for the spinner) at different points of time during a song. Taiko beatmaps have drumbeats and spinners. Catch beatmaps have fruits and spinners (which are bananas), which are arranged in a horizontally falling manner. Mania beatmaps consist of keys (depicted as a small bar) and holds. The beatmap is then played with accompanying music, simulating a sense of rhythm as the player interacts with the objects to the beat of the music.[12][13] Each beatmap is accompanied by music and a background (which can be disabled). The game can be played using various peripherals: the most common setup is a graphics tablet or computer mouse to control cursor movement, paired with a keyboard[14][5] or a mini keyboard with only two keys, and only the keyboard for osu!taiko, osu!catch, and osu!mania beatmaps.\r\n\r\nThe game offers a buyable extension service called osu!supporter, which grants extra features to the user.[15] osu!supporter does not affect the ranking system or provide any in-game advantage. While osu!supporter itself is not a recurring service (meaning it is a one-off payment), it has a limited time validity ranging from 1 month to 2 years; however, multiple purchases of osu!supporter service time can be entitled to one user, allowing for longer uninterrupted service.",
  "reference": [
    " \"Lazer 2024.130.2 \u00B7 changelog\". osu!. Retrieved 3 February 2024.",
    " \"a long-overdue update\". ppy blog. 30 June 2016. Archived from the original on 8 November 2020. Retrieved 20 August 2021. Until now we used some XNA code for input handling and low-level structs. These dependencies are almost compeletely [sic] removed from the project now, with OpenTK or similar open-source frameworks replacing them.",
    " \"GitHub - ppy\/osu-resources: assets used by osu!\". GitHub. Retrieved 19 January 2023.",
    " \"Osu!'s programming language?\". osu! Community Forum. 31 December 2016. Retrieved 27 July 2021."
  ]
}
```

*Response*

```json
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Thu, 11 Apr 2024 16:50:10 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
  "articleId": "21eae2d9-1a59-48a1-9d20-5e25d7349003",
  "title": "Osu!",
  "content": "Osu![a] (stylized as osu!) is a free-to-play rhythm game primarily developed, published, and created by Dean \"peppy\" Herbert. Inspired by iNiS' rhythm game Osu! Tatakae! Ouendan, it was written in C# on the .NET Framework,[4] and was released for Microsoft Windows on 16 September 2007. The game has throughout the years been ported to macOS, Linux, Android and iOS.\r\n\r\nAside from Osu! Tatakae! Ouendan, the game has been inspired by titles such as Taiko no Tatsujin, Beatmania IIDX,[5] EZ2DJ (EZ2CATCH), Elite Beat Agents, O2Jam, StepMania, and DJMax. All \"beatmaps\" in the game are community-made through the in-game map editor or through external tools. Four different game modes exist, offering various ways to play a beatmap. These modes can also be combined with optional modifiers, which can increase or decrease the difficulty.\r\n\r\nGameplay and features\r\nThere are four official game modes: \"osu!\" (called \"osu! standard\"), \"osu!taiko\", \"osu!catch\", and \"osu!mania\". With the addition of osu!lazer, players can now add custom gamemodes to the osu! client.[6][7] The original osu!standard mode remains the most popular to date and as of January 2023, the game has over 19.3 million monthly active users according to the game's global country leaderboards.[8]\r\n\r\n\"osu!standard\" takes direct inspiration from Osu! Tatakae! Ouendan games. In this gamemode, the player clicks circles to the beat of a song. This is the flagship gamemode featured on the osu! website.[9] \"osu!mania\" is a vertical scrolling rhythm game that mostly takes inspiration from Beatmania and Stepmania. The gamemode consists of notes that fall vertically in different lanes, with one key used to tap for each lane.[10] \"osu!taiko\" simulates playing on taiko and is based on Taiko no Tatsujin.[11] The final gamemode, osu!catch, is based on \"EZ2CATCH\", which was part of the EZ2DJ cabinet. In this gamemode, the player moves a catcher left or right in order to catch fruits falling from the top of the screen.[9][better source needed]\r\n\r\nEach mode offers a variety of \"beatmaps\", which are game levels that are played to songs of different lengths, ranging from \"TV size\" anime openings to \"marathons\" surpassing 7 minutes. In osu!standard, beatmaps consist of three items – hit circles, sliders, and spinners. The objective of the game is for the player to click on these items in time to the music. These items are collectively known as \"hit objects\" or \"circles\" and are arranged in different positions on the screen (except for the spinner) at different points of time during a song. Taiko beatmaps have drumbeats and spinners. Catch beatmaps have fruits and spinners (which are bananas), which are arranged in a horizontally falling manner. Mania beatmaps consist of keys (depicted as a small bar) and holds. The beatmap is then played with accompanying music, simulating a sense of rhythm as the player interacts with the objects to the beat of the music.[12][13] Each beatmap is accompanied by music and a background (which can be disabled). The game can be played using various peripherals: the most common setup is a graphics tablet or computer mouse to control cursor movement, paired with a keyboard[14][5] or a mini keyboard with only two keys, and only the keyboard for osu!taiko, osu!catch, and osu!mania beatmaps.\r\n\r\nThe game offers a buyable extension service called osu!supporter, which grants extra features to the user.[15] osu!supporter does not affect the ranking system or provide any in-game advantage. While osu!supporter itself is not a recurring service (meaning it is a one-off payment), it has a limited time validity ranging from 1 month to 2 years; however, multiple purchases of osu!supporter service time can be entitled to one user, allowing for longer uninterrupted service.",
  "reference": [
    " \"Lazer 2024.130.2 · changelog\". osu!. Retrieved 3 February 2024.",
    " \"a long-overdue update\". ppy blog. 30 June 2016. Archived from the original on 8 November 2020. Retrieved 20 August 2021. Until now we used some XNA code for input handling and low-level structs. These dependencies are almost compeletely [sic] removed from the project now, with OpenTK or similar open-source frameworks replacing them.",
    " \"GitHub - ppy/osu-resources: assets used by osu!\". GitHub. Retrieved 19 January 2023.",
    " \"Osu!'s programming language?\". osu! Community Forum. 31 December 2016. Retrieved 27 July 2021."
  ],
  "articleAuthor": "Sample Member 1",
  "createdDateTime": "2024-04-12T00:50:10.9846434+08:00"
}

```

**Update Article**

*Request*

```http
@host=http://localhost:5138
@token=Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI5ZWJmYTRkZS05NTI3LTQ4YjgtYmEwNS00NGI0MTc3MTg0ZjMiLCJqdGkiOiIwMTdjYjU5OS04MWE3LTRjNzYtYjI3NC0yMjMyYTY2NjM5ZDYiLCJyb2xlcyI6Ik1lbWJlciIsInBlcm1pc3Npb25zIjpbIlJlYWQiLCJVcGRhdGUiLCJEZWxldGUiLCJJbnNlcnQiXSwiZXhwIjoxNzEyODU3NTc4LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUxMzgvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MTM4LyJ9.JEBPYVXvHs5xTtUCgiDHEPGtBeTWc0NzYmqTTZ6GKGI

POST {{host}}/article/update
Content-Type: application/json
Authorization: {{token}}

{
  "articleId": "21eae2d9-1a59-48a1-9d20-5e25d7349003",
  "title": "Osu! Update!",
  "content": "Osu![a] (stylized as osu!) is a free-to-play rhythm game primarily developed, published, and created by Dean \"peppy\" Herbert. Inspired by iNiS' rhythm game Osu! Tatakae! Ouendan, it was written in C# on the .NET Framework,[4] and was released for Microsoft Windows on 16 September 2007. The game has throughout the years been ported to macOS, Linux, Android and iOS.\r\n\r\nAside from Osu! Tatakae! Ouendan, the game has been inspired by titles such as Taiko no Tatsujin, Beatmania IIDX,[5] EZ2DJ (EZ2CATCH), Elite Beat Agents, O2Jam, StepMania, and DJMax. All \"beatmaps\" in the game are community-made through the in-game map editor or through external tools. Four different game modes exist, offering various ways to play a beatmap. These modes can also be combined with optional modifiers, which can increase or decrease the difficulty.\r\n\r\nGameplay and features\r\nThere are four official game modes: \"osu!\" (called \"osu! standard\"), \"osu!taiko\", \"osu!catch\", and \"osu!mania\". With the addition of osu!lazer, players can now add custom gamemodes to the osu! client.[6][7] The original osu!standard mode remains the most popular to date and as of January 2023, the game has over 19.3 million monthly active users according to the game's global country leaderboards.[8]\r\n\r\n\"osu!standard\" takes direct inspiration from Osu! Tatakae! Ouendan games. In this gamemode, the player clicks circles to the beat of a song. This is the flagship gamemode featured on the osu! website.[9] \"osu!mania\" is a vertical scrolling rhythm game that mostly takes inspiration from Beatmania and Stepmania. The gamemode consists of notes that fall vertically in different lanes, with one key used to tap for each lane.[10] \"osu!taiko\" simulates playing on taiko and is based on Taiko no Tatsujin.[11] The final gamemode, osu!catch, is based on \"EZ2CATCH\", which was part of the EZ2DJ cabinet. In this gamemode, the player moves a catcher left or right in order to catch fruits falling from the top of the screen.[9][better source needed]\r\n\r\nEach mode offers a variety of \"beatmaps\", which are game levels that are played to songs of different lengths, ranging from \"TV size\" anime openings to \"marathons\" surpassing 7 minutes. In osu!standard, beatmaps consist of three items \u2013 hit circles, sliders, and spinners. The objective of the game is for the player to click on these items in time to the music. These items are collectively known as \"hit objects\" or \"circles\" and are arranged in different positions on the screen (except for the spinner) at different points of time during a song. Taiko beatmaps have drumbeats and spinners. Catch beatmaps have fruits and spinners (which are bananas), which are arranged in a horizontally falling manner. Mania beatmaps consist of keys (depicted as a small bar) and holds. The beatmap is then played with accompanying music, simulating a sense of rhythm as the player interacts with the objects to the beat of the music.[12][13] Each beatmap is accompanied by music and a background (which can be disabled). The game can be played using various peripherals: the most common setup is a graphics tablet or computer mouse to control cursor movement, paired with a keyboard[14][5] or a mini keyboard with only two keys, and only the keyboard for osu!taiko, osu!catch, and osu!mania beatmaps.\r\n\r\nThe game offers a buyable extension service called osu!supporter, which grants extra features to the user.[15] osu!supporter does not affect the ranking system or provide any in-game advantage. While osu!supporter itself is not a recurring service (meaning it is a one-off payment), it has a limited time validity ranging from 1 month to 2 years; however, multiple purchases of osu!supporter service time can be entitled to one user, allowing for longer uninterrupted service.",
  "reference": [
    " \"Lazer 2024.130.2 \u00B7 changelog\". osu!. Retrieved 3 February 2024.",
    " \"a long-overdue update\". ppy blog. 30 June 2016. Archived from the original on 8 November 2020. Retrieved 20 August 2021. Until now we used some XNA code for input handling and low-level structs. These dependencies are almost compeletely [sic] removed from the project now, with OpenTK or similar open-source frameworks replacing them.",
    " \"GitHub - ppy\/osu-resources: assets used by osu!\". GitHub. Retrieved 19 January 2023.",
    " \"Osu!'s programming language?\". osu! Community Forum. 31 December 2016. Retrieved 27 July 2021."
  ]
}
```

*Response*

```json
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Thu, 11 Apr 2024 16:53:01 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
  "updateId": "094a40dd-386a-4f83-90c2-0ff0020fb197",
  "title": "Osu! Update!",
  "content": "Osu![a] (stylized as osu!) is a free-to-play rhythm game primarily developed, published, and created by Dean \"peppy\" Herbert. Inspired by iNiS' rhythm game Osu! Tatakae! Ouendan, it was written in C# on the .NET Framework,[4] and was released for Microsoft Windows on 16 September 2007. The game has throughout the years been ported to macOS, Linux, Android and iOS.\r\n\r\nAside from Osu! Tatakae! Ouendan, the game has been inspired by titles such as Taiko no Tatsujin, Beatmania IIDX,[5] EZ2DJ (EZ2CATCH), Elite Beat Agents, O2Jam, StepMania, and DJMax. All \"beatmaps\" in the game are community-made through the in-game map editor or through external tools. Four different game modes exist, offering various ways to play a beatmap. These modes can also be combined with optional modifiers, which can increase or decrease the difficulty.\r\n\r\nGameplay and features\r\nThere are four official game modes: \"osu!\" (called \"osu! standard\"), \"osu!taiko\", \"osu!catch\", and \"osu!mania\". With the addition of osu!lazer, players can now add custom gamemodes to the osu! client.[6][7] The original osu!standard mode remains the most popular to date and as of January 2023, the game has over 19.3 million monthly active users according to the game's global country leaderboards.[8]\r\n\r\n\"osu!standard\" takes direct inspiration from Osu! Tatakae! Ouendan games. In this gamemode, the player clicks circles to the beat of a song. This is the flagship gamemode featured on the osu! website.[9] \"osu!mania\" is a vertical scrolling rhythm game that mostly takes inspiration from Beatmania and Stepmania. The gamemode consists of notes that fall vertically in different lanes, with one key used to tap for each lane.[10] \"osu!taiko\" simulates playing on taiko and is based on Taiko no Tatsujin.[11] The final gamemode, osu!catch, is based on \"EZ2CATCH\", which was part of the EZ2DJ cabinet. In this gamemode, the player moves a catcher left or right in order to catch fruits falling from the top of the screen.[9][better source needed]\r\n\r\nEach mode offers a variety of \"beatmaps\", which are game levels that are played to songs of different lengths, ranging from \"TV size\" anime openings to \"marathons\" surpassing 7 minutes. In osu!standard, beatmaps consist of three items – hit circles, sliders, and spinners. The objective of the game is for the player to click on these items in time to the music. These items are collectively known as \"hit objects\" or \"circles\" and are arranged in different positions on the screen (except for the spinner) at different points of time during a song. Taiko beatmaps have drumbeats and spinners. Catch beatmaps have fruits and spinners (which are bananas), which are arranged in a horizontally falling manner. Mania beatmaps consist of keys (depicted as a small bar) and holds. The beatmap is then played with accompanying music, simulating a sense of rhythm as the player interacts with the objects to the beat of the music.[12][13] Each beatmap is accompanied by music and a background (which can be disabled). The game can be played using various peripherals: the most common setup is a graphics tablet or computer mouse to control cursor movement, paired with a keyboard[14][5] or a mini keyboard with only two keys, and only the keyboard for osu!taiko, osu!catch, and osu!mania beatmaps.\r\n\r\nThe game offers a buyable extension service called osu!supporter, which grants extra features to the user.[15] osu!supporter does not affect the ranking system or provide any in-game advantage. While osu!supporter itself is not a recurring service (meaning it is a one-off payment), it has a limited time validity ranging from 1 month to 2 years; however, multiple purchases of osu!supporter service time can be entitled to one user, allowing for longer uninterrupted service.",
  "reference": [
    " \"Lazer 2024.130.2 · changelog\". osu!. Retrieved 3 February 2024.",
    " \"a long-overdue update\". ppy blog. 30 June 2016. Archived from the original on 8 November 2020. Retrieved 20 August 2021. Until now we used some XNA code for input handling and low-level structs. These dependencies are almost compeletely [sic] removed from the project now, with OpenTK or similar open-source frameworks replacing them.",
    " \"GitHub - ppy/osu-resources: assets used by osu!\". GitHub. Retrieved 19 January 2023.",
    " \"Osu!'s programming language?\". osu! Community Forum. 31 December 2016. Retrieved 27 July 2021."
  ],
  "updateOwner": "Sample Member 1",
  "fromThisArticle": {
    "id": "21eae2d9-1a59-48a1-9d20-5e25d7349003",
    "title": "Osu!"
  },
  "updatedDateTime": "2024-04-12T00:53:01.9193301+08:00"
}
```

## License

OpenWikiApi is licensed under the MIT License. See [LICENSE](LICENSE).
