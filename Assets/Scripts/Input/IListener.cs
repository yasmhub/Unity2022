using UnityEngine;

/*
public abstract class InputReceiver : MonoBehaviour {

    public abstract void InputUpdate(InputListener InputListener);
}
*/

public interface InputReceiver {

    void InputUpdate(InputListener InputListener);
}