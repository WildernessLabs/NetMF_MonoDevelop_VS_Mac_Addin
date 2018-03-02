using System;
using MonoDevelop.Projects.MSBuild;

namespace MonoDevelop.MicroFramework
{
		static class Extensions
		{
			public static void AddImportIfMissing (this MSBuildProject project, string name, string condition = null)
			{
				var existing = project.GetImport (name, condition);
				if (existing == null)
					project.AddNewImport (name, condition);
			}

			public static void RemoveProperty (this MSBuildProject project, string name)
			{
				foreach (var group in project.PropertyGroups)
					group.RemoveProperty (name);
			}
        }
 }