mergeInto(LibraryManager.library, {
  PasteFromClipboard: function (callbackObjectName, callbackMethodName) {
    // Check if Clipboard API is available
    if (!navigator.clipboard) {
      console.log("Clipboard API not available");
      return;
    }

    const callbackObjectStr = UTF8ToString(callbackObjectName);
    const callbackMethodStr = UTF8ToString(callbackMethodName);

    // Read text from clipboard
    navigator.clipboard
      .readText()
      .then(function (text) {
        console.log("Pasted content: " + text);
        // Call back into Unity with the pasted text
        myGameInstance.SendMessage(callbackObjectStr, callbackMethodStr, text);
      })
      .catch(function (error) {
        console.error("Could not read from clipboard: ", error);
      });
  },

  CopyToClipboard: function (textPtr) {
    var text = UTF8ToString(textPtr);

    // Check if Clipboard API is available
    if (!navigator.clipboard) {
      console.log("Clipboard API not available");
      return;
    }

    // Write text to clipboard
    navigator.clipboard
      .writeText(text)
      .then(function () {
        console.log("Text copied to clipboard");
      })
      .catch(function (error) {
        console.error("Could not copy to clipboard: ", error);
      });
  },
});
