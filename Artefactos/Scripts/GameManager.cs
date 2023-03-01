using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Niveis niveis;

    [SerializeField]
    private GameObject[] artefactosSpawners;
    [SerializeField]
    private List<GameObject> ruinasSpawners;

    public Ruina[] ruinas;
    public Artefacto[] artefactos;

    private void Start()
    {
    }

    private void SpawnRunas()
    {
        foreach (Ruina ruin in ruinas)
        {
            GameObject temp = Instantiate(ruin).gameObject;
            int randomNumber = Random.Range(0, artefactosSpawners.Length);
            temp.transform.position = ruinasSpawners[randomNumber].transform.position;
            ruinasSpawners.RemoveAt(randomNumber);
        }
    }

    private void SpawnArtefactos()
    {
        foreach (GameObject artefactoSpawner in artefactosSpawners)
        {

        }
    }
}
