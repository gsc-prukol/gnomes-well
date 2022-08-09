using System.Collections;
using UnityEngine;

public class RemoveAfterDelay : MonoBehaviour
{
    public float delay = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(nameof(Remove));
    }

    private IEnumerable Remove()
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
