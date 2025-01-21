using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Diplomarbeit_HHS.Models.Rechnungswesen;

namespace Diplomarbeit_HHS.Models
{
	internal class Rechnungswesen
	{
		//Rechnungswesen Einkauf
		public class RechnungwesenEinkauf
		{
			public int errId { get; set; }
			public string? art { get; set; }
			public int rid { get; set; }
			public int lid { get; set; }
			public double brutto { get; set; }
			public Boolean rechnunginordung { get; set; }
			public double skonto { get; set; }
			public Boolean verbucht { get; set; }
			public int uiid { get; set; }
		}

		public async Task<List<RechnungwesenEinkauf>> HoleAlleRWEK()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string RechnungswesenEinkaufURL = URLs.URLRechnungswesenEinkauf;
			HttpResponseMessage response = await client.GetAsync(RechnungswesenEinkaufURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<RechnungwesenEinkauf> rwek = JsonSerializer.Deserialize<List<RechnungwesenEinkauf>>(responseBody);
				Console.WriteLine(Convert.ToString(response));
				return rwek;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<RechnungwesenEinkauf>> ZeigeNeueIdAnRWEK()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string RechnungswesenEinkaufURL = URLs.URLRechnungswesenEinkauf;

			HttpResponseMessage response = await client.GetAsync(RechnungswesenEinkaufURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<RechnungwesenEinkauf> rwek = JsonSerializer.Deserialize<List<RechnungwesenEinkauf>>(responseBody);
				var newId = rwek;
				newId.Select(b => b.errId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<RechnungwesenEinkauf> HoleRWEKById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLRechnungswesenEinkauf}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				RechnungwesenEinkauf rwek = JsonSerializer.Deserialize<RechnungwesenEinkauf>(responseBody);
				return rwek;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<RechnungwesenEinkauf> GetRWEKById(int IDNumber)
		{
			var rwek = await HoleRWEKById(IDNumber);

			if (rwek == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int ErrId = rwek.errId;
				string? Art = rwek.art;
				int Rid = rwek.rid;
				int Lid = rwek.lid;
				double Brutto = rwek.brutto;
				Boolean Rechnunginordung = rwek.rechnunginordung;
				double Skonto = rwek.skonto;
				Boolean Verbucht = rwek.verbucht;
				int Uiid = rwek.uiid;
				return new RechnungwesenEinkauf { };
			}
		}


		public async Task<string> PostRWEK(int IDNumber, string? Art, int Rid, int Lid, double Brutto, Boolean Rechnunginordung, double Skonto, Boolean Verbucht, int Uiid)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			RechnungwesenEinkauf newRechnungwesenEinkauf = new RechnungwesenEinkauf { errId = IDNumber, art = Art, rid = Rid, lid = Lid, brutto = Brutto, rechnunginordung = Rechnunginordung, skonto = Skonto, verbucht = Verbucht, uiid = Uiid };
			string jsonData = JsonSerializer.Serialize(newRechnungwesenEinkauf);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLRechnungswesenEinkauf}/", content);

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

		public async Task<String> PutRWEK(int IDNumber, string? Art, int Rid, int Lid, double Brutto, Boolean Rechnunginordung, double Skonto, Boolean Verbucht, int Uiid)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			RechnungwesenEinkauf newRechnungwesenEinkauf = new RechnungwesenEinkauf { errId = IDNumber, art = Art, rid = Rid, lid = Lid, brutto = Brutto, rechnunginordung = Rechnunginordung, skonto = Skonto, verbucht = Verbucht, uiid = Uiid };
			string jsonData = JsonSerializer.Serialize(newRechnungwesenEinkauf);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLRechnungswesenEinkauf}/{IDNumber}", content);

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

		public async Task<String> DeleteRWEK(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLRechnungswesenEinkauf}/{IDNumber}");

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



		//Rechnungswesen Verkauf
		public class RechnungwesenVerkauf
		{
			public int vrrId { get; set; }
			public string? art { get; set; }
			public int rid { get; set; }
			public int kid { get; set; }
			public double brutto { get; set; }
			public Boolean rechnungordung { get; set; }
			public double skonto { get; set; }
			public int dvid { get; set; }
			public Boolean verbucht { get; set; }
			public int uiid { get; set; }
		}

		public async Task<List<RechnungwesenVerkauf>> HoleAlleRWVK()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string RechnungswesenVerkaufURL = URLs.URLRechnungswesenVerkauf;
			HttpResponseMessage response = await client.GetAsync(RechnungswesenVerkaufURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<RechnungwesenVerkauf> rwvk = JsonSerializer.Deserialize<List<RechnungwesenVerkauf>>(responseBody);
				Console.WriteLine(Convert.ToString(response));
				return rwvk;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<RechnungwesenVerkauf>> ZeigeNeueIdAnRWVK()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string RechnungswesenVerkaufURL = URLs.URLRechnungswesenVerkauf;
			HttpResponseMessage response = await client.GetAsync(RechnungswesenVerkaufURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<RechnungwesenVerkauf> rwvk = JsonSerializer.Deserialize<List<RechnungwesenVerkauf>>(responseBody);
				var newId = rwvk;
				newId.Select(b => b.vrrId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<RechnungwesenVerkauf> HoleRWVKById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLRechnungswesenVerkauf}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				RechnungwesenVerkauf rwvk = JsonSerializer.Deserialize<RechnungwesenVerkauf>(responseBody);
				return rwvk;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<RechnungwesenVerkauf> GetRWVKById(int IDNumber)
		{
			var rwvk = await HoleRWVKById(IDNumber);

			if (rwvk == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int VrrId = rwvk.vrrId;
				string? Art = rwvk.art;
				int Rid = rwvk.rid;
				int Kid = rwvk.kid;
				double Brutto = rwvk.brutto;
				Boolean Rechnunginordung = rwvk.rechnungordung;
				double Skonto = rwvk.skonto;
				int Dvid = rwvk.dvid;
				Boolean Verbucht = rwvk.verbucht;
				int Uiid = rwvk.uiid;

				return new RechnungwesenVerkauf { };
			}
		}

		public async Task<string> PostRWVK(int IDNumber, string? Art, int Rid, int Kid, double Brutto, Boolean Rechnunginordung, double Skonto, int Dvid, Boolean Verbucht, int Uiid)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			RechnungwesenVerkauf newRechnungwesenVerkauf = new RechnungwesenVerkauf { vrrId = IDNumber, art = Art, rid = Rid, kid = Kid, brutto = Brutto, rechnungordung = Rechnunginordung, skonto = Skonto, dvid=Dvid, verbucht = Verbucht, uiid = Uiid };
			string jsonData = JsonSerializer.Serialize(newRechnungwesenVerkauf);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLRechnungswesenVerkauf}/", content);

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

		public async Task<String> PutRWVK(int IDNumber, string? Art, int Rid, int Kid, double Brutto, Boolean Rechnunginordung, double Skonto, int Dvid, Boolean Verbucht, int Uiid)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			RechnungwesenVerkauf newRechnungwesenVerkauf = new RechnungwesenVerkauf { vrrId = IDNumber, art = Art, rid = Rid, kid = Kid, brutto = Brutto, rechnungordung = Rechnunginordung, skonto = Skonto, dvid = Dvid, verbucht = Verbucht, uiid = Uiid };
			string jsonData = JsonSerializer.Serialize(newRechnungwesenVerkauf);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLRechnungswesenVerkauf}/{IDNumber}", content);

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


		public async Task<String> DeleteRWVK(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLRechnungswesenVerkauf}/{IDNumber}");

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



		//Rechnungswesen Personal
		public class RechnungwesenPersonal
		{
			public int prrId { get; set; }
			public string? art { get; set; }
			public int prid { get; set; }
			public int lid { get; set; }
			public double brutto { get; set; }
			public Boolean rechnungordung { get; set; }
			public double skonto { get; set; }
			public Boolean verbucht { get; set; }
			public int uiid { get; set; }
		}

		public async Task<List<RechnungwesenPersonal>> HoleAlleRWPE()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string RechnungswesenPersonalURL = URLs.URLRechnungswesenPersonal;
			HttpResponseMessage response = await client.GetAsync(RechnungswesenPersonalURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<RechnungwesenPersonal> rwpe = JsonSerializer.Deserialize<List<RechnungwesenPersonal>>(responseBody);
				Console.WriteLine(Convert.ToString(response));
				return rwpe;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<List<RechnungwesenPersonal>> ZeigeNeueIdAnRWPE()
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string RechnungswesenPersonalURL = URLs.URLRechnungswesenPersonal;
			HttpResponseMessage response = await client.GetAsync(RechnungswesenPersonalURL);

			if (response.IsSuccessStatusCode)
			{
				string responseBody = await response.Content.ReadAsStringAsync();
				List<RechnungwesenPersonal> rwpe = JsonSerializer.Deserialize<List<RechnungwesenPersonal>>(responseBody);
				var newId = rwpe;
				newId.Select(b => b.prrId).ToList();
				Console.WriteLine(Convert.ToString(response));
				return newId;
			}
			else
			{
				string output = "\nFehlermeldung: " + response.StatusCode;
				return null;
			}
		}

		public async Task<RechnungwesenPersonal> HoleRWPEById(int id)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage GetResponse = await client.GetAsync($"{URLs.URLRechnungswesenPersonal}/{id}");

			if (GetResponse.IsSuccessStatusCode)
			{
				var responseBody = await GetResponse.Content.ReadAsStringAsync();
				RechnungwesenPersonal rwpe = JsonSerializer.Deserialize<RechnungwesenPersonal>(responseBody);
				return rwpe;
			}
			else
			{
				string error = "Error TestAPIGET: " + GetResponse.StatusCode;
				return null;
			}
		}

		public async Task<RechnungwesenPersonal> GetRWPEById(int IDNumber)
		{
			var rwpe = await HoleRWPEById(IDNumber);

			if (rwpe == null)
			{
				string error = "Ihre Werte sind null, da hat was ned funkt";
				return null;
			}
			else
			{
				int PrrId = rwpe.prrId;
				string? Art = rwpe.art;
				int Prid = rwpe.prid;
				int Lid = rwpe.lid;
				double Brutto = rwpe.brutto;
				Boolean Rechnungordung = rwpe.rechnungordung;
				double Skonto = rwpe.skonto;
				Boolean Verbucht = rwpe.verbucht;
				int Uiid = rwpe.uiid;
				return new RechnungwesenPersonal { };
			}
		}


		public async Task<string> PostRWPE(int IDNumber, string? Art, int Prid, int Lid, double Brutto, Boolean Rechnungordung, double Skonto, Boolean Verbucht, int Uiid)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			RechnungwesenPersonal newRechnungwesenPersonal = new RechnungwesenPersonal { prrId = IDNumber, art = Art, prid = Prid, lid = Lid, brutto = Brutto, rechnungordung = Rechnungordung, skonto = Skonto, verbucht = Verbucht, uiid = Uiid };
			string jsonData = JsonSerializer.Serialize(newRechnungwesenPersonal);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync($"{URLs.URLRechnungswesenPersonal}/", content);

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

		public async Task<String> PutRWPE(int IDNumber, string? Art, int Prid, int Lid, double Brutto, Boolean Rechnungordung, double Skonto, Boolean Verbucht, int Uiid)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			RechnungwesenPersonal newRechnungwesenPersonal = new RechnungwesenPersonal { prrId = IDNumber, art = Art, prid = Prid, lid = Lid, brutto = Brutto, rechnungordung = Rechnungordung, skonto = Skonto, verbucht = Verbucht, uiid = Uiid };
			string jsonData = JsonSerializer.Serialize(newRechnungwesenPersonal);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PutAsync($"{URLs.URLRechnungswesenPersonal}/{IDNumber}", content);

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


		public async Task<String> DeleteRWPE(int IDNumber)
		{
			HttpClient client = new HttpClient();
			var token = await SecureStorage.GetAsync("authToken");
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			HttpResponseMessage response = await client.DeleteAsync($"{URLs.URLRechnungswesenPersonal}/{IDNumber}");

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
