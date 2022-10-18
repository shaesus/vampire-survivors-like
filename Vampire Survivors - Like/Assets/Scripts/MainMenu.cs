using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator[] MainMenuEnemies;

    public void StartGame()
    {
        StartCoroutine(BeginStart());
    }

    public IEnumerator BeginStart()
    {
        EnemiesAttack();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }

    private void EnemiesAttack()
    {
        foreach (var animator in MainMenuEnemies)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void QuitGame()
    {
        //Exit game
    }
}
