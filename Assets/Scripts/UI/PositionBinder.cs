using UnityEngine;

public class PositionBinder : MonoBehaviour
{
    public Transform objectToBind;
    private float zOffset = 10f;

    void Update()
    {        
        Vector3 parentPos = objectToBind.position + new Vector3(0, 0, zOffset);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(parentPos);
        transform.position = worldPos;        
    }
}
