using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorTaskPanel : TaskPanel {
    public const int ERASE = 10;
    public const int MAX_LENGTH = 4;

    public Image TimerImage;
    public Button[] Buttons;
    public Text QuestionText;
    public Text AnswerText;

    public CalculatorTask task;

    public TaskSO t;

	public AudioClip inputSound;
	public AudioClip wrongSound;

    // Use this for initialization
    public override void Start () {
        task = (CalculatorTask)panelController.currentTask;

        QuestionText.text = task.questionText;
        AnswerText.text = "";

        for (int i = 0; i < Buttons.Length; i++) {
            int j = i;
            Button b = Buttons[i];
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(() => this.PressButton(j));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!task.Solved) {
            TimerImage.fillAmount = task.TimeRemaining / task.TimeGiven;
        }
    }
    public override void Display() {

    }

    public void PressButton(int i) {
        if (task.Solved) return;

		HUDCanvasManager.instance.audioSource.PlayOneShot(inputSound);

        if (i == ERASE) {
            if (AnswerText.text.Length == 0) return;
            AnswerText.text = AnswerText.text.Substring(0, AnswerText.text.Length - 1);
            return;
        }

        if (AnswerText.text.Length == MAX_LENGTH) return;

        AnswerText.text += i;
        if (int.Parse(AnswerText.text) == task.correctAnswer) {
            AnswerText.color = Color.green;
            task.Solved = true;
            task.OnTaskSuccess();

        }
    }
}
