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
    var msg = JSON.parse(message);
    var msgtype = msg.type;
    if (msgtype == "queue") {
        connection.stop();
        window.location.href = "Home/GameScreen?matchedWith=" + user + "&messageReceived=" + msg.playerType;
    }
    else if (msgtype == "NameApproval") {
        if (msg.approval == "true") {
            document.getElementById("challengeAccept").style.display = "block";
            document.getElementById("challengeAccept").innerText = "Waiting for someone to accept the challenge";

            var buttons = document.getElementsByTagName("button");
            var btn;
            for (btn = 1; btn <= buttons.length;btn++) {
                buttons[btn].style.display = "none";;
            }
        }
        else {
            document.getElementById("challengeAccept").style.display = "block";
            document.getElementById("challengeAccept").innerText = "This name is taken. Please select some other name";
        }
    }
    
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
