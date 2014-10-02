using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells
{
	public class LichFormSpell : TransformationSpell
	{
        public static int m_SpellID { get { return 904; } } // TOCHANGE

        private static int s_ManaCost = 50;
        private static SkillName s_SkillForCast = SkillName.ArtMagique;
        private static int s_MinSkillForCast = 50;
        private static TimeSpan s_DureeCast = TimeSpan.FromSeconds(1);

		public static readonly new SpellInfo Info = new SpellInfo(
				"Liche", "Rel Xen Corp Ort",
				SpellCircle.Fifth,
				203,
				9031,
                s_ManaCost,
                s_DureeCast,
                s_SkillForCast,
                s_MinSkillForCast,
                false,
				Reagent.GraveDust,
				Reagent.DaemonBlood,
				Reagent.NoxCrystal
            );

        public override int Body { get { return 24; } }
        public override int IntOffset { get { return 30; } }

		public LichFormSpell( Mobile caster, Item scroll ) : base( caster, scroll, Info )
		{
		}

		public override void PlayEffect( Mobile m )
		{
			m.PlaySound( 0x19C );
			m.FixedParticles( 0x3709, 1, 30, 9904, 1108, 6, EffectLayer.RightFoot );
		}
	}
}