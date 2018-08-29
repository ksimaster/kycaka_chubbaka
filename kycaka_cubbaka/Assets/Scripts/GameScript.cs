//Main script for Quiz game by Android | Helper (RU). || Основной скрипт игры викторины от Android | Helper (RU).
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour {

    [HideInInspector]
    public QuestionsList[] Questions;
    [HideInInspector]
    public int publicTimeCount = 20;
    [HideInInspector]
    public Color trueCC, falseCC,defaultCC;
    [HideInInspector]
    public int multiplierScore = 100;

    public Text questionText;
    public Button[] answerBttns = new Button[1]; // numbers Button, количество кнопок
    public Text[] answersText = new Text[1]; // numbers text in the button, тексты для кнопок (равны количеству кнопок)
    public GameObject[] answersIcons; // 0 - trueIcon; 1 - falseIcon;
    public Image headPanel;
    public GameObject exitPanel;
    public GameObject RulsPanel;
    public GameObject open_audio;
    public GameObject close_audio;
    public GameObject Audio_on_off;
    public GameObject Audio_on_off_1;
    public GameObject RulsPanel_2;
    public GameObject PausePanel;
    public GameObject finalText;
    public GameObject NumberOfTeam;
    public GameObject NameOfTeam1;
    public GameObject NameOfTeam2;
    public GameObject NameOfTeam3;
    public GameObject BeginTeam;
    public InputField InputNOT1;
    public InputField InputNOT2;
    public InputField InputNOT3;
    public Text time;
    public Text recordText;
    public Text scoreText;
    public Text BeginTeamText;
    public int ij;
    private int timeCount = 20;
    private int score = 0;
    private int scoreTeamOne = 0;
    private int scoreTeamTwo = 0;
    private int scoreTeamThree = 0;
    private float scoreForRecord;
    private int currentQ = 1;
    private bool answerClicked;
    public Texture2D editorImg;
    public Image bg;
    public GameObject BttnExit;

    // Для методов выбора команд и их названий 
    
   // public string nameTeam1, nameTeam2, nameTeam3;
    public bool numberTeam2, numberTeam3;
    public bool teamOneRC, teamTwoRC, teamThreeRC;
    public string nameT1, nameT2, nameT3;

    private bool trueColor, falseColor,defaultColor;
    private int randQ;
    private List<object> qList;
    private QuestionsList crntQ;

    void Update ()
    {
        scoreText.text = string.Format("Ваш счёт: {0:0}", score);
        scoreForRecord = Mathf.Lerp(scoreForRecord, PlayerPrefs.GetInt("score"), 6 * Time.deltaTime);
        recordText.text = string.Format("Ваш рекорд: {0:0}", scoreForRecord);
        if (defaultColor) headPanel.color = Color.Lerp(headPanel.color, defaultCC, 8 * Time.deltaTime);
        else if (trueColor) headPanel.color = Color.Lerp(headPanel.color, trueCC, 8 * Time.deltaTime);
        else if (falseColor) headPanel.color = Color.Lerp(headPanel.color, falseCC, 8 * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Escape) && !PausePanel.activeSelf) { PausePanel.SetActive(true); Time.timeScale = 0; }
        else if (Input.GetKeyDown(KeyCode.Escape) && PausePanel.activeSelf) { PausePanel.SetActive(false); Time.timeScale = 1; }
    }

    public void playBttn()
    {
        BeginTeam.SetActive(false);
        qList = new List<object>(Questions);
        generateQuestion();
        headPanel.GetComponent<Animation>().Play("HeadAnim");
        finalText.SetActive(false);
    }
    void generateQuestion()
    {
        if (qList.Count > 0)
        {
            if (scoreText.gameObject.activeSelf) scoreText.GetComponent<Animation>().Play("Bubble_Close_3");
           // randQ = Random.Range(0, qList.Count); //в случае викторины от 2х и выше
            randQ = 0; // в случае одного ответа
            crntQ = qList[randQ] as QuestionsList;
            if (crntQ != null)
            {
                questionText.text = crntQ.Question;
                questionText.GetComponent<Animation>().Play("Bubble_Open_1");
                List<string> answers = new List<string>(crntQ.answers);
                // в случае одного ответа
                // int randA = Random.Range(0, answers.Count); // рандомизатор ответа
                int randA = 0; // фиксируем ответ на первом, 0 элемент списка ответов
                answersText[0].text = answers[randA];
                answers.RemoveAt(randA);
                // в случае нескольких ответов запуск цикла for
                /*  for (int i = 0; i < crntQ.answers.Length; i++)
                  {
                      int randA = Random.Range(0, answers.Count);
                      answersText[i].text = answers[randA];
                      answers.RemoveAt(randA);
                  }
                  */
            }
            StartCoroutine(answersBttnsInAnim());
            timeCount = publicTimeCount;
            currentQ++;
        }
        else StartCoroutine(final());
    }
    public void answerBttn(int index)
    {
        answerClicked = true;
        StartCoroutine(trueOrFalse(answersText[index].text == crntQ.answers[0]));
    }
    IEnumerator final()
    {
        finalText.SetActive(true);
        yield return new WaitForSeconds(2);
        trueColor = false;
        defaultColor = true;
        headPanel.GetComponent<Animation>().Play("HeadAnimOut");
        scoreText.GetComponent<Animation>().Play("Bubble_Close_3");
        finalText.GetComponent<Animation>().Play("Bubble_Close_3");
       // if (score > PlayerPrefs.GetInt("score")) PlayerPrefs.SetInt("score", score); //Обновляет рекорд, у нас как дополнение при максимальной серии угадываний игроком
    }
    IEnumerator timer()
    {
        answerClicked = false;
        if (!time.gameObject.activeSelf) time.gameObject.SetActive(true);
        else time.GetComponent<Animation>().Play("Bubble_Open_3");
        while (timeCount > -1)
        {
            time.text = timeCount.ToString();
            timeCount--;
            yield return new WaitForSeconds(1);


            // Ниже в цикле if else решение для викторины со сбрасыванием таймера при правильном ответе
            /* if (!answerClicked)
             {
                 time.text = timeCount.ToString();
                 timeCount--;
                 yield return new WaitForSeconds(1);
             }
             else yield break;
             */
        }
        foreach (Button t in answerBttns) t.interactable = false;
        if (!answerClicked) StartCoroutine(timeOut());
    }
    IEnumerator answersBttnsInAnim()
    {
       // foreach (Button t in answerBttns) t.interactable = false;
        int i = 0;
        yield return new WaitForSeconds(1);
        //Для работы одного ответа
        if (!answerBttns[i].gameObject.activeSelf) answerBttns[i].gameObject.SetActive(true);
        else answerBttns[i].GetComponent<Animation>().Play("Bubble_Open_2");


        /*
         // Еще одна штука для работы 3х ответов
        while (i < answerBttns.Length)
        {
            if (!answerBttns[i].gameObject.activeSelf) answerBttns[i].gameObject.SetActive(true);
            else answerBttns[i].GetComponent<Animation>().Play("Bubble_Open_2");
            i++;
            yield return new WaitForSeconds(1);
        }
        */
        //  foreach (Button t in answerBttns) t.interactable = true; // эта штука перебирает кнопки, а у нас одна...
        answerBttns[0].interactable = true; //убираем и заменяем на одну команду
        yield return StartCoroutine(timer());
    }
    IEnumerator timeOut()
    {
        foreach (Button t in answerBttns) t.GetComponent<Animation>().Play("Bubble_Close_2");
        falseColor = true;
        yield return new WaitForSeconds(0.5f);
        if (!answersIcons[2].activeSelf) answersIcons[2].SetActive(true);
        else answersIcons[2].GetComponent<Animation>().Play("Bubble_Open_3");
        questionText.GetComponent<Animation>().Play("Bubble_Close_1");
        yield return new WaitForSeconds(0.5f);
        if (!scoreText.gameObject.activeSelf) scoreText.gameObject.SetActive(true);
        else scoreText.GetComponent<Animation>().Play("Bubble_Open_3");
        yield return new WaitForSeconds(2);
        answersIcons[2].GetComponent<Animation>().Play("Bubble_Close_3");
        time.GetComponent<Animation>().Play("Bubble_Close_3");
        falseColor = false;
        defaultColor = true;
        headPanel.GetComponent<Animation>().Play("HeadAnimOut");
        ij++;
        if (numberTeam2)
        {
            if (ij > 2) ij = 1;
        }else
        {
            if (ij > 3) ij = 1;
        }
        
        BeginTeam.SetActive(false);

        // if (score > PlayerPrefs.GetInt("score")) PlayerPrefs.SetInt("score", score); //Обновление рекорда, дубль для качественной работы, пока не требуется 
    }
    IEnumerator trueOrFalse(bool check)
    {
        defaultColor = false;
       // foreach (Button t in answerBttns) t.interactable = false; // опять foreach заменяем на обычную, в отличие от викторины на 3 ответа
        answerBttns[0].interactable = false;
        yield return new WaitForSeconds(1);
        if (check)
        {
           // score = score + 1;
            switch (ij)
            {
                case 1:
                    scoreTeamOne = scoreTeamOne + 1;
                    score = scoreTeamOne;
                    break;
                case 2:
                    scoreTeamTwo = scoreTeamTwo + 1;
                    score = scoreTeamTwo;
                    break;
                case 3:
                    scoreTeamThree = scoreTeamThree + 1;
                    score = scoreTeamThree;
                    break;
            }
            //Интересное решение, которое можно добавить к расширенной версии игры или доп. возможностям
            /*  score = score + (multiplierScore * currentQ) + (timeCount * multiplierScore); */
            //
            // foreach (Button t in answerBttns) t.GetComponent<Animation>().Play("Bubble_Close_2"); // опять foreach заменяем на for

            trueColor = true;
            yield return new WaitForSeconds(0.5f);
            if (!answersIcons[0].activeSelf) answersIcons[0].SetActive(true);
            else answersIcons[0].GetComponent<Animation>().Play("Bubble_Open_3");
            questionText.GetComponent<Animation>().Play("Bubble_Close_1");
            yield return new WaitForSeconds(0.5f);
            time.GetComponent<Animation>().Play("Bubble_Close_3");
            qList.RemoveAt(randQ);
            if (!scoreText.gameObject.activeSelf) scoreText.gameObject.SetActive(true);
            else scoreText.GetComponent<Animation>().Play("Bubble_Open_3");
            yield return new WaitForSeconds(1);
            answersIcons[0].GetComponent<Animation>().Play("Bubble_Close_3");
            trueColor = false;
            defaultColor = true;
            generateQuestion();
        }
        else
        {
            foreach (Button t in answerBttns) t.GetComponent<Animation>().Play("Bubble_Close_2");
            falseColor = true;
            yield return new WaitForSeconds(0.5f);
            if (!answersIcons[1].activeSelf) answersIcons[1].SetActive(true);
            else answersIcons[1].GetComponent<Animation>().Play("Bubble_Open_3");
            questionText.GetComponent<Animation>().Play("Bubble_Close_1");
            yield return new WaitForSeconds(0.5f);
            if (!scoreText.gameObject.activeSelf) scoreText.gameObject.SetActive(true);
            else scoreText.GetComponent<Animation>().Play("Bubble_Open_3");
            yield return new WaitForSeconds(1);
            answersIcons[1].GetComponent<Animation>().Play("Bubble_Close_3");
            time.GetComponent<Animation>().Play("Bubble_Close_3");
            falseColor = false;
            defaultColor = true;
            headPanel.GetComponent<Animation>().Play("HeadAnimOut");
            scoreText.GetComponent<Animation>().Play("Bubble_Close_3");
            if (score > PlayerPrefs.GetInt("score")) PlayerPrefs.SetInt("score", score);
            yield return new WaitForSeconds(1.5f);
            scoreText.gameObject.SetActive(false);
        }
    }

    public void ShowRulstBttn()
    {



       RulsPanel.SetActive(true);
        headPanel.GetComponent<Animation>().Play("HeadAnim");


    }


    public void ShowRulstBttn2()
    {



        RulsPanel.SetActive(false);
        RulsPanel_2.SetActive(true);


    }

    public void ShowRulstBttnbackRuls1()
    {


        RulsPanel_2.SetActive(false);
        RulsPanel.SetActive(true);
       


    }

    public void ShowAudioBttn()
    {



        Audio_on_off.SetActive(true);
        Audio_on_off_1.SetActive(true);
        open_audio.SetActive(false);
        close_audio.SetActive(true);


    }



    public void ShowAudioBttn_1()
    {



        Audio_on_off.SetActive(false);
        Audio_on_off_1.SetActive(false);
        close_audio.SetActive(false);
        open_audio.SetActive(true);


    }






    public void ShowRulstexit()
    {


        RulsPanel_2.SetActive(false);

        headPanel.GetComponent<Animation>().Play("HeadAnimOut");


    }

    public void RulstPan(int bttn)
    {
        if (bttn == 0)
        {
            if (score > PlayerPrefs.GetInt("score")) PlayerPrefs.SetInt("score", score);
            Application.Quit();
        }
        else
        {
            RulsPanel.SetActive(false); Time.timeScale = 1; ;
            headPanel.GetComponent<Animation>().Play("HeadAnimOut");
        }
    }



    public void ReturneMainMenu()


    {

        NumberOfTeam.SetActive(false);
        PausePanel.SetActive(false);
        headPanel.GetComponent<Animation>().Play("HeadAnimOut");





    }

    public void PusePan(int bttn)
    {
        if (bttn == 0)
        {
            if (score > PlayerPrefs.GetInt("score")) PlayerPrefs.SetInt("score", score);
            Application.Quit();
        }
        else
        {
            PausePanel.SetActive(false); Time.timeScale = 1; ;
          
        }
    }




    public void ShowExitBttn()
    {



        exitPanel.SetActive(true);
        headPanel.GetComponent<Animation>().Play("HeadAnim");


    }

    public void exitPan(int bttn)
    {
        if (bttn == 0)
        {
            if (score > PlayerPrefs.GetInt("score")) PlayerPrefs.SetInt("score", score);
            Application.Quit();
        }
        else { exitPanel.SetActive(false); Time.timeScale = 1; ;
            headPanel.GetComponent<Animation>().Play("HeadAnimOut"); }
    }

    public void ChooseNumberOfTeam()
    {
        NumberOfTeam.SetActive(true);
        headPanel.GetComponent<Animation>().Play("HeadAnim");
        

    }

    public void StatTable()
    {


    }


    public void SaveNameTeamOne()
    {

        nameT1 = InputNOT1.text;
        if (nameT1 == "") nameT1 = "Кусаки";
        Debug.Log(nameT1);
     
    }

    public void SaveNameTeamTwo()
    {

        nameT2 = InputNOT2.text;
        if (nameT2 == "") nameT2 = "Чуббаки";
        Debug.Log(nameT2);

    }

    public void SaveNameTeamThree()
    {

        nameT3 = InputNOT3.text;
        if (nameT3 == "") nameT3 = "Мураши";
        Debug.Log(nameT3);

    }

    public void BttnNameOfTeamOne()
    {
       
        NameOfTeam1.SetActive(false);
        NameOfTeam2.SetActive(true);
        if (nameT1 == "") nameT1 = "Кусаки";
    }
    // ПОПРАВИТЬ и прикрутить логику от количества команд
    public void BttnNameOfTeamTwo()
    {

        NameOfTeam2.SetActive(false);
        if (numberTeam2)
        {
            BeginTeam.SetActive(true);
            ij = 1;
            BeginTeamName();
        }
        else
        {
            NameOfTeam3.SetActive(true);
        }
        if (nameT2 == "") nameT2 = "Чуббаки";
    }

    public void BttnNameOfTeamThree()
    {

        NameOfTeam3.SetActive(false);
        BeginTeam.SetActive(true);
        if (nameT3 == "") nameT3 = "Мураши";
    }

    public void BeginTeamName()
    {

        switch (ij)
        {
            case 1:
                BeginTeamText.text = "   Ход команды   " + nameT1;
                break;
            case 2:
                BeginTeamText.text = "   Ход команды   " + nameT2;
                break;
            case 3:
                BeginTeamText.text = "   Ход команды   " + nameT3;
                break;
        }

    }

    /* //Скрипт по умному с очищением input
     public void BttnNameOfTeam()
      {
          if (numberTeam2)
          {
              // Если панелька с вводом команды не включена, надо включить
              // ВОПРОС КААААК?????????
             // if(NameOfTeam.ActiveSelf) NameOfTeam.
              if (ij<3)
              { 
              switch (ij)
              {
                  case 1:
                      var input1 = gameObject.GetComponent<InputField>();
                      var nameTeam1 = new InputField.SubmitEvent();
                      //se.AddListener(SubmitName);
                      nameTeam1.AddListener(SubmitName);
                      input1.onEndEdit = nameTeam1;

                      //второй способ
                      //or simply use the line below, 
                      //input.onEndEdit.AddListener(SubmitName);  // This also works
                      input1.onEndEdit.AddListener(SubmitName);

                      /*private void SubmitName(string arg0)
                      {
                        Debug.Log(arg0);
                     }
                      */
    /*       break;

       case 2:
           var input2 = gameObject.GetComponent<InputField>();
           var nameTeam2 = new InputField.SubmitEvent();
           //se.AddListener(SubmitName);
           nameTeam2.AddListener(SubmitName);
           input2.onEndEdit = nameTeam2;

           //второй способ
           //or simply use the line below, 
           //input.onEndEdit.AddListener(SubmitName);  // This also works
           input2.onEndEdit.AddListener(SubmitName);

           /*private void SubmitName(string arg0)
           {
             Debug.Log(arg0);
          }
           */
    /* break;
}
 ij++;
 NameOfTeam.SetActive(false);


} else
{
 BeginTeam.SetActive(true);
}

}
else if (numberTeam3)
{
if (ij < 4)
{
 switch (ij)
 {
     case 1:
         var input1 = gameObject.GetComponent<InputField>();
         var nameTeam1 = new InputField.SubmitEvent();
         //se.AddListener(SubmitName);
         nameTeam1.AddListener(SubmitName);
         input1.onEndEdit = nameTeam1;

         //второй способ
         //or simply use the line below, 
         //input.onEndEdit.AddListener(SubmitName);  // This also works
         input1.onEndEdit.AddListener(SubmitName);

         /*private void SubmitName(string arg0)
         {
           Debug.Log(arg0);
        }
         */
    /*    break;

    case 2:
        var input2 = gameObject.GetComponent<InputField>();
        var nameTeam2 = new InputField.SubmitEvent();
        //se.AddListener(SubmitName);
        nameTeam2.AddListener(SubmitName);
        input2.onEndEdit = nameTeam2;

        //второй способ
        //or simply use the line below, 
        //input.onEndEdit.AddListener(SubmitName);  // This also works
        input2.onEndEdit.AddListener(SubmitName);

        /*private void SubmitName(string arg0)
        {
          Debug.Log(arg0);
       }
        */
    /* break;
}
ij++;
NameOfTeam.SetActive(false);
}





} */

    public void BttnTwoTeam()
    {
        NumberOfTeam.SetActive(false);
        numberTeam2 = true;
        numberTeam3 = false;
        ij = 1;
        NameOfTeam1.SetActive(true);
       // NameTeam(ij);
       
       
            
      

        
    }
    // Для тестирования заполнения формы
    private void SubmitName(string arg0)
    {
        Debug.Log(arg0);
    }

    public void BttnThreeTeam()
    {
        numberTeam2 = false;
        numberTeam3 = true;
        //далее включение анимации для наименования команд
        ij = 1;
        NameOfTeam1.SetActive(true);

    }

    public void NameTeam(int nt)
    {
        /*
        if (numberTeam2)
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true); */
        //далее включение анимации для наименования команд, вызваемое 2 раза


        //запись из поля данных в переменную
        // первый способ
        // se переменная из примера
        switch(nt)
        {
            case 1:
            var input1 = gameObject.GetComponent<InputField>();
            var nameTeam1 = new InputField.SubmitEvent();
                //se.AddListener(SubmitName);
            nameTeam1.AddListener(SubmitName);
            input1.onEndEdit = nameTeam1;

         //второй способ
         //or simply use the line below, 
            //input.onEndEdit.AddListener(SubmitName);  // This also works
            input1.onEndEdit.AddListener(SubmitName);

                /*private void SubmitName(string arg0)
                {
                  Debug.Log(arg0);
               }
                */
                break;

            case 2:
                var input2 = gameObject.GetComponent<InputField>();
                var nameTeam2 = new InputField.SubmitEvent();
                //se.AddListener(SubmitName);
                nameTeam2.AddListener(SubmitName);
                input2.onEndEdit = nameTeam2;

                //второй способ
                //or simply use the line below, 
                //input.onEndEdit.AddListener(SubmitName);  // This also works
                input2.onEndEdit.AddListener(SubmitName);

                /*private void SubmitName(string arg0)
                {
                  Debug.Log(arg0);
               }
                */
                break;
            case 3:
                var input3 = gameObject.GetComponent<InputField>();
                var nameTeam3 = new InputField.SubmitEvent();
                //se.AddListener(SubmitName);
                nameTeam3.AddListener(SubmitName);
                input3.onEndEdit = nameTeam3;

                //второй способ
                //or simply use the line below, 
                //input.onEndEdit.AddListener(SubmitName);  // This also works
                input3.onEndEdit.AddListener(SubmitName);

                /*private void SubmitName(string arg0)
                {
                  Debug.Log(arg0);
               }
                */
                break;
        }
    }




}

[System.Serializable]
public class QuestionsList
{
    public string Question;
    public string[] answers = new string[3];
}