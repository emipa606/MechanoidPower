using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace Trunken_MechanoidPower
{
    // Token: 0x02000003 RID: 3
    public class ShipWreck_CompSnowExpand : ThingComp
    {
        // Token: 0x04000007 RID: 7
        private const float MaxOutdoorTemp = 10f;

        // Token: 0x04000008 RID: 8
        private static readonly HashSet<IntVec3> reachableCells = new HashSet<IntVec3>();

        // Token: 0x04000006 RID: 6
        private ModuleBase snowNoise;

        // Token: 0x04000005 RID: 5
        private float snowRadius;

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000002 RID: 2 RVA: 0x000020A4 File Offset: 0x000002A4
        public CompProperties_SnowExpandCrashWreck Props => (CompProperties_SnowExpandCrashWreck) props;

        // Token: 0x06000003 RID: 3 RVA: 0x000020C1 File Offset: 0x000002C1
        public override void PostExposeData()
        {
            Scribe_Values.Look(ref snowRadius, "snowRadius");
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000020DC File Offset: 0x000002DC
        public override void CompTick()
        {
            if (!parent.Spawned)
            {
                return;
            }

            if (parent.IsHashIntervalTick(Props.expandInterval))
            {
                TryExpandSnow();
            }
        }

        // Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
        private void TryExpandSnow()
        {
            if (parent.Map.mapTemperature.OutdoorTemp > Props.maxTemperate)
            {
                snowRadius = 0f;
            }
            else
            {
                if (snowNoise == null)
                {
                    snowNoise = new Perlin(0.054999999701976776, 2.0, 0.5, 5, Rand.Range(0, 651431),
                        QualityMode.Medium);
                }

                switch (snowRadius)
                {
                    case < 8f:
                        snowRadius += 1.3f;
                        break;
                    case < 17f:
                        snowRadius += 0.7f;
                        break;
                    case < 30f:
                        snowRadius += 0.4f;
                        break;
                    default:
                        snowRadius += 0.1f;
                        break;
                }

                snowRadius = Mathf.Min(snowRadius, Props.maxRadius);
                var occupiedRect = parent.OccupiedRect();
                reachableCells.Clear();
                parent.Map.floodFiller.FloodFill(parent.Position,
                    x => (float) x.DistanceToSquared(parent.Position) <= snowRadius * snowRadius &&
                         (occupiedRect.Contains(x) || !x.Filled(parent.Map)),
                    delegate(IntVec3 x) { reachableCells.Add(x); });
                var num = GenRadial.NumCellsInRadius(snowRadius);
                for (var i = 0; i < num; i++)
                {
                    var intVec = parent.Position + GenRadial.RadialPattern[i];
                    if (!intVec.InBounds(parent.Map))
                    {
                        continue;
                    }

                    if (!reachableCells.Contains(intVec))
                    {
                        continue;
                    }

                    var num2 = snowNoise.GetValue(intVec);
                    num2 += 1f;
                    num2 *= 0.5f;
                    if (num2 < 0.1f)
                    {
                        num2 = 0.1f;
                    }

                    if (!(parent.Map.snowGrid.GetDepth(intVec) <= num2))
                    {
                        continue;
                    }

                    var lengthHorizontal = (intVec - parent.Position).LengthHorizontal;
                    var num3 = 1f - (lengthHorizontal / snowRadius);
                    parent.Map.snowGrid.AddDepth(intVec, num3 * Props.addAmount * num2);
                }
            }
        }
    }
}