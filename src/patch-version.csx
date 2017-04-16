using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

const string git = "git";
const string gitArgs = "describe --tags --long";

var libInfoFile = @"HolzShots.Common\LibraryInformation.cs";
Console.WriteLine($"Using Info File: {libInfoFile}");

var proc = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = git,
        Arguments = gitArgs,
        UseShellExecute = false,
        RedirectStandardOutput = true,
        CreateNoWindow = true
    }
};
proc.Start();
var gitOutput = proc.StandardOutput.ReadToEnd();

// Sample:
// v0.1.0-0-gfe83453
var m = Regex.Match(gitOutput, @"v(?<version>.*)-(?<commits>\d+)-(?<hash>[A-Za-z0-9]+)");
if (!m.Success)
    throw new InvalidDataException($"invalid match");

var version = m.Groups["version"].Value;
var commits = m.Groups["commits"].Value;
var hash = m.Groups["hash"].Value;

if (string.IsNullOrWhiteSpace(version))
    throw new InvalidDataException($"invalid {nameof(version)}");
if (string.IsNullOrWhiteSpace(commits))
    throw new InvalidDataException($"invalid {nameof(commits)}");
if (string.IsNullOrWhiteSpace(hash))
    throw new InvalidDataException($"invalid {nameof(hash)}");

var buildDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

var info = File.ReadAllText(libInfoFile);
var newInfo = info.Replace("%VERSION%", version)
    .Replace("%COMMITS-SINCE-TAG%", commits)
    .Replace("%COMMIT-ID%", hash)
    .Replace("%BUILD-DATE%", buildDate);

Console.WriteLine($"Version: {version}");
Console.WriteLine($"Commits since tag: {commits}");
Console.WriteLine($"Commit ID: {hash}");
Console.WriteLine($"Build Date: {buildDate}");

File.WriteAllText(libInfoFile, newInfo);
