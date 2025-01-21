using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Diplomarbeit_HHS.Models
{
	internal class Kunden
	{
		public class KundenInhalt
		{
			public int kId { get; set; }
			public int fbNr { get; set; }
			public int kdNrSap { get; set; }
			public string? firmenname { get; set; }
			public string? suchbegriff { get; set; }
			public string? adresse { get; set; }
			public string? email { get; set; }
			public string? iban { get; set; }			
		}

		public async Task<List<KundenInhalt>> HoleAlleKunden()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string KundenURL = URLs.URLKunde;

			HttpResponseMessage response = await client.GetAsync(KundenURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<KundenInhalt> kundeninhalt = JsonSerializer.Deserialize<List<KundenInhalt>>(responseBody);

				Console.WriteLine(Convert.ToString(response));
				return kundeninhalt;

			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<KundenInhalt>> ZeigeNeueIdAn()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string KundenURL = URLs.URLKunde;

			HttpResponseMessage response = await client.GetAsync(KundenURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<KundenInhalt> kunde = JsonSerializer.Deserialize<List<KundenInhalt>>(responseBody);
				var newId = kunde;
				newId.Select(b => b.kId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<KundenInhalt> HoleKundeById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLKunde}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				KundenInhalt kunde = JsonSerializer.Deserialize<KundenInhalt>(responseBody);
				return kunde;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}


		public async Task<KundenInhalt> GetKundeById(int IDNumber)
		{
			var Kunde = await HoleKundeById(IDNumber);

			if (Kunde == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int KId = Kunde.kId;
				int FbNr = Kunde.fbNr;
				int KdNrSap = Kunde.kdNrSap;
				string? Firmenname = Kunde.firmenname;
				string? Suchbegriff = Kunde.suchbegriff;
				string? Adresse = Kunde.adresse;
				string? Email = Kunde.email;
				string? Iban = Kunde.iban;
				return new KundenInhalt { };
			}
		}


		public async Task<string> PostKunde(int IDNumber, int FbNr, int KdNrSap, string? Firmenname, string? Suchbegriff, string? Adresse, string? Email, string? Iban)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			KundenInhalt newKundeninhalt = new KundenInhalt { kId=IDNumber, fbNr=FbNr, kdNrSap=KdNrSap, firmenname=Firmenname, suchbegriff=Suchbegriff, adresse=Adresse, email=Email, iban=Iban };
			string jsonData = JsonSerializer.Serialize(newKundeninhalt);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLKunde}/", content);

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

		public async Task<String> PutKunde(int IDNumber, int FbNr, int KdNrSap, string? Firmenname, string? Suchbegriff, string? Adresse, string? Email, string? Iban)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			KundenInhalt newKundeninhalt = new KundenInhalt { kId = IDNumber, fbNr = FbNr, kdNrSap = KdNrSap, firmenname = Firmenname, suchbegriff = Suchbegriff, adresse = Adresse, email = Email, iban = Iban };
			string jsonData = JsonSerializer.Serialize(newKundeninhalt);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLKunde}/{IDNumber}", content);

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


		public async Task<String> DeleteKunde(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLKunde}/{IDNumber}");

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
