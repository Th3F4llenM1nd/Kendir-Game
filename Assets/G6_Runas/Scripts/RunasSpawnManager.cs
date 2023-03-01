using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Runas
{
  public class RunasSpawnManager : MonoBehaviour
  {
    public Levels level;
    private bool inputFieldSelected = false;
    [SerializeField]
    private TMP_InputField resposta;
    [SerializeField]
    private Transform runePlaceHolder;
    public List<Runes> runes = new();
    private Runes currentRune;
    private int maxRounds, round;
    void OnEnable()
    {
      SpawnRune();
    }

    private void OnDisable()
    {
      Destroy(currentRune.gameObject);
      resposta.text = "";
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
      {
        inputFieldSelected = !inputFieldSelected;
        if (inputFieldSelected)
          resposta.ActivateInputField();
        else
          CheckResponse();
      }
    }

    void SpawnRune()
    {
      switch (level)//falta dar gerar os valores
      {
        case Levels.Level1:
          currentRune = Instantiate(runes[Random.Range(0, runes.Count)]);
          maxRounds = 5;
          break;
        case Levels.Level2:
          currentRune = Instantiate(runes[Random.Range(0, runes.Count)]);
          maxRounds = 6;
          break;
        case Levels.Level3:
          currentRune = Instantiate(runes[Random.Range(0, runes.Count)]);
          maxRounds = 6;
          break;
      }
      currentRune.transform.SetParent(runePlaceHolder);
      currentRune.transform.position = Vector3.zero;
    }

    void CheckResponse()
    {
      if (currentRune.Volume == int.Parse(resposta.text))
      {
        Debug.Log("acertou");
        if (round < maxRounds)
          round++;
        else
          Debug.Log("Game End");
      }
      else if(int.Parse(resposta.text) > currentRune.Volume)
      {
        Debug.Log("numero introduzido é maior");
      }
      else if (int.Parse(resposta.text) < currentRune.Volume)
      {
        Debug.Log("numero introduzido é menor");
      }
    }
  }
}
