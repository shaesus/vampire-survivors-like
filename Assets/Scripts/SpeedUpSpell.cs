using System.Collections;
using UnityEngine;

public class SpeedUpSpell : Spell
{
    private float _speedUpMultiplier = 2f;
    private float _speedUpTime = 5f;

    private PlayerController _playerController;

    private void Awake()
    {
        _castCooldown = 15f;
        ManaCost = 70f;

        Name = "Speed-Up Spell";
    }

    public override string GetDescription()
    {
        return "Increases speed by " + _speedUpMultiplier + " times for " + _speedUpTime + " seconds.";
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
        CanCast = false;
        yield return new WaitForSeconds(_castCooldown);
        CanCast = true;
        Debug.Log("Can SpeedUp");
    }

    private IEnumerator StartSpeedUp()
    {
        _playerController.ChangeSpeed(_speedUpMultiplier);
        StartCoroutine(StartCooldown());
        yield return new WaitForSeconds(_speedUpTime);
        _playerController.SetDefaultSpeed();
    }

    public override void LvlUp()
    {
        _speedUpMultiplier += 0.1f;
        _speedUpTime += 1f;
    }
}
