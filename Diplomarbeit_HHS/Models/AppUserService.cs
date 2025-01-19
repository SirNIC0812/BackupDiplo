using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomarbeit_HHS.Models
{
	public class AppUserService
	{
		private string _username = "xxx"; //string.Empty;
		private string _rolle = "yyy"; //string.Empty;


		public string Username
		{
			get => _username;
			set
			{
				_username = value;
				NotifyStateChanged();
			}
		}
		public string Rolle
		{
			get => _rolle;
			set
			{
				_rolle = value;
				NotifyStateChanged();
			}
		}

		public event Action? OnChange;

		private void NotifyStateChanged() => OnChange?.Invoke();
	}
}
