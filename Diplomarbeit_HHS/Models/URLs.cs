﻿public static class URLs
{
	//public const string AnfangURL = "https://localhost:7286/api";								//Local
	public const string AnfangURL = "https://api.nwl.bhakwien10.at/UefaAPI/api";


	//Anmelden
	//public const string URLLogin = $"{AnfangURL}/Login/login";								//Local
	public const string URLLogin = $"{AnfangURL}/User/login";									//Schon Verwendet
	

	//Aufgaben
	public const string URLAufgaben = $"{AnfangURL}/Aufgaben";									//Schon Verwendet


	//Einkauf
	public const string URLEinkaufBestellung = $"{AnfangURL}/Einkauf/Bestellung";				//Schon Verwendet
	public const string URLEinkaufRechnung = $"{AnfangURL}/Einkauf/Rechnung";					//NOCH NICHT VERWENDET


	//Kunde
	public const string URLKunde = $"{AnfangURL}/Kunde";                                        //Schon Verwendet


	//Lieferant
	public const string URLLieferant = $"{AnfangURL}/Lieferant";                                //Schon Verwendet


	//Office
	public const string URLOfficeBestellung = $"{AnfangURL}/Office/Bestellung";					//NOCH NICHT VERWENDET
	public const string URLOfficeRechnung = $"{AnfangURL}/Office/Rechnung";						//NOCH NICHT VERWENDET


	//Personal
	public const string URLPersonal = $"{AnfangURL}/Personal";									//In Arbeit


	//Rechnungswesen
	public const string URLRechnungswesenEinkauf = $"{AnfangURL}/Rechnungswesen/Einkauf";       //Schon Verwendet
	public const string URLRechnungswesenVerkauf = $"{AnfangURL}/Rechnungswesen/Verkauf";       //NOCH NICHT VERWENDET
	public const string URLRechnungswesenPersonal = $"{AnfangURL}/Rechnungswesen/Personal";     //NOCH NICHT VERWENDET


	//User
	public const string URLUserGetUser = $"{AnfangURL}/User/getUser";                           //Brauch ich ned unbedingt || Schon Verwendet
	public const string URLUserGetUserAdmin = $"{AnfangURL}/User/getUser/Admin";				//wird dann User/getUser || Zeigt alle User + Admin
	public const string URLUserPostUser = $"{AnfangURL}/User/PostUser";                         //Post User || Schon Verwendet
	public const string URLUserPutDelete = $"{AnfangURL}/User";                                 //Put User || Schon Verwendet
	public const string URLUserInfo = $"{AnfangURL}/User/Userinfo";                             //Mehr Informationen über User || Schon Verwendet


	//Verkauf	
	public const string URLVerkauf = $"{AnfangURL}/Verkauf";                                    //Schon Verwendet
}