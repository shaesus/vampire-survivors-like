using System.Collections;
using UnityEngine;

public class SpeedUpSpell : Spell
{
    private float _speedUpMultiplier = 3f;
    private float _speedUpTime = 3f;

    private PlayerController _playerController;

    private void Awake()
    {
        _castCooldown = 1f;
        _manaCost = 70f;
    }

    private void Start()
    {
        _playerController = Player.Instance.GetComponent<PlayerController>();

        SpellSprite = HUD.Instance.SpeedUpSpellSprite;
    }

    public override void Cast()
    {
        if (_canCast)
        {
            StartCoroutine(StartSpeedUp());
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(_castCooldown);
        _canCast = true;
        Debug.Log("Can SpeedUp");
    }

    private IEnumerator StartSpeedUp()
    {
        _playerController.ChangeSpeed(_speedUpMultiplier);
        _canCast = false;
        yield return new WaitForSeconds(_speedUpTime);
        StartCoroutine(StartCooldown());
        _playerController.SetDefaultSpeed();
    }
}
