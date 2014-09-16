using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DispelFieldScroll : SpellScroll
	{
		[Constructable]
		public DispelFieldScroll() : this( 1 )
		{
		}

		[Constructable]
		public DispelFieldScroll( int amount ) : base( 34, 0x1F4E, amount )
		{
            Name = "Adjuration: Champ de dissipation";
		}

		public DispelFieldScroll( Serial serial ) : base( serial )
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

            Name = "Adjuration: Champ de dissipation";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new DispelFieldScroll( amount ), amount );
		}*/
	}
}