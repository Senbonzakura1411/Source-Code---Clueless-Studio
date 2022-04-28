using UnityEngine;
using UnityEngine.Events;

public class MakeButton : MonoBehaviour
{
    [SerializeField] private bool isStartButton;
    public UnityEvent unityEvent = new UnityEvent();
    private SIMONTEST _manager;

    private void Start()
    {
        _manager = GameObject.Find("SIMONTEST").GetComponent<SIMONTEST>();
    }

    void Update()
    {
        if (_manager.buttonsClickable || isStartButton)
        {
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray,out hit) && hit.collider.gameObject==gameObject)
                {
                    unityEvent.Invoke();
                }
            }
        }
        
    }
}
