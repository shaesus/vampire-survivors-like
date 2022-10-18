using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [HideInInspector] public UnityEvent OnPlayerTakeDamage = new UnityEvent();
    [HideInInspector] public UnityEvent OnPlayerChangeHp = new UnityEvent();
    [HideInInspector] public UnityEvent OnPlayerChangeMana = new UnityEvent();
    [HideInInspector] public UnityEvent OnPlayerChangeXp = new UnityEvent();
    [HideInInspector] public UnityEvent OnPlayerLvlUp = new UnityEvent();

    public SpriteRenderer SR;

    public float CurrentXP { get; private set; }
    public float XPForNextLevel { get; private set; } = 100f;
    public float CurrentMana { get; private set; }
    public float MaxMana { get; private set; } = 100f;
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; } = 100f;
    public float HpPerSecond { get; set; } = 5f;
    public float ManaPerSecond { get; set; } = 20f;

    public int Lvl { get; private set; } = 1;

    [SerializeField] private float _healDelay = 3f;

    private float _nextTimeToHeal;

    private bool _isBlinking = false;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;

        CurrentXP = 0f;

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OnPlayerTakeDamage.AddListener(BlinkSprite);

        StartCoroutine(RegenerateMana());
        StartCoroutine(RegenerateHp());
    }

    private IEnumerator RegenerateHp()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / HpPerSecond);
            if (Time.time > _nextTimeToHeal)
            {
                if (CurrentHealth <= MaxHealth - 1)
                {
                    CurrentHealth += 1;
                    OnPlayerChangeHp.Invoke();
                }
                else
                {
                    CurrentHealth = MaxHealth;
                }
            }
        }
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / ManaPerSecond);
            if (CurrentMana <= MaxMana - 1)
            {
                CurrentMana += 1;
                OnPlayerChangeMana.Invoke();
            }
            else
            {
                CurrentMana = MaxMana;
            }
        }
    }

    public void IncreaseXP(float xp)
    {
        CurrentXP += xp;
        if (CurrentXP >= XPForNextLevel)
        {
            CurrentXP = CurrentXP % XPForNextLevel;
            XPForNextLevel *= 1.2f;
            Lvl++;

            OnPlayerLvlUp.Invoke();
        }
        OnPlayerChangeXp.Invoke();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(gameObject.name + " tookDamage!");

        CurrentHealth -= damage;

        OnPlayerTakeDamage.Invoke();

        _nextTimeToHeal = Time.time + _healDelay;

        OnPlayerChangeHp.Invoke();

        if (CurrentHealth <= 0)
            Die();
    }

    private IEnumerator StartBlinkSprite()
    {
        if (_isBlinking == false)
        {
            _isBlinking = true;
            var color = SR.color;
            var defaultAlpha = color.a;
            color.a = 0f;
            SR.color = color;

            yield return new WaitForSeconds(0.2f);

            color.a = defaultAlpha;
            SR.color = color;
            _isBlinking = false;
        }
    }

    private void BlinkSprite()
    {
        StartCoroutine(StartBlinkSprite());
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died!");

        GameManager.Instance.EndGame();
    }

    public void DecreaseMana(float manaCost)
    {
        CurrentMana -= manaCost;
        if (CurrentMana < 0)
        {
            CurrentMana = 0f;
        }

        OnPlayerChangeMana.Invoke();

        Debug.Log(CurrentMana);
    }
}
