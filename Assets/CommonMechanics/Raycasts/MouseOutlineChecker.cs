using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Pointers
{
  public class MouseOutlineChecker : Pointers
  {

    public static RaycastHit currentObject;

    private void Update()
    {
      CheckOutlines(currentObject = MousePointer());
    }
  }
}

