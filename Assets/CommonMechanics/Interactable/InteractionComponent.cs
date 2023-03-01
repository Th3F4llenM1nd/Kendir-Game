using UnityEngine;

namespace Interaction
{
  public class InteractionComponent
  {
    public static Transform hand;
    private LayerMask interactableLayer;
    public InteractionComponent(Transform hand, LayerMask interactableLayer)
    {
      InteractionComponent.hand = hand;
      this.interactableLayer = interactableLayer;
    }

    public void Interact(Transform transform)
    {
      if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5, interactableLayer))
      {
        if (!hit.transform.TryGetComponent(out InteractionEvent interactionEvent))
        {
          if (hand.childCount == 0)
          {
            hit.transform.position = hand.position;
            hit.transform.SetParent(hand);
          }
          else
            Drop(hand);
        }
        else
        {
          if (hand.childCount > 0)
            interactionEvent.Event.Invoke();
        }
      }
      else
        if (hand.childCount > 0)
        Drop(hand);
    }

    private void Drop(Transform hand)
    {
      Transform temp = hand.GetChild(0);
      temp.eulerAngles = Vector3.zero;
      temp.localPosition += Vector3.forward * 3;
      temp.parent = null;
    }

    public static void PlaceObject(Transform placeHolder)
    {
      Transform obj = hand.GetChild(0);
      obj.SetPositionAndRotation(placeHolder.position, Quaternion.Euler(Vector3.zero));
      obj.SetParent(placeHolder);
    }
  }
}

