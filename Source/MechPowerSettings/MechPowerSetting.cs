using System;
using System.Linq;
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
			LongEventHandler.ExecuteWhenFinished(new Action(this.GetSettings));
			LongEventHandler.ExecuteWhenFinished(new Action(this.PushDatabase));
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
			DefDatabase<ThingDef>.GetNamed("MPC_MechanoidPCell", true).statBases.FirstOrDefault((StatModifier x) => x.stat == StatDefOf.MarketValue).value = MechPowerMod.marketvalue;
			DefDatabase<ThingDef>.GetNamed("MPC_MechanoidPowerCell", true).GetCompProperties<CompProperties_Power>().basePowerConsumption = -MechPowerMod.poweroutput;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000214E File Offset: 0x0000034E
		private void PushDatabase()
		{
			MechPowerMod.database = DefDatabase<ThingDef>.AllDefsListForReading;
			this.WriteSettings();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002164 File Offset: 0x00000364
		public override string SettingsCategory()
		{
			return Static.MechPower;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000217C File Offset: 0x0000037C
		public override void DoSettingsWindowContents(Rect rect)
		{
			Listing_Standard listing_Standard = new Listing_Standard();
			listing_Standard.Begin(rect);
			listing_Standard.Gap(10f);
			Rect rect2 = listing_Standard.GetRect(Text.LineHeight);
			Rect rect3 = GenUI.Rounded(GenUI.LeftHalf(rect2));
			Rect rect4 = GenUI.Rounded(GenUI.RightHalf(rect2));
			Rect rect5 = GenUI.Rounded(GenUI.LeftHalf(rect3));
			Rect rect6 = GenUI.Rounded(GenUI.RightHalf(rect3));
			Widgets.Label(rect5, "<b>Power Cell</b> market value");
			Widgets.Label(rect6, string.Format("<b>{0:00}</b> <color=#ababab>(Influence on difficulty)</color>", MechPowerMod.marketvalue).ToString());
			bool flag = Widgets.ButtonText(new Rect(rect4.xMin, rect4.y, rect4.height, rect4.height), "-", true, false, true);
			if (flag)
			{
				bool flag2 = MechPowerMod.marketvalue >= 500f;
				if (flag2)
				{
					MechPowerMod.marketvalue -= 50f;
				}
			}
			MechPowerMod.marketvalue = Widgets.HorizontalSlider(new Rect(rect4.xMin + rect4.height + 10f, rect4.y, rect4.width - (rect4.height * 2f + 20f), rect4.height), MechPowerMod.marketvalue, 500f, 4000f, true, null, null, null, -1f);
			bool flag3 = Widgets.ButtonText(new Rect(rect4.xMax - rect4.height, rect4.y, rect4.height, rect4.height), "+", true, false, true);
			if (flag3)
			{
				bool flag4 = MechPowerMod.marketvalue < 4000f;
				if (flag4)
				{
					MechPowerMod.marketvalue += 50f;
				}
			}
			listing_Standard.Gap(10f);
			Rect rect7 = listing_Standard.GetRect(Text.LineHeight);
			Rect rect8 = GenUI.Rounded(GenUI.LeftHalf(rect7));
			Rect rect9 = GenUI.Rounded(GenUI.RightHalf(rect7));
			Rect rect10 = GenUI.Rounded(GenUI.LeftHalf(rect8));
			Rect rect11 = GenUI.Rounded(GenUI.RightHalf(rect8));
			Widgets.Label(rect10, "<b>Power Cell</b> power output (W)");
			Widgets.Label(rect11, string.Format("<b>{0:00}W</b> <color=#ababab>(recommended: 5000W)</color>", MechPowerMod.poweroutput).ToString());
			bool flag5 = Widgets.ButtonText(new Rect(rect9.xMin, rect9.y, rect9.height, rect9.height), "-", true, false, true);
			if (flag5)
			{
				bool flag6 = MechPowerMod.poweroutput >= 2000f;
				if (flag6)
				{
					MechPowerMod.poweroutput -= 500f;
				}
			}
			MechPowerMod.poweroutput = Widgets.HorizontalSlider(new Rect(rect9.xMin + rect9.height + 10f, rect9.y, rect9.width - (rect9.height * 2f + 20f), rect9.height), MechPowerMod.poweroutput, 2000f, 20000f, true, null, null, null, -1f);
			bool flag7 = Widgets.ButtonText(new Rect(rect9.xMax - rect9.height, rect9.y, rect9.height, rect9.height), "+", true, false, true);
			if (flag7)
			{
				bool flag8 = MechPowerMod.poweroutput < 20000f;
				if (flag8)
				{
					MechPowerMod.poweroutput += 500f;
				}
			}
		}
	}
}
