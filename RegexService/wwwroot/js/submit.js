"use strict";
//window.alert("You are matched with " + getParameterByName("matchedWith") + ".\r\n You are the " + String(getParameterByName("messageReceived")));

if ("responder".localeCompare(String(getParameterByName("messageReceived"))) == 0) {
    document.getElementById("question-portal").innerHTML = "<p>Waiting for " + getParameterByName("matchedWith") + " to ask the problem";
}

document.getElementById("question-display").style.visibility = "hidden";
var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

function validateForm() {
    var strval = document.getElementById("RegexString").value;
    if (strval == "") {
        window.alert("Please enter a regex string");
    }
    if (document.getElementById("match1").value == "") {
        window.alert("Please enter a string which matches the regex");
    }
    if (document.getElementById("match2").value == "") {
        window.alert("Please enter a string which matches the regex");
    }
    if (document.getElementById("nomatch1").value == "") {
        window.alert("Please enter a string which does not match the regex");
    }
    if (document.getElementById("nomatch2").value == "") {
        window.alert("Please enter a string which does not match the regex");
    }

    var regex;
    try {
        regex = new RegExp(strval);
    }
    catch (err) {
        window.alert("Bad Regex, " + err);
    }
    var m1 = String(document.getElementById("match1").value);
    var m2 = String(document.getElementById("match2").value);
    var um1 = String(document.getElementById("nomatch1").value);
    var um2 = String(document.getElementById("nomatch2").value);
    if (m1.search(strval) == -1) {
        alert("Search string 1 does not match with regex");
    }
    if (m2.search(strval) == -1) {
        alert("Search string 2 does not match with regex");
    }
    if (um1.search(strval) != -1) {
        alert("One of the strings should not match with regex but matches");
    }
    if (um2.search(strval) != -1) {
        alert("One of the strings should not match with regex but matches");
    }
}

connection.on("ReceiveMessage", function (user, message) {
    var msg = JSON.parse(message);
    var msgtype = msg.type;
    if (msgtype == "query") {
        var matcherStrings = msg.matchString;
        var nomatcherStrings = msg.noMatchString;
        window.regexString = String(msg.regexString);
        document.getElementById("matcher1").innerText = matcherStrings[0];
        document.getElementById("matcher2").innerText = matcherStrings[1];
        document.getElementById("nomatcher1").innerText = nomatcherStrings[0];
        document.getElementById("nomatcher2").innerText = nomatcherStrings[1];
        document.getElementById("question-display").style.visibility = "visible";
        document.getElementById("question-portal").style.display = "none";
    }
    else if (msgtype == "hint") {
        alert("Player " + user + " needs more strings to guess");
    }
});

connection.start().then(function () {
    var pingmsg = new Object();
    pingmsg.type = "ping";
    window.userId = readCookie("user");

    connection.invoke("SendMessage", userId, JSON.stringify(pingmsg)).catch(function (err) {

        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});



function readCookie(name) {
    let key = name + "=";
    let cookies = document.cookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        let cookie = cookies[i];
        while (cookie.charAt(0) === ' ') {
            cookie = cookie.substring(1, cookie.length);
        }
        if (cookie.indexOf(key) === 0) {
            return cookie.substring(key.length, cookie.length);
        }
    }
    return null;
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
document.getElementById("SubmitAnswer").addEventListener("click", function (event) {
    var guess = document.getElementById("RegexAnswer").value;
    if (regexString.localeCompare(guess) == 0) {
        alert("Correct Answer");
    }
    else {
        alert("wrong Answer");
    }
});

document.getElementById("Hint").addEventListener("click", function (event) {
    var askhint = new Object();
    askhint.type = "hint";
    connection.invoke("SendMessage", userId, JSON.stringify(askhint)).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("SubmitRegex").addEventListener("click", function (event) {
    var strval = document.getElementById("RegexString").value;
    validateForm();
    var matchStrings = [document.getElementById("match1").value, document.getElementById("match2").value];
    var nomatchStrings = [document.getElementById("nomatch1").value, document.getElementById("nomatch2").value];
    var requestJson = new Object();
    requestJson.regexString = strval;
    requestJson.matchString = matchStrings;
    requestJson.noMatchString = nomatchStrings;
    requestJson.type = "query";
    connection.invoke("SendMessage", userId, JSON.stringify(requestJson)).catch(function (err) {

        return console.error(err.toString());
    });
    event.preventDefault();
});

