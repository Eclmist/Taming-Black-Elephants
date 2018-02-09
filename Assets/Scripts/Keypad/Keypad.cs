using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Keypad : MonoBehaviour, IInteractable
{
    [SerializeField] private Text LCD_Display;
    [SerializeField] private string correctPasscode = "3587";
    [SerializeField] private UnityEvent OnCorrectPass;
    [SerializeField] private UnityEvent OnIncorrectPass;
    [SerializeField] private UnityEvent OnKeypress;
    [SerializeField] private UnityEvent OnInteract;

    private string currentPass = "";

    public void KeyPress(int number)
    {
        if (currentPass.Length >= 4)
        {
            // Play buzzer sound
            return;
        }

        currentPass += number.ToString();

        UpdateLCD();

        OnKeypress.Invoke();
    }

    public void Backspace()
    {
        currentPass = currentPass.Substring(0, currentPass.Length - 1);

        UpdateLCD();

        OnKeypress.Invoke();
    }

    public void ClearLCD()
    {
        currentPass = "";

        UpdateLCD();

        OnKeypress.Invoke();
    }

    public void Enter()
    {
        if (currentPass == correctPasscode)
        {
            OnCorrectPass.Invoke();
            Close();
        }
        else
        {
            OnIncorrectPass.Invoke();
        }
    }

    public void UpdateLCD()
    {
        string paddedDisplay = "";

        
        for (int i = 0; i < currentPass.Length; i++)
        {
            paddedDisplay += currentPass[i];

            if (i+1 != currentPass.Length)
            {
                paddedDisplay += " ";
            }
        }

        LCD_Display.text = paddedDisplay;
    }

    public void Interact()
    {
        OnInteract.Invoke();
        Time.timeScale = 0;
    }

    public void Close()
    {
        Time.timeScale = 1;
    }
}
