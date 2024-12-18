let lastIDJS = 0;
window.setLastIDInJS = function (lastID) {
	lastIDJS = lastID;
	document.getElementById("AusgabeTest").innerHTML = "Erhaltener lastID-Wert aus C#: " + lastIDJS;
};

document.getElementById("IDNumber").addEventListener("input", ChangeButtons1);
function ChangeButtons1() {
	let IdNumber = document.getElementById("IDNumber").value;
	//let InnerHtmlId = document.getElementById("InnerHtmlId").value;
	document.getElementById("AusgabeTest").innerHTML = "IdNumber: " + IdNumber + "; lastIDJS: " + lastIDJS; //Kann man dann weg geben

	if (IdNumber <= lastIDJS) {
		document.getElementById("GETBestellungen").classList.remove("Hide");
		document.getElementById("POSTBestellungen").classList.add("Hide");
		document.getElementById("PUTBestellungen").classList.add("Hide");
		document.getElementById("DELETEBestellungen").classList.add("Hide");
		document.getElementById("AusgabeTest").innerHTML = "Es exisitert schon: IdNumber: " + IdNumber + "; lastIDJS: " + lastIDJS; //Kann man dann weg geben
	} else {
		document.getElementById("GETBestellungen").classList.add("Hide");
		document.getElementById("POSTBestellungen").classList.remove("Hide");
		document.getElementById("PUTBestellungen").classList.add("Hide");
		document.getElementById("DELETEBestellungen").classList.add("Hide");
		document.getElementById("AusgabeTest").innerHTML = "Es exisitert noch nicht: IdNumber: " + IdNumber + "; lastIDJS: " + lastIDJS; //Kann man dann weg geben
	}
};

document.getElementById("GETBestellungen").addEventListener("click", ChangeButtons2);
function ChangeButtons2() {
	document.getElementById("PUTBestellungen").classList.remove("Hide");
	document.getElementById("DELETEBestellungen").classList.remove("Hide");
};

document.getElementById("HeaderLogout").addEventListener("click", Abmelden);
function Abmelden() {
	alert("Sie haben sich erfolgreich Abgemeldet!");
};