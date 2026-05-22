using Nautilus.Utility;
using System.Collections.Generic;

namespace DroneInventory.Handlers;

internal class ContainerHandler
{
    public static readonly Dictionary<MapRoomCamera, StorageContainer> StorageContainers = new();

    public static void RemoveContainer(MapRoomCamera camera)
    {
        if (StorageContainers.ContainsKey(camera))
        {
            StorageContainers.Remove(camera);
        }
    }

    public static bool OpenCamera(MapRoomCamera camera)
    {
        if (StorageContainers.TryGetValue(camera, out var container))
        {
            container.Open();
            return true;
        }
        else
        {
            Plugin.Logger.LogError($"Failed to open storage container for camera {camera.cameraNumber}.");
            return false;
        }
    }

    public static StorageContainer GetContainer(MapRoomCamera camera)
    {
        if (StorageContainers.TryGetValue(camera, out var container))
        {
            return container;
        }
        else
        {
            Plugin.Logger.LogError($"Failed to get storage container for camera {camera.cameraNumber}.");
            return null;
        }
    }

    public static void InititalizeCamera(MapRoomCamera camera)
    {
        StorageContainer storageContainer = camera.transform.GetChild(0)?.GetComponent<StorageContainer>();
        if (storageContainer != null)
        {
            StorageContainers[camera] = storageContainer;
        }
    }

    public static InventoryItem AddItem(MapRoomCamera camera, Pickupable pickupable)
    {
        var pickupableCamera = pickupable.GetComponent<MapRoomCamera>();
        if (pickupableCamera != null)
        {
            if (!GetContainer(pickupableCamera).container.IsEmpty())
                return null;
        }
        if (StorageContainers.TryGetValue(camera, out var container))
        {
            var item = container.container.AddItem(pickupable);
            if (item != null)
                pickupable.Pickup(true);
            return item;
        }
        else
        {
            Plugin.Logger.LogError($"Failed to add item to storage container for camera {camera.cameraNumber}.");
            return null;
        }
    }
}
