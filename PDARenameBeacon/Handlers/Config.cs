using Nautilus.Json;
using Nautilus.Options.Attributes;

namespace PDARenameBeacon.Handlers;

[Menu("Pda Rename Beacon")]
internal class Config : ConfigFile
{
    [Toggle("Enable Mod", Tooltip = "Enable or disable the mod")]
    public bool enabled = true;

    [Toggle("Rename Vehicles", Tooltip = "Allow renaming of Seamoth and PRAWN")]
    public bool renameVehicles = false;
}
