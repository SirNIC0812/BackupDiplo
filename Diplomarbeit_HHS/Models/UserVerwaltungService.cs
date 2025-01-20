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
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string UserURL = URLs.URLUserGetUser;

			HttpResponseMessage response = await client.GetAsync(UserURL);

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
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string UserURL = URLs.URLUserGetUser;

			HttpResponseMessage response = await client.GetAsync(UserURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<User> user = JsonSerializer.Deserialize<List<User>>(responseBody);
				var newId = user;
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
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLUserGetUser}/{id}");

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




		public class UserInfo
		{
			public int uiId { get; set; }
			public int katalognummer { get; set; }
			public string? vorname { get; set; }
			public string? nachname { get; set; }
			public string? abteilung { get; set; }
			public bool abteilungsleiter { get; set; }
			public double bruttogehalt { get; set; }
			public int uId { get; set; }
		}


		public async Task<List<UserInfo>> HoleAlleUserInfo()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string UserInfoURL = URLs.URLUserInfo;

			HttpResponseMessage response = await client.GetAsync(UserInfoURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<UserInfo> userinfo = JsonSerializer.Deserialize<List<UserInfo>>(responseBody);

				Console.WriteLine(Convert.ToString(response));
				return userinfo;

			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}


		public async Task<List<UserInfo>> ZeigeNeueUserInfoIdAn()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string UserInfoURL = URLs.URLUserInfo;

			HttpResponseMessage response = await client.GetAsync(UserInfoURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<UserInfo> userinfo = JsonSerializer.Deserialize<List<UserInfo>>(responseBody);
				var newId = userinfo;
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


		public async Task<UserInfo> HoleUserInfoById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLUserInfo}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				UserInfo userinfo = JsonSerializer.Deserialize<UserInfo>(responseBody);
				return userinfo;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}


		public async Task<UserInfo> GetUserInfoById(int IDNumber)
		{
			var UserInfo = await HoleUserInfoById(IDNumber);

			if (UserInfo == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int uiId = UserInfo.uiId;
				int katalognummer = UserInfo.katalognummer;
				string? vorname = UserInfo.vorname;
				string? nachname = UserInfo.nachname;
				string? abteilung = UserInfo.abteilung;
				bool abteilungsleiter = UserInfo.abteilungsleiter;
				double bruttogehalt = UserInfo.bruttogehalt;
				int uId = UserInfo.uId;
				return new UserInfo { };
			}
		}


		public async Task<string> PostUserInfo(int IDNumber, int Katalognummer, string Vorname, string Nachname, string Abteilung, bool Abteilungsleiter, double Bruttogehalt, int UId)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			UserInfo newUserInfo = new UserInfo { uiId = IDNumber, katalognummer = Katalognummer, vorname = Vorname, nachname = Nachname, abteilung=Abteilung, abteilungsleiter=Abteilungsleiter, bruttogehalt=Bruttogehalt, uId=UId };
			string jsonData = JsonSerializer.Serialize(newUserInfo);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLUserInfo}/", content);

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


		public async Task<String> PutUserInfo(int IDNumber, int Katalognummer, string Vorname, string Nachname, string Abteilung, bool Abteilungsleiter, double Bruttogehalt, int UId)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			UserInfo newUserInfo = new UserInfo { uiId = IDNumber, katalognummer = Katalognummer, vorname = Vorname, nachname = Nachname, abteilung = Abteilung, abteilungsleiter = Abteilungsleiter, bruttogehalt = Bruttogehalt, uId = UId };
			string jsonData = JsonSerializer.Serialize(newUserInfo);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLUserInfo}/{IDNumber}", content);

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


		public async Task<String> DeleteUserInfo(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLUserInfo}/{IDNumber}");

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