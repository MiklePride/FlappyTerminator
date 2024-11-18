using System;

public class StartScreen : Window
{
    public event Action StartButtonClicked;

    protected override void OnButtonClick()
    {
        StartButtonClicked?.Invoke();
    }
}