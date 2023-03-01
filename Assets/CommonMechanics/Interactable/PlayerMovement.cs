using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaction;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  private LayerMask interactableLayer;

  private Transform hand;

  Vector2 rotation = Vector2.zero;
  public float cameraSpeed = 3;
  public float playerSpeed = 10;
  private InteractionComponent interaction;
  void Start()
  {
    hand = transform.GetChild(1);
    interaction = new InteractionComponent(hand,interactableLayer);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.W))
      transform.Translate(playerSpeed * Time.deltaTime * Vector3.forward);
    if (Input.GetKey(KeyCode.S))
      transform.Translate(playerSpeed * Time.deltaTime * -Vector3.forward);
    if (Input.GetKey(KeyCode.D))
      transform.Translate(playerSpeed * Time.deltaTime * Vector3.right);
    if (Input.GetKey(KeyCode.A))
      transform.Translate(playerSpeed * Time.deltaTime * -Vector3.right);


    rotation.y += Input.GetAxis("Mouse X");
    rotation.x += -Input.GetAxis("Mouse Y");
    if(playerSpeed != 0)
      transform.eulerAngles = rotation * cameraSpeed;

    if (Input.GetKeyUp(KeyCode.E))
      interaction.Interact(Camera.main.transform);
  }
}
