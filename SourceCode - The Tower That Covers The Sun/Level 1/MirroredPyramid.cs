using System.Collections;
using UnityEngine;

public class MirroredPyramid : MonoBehaviour
{
    [SerializeField] private bool isDayPyramid;
    [SerializeField] private Material childrenMaterial;

    public GameObject pyramidFrontPiece, pyramidRightPiece, pyramidBackPiece, pyramidLeftPiece;
    public bool isResolved;
    
    private int _correctPieces;

    private void Update()
    {
        isResolved = _correctPieces == 4;

        if (!GameManager.Instance.IsTransitioning) return;
        if (GameManager.Instance.IsDay)
        {
            if (isDayPyramid)
            {
                AddToAlpha(-0.01f);
               StartCoroutine(SetAlpha(0f));
            }
            else
            {
                AddToAlpha(0.01f);
                StartCoroutine(SetAlpha(1f));
            }
        }
        else
        {
            if (isDayPyramid)
            {
                AddToAlpha(0.01f);
                StartCoroutine(SetAlpha(1f));
            }
            else
            {
                AddToAlpha(-0.01f);
                StartCoroutine(SetAlpha(0f));
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        _correctPieces++;
    }
    private void OnTriggerExit(Collider other)
    {
        _correctPieces--;
    }
    private void AddToAlpha(float value)
    {
        Color color = childrenMaterial.color;
        if (color.a > 0f || color.a < 1f)
        {
            color.a += value;
        }
        childrenMaterial.color = color;
    }
    private IEnumerator SetAlpha(float alpha)
    {
        Color color = childrenMaterial.color;
        color.a = Mathf.Clamp( alpha, 0, 1 );
        yield return new WaitForSeconds(0.5f);
        childrenMaterial.color = color;
    }
}