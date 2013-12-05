using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
    public class AgglomerationSpell : ReligiousSpell
    {
        public static Hashtable m_AgglomerationTable = new Hashtable();
        public static Hashtable m_Timers = new Hashtable();

        private static SpellInfo m_Info = new SpellInfo(
                "Agglom�ration", "Sowi Fehu",
                SpellCircle.Second,
                212,
                9041
            );

        public override int RequiredAptitudeValue { get { return 2; } }
        public override NAptitude[] RequiredAptitude { get { return new NAptitude[] { NAptitude.Fanatisme }; } }

        public AgglomerationSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public override bool DelayedDamage { get { return false; } }

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

                TimeSpan duration = GetDurationForSpell(0.4);

                m_AgglomerationTable[m] = 0.02 + ((Caster.Skills[CastSkill].Value + Caster.Skills[DamageSkill].Value) / 4000); //2 � 7% par joueur.

                Timer t = new AgglomerationTimer(m, DateTime.Now + duration);
                m_Timers[m] = t;
                t.Start();

                m.FixedParticles(14170, 10, 15, 5013, 1942, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(490);
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
                m_AgglomerationTable.Remove(m);

                m.FixedParticles(14170, 10, 15, 5013, 1942, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                m.PlaySound(490);
            }
        }

        public class AgglomerationTimer : Timer
        {
            private Mobile m_target;
            private DateTime endtime;

            public AgglomerationTimer(Mobile target, DateTime end)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(2))
            {
                m_target = target;
                endtime = end;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if ((DateTime.Now >= endtime && AgglomerationSpell.m_AgglomerationTable.Contains(m_target)) || m_target == null || m_target.Deleted || !m_target.Alive)
                {
                    AgglomerationSpell.m_AgglomerationTable.Remove(m_target);
                    AgglomerationSpell.m_Timers.Remove(m_target);

                    m_target.FixedParticles(14170, 10, 15, 5013, 1942, 0, EffectLayer.Head); //ID, speed, dura, effect, hue, render, layer
                    m_target.PlaySound(490);

                    Stop();
                }
            }
        }

        private class InternalTarget : Target
        {
            private AgglomerationSpell m_Owner;

            public InternalTarget(AgglomerationSpell owner)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile && from == o)
                {
                    m_Owner.Target((Mobile)o);
                }
                else
                    from.SendMessage("Vous pouvez �tre la seule cible de ce sort !");
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}