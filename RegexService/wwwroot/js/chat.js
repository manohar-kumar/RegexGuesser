"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

function createCookie(key, value) {
    let cookie = escape(key) + "=" + escape(value) + ";";
    document.cookie = cookie;
    console.log(cookie);
    console.log("Creating new cookie with key: " + key + " value: " + value);
}

//Disable send button until connection is established
document.getElementById("queueButton").disabled = true;


connection.on("ReceiveMessage", function (user, message) {
    connection.stop();
    window.location.href = "Home/GameScreen?matchedWith=" + user + "&messageReceived=" + message;
});

connection.start().then(function () {
    document.getElementById("queueButton").disabled = false;
}).catch(function (err) {
        return console.error(err.toString());
    });

if (document.getElementById("queueButton")) {
    document.getElementById("queueButton").addEventListener("click", function (event) {
        var user = document.getElementById("playerName").value;
        createCookie("user", user);
        var message = new Object();
        message.type = "queue";
        connection.invoke("SendMessage", user, JSON.stringify(message)).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
}
