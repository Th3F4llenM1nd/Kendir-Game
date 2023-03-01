using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Interaction;

namespace Artefactos
{
  public class Ruin : MonoBehaviour
  {
    public Artifact artifactSolution;
    [SerializeField]
    private Transform _artifactPlaceHolder;
    public TextMeshPro question;

    public Transform ArtifactPlaceHolder { get => _artifactPlaceHolder; }

    public void GetSolution(Artifact solution) => artifactSolution = solution;

    public void PlaceObject() => InteractionComponent.PlaceObject(ArtifactPlaceHolder);

    public void ArtifactCheck()
    {
      if (ArtifactPlaceHolder.childCount == 1)
      {
        ArtifactPlaceHolder.GetChild(0).TryGetComponent(out Artifact artifact);
        if (artifact.Artifacts == artifactSolution.Artifacts)
        {
          Destroy(artifact.outline.outlineObject);
          Won(artifact.gameObject);
          question.enabled = false;
        }
        else
        {
          Lose(artifact);
        }
      }
      else
      {
        ArtifactPlaceHolder.GetChild(ArtifactPlaceHolder.childCount - 1).TryGetComponent(out Artifact artifact);
        artifact.transform.SetParent(InteractionComponent.hand);
        artifact.transform.localPosition = Vector3.zero;
        artifact.transform.eulerAngles = Vector3.zero;
      }
    }

    public void Level(Levels level)
    {
      switch (level)
      {
        case Levels.Level1:
          FirstLevel();
          break;
        case Levels.Level2:
          SecondLevel();
          break;
        case Levels.Level3:
          ThirdLevel();
          break;
      }
    }

    private void FirstLevel()
    {

    }
    private void SecondLevel()
    {

    }
    private void ThirdLevel()
    {

    }

    private void Won(GameObject artifact)
    {
      artifact.transform.localPosition = Vector3.up * 5;
      artifact.layer = 0;
    }

    private void Lose(Artifact artifact)
    {
      GameObject temp = artifact.gameObject;
      temp.transform.SetParent(artifact.spawner.transform);
      temp.transform.localPosition = Vector3.zero;
    }
  }

}
