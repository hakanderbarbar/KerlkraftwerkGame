using System;

namespace KerlkraftwerkGame.Managers;

public class GameEventHandler
{
    // Event für die Änderung der Map
    public event Action<string> MapChanged;

    // Methode zum Auslösen des MapChanged-Events
    public void ChangeMap(string newMap)
    {
        this.OnMapChanged(newMap);
    }

    // Geschützte Methode zum Auslösen des Events
    protected virtual void OnMapChanged(string newMap)
    {
        this.MapChanged?.Invoke(newMap);
    }
}