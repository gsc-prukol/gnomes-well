using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : MonoBehaviour
{
    public enum DamageType
    {
        Slicing,
        Burning
    }

    public Transform cameraFollowTarget;

    public Rigidbody2D ropeBody;

    public Sprite armHoldingEmpty;
    public Sprite armHoldingTreasure;

    public SpriteRenderer holdingArm;

    public GameObject deathPrefab;
    public GameObject flameDeathPrefab;
    public GameObject ghostPrefab;

    public float delayBeforeRemoving = 3.0f;
    public float delayBeforeReasingGhost = .25f;

    public GameObject bloodFountainPrefab;

    private bool _dead = false;
    private bool _holdingTreasure = false;
    public bool holdingTreasure 
    { 
        get => _holdingTreasure;
        set
        {
            if (_dead)
            {
                return;
            }

            _holdingTreasure = value;

            if (holdingArm == null)
            {
                return;
            }

            holdingArm.sprite = _holdingTreasure ? armHoldingTreasure : armHoldingEmpty;
        }
    }

    public void ShowDamageEffect(DamageType type)
    {
        GameObject prefab = type == DamageType.Burning ? flameDeathPrefab : deathPrefab;

        if (prefab == null)
        {
            return;
        }

        Instantiate(prefab, cameraFollowTarget.position, cameraFollowTarget.rotation);
    }

    public void DestroyGnome(DamageType type)
    {
        holdingTreasure = false;
        _dead = true;

        foreach (BodyPart part in GetComponentsInChildren<BodyPart>())
        {
            if (type == DamageType.Burning)
            {
                if (Random.Range(0, 2) == 0)
                {
                    part.ApplyDamageSprite(type);
                }
            }
            
            if (type == DamageType.Slicing)
            {
                part.ApplyDamageSprite(type);
            }
                
            bool shouldDetach = Random.Range(0, 2) == 0;

            if (shouldDetach)
            {
                part.Detach();

                if (type == DamageType.Slicing)
                {
                    Transform bloodFountanPosition = part.bloodFountainOrigin;
                    if (bloodFountanPosition != null && bloodFountainPrefab != null)
                    {
                        GameObject fountain = Instantiate(bloodFountainPrefab, bloodFountanPosition.position, bloodFountanPosition.rotation);
                        fountain.transform.SetParent(this.cameraFollowTarget, false);
                    }
                }

                var allJoints = part.GetComponentsInChildren<Joint2D>();

                foreach (Joint2D joint in allJoints)
                {
                    Destroy(joint);
                }
            }
        }

        var remove = gameObject.AddComponent<RemoveAfterDelay>();
        remove.delay = delayBeforeRemoving;

        StartCoroutine(ReleaseGhost());
    }

    private IEnumerator ReleaseGhost()
    {
        if (ghostPrefab == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(delayBeforeReasingGhost);

        Instantiate(ghostPrefab, transform.position, Quaternion.identity);
    }
}
