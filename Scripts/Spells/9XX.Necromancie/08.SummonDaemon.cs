using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells
{
	public class SummonDaemonSpell : Spell
	{
        public static int m_SpellID { get { return 908; } } // TOCHANGE

        private static int s_ManaCost = 50;
        private static SkillName s_SkillForCast = SkillName.ArtMagique;
        private static int s_MinSkillForCast = 50;
        private static TimeSpan s_DureeCast = TimeSpan.FromSeconds(1);

		public static readonly new SpellInfo Info = new SpellInfo(
				"Conjuration", "Kal Vas Xen Corp",
				8,
				269,
				9050,
                s_ManaCost,
                s_DureeCast,
                s_SkillForCast,
                s_MinSkillForCast,
                false,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public SummonDaemonSpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
		{
		}

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( (Caster.Followers + 5) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
            /*if (!Caster.InLOS(p))
            {
                Caster.SendLocalizedMessage(3000269); // That is out of sight.
            }
			else */if ( CheckSequence() )
            {
                double duration = (2 * Caster.Skills.Immuabilite.Fixed) / 5;

                //SpellHelper.Summon(new GolemArcanique(), Caster, 0x216, TimeSpan.FromSeconds(duration), true, true);
			}

			FinishSequence();
        }

        public override TimeSpan GetCastDelay()
        {
            return base.GetCastDelay() + TimeSpan.FromSeconds(6.0);
        }
	}
}