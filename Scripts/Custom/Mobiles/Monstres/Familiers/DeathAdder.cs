using System;
using Server;

namespace Server.Mobiles
{
	[CorpseName( "a death adder corpse" )]
	public class DeathAdder : BaseFamiliar
	{
		public DeathAdder()
		{
			Name = "a death adder";
			Body = 21;
			Hue = 0x455;
			BaseSoundID = 219;

			SetStr( 70 );
			SetDex( 150 );
			SetInt( 100 );

			SetHits( 50 );
			SetStam( 150 );
			SetMana( 0 );

			SetDamage( 1, 4 );
			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10 );
			SetResistance( ResistanceType.Perforant, 100 );

			SetSkill( SkillName.ArmePoing, 90.0 );
			SetSkill( SkillName.Tactiques, 50.0 );
			SetSkill( SkillName.Concentration, 100.0 );
			SetSkill( SkillName.Empoisonner, 150.0 );

			ControlSlots = 1;
		}

		public override Poison HitPoison{ get{ return (0.8 >= Utility.RandomDouble() ? Poison.Greater : Poison.Deadly); } }

		public DeathAdder( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}