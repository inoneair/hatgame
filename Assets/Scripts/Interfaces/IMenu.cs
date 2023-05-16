using System;

public interface IMenuLogic
{
    void Show();
    void Hide();

    void SubscribeOnReturn(Action handler);
}
