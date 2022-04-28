using UnityEngine;

public class PlayerVisualFX : MonoBehaviour
{
    private float _playerPos;
    private Material _mat;

    private static readonly int HoloStripes = Shader.PropertyToID("_HologramStripesAmount)");
    private static readonly int HologramStripesSpeed = Shader.PropertyToID("_HologramStripesSpeed");
    private static readonly int ChromAberrAmount = Shader.PropertyToID("_ChromAberrAmount");
    private static readonly int GlitchAmount = Shader.PropertyToID("_GlitchAmount");

    private void Start()
    {
        _mat = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        _playerPos = transform.parent.transform.position.x;
        _mat.SetFloat(HoloStripes, 0.5f - _playerPos * 0.0005f); // 0 to 1
        _mat.SetFloat(HologramStripesSpeed, 10f + _playerPos/60f); // 0 to 20
        _mat.SetFloat(ChromAberrAmount, 0f + _playerPos/1800f); // 0 to 1
        _mat.SetFloat(GlitchAmount, 5f + _playerPos * 0.025f); // 0 to 20
    }
}