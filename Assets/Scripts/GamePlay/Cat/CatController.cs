using System;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public event Action OnCatCatched;

    [Header("References")]
    [SerializeField] private PlayerController playerContoller;
    [SerializeField] private Transform playerTransform;

    [Header("Settings")]
    [SerializeField] private float defaultSpeed = 5f;
    [SerializeField] private float chaseSpeed = 7f;

    [Header("Navigation Settings")]
    [SerializeField] private float patrolRadius = 10f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private int maxDestinationAttempts = 10;
    [SerializeField] private float chaseDistanceThreshold = 1.5f;
    [SerializeField] private float chaseDistance = 2f;

    private NavMeshAgent catAgent;
    private CatStateController catStateController;

    private float timer;
    private bool isWaiting;
    private bool isChasing;
    private Vector3 initialPosition;

    private void Awake()
    {
        catAgent = GetComponent<NavMeshAgent>();
        catStateController = GetComponent<CatStateController>();
    }

    private void Start()
    {
        initialPosition = transform.position;
        SetRandomDestination();
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume 
            && GameManager.Instance.GetCurrentGameState() != GameState.CutScene)
        {
            catAgent.speed = 0f;
            return;
        }

        if (playerContoller.CanCatChase())
        {
            SetChaseMovemet();
        } else
        {
            SetPatrolMovement();
        }
    }

    private void SetChaseMovemet()
    {
        isChasing = true;

        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        Vector3 offsetPosition = playerTransform.position - directionToPlayer * chaseDistanceThreshold;
        catAgent.SetDestination(offsetPosition);
        catAgent.speed = chaseSpeed;
        catStateController.ChangeState(CatState.Running);

        if(Vector3.Distance(transform.position, playerTransform.position) <= chaseDistance && isChasing)
        {
            OnCatCatched?.Invoke();
            catStateController.ChangeState(CatState.Attacking);
            isChasing = false;
        }
    }
    private void SetPatrolMovement()
    {
        catAgent.speed = defaultSpeed;
        if (!catAgent.pathPending && catAgent.remainingDistance <= catAgent.stoppingDistance)
        {
            if(!isWaiting)
            {
                isWaiting = true;
                timer = waitTime;
                catStateController.ChangeState(CatState.Idle);
            }
        }

        if (isWaiting)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                isWaiting= false;
                SetRandomDestination();
                catStateController.ChangeState(CatState.Walking);
            }
        }
    }

    private void SetRandomDestination()
    {
        int attemps = 0;
        bool destinationSet = false;

        while (attemps < maxDestinationAttempts && !destinationSet) 
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
            randomDirection += initialPosition;

            if(NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;
                if (IsPositionBlocked(finalPosition))
                {
                    catAgent.SetDestination(finalPosition);
                    destinationSet = true;
                }
                else
                {
                    attemps++;
                }
            }
            else
            {
                attemps++;
            }
        }

        if (!destinationSet)
        {
            Debug.LogWarning("Failed to find a valid destination");
            isWaiting = true;
            timer = waitTime * 2;
        }
    }

    private bool IsPositionBlocked(Vector3 position)
    {
        if(NavMesh.Raycast(transform.position, position, out NavMeshHit hit, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }
}
