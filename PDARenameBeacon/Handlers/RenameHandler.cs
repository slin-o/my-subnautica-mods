using System;
using System.Collections.Generic;
namespace PDARenameBeacon.Handlers;

internal class RenameHandler
{

    private static Language main = Language.main;
    public static void Rename(Beacon beacon)
    {
        var label = beacon.beaconLabel;
        RequestString(label.labelName, label.SetLabel);
    }
    public static void Rename(SubName subName)
    {
        RequestString(subName.GetName(), subName.SetName);
    }

    private static void RequestString(string currentValue, Action<string> callback)
    {
        var renameLabel = main.Get("BeaconLabel");
        var renameSubmit = main.Get("BeaconSubmit");
        uGUI.main.userInput.RequestString(renameLabel, renameSubmit, currentValue, 25, new uGUI_UserInput.UserInputCallback(callback));
    }
}
