"use strict";
//window.alert("You are matched with " + getParameterByName("matchedWith") + ".\r\n You are the " + String(getParameterByName("messageReceived")));

if ("responder".localeCompare(String(getParameterByName("messageReceived"))) == 0) {
    document.getElementById("question-portal").innerHTML = "<p>Waiting for " + getParameterByName("matchedWith") + " to ask the problem";
}

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

    document.getElementById("SubmitRegex").addEventListener("click", function (event) {
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
        var message = "sampleMessage";
        var matchStrings = [document.getElementById("match1").value, document.getElementById("match2").value];
        var nomatchStrings = [document.getElementById("nomatch1").value, document.getElementById("nomatch2").value];
        var requestJson = new Object();
        requestJson.matchString = matchStrings;
        requestJson.noMatchString = nomatchStrings;

        var connection = JSON.parse(unescape(readCookie("connection")));
        new signalR.HubConnection(connection).invoke("SendMessage", "", requestJson).catch(function (err) {

            return console.error(err.toString());
        });
        event.preventDefault();
});

