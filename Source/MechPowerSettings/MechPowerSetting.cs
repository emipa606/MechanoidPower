using RimWorld;
using UnityEngine;
using Verse;

namespace MechPowerSettings
{
    // Token: 0x02000003 RID: 3
    internal class MechPowerSetting : Mod
    {
        // Token: 0x06000004 RID: 4 RVA: 0x000020A5 File Offset: 0x000002A5
        public MechPowerSetting(ModContentPack mcp) : base(mcp)
        {
            LongEventHandler.ExecuteWhenFinished(GetSettings);
            LongEventHandler.ExecuteWhenFinished(PushDatabase);
        }

        // Token: 0x06000005 RID: 5 RVA: 0x000020D4 File Offset: 0x000002D4
        public void GetSettings()
        {
            base.GetSettings<MechPowerMod>();
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000020E0 File Offset: 0x000002E0
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

            DefDatabase<ThingDef>.GetNamed("MPC_MechanoidPowerCell").GetCompProperties<CompProperties_Power>()
                .basePowerConsumption = -MechPowerMod.poweroutput;
        }

        // Token: 0x06000007 RID: 7 RVA: 0x0000214E File Offset: 0x0000034E
        private void PushDatabase()
        {
            MechPowerMod.database = DefDatabase<ThingDef>.AllDefsListForReading;
            WriteSettings();
        }

        // Token: 0x06000008 RID: 8 RVA: 0x00002164 File Offset: 0x00000364
        public override string SettingsCategory()
        {
            return Static.MechPower;
        }

        // Token: 0x06000009 RID: 9 RVA: 0x0000217C File Offset: 0x0000037C
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
            Widgets.Label(rect5, "<b>Power Cell</b> market value");
            Widgets.Label(rect6,
                $"<b>{MechPowerMod.marketvalue:00}</b> <color=#ababab>(Influence on difficulty)</color>");
            if (Widgets.ButtonText(new Rect(rect4.xMin, rect4.y, rect4.height, rect4.height), "-", true, false))
            {
                if (MechPowerMod.marketvalue >= 500f)
                {
                    MechPowerMod.marketvalue -= 50f;
                }
            }

            MechPowerMod.marketvalue = Widgets.HorizontalSlider(
                new Rect(rect4.xMin + rect4.height + 10f, rect4.y, rect4.width - ((rect4.height * 2f) + 20f),
                    rect4.height), MechPowerMod.marketvalue, 500f, 4000f, true);
            if (Widgets.ButtonText(new Rect(rect4.xMax - rect4.height, rect4.y, rect4.height, rect4.height),
                "+", true, false))
            {
                if (MechPowerMod.marketvalue < 4000f)
                {
                    MechPowerMod.marketvalue += 50f;
                }
            }

            listing_Standard.Gap(10f);
            var rect7 = listing_Standard.GetRect(Text.LineHeight);
            var rect8 = rect7.LeftHalf().Rounded();
            var rect9 = rect7.RightHalf().Rounded();
            var rect10 = rect8.LeftHalf().Rounded();
            var rect11 = rect8.RightHalf().Rounded();
            Widgets.Label(rect10, "<b>Power Cell</b> power output (W)");
            Widgets.Label(rect11,
                $"<b>{MechPowerMod.poweroutput:00}W</b> <color=#ababab>(recommended: 5000W)</color>");
            if (Widgets.ButtonText(new Rect(rect9.xMin, rect9.y, rect9.height, rect9.height), "-", true, false))
            {
                if (MechPowerMod.poweroutput >= 2000f)
                {
                    MechPowerMod.poweroutput -= 500f;
                }
            }

            MechPowerMod.poweroutput = Widgets.HorizontalSlider(
                new Rect(rect9.xMin + rect9.height + 10f, rect9.y, rect9.width - ((rect9.height * 2f) + 20f),
                    rect9.height), MechPowerMod.poweroutput, 2000f, 20000f, true);
            if (!Widgets.ButtonText(new Rect(rect9.xMax - rect9.height, rect9.y, rect9.height, rect9.height),
                "+", true, false))
            {
                return;
            }

            if (MechPowerMod.poweroutput < 20000f)
            {
                MechPowerMod.poweroutput += 500f;
            }
        }
    }
}