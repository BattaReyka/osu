﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables.Pieces;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModSpunOut : Mod, IApplicableToDrawableHitObjects, IUpdatableByPlayfield
    {
        public override string Name => "Spun Out";
        public override string Acronym => "SO";
        public override IconUsage? Icon => OsuIcon.ModSpunout;
        public override ModType Type => ModType.Automation;
        public override string Description => @"Spinners will be automatically completed.";
        public override double ScoreMultiplier => 0.9;
        public override bool Ranked => true;
        public override Type[] IncompatibleMods => new[] { typeof(ModAutoplay), typeof(OsuModAutopilot) };

        private double lastFrameTime;
        private float frameDelay;

        public void ApplyToDrawableHitObjects(IEnumerable<DrawableHitObject> drawables)
        {
            foreach (var hitObject in drawables)
            {
                if (hitObject is DrawableSpinner spinner)
                {
                    spinner.Disc.Enabled = false;
                    spinner.Disc.OnUpdate += d =>
                    {
                        var s = d as SpinnerDisc;

                        if (s.Valid)
                            s.Rotate(180 / MathF.PI * frameDelay / 40);
                    };
                }
            }
        }

        public void Update(Playfield playfield)
        {
            frameDelay = (float)(playfield.Time.Current - lastFrameTime);
            lastFrameTime = playfield.Time.Current;
        }
    }
}
