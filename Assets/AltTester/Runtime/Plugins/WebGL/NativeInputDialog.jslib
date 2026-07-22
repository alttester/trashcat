/*
    Copyright(C) 2026 Altom Consulting
*/

mergeInto(LibraryManager.library, {
    WebGL_ShowNativeInput: function (gameObjectNamePtr) {
        var gameObjectName = UTF8ToString(gameObjectNamePtr);

        // Disable Unity's keyboard capture to allow input in the dialog
        SendMessage(gameObjectName, 'SetUnityKeyboardCapture', 0);

        // Create Overlay Background
        var overlay = document.createElement("div");
        overlay.id = "alttester-native-popup";
        overlay.style = "position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.5); z-index:9999; display:flex; align-items:center; justify-content:center; font-family: sans-serif;";

        // Create Dialog Box
        var dialog = document.createElement("div");
        dialog.style = "background:white; padding:20px; border-radius:8px; width:300px; box-shadow: 0 4px 15px rgba(0,0,0,0.3);";
        dialog.innerHTML = `
            <h3 style="margin-top:0">AltTester® Connection Details</h3>
            <input type="text" id="AltTesterHostInputField" placeholder="Host" style="width:90%; margin-bottom:10px; padding:8px;">
            <input type="number" id="AltTesterPortInputField" placeholder="Port" style="width:90%; margin-bottom:10px; padding:8px;">
            <input type="text" id="AltTesterAppNameInputField" placeholder="App Name" style="width:90%; margin-bottom:10px; padding:8px;">
            <div style="margin-bottom:20px;">
                <input type="checkbox" id="AltTesterDontShowAgainCheckbox">
                <label for="AltTesterDontShowAgainCheckbox">Don't show this again</label>
            </div>
            <div style="text-align:right;">
                <button id="AltTesterCancelButton" style="margin-right:10px; padding:8px 16px;">Cancel</button>
                <button id="AltTesterOkButton" style="padding:8px 16px; background:#2196F3; color:white; border:none; border-radius:4px; cursor:pointer;">OK</button>
            </div>
        `;

        overlay.appendChild(dialog);
        document.body.appendChild(overlay);

        // Handle OK Click
        document.getElementById("AltTesterOkButton").onclick = function() {
            var result = {
                host: document.getElementById("AltTesterHostInputField").value,
                port: document.getElementById("AltTesterPortInputField").value,
                appName: document.getElementById("AltTesterAppNameInputField").value,
                dontShowAgain: document.getElementById("AltTesterDontShowAgainCheckbox").checked
            };
            
            // Restore Unity keyboard capture before closing
            SendMessage(gameObjectName, 'SetUnityKeyboardCapture', 1);

            // Send JSON back to Unity
            SendMessage(gameObjectName, "OnInputReceived", JSON.stringify(result));
            document.body.removeChild(overlay);
        };

        // Handle Cancel Click
        document.getElementById("AltTesterCancelButton").onclick = function() {
            // Restore Unity keyboard capture before closing
            SendMessage(gameObjectName, 'SetUnityKeyboardCapture', 1);

            document.body.removeChild(overlay);
        };
    }
});