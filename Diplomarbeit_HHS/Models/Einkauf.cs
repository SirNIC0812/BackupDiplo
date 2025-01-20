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
		public class DIP
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

		public async Task<List<DIP>> HoleAlleBestellungen()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string BestellungURL = URLs.URLEinkaufBestellung;

			HttpResponseMessage response = await client.GetAsync(BestellungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<DIP> bestellung = JsonSerializer.Deserialize<List<DIP>>(responseBody);

				Console.WriteLine(Convert.ToString(response));
				return bestellung;

			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<DIP>> ZeigeNeueIdAn()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string BestellungURL = URLs.URLEinkaufBestellung;

			HttpResponseMessage response = await client.GetAsync(BestellungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<DIP> bestellung = JsonSerializer.Deserialize<List<DIP>>(responseBody);
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

		public async Task<DIP> HoleBestellungById(int id)
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
				DIP bestellung = JsonSerializer.Deserialize<DIP>(responseBody);
				return bestellung;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<DIP> GetBestellungById(int IDNumber)
		{
			var Bestellung = await HoleBestellungById(IDNumber);

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
				return new DIP { };
			}
		}


		public async Task<string> PostBestellung(int IDNumber, DateTime? Bestelldatum, string Beschreibung, string Zahlungsart, int BestellNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, Boolean IsComplete)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			DIP newDIP = new DIP { bId = IDNumber, bestelldatum = Bestelldatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, bestellNRLieferant = BestellNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, isComplete = IsComplete };
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

		public async Task<String> PutBestellung(int IDNumber, DateTime? Bestelldatum, string Beschreibung, string Zahlungsart, int BestellNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, Boolean IsComplete)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			DIP newDIP = new DIP { bId = IDNumber, bestelldatum = Bestelldatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, bestellNRLieferant = BestellNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, isComplete = IsComplete };
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


		public async Task<String> DeleteBestellung(int IDNumber)
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
	}
}




//EinkaufRechnung
