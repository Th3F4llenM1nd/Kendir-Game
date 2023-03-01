using UnityEngine;

public class Outline : MonoBehaviour
{
  [SerializeField] private Material outlineMaterial;
  [SerializeField][Range(-1.05f, -1f)] private float outlineThickness = -1;
  [SerializeField] private Color outlineColor;
  public GameObject outlineObject;

  private void Start()
  {
    CreateOutline(outlineMaterial, outlineThickness, outlineColor);
    outlineObject.SetActive(false);
  }
  Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
  {
    outlineObject = Instantiate(gameObject, transform.position, transform.rotation, transform);
    for (int i = 0; i < outlineObject.transform.childCount; i++)
    {
      Destroy(outlineObject.transform.GetChild(i).gameObject);
    }
    Destroy(outlineObject.GetComponent<Outline>());
    Destroy(outlineObject.GetComponent<Collider>());
    Destroy(outlineObject.GetComponent<Planificacoes.Altar>());
    Destroy(outlineObject.GetComponent<Catos.Cato>());
    Destroy(outlineObject.GetComponent<Artefactos.Artifact>());
    Destroy(outlineObject.GetComponent<Avalanche.Building>());

    outlineObject.transform.localScale = new Vector3(1, 1, 1);
    Renderer rend = outlineObject.GetComponent<Renderer>();

    rend.material = outlineMat;
    rend.material.SetColor("_Color", color);
    rend.material.SetFloat("_Thicness", scaleFactor);
    rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

    return rend;
  }
}