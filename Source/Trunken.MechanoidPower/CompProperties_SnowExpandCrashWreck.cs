using Verse;

namespace Trunken_MechanoidPower;

public class CompProperties_SnowExpandCrashWreck : CompProperties
{
    public float addAmount = 0.12f;

    public int expandInterval = 500;

    public float maxRadius = 55f;

    public float maxTemperate = 30f;

    public CompProperties_SnowExpandCrashWreck()
    {
        compClass = typeof(ShipWreck_CompSnowExpand);
    }
}