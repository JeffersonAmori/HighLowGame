"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("WriteToPage", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = message;
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
        count: party.variation.range(20, 40),
    });
}
