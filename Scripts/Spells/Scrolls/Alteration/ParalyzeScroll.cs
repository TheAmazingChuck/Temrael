using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ParalyzeScroll : SpellScroll
	{
		[Constructable]
		public ParalyzeScroll() : this( 1 )
		{
		}

		[Constructable]
		public ParalyzeScroll( int amount ) : base( 38, 0x1F52, amount )
		{
            Name = "Alt�ration: Paralysie";
		}
		
		public ParalyzeScroll( Serial serial ) : base( serial )
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

            Name = "Alt�ration: Paralysie";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new ParalyzeScroll( amount ), amount );
		}*/
	}
}