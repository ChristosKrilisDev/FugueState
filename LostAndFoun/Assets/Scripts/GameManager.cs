using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameView;
    public GameObject _deathMenu, _endMenu, _quitMenu;

    bool isMenuOn;

    CanvasGroup fade;
    [SerializeField] float smooth;
    [SerializeField] float timer;
    float c_timer;

    static public GameManager Instance;



    private void Awake()
    {
        if(Instance != null)
            return;

        //else
        Instance = this;
    }

    void OnStart()
    {
        Time.timeScale = 1;

        if(GameObject.FindGameObjectWithTag("Fade"))
        {
            fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<CanvasGroup>();
        }
        if(gameView)
        {
            _endMenu.SetActive(false);
            _deathMenu.SetActive(false);
            _quitMenu.SetActive(false);
        }

        if(!gameView)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isMenuOn = false;
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnESCPress();
        }

        CursorState();
    }

    public void OnDeath()
    {
        DeathMenuActivity();
    }

    public void OnWin()
    {
        EndMenuActivity();
    }



    public void CursorState()
    {

        if(isMenuOn)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    private void DeathMenuActivity()
    {
        _deathMenu.SetActive(true);
        _endMenu.SetActive(false);

        isMenuOn = true;
    }

    private void EndMenuActivity()
    {
        _endMenu.SetActive(true);
        _deathMenu.SetActive(false);

        isMenuOn = true;
    }

    private void QuitMenuActivity()
    {
        if(!gameView)
            return;

        _quitMenu.SetActive(!_quitMenu.activeInHierarchy);
        int t = 0;
        if(_quitMenu.activeInHierarchy)
            t = 0;
        else
            t = 1;

        isMenuOn = _quitMenu.activeSelf;

        Debug.Log(isMenuOn);

        Time.timeScale = t;

    }




    public void QuitApp()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnESCPress()
    {
        QuitMenuActivity();
    }




    public void FadeIn()
    {

    }

    public void FadeOut()
    {

    }



    public CanvasGroup fadeImg;
    //IEnumerator FadeFX()
    //{
    //    float point =0;
    //    if()
    //    {

    //    }
    //    else
    //    {

    //    }


    //    while ( fadeImg.alpha != point)
    //    {
    //        fadeImg.alpha -= -0.1f;
    //    }

    //}

}
