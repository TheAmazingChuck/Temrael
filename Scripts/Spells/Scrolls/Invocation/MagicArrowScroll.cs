using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class MagicArrowScroll : SpellScroll
	{
		[Constructable]
		public MagicArrowScroll() : this( 1 )
		{
		}

		[Constructable]
		public MagicArrowScroll( int amount ) : base( MagicArrowSpell.spellID, 0x1F32, amount )
		{
            Name = "Invocation: Fl�che Magique";
		}
		
		public MagicArrowScroll( Serial serial ) : base( serial )
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

            Name = "Invocation: Fl�che Magique";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new MagicArrowScroll( amount ), amount );
		}*/
	}
}