using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
	public class RegenerationSpell : ReligiousSpell
	{
        public static Hashtable m_RegenerationTable = new Hashtable();
        public static Hashtable m_Timers = new Hashtable();

		private static SpellInfo m_Info = new SpellInfo(
                "R�g�n�ration", "Tyr Kano",
				SpellCircle.Second,
				212,
				9041
            );

        public RegenerationSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return false; } }

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, m);

                StopTimer(m);

                TimeSpan duration = GetDurationForSpell(0.1);

                m_RegenerationTable[m] = 1.10 + ((Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value) / 800);

                Timer t = new RegenerationTimer(m, DateTime.Now + duration);
                m_Timers[m] = t;
                t.Start();

                m.FixedParticles(14201, 9, 32, 5005, EffectLayer.Waist);
                m.PlaySound(532);
            }

            FinishSequence();
        }

        public void StopTimer(Mobile m)
        {
            Timer t = (Timer)m_Timers[m];

            if (t != null)
            {
                t.Stop();
                m_Timers.Remove(m);
                m_RegenerationTable.Remove(m);

                m.FixedParticles(14201, 9, 32, 5005, EffectLayer.Waist);
                m.PlaySound(532);
            }
        }

        public class RegenerationTimer : Timer
        {
            private Mobile m_target;
            private DateTime endtime;

            public RegenerationTimer(Mobile target, DateTime end)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            {
                m_target = target;
                endtime = end;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if ((DateTime.Now >= endtime && RegenerationSpell.m_RegenerationTable.Contains(m_target)) || m_target == null || m_target.Deleted || !m_target.Alive)
                {
                    RegenerationSpell.m_RegenerationTable.Remove(m_target);
                    RegenerationSpell.m_Timers.Remove(m_target);

                    m_target.FixedParticles(14201, 9, 32, 5005, EffectLayer.Waist);
                    m_target.PlaySound(532);

                    Stop();
                }
            }
        }

		private class InternalTarget : Target
		{
            private RegenerationSpell m_Owner;

            public InternalTarget(RegenerationSpell owner)
                : base(12, false, TargetFlags.Beneficial)
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}