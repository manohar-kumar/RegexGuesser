"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();
createCookie("connection", JSON.stringify(connection));
function createCookie(key, value) {
    let cookie = escape(key) + "=" + escape(value) + ";";
    document.cookie = cookie;
    console.log(cookie);
    console.log("Creating new cookie with key: " + key + " value: " + value);
}

console.log()
//Disable send button until connection is established
if (document.getElementById("queueButton")) {
    document.getElementById("queueButton").disabled = true;
}

connection.on("ReceiveMessage", function (user, message) {
    window.matchedWith = user;
    window.messageReceived = message;
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
    window.user = user;
    var message = "sampleMessage";
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
}
