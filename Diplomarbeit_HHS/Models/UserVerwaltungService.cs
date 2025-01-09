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
				List<User> bestellung = JsonSerializer.Deserialize<List<User>>(responseBody);

				Console.WriteLine(Convert.ToString(response));
				return bestellung;

			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

	}
}
