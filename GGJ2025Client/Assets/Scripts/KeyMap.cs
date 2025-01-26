using System;
using UnityEngine;

public static class KeyMap
{
    public const KeyCode FullscreenKey = KeyCode.F;
    public const KeyCode BubbleSpawnKey = KeyCode.B;
    public const KeyCode DebugPopKey = KeyCode.X;

    public record ArmKeys(KeyCode ArmKey, KeyCode WristKey, KeyCode FingerAKey, KeyCode FingerBKey);

    private static readonly ArmKeys LeftArmKeys = new(ArmKey: KeyCode.Q, WristKey: KeyCode.W, FingerAKey: KeyCode.E, FingerBKey: KeyCode.R);
    private static readonly ArmKeys RightArmKeys = new(ArmKey: KeyCode.P, WristKey: KeyCode.O, FingerAKey: KeyCode.I, FingerBKey: KeyCode.U);

    public static ArmKeys GetKeysForArm(ArmController.ArmType armType)
    {
        return armType switch
        {
            ArmController.ArmType.LeftArm => LeftArmKeys,
            ArmController.ArmType.RightArm => RightArmKeys,
            _ => throw new ArgumentException()
        };
    }
}