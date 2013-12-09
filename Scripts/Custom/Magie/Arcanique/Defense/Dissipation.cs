using System;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells
{
	public class DissipationSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Dissipation", "An Ort",
				SpellCircle.Third,
				218,
				9002,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
            );

        public override int RequiredAptitudeValue { get { return 2; } }
        public override NAptitude[] RequiredAptitude { get { return new NAptitude[] {NAptitude.Thaumaturgie }; } }

        public DissipationSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
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

            if (m is TMobile)
                dispellable = true;

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

                if (m is BaseCreature)
                {
                    BaseCreature bc = m as BaseCreature;

                    double dispelChance = 0;

                    if (bc != null)
                        dispelChance = (50.0 + ((100 * (Caster.Skills.ArtMagique.Value - bc.DispelDifficulty)) / (bc.DispelFocus * 2))) / 120;

                    dispelChance = SpellHelper.AdjustValue(Caster, dispelChance, NAptitude.Sorcellerie);

                    if (dispelChance > Utility.RandomDouble())
                    {
                        Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                        Effects.PlaySound(m, m.Map, 0x201);

                        m.Delete();
                    }
                    else
                    {
                        m.FixedEffect(0x3779, 10, 20);
                        Caster.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                    }
                }
                else if (m is TMobile)
                {
                    TMobile pm = m as TMobile;

                    double dispelChance = 0;

                    if (pm != null)
                        dispelChance = (Caster.Skills.ArtMagique.Value - pm.Skills[SkillName.Concentration].Value + 1) / 120;

                    dispelChance = SpellHelper.AdjustValue(Caster, dispelChance, NAptitude.Sorcellerie);

                    if (m == Caster)
                        dispelChance = 1;

                    if (dispelChance >= Utility.RandomDouble())
                    {
                        Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                        Effects.PlaySound(m, m.Map, 0x201);

                        pm.DispelAllTransformations();
                    }
                    else
                    {
                        m.FixedEffect(0x3779, 10, 20);
                        Caster.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                    }
                }
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
            private DissipationSpell m_Owner;

            public InternalTarget(DissipationSpell owner)
                : base(12, false, TargetFlags.None)
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