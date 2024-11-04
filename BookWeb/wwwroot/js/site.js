
var connection = new signalR.HubConnectionBuilder().withUrl("/shippingHub").build();

connection.on("ReceiveUpdate", function (message) {
    var notification = document.getElementById('notification');
    notification.innerText = message;
    notification.style.display = 'block';

    setTimeout(function () {
        notification.style.display = 'none';
    }, 3000);

    // Optionally reload the page
    setTimeout(function () {
        location.reload();
    }, 3500);
});

connection.start().then(function () {
    console.log("Connected to SignalR Hub");
}).catch(function (err) {
    return console.error(err.toString());
});
