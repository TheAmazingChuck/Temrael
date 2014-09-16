using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a patchwork skeletal corpse" )]
	public class PatchworkSkeleton : BaseCreature
	{
		/*public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}*/

		[Constructable]
		public PatchworkSkeleton() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a patchwork skeleton";
			Body = 121;
			BaseSoundID = 0x48D;

			SetStr( 96, 120 );
			SetDex( 71, 95 );
			SetInt( 16, 40 );

			SetHits( 58, 72 );

			SetDamage( 18, 22 );

			SetDamageType( ResistanceType.Physical, 85 );
			SetDamageType( ResistanceType.Tranchant, 15 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Contondant, 50, 60 );
			SetResistance( ResistanceType.Tranchant, 70, 80 );
			SetResistance( ResistanceType.Perforant, 100 );
			SetResistance( ResistanceType.Magie, 40, 50 );

			SetSkill( SkillName.Concentration, 70.1, 95.0 );
			SetSkill( SkillName.Tactiques, 55.1, 80.0 );
			SetSkill( SkillName.Anatomie, 50.1, 70.0 );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

        public override bool AlwaysMurderer { get { return true; } }
        public override double AttackSpeed { get { return 3.0; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		//public override int TreasureMapLevel{ get{ return 1; } }

		public PatchworkSkeleton( Serial serial ) : base( serial )
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