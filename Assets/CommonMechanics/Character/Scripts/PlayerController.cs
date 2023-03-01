using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace RPG.Control
{
  public class PlayerController : MonoBehaviour
  {
    [SerializeField]
    Camera cam;
    [SerializeField]
    NavMeshAgent navMeshAgent;

    [SerializeField]
    LayerMask IgnoreLayer;

    [SerializeField]
    GameObject cursorAnimObject;

    GameObject newCursor;

    bool isSprinting = false;

    public Interactable focus;

    private float baseSpeed = 1f;

    [SerializeField]
    bool isPaused = false;

    [SerializeField] private float rotateSpeed = 100.0f;
    [SerializeField] float sprintSpeed = 3f;

    //Vector3 moveDestination;

    // ### WASD Movement
    private float movementSpeed;
    private float directionForward = 0f;
    private Vector3 previousPosition;
    private Vector3 v_Movement;

    bool canMouse = true;

    IRaycastableController raycastControl;

    CameraMove camMove;

    public float MovementSpeed
    {
      get
      {
        return movementSpeed;
      }
    }
    private void Awake()
    {

      isPaused = false;
      canMouse = true;

      navMeshAgent = GetComponent<NavMeshAgent>();
      raycastControl = GetComponent<IRaycastableController>();
      camMove = cam.GetComponent<CameraMove>();
    }

    void Update()
    {
      //InputMouse_OrbitCamera();

      if (isPaused) return;

      if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) ||
          Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
      {
        navMeshAgent.ResetPath();
      }

      // ### WASD Controls
      if (Input.GetKey(KeyCode.LeftShift))
      {
        isSprinting = true;
      }
      else if (Input.GetKeyUp(KeyCode.LeftShift))
      {
        isSprinting = false;
      }

      if (Input.GetKey(KeyCode.W))
      {
        MoveForward();
      }
      else if (Input.GetKey(KeyCode.S))
      {
        MoveBackwards();
      }

      if (Input.GetKey(KeyCode.A))
      {
        TurnLeft();
      }
      else if (Input.GetKey(KeyCode.D))
      {
        TurnRight();
      }

      //if (raycastControl.InteractWithUI()) return;
      //if (raycastControl.InteractWithComponent(this)) return;
      if (canMouse && Input.GetMouseButtonUp(0))
      {
        MovePlayerToPoint();
        return;
      }
    }

    private void FixedUpdate()
    {
      ProcessMovement();
    }

    // ### WASD Controls
    void ProcessMovement()
    {
      v_Movement = (cam.transform.forward * directionForward).normalized;
      movementSpeed = ((transform.position - previousPosition).magnitude) / Time.fixedDeltaTime;
      previousPosition = transform.position;
    }

    public void MoveForward()
    {
      if (isPaused) return;
      directionForward = 1f;
      Vector3 destinationPos = transform.position + (navMeshAgent.speed) * baseSpeed * Time.deltaTime * v_Movement;

      if (isSprinting) destinationPos = transform.position + (navMeshAgent.speed) * baseSpeed * sprintSpeed * Time.deltaTime * v_Movement;

      transform.position = Vector3.MoveTowards(transform.position, destinationPos, 1f);
    }

    public void MoveBackwards()
    {
      if (isPaused) return;
      if (cam == null) return;
      directionForward = -1f;
      Vector3 destinationPos = transform.position + (navMeshAgent.speed / 2) * Time.deltaTime * v_Movement;
      transform.position = Vector3.MoveTowards(transform.position, destinationPos, 1f);
    }

    public void TurnLeft()
    {
      if (isPaused) return;
      transform.Rotate(rotateSpeed * Time.deltaTime * (Vector3.up * -1.0f));
      //cam.transform.rotation = transform.rotation;
      RotateInstant();
    }

    public void TurnRight()
    {
      if (isPaused) return;
      transform.Rotate(rotateSpeed * Time.deltaTime * (Vector3.up * 1.0f));
      //cam.transform.rotation = transform.rotation;
      RotateInstant();
    }

    public void RotateInstant()
    {
      if (isPaused) return;
      // Rotate instantly when changing direction
      if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
      {
        transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
      }
    }

    public void InputMouse_OrbitCamera()
    {
      if (camMove == null) return;

      if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
      {
        Cursor.visible = false;
        camMove.SetOrbitAxis(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
      }
      else
      {
        camMove.SetOrbitAxis();
        Cursor.visible = true;
      }
    }

    static public bool IsAgentOnNavMesh(GameObject agentObject, float onMeshThreshold)
    {
      Vector3 agentPosition = agentObject.transform.position;

      // Check for nearest point on navmesh to agent, within onMeshThreshold
      if (NavMesh.SamplePosition(agentPosition, out NavMeshHit hit, onMeshThreshold, NavMesh.AllAreas))
      {
        // Check if the positions are vertically aligned
        if (Mathf.Approximately(agentPosition.x, hit.position.x)
            && Mathf.Approximately(agentPosition.z, hit.position.z))
        {
          // Lastly, check if object is below navmesh
          return agentPosition.y >= hit.position.y;
        }
      }

      return false;
    }

    private void MovePlayerToPoint()
    {
      if (cam == null)
      {
        return;
      }

      if (EventSystem.current.IsPointerOverGameObject()) return;

      Ray ray = cam.ScreenPointToRay(Input.mousePosition);

      Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 0.5f);

      if (Physics.Raycast(ray, out RaycastHit hit, 100, ~IgnoreLayer))
      {
        Interactable interactable = hit.collider.GetComponent<Interactable>();
        if (interactable != null)
        {
          raycastControl.InteractMovable(false);
          SetFocus(interactable);
        }
        else
        {
          raycastControl.InteractMovable(true);
          RemoveFocus();
          MoveToPoint(hit.point);
          camMove.ResetLookOnTarget();
        }
      }

      NavMeshPath path = new();
      navMeshAgent.CalculatePath(hit.point, path);
      if (path.status == NavMeshPathStatus.PathPartial)
      {
        return;
      }

      navMeshAgent.SetDestination(hit.point);
    }

    public void MoveToPoint(Vector3 point)
    {
      if (!DestinationIsReachable(/*transform.position,*/ point))
      {
        return;
      }
      NavMeshPath path = new();
      navMeshAgent.CalculatePath(point, path);
      if (path.status == NavMeshPathStatus.PathPartial)
      {
        return;
      }

      newCursor = Instantiate(cursorAnimObject, point, Quaternion.identity);
      navMeshAgent.SetDestination(point);

      //moveDestination = point;

      Destroy(newCursor, 5f);
    }

    private bool DestinationIsReachable(/*Vector3 position,*/ Vector3 destination)
    {
      NavMeshPath meshPath = new();
      NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, meshPath);
      return meshPath.status != NavMeshPathStatus.PathInvalid;
    }

    public void SetFocus(Interactable newFocus)
    {
      if (focus != null)
      {
        // Debug.Log("Set focus 'AH'");
        focus.OnDefocused();
      }
      focus = newFocus;
      newFocus.OnFocused(transform);
    }

    public void RemoveFocus()
    {
      if (focus != null)
      {
        focus.OnDefocused();
      }
      focus = null;
    }

    public bool IsPaused()
    {
      return isPaused;
    }
    public void SetPause(bool b)
    {
      isPaused = b;
    }

    public void ToggleIsPause()
    {
      isPaused = !isPaused;
    }

    public void SetMouse(bool b)
    {
      canMouse = b;
    }

    public void WarpPlayer(Transform chosenPosition)
    {
      Debug.Log("Is it this?");
      navMeshAgent.enabled = false;
      gameObject.transform.SetPositionAndRotation(chosenPosition.position, chosenPosition.localRotation);
      navMeshAgent.enabled = true;
    }

    public float GetRotateSpeed()
    {
      return rotateSpeed;
    }

    public void SetMoveSpeedModifier(float value)
    {
      float percentage = value / 100f;
      baseSpeed = 1 + percentage;
    }
  }
}