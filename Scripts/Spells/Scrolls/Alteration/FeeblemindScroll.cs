using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class FeeblemindScroll : SpellScroll
	{
		[Constructable]
		public FeeblemindScroll() : this( 1 )
		{
		}

		[Constructable]
		public FeeblemindScroll( int amount ) : base( FeeblemindSpell.m_SpellID, 0x1F30, amount )
		{
            Name = "Altération: Débilité";
		}

		public FeeblemindScroll( Serial serial ) : base( serial )
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

            Name = "Altération: Débilité";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new FeeblemindScroll( amount ), amount );
		}*/
	}
}