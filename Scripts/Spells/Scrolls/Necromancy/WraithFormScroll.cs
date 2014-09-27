using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class WraithFormScroll : SpellScroll
	{
		[Constructable]
		public WraithFormScroll() : this( 1 )
		{
		}

		[Constructable]
		public WraithFormScroll( int amount ) : base( WraithFormSpell.m_SpellID, 0x226F, amount )
		{
            Name = "N�cromancie: Spectre";
		}

		public WraithFormScroll( Serial serial ) : base( serial )
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

            Name = "N�cromancie: Spectre";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new WraithFormScroll( amount ), amount );
		}*/
	}
}