using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ruina : MonoBehaviour
{
    public Artefacto artefactoSolucao;
    [SerializeField]
    private GameObject artefactoPlaceHolder;
    [SerializeField]
    private TextMeshProUGUI pergunta;

    public Artefacto GetSolucao()
    {
        return artefactoSolucao;
    }

    private void ArtefactoCheck()
    {
        if(artefactoPlaceHolder.transform.GetChild(0) != null)
        {

        }
    }

    public void PrimeiroNivel(Niveis niveis)
    {

    }

    public void SegundoNivel(Niveis niveis)
    {

    }

    public void TerceiroNivel(Niveis niveis)
    {

    }
}
