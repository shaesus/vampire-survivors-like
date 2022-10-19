using System.Collections;
using UnityEngine;

public class DashSpell : Spell
{
    public float DashForce { get; set; } = 55f;

    private float _dashingTime = 0.5f;

    private Player _playerInstance;
    private PlayerController _playerController;
    private Rigidbody2D _playerRb;
    private PlayerCombat _playerCombat;

    private void Awake()
    {
        _castCooldown = 1f;
        _manaCost = 20f;
    }

    private void Start()
    {
        _playerInstance = Player.Instance;
        _playerRb = _playerInstance.GetComponent<Rigidbody2D>();
        _playerController = _playerInstance.GetComponent<PlayerController>();
        _playerCombat = _playerInstance.gameObject.GetComponent<PlayerCombat>();

        SpellSprite = HUD.Instance.DashSpellSprite;
    }

    public override void Cast()
    {
        if (_canCast && Player.Instance.CurrentMana >= _manaCost)
        {
            StartCoroutine(Dash());
            Player.Instance.DecreaseMana(_manaCost);
        }
        else if (Player.Instance.CurrentMana <= _manaCost)
        {
            Debug.Log("Not enough mana!");
        }
    }

    private IEnumerator Dash()
    {
        _canCast = false;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Enemy"));

        StartCoroutine(DisableSpriteRenderer(_playerInstance.SR, 0.25f));

        if (_playerController.Movement.magnitude == 0)
        {
            _playerRb.AddForce(Vector2.right * _playerInstance.transform.localScale.x
                * DashForce, ForceMode2D.Impulse);
        }
        else
        {
            _playerRb.AddForce(_playerController.Movement.normalized * DashForce, ForceMode2D.Impulse);
        }

        SpawnTrail(_playerCombat.DashTrail);

        yield return new WaitForSeconds(_dashingTime);

        Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer("Player"),
            Physics2D.AllLayers);

        yield return new WaitForSeconds(_castCooldown);

        _canCast = true;
    }

    private void SpawnTrail(GameObject dashTrail)
    {
        var playerPosition = Player.Instance.transform.position;

        var angle = Utilities.GetAngle(Vector2.right, _playerController.Movement);

        var trail = Instantiate(dashTrail,
           new Vector3(playerPosition.x, playerPosition.y - 0.3f, playerPosition.z),
           Quaternion.AngleAxis(angle, Vector3.forward));

        if (angle > 90 && angle < 270)
        {
            trail.transform.localScale = new Vector3(1, -1, 1);
        }

        var localScale = trail.transform.localScale;
        trail.transform.localScale = new Vector3(localScale.x * DashForce / 10f / 5.5f, localScale.y, localScale.z);

        var trailRb = trail.GetComponent<Rigidbody2D>();
        trailRb.MovePosition(trailRb.position + _playerController.Movement.normalized
            * DashForce / 10f / 2);

        trail.GetComponent<SpriteRenderer>().enabled = true;
        trail.GetComponent<Animator>().enabled = true;

        Destroy(trail, 0.7f);
    }

    private IEnumerator DisableSpriteRenderer(SpriteRenderer spriteRenderer, float time)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(time);
        spriteRenderer.enabled = true;
    }

    public override void LvlUp()
    {
        _lvl++;
        DashForce *= 1.1f;
    }
}
