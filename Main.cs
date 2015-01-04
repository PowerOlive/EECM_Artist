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
		byte[][][] colors;
		Random rd;
		int[][][] bl;
		string key;
		int worldWidth, worldHeight, delay = 10;
		bool running, isOwner;

		public Main() {
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
		}

		void Main_Load(object sender, EventArgs e) {
			rd = new Random((int)DateTime.Now.Ticks);
			b_draw.Enabled = false;
			gb_settings.Enabled = false;
			if (!File.Exists("colors.bmp")) {
				MessageBox.Show("Error: Can not load \"colors.bmp\"");
				Close();
				return;
			}
			FileStream f = new FileStream("colors.bmp", FileMode.Open);
			Bitmap t = new Bitmap(f);
			colors = new byte[t.Height][][];
			
			for (int y = 0; y < t.Height; y++) {
				colors[y] = new byte[t.Width][];
				for (int x = 0; x < t.Width; x++) {
					Color v = t.GetPixel(x, y);
					colors[y][x] = new byte[] { v.A, v.R, v.G, v.B };
				}
			}
			f.Close();
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
				worldWidth = m.GetInt(10);
				worldHeight = m.GetInt(11);
				nu_xp.Maximum = worldWidth;
				nu_yp.Maximum = worldHeight;
				WD_parser(m, 13);
				con.Send(key + 'f', 1);
				con.Send("access", tb_code.Text);
				return;
			}
			if (m.Type == "access") {
				MessageBox.Show("Got Access.");
				return;
			}
			if (m.Type == "lostaccess") {
				if (!isOwner) {
					running = false;
					MessageBox.Show("Lost Access.");
				}
				return;
			}
			if (m.Type == "clear") {
				worldWidth = m.GetInt(0);
				worldHeight = m.GetInt(1);
				bl = new int[2][][];
				for (int l = 0; l < 2; l++) {
					bl[l] = new int[worldWidth][];
					for (int x = 0; x < worldWidth; x++) {
						bl[l][x] = new int[worldHeight];
					}
				}
				for (int x = 0; x < worldWidth; x++) {
					bl[0][x][0] = 9;
					bl[0][x][worldHeight - 1] = 9;
				}
				for (int y = 1; y < worldHeight - 1; y++) {
					bl[0][0][y] = 9;
					bl[0][worldWidth - 1][y] = 9;
				}
				return;
			}
			if (m.Type == "reset") {
				WD_parser(m, 0);
				return;
			}
			if (bl == null) return;
			if (m.Type == "b") {
				bl[m.GetInt(0)][m.GetInt(1)][m.GetInt(2)] = m.GetInt(3);
				return;
			}
			if (m.Type == "bc" || m.Type == "bs" || m.Type == "lb" || m.Type == "pt") {
				bl[0][m.GetInt(0)][m.GetInt(1)] = m.GetInt(2);
				return;
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
				bl[l] = new int[worldWidth][];
				for (int x = 0; x < worldWidth; x++) {
					bl[l][x] = new int[worldHeight];
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
				} catch(PlayerIOError err) {
					MessageBox.Show(err.Message, "Logging in failed");
				}
				if (isConnected) {
					try {
						con = cli.Multiplayer.JoinRoom(tb_wid.Text, null);
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
						MessageBox.Show(err.Message, "Joining world failed");
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
			//draw_blocks(); return;

			FileStream fb = new FileStream(ofd.FileName, FileMode.Open);
			Bitmap bt = new Bitmap(fb);
			fb.Close();
			byte[] last_color = new byte[] { 0, 0, 0, 0 };
			int last_id = 0,
				last_layer = 0;

			bool overr = true;

			int aX = 0,
				aY = 0;

			if (rb_att.Checked) {
				aX = (int)nu_xp.Value;
				aY = (int)nu_yp.Value;
				overr = false;
			}
			
			for (int y = 0; y + aY < worldHeight && y < bt.Height && running; y++) {
				for (int x = 0; x + aX < worldWidth && x < bt.Width && running; x++) {
					Color f = bt.GetPixel(x, y);
					byte[] c = new byte[]{ f.A, f.R, f.G, f.B };
					int id = 0,
						layer = 0;

					if (last_color[0] != c[0] || 
						last_color[1] != c[1] ||
						last_color[2] != c[2] || 
						last_color[3] != c[3]) {
						int diff = 0xFFFF;

						for (int j = 0; j < colors.Length; j++) {
							for (int k = 0; k < colors[j].Length; k++) {
								int d = difco(colors[j][k], c);
								if (d < diff) {
									diff = d;
									id = k;
									if (j == 1) id += 400;
									if (j == 2) {
										id += 500;
										layer = 1;
									}
								}
							}
						}

						last_id = id;
						last_layer = layer;
						last_color = c;
					} else {
						id = last_id;
						layer = last_layer;
					}
					if (bl[layer][x + aX][y + aY] != id) {
						con.Send(key, layer, x + aX, y + aY, id);
						Thread.Sleep(delay);
					}
					
					// Remove useless Backgrounds or Foreground blocks
					int other_layer = (layer == 0)? 1 : 0;
					if (bl[other_layer][x + aX][y + aY] != 0) {
						con.Send(key, other_layer, x + aX, y + aY, 0);
						Thread.Sleep(delay);
					}
				}
			}
			running = false;
			b_draw.Text = "Draw";
		}

		void draw_blocks() {
			int aX = 0,
				aY = 0;

			if (rb_att.Checked) {
				aX = (int)nu_xp.Value;
				aY = (int)nu_yp.Value;
			}

			for (int id = 0; id < 100; id++) {
				con.Send(key, 0, aX + id, aY, id);
				Thread.Sleep(delay);
				con.Send(key, 0, aX + id, aY + 1, id + 400);
				Thread.Sleep(delay);
				con.Send(key, 1, aX + id, aY + 2, id + 500);
				Thread.Sleep(delay);
			}
		}

		void rb_over_CheckedChanged(object sender, EventArgs e) {
			gb_settings.Enabled = !rb_over.Checked;
		}
	}
}
