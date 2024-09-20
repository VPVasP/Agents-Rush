using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Camera[] cameras;

    private void Awake()
    {
        //singleton pattern
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (cameras.Length > 1)
        {
            cameras[1].gameObject.SetActive(false);
        }
    }
    //enable the second camera for the second player 
    public void EnableSecondCamera()
    {
        if (cameras.Length > 1)
        {
            cameras[1].gameObject.SetActive(true);
            SplitScreen();
        }
    }

    //set up the cameras for the split screen
    private void SplitScreen()
    {
        if (cameras.Length >= 2)
        {
            cameras[0].rect = new Rect(0f, 0.5f, 1f, 0.5f);
            cameras[1].rect = new Rect(0f, 0f, 1f, 0.5f);
        }
    }
}