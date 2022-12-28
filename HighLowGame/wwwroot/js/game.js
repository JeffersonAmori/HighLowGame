"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();
var gameMasterUser = "Hi-Lo-GameMaster-User-62554875";

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("WriteToPage", function (user, message) {
    var currentUser = document.getElementById("userInput").value;

    var p = document.createElement("p");

    switch (user) {
        case currentUser:
            p.className = "userMessage";
            break;
        case gameMasterUser:
            p.className = "gameMessage";
            break;
        default:
            p.className = "otherUserMessage";
            break;
    }

    var messageList = document.getElementById("messagesList");
    messageList.appendChild(p);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    p.textContent = message;

    messageList.scrollTop = messageList.scrollHeight;
});

connection.on("Celebrate", function (user) {
    const currentUser = document.getElementById("userInput").value;
    if (user != currentUser)
        return;

    celebrate();
});

connection.on("UpdateEngine", function (newEngine) {
    document.getElementById("enginesList").value = newEngine;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("Guess", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function onEnginesListChange() {
    var selectedEngine = document.getElementById("enginesList").value;
    var user = document.getElementById("userInput").value;
    connection.invoke("EngineChanged", user, selectedEngine).catch(function (err) {
        return console.error(err.toString());
    });
}

function celebrate() {
    party.confetti(document.getElementsByTagName("Body")[0], {
        count: party.variation.range(50, 60),
        spread: party.variation.range(45, 55),
        size: party.variation.range(1.3, 1.45)
    });
}