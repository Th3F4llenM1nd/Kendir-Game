using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Interaction;

namespace Catos
{
  public class CatusGameManager : MonoBehaviour
  {
    public Levels level;
    [SerializeField]
    Cato catos;
    [SerializeField]
    Transform[] spawners, pedestals;
    int count;
    public Cato[] catoSolutions;

    private readonly List<GameObject> spawnedObjs = new();

    private void OnEnable()
    {
      switch (level)
      {
        case Levels.Level1:
          SpawnCatus(spawners.Length, 3, 6);
          break;
        case Levels.Level2:
          SpawnCatus(spawners.Length, 3, 6);
          break;
        case Levels.Level3:
          SpawnCatus(3, 5, 6);
          break;
      }
    }

    private void OnDisable()
    {
      foreach (var item in spawnedObjs)
      {
        Destroy(item);
      }
      spawnedObjs.Clear();
    }

    void SpawnCatus(int numberOfSpawners, int minCatus, int maxCatus)
    {
      if (level != Levels.Level3)
        catoSolutions = new Cato[numberOfSpawners];
      else
        catoSolutions = new Cato[6];

      int count = 0;
      for (int i = 0; i < numberOfSpawners; i++)
      {
        List<Cato> spawnedCactus = new();
        for (int j = 0; j < Random.Range(minCatus, maxCatus); j++)
        {
          Cato temp = Instantiate(catos);
          spawnedCactus.Add(temp);
          spawnedObjs.Add(temp.gameObject);
          temp.cor = (Colours)i;
          //sortear os valores do cato
          Vector3 randomPos;
        sortPos:
          randomPos = spawners[i].position + Random.insideUnitSphere * 3;
          randomPos = new Vector3(randomPos.x, 0, randomPos.z);
          foreach (var cactu in spawnedCactus)
          {
            if (Vector3.Distance(cactu.transform.position, randomPos) < 1.5f)
              goto sortPos;
          }
          temp.transform.position = randomPos;
        }
        if (level != Levels.Level3)
          catoSolutions[i] = spawnedCactus[Random.Range(0, spawnedCactus.Count)];
        else
        {
          int temp = Random.Range(0, spawnedCactus.Count);
          catoSolutions[count] = spawnedCactus[temp];
          ++count;
          spawnedCactus.RemoveAt(temp);
          catoSolutions[count] = spawnedCactus[Random.Range(0, spawnedCactus.Count)];
          ++count;
        }

        spawnedCactus.Clear();
      }
    }

    public void PlaceObject() => InteractionComponent.PlaceObject(transform.GetChild(0));

    public void CheckSolution()
    {
      foreach (Cato solution in catoSolutions)
      {
        GameObject temp = transform.GetChild(0).GetChild(0).gameObject;
        if (temp == solution.gameObject)
        {
          Debug.Log("Acertou");
          temp.transform.parent = null;
          temp.transform.position = pedestals[count].transform.position;
          count++;
          return;
        }
        else
        {
          Debug.Log("Errou");
          if (solution == catoSolutions[^1])
            Destroy(temp);
        }
      }
    }
  }
}

