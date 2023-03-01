using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pointers;


namespace Avalanche
{
  public class BuildingSelector : MonoBehaviour
  {
    public Levels level;

    public GameObject[] buttons;
    public static List<Building> buildings = new();

    private Building lastBuilding;
    private RaycastHit hit;

    [SerializeField]
    private BuildingAdjuster panel;

    [SerializeField]
    private Transform buildingPlaceHolder;

    private void OnEnable()
    {
      switch (level)//change the levels as needed
      {
        case Levels.Level1:
          for (int i = 0; i < 2; i++)
            buttons[i].SetActive(true);
          break;
        case Levels.Level2:
          for (int i = 0; i < 3; i++)
            buttons[i].SetActive(true);
          break;
        case Levels.Level3:
          for (int i = 0; i < 4; i++)
            buttons[i].SetActive(true);
          break;
      }
    }

    private void Update()
    {
      hit = MouseOutlineChecker.currentObject;
      PlaceBuilding();
      MoveBuilding();
      RepositionBuilding();
    }

    public void SelectBuilding(Building building)
    {
      if (lastBuilding != building)
      {
        Building temp = Instantiate(building);
        temp.transform.position = buildingPlaceHolder.position + new Vector3(0, building.GroundOffSet, 0);

        if (lastBuilding != null)
        {
          buildings.Remove(lastBuilding);
          Destroy(lastBuilding.gameObject);
        }


        lastBuilding = temp;
        buildings.Add(lastBuilding);
      }
    }

    private void MoveBuilding()
    {
      if (lastBuilding != null)
      {
        if (hit.point != Vector3.zero && hit.transform.gameObject.layer == 6)
        {
          lastBuilding.transform.position = new Vector3(hit.point.x, lastBuilding.GroundOffSet * lastBuilding.transform.localScale.y, hit.point.z);
          panel.transform.GetChild(0).gameObject.SetActive(false);
        }
      }
    }

    private void PlaceBuilding()
    {
      if (hit.point != Vector3.zero && Input.GetMouseButtonUp(0) && hit.transform.gameObject.layer == 6)
      {
        if (lastBuilding != null)
        {
          lastBuilding.gameObject.layer = 7;
          panel.transform.GetChild(0).gameObject.SetActive(true);
          panel.RefreshValues(lastBuilding);
          lastBuilding = null;
        }
      }
    }

    private void RepositionBuilding()
    {
      if (lastBuilding == null)
        if (hit.point != Vector3.zero && Input.GetMouseButtonUp(0) && hit.transform.gameObject.layer == 7)
        {
          lastBuilding = hit.transform.GetComponent<Building>();
          lastBuilding.transform.gameObject.layer = 2;
        }

    }

    public void DeleteBuilding()
    {
      if (BuildingAdjuster.building != null)
      {
        buildings.Remove(BuildingAdjuster.building);
        Destroy(BuildingAdjuster.building.gameObject);
      }
    }
  }
}
