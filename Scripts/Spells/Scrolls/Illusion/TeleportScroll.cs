using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class TeleportScroll : SpellScroll
	{
		[Constructable]
		public TeleportScroll() : this( 1 )
		{
		}

		[Constructable]
		public TeleportScroll( int amount ) : base( TeleportSpell.spellID, 0x1F42, amount )
		{
            Name = "Illusion: T�l�portation";
		}

		public TeleportScroll( Serial serial ) : base( serial )
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

            Name = "Illusion: T�l�portation";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new TeleportScroll( amount ), amount );
		}*/
	}
}