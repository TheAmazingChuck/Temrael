using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xF5C, 0xF5D )]
	public class Mace : BaseBashing
	{
        //public override int NiveauAttirail { get { return 1; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

        public override int DefStrengthReq { get { return Masse_Force1; } }
        public override int DefMinDamage { get { return Masse_MinDam1; } }
        public override int DefMaxDamage { get { return Masse_MaxDam1; } }
        public override int DefSpeed { get { return Masse_Vitesse; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		[Constructable]
		public Mace() : base( 0xF5C )
		{
			Weight = 14.0;
            Name = "Mace";
		}

		public Mace( Serial serial ) : base( serial )
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