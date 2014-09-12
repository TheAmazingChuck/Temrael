using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ice fiend corpse" )]
	public class IceFiend : BaseCreature
	{
		[Constructable]
		public IceFiend () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ice fiend";
			Body = 43;
			BaseSoundID = 357;

			SetStr( 376, 405 );
			SetDex( 176, 195 );
			SetInt( 201, 225 );

			SetHits( 226, 243 );

			SetDamage( 8, 19 );

			//SetSkill( SkillName.EvalInt, 80.1, 90.0 );
			SetSkill( SkillName.ArtMagique, 80.1, 90.0 );
			SetSkill( SkillName.Concentration, 75.1, 85.0 );
			SetSkill( SkillName.Tactiques, 80.1, 90.0 );
			SetSkill( SkillName.Anatomie, 80.1, 100.0 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Contondant, 10, 20 );
			SetResistance( ResistanceType.Tranchant, 60, 70 );
			SetResistance( ResistanceType.Perforant, 20, 30 );
			SetResistance( ResistanceType.Magie, 30, 40 );

			VirtualArmor = 60;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 1; } }

		public IceFiend( Serial serial ) : base( serial )
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