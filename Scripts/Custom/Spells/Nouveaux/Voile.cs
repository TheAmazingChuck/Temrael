﻿using System;
using Server.Targeting;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Spells.First
{
    public class Voile : Spell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Voile", "Des Lor",
                SpellCircle.First,
                236,
                9031,
                Reagent.SulfurousAsh,
                Reagent.SpidersSilk
            );

        public override int RequiredAptitudeValue { get { return 1; } }
        public override Aptitude[] RequiredAptitude { get { return new Aptitude[] { Aptitude.Illusion }; } }

        public Voile(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new VoileTarget(this);
        }

        private class VoileTarget : Target
        {
            private Spell m_Spell;

            public VoileTarget(Spell spell)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Spell = spell;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile && m_Spell.CheckBSequence((Mobile)targeted))
                {
                    Mobile targ = (Mobile)targeted;

                    SpellHelper.Turn(m_Spell.Caster, targ);

                    if (targ.BeginAction(typeof(LightCycle)))
                    {
                        double value = Utility.Random(15, 25);

                        value = SpellHelper.AdjustValue(m_Spell.Caster, value, Aptitude.Spiritisme);

                        new LightCycle.NightSightTimer(targ).Start();

                        targ.LightLevel = -100;

                        targ.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                        targ.PlaySound(0x1E3);
                    }
                    else
                    {
                        from.SendMessage("{0} deja le sort de voile applique.", from == targ ? "Vous avez" : "Ils ont");
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
