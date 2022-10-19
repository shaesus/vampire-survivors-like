using System.Collections;
using UnityEngine;

public class SpeedUpSpell : Spell
{
    private float _speedUpMultiplier = 3f;
    private float _speedUpTime = 10f;

    private PlayerController _playerController;

    private void Awake()
    {
        _castCooldown = 30f;
        ManaCost = 70f;
        Name = "Speed-Up Spell";
    }

    private void Start()
    {
        _playerController = Player.Instance.GetComponent<PlayerController>();

        SpellSprite = HUD.Instance.SpeedUpSpellSprite;
    }

    public override void Cast()
    {
        StartCoroutine(StartSpeedUp());
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(_castCooldown);
        CanCast = true;
        Debug.Log("Can SpeedUp");
    }

    private IEnumerator StartSpeedUp()
    {
        _playerController.ChangeSpeed(_speedUpMultiplier);
        CanCast = false;
        yield return new WaitForSeconds(_speedUpTime);
        StartCoroutine(StartCooldown());
        _playerController.SetDefaultSpeed();
    }
}
