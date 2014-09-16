using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class ExplosionScroll : SpellScroll
	{
		[Constructable]
		public ExplosionScroll() : this( 1 )
		{
		}

		[Constructable]
		public ExplosionScroll( int amount ) : base( ExplosionSpell.spellID, 0x1F57, amount )
		{
            Name = "�vocation: Explosion";
		}

		public ExplosionScroll( Serial serial ) : base( serial )
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

            Name = "�vocation: Explosion";
		}

		/*public override Item Dupe( int amount )
		{
			return base.Dupe( new ExplosionScroll( amount ), amount );
		}*/
	}
}