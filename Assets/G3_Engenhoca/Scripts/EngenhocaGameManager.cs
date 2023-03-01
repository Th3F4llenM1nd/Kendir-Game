using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engenhocas
{
  public class EngenhocaGameManager : MonoBehaviour
  {
    [SerializeField]
    private Levels level;

    [SerializeField]
    private Engenhoca[] engenhocas;
    [SerializeField]
    private Transform engenhocaPlaceHolder;

    private readonly List<GameObject> spawnedParts = new();
    private Engenhoca spawnedEngenhoca;

    void OnEnable()
    {
      SpawnEngenhoca();
      Spawner(spawnedEngenhoca.engenhocaParts);
      Spawner(spawnedEngenhoca.partsDecoys);
    }

    private void OnDisable()
    {
      foreach (GameObject item in spawnedParts)
      {
        Destroy(item);
      }
      spawnedParts.Clear();
    }

    private void Update()
    {
      ValidatePart();
    }

    private void ValidatePart()
    {

      if (Input.GetMouseButtonUp(0) && Pointers.MouseOutlineChecker.currentObject.transform != null)
      {
        GameObject temp = Pointers.MouseOutlineChecker.currentObject.transform.gameObject;
        foreach (GameObject parts in spawnedEngenhoca.engenhocaParts)
        {
          if (parts.name == temp.name.Remove(temp.name.Length - 7))
          {
            Debug.Log("Part");
            return;
          }
        }
        Destroy(temp);
      }
    }

    private void SpawnEngenhoca()
    {
      switch (level)//change according
      {
        case Levels.Level1:
          spawnedEngenhoca = Instantiate(engenhocas[Random.Range(0, 1)]);
          spawnedEngenhoca.transform.position = engenhocaPlaceHolder.position;
          spawnedParts.Add(spawnedEngenhoca.gameObject);
          break;
        case Levels.Level2:
          spawnedEngenhoca = Instantiate(engenhocas[Random.Range(1, 3)]);
          spawnedEngenhoca.transform.position = engenhocaPlaceHolder.position;
          spawnedParts.Add(spawnedEngenhoca.gameObject);
          break;
        case Levels.Level3:
          spawnedEngenhoca = Instantiate(engenhocas[Random.Range(3, 4)]);
          spawnedEngenhoca.transform.position = engenhocaPlaceHolder.position;
          spawnedParts.Add(spawnedEngenhoca.gameObject);
          break;
      }

    }

    private void Spawner(GameObject[] spawn)
    {
      foreach (GameObject part in spawn)
      {
        GameObject temp = Instantiate(part);
        spawnedParts.Add(temp);
      sortPos:
        temp.transform.position = spawnedEngenhoca.transform.position + Random.insideUnitSphere * 6;
        temp.transform.position = new Vector3(temp.transform.position.x, 0, temp.transform.position.z);
        foreach (var engenhocaPart in spawnedParts)
        {
          if (temp != engenhocaPart && Vector3.Distance(engenhocaPart.transform.position, temp.transform.position) < 1.5f)
            goto sortPos;
        }
      }
    }


    private void OnDrawGizmos()
    {
      Gizmos.DrawWireSphere(engenhocaPlaceHolder.transform.position, 6);
    }
  }
}
