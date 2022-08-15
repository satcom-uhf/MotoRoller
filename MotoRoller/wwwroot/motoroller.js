function log(msg) {
    document.getElementById("log").prepend(msg, document.createElement("br"));
}
function buf2hex(buffer) { // buffer is an ArrayBuffer
    return [...new Uint8Array(buffer)]
        .map(x => x.toString(16).padStart(2, '0'))
        .join(':');
}
var socket = new WebSocket(((window.location.protocol === "https:") ? "wss://" : "ws://") + window.location.host + "/ws");
socket.binaryType = "arraybuffer";
socket.onerror = function (s, e) {
    log(e);
};
socket.addEventListener('message', (event) => {
    if (event.data instanceof ArrayBuffer) {
        var bytes = buf2hex(event.data);
        log(bytes);
    } else {

        log('Message from server ' + event.data);
        var msg = event.data;
        if (msg.startsWith("DSPL:")) {
            msg = msg.replace("DSPL:", "");
            document.getElementById("LCD").innerText = msg;
        }
        
    }
});
socket.addEventListener('open', (event) => {
    log('Connected');
    socket.send('REFRESH');
});
var map = document.getElementById('image-map');
map.addEventListener('mousedown', pressed, false);
map.addEventListener('mouseup', free, false);
function pressed(e) {
    e.preventDefault();
    if (e.target !== e.currentTarget) {
        var clickedBtn = e.target.title;
        log("Press:" + clickedBtn);
        sendpress(clickedBtn);
    }
    e.stopPropagation();
}
function free(e) {
    if (e.target !== e.currentTarget) {
        var clickedBtn = e.target.title;
        log("Free:" + clickedBtn);
        sendfree(clickedBtn);
    }
    e.stopPropagation();
}
function send(url) {

    socket.send(url);
}
function sendpress(button) {
    send(`press/${button}`);
}
function sendfree(button) {
    send(`free/${button}`);
}
imageMapResize();