using osu.Framework.Bindables;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects;
using System;
using osu.Game.Beatmaps;

public class ModRandomAR : Mod, IApplicableToHitObject
{
    public override string Name => "Random AR";
    public override string Acronym => "RAR";
    public override ModType Type => ModType.DifficultyIncrease;
    public override double ScoreMultiplier => 1;
    
    public Bindable<int?> Seed { get; } = new Bindable<int?>();

    public void ApplyToHitObject(HitObject hitObject)
    {
        Random rng = Seed.HasValue ? new Random(Seed.Value) : new Random();

        float randomAR = (float)(5 + rng.NextDouble() * 5);

        hitObject.ApplyCustomRate(randomAR);
    }
}
