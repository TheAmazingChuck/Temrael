﻿using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Multis;

namespace Server.Items
{
    // version 1.1.1 Bed coordinates of 0,0,0 will cause npc to sleep and wake at it's current location.
    // version 1.0 initial release.
    public class SleeperBedrollEWAddon : SleeperBaseAddon
    {
        public override BaseAddonDeed Deed
        {
            get
            {
                return new SleeperBedrollEWAddonDeed();
            }
        }

        public SleeperBedrollEWAddon(Serial serial)
            : base(serial)
        {
        }

        [Constructable]
        public SleeperBedrollEWAddon()
        {
            Visible = true;
            Name = "Sleeper";
            AddComponent(new SleeperBedrollEWPiece(this, 0xA55), 0, 0, 0);
            //AddComponent(new SleeperBedrollEWPiece(this, 0xA7C), 0, 1, 0);
            //AddComponent(new SleeperBedrollEWPiece(this, 0xA79), 1, 0, 0);
            //AddComponent(new SleeperBedrollEWPiece(this, 0xA78), 1, 1, 0);
        }

        public override void OnDoubleClick(Mobile from)
        {
            UseBed(from, new Point3D(this.Location.X + 1, this.Location.Y, this.Location.Z + 6), Direction.East);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            // don't read any serialization for old scripts, it's read in the base class
            if (OldStyleSleepers)
                return;

            int version = reader.ReadInt();
        }
    }

    public class SleeperBedrollEWAddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new SleeperBedrollEWAddon();
            }
        }

        [Constructable]
        public SleeperBedrollEWAddonDeed()
        {
            Name = "a small bedroll facing east deed";
            ItemID = 0xA58;
        }

        public SleeperBedrollEWAddonDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    // Eni - the below is necesary to be compatible with older versions of the script
    // also because of furniture dyability.
    [Furniture]
    public class SleeperBedrollEWPiece : SleeperPiece
    {
        public SleeperBedrollEWPiece(SleeperBaseAddon sleeper, int itemid)
            : base(sleeper, itemid)
        {
        }

        public SleeperBedrollEWPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); }
    }
}
