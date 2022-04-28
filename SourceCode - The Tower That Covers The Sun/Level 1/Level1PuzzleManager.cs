using UnityEngine;
using UnityEngine.UI;

public class Level1PuzzleManager : MonoBehaviour
{
    [SerializeField] private MainPyramid mainPyramid;
    [SerializeField] private MirroredPyramid dayMirrorPyramid, nightMirrorPyramid;
    [SerializeField] private Button sunButton, moonButton;

    public Level1Cinematics lvCinematic;

    private void Awake()
    {
        AudioManager.Instance.Pause("menu");
        AudioManager.Instance.Play("nightTheme1");
    }

    private void Update()
    {
        if (mainPyramid.isResolved && dayMirrorPyramid.isResolved & nightMirrorPyramid.isResolved)
        {
            Debug.Log("Puzzle is completed");
            lvCinematic.StartFinalCinematic();
        }
    }

    public void CycleTime()
    {
        if (GameManager.Instance.IsDay)
        {
            sunButton.interactable = false;
            sunButton.GetComponent<Image>().raycastTarget = false;
            moonButton.interactable = true;
            moonButton.GetComponent<Image>().raycastTarget = true;
        }
        else
        {
            sunButton.interactable = true;
            sunButton.GetComponent<Image>().raycastTarget = true;
            moonButton.interactable = false;
            moonButton.GetComponent<Image>().raycastTarget = false;
        }
        GameManager.Instance.CycleTimeOfDay();
    }
}
