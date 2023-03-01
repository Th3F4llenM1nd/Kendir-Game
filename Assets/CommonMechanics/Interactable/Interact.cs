using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interaction
{
  public class Interact : MonoBehaviour
  {
    [SerializeField]
    private LayerMask interactableLayerMask;

    [SerializeField]
    private Transform hand;
    private InteractionComponent interaction;

    private void Start()
    {
      interaction = new(hand,interactableLayerMask);
    }

    private void Update()
    {
      if (Input.GetKeyUp(KeyCode.E))
      {
        interaction.Interact(Camera.main.transform);
      }
    }
  }

}

