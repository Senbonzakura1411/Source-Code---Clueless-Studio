using System;
using System.Collections;
using UnityEngine;

public class MainPyramid : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial, normalMaterial;
    [SerializeField] private MirroredPyramid dayMirrorPyramid, nightMirrorPyramid;
    [SerializeField] private GameObject pyramidFrontPiece, pyramidRightPiece, pyramidBackPiece, pyramidLeftPiece;

    public bool isResolved;

    private GameObject[] _piecesOrder;
    private int _correctPieces;
    private bool _canRotate = true;
    private int _selectedPiece;

    private void Start()
    {
        // Order is Front = 0, Right = 1, Back = 2, Left = 3
        _piecesOrder = new[] {pyramidFrontPiece, pyramidRightPiece, pyramidBackPiece, pyramidLeftPiece};
        UpdateHighlightedPiece(_piecesOrder[_selectedPiece], null);
    }


    private void Update()
    {
        if (_canRotate && !GameManager.Instance.IsTransitioning & !GameManager.Instance.IsCinematic)
        {
            PyramidPieceSelection();

            if (Input.GetKeyDown(KeyCode.R))
            {
                AudioManager.Instance.Play("objMovement");
                StartCoroutine(RotatePyramidPiece(_piecesOrder[_selectedPiece]));
                Debug.Log("Rotated Front");
                RotateMirroredPyramid(_selectedPiece, GameManager.Instance.IsDay);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.Instance.Play("largeObjMovement");
                UpdateHighlightedPiece(_selectedPiece == 0 ? _piecesOrder[3] : _piecesOrder[_selectedPiece - 1],
                    _piecesOrder[_selectedPiece]);
                RotatePyramid();
                Debug.Log("Rotated Pyramid");
            }
        }

        isResolved = _correctPieces == 4;
    }

    private void OnTriggerEnter(Collider other)
    {
        _correctPieces++;
    }

    private void OnTriggerExit(Collider other)
    {
        _correctPieces--;
    }

    private void RotateMirroredPyramid(int i, bool isDay)
    {
        if (isDay)
        {
            switch (i)
            {
                case 0:
                    StartCoroutine(RotatePyramidPiece(dayMirrorPyramid.pyramidFrontPiece));
                    break;
                case 1:
                    StartCoroutine(RotatePyramidPiece(dayMirrorPyramid.pyramidRightPiece));
                    break;
                case 2:
                    StartCoroutine(RotatePyramidPiece(dayMirrorPyramid.pyramidBackPiece));
                    break;
                case 3:
                    StartCoroutine(RotatePyramidPiece(dayMirrorPyramid.pyramidLeftPiece));
                    break;
                default:
                    Console.WriteLine("F");
                    break;
            }
        }
        else
        {
            switch (i)
            {
                case 0:
                    StartCoroutine(RotatePyramidPiece(nightMirrorPyramid.pyramidFrontPiece));
                    break;
                case 1:
                    StartCoroutine(RotatePyramidPiece(nightMirrorPyramid.pyramidRightPiece));
                    break;
                case 2:
                    StartCoroutine(RotatePyramidPiece(nightMirrorPyramid.pyramidBackPiece));
                    break;
                case 3:
                    StartCoroutine(RotatePyramidPiece(nightMirrorPyramid.pyramidLeftPiece));
                    break;
                default:
                    Console.WriteLine("Fx2");
                    break;
            }
        }
    }

    private void PyramidPieceSelection()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.Play("optionSound");
            var temp = _piecesOrder[_selectedPiece];

            if (_selectedPiece == 0)
            {
                _selectedPiece = 3;
            }   
            else
            {
                _selectedPiece--;
            }

            UpdateHighlightedPiece(_piecesOrder[_selectedPiece], temp);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.Instance.Play("optionSound");
            var temp = _piecesOrder[_selectedPiece];

            if (_selectedPiece == 3)
            {
                _selectedPiece = 0;
            }
            else
            {
                _selectedPiece++;
            }

            UpdateHighlightedPiece(_piecesOrder[_selectedPiece], temp);
        }
    }

    private void UpdateHighlightedPiece(GameObject newPiece, GameObject oldPiece)
    {
        if (newPiece != null)
        {
            newPiece.GetComponent<MeshRenderer>().material = highlightMaterial;
        }

        if (oldPiece != null)
        {
            oldPiece.GetComponent<MeshRenderer>().material = normalMaterial;
        }
    }

    private IEnumerator RotatePyramidCoroutine()
    {
        _canRotate = false;
        for (var i = 0; i < 18; i++)
        {
            transform.Rotate(0f, -5f, 0f);
            yield return new WaitForSeconds(0.01f);
        }

        _canRotate = true;
    }

    private IEnumerator RotatePyramidPiece(GameObject piece)
    {
        _canRotate = false;
        for (var i = 0; i < 18; i++)
        {
            piece.transform.Rotate(0, 0, 5f, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }

        _canRotate = true;
    }

    private void ReorderAfterRotation()
    {
        pyramidBackPiece = _piecesOrder[1];
        pyramidLeftPiece = _piecesOrder[2];
        pyramidFrontPiece = _piecesOrder[3];
        pyramidRightPiece = _piecesOrder[0];
        _piecesOrder = new[] {pyramidFrontPiece, pyramidRightPiece, pyramidBackPiece, pyramidLeftPiece};
    }


    // Pivot needs to be centered on children for this to work properly
    public void RotatePyramid()
    {
        StartCoroutine(RotatePyramidCoroutine());
        ReorderAfterRotation();
    }
}