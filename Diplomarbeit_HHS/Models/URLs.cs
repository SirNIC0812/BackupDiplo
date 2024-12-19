public static class URLs
{
	//public const string AnfangURL = "https://localhost:7286/api"; //Local
	public const string AnfangURL = "https://api.nwl.bhakwien10.at/UefaAPI/api";

	//public const string URLLogin = $"{AnfangURL}/Login/login";//local
	public const string URLLogin = $"{AnfangURL}/User/login";

	public const string URLEinkaufBestellung = $"{AnfangURL}/Einkauf/Bestellung";
	public const string URLEinkaufRechnung = $"{AnfangURL}/Einkauf/Rechnung";
	public const string URLEinkaufrechnung = $"{AnfangURL}/Einkaufrechnung";		//was ist der Unterschied zwischen den Zwei

	public const string URLAufgaben = $"{AnfangURL}/Aufgaben";

	public const string URLUserAdminRolle = $"{AnfangURL}/User/getAdminRolle";
	public const string URLUserUserRolle = $"{AnfangURL}/User/getUserRolle";
	public const string URLUserGetUser = $"{AnfangURL}/User/getUser";
	public const string URLUserGetAdmin = $"{AnfangURL}/User/Admin";
	public const string URLUserPostUser = $"{AnfangURL}/User/PostUser";
}