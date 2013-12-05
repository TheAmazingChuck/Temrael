using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ManaDrainScroll : SpellScroll
	{
		[Constructable]
		public ManaDrainScroll() : this( 1 )
		{
		}

		[Constructable]
		public ManaDrainScroll( int amount ) : base( 31, 0x1F4B, amount )
		{
		}

		public ManaDrainScroll( Serial serial ) : base( serial )
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

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new ManaDrainScroll( amount ), amount );
		}*/
	}
}