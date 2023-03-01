using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planificacoes
{
  public class Solidgeometric : MonoBehaviour
  {
    public Flattening flattening;
    public Animation[] decoys;
    public Animation anim;

    private void Start()
    {
      anim = GetComponent<Animation>();
    }
  }
}
