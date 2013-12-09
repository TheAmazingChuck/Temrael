using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells
{
	public class ChantSpell : BardeSpell
	{
        public static Hashtable m_Timers = new Hashtable();

		private static SpellInfo m_Info = new SpellInfo(
				"Chant", "",
				SpellCircle.First,
				215,
				9041,
				false
			);

        public override int RequiredAptitudeValue { get { return 6; } }
        public override NAptitude[] RequiredAptitude { get { return new NAptitude[] { NAptitude.Composition }; } }

        public ChantSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
        {
            if (CheckSequence())
            {
                TimeSpan duration = GetDurationForSpell(20, 1.5);
                double amount = 2; 

                if (Caster is TMobile)
                {
                    amount *= ((TMobile)Caster).GetAptitudeValue(NAptitude.Composition);
                }

                DateTime endtime = DateTime.Now + duration;

                ArrayList m_target = new ArrayList();

                Map map = Caster.Map;

                if (map != null)
                {
                    foreach (Mobile m in Caster.GetMobilesInRange(8))
                    {
                        if (Caster.CanBeBeneficial(m, false) && (Caster.Party == m.Party) && (m.AccessLevel < AccessLevel.GameMaster))
                            m_target.Add(m);
                    }
                }

                for (int i = 0; i < m_target.Count; ++i)
                {
                    Mobile targ = (Mobile)m_target[i];

                    StopTimer(targ);

                    Timer t = new ChantTimer(targ, amount, DateTime.Now + duration);
                    m_Timers[targ] = t;
                    t.Start();

                    targ.FixedParticles(14201, 10, 20, 5013, 1944, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                    targ.PlaySound(580);
                }
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

                m.FixedParticles(14201, 10, 20, 5013, 1944, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(580);
            }
        }

        public class ChantTimer : Timer
        {
            private Mobile m_target;
            private DateTime endtime;
            private double m_amount;

            public ChantTimer(Mobile target, double amount, DateTime end)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(10))
            {
                m_target = target;
                endtime = end;
                m_amount = amount;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (DateTime.Now >= endtime || m_target == null || m_target.Deleted || !m_target.Alive)
                {
                    ChantSpell.m_Timers.Remove(m_target);

                    m_target.FixedParticles(14201, 10, 20, 5013, 1944, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                    m_target.PlaySound(580);

                    Stop();
                }
                else
                {
                    m_target.FixedParticles(14201, 10, 20, 5013, 1944, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                    m_target.Mana += (int)m_amount;
                }
            }
        }
	}
}