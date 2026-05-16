using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessStorageModules.Handlers;
using HarmonyLib;

namespace AccessStorageModules.Patches;

[HarmonyPatch(typeof(Vehicle))]
internal class VehiclePatch
{
    [HarmonyPatch(nameof(Vehicle.SlotKeyDown))]
    [HarmonyPostfix]
    public static void SlotKeyDownPostfix(Vehicle __instance, int slotID){
        if (__instance.modules.GetTechTypeInSlot(__instance.slotIDs[slotID]) == TechType.VehicleStorageModule)
            OpenStorageHandler.OpenStorage(__instance, slotID);

    }
}
