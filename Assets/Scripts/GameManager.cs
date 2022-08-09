using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject startingPoint;

    public Rope rope;

    public CameraFollow cameraFollow;

    public GameObject gnomePrefab;

    public RectTransform mainMenu;
    public RectTransform gameplayMenu;
    public RectTransform gameOverMenu;

    public bool gnomeInvincible { get; set; }

    public float delayAfterDeath = 1.0f;

    public AudioClip gnomeDiedSound;
    public AudioClip gameOverSound;

    private Gnome _currentGnome;

    // Start is called before the first frame update
    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        if (gameOverMenu)
        {
            gameOverMenu.gameObject.SetActive(false);
        }

        if (mainMenu)
        {
            mainMenu.gameObject.SetActive(false);
        }

        if (gameplayMenu)
        {
            gameplayMenu.gameObject.SetActive(true);
        }

        var resesObjects = FindObjectsOfType<Resettable>();

        foreach (Resettable resettable in resesObjects)
        {
            resettable.Reset();
        }

        CreateNewGnome();

        Time.timeScale = 1.0f;
    }

    private void CreateNewGnome()
    {
        RemoveGnome();

        GameObject newGnome = Instantiate(gnomePrefab, startingPoint.transform.position, Quaternion.identity);

        _currentGnome = newGnome.GetComponent<Gnome>();

        rope.gameObject.SetActive(true);

        rope.connectedObject = _currentGnome.ropeBody;

        rope.ResetLength();

        cameraFollow.target = _currentGnome.cameraFollowTarget;
    }

    private void RemoveGnome()
    {
        if (gnomeInvincible)
        {
            return;
        }

        rope.gameObject.SetActive(false);

        cameraFollow.target = null;

        if (_currentGnome == null)
        {
            return;
        }

        _currentGnome.holdingTreasure = false;

        _currentGnome.gameObject.tag = Tags.UNTAGGED;

        foreach(Transform child in _currentGnome.transform)
        {
            child.gameObject.tag = Tags.UNTAGGED;
        }

        _currentGnome = null;
    }

    private void KillGnome(Gnome.DamageType damageType)
    {
        var audio = GetComponent<AudioSource>();
        
        if (audio)
        {
            audio.PlayOneShot(this.gnomeDiedSound);
        }

        _currentGnome.ShowDamageEffect(damageType);

        if (gnomeInvincible)
        {
            return;
        }

        _currentGnome.DestroyGnome(damageType);

        RemoveGnome();

        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(delayAfterDeath);
        
        Reset();
    }

    public void TrapTouched()
    {
        KillGnome(Gnome.DamageType.Slicing);
    }

    public void FireTrapTouched()
    {
        KillGnome(Gnome.DamageType.Burning);
    }

    public void TreasureCollected()
    {
        _currentGnome.holdingTreasure = true;
    }

    public void ExitReached()
    {
        if (_currentGnome == null || !_currentGnome.holdingTreasure)
        {
            return;
        }

        var audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.PlayOneShot(this.gameOverSound);
        }

        Time.timeScale = 0f;

        if (gameOverMenu)
        {
            gameOverMenu.gameObject.SetActive(true);
        }

        if (gameplayMenu)
        {
            gameplayMenu.gameObject.SetActive(false);
        }
    }

    public void SetPaused(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
     
        mainMenu.gameObject.SetActive(paused);
        gameplayMenu.gameObject.SetActive(!paused);
    }

    public void RestartGame()
    {
        Destroy(_currentGnome.gameObject);
        _currentGnome = null;

        Reset();
    }
}
