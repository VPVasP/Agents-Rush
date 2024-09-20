using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("Players")]
    public List<Transform> players = new List<Transform>();
    public Transform secondPlayer;
    public int secondPlayerHealth = 100;

    public Transform[] reSpawnPoints;

    [Header("UI Elements")]
    public Slider secondPlayerHealthSlider;
    public Slider secondPlayerRageSlider;
    public GameObject pressBToSpawnText;

    private bool hasSpawnedSecondPlayer = false;
    private bool secondPlayerFollowsFirst = true;

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
        secondPlayer.gameObject.SetActive(false);
        secondPlayerHealthSlider.gameObject.SetActive(false);
        secondPlayerRageSlider.gameObject.SetActive(false);
        pressBToSpawnText.SetActive(true);
        secondPlayerFollowsFirst = true;
    }

    private void Update()
    {
        HandleSecondPlayerSpawn();

        if (!hasSpawnedSecondPlayer && secondPlayerFollowsFirst)
        {
            FollowFirstPlayer();
        }
    }

    //checks for input to spawn the second player
    private void HandleSecondPlayerSpawn()
    {
        if (Input.GetKeyDown(KeyCode.B) && !hasSpawnedSecondPlayer && secondPlayerFollowsFirst)
        {
            SpawnSecondPlayer();
        }
    }

    ///spawns the second player and updates UI elements
    private void SpawnSecondPlayer()
    {
        secondPlayer.gameObject.SetActive(true);
        secondPlayerHealthSlider.gameObject.SetActive(true);
        secondPlayerRageSlider.gameObject.SetActive(true);
        secondPlayerHealthSlider.value = secondPlayerHealth;

        pressBToSpawnText.SetActive(false);

        hasSpawnedSecondPlayer = true;
        secondPlayerFollowsFirst = false;

        CameraManager.instance.EnableSecondCamera();
    }

    ///makes the second player follow the first player before being spawned
    private void FollowFirstPlayer()
    {
        float followSpeed = 1f;
        Vector3 targetPosition = new Vector3(players[0].position.x, 16f, players[0].position.z);
        secondPlayer.position = Vector3.Lerp(secondPlayer.position, targetPosition, Time.deltaTime * followSpeed);
    }
}