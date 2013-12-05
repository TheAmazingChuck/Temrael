using System;
using Server;
using Server.Items;

namespace Server.Factions
{
	public class FactionHenchman : BaseFactionGuard
	{
		public override GuardAI GuardAI{ get{ return GuardAI.Melee; } }

		[Constructable]
		public FactionHenchman() : base( "the henchman" )
		{
			GenerateBody( false, true );

			SetStr( 91, 115 );
			SetDex( 61, 85 );
			SetInt( 81, 95 );

			SetDamage( 10, 14 );

			SetResistance( ResistanceType.Physical, 10, 30 );
			SetResistance( ResistanceType.Contondant, 10, 30 );
			SetResistance( ResistanceType.Tranchant, 10, 30 );
			SetResistance( ResistanceType.Magie, 10, 30 );
			SetResistance( ResistanceType.Perforant, 10, 30 );

			VirtualArmor = 8;

			SetSkill( SkillName.ArmePerforante, 80.0, 90.0 );
			SetSkill( SkillName.ArmePoing, 80.0, 90.0 );
			SetSkill( SkillName.Tactiques, 80.0, 90.0 );
			SetSkill( SkillName.Concentration, 80.0, 90.0 );
			SetSkill( SkillName.Soins, 80.0, 90.0 );
			//SetSkill( SkillName.Anatomy, 80.0, 90.0 );

			AddItem( new StuddedChest() );
			AddItem( new StuddedLegs() );
			AddItem( new StuddedArms() );
			AddItem( new StuddedGloves() );
			AddItem( new StuddedGorget() );
			AddItem( new Boots() );
			AddItem( Newbied( new Spear() ) );

			PackItem( new Bandage( Utility.RandomMinMax( 10, 20 ) ) );
			PackWeakPotions( 1, 4 );
		}

		public FactionHenchman( Serial serial ) : base( serial )
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