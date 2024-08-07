using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Reference to the VideoPlayer component

    public string nextSceneName;     // Name of the next scene to load after the video ends


    public void Awake()
    {
        // Subscribe to the video player's loopPointReached event
        videoPlayer.loopPointReached += EndReached;

        // Start playing the video
        videoPlayer.Play();
    }

    void EndReached(VideoPlayer vp)
    {
        // Load the next scene
        SceneManager.LoadSceneAsync("Level1");
    }
}



