using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject Menu;

    [SerializeField] private Animator[] _mainMenuEnemiesAnimators;
    [SerializeField] private Animator _fadingPanelAnimator;

    [SerializeField] private float _fadeDelay = 0.8f;

    public void StartGame()
    {
        Debug.Log("Start");
        StartCoroutine(BeginStart());
    }

    public IEnumerator BeginStart()
    {
        EnemiesAttack();
        yield return new WaitForSeconds(_fadeDelay);
        _fadingPanelAnimator.gameObject.SetActive(true);
        _fadingPanelAnimator.SetTrigger("Fade");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(1);
    }

    private void EnemiesAttack()
    {
        foreach (var animator in _mainMenuEnemiesAnimators)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void OpenOptionsMenu()
    {
        Menu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void ReturnBackToMainMenu()
    {
        OptionsMenu.SetActive(false);
        Menu.SetActive(true);
    }

    public void QuitGame()
    {
        //Exit game
    }
}
