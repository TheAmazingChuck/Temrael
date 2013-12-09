using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Necromancy
{
	public class VampiricEmbraceSpell : TransformationSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Vampirisme", "Rel Xen An Sanct",
				SpellCircle.Fifth,
				203,
				9031,
				Reagent.BatWing,
				Reagent.NoxCrystal,
				Reagent.PigIron
            );

        public override int RequiredAptitudeValue { get { return 12; } }
        public override NAptitude[] RequiredAptitude { get { return new NAptitude[] {NAptitude.Necromancie }; } }

        public override int Body { get { return 153; } }
        public override int Hue { get { return 0x4001; } }
        public override int DexOffset { get { return 10; } }

		public VampiricEmbraceSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void PlayEffect( Mobile m )
		{
			Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x373A, 1, 17, 1108, 7, 9914, 0 );
			Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x376A, 1, 22, 67, 7, 9502, 0 );
			Effects.PlaySound( m.Location, m.Map, 0x4B1 );
		}
	}
}