﻿using System;
using Server.Targeting;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Spells.First
{
    public class Bourrasque : Spell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Bourrasque", "In Grav",
                SpellCircle.First,
                236,
                9031,
                Reagent.MandrakeRoot
            );

        public override int RequiredAptitudeValue { get { return 1; } }
        public override Aptitude[] RequiredAptitude { get { return new Aptitude[] { Aptitude.Evocation }; } }

        public Bourrasque(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new BourrasqueTarget(this);
        }

        private class BourrasqueTarget : Target
        {
            private Spell m_Spell;

            public BourrasqueTarget(Spell spell)
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

                        double value = Utility.Random(3, 7);

                        //value = SpellHelper.AdjustValue(m_Spell.Caster, value, NAptitude.Spiritisme);

                        Server.Misc.Weather.RemoveWeather(from.Location);

                        Server.Misc.Weather.AddWeather(weather.Temperature, weather.Cloud, (QuantityOfWind)value, false, new Rectangle2D(new Point2D(0, 0), new Point2D(6145, 4097)));

                        from.SendMessage(String.Concat("Le vent est désormais ", ((QuantityOfWind)value).ToString()));
                    }
                    else
                    {
                        from.SendMessage("Vous ne pouvez pas faire ca sous terre !");
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
