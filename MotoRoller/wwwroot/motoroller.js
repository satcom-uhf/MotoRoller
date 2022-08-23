﻿function log(msg) {
    //document.getElementById("log").prepend(msg, document.createElement("br"));
    console.info(msg);
}
function buf2hex(buffer) { // buffer is an ArrayBuffer
    return [...new Uint8Array(buffer)]
        .map(x => x.toString(16).padStart(2, '0'))
        .join(':');
}
var socket;
function connect() {
    socket = new WebSocket(((window.location.protocol === "https:") ? "wss://" : "ws://") + window.location.host + "/ws");
    socket.binaryType = "arraybuffer";
    socket.onerror = function (s, e) {
        log(e);
    };
    function setVisibility(el, flag) {
        document.getElementById(el).style.visibility = flag ? 'visible' : 'hidden';
    }
    socket.addEventListener('message', (event) => {
        if (event.data instanceof ArrayBuffer) {
            var bytes = buf2hex(event.data);
            log(bytes);
        } else {
            log(event.data);
            var msg = event.data;
            if (msg.startsWith("DSPL:")) {
                msg = msg.replace("DSPL:", "");
                var unordered = JSON.parse(msg);
                console.warn(unordered);
                const ordered = Object.keys(unordered).sort().reduce(
                    (obj, key) => {
                        obj[key] = unordered[key];
                        return obj;
                    },
                    {}
                );
                var dspl = "";
                for (let p in ordered) {
                    dspl += ordered[p] + "\r\n";
                }
                document.getElementById("FREQ").innerText = dspl;
            }
            else if (msg.startsWith("ICONS:")) {
                msg = msg.replace("ICONS:", "");
                var icons = JSON.parse(msg);
                setVisibility("Antenna", icons.Antenna);
                var level = [icons.S1, icons.S2, icons.S3, icons.S4, icons.S5].filter(Boolean).length;
                setVisibility("RSSI", level > 0);
                document.getElementById("RSSI").className = `icon-wifi-quality-bars_bars5b-${level}`;
                setVisibility("Monitor", icons.Monitor);
                var scanStatus = icons.Scan ? 'Z' : ' ';
                var priorityScan = icons.ScanDot ? '.' : '';
                document.getElementById('Scan').innerText = scanStatus + priorityScan;
                document.getElementById("PWR").innerText = icons.HighPower?'H':'L';
            }
        }
    });
    socket.addEventListener('open', (event) => {
        log('Connected');
        socket.send('REFRESH');
    });
    socket.addEventListener('close', (event) => {
        log('Reconnecting');
        setTimeout(() => connect(), 1000);
    });
}
connect();
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