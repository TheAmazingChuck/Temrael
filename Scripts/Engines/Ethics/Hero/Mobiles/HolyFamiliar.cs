using System;
using Server;
using Server.Ethics;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a holy corpse" )]
	public class HolyFamiliar : BaseCreature
	{
		public override bool IsDispellable { get { return false; } }
		public override bool IsBondable { get { return false; } }

		[Constructable]
		public HolyFamiliar()
			: base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a silver wolf";
			Body = 100;
			BaseSoundID = 0xE5;

			SetStr( 96, 120 );
			SetDex( 81, 105 );
			SetInt( 36, 60 );

			SetHits( 58, 72 );
			SetMana( 0 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			
			SetResistance( ResistanceType.Magie, 10, 15 );

			SetSkill( SkillName.Concentration, 57.6, 75.0 );
			SetSkill( SkillName.Tactiques, 50.1, 70.0 );
			SetSkill( SkillName.Anatomie, 60.1, 80.0 );

			VirtualArmor = 22;

			Tamable = false;
			ControlSlots = 1;
		}

		public override int Meat { get { return 1; } }
		public override int Hides { get { return 7; } }
		public override FoodType FavoriteFood { get { return FoodType.Meat; } }
		public override PackInstinct PackInstinct { get { return PackInstinct.Canine; } }

		public HolyFamiliar( Serial serial )
			: base( serial )
		{
		}

		public override string ApplyNameSuffix( string suffix )
		{
			if ( suffix.Length == 0 )
				suffix = Ethic.Hero.Definition.Adjunct.String;
			else
				suffix = String.Concat( suffix, " ", Ethic.Hero.Definition.Adjunct.String );

			return base.ApplyNameSuffix( suffix );
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