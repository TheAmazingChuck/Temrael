﻿using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Third
{
    public class BenirMiracle : ReligiousSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                "Bénir", "",
                SpellCircle.Eighth,
                17,
                9050
            );

        public override int RequiredAptitudeValue { get { return 10; } }
        public override NAptitude[] RequiredAptitude { get { return new NAptitude[] { NAptitude.Benedictions }; } }

        public BenirMiracle(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckBSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.AddStatBonus(Caster, m, StatType.Str); SpellHelper.DisableSkillCheck = true;
                SpellHelper.AddStatBonus(Caster, m, StatType.Dex);
                SpellHelper.AddStatBonus(Caster, m, StatType.Int); SpellHelper.DisableSkillCheck = false;

                m.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                m.PlaySound(0x1EA);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private BenirMiracle m_Owner;

            public InternalTarget(BenirMiracle owner)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}