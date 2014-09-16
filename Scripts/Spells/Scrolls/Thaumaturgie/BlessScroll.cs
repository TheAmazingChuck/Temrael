using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BlessScroll : SpellScroll
	{
		[Constructable]
		public BlessScroll() : this( 1 )
		{
		}

		[Constructable]
		public BlessScroll( int amount ) : base( 17, 0x1F3D, amount )
		{
            Name = "Thaumaturgie: Puissance";
		}

		public BlessScroll( Serial serial ) : base( serial )
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

            Name = "Thaumaturgie: Puissance";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new BlessScroll( amount ), amount );
		}*/
	}
}