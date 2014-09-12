using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ore elemental corpse" )]
	public class VeriteElemental : BaseCreature
	{
		[Constructable]
		public VeriteElemental() : this( 2 )
		{
		}

		[Constructable]
		public VeriteElemental( int oreAmount ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a verite elemental";
            Body = 14;
			BaseSoundID = 268;

			SetStr( 226, 255 );
			SetDex( 126, 145 );
			SetInt( 71, 92 );

			SetHits( 136, 153 );

			SetDamage( 9, 16 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Magie, 50 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Contondant, 10, 20 );
			SetResistance( ResistanceType.Tranchant, 50, 60 );
			SetResistance( ResistanceType.Perforant, 50, 60 );
			SetResistance( ResistanceType.Magie, 50, 60 );

			SetSkill( SkillName.Concentration, 50.1, 95.0 );
			SetSkill( SkillName.Tactiques, 60.1, 100.0 );
			SetSkill( SkillName.Anatomie, 60.1, 100.0 );

			VirtualArmor = 35;

			Item ore = new MytherilOre( oreAmount );
			ore.ItemID = 0x19B9;
			PackItem( ore );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public VeriteElemental( Serial serial ) : base( serial )
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