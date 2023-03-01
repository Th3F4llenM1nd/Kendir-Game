using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avalanche
{
  public class Building : MonoBehaviour
  {
    [SerializeField]
    private float groundOffset;

    public float altura, largura, tamanhoProximaCasa, preco, precoPorModificacao;
    public float precoTotal;
    public int numeroCasas, pessoasPorCasa,pessoasTotais;

    public float GroundOffSet { get => groundOffset; }
  }
}

