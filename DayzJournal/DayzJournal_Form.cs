using System.Collections.Generic;
using System.Globalization;
using QueryMaster;
using System;
using System.Drawing;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using System.Collections.ObjectModel;

namespace DayzJournal
{
	public partial class DshForm : Form
	{
		internal static string CfgPathName;
		internal static string CfgFileName;
		internal static string CfgFullName;
		internal static string CsvFullName;
		private FileSystemWatcher _dayzProfileWatcher;
		private static System.Timers.Timer _timer;
		private int _sortLvHistoryColumn = -1;
		private int _sortLvInfoColumn = -1;
		private static DayZProfile _dayZProfile = new DayZProfile();
		private static readonly HistoryServerList HistoryServerList = new HistoryServerList();

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

		public DshForm()
		{
			InitializeComponent();
		}

		private void lbCfgFile_DoubleClick(object sender, EventArgs e)
		{
			GetCfgFileName(false);
		}

		private void lbCsvFile_DoubleClick(object sender, EventArgs e)
		{
			GetCsvFileName(false);
		}

		private void btClose_Click(object sender, EventArgs e)
		{
			if (ActiveForm != null) ActiveForm.Close();
		}

		private void btRefresh_Click(object sender, EventArgs e)
		{
			refresh();
		}

		private void lvHistoryColumn_Click(object o, ColumnClickEventArgs e)
		{
			if (e.Column != _sortLvHistoryColumn)
			{

				_sortLvHistoryColumn = e.Column;
				lvHistory.Sorting = SortOrder.Ascending;
			}
			else
			{
				lvHistory.Sorting = lvHistory.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
			}
			lvHistory.Sort();
			lvHistory.ListViewItemSorter = new ListViewItemComparer(e.Column, lvHistory.Sorting);
		}

		private void lvInfoColumn_Click(object o, ColumnClickEventArgs e)
		{
			if (e.Column != _sortLvInfoColumn)
			{
				_sortLvInfoColumn = e.Column;
				lvInfo.Sorting = SortOrder.Ascending;
			}
			else
			{
				lvInfo.Sorting = lvInfo.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
			}
			lvInfo.Sort();
			lvInfo.ListViewItemSorter = new ListViewItemComparer(e.Column, lvInfo.Sorting);
		}


		private void DSH_Form_Load(object sender, EventArgs e)
		{
			splitContainer1.Panel2Collapsed = true;
			splitContainer1.IsSplitterFixed = true;
			splitContainer1.Panel2.Hide();
			Width = 850;

			GetCfgFileName();
			GetCsvFileName();

			_dayzProfileWatcher = new FileSystemWatcher
			{
				Path = CfgPathName,
				Filter = "*.DayZProfile",
				NotifyFilter = NotifyFilters.LastWrite
			};
			_dayzProfileWatcher.Changed += OnDayzProfileWatcherChanged;
			_dayzProfileWatcher.EnableRaisingEvents = true;

			_timer = new System.Timers.Timer(30000);
			_timer.Elapsed += OnTimerElapsed;
			_timer.Enabled = true;

			HistoryServerList.FullName = CsvFullName;
			HistoryServerList.Read();

			SetServerInfo();
			refresh();

			lvHistory.Columns[0].Width = -2;
			lvHistory.Columns[1].Width = -2;
			lvHistory.Columns[2].Width = -2;
			lvHistory.Columns[3].Width = -2;
		}

		private void GetCfgFileName(bool auto = true)
		{
			if (auto)
			{
				CfgPathName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
					"Dayz");
				CfgFileName = Environment.UserName + ".DayZProfile";
				CfgFullName = Path.Combine(CfgPathName, CfgFileName);
			}
			else
			{
				var getCfg = new OpenFileDialog
				{
					Title = "select cfg file",
					InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Dayz"),
					Filter = "*.DayZProfile|*.DayZProfile",
					CheckFileExists = true
				};
				var ret = getCfg.ShowDialog(this);
				if (ret == DialogResult.OK)
				{
					CfgFullName = getCfg.FileName;
					CfgPathName = Path.GetFileName(CfgFullName);
					CfgFileName = Path.GetDirectoryName(CfgFullName);
				}
			}
			lbCfgFile.Text = CfgFullName;
		}

		private void GetCsvFileName(bool auto = true)
		{
			if (auto)
			{
				CsvFullName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Dayz");
				CsvFullName = Path.Combine(CsvFullName, "DayzJournal.csv");
			}
			else
			{
				var getCsv = new OpenFileDialog
				{
					Title = "select csv file",
					InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Dayz"),
					Filter = "*DayzJournal.csv|*DayzJournal.csv",
					CheckFileExists = false
				};
				var ret = getCsv.ShowDialog(this);
				if (ret == DialogResult.OK)
				{
					CsvFullName = getCsv.FileName;
				}
			}
			lbCsvFile.Text = CsvFullName;
		}

		private void refresh_lvHistory()
		{
			var selectedIndexes = (from ListViewItem item in lvHistory.SelectedItems select item.Index).ToList();

			lvHistory.Items.Clear();

			foreach (var x in HistoryServerList.Csv)
			{
				string[] row = {x.LastPlayTime.ToString(CultureInfo.CurrentUICulture), x.AddrIpPort, x.PlayerName, x.Name};
				var listViewItem = new ListViewItem(row) {Tag = x};
				listViewItem.SubItems.Add(x.AddrIpPort);
				listViewItem.SubItems.Add(x.PlayerName);
				listViewItem.SubItems.Add(x.Name);
				lvHistory.Items.Add(listViewItem);
			}

			if (selectedIndexes.Any() && selectedIndexes.Count < lvHistory.Items.Count)
			{
				foreach (var i in selectedIndexes.Where(i => i < lvHistory.Items.Count))
				{
					lvHistory.Items[i].Selected = true;
				}
			}
			FlashWindow(Handle, false);
		}

		private void OnDayzProfileWatcherChanged(object source, FileSystemEventArgs e)
		{
			Thread.Sleep(1000);
			refresh();
		}

		private void OnTimerElapsed(Object source, ElapsedEventArgs e)
		{
			if (lvInfo.InvokeRequired)
				BeginInvoke(new Action(RefreshServerInfo));
		}

		private void lvHistory_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
				return;

			var listView = sender as ListView;
			if (listView == null)
				return;

			var item = listView.GetItemAt(e.X, e.Y);
			if (item == null)
				return;

			item.Selected = true;
			var contextMenu = GetContextMenu(listView, item);
			contextMenu.Show(listView, e.Location);
		}

		private ContextMenu GetContextMenu(ListView listView, ListViewItem item)
		{
			var server = (HistoryServer) item.Tag;
			var menuItem1Copy = new MenuItem
			{
				Text = string.Format("copy '{0}'", server.AddrIpPort), 
				Tag = server
			};
			menuItem1Copy.Click += menuItem1Copy_Click;

			var menuItem2Del = new MenuItem
			{
				Text =
					string.Format("delete {0} row{1}", listView.SelectedItems.Count, listView.SelectedItems.Count > 1 ? "s" : null),
				Tag = listView.SelectedItems
			};
			menuItem2Del.Click += menuItem2Del_Click;

			var menuItem3OpenLink = new MenuItem
			{
				Text = string.Format("dayzspy"),
				Tag = server
			};
			menuItem3OpenLink.Click += menuItem3OpenLink_Click;

			var menuItems = new[] {menuItem1Copy, menuItem2Del, menuItem3OpenLink};
			var contextMenu = new ContextMenu(menuItems);

			return contextMenu;
		}

		private static void menuItem1Copy_Click(object sender, EventArgs e)
		{
			var menuItem = sender as MenuItem;
			if (menuItem == null) return;
			var server = (HistoryServer) menuItem.Tag;
			if (!string.IsNullOrEmpty(server.AddrIpPort))
				Clipboard.SetText(((HistoryServer) menuItem.Tag).AddrIpPort);
		}

		private void menuItem2Del_Click(object sender, EventArgs e)
		{
			var menuItem = sender as MenuItem;
			if (menuItem != null)
			{
				var selectedListViewItemCollection = (ListView.SelectedListViewItemCollection) menuItem.Tag;
				foreach (var sv in from ListViewItem slv in selectedListViewItemCollection select (HistoryServer) slv.Tag)
				{
					Console.WriteLine(HistoryServerList.Csv.Count());
					HistoryServerList.Csv.Remove(sv);
					Console.WriteLine(HistoryServerList.Csv.Count());
				}
			}
			HistoryServerList.Write();
			refresh_lvHistory();
		}

		private static void menuItem3OpenLink_Click(object sender, EventArgs e)
		{
			var menuItem = sender as MenuItem;
			if (menuItem == null) return;
			var server = (HistoryServer) menuItem.Tag;
			if (string.IsNullOrEmpty(server.AddrIpPort)) return;
			var url = string.Format("https://www.dayzspy.com/server/{0}/live", ((HistoryServer) menuItem.Tag).AddrIpPort);
			System.Diagnostics.Process.Start(url);
		}

		private void DSH_Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.A || !e.Control) return;
			foreach (ListViewItem lvItem in lvHistory.Items)
				lvItem.Selected = true;
		}

		private void lvHistory_MouseMove(object sender, MouseEventArgs e)
		{
			if (MouseButtons != MouseButtons.Left) return;
			var lvItem = lvHistory.HitTest(e.Location).Item;
			if (lvItem != null)
			{
				lvItem.Selected = true;
			}
		}

		private void lvHistory_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			SwitchPanel();
		}

		private void SwitchPanel()
		{
			if (splitContainer1.Panel2Collapsed)
			{
				var panel1Width = splitContainer1.Panel1.Width;

				splitContainer1.Panel1.MinimumSize = new Size {Width = panel1Width};
				splitContainer1.FixedPanel = FixedPanel.Panel1;
				splitContainer1.IsSplitterFixed = true;

				Width += 250;

				splitContainer1.Panel2Collapsed = false;
				splitContainer1.Panel2.Show();
				splitContainer1.IsSplitterFixed = false;
				splitContainer1.FixedPanel = FixedPanel.None;
				splitContainer1.Panel1.MinimumSize = new Size();
				RefreshServerInfo();
				_timer.Enabled = true;
			}
			else
			{
				var panel1Width = splitContainer1.Panel1.Width;
				splitContainer1.Panel2Collapsed = true;
				splitContainer1.Panel2.Hide();
				Width -= (splitContainer1.Panel1.Width - panel1Width);
				_timer.Enabled = false;
				SetServerInfo();
			}
		}

		private void lvHistory_KeyDown(object sender, KeyEventArgs e)
		{
			if (Keys.Delete != e.KeyCode) return;
			foreach (ListViewItem listViewItem in ((ListView) sender).SelectedItems)
			{
				listViewItem.Remove();
			}
		}

		private void lvHistory_SelectedIndexChanged(object sender, EventArgs e)
		{
			_timer.Enabled = false;
			if ((!splitContainer1.Panel2Collapsed & lvHistory.SelectedItems.Count > 0))
			{
				RefreshServerInfo();
				_timer.Enabled = true;
				Console.WriteLine("RefreshServer 'lvHistory_SelectedIndexChanged'");
			}
			else
			{
				SetServerInfo();
			}
		}

		private void btGroup_Click(object sender, EventArgs e)
		{
			if (HistoryServerList == null || HistoryServerList.Csv == null || !HistoryServerList.Csv.Any()) return;
			var newServerList2 =
				(from item in HistoryServerList.Csv
					group item by new
					{
						ServerIpPort = item.AddrIpPort,
						Player = item.PlayerName,
						ServerName = item.Name
					}
					into grouped
					let maxTime = grouped.Max(i => i.LastPlayTime)
					select grouped.First(i => i.LastPlayTime == maxTime)).ToList();
			HistoryServerList.Csv = newServerList2;
			HistoryServerList.Write();
			refresh_lvHistory();
		}

		private void RefreshServerInfo()
		{
			try
			{
				if (splitContainer1.Panel2Collapsed || lvHistory.SelectedItems.Count <= 0)
					return;
				var server = (HistoryServer)lvHistory.SelectedItems[0].Tag;

				SetServerInfo();

				lbServerInfo2.ForeColor = Color.Red;
				lbServerInfo2.Text = "connecting...";

				var masterServer = MasterQuery.GetMasterServerInstance(EngineType.Source);
				var ipfilter = new IpFilter {GameAddr = server.IPEndPoint.ToString(), App = Game.DayZ};

				masterServer.GetAddresses(QueryMaster.Region.Rest_of_the_world, MasterIpCallback, ipfilter);
			}
			catch (Exception ex)
			{
				SetServerInfo();
				MessageBox.Show(ex.ToString());
			}
		}

		private void MasterIpCallback(ReadOnlyCollection<IPEndPoint> endPoints)
		{
			HistoryServer server;
			
			if (endPoints != null)
			{
				foreach (var endPoint in endPoints)
				{
					//"0.0.0.0:0" is the last address 
					if (endPoint.Address.ToString() != "0.0.0.0")
					{
						var qmServer = ServerQuery.GetServerInstance(Game.DayZ, endPoint);

						var info = qmServer.GetInfo();
						if (info != null)
						{
							server = new HistoryServer
							{
								Ping = info.Ping,
								Name = info.Name,
								GameVersion = info.GameVersion,
								Players = info.Players,
								MaxPlayers = info.MaxPlayers
							};
							//Keywords	"battleye,privHive,etm2.000000,13:06"
							var keywords = info.Extra.Keywords;

							var kWords = keywords.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
							
							server.UpTime = kWords.Last();
							server.Hive = kWords.Contains("privHive") ? ", privHive" : ", publHive";
							server.Hive = kWords.Contains("no3rd") ? server.Hive + ", 1PP" : server.Hive;

							var shard = (from k in kWords
								where k.Contains("shard")
								select k).FirstOrDefault();

							if (shard != null)
								server.Hive += string.Format(", {0}",shard);

							var foo = 0;
							Action<int> actionFoo = x => foo++;

							var getPlayers = qmServer.GetPlayers(actionFoo);
							if (getPlayers != null)
								server.PlayerList = getPlayers.ToList();

							SetServerInfoCheckInvoked(server);
							break;
						}
					}
				}
			}
			else
			{
				server = new HistoryServer {GameVersion = "offline?"};
				SetServerInfoCheckInvoked(server);
			}
		}

		internal void SetServerInfoCheckInvoked(HistoryServer server = null)
		{
			if (lvInfo.InvokeRequired)
				BeginInvoke(new Action(() => SetServerInfo(server)));
			else
				SetServerInfo(server);
		}

		internal void SetServerInfo(HistoryServer server = null)
		{
			if (server == null)
			{
				lvInfo.Items.Clear();
				headerServerInfo1.Text = null;
				headerServerInfo2.Text = null;
				lbServerInfo1.Text = null;
				lbServerInfo2.Text = null;
				Text = "DayzJournal by rom";
				lbServerInfo2.ForeColor = SystemColors.ControlText;
			}
			else
			{
				if (!string.IsNullOrEmpty(server.Name))
					Text = string.Format("DayzJournal - {0}", server.Name);
				if (server.Name != null)
					headerServerInfo1.Text = "online";
				if (server.Players > 0 || server.MaxPlayers > 0)
					headerServerInfo2.Text = string.Format("{0}/{1}", server.Players, server.MaxPlayers);
				if (server.Ping != 0)
					lbServerInfo1.Text = string.Format("ping {0} ms, uptime {1}", server.Ping, server.UpTime);
				if (server.GameVersion != null)
				{
					if (server.GameVersion.Equals("offline?"))
						lbServerInfo2.Text = server.GameVersion;
					else
					{
						lbServerInfo2.Text = string.Format("{0}{1}", server.GameVersion, server.Hive);
						lbServerInfo2.ForeColor = Color.ForestGreen;
					}
				}
				
				if (server.PlayerList != null && server.PlayerList.Any())
				{
					foreach (var player in server.PlayerList)
					{
						var lvItem = new ListViewItem(string.Format("{0:D2}:{1:D2}:{2:D2}", player.Time.Hours, player.Time.Minutes, player.Time.Seconds));
						lvItem.SubItems.Add(string.IsNullOrEmpty(player.Name) ? "player connecting..." : player.Name);
						lvInfo.Items.Add(lvItem);
					}
				}
				lvInfo.Columns[0].Width = -2;
				lvInfo.Columns[1].Width = -2;
			}
		}

	}
}