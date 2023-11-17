using System;
public interface IFStateEvent
{
    void SetMainGameEvent(Action method);
    void SetTitleMenuEvent(Action method);
    void SetInGameMenuEvent(Action method);
    void SetResultMenuEvent(Action method);
    void SetOptionEvent(Action method);
}
