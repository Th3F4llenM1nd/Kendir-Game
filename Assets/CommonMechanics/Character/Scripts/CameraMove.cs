//using AkshayDhotre.GraphicSettingsMenu;
using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
  [SerializeField] Transform target;

  [Serializable]
  public class PositionSettings
  {
    public Vector3 targetPosOffset = new(0.0f, 1.0f, 0.0f);
    public float lookSmooth = 100.0f;
    public float distanceFromTarget = -8.0f;
    public float zoomSmooth = 10.0f;
    public float maxZoom = -2.0f;
    public float minZoom = -15.0f;
    public bool smoothFollow = true;
    public float smooth = 0.05f;

    [HideInInspector] public float newDistance = -8.0f;
    [HideInInspector] public float adjustmentDistance = -8.0f;
  }

  [Serializable]
  private class OrbitSettings
  {
    public float xRotation = -20.0f;
    public float yRotation = 180.0f;
    public float initialYRotation = 180f;
    public float maxXRotation = 25.0f;
    public float minXRotation = -85.0f;
    public float vOrbitSmooth = 150.0f;
    public float hOrbitSmooth = 150.0f;

    public bool invertXAxis = false;
    public bool invertYAxis = false;
  }

  [Serializable]
  private class DebugSettings
  {
    public bool drawDesiredCollisionLines = true;
    public bool drawAdjustedCollisionLines = true;
  }

  public PositionSettings position = new();
  [SerializeField] OrbitSettings orbit = new();
  [SerializeField] CollisionHandler collision = new();
  [SerializeField] DebugSettings debug = new();
  [SerializeField] bool preciseFollow = true;
  //[SerializeField] GraphicMenuManager graphicsMenuManager;

  Vector3 targetPos = Vector3.zero;
  Vector3 destination = Vector3.zero;
  Vector3 adjustedDestination = Vector3.zero;
  Vector3 cameraVelocity = Vector3.zero;
  float vOrbitInput, hOrbitInput;//, zoomInput, hOrbitSnapInput, yOrbitSnapInput;



  //PlayerController player;

  // used for snapping
  //float animationDuration = 0f, animationPercentage = 0f, animationFinalDuration = 0.1f;
  //float currentyOrbit = 0f;

  // Start is called before the first frame update
  void Start()
  {
    //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    var cam = GetComponent<Camera>();

    //vOrbitInput = hOrbitInput = zoomInput = hOrbitSnapInput = yOrbitSnapInput = 0.0f;

    SetCameraTarget(target);
    MoveToTarget();
    ResetLookOnTarget();

    collision.Initialize(cam);
    collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
    collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

    /*graphicsMenuManager.gameObject.SetActive(true);
		graphicsMenuManager.Reload();
		graphicsMenuManager.gameObject.SetActive(false);*/
  }

  // Update is called once per frame
  void Update()
  {
    //ZoomOnTarget();
    CheckCollisions();

    GetInput();

    // move
    MoveToTarget();
  }

  private void CheckCollisions()
  {
    collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
    collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

    // draw debug lines
    for (int i = 0; i < 5; i++)
    {
      if (debug.drawAdjustedCollisionLines)
      {
        Debug.DrawLine(targetPos, collision.desiredCameraClipPoints[i], Color.white);
      }
      if (debug.drawDesiredCollisionLines)
      {
        Debug.DrawLine(targetPos, collision.adjustedCameraClipPoints[i], Color.green);
      }
    }

    collision.CheckCollision(targetPos);
    position.adjustmentDistance = collision.GetAdjustedDistanceWithRayFrom(targetPos);
  }

  private void LateUpdate()
  {
    // rotate
    LookAtTarget();

    // player input orbit
    OrbitTarget();
  }
  public void SetCameraTarget(Transform newTarget)
  {
    target = newTarget;

    if (target == null)
    {
      Debug.LogError("There's no target.");
    }
  }

  void GetInput()
  {
    //zoomInput = Input.GetAxisRaw("Mouse ScrollWheel");

    if (Input.GetMouseButton(1))
    {
      float xAxis = 1;
      float yAxis = 1;

      if (!orbit.invertXAxis) xAxis = -1;
      if (orbit.invertYAxis) yAxis = -1;

      vOrbitInput = Input.GetAxisRaw("Mouse Y") * yAxis * -50;
      hOrbitInput = Input.GetAxisRaw("Mouse X") * xAxis * 50;
    }

    if (Input.GetMouseButtonUp(1))
    {
      hOrbitInput = 0f;
      vOrbitInput = 0f;
    }
  }

  public void GoToPlayerBack()
  {
    if (preciseFollow)
    {
      if (orbit.yRotation != orbit.initialYRotation)
      {
        //animationDuration = 0f;
        //animationPercentage = 0f;
        //animationFinalDuration = 1f;

        //hOrbitSnapInput = 1f;
        //currentyOrbit = orbit.yRotation;
      }
    }
  }

  void MoveToTarget()
  {
    if (targetPos != null && target != null)
    {
      targetPos = target.position + position.targetPosOffset;
      destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.localEulerAngles.y, 0.0f) * -Vector3.forward * position.distanceFromTarget;
      destination += targetPos;

      if (collision.isColliding)
      {
        adjustedDestination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.localEulerAngles.y, 0.0f) * Vector3.forward * position.adjustmentDistance;
        adjustedDestination += targetPos;

        if (position.smoothFollow)
        {
          // Use smooth damp function
          transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination, ref cameraVelocity, position.smooth);
        }
        else
        {
          transform.position = adjustedDestination;
        }
      }
      else
      {
        if (position.smoothFollow)
        {
          // Use smooth damp function
          transform.position = Vector3.SmoothDamp(transform.position, destination, ref cameraVelocity, position.smooth);
        }
        else
        {
          transform.position = destination;
        }
      }
    }
  }

  void LookAtTarget()
  {
    Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
  }

  void OrbitTarget()
  {
    orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
    orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

    if (orbit.yRotation > 360f) orbit.yRotation -= 360f;
    if (orbit.yRotation < 0) orbit.yRotation = 360f - orbit.yRotation;
    if (orbit.xRotation > 360f) orbit.xRotation -= 360f;
    if (orbit.xRotation < 0) orbit.xRotation = 360f - orbit.xRotation;
  }

  public void SetOrbitAxis(float verticalOrbitAxis = 0.0f, float horizontalOrbitAxis = 0.0f)
  {
    //if (!canControlCamera) return;

    vOrbitInput = verticalOrbitAxis;
    hOrbitInput = horizontalOrbitAxis;
  }

  public void ResetLookOnTarget()
  {
    orbit.yRotation = orbit.initialYRotation;
    orbit.xRotation = -10.0f;
  }

  public void SetInvertXAxis(bool toInvert)
  {
    orbit.invertXAxis = toInvert;
  }

  public void SetInvertYAxis(bool toInvert)
  {
    orbit.invertYAxis = toInvert;
  }

  [Serializable]
  public class CollisionHandler
  {
    public LayerMask collisionLayerMask;

    [HideInInspector] public bool isColliding = false;
    [HideInInspector] public Vector3[] adjustedCameraClipPoints;
    [HideInInspector] public Vector3[] desiredCameraClipPoints;

    Camera camera;

    public void Initialize(Camera _camera)
    {
      camera = _camera;
      adjustedCameraClipPoints = new Vector3[5];
      desiredCameraClipPoints = new Vector3[5];
    }

    public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
    {
      if (!camera) return;

      // Clear the contents of intoArray
      intoArray = new Vector3[5];

      float z = camera.nearClipPlane;
      float x = Mathf.Tan(camera.fieldOfView / 3.41f) * z;
      float y = x / camera.aspect;

      // Top Left
      intoArray[0] = (atRotation * new Vector3(-x, y, z) + cameraPosition); // Added and rotated the point relative to camera

      // Top Right
      intoArray[1] = (atRotation * new Vector3(x, y, z) + cameraPosition); // Added and rotated the point relative to camera

      // Bottom Left
      intoArray[2] = (atRotation * new Vector3(-x, -y, z) + cameraPosition); // Added and rotated the point relative to camera

      // Bottom Right
      intoArray[3] = (atRotation * new Vector3(x, -y, z) + cameraPosition); // Added and rotated the point relative to camera

      // Camera's position
      intoArray[4] = cameraPosition - camera.transform.forward;
    }

    public void CheckCollision(Vector3 targetPosition)
    {
      isColliding = CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition);
    }

    bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
    {
      for (int i = 0; i < clipPoints.Length; i++)
      {
        Ray ray = new(fromPosition, clipPoints[i] - fromPosition);
        float distance = Vector3.Distance(clipPoints[i], fromPosition);
        if (Physics.Raycast(ray, distance, collisionLayerMask))
        {
          return true;
        }
      }

      return false;
    }

    public float GetAdjustedDistanceWithRayFrom(Vector3 fromPosition)
    {
      float distance = -1.0f;

      for (int i = 0; i < desiredCameraClipPoints.Length; i++)
      {
        Ray ray = new(fromPosition, desiredCameraClipPoints[i] - fromPosition);

        if (Physics.SphereCast(ray, 0.01f, out RaycastHit hit))
        {
          if (distance == -1.0f || hit.distance < distance)
          {
            distance = hit.distance;
          }
        }
      }

      if (distance == -1.0f)
      {
        return 0.0f;
      }
      else
      {
        return distance;
      }
    }
  }
}
