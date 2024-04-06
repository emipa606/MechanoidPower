using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace Trunken_MechanoidPower;

public class ShipWreck_CompSnowExpand : ThingComp
{
    private const float MaxOutdoorTemp = 10f;

    private static readonly HashSet<IntVec3> reachableCells = [];

    private ModuleBase snowNoise;

    private float snowRadius;

    public CompProperties_SnowExpandCrashWreck Props => (CompProperties_SnowExpandCrashWreck)props;

    public override void PostExposeData()
    {
        Scribe_Values.Look(ref snowRadius, "snowRadius");
    }

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
                x => x.DistanceToSquared(parent.Position) <= snowRadius * snowRadius &&
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