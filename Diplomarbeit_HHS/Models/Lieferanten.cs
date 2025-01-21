using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplomarbeit_HHS.Models
{
	internal class Lieferanten
	{
		public class LieferantenInhalt
		{
			public int lId { get; set; }
			public int fbNr { get; set; }
			public int lieferantenNrSap { get; set; }
			public string? firmenname { get; set; }
			public string? suchbegriff { get; set; }
			public string? adresse { get; set; }
			public string? email { get; set; }
			public string? iban { get; set; }
		}

		public async Task<List<LieferantenInhalt>> HoleAlleLieferanten()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string LieferantenURL = URLs.URLLieferant;

			HttpResponseMessage response = await client.GetAsync(LieferantenURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<LieferantenInhalt> lieferanteninhalt = JsonSerializer.Deserialize<List<LieferantenInhalt>>(responseBody);

				Console.WriteLine(Convert.ToString(response));
				return lieferanteninhalt;

			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<LieferantenInhalt>> ZeigeNeueIdAn()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string LieferantenURL = URLs.URLLieferant;


			HttpResponseMessage response = await client.GetAsync(LieferantenURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<LieferantenInhalt> lieferant = JsonSerializer.Deserialize<List<LieferantenInhalt>>(responseBody);
				var newId = lieferant;
				newId.Select(b => b.lId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<LieferantenInhalt> HoleLieferantById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLLieferant}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				LieferantenInhalt lieferant = JsonSerializer.Deserialize<LieferantenInhalt>(responseBody);
				return lieferant;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}


		public async Task<LieferantenInhalt> GetLieferantById(int IDNumber)
		{
			var Lieferant = await HoleLieferantById(IDNumber);

			if (Lieferant == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int KId = Lieferant.lId;
				int FbNr = Lieferant.fbNr;
				int KdNrSap = Lieferant.lieferantenNrSap;
				string? Firmenname = Lieferant.firmenname;
				string? Suchbegriff = Lieferant.suchbegriff;
				string? Adresse = Lieferant.adresse;
				string? Email = Lieferant.email;
				string? Iban = Lieferant.iban;
				return new LieferantenInhalt { };
			}
		}


		public async Task<string> PostKunde(int IDNumber, int FbNr, int LieferantenNrSap, string? Firmenname, string? Suchbegriff, string? Adresse, string? Email, string? Iban)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			LieferantenInhalt newLieferanteninhalt = new LieferantenInhalt { lId = IDNumber, fbNr = FbNr, lieferantenNrSap = LieferantenNrSap, firmenname = Firmenname, suchbegriff = Suchbegriff, adresse = Adresse, email = Email, iban = Iban };
			string jsonData = JsonSerializer.Serialize(newLieferanteninhalt);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLLieferant}/", content);

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

		public async Task<String> PutKunde(int IDNumber, int FbNr, int LieferantenNrSap, string? Firmenname, string? Suchbegriff, string? Adresse, string? Email, string? Iban)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			LieferantenInhalt newLieferanteninhalt = new LieferantenInhalt { lId = IDNumber, fbNr = FbNr, lieferantenNrSap = LieferantenNrSap, firmenname = Firmenname, suchbegriff = Suchbegriff, adresse = Adresse, email = Email, iban = Iban };
			string jsonData = JsonSerializer.Serialize(newLieferanteninhalt);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLLieferant}/{IDNumber}", content);

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

			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLLieferant}/{IDNumber}");

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
