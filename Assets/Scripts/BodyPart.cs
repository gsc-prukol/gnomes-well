using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour
{
    public Sprite detachedSprite;
    public Sprite burnedSprite;

    public Transform bloodFountainOrigin;

    private bool _detached = false;

    public void Detach()
    {
        _detached = true;

        this.tag = Tags.UNTAGGED;
        transform.SetParent(null, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_detached)
        {
            return;
        }

        var rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody.velocity.magnitude < 0.001f)
        {
            foreach(Joint2D joint in GetComponentsInChildren<Joint2D>())
            {
                Destroy(joint);
            }

            foreach(Rigidbody2D body in GetComponentsInChildren<Rigidbody2D>())
            {
                Destroy(body);
            }
            
            foreach(Collider2D collider in GetComponentsInChildren<Collider2D>())
            {
                Destroy(collider);
            }

            Destroy(this);
        }
    }

    public void ApplyDamageSprite(Gnome.DamageType damageType)
    {
        Sprite spriteToUse = null;

        switch (damageType)
        {
            case Gnome.DamageType.Burning:
                spriteToUse = burnedSprite;
                break;
                
            case Gnome.DamageType.Slicing:
                spriteToUse = detachedSprite;
                break;
        }

        if (spriteToUse == null)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = spriteToUse;
    }
}
