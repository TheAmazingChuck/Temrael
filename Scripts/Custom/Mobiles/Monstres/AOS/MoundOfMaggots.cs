using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a maggoty corpse" )] // TODO: Corpse name?
	public class MoundOfMaggots : BaseCreature
	{
		[Constructable]
		public MoundOfMaggots() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a mound of maggots";
			Body = 110;
			BaseSoundID = 898;

			SetStr( 61, 70 );
			SetDex( 61, 70 );
			SetInt( 10 );

			SetMana( 0 );

			SetDamage( 3, 9 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Perforant, 50 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Perforant, 100 );

			SetSkill( SkillName.Tactiques, 50.0 );
			SetSkill( SkillName.Anatomie, 50.1, 60.0 );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Gems );
		}

        public override bool AlwaysMurderer { get { return true; } }
        public override double AttackSpeed { get { return 3.5; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		//public override int TreasureMapLevel{ get{ return 1; } }

		public MoundOfMaggots( Serial serial ) : base( serial )
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