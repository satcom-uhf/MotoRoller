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
            } else {
                var data = JSON.parse(msg);

                switch (data.type) {
                    case "login":
                        handleLogin(data.success);
                        break;
                    //when somebody wants to call us 
                    case "offer":
                        handleOffer(data.offer, data.name);
                        break;
                    case "answer":
                        handleAnswer(data.answer);
                        break;
                    //when a remote peer sends an ice candidate to us 
                    case "candidate":
                        handleCandidate(data.candidate);
                        break;
                    case "leave":
                        handleLeave();
                        break;
                    default:
                        break;
                }
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
function sendString(data) {
    socket.send(data);
}
function sendpress(button) {
    sendString(`press/${button}`);
}
function sendfree(button) {
    sendString(`free/${button}`);
}

imageMapResize();


/////sound

////our username 
//var name;
//var connectedUser;
//function send(data) {
//    if (connectedUser) {
//        data.name = connectedUser;
//    } 
//    sendString(JSON.stringify(data))
//}


////****** 
////UI selectors block 
////****** 
//navigator.getUserMedia = (navigator.getUserMedia ||
//    navigator.webkitGetUserMedia ||
//    navigator.mozGetUserMedia ||
//    navigator.msGetUserMedia);
//const PeerConnection =
//    window.RTCPeerConnection ||
//    window.mozRTCPeerConnection ||
//    window.webkitRTCPeerConnection;

//var loginPage = document.querySelector('#loginPage');
//var usernameInput = document.querySelector('#usernameInput');
//var loginBtn = document.querySelector('#loginBtn');

//var callPage = document.querySelector('#callPage');
//var callToUsernameInput = document.querySelector('#callToUsernameInput');
//var callBtn = document.querySelector('#callBtn');

//var hangUpBtn = document.querySelector('#hangUpBtn');
//var localAudio = document.querySelector('#localAudio');
//var remoteAudio = document.querySelector('#remoteAudio');

//var yourConn;
//var stream;

//callPage.style.display = "none";

//// Login when the user clicks the button 
//loginBtn.addEventListener("click", function (event) {
//    name = usernameInput.value;

//    if (name.length > 0) {
//        send({
//            type: "login",
//            name: name
//        });
//    }

//});

//function handleLogin(success) {
//    if (success === false) {
//        alert("Ooops...try a different username");
//    } else {
//        loginPage.style.display = "none";
//        callPage.style.display = "block";

//        //********************** 
//        //Starting a peer connection 
//        //********************** 

//        //getting local audio stream 
//        navigator.getUserMedia({ video: false, audio: true }, function (myStream) {
//            stream = myStream;

//            //displaying local audio stream on the page 
//            localAudio.srcObject = stream;

//            //using Google public stun server 
//            var configuration = {
//                "iceServers": [{ "url": "stun:stun2.1.google.com:19302" }],
//                offerToReceiveAudio: true
//            };

//            yourConn = new PeerConnection(configuration);

//            // setup stream listening 
//            yourConn.addStream(stream);

//            //when a remote user adds stream to the peer connection, we display it 
//            yourConn.ontrack = function (e) {
//                remoteAudio.srcObject = e.streams[0];
//            };
//            if (mystream) mystream.getTracks().forEach(
//                track => talk.pc.addTrack(track, mystream)
//            );

//            // Setup ice handling 
//            yourConn.onicecandidate = function (event) {
//                if (event.candidate) {
//                    send({
//                        type: "candidate",
//                        candidate: event.candidate
//                    });
//                }
//            };

//        }, function (error) {
//            console.log(error);
//        });

//    }
//};

////initiating a call 
//callBtn.addEventListener("click", function () {
//    var callToUsername = callToUsernameInput.value;

//    if (callToUsername.length > 0) {
//        connectedUser = callToUsername;

//        // create an offer 
//        yourConn.createOffer(function (offer) {
//            send({
//                type: "offer",
//                offer: offer
//            });

//            yourConn.setLocalDescription(offer);
//        }, function (error) {
//            alert("Error when creating an offer");
//        });
//    }
//});

////when somebody sends us an offer 
//function handleOffer(offer, name) {
//    connectedUser = name;
//    yourConn.setRemoteDescription(new RTCSessionDescription(offer));

//    //create an answer to an offer 
//    yourConn.createAnswer(function (answer) {
//        yourConn.setLocalDescription(answer);

//        send({
//            type: "answer",
//            answer: answer
//        });

//    }, function (error) {
//        alert("Error when creating an answer");
//    });

//};

////when we got an answer from a remote user 
//function handleAnswer(answer) {
//    yourConn.setRemoteDescription(new RTCSessionDescription(answer));
//};

////when we got an ice candidate from a remote user 
//function handleCandidate(candidate) {
//    yourConn.addIceCandidate(new RTCIceCandidate(candidate));
//};

////hang up
//hangUpBtn.addEventListener("click", function () {
//    send({
//        type: "leave"
//    });

//    handleLeave();
//});

//function handleLeave() {
//    connectedUser = null;
//    remoteAudio.src = null;

//    yourConn.close();
//    yourConn.onicecandidate = null;
//    yourConn.onaddstream = null;
//};