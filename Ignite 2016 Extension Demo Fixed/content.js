chrome.runtime.sendMessage({
	message: "hello-world",
});

chrome.runtime.onMessage.addListener(function(request, sender, sendResponse) {
	console.log("Content script received a message: " + request.message);
})