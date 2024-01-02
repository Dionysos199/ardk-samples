using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    int input;
    public GameObject[] shapes;
    private GameObject objectToSpawn;
    private string targetTag; // Set the tag of the surfaces where objects can be spawned

    private GameObject spawnedObject;
    private bool isTouching = false;
    public void switchShape(int n)
    {
        input = n;
    }
    void Update()
    {
        targetTag = "SpawnSurface";
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(targetTag))
                    {
                        // Spawn an object along the hit normal
                        spawnedObject = SpawnObject(hit.point, hit.normal);
                        
                        spawnedObject.transform.parent = hit.collider.transform;
                        isTouching = true;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isTouching && spawnedObject != null)
                    {
                        // Update the position of the spawned object along the hit normal
                        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(targetTag))
                        {
                           // spawnedObject.transform.position = hit.point;
                            Debug.Log(hit.collider.transform.name);

                            UpdateObjectTransform(spawnedObject,hit.collider.transform, hit.point, hit.normal);
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    isTouching = false;
                    break;
            }
        }
    }
    void UpdateObjectTransform(GameObject obj, Transform targetObj, Vector3 position, Vector3 normal)
    {
        // Update the position and rotation of the object based on the hit information

        // Translate the object 1 cm along its normal
        Vector3 offset = normal.normalized * 0.01f;
        obj.transform.position = position + offset;
        
        // Rotate the object to align with the hit normal
        obj.transform.rotation = Quaternion.LookRotation(targetObj.transform.right, normal);
    }
    GameObject SpawnObject(Vector3 position, Vector3 normal)
    {
        objectToSpawn = shapes[input];
        // Instantiate the object at the hit position
        GameObject spawned = Instantiate(objectToSpawn, position, Quaternion.identity);

        // Rotate the spawned object to align with the hit normal
        spawned.transform.rotation = Quaternion.LookRotation(normal);
        return spawned;
    }
}
