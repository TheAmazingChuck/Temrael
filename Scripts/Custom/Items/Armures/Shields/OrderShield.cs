using System;
using Server;
using Server.Guilds;

namespace Server.Items
{
	public class OrderShield : BaseShield
	{
        //public override int NiveauAttirail { get { return 3; } }

        public override int BasePhysicalResistance { get { return ShldOrder.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldOrder.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldOrder.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldOrder.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldOrder.resistance_Magique; } }

        public override int InitMinHits { get { return ShldOrder.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldOrder.max_Durabilite; } }

        public override int AosStrReq { get { return ShldOrder.force_Requise; } }
        public override int AosDexBonus { get { return ShldOrder.malus_Dex; } }

		public override int ArmorBase{ get{ return 30; } }

		[Constructable]
		public OrderShield() : base( 0x1BC4 )
		{
			Weight = 7.0;
            Name = "Bouclier d'Ordre";
		}

		public OrderShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 7.0;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}

		public override bool OnEquip( Mobile from )
		{
			return Validate( from ) && base.OnEquip( from );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( Validate( Parent as Mobile ) )
				base.OnSingleClick( from );
		}

		public virtual bool Validate( Mobile m )
		{
			if ( Core.AOS || m == null || !m.Player || m.AccessLevel != AccessLevel.Player )
				return true;

			Guild g = m.Guild as Guild;

			if ( g == null || g.Type != GuildType.Order )
			{
				m.FixedEffect( 0x3728, 10, 13 );
				Delete();

				return false;
			}

			return true;
		}
	}
}