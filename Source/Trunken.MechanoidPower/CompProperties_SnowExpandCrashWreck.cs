using Verse;

namespace Trunken_MechanoidPower;

public class CompProperties_SnowExpandCrashWreck : CompProperties
{
    public readonly float addAmount = 0.12f;

    public readonly int expandInterval = 500;

    public readonly float maxRadius = 55f;

    public readonly float maxTemperate = 30f;

    public CompProperties_SnowExpandCrashWreck()
    {
        compClass = typeof(ShipWreck_CompSnowExpand);
    }
}