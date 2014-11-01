using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
	public class ProtectionSpell : Spell
	{
        public static int m_SpellID { get { return 402; } } // TOCHANGE

        private static short s_Cercle = 2;

		public static readonly new SpellInfo Info = new SpellInfo(
				"Protection", "Uus Sanct",
                s_Cercle,
                203,
                9031,
                GetBaseManaCost(s_Cercle),
                TimeSpan.FromSeconds(1),
                SkillName.ArtMagique,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.SulfurousAsh
            );

		public ProtectionSpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
		{
		}

		public override bool CheckCast()
		{
			/*if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
				return false;
			}*/

			return true;
		}

        public override void OnCast()
        {
            if (Caster is TMobile)
            {
                Caster.Target = new InternalTarget(this);
            }
            else
            {
                if (!Caster.CanBeginAction(typeof(DefensiveSpell)))
                {
                    Caster.SendLocalizedMessage(1005385); // The spell will not adhere to you at this time.
                }
                else if (CheckSequence())
                {
                    if (Caster.BeginAction(typeof(DefensiveSpell)))
                    {
                        double value = (int)(Caster.Skills[SkillName.Thaumaturgie].Value + Caster.Skills[SkillName.ArtMagique].Value + Caster.Skills[SkillName.Meditation].Value);
                        value /= 9;

                        value = SpellHelper.AdjustValue(Caster, value);

                        if (value > 30)
                            value = 30;

                        double duree = value * 4;

                        duree = SpellHelper.AdjustValue(Caster, duree);

                        if (duree > 60)
                            duree = 60;

                        Caster.VirtualArmorMod += (int)value;

                        new InternalTimer(Caster, (int)value, duree).Start();

                        Caster.FixedParticles(0x375A, 9, 20, 5016, EffectLayer.Waist);
                        Caster.PlaySound(0x1ED);
                    }
                    else
                    {
                        Caster.SendLocalizedMessage(1005385); // The spell will not adhere to you at this time.
                    }
                }
                FinishSequence();
            }
        }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckSequence())
            {
                double value = (int)(Caster.Skills[SkillName.Thaumaturgie].Value + Caster.Skills[SkillName.ArtMagique].Value + Caster.Skills[SkillName.Meditation].Value);
                value /= 9;

                value = SpellHelper.AdjustValue(Caster, value);

                if (value > 30)
                    value = 30;

                double duree = value * 4;

                duree = SpellHelper.AdjustValue(Caster, duree);

                if (duree > 60)
                    duree = 60;

                if (m.BeginAction(typeof(DefensiveSpell)))
                {
                    if (m.VirtualArmorMod == 0)
                    {
                        m.VirtualArmorMod += (int)value;

                        new InternalTimer(m, (int)value, duree).Start();

                        m.FixedParticles(0x375A, 9, 20, 5016, EffectLayer.Waist);
                        m.PlaySound(0x1ED);
                    }
                }
            }

            FinishSequence();
        }

        private class InternalTimer : Timer
        {
            private Mobile m_Owner;
            private int m_Value;

            public InternalTimer(Mobile caster, int value, double duree) : base(TimeSpan.FromSeconds(duree))
            {
                Priority = TimerPriority.OneSecond;

                m_Owner = caster;
                m_Value = value;
            }

            protected override void OnTick()
            {
                m_Owner.EndAction(typeof(DefensiveSpell));
                m_Owner.VirtualArmorMod -= m_Value;

                if (m_Owner.VirtualArmorMod < 0)
                    m_Owner.VirtualArmorMod = 0;

                m_Owner.SendMessage("Protection prend fin");
            }
        }

        private class InternalTarget : Target
        {
            private ProtectionSpell m_Owner;

            public InternalTarget(ProtectionSpell owner)
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
