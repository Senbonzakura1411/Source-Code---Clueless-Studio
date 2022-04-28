using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float _defaultPosY;
    
    private void Start()
    {
        _defaultPosY = transform.position.y;
    }
    
    private void Update()
    {
        // Apply position
        transform.position = new Vector3(target.position.x, _defaultPosY, target.position.z);
        // Apply rotation
        transform.rotation = Quaternion.Euler(90, target.eulerAngles.y, 0);
    }
}