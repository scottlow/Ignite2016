chrome.runtime.onMessage.addListener(function(request, sender, sendResponse) {
	switch (request.message) {
		case "hello-world":
			sendResponse({message: "Hello from the background script!"});
			break;
	}
});

// var numRequests = 0;

// chrome.webRequest.onBeforeRequest.addListener(function() {
// 	numRequests++;
// 	chrome.browserAction.setBadgeText({
// 		text: numRequests.toString()
// 	});
// 	chrome.runtime.sendMessage({
// 		message: "request-made"
// 	});
// }, {
// 	urls: ["<all_urls>"]
// });