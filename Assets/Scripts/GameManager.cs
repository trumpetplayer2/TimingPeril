using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera[] cameras;
    public int levelID;
    private static int attemptNumber = 1;
    //public TextMeshProUGUI Score;
    //public TextMeshProUGUI Timer;
    //public TextMeshProUGUI Swaps;
    //public TextMeshProUGUI AttemptsText;
    public TextMeshProUGUI TimeUntilNextSwitch;
    public double baseScore = 100;
    public int generalTime = 60;
    public int baseTime = 30;
    public static int timeBetweenSwitches = 30;
    private int timeSinceLastSwitch = 0;
    private int timer = 0;
    private int totalTime = 0;
    private int sideID = 0;
    private float nextUpdate = 0f;
    public static GameManager instance;
    public GameObject[] pauseObjects;
    public GameObject[] resumeObjects;
    public GameObject[] goals;
    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1;
        //Null check
        if (instance == null)
        {
            //Define instance for easy access later
            instance = this;
        }else
        {
            if (instance.isPaused)
            {
                //Game last ended while paused, delete instance as they left the match to go to main menu
                Destroy(instance);
                instance = this;
            }
            else
            //Check if we're on same level, no need to reset if we are, but if we are on a different level, we do need to reset
            if (levelID != instance.levelID)
            {
                //This is a different level, IF we have stuff to carry between levels, define here
                Destroy(instance);
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        isPaused = false;
        //Toggle menu to correct states
        if (pauseObjects.Length > 0)
        {
            foreach (GameObject obj in pauseObjects)
            {
                obj.SetActive(false);
            }
        }
        if (resumeObjects.Length > 0)
        {
            foreach (GameObject obj in resumeObjects)
            {
                obj.SetActive(true);
            }
        }

        //AttemptsText.text = "Attempts: " + attemptNumber;
        timer = baseTime;
        nextUpdate = Time.time + 1f;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i != 0)
            {
                cameras[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            togglePause();
        }
        //Dont fire anything after this
        if (isPaused) { return; }
        /*-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
             * Below this line is paused when pause script is ran *
         *-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-*/
        if (Time.time > nextUpdate)
        {
            timer += 1;
            nextUpdate += 1;
            totalTime += 1;
            timeSinceLastSwitch += 1;
        }
        if (Input.GetButtonDown("Fire1") && timeSinceLastSwitch > 5)
        {
            timeSinceLastSwitch += timeBetweenSwitches - timeSinceLastSwitch;
        }

        if (timeSinceLastSwitch >= timeBetweenSwitches)
        {
            //Switch sides
            //Reset if at max side number
            if (sideID == (cameras.Length - 1))
            {
                sideID = 0;
                cameras[cameras.Length - 1].gameObject.SetActive(false);
                cameras[0].gameObject.SetActive(true);
            }
            else
            {
                cameras[sideID].gameObject.SetActive(false);
                sideID++;
                cameras[sideID].gameObject.SetActive(true);
            }
            timeSinceLastSwitch = 0;
        }
        //Timer.text = "Time: " + timer;
        TimeUntilNextSwitch.text = "Switch: " + (timeBetweenSwitches - timeSinceLastSwitch);
        //Check if all players are on the goals
        if (allOnGoal())
        {
            //You win
            finishLevel();
        }
    }

    bool allOnGoal()
    {
        bool onGoal = true;
        foreach (GameObject goal in goals)
        {
            if(goal.GetComponent<GoalScript>() == null) { continue; }
            if (!goal.GetComponent<GoalScript>().getOnGoal())
            {
                onGoal = false;
            }
        }
        return onGoal;
    }

    void finishLevel()
    {
        Debug.Log("Finished level, this is where to connect levels at");
    }

    public int getSideID()
    {
        return sideID;
    }
    public void Reset()
    {
        attemptNumber += 1;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void calculateScore()
    {
        //Calculate score
        //int SwapsUsed = Int32.Parse(Swaps.text.Substring(7).Trim());
        //Calculate Score
        //double totalScore = baseScore * generalTime / (totalTime);
        //Score.text = "Score: " + Math.Round(totalScore);
        //Reset Attempts
        attemptNumber = 1;
    }

    public void switchScenes(string scene)
    {
        if (SceneManager.GetSceneByName(scene) == null) { Debug.LogError("Scene " + scene + "did not exist!"); return; }
        SceneManager.LoadScene(scene);
    }

    public void togglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        isPaused = !isPaused;
        //Update all pause/unpause objects
        if(pauseObjects.Length > 0)
        {
            foreach(GameObject obj in pauseObjects)
            {
                obj.SetActive(isPaused);
            }
        }
        if (resumeObjects.Length > 0)
        {
            foreach (GameObject obj in resumeObjects)
            {
                obj.SetActive(!isPaused);
            }
        }
    }
}
