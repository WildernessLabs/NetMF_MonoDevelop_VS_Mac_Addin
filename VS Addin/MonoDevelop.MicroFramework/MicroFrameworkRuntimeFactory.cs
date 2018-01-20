using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MonoDevelop.Core;
using MonoDevelop.Core.AddIns;
using MonoDevelop.Core.Assemblies;

namespace MonoDevelop.MicroFramework
{
	public class MicroFrameworkRuntimeFactory : ITargetRuntimeFactory
	{
		static MicroFrameworkRuntimeFactory ()
		{
			/* We use this tricky to run before MonoTargetRuntimeFactory
			 * so that we can setup the framework locally and inject
			 * the extra reference assembly path via environment variable
			 */
			if (!Platform.IsWindows) {
				FrameworkSetup.Run ();

				const string ExtraFrameworkEnvironmentName = "XBUILD_FRAMEWORK_FOLDERS_PATH";
				var existingValue = Environment.GetEnvironmentVariable (ExtraFrameworkEnvironmentName);
				var extraFramework = Path.Combine (FrameworkSetup.InstallRoot, "xbuild-framework");
				if (!string.IsNullOrEmpty (existingValue))
					extraFramework = existingValue + Path.PathSeparator + extraFramework;
				Environment.SetEnvironmentVariable (ExtraFrameworkEnvironmentName, extraFramework);
			}
		}

		public IEnumerable<TargetRuntime> CreateRuntimes ()
		{
			return Enumerable.Empty<TargetRuntime> ();
		}
	}
}
