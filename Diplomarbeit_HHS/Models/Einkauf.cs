using Diplomarbeit_HHS.Components.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplomarbeit_HHS.Models
{
	
	internal class Einkauf
	{
		//EinkaufBestellungen
		public class EinkaufBestellungen
		{
			public int bId { get; set; }
			public DateTime? bestelldatum { get; set; }
			public string? beschreibung { get; set; }
			public string? zahlungsart { get; set; }
			public int bestellNRLieferant { get; set; }
			public int uiid { get; set; }
			public int lid { get; set; }
			public int anzahl { get; set; }
			public double netto { get; set; }
			public double steuer { get; set; }
			public double skonto { get; set; }
			public double brutto { get; set; }
			public Boolean isComplete { get; set; }
		}

		public async Task<List<EinkaufBestellungen>> HoleAlleEinkaufBestellungen()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string BestellungURL = URLs.URLEinkaufBestellung;

			HttpResponseMessage response = await client.GetAsync(BestellungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<EinkaufBestellungen> bestellung = JsonSerializer.Deserialize<List<EinkaufBestellungen>>(responseBody);
				Console.WriteLine(Convert.ToString(response));
				return bestellung;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<EinkaufBestellungen>> ZeigeNeueIdAnEKBE()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string BestellungURL = URLs.URLEinkaufBestellung;

			HttpResponseMessage response = await client.GetAsync(BestellungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<EinkaufBestellungen> bestellung = JsonSerializer.Deserialize<List<EinkaufBestellungen>>(responseBody);
				var newId = bestellung;
				newId.Select(b => b.bId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<EinkaufBestellungen> HoleEinkaufBestellungById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			//Der obrige code bleibt immer gleich (Klasse erstellen ig)
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLEinkaufBestellung}/{id}");
			//OutputGetPostPutDelete = $"{URLs.URLEinkaufBestellung}/{IDNumber}";

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				EinkaufBestellungen bestellung = JsonSerializer.Deserialize<EinkaufBestellungen>(responseBody);
				return bestellung;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<EinkaufBestellungen> GetEinkaufBestellungById(int IDNumber)
		{
			var Bestellung = await HoleEinkaufBestellungById(IDNumber);

			if (Bestellung == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				DateTime? Bestelldatum = Bestellung.bestelldatum;
				string? Beschreibung = Bestellung.beschreibung;
				string? Zahlungsart = Bestellung.zahlungsart; ;
				int BestellNRLieferant = Bestellung.bestellNRLieferant; ;
				int Uiid = Bestellung.uiid; ;
				int Lid = Bestellung.lid; ;
				int Anzahl = Bestellung.anzahl;
				double Netto = Bestellung.netto;
				double Steuer = Bestellung.steuer;
				double Skonto = Bestellung.skonto;
				double Brutto = Bestellung.brutto;
				Boolean IsComplete = Bestellung.isComplete;
				return new EinkaufBestellungen { };
			}
		}


		public async Task<string> PostEinkaufBestellung(int IDNumber, DateTime? Bestelldatum, string Beschreibung, string Zahlungsart, int BestellNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, Boolean IsComplete)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			EinkaufBestellungen newDIP = new EinkaufBestellungen { bId = IDNumber, bestelldatum = Bestelldatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, bestellNRLieferant = BestellNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, isComplete = IsComplete };
			string jsonData = JsonSerializer.Serialize(newDIP);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLEinkaufBestellung}/", content);

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				string OutputResult = responseBody;
				string OutputGetPostPutDelete = "POST wurde erfolgreich ausgeführt!";
				return OutputGetPostPutDelete;
			}
			else
			{
				string errorDetails = await response.Content.ReadAsStringAsync();
				Console.WriteLine("Details: " + errorDetails);
				string OutputGetPostPutDelete = "Error TestAPIPOST: " + errorDetails  + ""+ response.Content + " || " + response.Headers + " || " + response.ReasonPhrase + " || " + response.StatusCode + " || " + response.RequestMessage+ " || ";
				return OutputGetPostPutDelete;
			}
		}

		public async Task<String> PutEinkaufBestellung(int IDNumber, DateTime? Bestelldatum, string Beschreibung, string Zahlungsart, int BestellNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, Boolean IsComplete)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			EinkaufBestellungen newDIP = new EinkaufBestellungen { bId = IDNumber, bestelldatum = Bestelldatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, bestellNRLieferant = BestellNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, isComplete = IsComplete };
			string jsonData = JsonSerializer.Serialize(newDIP);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLEinkaufBestellung}/{IDNumber}", content);

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				string OutputResult = responseBody;
				string OutputGetPostPutDelete = "PUT wurde erfolgreich ausgeführt!";
				return OutputGetPostPutDelete;
			}
			else
			{
				string OutputGetPostPutDelete = "Error TestAPIPUT: " + response.StatusCode;
				return OutputGetPostPutDelete;
			}
		}


		public async Task<String> DeleteEinkaufBestellung(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLEinkaufBestellung}/{IDNumber}");

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				string OutputResult = responseBody;
				string OutputGetPostPutDelete = "DELETE wurde erfolgreich ausgeführt!";
				return OutputGetPostPutDelete;
			}
			else
			{ 
				string OutputGetPostPutDelete = "Error TestAPIDELETE: " + response.StatusCode;
				return OutputGetPostPutDelete;
			}
		}




		//EinkaufRechnung
		public class EinkaufRechnung
		{
			public int rId { get; set; }//
			public DateTime? rechnungsdatum { get; set; }//
			public string? beschreibung { get; set; }
			public string? zahlungsart { get; set; }
			public int rechnungsNRLieferant { get; set; }//
			public int uiid { get; set; }
			public int lid { get; set; }
			public int anzahl { get; set; }
			public double netto { get; set; }
			public double steuer { get; set; }
			public double skonto { get; set; }
			public double brutto { get; set; }
			public Boolean gezahlt { get; set; }//
		}

		public async Task<List<EinkaufRechnung>> HoleAlleEinkaufRechnungen()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string EinkaufRechnungURL = URLs.URLEinkaufRechnung;

			HttpResponseMessage response = await client.GetAsync(EinkaufRechnungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<EinkaufRechnung> rechnung = JsonSerializer.Deserialize<List<EinkaufRechnung>>(responseBody);
				Console.WriteLine(Convert.ToString(response));
				return rechnung;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<EinkaufRechnung>> ZeigeNeueIdAnEKRE()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string EinkaufRechnungURL = URLs.URLEinkaufRechnung;
			HttpResponseMessage response = await client.GetAsync(EinkaufRechnungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<EinkaufRechnung> rechnung = JsonSerializer.Deserialize<List<EinkaufRechnung>>(responseBody);
				var newId = rechnung;
				newId.Select(b => b.rId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<EinkaufRechnung> HoleEinkaufRechnungById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLEinkaufRechnung}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				EinkaufRechnung rechnung = JsonSerializer.Deserialize<EinkaufRechnung>(responseBody);
				return rechnung;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<EinkaufRechnung> GetEinkaufRechnungById(int IDNumber)
		{
			var Rechnung = await HoleEinkaufRechnungById(IDNumber);

			if (Rechnung == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int RId = Rechnung.rId;
				DateTime? Rechnungsdatum = Rechnung.rechnungsdatum;
				string? Beschreibung = Rechnung.beschreibung;
				string? Zahlungsart = Rechnung.zahlungsart; ;
				int RechnungsNRLieferant = Rechnung.rechnungsNRLieferant; ;
				int Uiid = Rechnung.uiid; ;
				int Lid = Rechnung.lid; ;
				int Anzahl = Rechnung.anzahl;
				double Netto = Rechnung.netto;
				double Steuer = Rechnung.steuer;
				double Skonto = Rechnung.skonto;
				double Brutto = Rechnung.brutto;
				Boolean Gezahlt = Rechnung.gezahlt;
				return new EinkaufRechnung { };
			}
		}


		public async Task<string> PostEinkaufRechnung(int IDNumber, DateTime? Rechnungsdatum, string Beschreibung, string Zahlungsart, int RechnungsNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, Boolean Gezahlt)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			EinkaufRechnung newEinkaufRechnung = new EinkaufRechnung { rId = IDNumber, rechnungsdatum = Rechnungsdatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, rechnungsNRLieferant = RechnungsNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, gezahlt = Gezahlt };
			string jsonData = JsonSerializer.Serialize(newEinkaufRechnung);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLEinkaufRechnung}/", content);

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				string OutputResult = responseBody;
				string OutputGetPostPutDelete = "POST wurde erfolgreich ausgeführt!";
				return OutputGetPostPutDelete;
			}
			else
			{
				string errorDetails = await response.Content.ReadAsStringAsync();
				Console.WriteLine("Details: " + errorDetails);
				string OutputGetPostPutDelete = "Error TestAPIPOST: " + errorDetails + "" + response.Content + " || " + response.Headers + " || " + response.ReasonPhrase + " || " + response.StatusCode + " || " + response.RequestMessage + " || ";
				return OutputGetPostPutDelete;
			}
		}

		public async Task<String> PutEinkaufRechnung(int IDNumber, DateTime? Rechnungsdatum, string Beschreibung, string Zahlungsart, int RechnungsNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, Boolean Gezahlt)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			EinkaufRechnung newEinkaufRechnung = new EinkaufRechnung { rId = IDNumber, rechnungsdatum = Rechnungsdatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, rechnungsNRLieferant = RechnungsNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, gezahlt = Gezahlt };
			string jsonData = JsonSerializer.Serialize(newEinkaufRechnung);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLEinkaufRechnung}/{IDNumber}", content);

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				string OutputResult = responseBody;
				string OutputGetPostPutDelete = "PUT wurde erfolgreich ausgeführt!";
				return OutputGetPostPutDelete;
			}
			else
			{
				string OutputGetPostPutDelete = "Error TestAPIPUT: " + response.StatusCode;
				return OutputGetPostPutDelete;
			}
		}


		public async Task<String> DeleteEinkaufRechnung(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLEinkaufRechnung}/{IDNumber}");

			if (response.IsSuccessStatusCode)
			{
				var responseBody = await response.Content.ReadAsStringAsync();
				string OutputResult = responseBody;
				string OutputGetPostPutDelete = "DELETE wurde erfolgreich ausgeführt!";
				return OutputGetPostPutDelete;
			}
			else
			{
				string OutputGetPostPutDelete = "Error TestAPIDELETE: " + response.StatusCode;
				return OutputGetPostPutDelete;
			}
		}
	}
}
