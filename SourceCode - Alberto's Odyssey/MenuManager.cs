using System;
using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private AudioClip westernClip, thrillerClip, tumbleweedClip;
    [SerializeField] private GameObject maleDanceZombie, femaleDanceZombie;
    [SerializeField] private GameObject tumbleweedPrefab;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(MenuCoroutine());
    }

    private void Update()
    {
        if (!canvas.activeSelf && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetAxis("Mouse X") != 0))
        {
            canvas.SetActive(true);
        }
    }

    private IEnumerator MenuCoroutine()
    {
        yield return new WaitForSeconds(63.5f);
        _audioSource.PlayOneShot(tumbleweedClip);
        canvas.SetActive(false);
        _audioSource.Stop();
        tumbleweedPrefab.SetActive(true);
        yield return new WaitForSeconds(4f);
        maleDanceZombie.GetComponent<Animator>().SetBool("IsThriller", true);
        femaleDanceZombie.GetComponent<Animator>().SetBool("IsThriller", true);
        _audioSource.clip = thrillerClip;
        _audioSource.Play();
        yield return new WaitForSeconds(109f);
        _audioSource.clip = westernClip;
        _audioSource.Play();
    }
}