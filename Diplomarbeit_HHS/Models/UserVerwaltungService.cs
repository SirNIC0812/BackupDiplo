using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplomarbeit_HHS.Models
{
	internal class UserVerwaltungService
	{
		public class User
		{
			public int uId { get; set; }
			public string? username { get; set; }
			public string? passwort { get; set; }
			public string? rolle { get; set; }
		}


		public async Task<List<User>> HoleAlleUser()
		{
			HttpClient GetBestellungen = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			GetBestellungen.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string BestellungURL = URLs.URLUserGetUser;

			HttpResponseMessage response = await GetBestellungen.GetAsync(BestellungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<User> user = JsonSerializer.Deserialize<List<User>>(responseBody);

				Console.WriteLine(Convert.ToString(response));
				return user;

			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}


		public async Task<List<User>> ZeigeNeueIdAn()
		{
			HttpClient GetBestellungen = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			GetBestellungen.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string BestellungURL = URLs.URLUserGetUser;

			HttpResponseMessage response = await GetBestellungen.GetAsync(BestellungURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<User> bestellung = JsonSerializer.Deserialize<List<User>>(responseBody);
				var newId = bestellung;
				newId.Select(b => b.uId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}


		public async Task<User> HoleUserById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			//Der obrige code bleibt immer gleich (Klasse erstellen ig)
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLUserGetUser}/{id}");
			//OutputGetPostPutDelete = $"{URLs.URLEinkaufBestellung}/{IDNumber}";

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				User user = JsonSerializer.Deserialize<User>(responseBody);
				return user;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}


		public async Task<User> GetUserById(int IDNumber)
		{
			var UserInfo = await HoleUserById(IDNumber);

			if (UserInfo == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int uId = UserInfo.uId;
				string? username = UserInfo.username;
				string? passwort = UserInfo.passwort;
				string? rolle = UserInfo.rolle;
				return new User { };
			}
		}


		public async Task<string> PostUser(int IDNumber, string Username, string Passwort, string Rolle)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			User newUser = new User { uId = IDNumber, username= Username, passwort = Passwort, rolle = Rolle };
			string jsonData = JsonSerializer.Serialize(newUser);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLUserPostUser}/", content);

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


		public async Task<String> PutUser(int IDNumber, string Username, string Passwort, string Rolle)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			User newUser = new User { uId = IDNumber, username = Username, passwort = Passwort, rolle = Rolle };
			string jsonData = JsonSerializer.Serialize(newUser);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLUserPutDelete}/{IDNumber}", content);

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


		public async Task<String> DeleteUser(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLUserPutDelete}/{IDNumber}");

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