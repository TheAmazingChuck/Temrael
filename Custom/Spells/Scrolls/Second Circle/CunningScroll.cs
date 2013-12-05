using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CunningScroll : SpellScroll
	{
		[Constructable]
		public CunningScroll() : this( 1 )
		{
		}

		[Constructable]
		public CunningScroll( int amount ) : base( 10, 0x1F36, amount )
		{
		}

		public CunningScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new CunningScroll( amount ), amount );
		}*/
	}
}