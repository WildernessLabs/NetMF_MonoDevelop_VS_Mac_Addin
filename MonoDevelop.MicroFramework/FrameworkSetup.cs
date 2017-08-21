using System;
using MonoDevelop.Core;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;
using Mono.Unix;

namespace MonoDevelop.MicroFramework
{
	static class FrameworkSetup
	{
		static string GetChecksum (string file)
		{
			if (!File.Exists (file))
				return string.Empty;
			using (var stream = File.OpenRead (file))
			using (var sha = SHA1.Create ()) {
				byte [] checksum = sha.ComputeHash (stream);
				return BitConverter.ToString (checksum);
			}
		}

		static void SetupRegistry (string installRoot)
		{
			const string RegistryKey = "Software\\Microsoft\\.NETMicroFramework\\v4.3";
			var registryKey = Registry.CurrentUser.OpenSubKey (RegistryKey, true);
			if (registryKey == null) {
				registryKey = Registry.CurrentUser.CreateSubKey (RegistryKey);
			}
			var assemblyRoot = Path.Combine (installRoot, "frameworks", "Microsoft .NET Micro Framework", "v4.3");
			if (registryKey.GetValue ("InstallRoot")?.ToString () != assemblyRoot) {
				registryKey.SetValue ("BuildNumber", "1");
				registryKey.SetValue ("RevisionNumber", "0");
				registryKey.SetValue ("InstallRoot", assemblyRoot);
			}
			var assFolderKey = registryKey.OpenSubKey ("AssemblyFolder", true);
			if (assFolderKey == null) {
				assFolderKey = registryKey.CreateSubKey ("AssemblyFolder");
				assFolderKey.SetValue ("", "");
			}
		}

		static bool SyncFrameworkDirectory (string addinDir, string installRoot)
		{
			var newlyInstalled = false;

			if (!Directory.Exists (installRoot)) {
				Directory.CreateDirectory (installRoot);
				newlyInstalled = true;
			}

			var checkFile = Path.Combine ("frameworks", "Microsoft .NET Micro Framework", "v4.3", "Tools", "MetaDataProcessor.exe");
			if (!File.Exists (Path.Combine (installRoot, checkFile))
				|| GetChecksum (Path.Combine (installRoot, checkFile)) != GetChecksum (Path.Combine (addinDir, "files", checkFile))) {
				newlyInstalled = true;
				foreach (var folder in new [] { "xbuild", "xbuild-framework", "frameworks" })
					FileService.CopyDirectory (Path.Combine (addinDir, "files", folder), Path.Combine (installRoot, folder));
			}

			// Make sure utilities are executable
			foreach (var utility in new [] { "MetaDataProcessor.exe", "MetaDataProcessor" }) {
				var fi = new UnixFileInfo (Path.Combine (installRoot, "frameworks", "Microsoft .NET Micro Framework", "v4.3", "Tools", utility));
				var attrs = fi.FileAccessPermissions;
				attrs |= FileAccessPermissions.UserExecute;
				fi.FileAccessPermissions = attrs;
			}

			return newlyInstalled;
		}

		public static void Run ()
		{
			if (!Platform.IsWindows) {
				var addinDir = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().CodeBase).Replace ("file:", "");
				var installRoot = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData), ".NETMicroFramework");

				SetupRegistry (installRoot);
				SyncFrameworkDirectory (addinDir, installRoot);
			}
		}
	}
}

