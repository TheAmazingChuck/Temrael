using System; 
using Server.Items; 

namespace Server.Mobiles 
{ 
    
	public class SpectralArmour : BaseCreature 
	{ 
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		[Constructable] 
		public SpectralArmour() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Body = 637; 
			Hue = 0x8026; 
			Name = "spectral armour"; 

			Buckler buckler = new Buckler();
			ChainCoif coif = new ChainCoif();
			PlateGloves gloves = new PlateGloves();

			buckler.Hue = 0x835; buckler.Movable = false;
			coif.Hue = 0x835;
			gloves.Hue = 0x835;

			AddItem( buckler );
			AddItem( coif );
			AddItem( gloves );

			SetStr( 101, 110 ); 
			SetDex( 101, 110 ); 
			SetInt( 101, 110 );

			SetHits( 178, 201 );
			SetStam( 191, 200 );

			SetDamage( 10, 22 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Tranchant, 25 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Contondant, 20, 30 );
			SetResistance( ResistanceType.Tranchant, 30, 40 );
			SetResistance( ResistanceType.Perforant, 20, 30 );
			SetResistance( ResistanceType.Magie, 20, 30 );

			SetSkill( SkillName.Anatomie, 75.1, 100.0 ); 
			SetSkill( SkillName.Tactiques, 90.1, 100.0 ); 
			SetSkill( SkillName.Concentration, 90.1, 100 ); 

			VirtualArmor = 40; 
           
		}

		public override int GetIdleSound()
		{
			return 0x200;
		}

		public override int GetAngerSound()
		{
			return 0x56;
		}

		public override bool OnBeforeDeath()
		{
			if ( !base.OnBeforeDeath() )
				return false;

			Gold gold = new Gold( Utility.RandomMinMax( 240, 375 ) );
			gold.MoveToWorld( Location, Map );

			Effects.SendLocationEffect( Location, Map, 0x376A, 10, 1 );
			return true;
		}

		public override Poison PoisonImmune{ get{ return Poison.Regular; } }

		public SpectralArmour( Serial serial ) : base( serial ) 
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