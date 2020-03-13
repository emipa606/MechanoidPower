using System;
using Verse;

namespace Trunken_MechanoidPower
{
	// Token: 0x02000002 RID: 2
	public class CompProperties_SnowExpandCrashWreck : CompProperties
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CompProperties_SnowExpandCrashWreck()
		{
			this.compClass = typeof(ShipWreck_CompSnowExpand);
		}

		// Token: 0x04000001 RID: 1
		public int expandInterval = 500;

		// Token: 0x04000002 RID: 2
		public float addAmount = 0.12f;

		// Token: 0x04000003 RID: 3
		public float maxRadius = 55f;

		// Token: 0x04000004 RID: 4
		public float maxTemperate = 30f;
	}
}
