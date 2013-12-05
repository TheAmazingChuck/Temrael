using System;
using Server;

namespace Server.Items
{
	public class JackalsCollar : PlateGorget
	{
		public override int LabelNumber{ get{ return 1061594; } } // Jackal's Collar
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BaseContondantResistance{ get{ return 23; } }
		public override int BaseTranchantResistance{ get{ return 17; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public JackalsCollar()
		{
			Hue = 0x6D1;
			Attributes.BonusDex = 15;
			Attributes.RegenHits = 2;
		}

		public JackalsCollar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 )
			{
				if ( Hue == 0x54B )
					Hue = 0x6D1;

				ContondantBonus = 0;
				TranchantBonus = 0;
			}
		}
	}
}