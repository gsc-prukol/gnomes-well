using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    
    public float topLimit = 10f;
    public float bottomLimit = -10f;

    public float followSpeed = .5f;

    // Update is called once per frame
    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 newPosition = this.transform.position;
        float newY = newPosition.y;

        newY = Mathf.Lerp(newY, target.position.y, followSpeed);

        newY = Mathf.Min(newY, topLimit);
        newY = Mathf.Max(newY, bottomLimit);

        
        newPosition.y = newY;

        transform.position = newPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        float x = this.transform.position.x;
        float z = this.transform.position.z;

        var topPoint = new Vector3(x, topLimit, z);
        var bottomPoint = new Vector3(x, bottomLimit, z);

        Gizmos.DrawLine(topPoint, bottomPoint);
    }
}
