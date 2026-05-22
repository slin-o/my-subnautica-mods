using System.Collections.Generic;

namespace PDARenameBeacon.Handlers;


internal class BeaconMenuHandler
{
    public static Dictionary<uGUI_PingEntry, Beacon> BeaconMapping = new Dictionary<uGUI_PingEntry, Beacon>();
    public static Dictionary<uGUI_PingEntry, SubName> SubMapping = new Dictionary<uGUI_PingEntry, SubName>();

    public static void UpdateMapping(uGUI_PingTab tabInstance)
    {
        BeaconMapping.Clear();
        SubMapping.Clear();

        foreach (var pingInstance in PingManager.pings.Values)
        {
            var subName = pingInstance.pingType switch {
                PingType.Seamoth => pingInstance.GetComponentInParent<Vehicle>().subName,
                PingType.Exosuit => pingInstance.GetComponentInParent<Vehicle>().subName,
                _ => null
            };
            if (subName == null || !tabInstance.entries.TryGetValue(pingInstance.Id, out var pingEntry))
                continue;
            SubMapping.Add(pingEntry, subName);
        }
        
        var beacons = BeaconManager.beacons;

        foreach (var beacon in beacons)
        {
            var pingInstance = beacon.beaconLabel.pingInstance;
            if (!tabInstance.entries.TryGetValue(pingInstance.Id, out var pingEntry))
                continue;
            BeaconMapping.Add(pingEntry, beacon);
        }
    }
}
