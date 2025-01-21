using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplomarbeit_HHS.Models
{
	internal class Office
	{
		//OfficeBestellungen
		public class OfficeBestellungen
		{
			public int pbId { get; set; }
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

		public async Task<List<OfficeBestellungen>> HoleAlleOfficeBestellungen()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string OfficeBestellungenURL = URLs.URLOfficeBestellung;

			HttpResponseMessage response = await client.GetAsync(OfficeBestellungenURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<OfficeBestellungen> bestellung = JsonSerializer.Deserialize<List<OfficeBestellungen>>(responseBody);
				Console.WriteLine(Convert.ToString(response));
				return bestellung;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<OfficeBestellungen>> ZeigeNeueIdAnOFBE()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string OfficeBestellungenURL = URLs.URLOfficeBestellung;

			HttpResponseMessage response = await client.GetAsync(OfficeBestellungenURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<OfficeBestellungen> bestellung = JsonSerializer.Deserialize<List<OfficeBestellungen>>(responseBody);
				var newId = bestellung;
				newId.Select(b => b.pbId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<OfficeBestellungen> HoleOfficeBestellungById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLOfficeBestellung}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				OfficeBestellungen bestellung = JsonSerializer.Deserialize<OfficeBestellungen>(responseBody);
				return bestellung;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<OfficeBestellungen> GetOfficeBestellungById(int IDNumber)
		{
			var Bestellung = await HoleOfficeBestellungById(IDNumber);

			if (Bestellung == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int PbId = Bestellung.pbId;
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
				return new OfficeBestellungen { };
			}
		}


		public async Task<string> PostOfficeBestellung(int IDNumber, DateTime? Bestelldatum, string Beschreibung, string Zahlungsart, int BestellNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, Boolean IsComplete)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			OfficeBestellungen newOfficeBestellungen = new OfficeBestellungen { pbId = IDNumber, bestelldatum = Bestelldatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, bestellNRLieferant = BestellNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, isComplete = IsComplete };
			string jsonData = JsonSerializer.Serialize(newOfficeBestellungen);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLOfficeBestellung}/", content);

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

		public async Task<String> PutOfficeBestellung(int IDNumber, DateTime? Bestelldatum, string Beschreibung, string Zahlungsart, int BestellNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, Boolean IsComplete)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			OfficeBestellungen newOfficeBestellungen = new OfficeBestellungen { pbId = IDNumber, bestelldatum = Bestelldatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, bestellNRLieferant = BestellNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, isComplete = IsComplete };
			string jsonData = JsonSerializer.Serialize(newOfficeBestellungen);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLOfficeBestellung}/{IDNumber}", content);

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


		public async Task<String> DeleteOfficeBestellung(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLOfficeBestellung}/{IDNumber}");

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




		//OfficeRechnung
		public class OfficeRechnung
		{
			public int prId { get; set; }
			public DateTime? rechnungsdatum { get; set; }
			public string? beschreibung { get; set; }
			public string? zahlungsart { get; set; }
			public int rechnungsNRLieferant { get; set; }
			public int uiid { get; set; }
			public int lid { get; set; }
			public int anzahl { get; set; }
			public double netto { get; set; }
			public double steuer { get; set; }
			public double skonto { get; set; }
			public double brutto { get; set; }
			public int bid {  get; set; }
			public Boolean gezahlt { get; set; }
		}

		public async Task<List<OfficeRechnung>> HoleAlleOfficeRechnungen()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string OfficeRechnungURL = URLs.URLOfficeRechnung;
			HttpResponseMessage response = await client.GetAsync(OfficeRechnungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<OfficeRechnung> rechnung = JsonSerializer.Deserialize<List<OfficeRechnung>>(responseBody);
				Console.WriteLine(Convert.ToString(response));
				return rechnung;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<OfficeRechnung>> ZeigeNeueIdAnOFRE()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string OfficeRechnungURL = URLs.URLOfficeRechnung;
			HttpResponseMessage response = await client.GetAsync(OfficeRechnungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<OfficeRechnung> rechnung = JsonSerializer.Deserialize<List<OfficeRechnung>>(responseBody);
				var newId = rechnung;
				newId.Select(b => b.prId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<OfficeRechnung> HoleOfficeRechnungById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLOfficeRechnung}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				OfficeRechnung rechnung = JsonSerializer.Deserialize<OfficeRechnung>(responseBody);
				return rechnung;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<OfficeRechnung> GetOfficeRechnungById(int IDNumber)
		{
			var Rechnung = await HoleOfficeRechnungById(IDNumber);

			if (Rechnung == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int PrId = Rechnung.prId;
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
				int Bid = Rechnung.bid;
				Boolean Gezahlt = Rechnung.gezahlt;
				return new OfficeRechnung { };
			}
		}


		public async Task<string> PostOfficeRechnung(int IDNumber, DateTime? Rechnungsdatum, string Beschreibung, string Zahlungsart, int RechnungsNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, int BId, Boolean Gezahlt)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			OfficeRechnung newOfficeRechnung = new OfficeRechnung { prId = IDNumber, rechnungsdatum = Rechnungsdatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, rechnungsNRLieferant = RechnungsNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, bid=BId, gezahlt = Gezahlt };
			string jsonData = JsonSerializer.Serialize(newOfficeRechnung);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLOfficeRechnung}/", content);

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

		public async Task<String> PutOfficeRechnung(int IDNumber, DateTime? Rechnungsdatum, string Beschreibung, string Zahlungsart, int RechnungsNRLieferant, int Uiid, int Lid, int Anzahl, double Netto, double Steuer, double Skonto, double Brutto, int BId, Boolean Gezahlt)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			OfficeRechnung newOfficeRechnung = new OfficeRechnung { prId = IDNumber, rechnungsdatum = Rechnungsdatum, beschreibung = Beschreibung, zahlungsart = Zahlungsart, rechnungsNRLieferant = RechnungsNRLieferant, uiid = Uiid, lid = Lid, anzahl = Anzahl, netto = Netto, steuer = Steuer, skonto = Skonto, brutto = Brutto, bid = BId, gezahlt = Gezahlt };
			string jsonData = JsonSerializer.Serialize(newOfficeRechnung);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLOfficeRechnung}/{IDNumber}", content);

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


		public async Task<String> DeleteOfficeRechnung(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLOfficeRechnung}/{IDNumber}");

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
