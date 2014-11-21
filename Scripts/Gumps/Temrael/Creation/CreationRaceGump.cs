﻿using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using System.Reflection;
using Server.HuePickers;
using System.Collections.Generic;
using Server.Engines.Races;

namespace Server.Gumps.Creation
{
    public class CreationRaceGump : BaseCreationGump
    {
        private Race m_Race;

        public CreationRaceGump(PlayerMobile from)
            : this(from, from.Race)
        {
        }

        public CreationRaceGump(PlayerMobile from, Race race)
            : base(from, "Race", 560, 622, 1)
        {
            m_Race = race;

            int x = XBase;
            int y = YBase;
            int line = 0;
            int scale = 25;

            AddTitre(x + 360, y + line * scale, 190, "Races");
            ++line;
            for (int i = 1; i < 8; i++)
            {
                AddButton(x + 360, y + line * scale, 0x4b9, 0x4bA, i + 50, GumpButtonType.Reply, 0);
                AddHtmlTexte(x + 375, y + line * scale, DefaultHtmlLength, Race.GetRaceInstance(i).Name);
                ++line;
            }


            if (race != null)
            {
                int linetmp = line;

                line = 0;
                if (race.Image != -1)
                {
                    AddButton(x, y + line * scale, 8, race.Image);
                    AddTooltip(race.Tooltip);
                }

                line = linetmp;
                AddSection(x + 260, y + line * scale, 275, 170, race.Name, race.Description);
                line += 10;
                AddButton(470, 645, 52, 52, 8, GumpButtonType.Reply, 0);
                AddHtml(520, 645 + 12, 200, 20, "<h3><basefont color=#025a>Confirmer<basefont></h3>", false, false);

                line = 12;


            }
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            PlayerMobile from = (PlayerMobile)sender.Mobile;

            if (from.Deleted || !from.Alive)
                return;

            if (info.ButtonID < 8)
            {
                base.OnResponse(sender, info);
                return;
            }

            switch (info.ButtonID)
            {
                case 8:
                    DeleteItemsOnChar(from);
                    from.Race = m_Race;
                    //from.Hue = m_Race.Hues[0];
                    break;
            }

            if (info.ButtonID >= 50)
            {
                from.SendGump(new CreationRaceGump(from, Race.GetRaceInstance(info.ButtonID - 50)));
            }
        }
        public void DeleteItemsOnChar(PlayerMobile from)
        {
            if (from.Backpack != null)
            {
                while (from.Backpack.Items.Count > 0)
                    ((Item)from.Backpack.Items[0]).Delete();
            }

            Item item;

            item = from.FindItemOnLayer(Layer.Arms);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Bracelet);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Cloak);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Earrings);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Gloves);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Helm);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.InnerLegs);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.InnerTorso);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.MiddleTorso);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Neck);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.OuterLegs);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.OuterTorso);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Pants);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Ring);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Shirt);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Shoes);
            if (item != null)
                item.Delete();
            item = from.FindItemOnLayer(Layer.Waist);
            if (item != null)
                item.Delete();
        }
    }
}
