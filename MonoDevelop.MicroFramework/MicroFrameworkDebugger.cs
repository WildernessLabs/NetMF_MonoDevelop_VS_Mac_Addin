using Mono.Debugging.Client;
using MonoDevelop.Core.Execution;
using MonoDevelop.Debugger;

namespace MonoDevelop.MicroFramework
{
	public class MicroFrameworkDebugger : IDebuggerEngine
	{
		public bool CanDebugCommand(ExecutionCommand command)
		{
			return command as MicroFrameworkExecutionCommand != null;
		}

		public DebuggerStartInfo CreateDebuggerStartInfo(ExecutionCommand command)
		{
			return new MicroFrameworkDebuggerStartInfo(command as MicroFrameworkExecutionCommand);
		}

		public DebuggerSession CreateSession()
		{
			return new MicroFrameworkDebuggerSession();
		}

		public ProcessInfo[] GetAttachableProcesses()
		{
			return new ProcessInfo[0];
		}
	}
}
