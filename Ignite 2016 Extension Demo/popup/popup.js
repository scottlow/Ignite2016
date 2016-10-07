var numRequestsSpan = document.getElementById('numRequests');
var backgroundPage = chrome.extension.getBackgroundPage();

function updateMetrics() {
	numRequestsSpan.innerText = backgroundPage.numRequests;
}

chrome.runtime.onMessage.addListener(function(request, sender, sendResponse) {
	switch (request.message) {
		case "request-made":
			updateMetrics();
			break;
	}
});

updateMetrics();