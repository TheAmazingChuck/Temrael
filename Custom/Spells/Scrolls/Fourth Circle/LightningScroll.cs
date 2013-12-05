using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class LightningScroll : SpellScroll
	{
		[Constructable]
		public LightningScroll() : this( 1 )
		{
		}

		[Constructable]
		public LightningScroll( int amount ) : base( 30, 0x1F4A, amount )
		{
		}

		public LightningScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new LightningScroll( amount ), amount );
		}*/
	}
}