using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The player class contains the common features for green and purple players
public abstract class Car : MonoBehaviour
{
    protected GameManager gameManager;

    protected FieldOfView fieldOfView;

    protected List<GameObject> obstacles;
    protected GameObject prison;
    protected GameObject target;

    protected Vector3 direction;

    //Max & Min values for car's speed
    protected float minSpeed = 10f;
    protected float maxSpeed = 13f;

    //Max & Min values for car's rotation speed
    protected float minRotationSpeed = 360f;
    protected float maxRotationSpeed = 720f;

    protected float carRotationSpeed;
    protected float carSpeed;

    protected string policeTag;
    protected string sedanTag;
    protected string obstacleTag;
    protected string wallTag;
    protected string jailedTag;

    protected float catchDistance;
    protected float jailedDistance;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance();

        policeTag = gameManager.GetPoliceTag();
        sedanTag = gameManager.GetSedanTag();

        obstacleTag = gameManager.ObstacleTag();
        wallTag = gameManager.GetWallTag();

        jailedTag = gameManager.GetJailedTag();
        prison = gameManager.GetPrison();

        catchDistance = gameManager.GetCatchDistance();
        jailedDistance = gameManager.GetJailedDistance();

        fieldOfView = GetComponent<FieldOfView>();

        obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag(obstacleTag));
       
        carRotationSpeed = SetCarRotation();
        carSpeed = SetCarSpeed();

        MoveToRandom();
    }

    public Vector3 BarrierBounce(GameObject visibleObject)
    {
        float x = transform.position.x - visibleObject.transform.position.x;
        float z = transform.position.z - visibleObject.transform.position.z;

        direction = new Vector3(x, 0f, z) - transform.position;

        direction = new Vector3(direction.x, 0f, direction.z).normalized;

        return direction;
    }

    public Vector3 Bounce(GameObject visibleObject)
    {
        direction.x = transform.position.x - visibleObject.transform.position.x;
        direction.z = transform.position.z - visibleObject.transform.position.z;

        direction = new Vector3(direction.x, 0f, direction.z).normalized;

        return direction;
    }

    public float SetCarRotation()
    {
        return Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    public float SetCarSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }

    public int SortByDistance(GameObject a, GameObject b)
    {
        float squaredRangeA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredRangeB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredRangeA.CompareTo(squaredRangeB);
    }

    public void AvoidObjects()
    {
        foreach (GameObject visibleObject in fieldOfView.GetNearObstacles())
        {
            if ((gameObject.tag == policeTag && visibleObject.tag == sedanTag) || gameObject.tag == sedanTag && gameObject.GetComponent<Sedan>().Captured())
                return;

            if (visibleObject.tag == wallTag)
                direction = BarrierBounce(visibleObject);
            else
                direction = Bounce(visibleObject);

            if(direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, carRotationSpeed);
            }
            
            transform.Translate(new Vector3(direction.x, 0f, direction.z) * carSpeed * Time.deltaTime, Space.World);
        }

    }

    public void MoveToRandom()
    {
        int randomObstacle = Random.Range(0, obstacles.Count);
        Debug.Log(obstacles[randomObstacle]);
        MoveTo(obstacles[randomObstacle]);
    }

    public void Move()
    {
        if (fieldOfView.detectedObjects.Count != 0)
            AvoidObjects();

        else
        {
            if(direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, carRotationSpeed * Time.deltaTime);
            }
            
            transform.Translate(new Vector3(direction.x, 0f, direction.z) * carSpeed * Time.deltaTime, Space.World);
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void SetTarget(GameObject targetObject)
    {
        target = targetObject;
    }

    public void ChangeJailedTag()
    {
        gameObject.tag = jailedTag;
    }

    public bool TransportToPrison(Sedan sedan)
    {
        if (fieldOfView.detectedObjects.Count != 0)
            AvoidObjects();

        MoveTo(prison);

        return (Vector2.Distance(sedan.transform.position, new Vector2(prison.transform.position.x, prison.transform.position.z)) <= jailedDistance);
    }

    public void MoveTo(GameObject target)
    {
        if (fieldOfView.detectedObjects.Count != 0)
            AvoidObjects();

        direction = target.transform.position - transform.position;
        direction = new Vector3(direction.x, 0f, direction.z).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, carRotationSpeed * Time.deltaTime);
        }

        transform.Translate(new Vector3(direction.x, 0f, direction.z) * carSpeed * Time.deltaTime, Space.World);
    }
}
