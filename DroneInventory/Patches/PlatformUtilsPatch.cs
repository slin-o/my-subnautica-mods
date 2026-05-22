using DroneInventory.Prefabs;
using HarmonyLib;
using System.Collections;

namespace DroneInventory.Patches;

[HarmonyPatch(typeof(PlatformUtils))]
internal class PlatformUtilsPatch
{
    [HarmonyPatch(nameof(PlatformUtils.PlatformInitAsync))]
    [HarmonyPostfix]
    public static IEnumerator Postfix(IEnumerator result)
    {
        yield return result;
        Plugin.Logger.LogInfo($" Starting Coroutine.");
        yield return MapRoomCameraStoragePrefab.ModifyMapRoomCameraPrefab();
    }
}
