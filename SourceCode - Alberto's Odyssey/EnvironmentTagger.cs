using UnityEngine;

public class EnvironmentTagger : MonoBehaviour
{
    private void Start()
    {
        foreach (var children in transform.GetComponentsInChildren<Transform>())
        {
            children.tag = "Environment";
        }
    }
}
