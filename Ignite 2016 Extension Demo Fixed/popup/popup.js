var numRequestsDiv = document.getElementById('numRequests');
var backgroundPage = chrome.extension.getBackgroundPage();

function updateMetrics() {
	numRequestsDiv.innerText = backgroundPage.numRequests;
}

chrome.runtime.onMessage.addListener(function(request, sender, sendResponse) {
	switch (request.message) {
		case "request-made":
			updateMetrics();
			break;
	}
});

updateMetrics();