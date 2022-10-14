using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [HideInInspector] public UnityEvent OnPlayerTakeDamage = new UnityEvent();
    [HideInInspector] public UnityEvent OnPlayerChangeHp = new UnityEvent();
    [HideInInspector] public UnityEvent OnPlayerChangeMana = new UnityEvent();

    public SpriteRenderer spriteRenderer;

    public float CurrentMana { get; private set; }
    public float MaxMana { get; private set; } = 100f;
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; } = 200f;
    public float HpPerSecond { get; set; } = 5f;
    public float ManaPerSecond { get; set; } = 20f;

    [SerializeField] private float _healDelay = 3f;

    private float _nextTimeToHeal;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;

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

    private void BlinkSprite()
    {
        StartCoroutine(Utilities.BlinkSprite(spriteRenderer));
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
