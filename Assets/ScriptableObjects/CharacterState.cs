using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    walk,
    attack,
    interact,
    stagger,
    idle,
};

[CreateAssetMenu]
public class CharacterState : ScriptableObject {
    public State category;
}
