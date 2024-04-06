using Mlie;
using RimWorld;
using UnityEngine;
using Verse;

namespace MechPowerSettings;

internal class MechPowerSetting : Mod
{
    private static string currentVersion;

    public MechPowerSetting(ModContentPack mcp) : base(mcp)
    {
        LongEventHandler.ExecuteWhenFinished(GetSettings);
        LongEventHandler.ExecuteWhenFinished(PushDatabase);
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(mcp.ModMetaData);
    }

    public void GetSettings()
    {
        base.GetSettings<MechPowerMod>();
    }

    public override void WriteSettings()
    {
        base.WriteSettings();
        StatModifier first = null;
        foreach (var x in DefDatabase<ThingDef>.GetNamed("MPC_MechanoidPCell").statBases)
        {
            if (x.stat != StatDefOf.MarketValue)
            {
                continue;
            }

            first = x;
            break;
        }

        if (first != null)
        {
            first.value = MechPowerMod.marketvalue;
        }

        var powerCellCompPropertiesPower = DefDatabase<ThingDef>.GetNamed("MPC_MechanoidPowerCell")
            .GetCompProperties<CompProperties_Power>();
        powerCellCompPropertiesPower.basePowerConsumption = MechPowerMod.poweroutput;
    }

    private void PushDatabase()
    {
        WriteSettings();
    }

    public override string SettingsCategory()
    {
        return Static.MechPower;
    }

    public override void DoSettingsWindowContents(Rect rect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(rect);
        listing_Standard.Gap(10f);
        var rect2 = listing_Standard.GetRect(Text.LineHeight);
        var rect3 = rect2.LeftHalf().Rounded();
        var rect4 = rect2.RightHalf().Rounded();
        var rect5 = rect3.LeftHalf().Rounded();
        var rect6 = rect3.RightHalf().Rounded();
        Widgets.Label(rect5, "MePo.MarketValue".Translate());
        Widgets.Label(rect6, "MePo.Difficulty".Translate(MechPowerMod.marketvalue.ToStringMoney()));
        if (Widgets.ButtonText(new Rect(rect4.xMin, rect4.y, rect4.height, rect4.height), "-", true, false))
        {
            MechPowerMod.marketvalue = Mathf.Max(500f, MechPowerMod.marketvalue - 50f);
        }

        MechPowerMod.marketvalue = Widgets.HorizontalSlider(
            new Rect(rect4.xMin + rect4.height + 10f, rect4.y, rect4.width - ((rect4.height * 2f) + 20f),
                rect4.height), MechPowerMod.marketvalue, 500f, 4000f, true);
        if (Widgets.ButtonText(new Rect(rect4.xMax - rect4.height, rect4.y, rect4.height, rect4.height),
                "+", true, false))
        {
            MechPowerMod.marketvalue = Mathf.Min(4000f, MechPowerMod.marketvalue + 50f);
        }

        listing_Standard.Gap(10f);
        var rect7 = listing_Standard.GetRect(Text.LineHeight);
        var rect8 = rect7.LeftHalf().Rounded();
        var rect9 = rect7.RightHalf().Rounded();
        var rect10 = rect8.LeftHalf().Rounded();
        var rect11 = rect8.RightHalf().Rounded();
        Widgets.Label(rect10, "MePo.PowerOutput".Translate());
        Widgets.Label(rect11, "MePo.Recommended".Translate(-MechPowerMod.poweroutput));
        var tempPower = -MechPowerMod.poweroutput;
        if (Widgets.ButtonText(new Rect(rect9.xMin, rect9.y, rect9.height, rect9.height), "-", true, false))
        {
            tempPower = Mathf.Max(500f, tempPower - 500f);
        }

        tempPower = Widgets.HorizontalSlider(
            new Rect(rect9.xMin + rect9.height + 10f, rect9.y, rect9.width - ((rect9.height * 2f) + 20f),
                rect9.height), tempPower, 2000f, 20000f, true);

        if (Widgets.ButtonText(new Rect(rect9.xMax - rect9.height, rect9.y, rect9.height, rect9.height),
                "+", true, false))
        {
            tempPower = Mathf.Min(20000f, tempPower + 500f);
        }

        MechPowerMod.poweroutput = -tempPower;

        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("MePo.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
    }
}