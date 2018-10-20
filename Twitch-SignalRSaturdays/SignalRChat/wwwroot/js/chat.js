"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

function ClearTyping() {
    var root = document.getElementById("typingList");
    while (root.firstChild) {
        root.removeChild(root.firstChild);
    }
}

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    ClearTyping();
});

connection.on("TypingMessage", function (user) {

    ClearTyping();
    var li = document.createElement("li");
    var encodedMsg = user + " is typing...";
    li.textContent = encodedMsg;
    document.getElementById("typingList").appendChild(li);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("messageInput").addEventListener("input", function (evt) {
    var user = document.getElementById("userInput").value;
    connection.invoke("Typing", user).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    document.getElementById("messageInput").value = "";
    ClearTyping();
});