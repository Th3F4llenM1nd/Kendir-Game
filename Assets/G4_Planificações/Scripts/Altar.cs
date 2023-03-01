using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pointers;

namespace Planificacoes
{
  public class Altar : MonoBehaviour
  {
    public Transform solidPlaceHolder;
    public Solidgeometric solution;
    [HideInInspector]
    public PlanificacoesGameManager manager;
    Transform solid;

    private void Update()
    {
      CheckSolution();
    }

    void CheckSolution()
    {
      if (Input.GetMouseButtonUp(0))
        if (MouseOutlineChecker.currentObject.transform != null)
          if (MouseOutlineChecker.currentObject.transform.TryGetComponent(out Solidgeometric currentObject))
          {
            currentObject.anim.Play();
            if (solution.flattening == currentObject.flattening)
              Invoke(nameof(Won),3);
            else
            {
              solid = currentObject.transform;
              Invoke(nameof(DestroySolid), 3);
            }
          }
    }

    private void DestroySolid()
    {
      manager.spawnedObjs.Remove(solid);
      Destroy(solid.gameObject);
    }

    public void Won()
    {
      if (!manager.NextRoundAndCheck())
      {
        manager.SetupRound();
      }
    }

    public void Lose()
    {
    }
  }
}


