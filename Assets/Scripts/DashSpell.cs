using System.Collections;
using UnityEngine;

public class DashSpell : Spell
{
    private readonly float _dashingTime = 0.5f;
    
    private float _dashForce = 55f;
    
    private Player _playerInstance;
    private PlayerController _playerController;
    private Rigidbody2D _playerRb;
    private PlayerCombat _playerCombat;

    private void Awake()
    {
        _castCooldown = 1f;
        ManaCost = 20f;
        
        Name = "Dash Spell";
    }

    public override string GetDescription()
    {
        return "Allows you to dash.";
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
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        CanCast = false;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Enemy"));

        StartCoroutine(DisableSpriteRenderer(_playerInstance.SR, 0.25f));

        if (_playerController.Movement.magnitude == 0)
        {
            _playerRb.AddForce(Vector2.right * _playerInstance.transform.localScale.x
                * _dashForce, ForceMode2D.Impulse);
        }
        else
        {
            _playerRb.AddForce(_playerController.Movement.normalized * _dashForce, ForceMode2D.Impulse);
        }

        SpawnTrail(_playerCombat.DashTrail);

        StartCoroutine(EnableCollision());

        yield return new WaitForSeconds(_castCooldown);

        CanCast = true;
    }

    private IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(_dashingTime);

        Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer("Player"),
            Physics2D.AllLayers);
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
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
        trail.transform.localScale = new Vector3(localScale.x * _dashForce / 10f / 5.5f, localScale.y, localScale.z);

        var trailRb = trail.GetComponent<Rigidbody2D>();
        trailRb.MovePosition(trailRb.position + _playerController.Movement.normalized
            * _dashForce / 10f / 2);

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
        _dashForce *= 1.1f;
    }
}
