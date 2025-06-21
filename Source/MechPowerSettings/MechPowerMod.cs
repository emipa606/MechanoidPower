using Verse;

namespace MechPowerSettings;

internal class MechPowerMod : ModSettings
{
    internal static float Poweroutput = -5000f;

    internal static float Marketvalue = 2000f;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref Poweroutput, "powerOutput", -5000f);
        Scribe_Values.Look(ref Marketvalue, "marketValue", 2000f);
        if (Poweroutput > 0f)
        {
            Poweroutput = -Poweroutput;
        }
    }
}