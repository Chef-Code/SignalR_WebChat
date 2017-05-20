# SignalR_WebChat

Although the name of the Repository says otherwise, this is not a webchat repository... It's more that that! Currently this repository represents a real time multiplayer game of pinochle.

###Client
[Quick Link](SignalR_WebChat/Views/Game/JoinGameTable.cshtml) to the main view which is currently under construction!

###Server
[GameHub Link](SignalR_WebChat/GameHub.cs) which is the hub that talks to the (client) main view above. oddly named: JoinGameTable.cshtml

Which uses [GameState Link](SignalR_WebChat/GameState.cs) to keep a singleton of the current game state.
