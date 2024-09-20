using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class GamePhaseManager : MonoBehaviour
{
    public static GamePhaseManager instance;

    public enum GamePhase { Phase1, Phase2, Phase3 }
    public GamePhase currentPhase = GamePhase.Phase1;

    public enum GameRound { Round1, Round2 }
    public GameRound currentRound = GameRound.Round1;

    [Header("Phase Settings")]
    public GameObject[] doors;
    public GameObject[] phases;
    public GameObject bossDialogue;
    public GameObject[] waves;

    private bool hasSpawnedFruit;

    public GameObject fruitPrefab;
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
        InitializePhase1();
    }

    ///handles actions when an enemy is killed, based on the total number of enemies killed
    public void OnEnemyKilled(int totalEnemiesKilled)
    {
        switch (totalEnemiesKilled)
        {
            case 3:
                if (currentRound == GameRound.Round1)
                {
                    StartCoroutine(EndOfWave1());
                }
                break;

            case 5:
                if (currentRound == GameRound.Round2)
                {
                    EnemyManager.instance.SpawnEnemies(4);
                }
                break;

            case 6:
                if (!hasSpawnedFruit)
                {
                    EnemyManager.instance.SpawnFruit(1);
                    hasSpawnedFruit = true;
                    Debug.Log("Spawned Fruit");
                }
                break;

            case 10:
                StartPhase2();
                break;

            case 12:

                break;

            case 21:
                StartPhase3();
                break;

            default:
                break;
        }
    }

    //ends the first wave after a delay
    private IEnumerator EndOfWave1()
    {
        AudioManager.instance.SetVolume(0.3f);
        yield return new WaitForSeconds(3f);
        AudioManager.instance.SetVolume(1f);
        currentRound = GameRound.Round2;
        EnemyManager.instance.SpawnEnemies(3);
        Debug.Log("Spawned enemies 3");
    }

    //initializes settings for Phase 1
    private void InitializePhase1()
    {
        doors[0].SetActive(true);
        phases[0].SetActive(false);
        bossDialogue.SetActive(false);
    }

    //starts Phase 2
    private void StartPhase2()
    {
        currentPhase = GamePhase.Phase2;
        doors[1].SetActive(false);
        phases[0].SetActive(true);
        EnemyManager.instance.SpawnEnemies(7);
    }
    //starts Phase 3
    private void StartPhase3()
    {
        currentPhase = GamePhase.Phase3;
        doors[3].GetComponent<DoorBeforeBoss>().DisableMeshAndTrigger();
    }
}