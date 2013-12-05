using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x1451, 0x1456 )]
	public class BoneHelm : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseContondantResistance{ get{ return 3; } }
		public override int BaseTranchantResistance{ get{ return 4; } }
		public override int BasePerforantResistance{ get{ return 2; } }
		public override int BaseMagieResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 25; } }
		public override int InitMaxHits{ get{ return 30; } }

		public override int AosStrReq{ get{ return 20; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public BoneHelm() : base( 0x1451 )
		{
			Weight = 3.0;
		}

		public BoneHelm( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			if ( Weight == 1.0 )
				Weight = 3.0;
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}