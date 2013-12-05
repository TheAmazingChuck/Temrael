using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a vampire bat corpse" )]
	public class VampireBatFamiliar : BaseFamiliar
	{
		public VampireBatFamiliar()
		{
			Name = "a vampire bat";
			Body = 108;
			BaseSoundID = 0x270;

			SetStr( 120 );
			SetDex( 120 );
			SetInt( 100 );

			SetHits( 90 );
			SetStam( 120 );
			SetMana( 0 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10, 15 );
			SetResistance( ResistanceType.Contondant, 10, 15 );
			SetResistance( ResistanceType.Tranchant, 10, 15 );
			SetResistance( ResistanceType.Perforant, 10, 15 );
			SetResistance( ResistanceType.Magie, 10, 15 );

			SetSkill( SkillName.ArmePoing, 95.1, 100.0 );
			SetSkill( SkillName.Tactiques, 50.0 );

			ControlSlots = 1;
		}

		public VampireBatFamiliar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}