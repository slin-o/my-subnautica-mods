using System.Collections;
using UnityEngine;

namespace DroneInventory.Prefabs;

internal class MapRoomCameraStoragePrefab
{
    public static IEnumerator ModifyMapRoomCameraPrefab()
    {
        var logger = Plugin.Logger;
        logger.LogInfo($" Attempting to Attaching Storage");
        CoroutineTask<GameObject> request = CraftData.GetPrefabForTechTypeAsync(TechType.MapRoomCamera, false);
        yield return request;

        var prefab = request.GetResult();
        logger.LogInfo($" Ensuring COI");
        var coi = prefab.transform.GetChild(0)?.gameObject.EnsureComponent<ChildObjectIdentifier>();

        if (coi)
        {
            logger.LogInfo($"Attaching Storage");
            var storageContainer = coi.gameObject.EnsureComponent<StorageContainer>();
            storageContainer.prefabRoot = prefab;
            storageContainer.storageRoot = coi;

            storageContainer.width = 2;
            storageContainer.height = 2;
            storageContainer.storageLabel = "CAMERA DRONE";
            logger.LogInfo($"Storage attached successfully!");
        }
        else
        {
            logger.LogError($"Failed to add COI. Unable to attach storage!");
        }

    }
}
