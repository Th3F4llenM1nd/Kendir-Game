using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Avalanche
{
  public class BuildingAdjuster : MonoBehaviour
  {
    private GameObject panel;

    [SerializeField]
    private Slider altura, largura;

    [SerializeField]
    private BuildingSelector selector;

    [SerializeField]
    private TextMeshProUGUI casas, pessoas, preco, numAltura, numLargura, custoTotal, pessoasTotal, casasTotal;

    public static Building building;

    private void Start()
    {
      panel = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
      selector.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
      selector.gameObject.SetActive(false);
      ResetGame();
    }

    private void ResetGame()
    {
      foreach (var item in BuildingSelector.buildings)
      {
        Destroy(item.gameObject);
      }
      custoTotal.text = "0";
      pessoasTotal.text = "0";
      casasTotal.text = "0";
      panel.SetActive(false);
      BuildingSelector.buildings.Clear();
    }

    public void RefreshValues(Building currentBuilding)
    {
      building = currentBuilding;
      altura.value = altura.minValue;
      largura.value = largura.minValue;
      SlidersTexts();
      BuildingValues();
      BuildingsTotal();
    }

    public void SlidersValues()
    {
      building.altura = altura.value;
      building.largura = largura.value;
      building.transform.localScale = new Vector3(largura.value, altura.value, largura.value);
      building.transform.position = new Vector3(building.transform.position.x, building.GroundOffSet * building.transform.localScale.y, building.transform.position.z);
      SlidersTexts();



      BuildingValues();
      BuildingsTotal();
    }

    private void SlidersTexts()
    {
      numAltura.text = Mathf.Round(altura.value).ToString();
      numLargura.text = Mathf.Round(largura.value).ToString();
    }

    private void BuildingValues()
    {
      float casasTemp = Mathf.Round(building.tamanhoProximaCasa * building.altura + building.tamanhoProximaCasa * building.largura);
      building.numeroCasas = Mathf.RoundToInt(casasTemp);
      float precoTemp = Mathf.Round(building.precoPorModificacao * building.altura + building.precoPorModificacao * building.largura);
      building.precoTotal = precoTemp;
      int pessoasTemp = building.pessoasPorCasa * building.numeroCasas;
      building.pessoasTotais = pessoasTemp;




      casas.text = casasTemp.ToString();
      pessoas.text = pessoasTemp.ToString();
      preco.text = precoTemp.ToString();
    }

    public void BuildingsTotal()
    {
      float precoTotal = 0;
      float casasTotais = 0;
      int pessoasTotais = 0;
      foreach (Building building in BuildingSelector.buildings)
      {
        precoTotal += building.precoTotal;
        casasTotais += building.numeroCasas;
        pessoasTotais += building.pessoasTotais;
      }
      custoTotal.text = precoTotal.ToString();
      pessoasTotal.text = pessoasTotais.ToString();
      casasTotal.text = casasTotais.ToString();
    }
  }
}
