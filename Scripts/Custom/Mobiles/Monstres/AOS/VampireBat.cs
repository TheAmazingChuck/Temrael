using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a vampire bat corpse" )]
	public class VampireBat : BaseCreature
	{
		[Constructable]
		public VampireBat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a vampire bat";
			Body = 108;
			BaseSoundID = 0x270;

			SetStr( 91, 110 );
			SetDex( 91, 115 );
			SetInt( 26, 50 );

			SetHits( 55, 66 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Perforant, 20 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Contondant, 15, 25 );
			SetResistance( ResistanceType.Tranchant, 15, 25 );
			SetResistance( ResistanceType.Perforant, 60, 70 );
			SetResistance( ResistanceType.Magie, 40, 50 );

			SetSkill( SkillName.Concentration, 70.1, 95.0 );
			SetSkill( SkillName.Tactiques, 55.1, 80.0 );
			SetSkill( SkillName.Anatomie, 30.1, 55.0 );

			Fame = 1000;
			Karma = -1000;
		}

        public override bool AlwaysMurderer { get { return true; } }
        public override double AttackSpeed { get { return 2.0; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int GetIdleSound()
		{
			return 0x29B;
		}

		public VampireBat( Serial serial ) : base( serial )
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