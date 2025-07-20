mergeInto(LibraryManager.library, {

  OpenUrl: function (url) {
    window.open(UTF8ToString(url),'_self')
  },

  SetCookie: function(cookieName, value){
    var date = new Date();
    var expirationTime = new Date(date.getTime() + 30 * 60000);
    document.cookie = UTF8ToString(cookieName) + "=" + UTF8ToString(value) + "; expires=" + UTF8ToString(expirationTime) + "; path=/"
  },

  GetCookie: function(cookieName)
  {
    const name = UTF8ToString(cookieName) + "=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookieArray = decodedCookie.split(';');

    for (let i = 0; i < cookieArray.length; i++) {
        let cookie = cookieArray[i];
        while (cookie.charAt(0) === ' ') {
            cookie = cookie.substring(1);
        }
        if (cookie.indexOf(name) === 0) {
            var returnStr = cookie.substring(name.length, cookie.length);
            var bufferSize = lengthBytesUTF8(returnStr) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        }
    }
    return null; // Return null if the cookie is not found
  },

  DeleteCookie: function(cookieName)
  {
    document.cookie = UTF8ToString(cookieName) +"=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/";
  },

  RedirectClearParam: function()
  {
    window.location.href = window.location.origin;
  },

  AccessCamAndMic: function()
  {
    let All_mediaDevices=navigator.mediaDevices
    if (!All_mediaDevices || !All_mediaDevices.getUserMedia) {
        console.log("getUserMedia() not supported.");
        return;
    }
    All_mediaDevices.getUserMedia({
        audio: true,
        video: true
    })
    .then(function(stream) {
/*
        if(!stream.getAudioTracks().length && !stream.getVideoTracks().length)
        {
            myUnityInstance.SendMessage('StartUpManager', 'JsHandleAccessCamAndMic', "Please Allow Microphone and Camera permissions to use this app, then refresh the page");
        }
        else if(!stream.getAudioTracks().length)
        {
            myUnityInstance.SendMessage('StartUpManager', 'JsHandleAccessCamAndMic', "Please Allow Microphone permissions to use this app, then refresh the page");
        }
        else if(!stream.getVideoTracks().length)
        {
            myUnityInstance.SendMessage('StartUpManager', 'JsHandleAccessCamAndMic', "Please Allow Camera permissions to use this app, then refresh the page");
        }
        else
        {
            myUnityInstance.SendMessage('StartUpManager', 'JsHandleAccessCamAndMic', "");
        }
*/
    })
    .catch(function(e) {
        console.log(e.name + ": " + e.message);
    });
  },

  StartRecording: function()
  {
    mediaRecorder.start();
  },

  StopRecording: function()
  {
    mediaRecorder.stop();
  },

  LoginMicrosoft: function()
  {
    var tenantId = "3485b963-82ba-4a6f-810f-b5cc226ff898";
    var clientId = "3a8a511f-290f-4d48-b1b2-4f4213492fb8";
    var redirectUri = "https://virtualclassroom.apps.binus.ac.id/";
    var scope = "openid profile User.Read";
    var url = "https://login.microsoftonline.com/" + tenantId + "/oauth2/v2.0/authorize?client_id=" + clientId + "&redirect_uri=" + encodeURI(redirectUri) + "&response_type=code&scope=" + encodeURI(scope) + "&prompt=consent&response_mode=query";
    window.open(url, '_self');
  },

  StartShareScreen: function()
  {
    if(isSafari())
    {
        alert("Screen sharing tidak tersedia di Safari Browser. Silakan gunakan Chrome atau Microsoft Edge.");
    }
    else
    {
        navigator.mediaDevices.getDisplayMedia(displayMediaOptions)
        .then(stream => {
            screenStream = stream;

            myUnityInstance.SendMessage('ShareScreenManager', 'JSHandleStartShareScreen', 1);

            stream.oninactive = () => {
                myUnityInstance.SendMessage('ShareScreenManager', 'JSHandleStopShareScreen', 1);
                videoElement.srcObject = null;
            };
        })
        .catch(error => {
            myUnityInstance.SendMessage('ShareScreenManager', 'JSHandleStartShareScreen', 0);
        });
    }
  },

  StopShareScreen: function()
  {
    let tracks = screenStream.getTracks();

    tracks.forEach((track) => track.stop());
  },

  CaptureFrameAndSendToUnity: function()
  {
    var canvas = document.createElement('canvas');
    var context = canvas.getContext('2d');

    // Create a video element and set its srcObject to the MediaStream
    // var videoElement = document.createElement('video');
    if(videoElement.srcObject == null) videoElement.srcObject = screenStream;

    // Ensure the video is playing (you may want to handle this differently based on your use case)
    videoElement.play().then(() => {
        // Set canvas dimensions based on video element
        canvas.width = videoElement.videoWidth;
        canvas.height = videoElement.videoHeight;

        // Draw the video frame to the canvas
        context.drawImage(videoElement, 0, 0, canvas.width, canvas.height);

        // Get image data and send to Unity
        var imageData = context.getImageData(0, 0, canvas.width, canvas.height);

        var messageData = encodeImageData(imageData) + ',' + canvas.width + ',' + canvas.height;
        myUnityInstance.SendMessage('ShareScreenManager', 'OnVideoFrame', messageData);
    }).catch(error => {
        console.error('Error playing video:', error);
    });
  }
});