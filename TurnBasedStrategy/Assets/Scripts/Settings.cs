using UnityEngine;

public static class Settings
{
    public const int actionPointMax = 2;

    public const float minFollowYOffset = 4f;
    public const float maxFollowYOffset = 12f;

    public static int isWalking = Animator.StringToHash("IsWalking");

    public const string moveActionName = "MOVE";
    public const string spinActionName = "SPIN";
}