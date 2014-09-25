using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a ronin corpse" )]
	public class Ronin : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public Ronin() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Hue = Utility.RandomSkinHue();
			Name = "a ronin";
			Body = (( this.Female = Utility.RandomBool() ) ? Body = 0x191 : Body = 0x190);
			
			Hue = Utility.RandomSkinHue();

			SetStr( 326, 375 );
			SetDex( 31, 45 );
			SetInt( 101, 110 );

	        SetHits( 301, 400 );
			SetMana( 101, 110 );

			SetDamage( 17, 25 );

			SetDamageType( ResistanceType.Physical, 90 );

			SetResistance( ResistanceType.Physical, 55, 75 );
			SetResistance( ResistanceType.Magie, 55, 75 );

			SetSkill( SkillName.Concentration, 42.6, 57.5 );
			SetSkill( SkillName.Tactiques, 115.1, 130.0 );
			SetSkill( SkillName.Anatomie, 92.6, 107.5 );
			//SetSkill( SkillName.Anatomy, 110.1, 125.0 );

			SetSkill( SkillName.ArmePerforante, 92.6, 107.5 );
			SetSkill( SkillName.ArmeContondante, 92.6, 107.5 );
			SetSkill( SkillName.ArmeTranchante, 92.6, 107.5 );

			AddItem( new SamuraiTabi() );



				AddItem( new Halberd() );


			PackItem( new Longsword() );

			Utility.AssignRandomHair( this );
		}
		
		public override void OnDeath( Container c )
 		{
			base.OnDeath( c );
	 		c.DropItem( new BookOfBushido() );
 		}

		// TODO: Bushido abilities

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }

		public Ronin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
