using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.VisualScripting;
using System.Data;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameover = false; // 게임 오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트
    public int onButtonCount = 0;
    public bool isCubeReady = true;
    private int score = 0; // 게임 점수

    [SerializeField] private Transform spawnedObjectPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"{GameManager.instance.onButtonCount}");

        if (onButtonCount>=2 && isCubeReady)
        {
            isCubeReady=false;

            for (int i=0; i<7; i++)
            {
                Transform spawnedObjectTransform = Instantiate(spawnedObjectPrefab);
                spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
                spawnedObjectTransform.GetComponent<Rigidbody>().isKinematic = false;
                spawnedObjectTransform.position = 
                        new Vector3(Random.Range(-10f, 40f), 5, Random.Range(30f, 90f));
                
            }
            GameObject targetObject = GameObject.Find("ChasingMonster"); // "ObjectName"은 활성화할 오브젝트 이름
            if (targetObject != null)
            {
                targetObject.GetComponent<ChasingEnemy>().OnChasing();
            }
            else
            {
                Debug.Log("오브젝트를 찾을 수 없거나 이미 활성화 상태입니다.");
            }
        }

    }

}
