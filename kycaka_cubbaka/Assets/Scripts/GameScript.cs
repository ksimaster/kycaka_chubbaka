//Main script for Quiz game by Android | Helper (RU). || Основной скрипт игры викторины от Android | Helper (RU).
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
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
    public int[] randQList; 
    public Image headPanel;
    public GameObject HeadAllPanel;
    public GameObject exitPanel;
    public GameObject RulsPanel;
    public GameObject open_audio;
    public GameObject close_audio;
    public GameObject Audio_on_off;
    public GameObject Audio_on_off_1;
    public GameObject RulsPanel_2;
    public GameObject PausePanelBattn;
    public GameObject PausePanel;
    public GameObject finalText;
    public GameObject ExitPanelMainMenu;
    public GameObject ExitPanelExit;
    public GameObject NumberOfTeam;
    public GameObject PauseBttn_on;
    public GameObject NameOfTeam1;
    public GameObject NameOfTeam2;
    public GameObject NameOfTeam3;
    public GameObject BeginTeam;
    public GameObject PlayBttn;
    public GameObject RulsBttn;
    public GameObject ExitBttn;
    public GameObject PauseOnBttn;
    public GameObject ComeBackGameBttn;
    public GameObject AudioMan;
    //Объявление листа статистики
    public GameObject StatList;
    public Text StatNameOne;
    public Text ScoreTeamOne;
    public Text StatNameTwo;
    public Text ScoreTeamTwo;
    public Text StatNameThree;
    public Text ScoreTeamThree;
    public InputField InputNOT1;
    public InputField InputNOT2;
    public InputField InputNOT3;
    public Text time;
    public Text recordText;
    public Text scoreText;
    public Text BeginTeamText;
    public int T;
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
    //переменные, определяющие воздействие паузы на соответствующие панели
    public bool StatOnOff, RulsOnOff, NumberOfTeamOnOff, NameOfTeam1OnOff, NameOfTeam2OnOff, NameOfTeam3OnOff, BeginTeamOnOff;
  //  public bool HeadAllPanelOnOff;
    public string nameT1, nameT2, nameT3;

    private bool trueColor, falseColor,defaultColor;
    private int randQ;
    private List<object> qList;
    private QuestionsList crntQ;

    private void Start()
    {
        qList = new List<object>(Questions);
 
        randQList = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,-1,-1,-1,-1,-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, - 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
        Debug.Log(randQList.Length);
        StatOnOff = true;
        RulsOnOff = true;
        NumberOfTeamOnOff = true;
        NameOfTeam1OnOff = true;
        NameOfTeam2OnOff = true;
        NameOfTeam3OnOff = true;
        BeginTeamOnOff = true;
        //HeadAllPanelOnOff = true;

}

    void Update ()
    {
        scoreText.text = string.Format("Ваш счёт: {0:0}", score);
        scoreForRecord = Mathf.Lerp(scoreForRecord, PlayerPrefs.GetInt("score"), 6 * Time.deltaTime);
        recordText.text = string.Format("Ваш рекорд: {0:0}", scoreForRecord);
        if (defaultColor) headPanel.color = Color.Lerp(headPanel.color, defaultCC, 8 * Time.deltaTime);
        else if (trueColor) headPanel.color = Color.Lerp(headPanel.color, trueCC, 8 * Time.deltaTime);
        else if (falseColor) headPanel.color = Color.Lerp(headPanel.color, falseCC, 8 * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Escape) && !PausePanel.activeSelf && !ExitPanelExit.activeSelf && !ExitPanelMainMenu.activeSelf)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            //Отключение тикания
            if (AudioMan.activeSelf) AudioMan.SetActive(false);
            //Отключаем панель статов, если она включена
            if (StatList.activeSelf)
            { 
                StatList.SetActive(false);
                StatOnOff = false;
            }
            //Отключаем панель правил, если она включена
            if (RulsPanel.activeSelf)
            {
                RulsPanel.SetActive(false);
                RulsOnOff = false;
            }
            //Отключаем панель выбора количества команд, если она включена
            if (NumberOfTeam.activeSelf)
            {
                NumberOfTeam.SetActive(false);
                NumberOfTeamOnOff = false;
            }

            //Отключаем панель ввода первой команды, если она включена
            if (NameOfTeam1.activeSelf)
            {
                NameOfTeam1.SetActive(false);
                NameOfTeam1OnOff = false;
            }

            //Отключаем панель ввода второй команды, если она включена
            if (NameOfTeam2.activeSelf)
            {
                NameOfTeam2.SetActive(false);
                NameOfTeam2OnOff = false;
            }

            //Отключаем панель ввода третьей команды, если она включена
            if (NameOfTeam3.activeSelf)
            {
                NameOfTeam3.SetActive(false);
                NameOfTeam3OnOff = false;
            }
            
            //Отключаем панель начинает команда, если она включена
            if (BeginTeam.activeSelf)
            {
                BeginTeam.SetActive(false);
                BeginTeamOnOff = false;
            }
/*
            //Отключаем панель основной игры (раунда), если она включена
            if (HeadAllPanel.activeSelf)
            {
                HeadAllPanel.SetActive(false);
                HeadAllPanelOnOff = false;
            }
            */
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && PausePanel.activeSelf && !ExitPanelExit.activeSelf && !ExitPanelMainMenu.activeSelf)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            //включение тикания
            if (!AudioMan.activeSelf && open_audio.activeSelf && T>0 && T<7) AudioMan.SetActive(true);
            //включаем панель статов, если пауза её отключала
            if (!StatOnOff&&!StatList.activeSelf)
            { 
                StatList.SetActive(true);
                StatOnOff = true;
            }
            //включаем панель правил, если пауза её отключала
            if (!RulsOnOff && !RulsPanel.activeSelf)
            { 
                RulsPanel.SetActive(true);
                RulsOnOff = true;
            }
            //включаем панель выбора количества команд, если пауза её отключала
            if (!NumberOfTeamOnOff && !NumberOfTeam.activeSelf)
            { 
                NumberOfTeam.SetActive(true);
                NumberOfTeamOnOff = true;
            }
            //включаем панель ввода первой команды, если пауза её отключала
            if (!NameOfTeam1OnOff && !NameOfTeam1.activeSelf)
            { 
                NameOfTeam1.SetActive(true);
                NameOfTeam1OnOff = true;
            }
            //включаем панель ввода второй команды, если пауза её отключала
            if (!NameOfTeam2OnOff && !NameOfTeam2.activeSelf)
            { 
                NameOfTeam2.SetActive(true);
                NameOfTeam2OnOff = true;
            }
            //включаем панель ввода третьей команды, если пауза её отключала
            if (!NameOfTeam3OnOff && !NameOfTeam3.activeSelf)
            { 
                NameOfTeam3.SetActive(true);
                NameOfTeam3OnOff = true;
            }
            //включаем панель начинает команда, если пауза её отключала
            if (!BeginTeamOnOff && !BeginTeam.activeSelf)
            {
                BeginTeam.SetActive(true);
                BeginTeamOnOff = true;
            }
          /* 
            //включаем панель основной игры (раунда), если пауза её отключала
            if (!HeadAllPanelOnOff && !HeadAllPanel.activeSelf)
            {
                HeadAllPanel.SetActive(true);
                HeadAllPanelOnOff = true;
            }
            */
        }
    }

    public void playBttn()
    {
        T = publicTimeCount;
        StartCoroutine(OneSecond());
        BeginTeam.SetActive(false);
       // qList = new List<object>(Questions); // если требуется генерить лист каждый раз
        generateQuestion();
        StartCoroutine(timer());
        timeCount = publicTimeCount;
        Debug.Log(T);
       // headPanel.GetComponent<Animation>().Play("HeadAnim"); // убрал, так как лишняя анимация
        finalText.SetActive(false);
    }
    void generateQuestion()
    {
        

        if (qList.Count > 0)
        {
            if (scoreText.gameObject.activeSelf) scoreText.GetComponent<Animation>().Play("Bubble_Close_3");
            // randQ = Random.Range(0, qList.Count); //в случае викторины от 2х и выше
            //randQ = 246; // 0 - отсутствует перемешка
            //рандомизация вопроса
           // Debug.Log("Первый из randQList" + randQList[1]);
            randQ = Random.Range(0, qList.Count);
            Debug.Log("После новой рандомизации: " + randQ);
            if (randQList[0] == -1) randQList[0] = randQ;
            // проверка был ли такой randQ сделано через for и if, можно через switch
            for (int jj=0; jj<randQList.Length; jj++)
            {
                Debug.Log(randQList[jj]);
                if (randQList[jj]==randQ)
                {
                    randQ = Random.Range(0, qList.Count);
                    // добавляем в массив новый randQ
                    for (int ii = 1; ii < randQList.Length; ii++)
                    {
                        if (randQList[ii] == -1)
                        { 
                            randQList[ii] = randQ;
                            goto link1;
                        }
                    }
                    


                }
                
            }
           
            link1:
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
            //StartCoroutine(timer()); // было в викторине
            //  timeCount = publicTimeCount; // было в викторине
            currentQ++;
        }
        else StartCoroutine(final());
    }
    public void answerBttn(int index)
    {
        answerClicked = true;
        timeCount = T;
        StartCoroutine(trueOrFalse(answersText[index].text == crntQ.answers[0]));
    }
    IEnumerator OneSecond()
    {
        yield return new WaitForSeconds(1);

    }
    IEnumerator final()
    {
        // finalText.SetActive(true);

        //ВНИМАНИЕ! Здесь должна быть выскакивающая панелька о том, что игра закончена
        //перенесено из тайм аута
        falseColor = true;
        //Отключение тикания
        if (AudioMan.activeSelf) AudioMan.SetActive(false);
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
        // headPanel.GetComponent<Animation>().Play("HeadAnimOut");
        //headPanel.GetComponent<Animation>().Play("HeadAnimOut");
        answerBttns[0].GetComponent<Animation>().Play("Bubble_Close_2");
        scoreText.GetComponent<Animation>().Play("Bubble_Close_3");
        finalText.GetComponent<Animation>().Play("Bubble_Close_3");
        //Для того чтобы отключилась панелька c истекшим временем пишем 2 строчки
        if (answersIcons[2].activeSelf) answersIcons[2].SetActive(false);
        else answersIcons[2].GetComponent<Animation>().Play("Bubble_Close_3");
        //Для того чтобы отключилась панель с очками пишем 2 строчки
        if (scoreText.gameObject.activeSelf) scoreText.gameObject.SetActive(false);
        else scoreText.GetComponent<Animation>().Play("Bubble_Close_3");
        // 

        yield return new WaitForSeconds(2);
        BttnGoStat();
        ComeBackGameBttn.SetActive(false);
        PauseOnBttn.SetActive(false);
        
        
        // trueColor = false;
        // defaultColor = true;
        //headPanel.GetComponent<Animation>().Play("HeadAnimOut");
        // scoreText.GetComponent<Animation>().Play("Bubble_Close_3");
        // finalText.GetComponent<Animation>().Play("Bubble_Close_3");
        // if (score > PlayerPrefs.GetInt("score")) PlayerPrefs.SetInt("score", score); //Обновляет рекорд, у нас как дополнение при максимальной серии угадываний игроком
    }
    IEnumerator timer()
    {
        timeCount = T;
        
        answerClicked = false;
        if (!time.gameObject.activeSelf) time.gameObject.SetActive(true);
        else time.GetComponent<Animation>().Play("Bubble_Open_3");
        while (timeCount > -1)
        {
            
            if(answerClicked) yield return new WaitForSeconds(1);
            
            if(!answerClicked)
            {
            
            time.text = timeCount.ToString();
            timeCount--;
             T = timeCount;
                //включение тикания
                if(T==7 && open_audio.activeSelf)
                {
                    AudioMan.SetActive(true);
                }
             yield return new WaitForSeconds(1);
            }
            else
            {
                yield break;
            }

                
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
        //foreach (Button t in answerBttns) t.interactable = false;
        if (timeCount<0) StartCoroutine(timeOut());
        
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
        yield return new WaitForSeconds(0.5f);
        PauseOnBttn.SetActive(true);
        // yield return StartCoroutine(timer()); //Данная строчка для запуска таймера в режиме викторины связанная с перезапуском таймера в случае правильного ответа
    }
    IEnumerator timeOut()
    {
        //foreach (Button t in answerBttns) t.GetComponent<Animation>().Play("Bubble_Close_2");
        
        falseColor = true;
        //отключение тикания
        if (AudioMan.activeSelf) AudioMan.SetActive(false);
        answerBttns[0].interactable = false;
        //answerBttns[0].GetComponent<Animation>().Play("Bubble_Close_2");
        yield return new WaitForSeconds(0.7f);
        if (!answersIcons[2].activeSelf) answersIcons[2].SetActive(true);
        else answersIcons[2].GetComponent<Animation>().Play("Bubble_Open_3");
        yield return new WaitForSeconds(0.7f);
        questionText.GetComponent<Animation>().Play("Bubble_Close_1");
        yield return new WaitForSeconds(0.7f);
        if (!scoreText.gameObject.activeSelf) scoreText.gameObject.SetActive(true);
        else scoreText.GetComponent<Animation>().Play("Bubble_Open_3");
        yield return new WaitForSeconds(0.2f);
        answersIcons[2].GetComponent<Animation>().Play("Bubble_Close_3");
        time.GetComponent<Animation>().Play("Bubble_Close_3");
       // yield return new WaitForSeconds(0.5f);
        falseColor = false;
        defaultColor = true;
        // headPanel.GetComponent<Animation>().Play("HeadAnimOut");
        //headPanel.GetComponent<Animation>().Play("HeadAnimOut");
        answerBttns[0].GetComponent<Animation>().Play("Bubble_Close_2");
        scoreText.GetComponent<Animation>().Play("Bubble_Close_3");
        finalText.GetComponent<Animation>().Play("Bubble_Close_3");
        PauseOnBttn.SetActive(false);
        
        //Для того чтобы отключилась панелька c истекшим временем пишем 2 строчки
        if (answersIcons[2].activeSelf) answersIcons[2].SetActive(false);
        else answersIcons[2].GetComponent<Animation>().Play("Bubble_Close_3");
        //Для того чтобы отключилась панель с очками пишем 2 строчки
        if (scoreText.gameObject.activeSelf) scoreText.gameObject.SetActive(false);
        else scoreText.GetComponent<Animation>().Play("Bubble_Close_3");
        //далее смена команд в зависимости от их количества
        ij++;
        if (numberTeam2)
        {
            if (ij > 2) ij = 1;
        }else
        {
            if (ij > 3) ij = 1;
        }
        Debug.Log(score);
        Debug.Log(scoreTeamOne);
        Debug.Log(scoreTeamTwo);
        Debug.Log(scoreTeamThree);
        Debug.Log(T);
        BeginTeamName();
        BeginTeam.SetActive(true);

        // if (score > PlayerPrefs.GetInt("score")) PlayerPrefs.SetInt("score", score); //Обновление рекорда, дубль для качественной работы, пока не требуется 
    }
    IEnumerator trueOrFalse(bool check)
    {
        defaultColor = false;
       // foreach (Button t in answerBttns) t.interactable = false; // опять foreach заменяем на обычную, в отличие от викторины на 3 ответа
        answerBttns[0].interactable = false;
        // по умолчанию следующее значение в скобках было равно 1
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
            StartCoroutine(timer());
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

    public void PauseOnGameBttn()
    {
        if (!PausePanel.activeSelf)
        {
            if (AudioMan.activeSelf) AudioMan.SetActive(false);
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        

    }

    public void PauseOnGameBttn_1()
    {
        if (!PausePanelBattn.activeSelf)
        {
            if (AudioMan.activeSelf) AudioMan.SetActive(false);
            PausePanelBattn.SetActive(true);
            PauseBttn_on.SetActive(false);
            Time.timeScale = 0;
        }


    }

    public void PauseOnGameBttn_2()
    {
  
            PausePanelBattn.SetActive(false);
            PauseBttn_on.SetActive(true);
            Time.timeScale = 1;
            if (!AudioMan.activeSelf && open_audio.activeSelf && T > 0 && T < 7) AudioMan.SetActive(true);


    }



    public void ShowRulstBttn()
    {

        RulsBttn.SetActive(false);  
        PlayBttn.SetActive(false);
        ExitBttn.SetActive(false);
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


    public void ExitMainMenu_1()

    {

        PausePanel.SetActive(false);
        ExitPanelMainMenu.SetActive(true);

    }
    public void ExitMainMenu_2()

    {

        PausePanel.SetActive(true);
        ExitPanelMainMenu.SetActive(false);

    }



    public void ExitMainMenu_3()

    {

        PausePanel.SetActive(false);
        ExitPanelExit.SetActive(true);

    }
    public void ExitMainMenu_4()

    {

        PausePanel.SetActive(true);
        ExitPanelExit.SetActive(false);

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
        RulsBttn.SetActive(true);
        PlayBttn.SetActive(true);
        ExitBttn.SetActive(true);


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



    public void ReturnePausePanel()
    {
        PausePanel.SetActive(true);
        
    }


    public void ReturneMainMenu()
       

    {
        //Application.LoadLevel("Game");
        // Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;  // e,bhftn jib,re yfkj;tybq
        // NumberOfTeam.SetActive(false);
        //  PausePanel.SetActive(false);
        //  headPanel.GetComponent<Animation>().Play("HeadAnimOut");
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
            PausePanel.SetActive(false);
            Time.timeScale = 1; ;
            if (!AudioMan.activeSelf && open_audio.activeSelf && T > 0 && T < 7) AudioMan.SetActive(true);
        }
    }




    public void ShowExitBttn()
    {


        RulsBttn.SetActive(false);
        PlayBttn.SetActive(false);
        ExitBttn.SetActive(false);
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
        RulsBttn.SetActive(true);
        PlayBttn.SetActive(true);
        ExitBttn.SetActive(true);
    }
    //Выбор команды
    public void ChooseNumberOfTeam()
    {
        NumberOfTeam.SetActive(true);
        headPanel.GetComponent<Animation>().Play("HeadAnim");
        RulsBttn.SetActive(false);
        PlayBttn.SetActive(false);
        ExitBttn.SetActive(false);


    }

    // Начинается раздел статистики, StatList
    public void StatTable()
    {
        StatList.SetActive(true);
        StatNameOne.text = nameT1;
        ScoreTeamOne.text = scoreTeamOne.ToString();
        StatNameTwo.text = nameT2;
        ScoreTeamTwo.text = scoreTeamTwo.ToString();
        if (numberTeam2)
        {

            StatNameThree.text = "";
            ScoreTeamThree.text = "";
        }else
        {
            StatNameThree.text = nameT3;
            ScoreTeamThree.text = scoreTeamThree.ToString();

        }

    }

    public void BttnGoStat()
    {
            
        BeginTeam.SetActive(false);
        StatList.SetActive(true);
        StatTable();
       
    }

    public void BttnGoBeginTeam()
    {
        StatList.SetActive(false);
        BeginTeam.SetActive(true);
        
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
        BeginTeamName();
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
        NumberOfTeam.SetActive(false);
        numberTeam2 = false;
        numberTeam3 = true;
        //далее включение панели для наименования команд
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