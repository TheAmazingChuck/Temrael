﻿using System;
using Server;

namespace Server.Items
{
    public class TotalManaPotion : BaseManaPotion
    {
        public override double Mana { get { return 1.0; } }

        [Constructable]
        public TotalManaPotion()
            : base(PotionEffect.Refresh)
        {
            Name = "Grande potion de mana";
        }

        public TotalManaPotion(Serial serial)
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