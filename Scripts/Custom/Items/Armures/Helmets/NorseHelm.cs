using System;
using Server;

namespace Server.Items
{
	public class NorseHelm : BaseArmor
	{
        public override int NiveauAttirail { get { return Ring_Niveau; } }

        public override int BasePhysicalResistance { get { return Ring_Physique; } }
        public override int BaseContondantResistance { get { return Ring_Contondant; } }
        public override int BaseTranchantResistance { get { return Ring_Tranchant; } }
        public override int BasePerforantResistance { get { return Ring_Perforant; } }
        public override int BaseMagieResistance { get { return Ring_Magique; } }

        public override int InitMinHits { get { return Ring_MinDurabilite; } }
        public override int InitMaxHits { get { return Ring_MaxDurabilite; } }

        public override int AosStrReq { get { return Ring_Force; } }
		public override int OldStrReq{ get{ return 40; } }

        public override int OldDexBonus { get { return Ring_Dex; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public NorseHelm() : base( 0x140E )
		{
			Weight = 5.0;
		}

		public NorseHelm( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 5.0;
		}
	}
}