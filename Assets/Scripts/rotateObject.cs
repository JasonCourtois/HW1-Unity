using UnityEngine;

public class rotateObject : MonoBehaviour
{
    public float rotationSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the go based on the set rotation speed.
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
