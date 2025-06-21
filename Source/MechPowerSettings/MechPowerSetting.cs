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
        LongEventHandler.ExecuteWhenFinished(getSettings);
        LongEventHandler.ExecuteWhenFinished(pushDatabase);
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(mcp.ModMetaData);
    }

    private void getSettings()
    {
        GetSettings<MechPowerMod>();
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
            first.value = MechPowerMod.Marketvalue;
        }

        var powerCellCompPropertiesPower = DefDatabase<ThingDef>.GetNamed("MPC_MechanoidPowerCell")
            .GetCompProperties<CompProperties_Power>();
        powerCellCompPropertiesPower.basePowerConsumption = MechPowerMod.Poweroutput;
    }

    private void pushDatabase()
    {
        WriteSettings();
    }

    public override string SettingsCategory()
    {
        return Static.MechPower;
    }

    public override void DoSettingsWindowContents(Rect rect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        listingStandard.Gap(10f);
        var rect2 = listingStandard.GetRect(Text.LineHeight);
        var rect3 = rect2.LeftHalf().Rounded();
        var rect4 = rect2.RightHalf().Rounded();
        var rect5 = rect3.LeftHalf().Rounded();
        var rect6 = rect3.RightHalf().Rounded();
        Widgets.Label(rect5, "MePo.MarketValue".Translate());
        Widgets.Label(rect6, "MePo.Difficulty".Translate(MechPowerMod.Marketvalue.ToStringMoney()));
        if (Widgets.ButtonText(new Rect(rect4.xMin, rect4.y, rect4.height, rect4.height), "-", true, false))
        {
            MechPowerMod.Marketvalue = Mathf.Max(500f, MechPowerMod.Marketvalue - 50f);
        }

        MechPowerMod.Marketvalue = Widgets.HorizontalSlider(
            new Rect(rect4.xMin + rect4.height + 10f, rect4.y, rect4.width - ((rect4.height * 2f) + 20f),
                rect4.height), MechPowerMod.Marketvalue, 500f, 4000f, true);
        if (Widgets.ButtonText(new Rect(rect4.xMax - rect4.height, rect4.y, rect4.height, rect4.height),
                "+", true, false))
        {
            MechPowerMod.Marketvalue = Mathf.Min(4000f, MechPowerMod.Marketvalue + 50f);
        }

        listingStandard.Gap(10f);
        var rect7 = listingStandard.GetRect(Text.LineHeight);
        var rect8 = rect7.LeftHalf().Rounded();
        var rect9 = rect7.RightHalf().Rounded();
        var rect10 = rect8.LeftHalf().Rounded();
        var rect11 = rect8.RightHalf().Rounded();
        Widgets.Label(rect10, "MePo.PowerOutput".Translate());
        Widgets.Label(rect11, "MePo.Recommended".Translate(-MechPowerMod.Poweroutput));
        var tempPower = -MechPowerMod.Poweroutput;
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

        MechPowerMod.Poweroutput = -tempPower;

        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("MePo.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}