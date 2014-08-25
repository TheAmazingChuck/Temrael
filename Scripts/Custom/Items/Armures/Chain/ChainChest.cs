using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13bf, 0x13c4 )]
	public class ChainChest : BaseArmor
	{
        //public override int NiveauAttirail { get { return Chain_Niveau; } }

        public override int BasePhysicalResistance { get { return ArmorChain.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ArmorChain.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ArmorChain.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ArmorChain.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ArmorChain.resistance_Magique; } }

        public override int InitMinHits { get { return ArmorChain.min_Durabilite; } }
        public override int InitMaxHits { get { return ArmorChain.max_Durabilite; } }

        public override int AosStrReq { get { return ArmorChain.force_Requise; } }
        public override int AosDexBonus { get { return ArmorChain.malus_Dex; } }

		public override int ArmorBase{ get{ return 28; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Chainmail; } }

		[Constructable]
		public ChainChest() : base( 0x13BF )
		{
			Weight = 7.0;
		}

		public ChainChest( Serial serial ) : base( serial )
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
		}
	}
}