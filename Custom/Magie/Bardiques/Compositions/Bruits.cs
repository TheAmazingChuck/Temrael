using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells
{
	public class BruitSpell : BardeSpell
	{
        public static Hashtable m_BruitTable = new Hashtable();
        public static Hashtable m_Timers = new Hashtable();

        //Identification des constantes utilis�es (pour modification ais�e)
        private const double bonus_donne = 0.03;
        private const int portee = 8;

		private static SpellInfo m_Info = new SpellInfo(
				"Bruit", "",
				SpellCircle.First,
				215,
				9041,
				false
			);

        public override int RequiredAptitudeValue { get { return 1; } }
        public override NAptitude[] RequiredAptitude { get { return new NAptitude[] { NAptitude.Composition }; } }

        public BruitSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
        {
            if (CheckSequence())
            {
                TimeSpan duration = GetDurationForSpell(20, 1.5);
                double factor = 0;

                //Calcul du bonus donn� par le sort (niveau * bonus_donne)
                if (Caster is TMobile)
                {
                    factor += (double)(((TMobile)Caster).GetAptitudeValue(NAptitude.Composition) * bonus_donne);
                }

                DateTime endtime = DateTime.Now + duration;

                ArrayList m_target = new ArrayList();

                Map map = Caster.Map;

                //D�finition des cibles du sort
                if (map != null)
                {
                    foreach (Mobile m in Caster.GetMobilesInRange(portee))
                    {
                        if (Caster.CanBeBeneficial(m, false) && (Caster.Party == m.Party) && (m.AccessLevel < AccessLevel.GameMaster))
                            m_target.Add(m);
                    }
                }

                for (int i = 0; i < m_target.Count; ++i)
                {
                    Mobile targ = (Mobile)m_target[i];

                    StopTimer(targ);

                    m_BruitTable[targ] = factor;

                    Timer t = new BruitTimer(targ, DateTime.Now + duration);
                    m_Timers[targ] = t;
                    t.Start();

                    targ.FixedParticles(14170, 10, 20, 5013, 2144, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                    targ.PlaySound(526);
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
                m_BruitTable.Remove(m);

                m.FixedParticles(14170, 10, 20, 5013, 2144, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(526);
            }
        }

        public class BruitTimer : Timer
        {
            private Mobile m_target;
            private DateTime endtime;

            public BruitTimer(Mobile target, DateTime end)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(2))
            {
                m_target = target;
                endtime = end;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if ((DateTime.Now >= endtime && BruitSpell.m_BruitTable.Contains(m_target)) || m_target == null || m_target.Deleted || !m_target.Alive)
                {
                    BruitSpell.m_BruitTable.Remove(m_target);
                    BruitSpell.m_Timers.Remove(m_target);

                    m_target.FixedParticles(14170, 10, 20, 5013, 2144, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                    m_target.PlaySound(526);

                    Stop();
                }
            }
        }
	}
}