using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplomarbeit_HHS.Models
{
	internal class Verkauf
	{
		public class VerkaufInhalt
		{
			public int vId { get; set; }
			public string? webshop { get; set; }
			public string? email { get; set; }
			public int bestellNr { get; set; }
			public string? zahlungsart { get; set; }
			public DateOnly? bestelldatum { get; set; }
			public double brutto { get; set; }
			public double netto { get; set; }
			public double skonto { get; set; }
			public Boolean relSerstellt { get; set; }
			public Boolean eMailversendet { get; set; }
			public Boolean bezahlt { get; set; }
			public int kid { get; set; }
			public int uiid { get; set; }
			
		}

		public async Task<List<VerkaufInhalt>> HoleAlleVerkaeufe()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string VerkaufURL = URLs.URLVerkauf;

			HttpResponseMessage response = await client.GetAsync(VerkaufURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<VerkaufInhalt> verkaufinhalt = JsonSerializer.Deserialize<List<VerkaufInhalt>>(responseBody);

				Console.WriteLine(Convert.ToString(response));
				return verkaufinhalt;

			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<VerkaufInhalt>> ZeigeNeueIdAn()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string VerkaufURL = URLs.URLVerkauf;

			HttpResponseMessage response = await client.GetAsync(VerkaufURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<VerkaufInhalt> bestellung = JsonSerializer.Deserialize<List<VerkaufInhalt>>(responseBody);
				var newId = bestellung;
				newId.Select(b => b.vId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<VerkaufInhalt> HoleVerkaufById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			//Der obrige code bleibt immer gleich (Klasse erstellen ig)
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLVerkauf}/{id}");
			//OutputGetPostPutDelete = $"{URLs.URLEinkaufBestellung}/{IDNumber}";

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				VerkaufInhalt verkaufinhalt = JsonSerializer.Deserialize<VerkaufInhalt>(responseBody);
				return verkaufinhalt;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<VerkaufInhalt> GetVerkaufById(int IDNumber)
		{
			var Verkauf = await HoleVerkaufById(IDNumber);

			if (Verkauf == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int VId = Verkauf.vId;
				string? Webshop = Verkauf.webshop;
				string? Email = Verkauf.email;
				int BestellNr = Verkauf.bestellNr;
				string? Zahlungsart = Verkauf.zahlungsart;
				DateOnly? Bestelldatum = Verkauf.bestelldatum;
				double Brutto = Verkauf.brutto;
				double Netto = Verkauf.netto;
				double Skonto = Verkauf.skonto;
				Boolean RelSerstellt = Verkauf.relSerstellt;
				Boolean EMailversendet = Verkauf.eMailversendet;
				Boolean Bezahlt = Verkauf.bezahlt;
				int Kid = Verkauf.kid;
				int Uiid = Verkauf.uiid;
				return new VerkaufInhalt { };
			}
		}


		public async Task<string> PostVerkauf(int IDNumber, string? Webshop, string? Email, int BestellNr, string? Zahlungsart, DateOnly? Bestelldatum, double Brutto, double Netto, double Skonto, Boolean RelSerstellt, Boolean EMailversendet, Boolean Bezahlt, int Kid, int Uiid)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			VerkaufInhalt newVerkaufinhalt = new VerkaufInhalt { vId = IDNumber, webshop =Webshop, email =Email, bestellNr=BestellNr, zahlungsart=Zahlungsart, bestelldatum=Bestelldatum, brutto=Brutto, netto=Netto, skonto=Skonto, relSerstellt=RelSerstellt, eMailversendet=EMailversendet, bezahlt=Bezahlt,kid=Kid,uiid=Uiid };
			string jsonData = JsonSerializer.Serialize(newVerkaufinhalt);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLVerkauf}/", content);

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

		public async Task<String> PutVerkauf(int IDNumber, string? Webshop, string? Email, int BestellNr, string? Zahlungsart, DateOnly? Bestelldatum, double Brutto, double Netto, double Skonto, Boolean RelSerstellt, Boolean EMailversendet, Boolean Bezahlt, int Kid, int Uiid)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			VerkaufInhalt newVerkaufinhalt = new VerkaufInhalt { vId = IDNumber, webshop = Webshop, email = Email, bestellNr = BestellNr, zahlungsart = Zahlungsart, bestelldatum = Bestelldatum, brutto = Brutto, netto = Netto, skonto = Skonto, relSerstellt = RelSerstellt, eMailversendet = EMailversendet, bezahlt = Bezahlt, kid = Kid, uiid = Uiid };
			string jsonData = JsonSerializer.Serialize(newVerkaufinhalt);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLVerkauf}/{IDNumber}", content);

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


		public async Task<String> DeleteVerkauf(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLVerkauf}/{IDNumber}");

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
