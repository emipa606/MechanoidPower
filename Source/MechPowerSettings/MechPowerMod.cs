using Verse;

namespace MechPowerSettings;

internal class MechPowerMod : ModSettings
{
    internal static float poweroutput = -5000f;

    internal static float marketvalue = 2000f;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref poweroutput, "powerOutput", -5000f);
        Scribe_Values.Look(ref marketvalue, "marketValue", 2000f);
        if (poweroutput > 0f)
        {
            poweroutput = -poweroutput;
        }
    }
}