using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server;

namespace Server.Spells
{
	public class FlecheMagiqueSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Fleche Magique", "In Por Ylem",
				SpellCircle.First,
				212,
				9041,
				Reagent.SulfurousAsh
            );

        public override int RequiredAptitudeValue { get { return 3; } }
        public override NAptitude[] RequiredAptitude { get { return new NAptitude[] { NAptitude.Alteration }; } }

        public FlecheMagiqueSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
            else if (CheckHSequence(m))
            {
                Mobile source = Caster;

                SpellHelper.Turn(source, m);

                SpellHelper.CheckReflect((int)this.Circle, ref source, ref m);

                double damage = GetNewAosDamage(2, 1, 2, true); ;

                source.MovingParticles(m, 0x36E4, 5, 0, false, true, 3006, 4006, 0);
                source.PlaySound(0x1E5);

                SpellHelper.Damage(this, m, damage, 0, 100, 0, 0, 0);
            }

			FinishSequence();
		}

		private class InternalTarget : Target
		{
            private FlecheMagiqueSpell m_Owner;

            public InternalTarget(FlecheMagiqueSpell owner)
                : base(12, false, TargetFlags.Harmful)
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