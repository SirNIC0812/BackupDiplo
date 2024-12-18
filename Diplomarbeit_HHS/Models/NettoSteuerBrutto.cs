using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomarbeit_HHS.Models
{
	internal class NettoSteuerBrutto
	{
		public class NettoBruttoRechner
		{
			public int netto { get; set; }
			public int steuer { get; set; }
			public int brutto { get; set; }
		}

		public async Task<string> NettoBruttoRechnerRechnen(double netto)
		{
			double steuer = netto / 5;
			double steuerRunden = Math.Round(steuer, 2);
			double brutto = netto + steuer;
			double bruttoRunden = Math.Round(brutto, 2);
			string ausgabe = "Netto: " + netto + "\nSteuer: " + steuerRunden + "\nBrutto: " + bruttoRunden;
			return ausgabe;
		}
		public async Task<double> NettoSteuer(double netto)
		{
			double steuer = netto / 5;
			double steuerRunden = Math.Round(steuer, 2);
			return steuerRunden;
		}

		public async Task<double> NettoBrutto(double netto)
		{
			double brutto = netto / 5 * 6;
			double bruttoRunden = Math.Round(brutto, 2);
			return bruttoRunden;
		}
	}
}
