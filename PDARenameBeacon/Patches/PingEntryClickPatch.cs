using HarmonyLib;

namespace PDARenameBeacon.Patches;

[HarmonyPatch(typeof(uGUI_PingEntry), nameof(uGUI_PingEntry.Awake))]
static class PingEntryClickPatch
{
    [HarmonyPostfix]
    static void Postfix(uGUI_PingEntry __instance)
    {
        __instance.gameObject.AddComponent<Handlers.PingEntryClickHandler>();
    }
}
