using DemoCollectInfoApp;
using System.Text.Json;
using Microsoft.Win32;

List<string> installs = new List<string>();
string fileName = "KeysPaths.json";

if (!File.Exists(fileName))
{
    List<KeysPath> keys = new List<KeysPath>() {
        new KeysPath(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"),
        new KeysPath(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall")
    };
    string jsonStringe = JsonSerializer.Serialize(keys, new JsonSerializerOptions
    {
        WriteIndented = true,
    });
    File.WriteAllText(fileName, jsonStringe);
}

using StreamReader r = new(fileName);
List<KeysPath>? keysFromJson = await JsonSerializer.DeserializeAsync<List<KeysPath>>(r.BaseStream);
r.Close();

if(keysFromJson == null)
{
    throw new Exception("Cannot find the json file");
}

var lminstalls = SystemLogic.FindInstalls(RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64), keysFromJson.Select(i => i.Path).ToList());
var cuinstalls = SystemLogic.FindInstalls(RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64), keysFromJson.Select(i => i.Path).ToList());
installs.AddRange(lminstalls.Select(i => i.DisplayName));
installs.AddRange(cuinstalls.Select(i => i.DisplayName));
installs = installs.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
installs.Sort(); // The list of ALL installed applications

foreach (var item in installs)
{
    Console.WriteLine(item);
}
string jsonString = JsonSerializer.Serialize(keysFromJson, new JsonSerializerOptions
{
    WriteIndented = true,
});
File.WriteAllText(fileName, jsonString);
Console.ReadLine();