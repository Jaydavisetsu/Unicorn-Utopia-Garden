using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject characterPrefab1;
    public GameObject characterPrefab2;
    public Transform spawnLocation1;
    public Transform spawnLocation2;

    private bool videoPlayed = false;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; // Subscribe to the event when the video finishes
            videoPlayer.Play(); // Start playing the video
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (!videoPlayed)
        {
            videoPlayed = true; // Ensure this is only called once
            SpawnCharacters();
        }
    }

    void SpawnCharacters()
    {
        if (characterPrefab1 != null && spawnLocation1 != null)
        {
            Instantiate(characterPrefab1, spawnLocation1.position, spawnLocation1.rotation);
        }

        if (characterPrefab2 != null && spawnLocation2 != null)
        {
            Instantiate(characterPrefab2, spawnLocation2.position, spawnLocation2.rotation);
        }
    }
}


