using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class HorrificBeastScroll : SpellScroll
	{
		[Constructable]
		public HorrificBeastScroll() : this( 1 )
		{
		}

		[Constructable]
		public HorrificBeastScroll( int amount ) : base( HorrificBeastSpell.m_SpellID, 0x2265, amount )
		{
            Name = "Alteration: Bête Horrifique";
		}

		public HorrificBeastScroll( Serial serial ) : base( serial )
		{
		}
	}
}