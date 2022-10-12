using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnSpellSwitch = new UnityEvent();
    [HideInInspector] public UnityEvent OnAttack = new UnityEvent();

    public GameObject ShootPoint;
    public GameObject DashTrail;
    public GameObject MeleeAttackTrail;
    public GameObject ExplosionEffect;

    public UnityAction[] Spells { get; set; }

    public Vector2 LookDirection { get; private set; }

    private Camera _mainCam;

    private Rigidbody2D _shootPointRb;

    private void Awake()
    {
        _mainCam = Camera.main;

        _shootPointRb = ShootPoint.GetComponent<Rigidbody2D>();

        Spells = new UnityAction[2] { null, null };

        OnSpellSwitch.AddListener(SwitchSpells);
    }

    private void Update()
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        LookDirection = (mousePos - (Vector2)transform.position).normalized;

        if (LookDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (LookDirection.x >= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        var angle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
        _shootPointRb.rotation = angle;

        if (Input.GetMouseButtonDown(0))
        {
            OnAttack.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Spells[0] != null)
        {
            Spells[0].Invoke();
            Debug.Log("Spell 1 casted");
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Spells[1] != null)
        {
            Spells[1].Invoke();
            Debug.Log("Spell 2 casted");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            OnSpellSwitch.Invoke();
        }
    }

    private void SwitchSpells()
    {
        (Spells[0], Spells[1]) = (Spells[1], Spells[0]);
    }
}
