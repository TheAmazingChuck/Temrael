using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class EnergyBoltScroll : SpellScroll
	{
		[Constructable]
		public EnergyBoltScroll() : this( 1 )
		{
		}

		[Constructable]
		public EnergyBoltScroll( int amount ) : base( EnergyBoltSpell.spellID, 0x1F56, amount )
		{
            Name = "�vocation: �nergie";
		}

		public EnergyBoltScroll( Serial serial ) : base( serial )
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

            Name = "�vocation: �nergie";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new EnergyBoltScroll( amount ), amount );
		}*/
	}
}