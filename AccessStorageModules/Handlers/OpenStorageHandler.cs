using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VFXParticlesPool;

namespace AccessStorageModules.Handlers;

internal class OpenStorageHandler
{
    public static void OpenStorage(Vehicle vehicle, int slotID)
    {
        if (vehicle is Exosuit exo)
        {
            exo.storageContainer.Open();
        }
        else if (vehicle is SeaMoth seamoth)
        {
            seamoth.storageInputs[slotID].OpenPDA();
        }
    }
}
