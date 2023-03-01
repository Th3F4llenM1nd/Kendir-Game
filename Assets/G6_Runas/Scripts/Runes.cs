using UnityEngine;

namespace Runas
{
  public class Runes : MonoBehaviour
  {
    [SerializeField]
    private int _numeroCubos;
    [SerializeField]
    private float _volume;

    public int NumeroCubos { get => _numeroCubos; }

    public float Volume { get => _volume; }
  }
}

