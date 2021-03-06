﻿using System;
using Server;

namespace Server.Items
{
    public class ManaPotion : BaseManaPotion
    {
        public override double Mana { get { return 0.3; } }

        [Constructable]
        public ManaPotion()
            : base(PotionEffect.Refresh)
        {
            Name = "Potion de mana";
        }

        public ManaPotion(Serial serial)
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