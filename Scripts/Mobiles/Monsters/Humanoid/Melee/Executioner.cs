using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
	public class Executioner : BaseCreature 
	{ 
		[Constructable] 
		public Executioner() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			SpeechHue = Utility.RandomDyedHue(); 
			Title = "the executioner"; 
			Hue = Utility.RandomSkinHue(); 

			if ( this.Female = Utility.RandomBool() ) 
			{ 
				this.Body = 0x191; 
				this.Name = NameList.RandomName( "female" ); 
				AddItem( new Skirt( Utility.RandomRedHue() ) ); 
			} 
			else 
			{ 
				this.Body = 0x190; 
				this.Name = NameList.RandomName( "male" ); 
				AddItem( new ShortPants( Utility.RandomRedHue() ) ); 
			} 

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Contondant, 25, 30 );
			SetResistance( ResistanceType.Tranchant, 25, 30 );
			SetResistance( ResistanceType.Perforant, 10, 20 );
			SetResistance( ResistanceType.Magie, 10, 20 );

			//SetSkill( SkillName.Anatomy, 125.0 );
			SetSkill( SkillName.ArmePerforante, 46.0, 77.5 );
			SetSkill( SkillName.ArmeContondante, 35.0, 57.5 );
			SetSkill( SkillName.Empoisonnement, 60.0, 82.5 );
			SetSkill( SkillName.Concentration, 83.5, 92.5 );
			SetSkill( SkillName.ArmeTranchante, 125.0 );
			SetSkill( SkillName.Tactiques, 125.0 );
			SetSkill( SkillName.Foresterie, 125.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 40;

			AddItem( new ThighBoots( Utility.RandomRedHue() ) ); 
			AddItem( new Surcoat( Utility.RandomRedHue() ) );    
			AddItem( new ExecutionersAxe());

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Meager );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public Executioner( Serial serial ) : base( serial ) 
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