using System;
using Server;

namespace Server.Items
{
	public class Buckler : BaseShield
	{
        public override int NiveauAttirail { get { return 1; } }

        public override int BasePhysicalResistance { get { return Bouclier_Def1; } }
        public override int BaseContondantResistance { get { return Bouclier_Def1; } }
        public override int BaseTranchantResistance { get { return Bouclier_Def1; } }
        public override int BasePerforantResistance { get { return Bouclier_Def1; } }
        public override int BaseMagieResistance { get { return Bouclier_Def1; } }

        public override int InitMinHits { get { return Bouclier_MinDurabilite1; } }
        public override int InitMaxHits { get { return Bouclier_MaxDurabilite1; } }

        public override int AosStrReq { get { return Bouclier_Force1; } }

		public override int ArmorBase{ get{ return 7; } }

		[Constructable]
		public Buckler() : base( 0x1B73 )
		{
			Weight = 5.0;
		}

		public Buckler( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}