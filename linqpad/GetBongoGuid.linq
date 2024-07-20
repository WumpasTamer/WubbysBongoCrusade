<Query Kind="Statements">
  <NuGetReference>SharpDX.DirectInput</NuGetReference>
  <Namespace>SharpDX.DirectInput</Namespace>
</Query>

#define _DUMPALL // Remove underscore to see ALL input devices

var directInput = new DirectInput();

Console.WriteLine($"Enumerating input devices...");
// Enuerate all inputs, store em off
var devicesPre = ((DeviceType[])Enum.GetValues(typeof(DeviceType)))
	.SelectMany(dt => 
		directInput.GetDevices(dt, DeviceEnumerationFlags.AllDevices))
		.ToArray();
		
// Extract all the guids for easier post compare		
var instanceGuids = devicesPre.Select(p => p.InstanceGuid).ToArray();

#if DUMPALL
devicesPre.Dump("Devices before bongos");
#endif

Console.WriteLine($"Waiting for user to plug in bongos (hit enter after connected)");

// Wait for ranga to plug in bongos
Console.ReadLine();

// Enumerate inputs again, then select the one(s) that don't appear in the original list
var bongoCandidate = ((DeviceType[])Enum.GetValues(typeof(DeviceType)))
	.SelectMany(dt =>
		directInput.GetDevices(dt, DeviceEnumerationFlags.AllDevices))
		.Where(d => !instanceGuids.Contains(d.InstanceGuid))
		.ToList()
		.FirstOrDefault();

// Show the new input device, 'InstanceGuid' is whaet you are looking for

// if linqpad
bongoCandidate.Dump("Bongos");
// if in Visual Studio/ VS code
//Console.WriteLine($"{bongoCandidate.InstanceGuid}");

Enumerable.Range(0, 3).ToList().ForEach(_ => { Console.WriteLine("HFT"); Thread.Sleep(300); });