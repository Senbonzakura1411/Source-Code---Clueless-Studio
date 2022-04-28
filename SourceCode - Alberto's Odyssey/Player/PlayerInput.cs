using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 WorldXZPositionVector { get; private set; }

    public Vector3 WorldFixedYPositionVector { get; private set; }

    public bool MouseLeftClick { get; private set; }
    
    public bool ReloadInput { get; private set; }

    private Camera _mainCamera;

    private void Awake() => _mainCamera = FindObjectOfType<Camera>();

    private void Update()
    {
        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, 50f))
        {
            Vector3 pointToLook = hitInfo.point;
            pointToLook.y = 0f;
            WorldXZPositionVector = pointToLook;
            pointToLook.y = 1.4f;
            WorldFixedYPositionVector = pointToLook;
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
        }

        MouseLeftClick = Input.GetKeyDown(KeyCode.Mouse0);

        ReloadInput = Input.GetKeyDown(KeyCode.R);
    }
}