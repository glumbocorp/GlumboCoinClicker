using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeJRappUsers : InteractorBase
{
    [SerializeField] Main main;
    [SerializeField] float relativeUsers;
    public override void Trigger()
    {
        main.changeTotalUsers(relativeUsers);
    }
}
