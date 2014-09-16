using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class WeakenScroll : SpellScroll
	{
		[Constructable]
		public WeakenScroll() : this( 1 )
		{
		}

		[Constructable]
		public WeakenScroll( int amount ) : base( WeakenSpell.spellID, 0x1F34, amount )
		{
            Name = "Alt�ration: Faiblesse";
		}

		public WeakenScroll( Serial serial ) : base( serial )
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

            Name = "Alt�ration: Faiblesse";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new WeakenScroll( amount ), amount );
		}*/
	}
}