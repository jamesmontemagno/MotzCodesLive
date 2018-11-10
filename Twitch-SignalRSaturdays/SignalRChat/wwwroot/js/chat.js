"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

function ClearTyping() {
    var root = document.getElementById("typingList");
    while (root.firstChild) {
        root.removeChild(root.firstChild);
    }
}

function AddTypingMessage(message) {
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("typingList").appendChild(li);
}

function CleanMessage(message) {
    return message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
}

function AddMessage(message) {
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("messagesList").appendChild(li);
}

function GetString(id) {
    return document.getElementById(id).value;
}

function GetUser() {
    return GetString("userInput");
}

function GetMessage() {
    return GetString("messageInput");
}

function GetGroup() {
    return GetString("groupInput");
}

function IsNullOrWhiteSpace(str) {
    return str == null || str.replace(/\s/g, '').length < 1;
}

connection.on("EnteredOrLeft", function (message) {
    AddMessage(CleanMessage(message));
    ClearTyping();
});

connection.on("ReceiveMessage", function (user, message) {
    AddMessage(user + " says " + CleanMessage(message)); 
    ClearTyping();
});

connection.on("TypingMessage", function (user) {
    ClearTyping();
    AddTypingMessage(user + " is typing...");
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("enterButton").addEventListener("click", function (event) {
    connection.invoke("AddToGroup", GetGroup(), GetUser()).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("exitButton").addEventListener("click", function (event) {
    connection.invoke("RemoveFromGroup", GetGroup(), GetUser()).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


document.getElementById("messageInput").addEventListener("input", function (evt) {
    var group = GetGroup();
    if (IsNullOrWhiteSpace(group)) {
        connection.invoke("Typing", GetUser()).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        connection.invoke("TypingGroup", group, GetUser()).catch(function (err) {
            return console.error(err.toString());
        });
    }
});

document.getElementById("groupSendButton").addEventListener("click", function (event) {
    connection.invoke("SendMessageGroup", GetGroup(), GetUser(), GetMessage()).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    document.getElementById("messageInput").value = "";
    ClearTyping();
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("SendMessage", GetUser(), GetMessage()).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    document.getElementById("messageInput").value = "";
    ClearTyping();
});