using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ContinuousMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private bool _destroyOnSceneChange = false;
    private string _currentSceneName = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        var musicSources = FindObjectsOfType<ContinuousMusic>();
        for (int i = 0; i < musicSources.Length; i++)
        {
            if (musicSources[i] != this)
            {
                // seek to the same position in the song
                _audioSource.time = musicSources[i]._audioSource.time;
                Destroy(musicSources[i].gameObject);
                break;
            }
        }

        _currentSceneName = SceneManager.GetActiveScene().name;

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneChange;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneChange;
    }

    private void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
        if (_destroyOnSceneChange && scene.name != _currentSceneName) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
