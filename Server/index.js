var portCustom = 5200;
var server = require("socket.io")(process.env.PORT || portCustom);

// classes

var Player = require("./class/player.js");
var Food = require("./class/food.js");
var countPlayersClass = require("./class/countPlayersClass.js");
var message = require("./class/message.js");
var clients = [];
var sockets = [];
var foods = [];
var messagesPlayers = [];
var count_clients = 0;
var pl = null;
var food = null;
var ms = null;

console.log("started server!");

food = new Food();
ms = new message();
countPlayersData = new countPlayersClass();


server.on("connection", function(socket) {
	count_clients = count_clients + 1;
	countPlayersData.count = count_clients.toString();
	var pl = new Player();
clients[pl.id] = pl;
sockets[pl.id] = socket;
var idPlayer = pl.id;
socket.emit("GetID", {id: idPlayer, name: pl.name});
socket.emit("GetCountPlayersFromServer", countPlayersData);
socket.broadcast.emit("GetCountPlayersFromServer", countPlayersData);
console.log("New player: ID: " + clients[pl.id].id);
var master = false;
      socket.broadcast.emit("NewPlayerConnect", pl);
for (playerID in  clients) {
	if (playerID != idPlayer) {
		socket.emit("create", clients[playerID]);
		socket.emit("SetColorPlayer", clients[playerID]);
		socket.emit("SetNickName", clients[playerID]);
		socket.emit("SetScorePlayer", clients[playerID]);
	//	console.log("Event 120");
	}

	for (foodID in foods) {
		socket.emit("spawnFood", foods[foodID]);
		//	console.log("Event 121");
	}

   for (messageID in messagesPlayers) {
      socket.emit("SendMessage", messagesPlayers[messageID]);
      // console.log("Event 122");
   }
}
socket.on("updatePosition", function(data) {
      pl.position.x = data.position.x;
      pl.position.y = data.position.y;
      pl.scale.x = data.scale.x;
      pl.scale.y = data.scale.y;
      pl.scale.z = data.scale.z;
 //    console.log("Player (" + pl.id + ")" + " uploaded vector2! Data: X: " + data.position.x + " Y: " + data.position.y);

      
socket.broadcast.emit("updatePosition", pl);
   });

socket.on("SendMessage", function(data) {
   ms = new message();
      ms.dataMessage = data.dataMessage;
      messagesPlayers[ms.id] = ms;
    console.log(ms.dataMessage);
    socket.emit("SendMessage", ms);
      socket.broadcast.emit("SendMessage", ms);
   });

socket.on("spawnFood", function(data) {
	food = new Food();
      food.position.x = data.position.x;
      food.position.y = data.position.y;
      food.scale.x = data.scale.x;
      food.scale.y = data.scale.y;
      food.scale.z = data.scale.z;
      food.color.r = data.color.r;
      food.color.g = data.color.g;
      food.color.b = data.color.b;
      foods[food.id] = food;
      socket.emit("spawnFood", food);
      socket.broadcast.emit("spawnFood", food);
   });

socket.on("destroyFood", function(data) {
	delete foods[data.id];
//	console.log("Food (ID " + data.id + ") destroyed.");
	socket.emit("destroyFood", data);
      socket.broadcast.emit("destroyFood", data);
   });

socket.on("SetNickName", function(data) {
      pl.name = data.name;
    console.log("Player (" + pl.id + ")" + " uploaded NickName: " + data.name);
    socket.emit("SetNickName", pl);
      socket.broadcast.emit("SetNickName", pl);
   });



socket.on("SetColorPlayer", function(data) {
      pl.color = data.color;
      socket.broadcast.emit("SetColorPlayer", pl);
   });

socket.on("SetScorePlayer", function(data) {
      pl.score = data.score;
      socket.emit("SetScorePlayer", pl);
      socket.broadcast.emit("SetScorePlayer", pl);
   });
socket.on("createEvent2", function(data) {
      socket.broadcast.emit("create", pl);
      socket.emit("create", pl);

   });

socket.on("FoodPlayer", function(data) {
      socket.broadcast.emit("destroy", data);
      socket.emit("destroy", data);
   });



socket.on("NewPlayerConnect", function(data) {
     console.log("Event 113");
   });
socket.on("disconnect", function() {
	console.log("Player disconnected: his ID: " + clients[idPlayer].id);
	socket.broadcast.emit("destroy", pl);
	socket.broadcast.emit("PlayerDisconnected", pl);
	count_clients = count_clients - 1;
	countPlayersData.count = count_clients.toString();
	socket.emit("GetCountPlayersFromServer", countPlayersData);
socket.broadcast.emit("GetCountPlayersFromServer", countPlayersData);
	if (count_clients < 1) {
		foods = [];
      messagesPlayers = [];
		console.log("Foods and messages deleted");
	}
delete clients[idPlayer];		
delete sockets[idPlayer];
});
});