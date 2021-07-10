using System.Collections.Generic;
using Verse;

namespace MechPowerSettings
{
    // Token: 0x02000002 RID: 2
    internal class MechPowerMod : ModSettings
    {
        // Token: 0x04000001 RID: 1
        internal static float poweroutput = 5000f;

        // Token: 0x04000002 RID: 2
        internal static float marketvalue = 2000f;

        // Token: 0x04000003 RID: 3
        internal static List<ThingDef> database;

        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref poweroutput, "powerOutput", 5000f);
            Scribe_Values.Look(ref marketvalue, "marketValue", 2000f);
        }
    }
}