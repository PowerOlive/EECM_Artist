using System;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using PlayerIOClient;

namespace BB_Artist {
	public partial class Main : Form {
		Client cli;
		Connection con;
		byte[][] FG, BG;
		Random rd;
		int[][][] bl;
		string key;
		int wX, wY, delay = 10;
		bool running, isOwner;

		public Main() {
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
		}

		void Main_Load(object sender, EventArgs e) {
			rd = new Random((int)DateTime.Now.Ticks);
			b_draw.Enabled = false;
			gb_settings.Enabled = false;
			if (File.Exists("colors.bmp") && File.Exists("colors2.bmp")) {
				FileStream f = new FileStream("colors.bmp", FileMode.Open);
				Bitmap t = new Bitmap(f);
				FG = new byte[t.Width][];
				for (int i = 0; i < t.Width; i++) {
					Color v = t.GetPixel(i, 0);
					FG[i] = new byte[] { v.A, v.R, v.G, v.B };
				}
				f.Close();
				f = new FileStream("colors2.bmp", FileMode.Open);
				t = new Bitmap(f);
				BG = new byte[t.Width][];
				for (int i = 0; i < t.Width; i++) {
					Color v = t.GetPixel(i, 0);
					BG[i] = new byte[] { v.A, v.R, v.G, v.B };
				}
				f.Close();
			} else {
				MessageBox.Show("Error: Can not load \"colors.bmp\" or \"colors2.bmp\"");
				Close();
			}
		}

		void Main_Closing(object sender, FormClosingEventArgs e) {
			running = false; 
			if (con != null) {
				if (con.Connected) {
					con.Disconnect();
				}
			}
			Thread.Sleep(500);
			con = null;
			cli = null;
		}

		void OnMsg(object sender, PlayerIOClient.Message m) {
			if (m.Type == "init") {
				key = getkey(m.GetString(3));
				isOwner = m.GetBoolean(9);
				wX = m.GetInt(10);
				wY = m.GetInt(11);
				nu_xp.Maximum = wX;
				nu_yp.Maximum = wY;
				WD_parser(m, 13);
				con.Send(key + 'f', 1);
				con.Send("access", tb_code.Text);
			} else if (m.Type == "access") {
				MessageBox.Show("Got Access.");
			} else if (m.Type == "lostaccess") {
				if (!isOwner) {
					running = false;
					MessageBox.Show("Lost Access.");
				}
			} else if (m.Type == "clear") {
				wX = m.GetInt(0);
				wY = m.GetInt(1);
				bl = new int[2][][];
				for (int l = 0; l < 2; l++) {
					bl[l] = new int[wX][];
					for (int x = 0; x < wX; x++) {
						bl[l][x] = new int[wY];
					}
				}
				for (int x = 0; x < wX; x++) {
					bl[0][x][0] = 9;
					bl[0][x][wY - 1] = 9;
				}
				for (int y = 1; y < wY - 1; y++) {
					bl[0][0][y] = 9;
					bl[0][wX - 1][y] = 9;
				}
			} else if (m.Type == "reset") {
				WD_parser(m, 0);
			} else if (m.Type == "b" && bl != null) {
				bl[m.GetInt(0)][m.GetInt(1)][m.GetInt(2)] = m.GetInt(3);
			} else if ((m.Type == "bc" || m.Type == "bs" || m.Type == "lb" || m.Type == "pt") && bl != null) {
				bl[0][m.GetInt(0)][m.GetInt(1)] = m.GetInt(2);
			}
		}

		int difco(byte[] c, byte[] k) {
			int e = 0;
			e += Math.Abs(c[0] - k[0]);
			e += Math.Abs(c[1] - k[1]);
			e += Math.Abs(c[2] - k[2]);
			e += Math.Abs(c[3] - k[3]);
			return e;
		}

		string getkey(string arg1) {
			int num = 0;
			string str = "";
			for (int i = 0; i < arg1.Length; i++) {
				num = arg1[i];
				if ((num >= 0x61) && (num <= 0x7a)) {
					if (num > 0x6d) num -= 13; else num += 13;
				} else if ((num >= 0x41) && (num <= 90)) {
					if (num > 0x4d) num -= 13; else num += 13;
				}
				str += ((char)num);
			}
			return str;
		}

		void WD_parser(PlayerIOClient.Message m, uint c) {
			bl = new int[2][][];
			for (int l = 0; l < 2; l++) {
				bl[l] = new int[wX][];
				for (int x = 0; x < wX; x++) {
					bl[l][x] = new int[wY];
				}
			}
			while (c < m.Count) {
				int bid = m.GetInt(c);
				int l = m.GetInt(c + 1);
				byte[] pX = m.GetByteArray(c + 2);
				byte[] pY = m.GetByteArray(c + 3);
				for (int n = 0; n < pX.Length; n += 2) {
					int x = pX[n] << 8 | pX[n + 1],
						y = pY[n] << 8 | pY[n + 1];
					bl[l][x][y] = bid;
				}
				if (bid == 43 || bid == 77 || bid == 83 || bid == 1000)
					c += 5;
				else if (bid == 242)
					c += 7;
				else
					c += 4;
			}
		}

		void b_con_Click(object sender, EventArgs e) {
			b_con.Enabled = false;
			bool isConnected = false;
			if (con != null) {
				if (con.Connected) {
					isConnected = true;
				}
			}
			if (!isConnected) {
				try {
					cli = PlayerIO.QuickConnect.SimpleConnect("eehw11-uzvrgq6urkyo6tdtrgm1ra", tb_mail.Text, tb_pass.Text);
					isConnected = true;
				} catch {
					MessageBox.Show("Can not connect, check your login data.");
				}
				if (isConnected) {
					try {
						con = cli.Multiplayer.CreateJoinRoom(tb_wid.Text, "EE5", true, null, null);
						con.OnMessage += new MessageReceivedEventHandler(OnMsg);
						con.OnDisconnect += delegate(object o, string s) {
							running = false;
							b_draw.Enabled = false;
							b_con.Text = "Connect";
							b_draw.Text = "Draw";
							MessageBox.Show("Disconected");
						};
						con.Send("botinit");
						b_con.Text = "Disconnect";
						b_draw.Enabled = true;
					} catch(PlayerIOError err) {
						MessageBox.Show("Can not open or join the room." + err.Message);
					}
				}
			} else {
				running = false;
				b_con.Text = "Connect";
				Thread.Sleep(500);
				if (con != null) {
					if (con.Connected) {
						con.Disconnect();
					}
				}
				con = null;
			}
			b_con.Enabled = true;
		}

		void nu_delay_ValueChanged(object sender, EventArgs e) {
			delay = 1;
			int.TryParse(nu_delay.Value.ToString(), out delay);
			if (delay < 1 || delay > 100) {
				delay = 1;
			}
		}

		void b_draw_Click(object sender, EventArgs e) {
			if (!running) {
				if (ofd.ShowDialog() == DialogResult.OK) {
					if (File.Exists(ofd.FileName)) {
						bool isConected = false;
						if (con != null) {
							if (con.Connected) {
								isConected = true;
							}
						}
						if (isConected) {
							b_draw.Text = "Stop";
							running = true;
							Thread thr = new Thread(building);
							thr.Start();
						} else MessageBox.Show("Error: Not connected.");
					} else MessageBox.Show("Error: File does not exist.");
				}
			} else {
				running = false;
				Thread.Sleep(500);
				b_draw.Text = "Draw";
			}
		}

		void building() {
			FileStream fb = new FileStream(ofd.FileName, FileMode.Open);
			Bitmap bt = new Bitmap(fb);
			fb.Close();
			byte[] last_color = new byte[]{ 0, 0, 0, 0 };
			int last_id = 0;
			bool last_layer = true,
				overr = true;
			int aX = 0,
				aY = 0;
			if (rb_att.Checked) {
				aX = (int)nu_xp.Value;
				aY = (int)nu_yp.Value;
				overr = false;
			}
			
			for (int y = 0; y + aY < wY && y < bt.Height && running; y++) {
				for (int x = 0; x + aX < wX && x < bt.Width && running; x++) {
					Color f = bt.GetPixel(x, y);
					byte[] c = new byte[]{ f.A, f.R, f.G, f.B };
					int b = 0;
					byte layer = 0;

					if (last_color[0] != c[0] || 
						last_color[1] != c[1] ||
						last_color[2] != c[2] || 
						last_color[3] != c[3]) {
						int dif = 9999;
						
						for (int i = 0; i < FG.Length; i++) {
							int d = difco(FG[i], c);
							if (d < dif) {
								dif = d;
								b = i;
							}
						}
						for (int i = 0; i < BG.Length; i++) {
							int d = difco(BG[i], c);
							if (d < dif) {
								dif = d;
								b = i + 500;
								layer = 1;
							}
						}
						last_id = b;
						last_layer = layer;
						last_color = c;
					} else {
						b = last_id;
						layer = last_layer;
					}
					if (bl[layer][x + aX][y + aY] != b) {
						con.Send(key, layer, x + aX, y + aY, b);
						Thread.Sleep(delay);
					}
					
					// Remove useless Backgrounds or Foreground blocks
					byte other_layer = (layer == 0)? 1 : 0;
					if (bl[other_layer][x + aX][y + aY] != 0) {
						con.Send(key, other_layer, x + aX, y + aY, 0);
						Thread.Sleep(delay);
					}
				}
			}
			try {
				running = false;
				b_draw.Text = "Draw";
			} catch { }
		}

		void rb_over_CheckedChanged(object sender, EventArgs e) {
			gb_settings.Enabled = !rb_over.Checked;
		}
	}
}
