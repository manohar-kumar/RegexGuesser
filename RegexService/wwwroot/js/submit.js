"use strict";

if ("responder".localeCompare(String(getParameterByName("messageReceived"))) == 0) {
    document.getElementById("question-portal").innerHTML = "<p>Waiting for " + getParameterByName("matchedWith") + " to ask the problem";
}
else {
    document.getElementById("question-display").style.display = "none";
}

document.getElementById("question-display").style.visibility = "hidden";
var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

function validateForm() {
    var strval = document.getElementById("RegexString").value;
    if (strval == "") {
        window.alert("Please enter a regex string");
        throw new error("No regex string entered");
    }
    var unorderListMatchString = document.getElementById("matchStrings");
    var totalStringsYet = unorderListMatchString.children.length;

    for (var i = 1; i <= totalStringsYet; i++) {
        if (document.getElementById("match" + String(i)).value == "") {
            window.alert("Please enter a string which matches the regex");
            throw new error("A string which matches the regex has not been entered");
        }
        if (document.getElementById("nomatch" + String(i)).value == "") {
            window.alert("Please enter a string which does not match the regex");
            throw new error("A string which does not match the regex has not been entered");
        }
    }

    var regex;
    try {
        regex = new RegExp(strval);
    }
    catch (err) {
        window.alert("Bad Regex, " + err);
    }

    for (var i = 1; i <= totalStringsYet; i++) {
        var m = String(document.getElementById("match" + String(i)).value);
        var um = String(document.getElementById("nomatch" + String(i)).value);

        if (m.search(strval) == -1) {
            alert("Search string " + i + " does not match with regex");
            throw new error("Hint string should match with regex");
        }

        if (um.search(strval) != -1) {
            alert("One of the strings should not match with regex but matches");
            throw new error("One of the strings should not match with regex but matches");
        }
    }
}

connection.on("ReceiveMessage", function (user, message) {
    var msg = JSON.parse(message);
    var msgtype = msg.type;
    if (msgtype == "query") {
        var matcherStrings = msg.matchString;
        var nomatcherStrings = msg.noMatchString;
        var regexStr = String(msg.regexString);
        if (window.regexString && regexStr.localeCompare(regexString) != 0) {
            document.getElementById("matcherStrings").innerHTML = "";
            document.getElementById("nomatcherStrings").innerHTML = "";
        }

        window.regexString = regexStr;
        var unorderListMatchString = document.getElementById("matcherStrings");
        var unorderListnoMatchString = document.getElementById("nomatcherStrings");
        var totalStringsYet = unorderListMatchString.children.length;

        for (var i = 1; i <= matcherStrings.length; i++) {
            if (i > totalStringsYet) {
                var li = document.createElement("li");
                var x = document.createTextNode(matcherStrings[i - 1]);
                x.id = "matcher" + String(totalStringsYet + 1);
                li.appendChild(x);
                unorderListMatchString.appendChild(li);

                li = document.createElement("li");
                x = document.createTextNode(nomatcherStrings[i - 1]);
                x.id = "nomatcher" + String(totalStringsYet + 1);
                li.appendChild(x);
                unorderListnoMatchString.appendChild(li);
            }
        }

        document.getElementById("Result").style.display = "none";
        document.getElementById("Hint").style.display = "block";
        document.getElementById("question-display").style.visibility = "visible";
        document.getElementById("question-portal").style.display = "none";
    }
    else if (msgtype == "hint") {
        alert("Player " + user + " needs more strings to guess");
        var unorderListMatchString = document.getElementById("matchStrings");
        var totalStringsYet = unorderListMatchString.children.length;
        var li = document.createElement("li");
        var x = document.createElement("INPUT");
        x.setAttribute("id", "match" + String(totalStringsYet + 1))
        x.setAttribute("type", "text");
        li.appendChild(x);
        unorderListMatchString.appendChild(li);

        var unorderListnoMatchString = document.getElementById("nomatchStrings");
        li = document.createElement("li");
        x = document.createElement("INPUT");
        x.setAttribute("id", "nomatch" + String(totalStringsYet + 1))
        x.setAttribute("type", "text");
        li.appendChild(x);
        unorderListnoMatchString.appendChild(li);
    }
    else if (msgtype == "endQuestion") {
        if (msg.outcome == "solved") {
            alert("Player " + user + " has guessed the string correctly. Please reset and ask another question or Click on home to queue again.")
        }
        else if (msg.outcome == "giveUp") {
            alert("Player " + user + " has given up. Please reset and ask another question or Click on home to queue again.")
        }
    }
    else if (msgtype == "exception") {
        alert(msg.error);
        throw new error(msg.error);
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
        var unorderListMatchString = document.getElementById("matcherStrings");
        var point = Math.round(70 - 10 * Math.pow(unorderListMatchString.children.length, 1.2));
        document.getElementById("Result").style.display = "block";
        document.getElementById("Hint").style.display = "none";
        document.getElementById("Points").style.display = "block";
        document.getElementById("Points").innerText = "You win " + String(point) + " points";
        document.getElementById("Winner").innerText = "You Guessed it Right.";
        var answered = new Object();
        answered.type = "endQuestion";
        answered.outcome = "solved";
        connection.invoke("SendMessage", userId, JSON.stringify(answered)).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        document.getElementById("Result").style.display = "block";
        document.getElementById("Points").style.display = "None";
        document.getElementById("Hint").style.display = "block";
        document.getElementById("Winner").innerText = "You are wrong. Try again or ask for more strings.";
    }
});

document.getElementById("HintBtn").addEventListener("click", function (event) {
    var askhint = new Object();
    askhint.type = "hint";
    connection.invoke("SendMessage", userId, JSON.stringify(askhint)).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("GiveUp").addEventListener("click", function (event) {
    document.getElementById("Result").style.display = "block";
    document.getElementById("Hint").style.display = "none";
    document.getElementById("Winner").innerText = "Too tough eh!. The answer was '"+window.regexString + "'. \r\n Please wait for another question or go to home to queue again.";
    var answered = new Object();
    answered.type = "endQuestion";
    answered.outcome = "giveUp";
    connection.invoke("SendMessage", userId, JSON.stringify(answered)).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("Reset").addEventListener("click", function (event) {
    document.getElementById("RegexString").value = "";
    var unorderListMatchString = document.getElementById("matchStrings");
    var totalStringsYet = unorderListMatchString.children.length;
    var unorderListnoMatchString = document.getElementById("nomatchStrings");

    for (var i = 1; i <= totalStringsYet; i++) {
        document.getElementById("match" + String(i)).value = "";
        document.getElementById("nomatch" + String(i)).value = "";
        if (i > 2) {
            unorderListMatchString.removeChild(unorderListMatchString.children[i - 1]);
            unorderListnoMatchString.removeChild(unorderListnoMatchString.children[i - 1]);
        }
    }
});

document.getElementById("SubmitRegex").addEventListener("click", function (event) {
    var strval = document.getElementById("RegexString").value;
    validateForm();
    var unorderListMatchString = document.getElementById("matchStrings");
    var totalStringsYet = unorderListMatchString.children.length;

    var matchStrings = new Array();
    var nomatchStrings = new Array();
    for (var i = 1; i <= totalStringsYet; i++) {
        matchStrings.push(document.getElementById("match" + String(i)).value);
        nomatchStrings.push(document.getElementById("nomatch" + String(i)).value);
    }

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

