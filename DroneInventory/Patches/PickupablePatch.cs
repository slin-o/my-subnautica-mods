using DroneInventory.Handlers;
using HarmonyLib;
using System.Linq;
using static HandReticle;

namespace DroneInventory.Patches;

[HarmonyPatch(typeof(Pickupable))]
internal class PickupablePatch
{
    [HarmonyPatch(nameof(Pickupable.OnHandHover))]
    [HarmonyPostfix]
    public static void HoverPostfix(Pickupable __instance, GUIHand hand)
    {


        var camera = __instance.GetComponent<MapRoomCamera>();
        if (camera != null)
        {
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, "OpenStorage", true, GameInput.Button.AltTool);
            if (!ContainerHandler.GetContainer(camera).IsEmpty())
                HandReticle.main.SetText(HandReticle.TextType.Hand, "Camera Drone is not empty - cannot pick up", true, GameInput.Button.None);
        }

        var activeCamera = MapRoomCamera.cameras.Where((x) => x.active).FirstOrDefault();
        if (activeCamera != null && __instance.isPickupable && !ContainerHandler.GetContainer(activeCamera).container.HasRoomFor(__instance))
        {
            main.SetText(HandReticle.TextType.Hand, __instance.GetTechType().AsString(false), true, GameInput.Button.None);
            main.SetText(HandReticle.TextType.HandSubscript, "InventoryFull", true, GameInput.Button.None);
        }
    }

    [HarmonyPatch(nameof(Pickupable.OnHandClick))]
    [HarmonyPrefix]
    public static bool ClickPrefix(Pickupable __instance, GUIHand hand)
    {
        var camera = __instance.GetComponent<MapRoomCamera>();
        if (camera == null)
            return true;
        if (!ContainerHandler.GetContainer(camera).IsEmpty())
            return false;
        return true;

    }
}
