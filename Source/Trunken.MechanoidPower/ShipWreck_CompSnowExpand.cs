using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace Trunken_MechanoidPower
{
	// Token: 0x02000003 RID: 3
	public class ShipWreck_CompSnowExpand : ThingComp
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020A4 File Offset: 0x000002A4
		public CompProperties_SnowExpandCrashWreck Props
		{
			get
			{
				return (CompProperties_SnowExpandCrashWreck)this.props;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020C1 File Offset: 0x000002C1
		public override void PostExposeData()
		{
			Scribe_Values.Look<float>(ref this.snowRadius, "snowRadius", 0f, false);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
		public override void CompTick()
		{
			bool flag = !this.parent.Spawned;
			if (!flag)
			{
				bool flag2 = this.parent.IsHashIntervalTick(this.Props.expandInterval);
				if (flag2)
				{
					this.TryExpandSnow();
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
		private void TryExpandSnow()
		{
			bool flag = this.parent.Map.mapTemperature.OutdoorTemp > this.Props.maxTemperate;
			if (flag)
			{
				this.snowRadius = 0f;
			}
			else
			{
				bool flag2 = this.snowNoise == null;
				if (flag2)
				{
					this.snowNoise = new Perlin(0.054999999701976776, 2.0, 0.5, 5, Rand.Range(0, 651431), QualityMode.Medium);
				}
				bool flag3 = this.snowRadius < 8f;
				if (flag3)
				{
					this.snowRadius += 1.3f;
				}
				else
				{
					bool flag4 = this.snowRadius < 17f;
					if (flag4)
					{
						this.snowRadius += 0.7f;
					}
					else
					{
						bool flag5 = this.snowRadius < 30f;
						if (flag5)
						{
							this.snowRadius += 0.4f;
						}
						else
						{
							this.snowRadius += 0.1f;
						}
					}
				}
				this.snowRadius = Mathf.Min(this.snowRadius, this.Props.maxRadius);
				CellRect occupiedRect = this.parent.OccupiedRect();
				ShipWreck_CompSnowExpand.reachableCells.Clear();
				this.parent.Map.floodFiller.FloodFill(this.parent.Position, (IntVec3 x) => (float)x.DistanceToSquared(this.parent.Position) <= this.snowRadius * this.snowRadius && (occupiedRect.Contains(x) || !x.Filled(this.parent.Map)), delegate(IntVec3 x)
				{
					ShipWreck_CompSnowExpand.reachableCells.Add(x);
				}, int.MaxValue, false, null);
				int num = GenRadial.NumCellsInRadius(this.snowRadius);
				for (int i = 0; i < num; i++)
				{
					IntVec3 intVec = this.parent.Position + GenRadial.RadialPattern[i];
					bool flag6 = intVec.InBounds(this.parent.Map);
					if (flag6)
					{
						bool flag7 = ShipWreck_CompSnowExpand.reachableCells.Contains(intVec);
						if (flag7)
						{
							float num2 = this.snowNoise.GetValue(intVec);
							num2 += 1f;
							num2 *= 0.5f;
							bool flag8 = num2 < 0.1f;
							if (flag8)
							{
								num2 = 0.1f;
							}
							bool flag9 = this.parent.Map.snowGrid.GetDepth(intVec) <= num2;
							if (flag9)
							{
								float lengthHorizontal = (intVec - this.parent.Position).LengthHorizontal;
								float num3 = 1f - lengthHorizontal / this.snowRadius;
								this.parent.Map.snowGrid.AddDepth(intVec, num3 * this.Props.addAmount * num2);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000005 RID: 5
		private float snowRadius;

		// Token: 0x04000006 RID: 6
		private ModuleBase snowNoise;

		// Token: 0x04000007 RID: 7
		private const float MaxOutdoorTemp = 10f;

		// Token: 0x04000008 RID: 8
		private static HashSet<IntVec3> reachableCells = new HashSet<IntVec3>();
	}
}
