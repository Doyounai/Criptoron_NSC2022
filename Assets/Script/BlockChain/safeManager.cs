using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class safeManager : MonoBehaviour
{
    public bool isMove = true;
    public List<safe> safes = new List<safe>();
    public GameObject safePanel;

    int currentuse = 0;
    bool isCorrectSafe = false;

    public GameObject correct;
    public GameObject inCorrect;

    public GameObject winnerPanel;
    public TextMeshProUGUI winnerText;

    private void Awake()
    {
        randomNewSafe();
    }

    private void Update()
    {
        if (safePanel.active && Input.GetKeyDown(KeyCode.Escape))
            inactivePanel();
    }

    private void randomNewSafe()
    {
        int correctSafe = Random.Range(0, safes.Count);

        for (int i = 0; i < safes.Count; i++)
        {
            safes[i].is_correctSafe = false;
        }

        safes[correctSafe].is_correctSafe = true;
    }

    public void activePanel(bool isCorrect, safe _safe)
    {
        safePanel.SetActive(true);
        isCorrectSafe = isCorrect;
        currentuse = safes.IndexOf(_safe);
        safes[currentuse].is_currentUse = true;
    }

    public void inactivePanel()
    {
        safePanel.SetActive(false);
        correct.SetActive(false);
        inCorrect.SetActive(false);
        safes[currentuse].is_currentUse = false;
    }

    public void result()
    {
        if(isCorrectSafe)
        {
            correct.SetActive(true);
            showWinner("Player");
        }
        else
        {
            inCorrect.SetActive(true);
        }
    }

    public void showWinner(string name)
    {
        inactivePanel();

        winnerText.text = name + " คือผู้ชนะ " + name + " ได้เป็นคนเขียนบล็อกใหม่";
        winnerPanel.SetActive(true);

        GameObject.FindObjectOfType<PlayerMovmentController>().restart();
        GameObject.FindObjectOfType<PlayerMovmentController>().is_Canmove = false;

        Bot[] bots = GameObject.FindObjectsOfType<Bot>();

        for (int i = 0; i < bots.Length; i++)
        {
            bots[i].transform.position = bots[i].startPosition;
            bots[i].stopWalking();
        }
    }

    public void again()
    {
        randomNewSafe();
        winnerPanel.SetActive(false);

        GameObject.FindObjectOfType<PlayerMovmentController>().is_Canmove = true;

        Bot[] bots = GameObject.FindObjectsOfType<Bot>();

        for (int i = 0; i < bots.Length; i++)
        {
            bots[i].playWalking();
        }
    }
}
