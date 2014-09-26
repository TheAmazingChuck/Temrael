using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells
{
	public class AppelDeLaLicheSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
                "Appel de la liche", "Kal Vas Xen In Corp",
				SpellCircle.Eighth,
				269,
				9070,
				false,
				Reagent.GraveDust,
				Reagent.DaemonBlood,
				Reagent.PigIron
			);

        public override SkillName CastSkill { get { return SkillName.ArtMagique; } }
        public override SkillName DamageSkill { get { return SkillName.Necromancie; } }

        public override bool Invocation { get { return true; } }

        public AppelDeLaLicheSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
		{
		}

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( (Caster.Followers + 4) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
                TimeSpan duration = GetDurationForSpell(30, 1.2);

				SpellHelper.Summon( new SummonedLich(), Caster, 0x217, duration, false, false );
			}

			FinishSequence();
		}
	}
}