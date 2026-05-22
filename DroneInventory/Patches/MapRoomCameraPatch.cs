using DroneInventory.Handlers;
using HarmonyLib;

namespace DroneInventory.Patches;

[HarmonyPatch(typeof(MapRoomCamera))]
internal class MapRoomCameraPatch
{
    [HarmonyPatch(nameof(MapRoomCamera.HandleInput))]
    [HarmonyPostfix]
    public static void InputPostfix(MapRoomCamera __instance)
    {
        if (!__instance.IsControlled())
            return;
        if (GameInput.GetButtonDown(GameInput.Button.LeftHand))
            TargetHandler.Click(__instance);
    }

    [HarmonyPatch(nameof(MapRoomCamera.Update))]
    [HarmonyPostfix]
    public static void UpdatePostfix(MapRoomCamera __instance)
    {
        if (__instance.IsControlled())
        {
            TargetHandler.Hover(__instance);
        }

        var activeCamera = Player.main?.guiHand?.activeTarget?.GetComponent<MapRoomCamera>();

        if (activeCamera == __instance) {
            if (GameInput.GetButtonDown(GameInput.Button.AltTool))
            {
                ContainerHandler.OpenCamera(__instance);
            }
        }


        //if (Inventory.main.GetHeldTool() != __instance ||
        //    !ContainerHandler.StorageContainers.TryGetValue(__instance, out StorageContainer storageContainer) ||
        //    storageContainer == null) return;
        //if (storageContainer.GetOpen() || IngameMenu.main.selected) return;
        //if (!Targeting.GetTarget(Player.main.gameObject, 2f, out var activeTarget, out _)) return;
        //GameObject rootTarget = UWE.Utils.GetEntityRoot(activeTarget) ?? activeTarget;

    }


    [HarmonyPatch(nameof(MapRoomCamera.Start))]
    [HarmonyPrefix]
    public static void StartPrefix(MapRoomCamera __instance)
    {
        ContainerHandler.InititalizeCamera(__instance);
    }
}

