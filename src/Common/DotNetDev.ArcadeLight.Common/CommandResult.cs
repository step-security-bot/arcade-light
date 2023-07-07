// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Diagnostics;
using System.Text;

namespace DotNetDev.ArcadeLight.Common
{
#pragma warning disable CA1815 // Override equals and operator equals on value types
  public struct CommandResult
#pragma warning restore CA1815 // Override equals and operator equals on value types
  {
    public static readonly CommandResult Empty;

    public ProcessStartInfo StartInfo { get; }
    public int ExitCode { get; }
    public string StdOut { get; }
    public string StdErr { get; }

    public CommandResult(ProcessStartInfo startInfo, int exitCode, string stdOut, string stdErr)
    {
      StartInfo = startInfo;
      ExitCode = exitCode;
      StdOut = stdOut;
      StdErr = stdErr;
    }

    public void EnsureSuccessful(bool suppressOutput = false)
    {
      if (ExitCode != 0)
      {
        StringBuilder message = new StringBuilder($"Command failed with exit code {ExitCode}: {StartInfo.FileName} {StartInfo.Arguments}");

        if (!suppressOutput)
        {
          if (!string.IsNullOrEmpty(StdOut))
          {
            message.AppendLine($"{Environment.NewLine}Standard Output:{Environment.NewLine}{StdOut}");
          }

          if (!string.IsNullOrEmpty(StdErr))
          {
            message.AppendLine($"{Environment.NewLine}Standard Error:{Environment.NewLine}{StdErr}");
          }
        }

        throw new ArgumentException(message.ToString());
      }
    }
  }
}
