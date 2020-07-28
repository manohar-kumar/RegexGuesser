"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

//Disable send button until connection is established
document.getElementById("queueButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    window.location.href = "Home/GameScreen";
});

connection.start().then(function () {
    document.getElementById("queueButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("queueButton").addEventListener("click", function (event) {
    var user = document.getElementById("playerName").value;
    var message = "sampleMessage";
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});