using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diplomarbeit_HHS.Models
{
	internal class Personal
	{

		public class PersonalInhalt
		{
			public int pId { get; set; }
			public int sv { get; set; }
			public double lStbemaessung { get; set; }
			public double lSt { get; set; }
			public double nettoG { get; set; }
			public double dga { get; set; }
			public double mvk { get; set; }
			public double db { get; set; }
			public double dz { get; set; }
			public double kSt { get; set; }
			public double ubAbgabe { get; set; }
			public int uiid { get; set; }
			public string? zeitraum { get; set; }
			public bool gezahlt { get; set; }
		}

		public async Task<List<PersonalInhalt>> HoleAllePersonalInhalt()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string PersonalURL = URLs.URLPersonal;

			HttpResponseMessage response = await client.GetAsync(PersonalURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<PersonalInhalt> personalinhalt = JsonSerializer.Deserialize<List<PersonalInhalt>>(responseBody);

				Console.WriteLine(Convert.ToString(response));
				return personalinhalt;

			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<PersonalInhalt>> ZeigeNeueIdAn()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string PersonalURL = URLs.URLPersonal;

			HttpResponseMessage response = await client.GetAsync(PersonalURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<PersonalInhalt> personalinhalt = JsonSerializer.Deserialize<List<PersonalInhalt>>(responseBody);
				var newId = personalinhalt;
				newId.Select(b => b.pId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<PersonalInhalt> HolePersonalById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLPersonal}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				PersonalInhalt kunde = JsonSerializer.Deserialize<PersonalInhalt>(responseBody);
				return kunde;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}


		public async Task<PersonalInhalt> GetPersonalById(int IDNumber)
		{
			var Personal = await HolePersonalById(IDNumber);

			if (Personal == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int PId = Personal.pId;
				int Sv = Personal.sv;
				double LStbemaessung = Personal.lStbemaessung;
				double LSt = Personal.lSt;
				double NettoG = Personal.nettoG;
				double Dga = Personal.dga;
				double Mvk = Personal.mvk;
				double Db = Personal.db;
				double Dz = Personal.dz;
				double KSt = Personal.kSt;
				double UbAbgabe = Personal.ubAbgabe;
				int Uiid = Personal.uiid;
				string? Zeitraum = Personal.zeitraum;
				bool Gezahlt = Personal.gezahlt;

				return new PersonalInhalt { };
			}
		}


		public async Task<string> PostPersonal(int IDNumber, int Sv, double LStbemaessung, double LSt, double NettoG, double Dga, double Mvk, double Db, double Dz, double KSt, double UbAbgbe, int Uiid, string Zeitraum, bool Gezahlt)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			PersonalInhalt newPersonalinhalt = new PersonalInhalt { pId = IDNumber, sv = Sv, lStbemaessung=LStbemaessung, lSt=LSt, nettoG=NettoG, dga=Dga, mvk=Mvk, db=Db, dz=Dz, kSt=KSt, ubAbgabe=UbAbgbe, uiid=Uiid, zeitraum=Zeitraum, gezahlt=Gezahlt };
			string jsonData = JsonSerializer.Serialize(newPersonalinhalt);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLPersonal}/", content);

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

		public async Task<String> PutPersonal(int IDNumber, int Sv, double LStbemaessung, double LSt, double NettoG, double Dga, double Mvk, double Db, double Dz, double KSt, double UbAbgbe, int Uiid, string Zeitraum, bool Gezahlt)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			PersonalInhalt newPersonalinhalt = new PersonalInhalt { pId = IDNumber, sv = Sv, lStbemaessung = LStbemaessung, lSt = LSt, nettoG = NettoG, dga = Dga, mvk = Mvk, db = Db, dz = Dz, kSt = KSt, ubAbgabe = UbAbgbe, uiid = Uiid, zeitraum = Zeitraum, gezahlt = Gezahlt };
			string jsonData = JsonSerializer.Serialize(newPersonalinhalt);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLPersonal}/{IDNumber}", content);

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


		public async Task<String> DeletePersonal(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLPersonal}/{IDNumber}");

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
