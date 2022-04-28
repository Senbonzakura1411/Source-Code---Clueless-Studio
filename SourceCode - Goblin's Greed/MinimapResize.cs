using UnityEngine;

public class MinimapResize : MonoBehaviour
{
    private void Start()
    {
        var h = (float)Screen.width;
        var v = (float)Screen.height;

        var xRatio = 1920f / h;
        var yRatio = 1080f / v;

        var newScale = new Vector3(transform.localScale.x * xRatio, transform.localScale.y * yRatio, 1f);

        transform.localScale = newScale;
    }
}
