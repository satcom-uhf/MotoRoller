let ptt = document.getElementById('ptt');

function setCoords() {
    let coords = document.getElementById('pttZone').getAttribute('coords').split(',').map(Number);
    ptt.style.left = coords[0] + 'px';
    ptt.style.top = coords[1] + 'px';
}
function log(msg) {
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
                var lines = 0;
                for (let p in ordered) {
                    lines++;
                    if (lines > 3) break;
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
                document.getElementById("PWR").innerText = icons.HighPower ? 'H' : 'L';
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
window.addEventListener('resize', () => setTimeout(setCoords, 500));

function pressed(e) {
    e.preventDefault();
    if (e.target !== e.currentTarget) {
        var clickedBtn = e.target.title;
        log("Press:" + clickedBtn);
        sendpress(clickedBtn);
    }
    e.stopPropagation();
}
var map = document.getElementById('image-map');
map.addEventListener('mousedown', pressed, false);
map.addEventListener('mouseup', free, false);
function free(e) {
    if (e.target !== e.currentTarget) {
        var clickedBtn = e.target.title;
        log("Free:" + clickedBtn);
        sendfree(clickedBtn);
    }
    e.stopPropagation();
}
function sendString(data) {
    socket.send(data);
}
function sendpress(button) {
    sendString(`press/${button}`);
}
function sendfree(button) {
    sendString(`free/${button}`);
}

function pressPtt() {
    ptt.classList.add("active");
    sendpress("ptt");
}
function freePtt() {
    ptt.classList.remove("active");
    sendfree("ptt");
}

imageMapResize();
setCoords();

/////sound

////our username 
var name;
var connectedUser;
function send(data) {
    if (connectedUser) {
        data.name = connectedUser;
    } 
    sendString(JSON.stringify(data))
}
PHONE.send = send;

(() => {
    'use strict';

    // ~Warning~ You must get your own API Keys for non-demo purposes.
    // ~Warning~ Get your PubNub API Keys: https://www.pubnub.com/get-started/
    // The phone *number* can by any string value

    let session = null;
    const number = Math.ceil(Math.random() * 10000);
    const phone = PHONE({
        number: number
        , autocam: false
        , publish_key: 'pub-c-561a7378-fa06-4c50-a331-5c0056d0163c'
        , subscribe_key: 'sub-c-17b7db8a-3915-11e4-9868-02ee2ddab7fe'
    });

    // Debugging Output
    phone.debug(info => console.info(info));

    // Show Number
    phone.$('number').innerHTML = 'Number: ' + number;
    phone.camera.start().then(() => phone.camera.manageAudio(false));

    // Local Camera Display
    phone.camera.ready(video => {
        phone.$('video-out').appendChild(video);
    });

    // As soon as the phone is ready we can make calls
    phone.ready(() => {

        // Start Call
        phone.bind(
            'mousedown,touchstart'
            , phone.$('ptt')
            , event => {
                phone.camera.manageAudio(true);
                pressPtt();
            }
        );
        phone.bind(
            'mouseup,touchend'
            , phone.$('ptt')
            , event => {
                phone.camera.manageAudio(false);
                freePtt();
            }
        );
        phone.bind(
            'mousedown,touchstart'
            , phone.$('startcall')
            , event => session = phone.dial(phone.$('dial').value)
        );

        phone.bind(
            'mousedown,touchstart'
            , phone.$('fullscreen')
            , event => {
                if ('wakeLock' in navigator) {
                    navigator.wakeLock.request();
                }
                document.getElementById("motorolaFront").requestFullscreen();

            }
        );

    });

    // When Call Comes In or is to be Connected
    phone.receive(function (session) {

        // Display Your Friend's Live Video
        session.connected(function (session) {
            phone.$('video-out').appendChild(session.video);
        });

    });

})();


