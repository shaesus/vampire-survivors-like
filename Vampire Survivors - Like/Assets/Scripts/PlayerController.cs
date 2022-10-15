using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 Movement { get; private set; }

    [SerializeField] private float _speed = 5f;

    private float _horizontalInput;
    private float _verticalInput;

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F)) //Increment game stage
        {
            GameManager.Instance.IncrementStage();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.LvlUpAllWeaponsAndSpells();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.TogglePauseGame();
        }
    }

    private void FixedUpdate()
    {
        Movement = new Vector2(_horizontalInput, _verticalInput) * _speed * Time.fixedDeltaTime;
        transform.Translate(Movement);
    }
}
