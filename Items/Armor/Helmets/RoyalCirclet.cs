using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x2B6F, 0x3166 )]
	public class RoyalCirclet : BaseArmor
	{
		public override Race RequiredRace { get { return Race.Elf; } }

		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseContondantResistance{ get{ return 5; } }
		public override int BaseTranchantResistance{ get{ return 2; } }
		public override int BasePerforantResistance{ get{ return 2; } }
		public override int BaseMagieResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 35; } }

		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public RoyalCirclet() : base( 0x2B6F )
		{
			Weight = 2.0;
		}

		public RoyalCirclet( Serial serial ) : base( serial )
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