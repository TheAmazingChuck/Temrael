using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ore elemental corpse" )]
	public class ValoriteElemental : BaseCreature
	{
		[Constructable]
		public ValoriteElemental() : this( 2 )
		{
		}

		[Constructable]
		public ValoriteElemental( int oreAmount ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			// TODO: Gas attack
			Name = "a valorite elemental";
            Body = 14;
			BaseSoundID = 268;

			SetStr( 226, 255 );
			SetDex( 126, 145 );
			SetInt( 71, 92 );

			SetHits( 136, 153 );

			SetDamage( 28 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Contondant, 25 );
			SetDamageType( ResistanceType.Tranchant, 25 );
			SetDamageType( ResistanceType.Magie, 25 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Contondant, 50, 60 );
			SetResistance( ResistanceType.Tranchant, 50, 60 );
			SetResistance( ResistanceType.Perforant, 50, 60 );
			SetResistance( ResistanceType.Magie, 40, 50 );

			SetSkill( SkillName.Concentration, 50.1, 95.0 );
			SetSkill( SkillName.Tactiques, 60.1, 100.0 );
			SetSkill( SkillName.ArmePoing, 60.1, 100.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 38;

			Item ore = new MytherilOre( oreAmount );
			ore.ItemID = 0x19B9;
			PackItem( ore );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Gems, 4 );
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)from;

				if ( bc.Controlled || bc.BardTarget == this )
					damage = 0; // Immune to pets and provoked creatures
			}
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			reflect = true; // Every spell is reflected back to the caster
		}

		public ValoriteElemental( Serial serial ) : base( serial )
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