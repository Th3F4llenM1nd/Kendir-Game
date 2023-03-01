using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Control
{
  public class IRaycastableController : MonoBehaviour
  {
    [System.Serializable]
    struct CursorMapping
    {
      public CursorType type;
      public Texture2D texture;
      public Vector2 hotspot;
    }
    [SerializeField] CursorMapping[] cursorMappings = null;
    [SerializeField] float raycastRadius = 1f;

    bool isDraggingUI = false;

    public bool InteractWithComponent(PlayerController playerController)
    {
      RaycastHit[] hits = RaycastAllSorted();
      foreach (RaycastHit hit in hits)
      {
        IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
        foreach (IRaycastable raycastable in raycastables)
        {
          if (raycastable.HandleRaycast(playerController))
          {
            SetCursor(/*raycastable.GetCursorType()*/);
            return true;
          }
        }
      }
      return false;
    }


    public bool InteractWithUI()
    {
      if (Input.GetMouseButtonUp(0))
      {
        isDraggingUI = false;
      }
      if (EventSystem.current.IsPointerOverGameObject())
      {
        if (Input.GetMouseButtonDown(0))
        {
          isDraggingUI = true;
        }
        SetCursor(/*CursorType.UI*/);
        return true;
      }
      if (isDraggingUI)
      {
        return true;
      }
      return false;
    }

    public void InteractMovable(bool movable)
    {
      if (movable)
      {
        SetCursor(/*CursorType.Movement*/);
      }
      else
      {
        SetCursor(/*CursorType.None*/);
      }
    }

    RaycastHit[] RaycastAllSorted()
    {
      RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
      float[] distances = new float[hits.Length];
      for (int i = 0; i < hits.Length; i++)
      {
        distances[i] = hits[i].distance;
      }
      System.Array.Sort(distances, hits);
      return hits;
    }


    //OUT OF ORDER FOR NOW
    private void SetCursor(/*CursorType type*/)
    {
      //FOR NOW IT WILL ALWAYS BE SET TO NORMAL SINCE WE AREN'T DETECTING THINGS PROPERLY OR CONSISTENTLY
      //type = CursorType.UI;

      CursorMapping mapping = GetCursorMapping(CursorType.UI);
      Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
    }

    private CursorMapping GetCursorMapping(CursorType type)
    {
      foreach (CursorMapping mapping in cursorMappings)
      {
        if (mapping.type == type)
        {
          return mapping;
        }
      }
      return cursorMappings[0];
    }

    private static Ray GetMouseRay()
    {
      return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
  }
}