using System;

public interface IMenuLogic
{
    void Show();
    void Hide();

    IDisposable SubscribeOnShow(Action handler);

    IDisposable SubscribeOnReturn(Action handler);
}
