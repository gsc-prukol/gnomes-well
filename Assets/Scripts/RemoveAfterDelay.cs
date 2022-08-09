using System.Collections;
using UnityEngine;

public class RemoveAfterDelay : MonoBehaviour
{
    public float delay = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Remove());
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
