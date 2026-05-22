using UnityEngine;
using UnityEngine.EventSystems;

namespace PDARenameBeacon.Handlers;

public class PingEntryClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Plugin.Config.enabled)
            return;
        var ping_entry = eventData.pointerPress.GetComponent<uGUI_PingEntry>();
        var beaconFound = BeaconMenuHandler.BeaconMapping.TryGetValue(ping_entry, out var beacon);
        var subFound = BeaconMenuHandler.SubMapping.TryGetValue(ping_entry, out var sub);
        if (beaconFound) 
            RenameHandler.Rename(beacon);
        if (!Plugin.Config.renameVehicles)
            return;
        if (subFound)
            RenameHandler.Rename(sub);

    }
}
