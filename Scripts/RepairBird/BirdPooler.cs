using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using DG.Tweening;

public class BirdPooler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnvironmentReferences environmentReferences;
    [SerializeField] private GameEvent failed;
    [SerializeField] private GameEvent success;
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private Transform birdSpawnPosition;
    [SerializeField] private Transform birdDespawnPosition;
    private List<DamagedBird> birdsToRepair = new List<DamagedBird>();
    private float coolDownTimer;

    [Header("ConveyorBelt")]
    [SerializeField] private ScrollTexture scrollTexture;
    [SerializeField] private float offsetForTextureScroll;
    [SerializeField] private float despawnDistance = 0.1f;

    [Header("Difficulty Settings")]
    [SerializeField] private FloatVariable spawnDelay;
    [SerializeField] private FloatVariable timeWindow;
    
    [Header("Pooling")]
    private List<GameObject> asleepObjects = new List<GameObject>();
    private List<GameObject> usedObjects = new List<GameObject>();
    [SerializeField] private GameObject poolObjectsParent;
    [SerializeField] private int bridsAmount;

    #region DefinitionOfPool
    public void InitPool()
    {
        for (int i = 0; i < bridsAmount; i++)
        {
            InitGO(birdPrefab);
        }
    }

    private void InitGO(GameObject g)
    {
        GameObject go = Instantiate(g, Vector3.zero, Quaternion.identity, poolObjectsParent.transform);
        asleepObjects.Add(go);
        go.SetActive(false);
    }

    public void ExpandPool(GameObject g)
    {
        InitGO(g);
    }

    public GameObject Pop(GameObject g)
    {
        GameObject go = asleepObjects.Find(x => g.name + "(Clone)" == x.name);

        if (go == null)
        {
            ExpandPool(g);

            go = asleepObjects.Find(x => g.name + "(Clone)" == x.name);
        }

        go.SetActive(true);

        asleepObjects.Remove(go);
        usedObjects.Add(go);

        return go;
    }

    public void Push(GameObject go)
    {
        go.transform.parent = poolObjectsParent.transform;
        usedObjects.Remove(go);
        asleepObjects.Add(go);
        go.SetActive(false);
    }

    #endregion Pooler

    void Start()
    {
        scrollTexture = environmentReferences.scrollTexture;
        InitPool();
    }

    // Update is called once per frame
    void Update()
    {
        DespawnBirds();
        PoolBirds();
        scrollTexture.scroll.y = scrollTexture.scroll.y + (Time.deltaTime * (1 / timeWindow.Value) * offsetForTextureScroll);
    }

    public void OnRestartDo()
    {
        for (int i = birdsToRepair.Count - 1; i >= 0; i--)
        {
            Destroy(birdsToRepair[i].gameObject);
            birdsToRepair.Remove(birdsToRepair[i]);
        }
    }

    private void PoolBirds()//rewrite into pooling for performance
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        else
        {
            SpawnBird();
            coolDownTimer = spawnDelay.Value;
        }
    }

    private void DespawnBirds()
    {
        for (int i = birdsToRepair.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(birdsToRepair[i].transform.position, birdDespawnPosition.position) < despawnDistance)
            {
                if (birdsToRepair[i].IsDamaged())
                {
                    failed.Raise();
                }
                else
                {
                    success.Raise();
                }
                Push(birdsToRepair[i].gameObject);
                birdsToRepair.Remove(birdsToRepair[i]);
            }
        }
    }

    private void SpawnBird()
    {
        DamagedBird db = Pop(birdPrefab).GetComponent<DamagedBird>();
        db.transform.position = birdSpawnPosition.position;
        db.transform.rotation = birdSpawnPosition.rotation;
        db.Initialize();
        birdsToRepair.Add(db);
        db.transform.DOMove(birdDespawnPosition.position, timeWindow.Value).SetEase(Ease.Linear);
    }
}
