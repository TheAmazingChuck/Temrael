﻿using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class TempeteScroll : SpellScroll
    {
        [Constructable]
        public TempeteScroll()
            : this(1)
        {
        }

        [Constructable]
        public TempeteScroll(int amount)
            : base(204, 0x1F65, amount)
        {
        }

        public TempeteScroll(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}