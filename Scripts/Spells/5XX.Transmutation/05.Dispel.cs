using System;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
	public class DispelSpell : Spell
	{
        public static int m_SpellID { get { return 505; } } // TOCHANGE

        private static int s_ManaCost = 50;
        private static SkillName s_SkillForCast = SkillName.ArtMagique;
        private static int s_MinSkillForCast = 50;
        private static TimeSpan s_DureeCast = TimeSpan.FromSeconds(1);
        
        public static readonly SpellInfo m_Info = new SpellInfo(
				"Dissipation", "An Ort",
				SpellCircle.Fifth,
				218,
				9002,
                s_ManaCost,
                s_DureeCast,
                s_SkillForCast,
                s_MinSkillForCast,
                false,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
            );

		public DispelSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			Type t = m.GetType();
			bool dispellable = false;

			if ( m is BaseCreature )
				dispellable = ((BaseCreature)m).Summoned && !((BaseCreature)m).IsAnimatedDead;

			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( !dispellable )
			{
				Caster.SendLocalizedMessage( 1005049 ); // That cannot be dispelled.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				BaseCreature bc = m as BaseCreature;

				double dispelChance = 0;

				if ( bc != null )
					dispelChance = (50.0 + ((100 * (Caster.Skills.Necromancie.Value - bc.DispelDifficulty)) / (bc.DispelFocus * 2))) / 120;

                dispelChance = SpellHelper.AdjustValue(Caster, dispelChance);

				if ( dispelChance > Utility.RandomDouble() )
				{
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
					Effects.PlaySound( m, m.Map, 0x201 );

					m.Delete();
				}
				else
                {
                    Caster.DoHarmful(m);

					m.FixedEffect( 0x3779, 10, 20 );
					Caster.SendLocalizedMessage( 1010084 ); // The creature resisted the attempt to dispel it!
				}
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private DispelSpell m_Owner;

			public InternalTarget( DispelSpell owner ) : base( 12, false, TargetFlags.Harmful )
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