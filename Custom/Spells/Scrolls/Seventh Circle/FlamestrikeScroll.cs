using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class FlamestrikeScroll : SpellScroll
	{
		[Constructable]
		public FlamestrikeScroll() : this( 1 )
		{
		}

		[Constructable]
		public FlamestrikeScroll( int amount ) : base( 51, 0x1F5F, amount )
		{
		}

		public FlamestrikeScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new FlamestrikeScroll( amount ), amount );
		}*/
	}
}