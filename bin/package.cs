//css_args /ac
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System;

void main(string[] args)
{
    var version = Directory.GetFiles(".", "CSScriptNpp.*.msi").Select(x =>Path.GetFileNameWithoutExtension(x)).First().Replace("CSScriptNpp.", "");

    Console.WriteLine("Injecting version into file names: " + version);

    var zipFile = Directory.GetFiles(".", "CSScriptNpp.zip").FirstOrDefault();
    if(zipFile != null)
    {
        string distro = Path.Combine(Path.GetDirectoryName(zipFile), Path.GetFileNameWithoutExtension(zipFile)+"."+version+".zip");
        if (File.Exists(distro))
            File.Delete(distro);
        File.Move(zipFile, distro);
        File.Copy(distro, @"E:\cs-script\cs-scriptWEB\npp\" + Path.GetFileName(distro), true);
    }

    var sevenZFile = Directory.GetFiles(".", "CSScriptNpp.7z").FirstOrDefault();
    if(sevenZFile != null)
    {
        string distro = Path.Combine(Path.GetDirectoryName(sevenZFile), Path.GetFileNameWithoutExtension(sevenZFile)+"."+version+".7z");
        if (File.Exists(distro))
            File.Delete(distro);
        File.Move(sevenZFile, distro);
    }

    File.WriteAllText(@"E:\cs-script\cs-scriptWEB\npp\latest_version.txt", version);

    File.Copy(@"E:\cs-script\cs-scriptWEB\npp\latest_version.txt", @".\latest_version.txt");
    File.Copy(@".\latest_version.txt", @".\latest_version_dbg.txt");
                  
    File.Copy(@"..\src\CSScriptNpp\Resources\WhatsNew.txt",
              @".\CSScriptNpp."+version+".ReleaseNotes.txt", true);
}