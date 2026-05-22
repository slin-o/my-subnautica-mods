using UnityEngine;

namespace DroneInventory.Handlers;

internal class TargetHandler
{
    public static void Hover(MapRoomCamera camera)
    {
        GameObject target = GetTarget(camera.gameObject);

        var pickupable = target.GetComponent<Pickupable>();
        var breakable = target.GetComponent<BreakableResource>();

        GUIHand component = Player.main.guiHand;
        if (breakable != null)
        {
            GUIHand.Send(target, HandTargetEventType.Hover, Player.main.guiHand);
            return;
        }
        if (pickupable != null && pickupable.isPickupable)
        {
            GUIHand.Send(target, HandTargetEventType.Hover, Player.main.guiHand);
        }
    }

    public static void Click(MapRoomCamera camera)
    {
        GameObject target = GetTarget(camera.gameObject);

        var pickupable = target.GetComponent<Pickupable>();
        var breakable = target.GetComponent<BreakableResource>();
        if (breakable != null) {
            GUIHand.Send(target, HandTargetEventType.Click, Player.main.guiHand);
            return;
        }
        if (pickupable != null && pickupable.isPickupable)
        {
            ContainerHandler.AddItem(camera, pickupable);
        }
        
    }

    public static GameObject GetTarget(GameObject ignoreObj)
    {
        if (!Targeting.GetTarget(ignoreObj, 2f, out var activeTarget, out var _))
            return null;
        Transform transform = activeTarget.transform;
        IHandTarget handTarget;
        while (transform != null)
        {
            handTarget = transform.GetComponent<IHandTarget>();
            if (handTarget != null)
            {
                activeTarget = transform.gameObject;
                break;
            }

            transform = transform.parent;
        }

        return activeTarget;
    }
}
