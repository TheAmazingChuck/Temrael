﻿using System;
using Server.Targeting;
using Server.Network;
using Server;
using Server.Mobiles;
using Server.Misc;

namespace Server.Spells.First
{
    public class Froid : Spell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Tempete", "An Flam",
                SpellCircle.First,
                236,
                9031,
                Reagent.Bloodmoss
            );

        public override int RequiredAptitudeValue { get { return 1; } }
        public override Aptitude[] RequiredAptitude { get { return new Aptitude[] { Aptitude.Evocation }; } }

        public Froid(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new FroidTarget(this);
        }

        private class FroidTarget : Target
        {
            private Spell m_Spell;

            public FroidTarget(Spell spell)
                : base(12, true, TargetFlags.Beneficial)
            {
                m_Spell = spell;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is LandTarget)
                {
                    Server.Misc.Weather weather = Server.Misc.Weather.GetWeather(from.Location);

                    if (!(weather.Cloud == DensityOfCloud.Caverne))
                    {
                        LandTarget targ = (LandTarget)targeted;

                        SpellHelper.Turn(m_Spell.Caster, targ);

                        double value = Utility.RandomMinMax(0, 4);

                        //value = SpellHelper.AdjustValue(m_Spell.Caster, value, NAptitude.Spiritisme);

                        Server.Misc.Weather.RemoveWeather(from.Location);

                        Server.Misc.Weather.AddWeather((Temperature)value, weather.Cloud, weather.Wind, false, new Rectangle2D(new Point2D(0, 0), new Point2D(6145, 4097)));

                        from.SendMessage(String.Concat("La température est désormais ", ((Temperature)value).ToString()));
                    }
                    else
                    {
                        from.SendMessage("Vous ne pouvez pas faire ça sous terre !");
                    }
                }

                m_Spell.FinishSequence();
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Spell.FinishSequence();
            }
        }
    }
}
