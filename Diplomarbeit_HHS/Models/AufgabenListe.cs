using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplomarbeit_HHS.Models
{
	internal class AufgabenListe
	{
		public class Aufgabe
		{
			public int aId { get; set; }
			public string? aufgabe { get; set; }
			public DateTime? expiryDate { get; set; }
		}

		private int AId;
		private string? AufgabenInhalt;
		private DateTime? ExpiryDate;

		public async Task<List<Aufgabe>> HoleAlleAufgaben()
		{
			HttpClient GetAufgabe = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			GetAufgabe.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string AufgabengURL = URLs.URLAufgaben;

			HttpResponseMessage response = await GetAufgabe.GetAsync(AufgabengURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<Aufgabe> aufgabe = JsonSerializer.Deserialize<List<Aufgabe>>(responseBody);

				Console.WriteLine(Convert.ToString(response));
				return aufgabe;

			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<Aufgabe>> ZeigeNeueIdAn()
		{
			HttpClient GetAufgaben = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			GetAufgaben.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string AufgabengURL = URLs.URLAufgaben;

			HttpResponseMessage response = await GetAufgaben.GetAsync(AufgabengURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<Aufgabe> aufgabe = JsonSerializer.Deserialize<List<Aufgabe>>(responseBody);
				var newId = aufgabe;
				newId.Select(a => a.aId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<Aufgabe> HoleAufgabeById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			//Der obrige code bleibt immer gleich (Klasse erstellen ig)
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLAufgaben}/{id}");
			//OutputGetPostPutDelete = $"{URLs.URLEinkaufBestellung}/{IDNumber}";

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				Aufgabe aufgabe = JsonSerializer.Deserialize<Aufgabe>(responseBody);
				return aufgabe;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<Aufgabe> GetAufgabeById(int IDNumber)
		{
			var Aufgabe = await HoleAufgabeById(IDNumber);

			if (Aufgabe == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int AId = Aufgabe.aId;
				string? AufgabenInhalt = Aufgabe.aufgabe;
				DateTime? ExpiryDate = Aufgabe.expiryDate;

				return new Aufgabe { };
			}
		}


		public async Task<string> PostAufgabe(int AId, string Aufgabe, DateTime? ExpiryDate)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			Aufgabe newAufgabe = new Aufgabe { aId = AId, aufgabe = Aufgabe, expiryDate = ExpiryDate };
			string jsonData = JsonSerializer.Serialize(newAufgabe);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLAufgaben}/", content);

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

		public async Task<String> PutAufgabe(int AId, string Aufgabe, DateTime? ExpiryDate)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			Aufgabe newAufgabe = new Aufgabe { aId = AId, aufgabe = Aufgabe, expiryDate = ExpiryDate };
			string jsonData = JsonSerializer.Serialize(newAufgabe);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLAufgaben}/{AId}", content);

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


		public async Task<String> DeleteAufgabe(int AId)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLAufgaben}/{AId}");

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
