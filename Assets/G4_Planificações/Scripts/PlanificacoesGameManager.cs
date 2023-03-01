using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planificacoes
{
  public class PlanificacoesGameManager : MonoBehaviour
  {
    [SerializeField]
    private Levels level;
    [SerializeField]
    private int roundsMax = 5;
    [SerializeField]
    private Altar altar;

    private float changeOfRandomFigure;

    public List<Solidgeometric> solidgeometricsLevel1, solidgeometricsLevel2, solidgeometricsLevel3;
    public List<Transform> spawners;

    private List<Solidgeometric> _solidgeometrics;
    private List<Transform> _spawners;

    [HideInInspector]
    public List<Transform> spawnedObjs;

    private int round = 0;

    private void OnEnable()
    {
      spawnedObjs = new List<Transform>();
      altar.manager = this;
      SetupRound();
    }

    private void OnDisable()
    {
      ResetGame();
    }

    private void ResetGame()
    {
      foreach (Transform item in spawnedObjs)
      {
        Destroy(item.gameObject);
      }
      spawnedObjs.Clear();
      _solidgeometrics.Clear();
      _spawners.Clear();
      round = 0;
    }

    private void NextRound() => ++round;

    private bool CheckEnd()
    {
      if (round >= roundsMax)
      {
        Debug.Log("End Game");
        return true;
      }
      return false;
    }

    public bool NextRoundAndCheck()
    {
      NextRound();
      return CheckEnd();
    }

    public void SetupRound()
    {
      foreach (var item in spawnedObjs)
      {
        Destroy(item.gameObject);
      }
      spawnedObjs.Clear();

      switch (level)
      {
        case Levels.Level1:
          _solidgeometrics = new List<Solidgeometric>(solidgeometricsLevel1);
          changeOfRandomFigure = 0f;
          break;
        case Levels.Level2:
          _solidgeometrics = new List<Solidgeometric>(solidgeometricsLevel2);
          changeOfRandomFigure = 0.5f;
          break;
        case Levels.Level3:
          _solidgeometrics = new List<Solidgeometric>(solidgeometricsLevel3);
          changeOfRandomFigure = 1f;
          break;
      }


      _spawners = new List<Transform>(spawners);
      SpawnSolution();
      SpawnDecoys();
    }

    private void SpawnSolution()
    {
      int randomNumber = Random.Range(0, _solidgeometrics.Count);
      altar.solution = Instantiate(_solidgeometrics[randomNumber]);
      spawnedObjs.Add(altar.solution.transform);
      Destroy(altar.solution.GetComponent<Outline>());
      altar.solution.transform.position = altar.solidPlaceHolder.position + Vector3.up;
      altar.solution.transform.localScale = altar.solution.transform.localScale / 2f;
      altar.solution.transform.SetParent(altar.solidPlaceHolder);
      altar.solution = Instantiate(_solidgeometrics[randomNumber]);
      spawnedObjs.Add(altar.solution.transform);
      _solidgeometrics.RemoveAt(randomNumber);
      randomNumber = Random.Range(0, _spawners.Count);
      altar.solution.transform.position = _spawners[randomNumber].position;
      altar.solution.transform.localScale = altar.solution.transform.localScale / 2f;
      altar.solution.transform.SetParent(_spawners[randomNumber]);
      _spawners.RemoveAt(randomNumber);
    }

    private void SpawnDecoys()
    {
      for (int i = 0; i < _spawners.Count; i++)
      {
        int randomNumber;
        GameObject temp;
        if (Random.value > changeOfRandomFigure)
        {
          randomNumber = Random.Range(0, _solidgeometrics.Count);
          temp = Instantiate(_solidgeometrics[randomNumber].gameObject);
          _solidgeometrics.RemoveAt(randomNumber);
        }
        else
        {
          randomNumber = Random.Range(0,altar.solution.decoys.Length);
          temp = Instantiate(altar.solution.decoys[randomNumber].gameObject);
        }
        spawnedObjs.Add(temp.transform);
        temp.transform.position = _spawners[i].position;
        temp.transform.localScale = temp.transform.localScale / 2f;
        temp.transform.SetParent(_spawners[i]);
      }
    }
  }
}

