using HarmonyLib;
using PDARenameBeacon.Handlers;
using System;
namespace PDARenameBeacon.Patches;

[HarmonyPatch(typeof(uGUI_PingTab), nameof(uGUI_PingTab.UpdateEntries))]
static class PingTabUpdatePatch
{
    [HarmonyPostfix]
    static void Postfix(uGUI_PingTab __instance)
    {
        BeaconMenuHandler.UpdateMapping(__instance);
    }
}
