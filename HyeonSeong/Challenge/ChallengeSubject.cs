using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeSubject
{
    public delegate void ChallengeHandler(ChallengeBroker.Item item, string name,int achieve);
    public ChallengeHandler challenge_handler;
}
