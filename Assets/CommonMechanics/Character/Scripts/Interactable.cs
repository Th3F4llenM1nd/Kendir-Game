using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
  [SerializeField]
  protected float radius;
  protected bool hasInteracted = false;
  protected bool isFocus = false;
  protected Transform player;
  protected NotificationManager notificationManager;
  protected PlayerController playerController;


  void Start()
  {
    GameObject canvas = GameObject.Find("MainCanvas");
    notificationManager = canvas.GetComponent<NotificationManager>();
    playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
  }

  public NotificationManager GetNotificationManager()
  {
    foreach (NotificationManager nm in FindObjectsOfType<NotificationManager>())
    {
      if (nm.gameObject.CompareTag("MainCanvas"))
      {
        notificationManager = nm;
      }
    }
    return notificationManager;
  }

  public virtual void Interact()
  {
    Debug.Log("PIÇAS PARA A TUA MÃE");
  }

  void Update()
  {
    if (isFocus && !hasInteracted)
    {
      float distance = Vector3.Distance(player.position, transform.position);

      //notificationManager.ShowItemText();

      if (distance <= radius)
      {
        Interact();
        hasInteracted = true;
      }
    }
  }

  public virtual void LeaveArea()
  {
    //notificationManager.HideItemText();
  }

  public void OnFocused(Transform playerTransform)
  {
    isFocus = true;
    player = playerTransform;
    hasInteracted = false;
  }

  public void OnDefocused()
  {
    isFocus = false;
    player = null;
    if (hasInteracted)
    {
      LeaveArea();
    }
    hasInteracted = false;
  }

  public float GetRadius()
  {
    return radius;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}