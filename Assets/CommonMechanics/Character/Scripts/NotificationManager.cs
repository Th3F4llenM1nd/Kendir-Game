using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject txtParent;
    [SerializeField]
    private GameObject textObject;
    [SerializeField]
    private GameObject mainCamera;

    void Start()
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
        txtParent.SetActive(false);
    }

    public void Show(string input)
    {
        txtParent.SetActive(true);
        text.SetText(input);
        StartCoroutine(AlphaFade(3f));
    }

    public void ShowGame(string input)
    {
        txtParent.SetActive(true);
        text.SetText(input);
        StartCoroutine(AlphaFade(3f));
    }

    public void AlphaShow(string input, float delay)
    {
        txtParent.SetActive(true);
        text.SetText(input);
        StartCoroutine(AlphaFade(delay));
    }
    public IEnumerator AlphaFade(float delay)
    {
        yield return new WaitForSeconds(delay);
        txtParent.SetActive(false);
    }

    public void EnableMainCamera()
    {
        mainCamera.SetActive(true);
    }

    public IEnumerator WaitForClosure(GameObject popup)
    {
        while (popup != null)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
}