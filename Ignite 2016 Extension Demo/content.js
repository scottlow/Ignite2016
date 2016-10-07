chrome.runtime.sendMessage({
	message: "hello-world",
}, function(response) {
	console.log("Content script received a message: " + response.message);
});