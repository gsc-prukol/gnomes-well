using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    public Sprite spriteToUse;
    public SpriteRenderer spriteRenderer;

    private Sprite _originalSprite;
    
    public void SwapSprite()
    {
        if (spriteToUse == spriteRenderer.sprite)
        {
            return;
        }

        _originalSprite = spriteRenderer.sprite;
        spriteRenderer.sprite = spriteToUse;
    }

    public void ResetSprite()
    {
        if (_originalSprite == null)
        {
            return;
        }

        spriteRenderer.sprite = _originalSprite;
    }
}
