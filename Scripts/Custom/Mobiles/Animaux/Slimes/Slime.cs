using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a slimey corpse" )]
	public class Slime : BaseCreature
	{
		[Constructable]
		public Slime() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a slime";
			Body = 51;
			BaseSoundID = 456;

            ExpKillBonus = 2;

			//Hue = Utility.RandomSlimeHue();

			SetStr( 22, 34 );
			SetDex( 16, 21 );
			SetInt( 16, 20 );

			SetHits( 15, 19 );

			SetDamage( 1, 5 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 5, 10 );
			SetResistance( ResistanceType.Perforant, 10, 20 );

			SetSkill( SkillName.Empoisonnement, 30.1, 50.0 );
			SetSkill( SkillName.Concentration, 15.1, 20.0 );
			SetSkill( SkillName.Tactiques, 19.3, 34.0 );
			SetSkill( SkillName.Anatomie, 19.3, 34.0 );

			VirtualArmor = 8;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 25.0;
		}

		/*public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
			AddLoot( LootPack.Gems );
		}*/

        public override double AttackSpeed { get { return 4.0; } }
		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }
		public override Poison HitPoison{ get{ return Poison.Lesser; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.FruitsAndVegies | FoodType.GrainsAndHay | FoodType.Eggs; } }

		public Slime( Serial serial ) : base( serial )
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
