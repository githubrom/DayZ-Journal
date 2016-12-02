using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Forms;
using System.Net;
using QueryMaster;

namespace DayzJournal
{
	internal class DayZProfile
	{
		internal string PlayerName;
		internal string LastMpServer;
		internal string LastMpServerName;
		internal DateTime LastWriteTime;
		internal string CfgName
		{
			get;
			set;
		}
		internal void Read()
		{
			if (!File.Exists(CfgName)) return;
			var dayZProfile = File.ReadAllText(CfgName);
			PlayerName = dayZProfile.Split(new[] { "playerName=\"" }, StringSplitOptions.None)[1].Split(new[] { "\";" }, StringSplitOptions.None)[0].Trim();
			LastMpServer = dayZProfile.Split(new[] { "lastMPServer=\"" }, StringSplitOptions.None)[1].Split(new[] { "\";" }, StringSplitOptions.None)[0].Trim();
			LastMpServerName = dayZProfile.Split(new[] { "lastMPServerName=\"" }, StringSplitOptions.None)[1].Split(new[] { "\";" }, StringSplitOptions.None)[0].Trim();
			var writeTime = File.GetLastWriteTime(CfgName).ToString(CultureInfo.CurrentCulture);
			LastWriteTime = DateTime.Parse(writeTime);
		}
	}

	internal partial class HistoryServer
	{
		internal string PlayerName { get; set; }
		internal IPEndPoint IPEndPoint 
		{
			get
			{
				IPEndPoint iPEndPoint = null;
				Uri url;
				IPAddress ip;
				if (Uri.TryCreate(String.Format("http://{0}", AddrIpPort), UriKind.Absolute, out url) &&
					IPAddress.TryParse(url.Host, out ip))
				{
					iPEndPoint = new IPEndPoint(ip, url.Port);
				}
				return iPEndPoint;
			}
		}
		internal string AddrIpPort { get; set; }
		internal string Name { get; set; }
		internal DateTime LastPlayTime { get; set; }

		internal long Ping { get; set; }
		internal string GameVersion { get; set; }
		internal int Players { get; set; }
		internal byte MaxPlayers { get; set; }
		internal string Hive { get; set; }
		internal string UpTime { get; set; }
		internal List<Player> PlayerList { get; set; }
	}

	internal class HistoryServerList
	{
		internal List<HistoryServer> Csv = new List<HistoryServer>();
		internal string FullName { get; set; }
		internal void Read()
		{
			if (string.IsNullOrEmpty(FullName))
				Csv = new List<HistoryServer>();

			if (!File.Exists(FullName))
				using (var sw = File.CreateText(FullName))
				{
					sw.WriteLine(string.Empty);
					sw.Close();
				}

			Csv = new List<HistoryServer>();
			using (var tfParser = new TextFieldParser(FullName))
			{
				tfParser.CommentTokens = new[] { "#" };
				tfParser.SetDelimiters(new[] { ";" });
				tfParser.HasFieldsEnclosedInQuotes = false;

				while (!tfParser.EndOfData)
				{
					var fields = tfParser.ReadFields();
					var server = new HistoryServer();
					try
					{
						if (fields != null) server.LastPlayTime = DateTime.Parse(fields[0]);
					}
					catch
					{
						server.LastPlayTime = new DateTime();
					}
					if (fields != null)
					{
						server.AddrIpPort = fields[1];
						server.PlayerName = fields[2];
						server.Name = fields[3];

					}
					Csv.Add(server);
				}
			}
		}
		internal void Update(DayZProfile dayzProfile)
		{
			var oldServer = (from s in Csv
							 where s.LastPlayTime == dayzProfile.LastWriteTime
							 select s)
						.ToList();

			if (oldServer.Any())
				return;

			var server = new HistoryServer
			{
				PlayerName = dayzProfile.PlayerName, 
				AddrIpPort = dayzProfile.LastMpServer,
				Name = dayzProfile.LastMpServerName,
				LastPlayTime = dayzProfile.LastWriteTime
			};
		
			Csv.Add(server);

		}
		internal void Write()
		{

			var csvStringBuilder = new StringBuilder();

			foreach (var s in Csv)
			{
				var newLine = string.Format("{0};{1};{2};{3}", s.LastPlayTime, s.AddrIpPort, s.PlayerName, s.Name);
				csvStringBuilder.AppendLine(newLine);
			}

			File.WriteAllText(FullName, csvStringBuilder.ToString());

		}
	}

	internal class DayZPlayer
	{
		public string Href { get; set; }
		public string Name { get; set; }
	}

	internal class ListViewItemComparer : IComparer
	{
		private readonly int _col;
		private readonly SortOrder _order;

		public ListViewItemComparer()
		{
			_col = 0;
			_order = SortOrder.Ascending;
		}

		public ListViewItemComparer(int column, SortOrder order)
		{
			_col = column;
			_order = order;
		}

		public int Compare(object x, object y)
		{
			int returnVal;

			var listviewX = ((ListViewItem) x).SubItems[_col].Text;
			var listviewY = ((ListViewItem) y).SubItems[_col].Text;

			DateTime firstDate;
			DateTime secondDate;

			if ((DateTime.TryParse(listviewX, out firstDate)) && (DateTime.TryParse(listviewY, out secondDate)))
				returnVal = DateTime.Compare(firstDate, secondDate);
			else
				returnVal = string.CompareOrdinal(listviewX, listviewY);

			if (_order == SortOrder.Descending)
				returnVal *= -1;
			return returnVal;
		}
	}

}
