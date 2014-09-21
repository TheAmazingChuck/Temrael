﻿using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    class PaysanNomadeGarde : BaseGardes
    {
        [Constructable]
        public PaysanNomadeGarde(GeoController geo, ArmyController army) : base (geo, army)
        {
            IsGuarding = false;

            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(new ShortPants(Utility.RandomNeutralHue()));
            }

            Title = "Soldat";
            Item hair = new Item(Utility.RandomList(0x203B, 0x2049, 0x2048, 0x204A));
            hair.Hue = Utility.RandomNeutralHue();
            hair.Layer = Layer.Hair;
            hair.Movable = false;
            AddItem(hair);

            if (Utility.RandomBool() && !this.Female)
            {
                Item beard = new Item(Utility.RandomList(0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D));

                beard.Hue = hair.Hue;
                beard.Layer = Layer.FacialHair;
                beard.Movable = false;

                AddItem(beard);
            }

            SetStr(91, 91);
            SetDex(91, 91);
            SetInt(50, 50);

            SetDamage(7, 14);

            SetSkill(SkillName.Tactiques, 36, 67);
            SetSkill(SkillName.ArtMagique, 22, 22);
            SetSkill(SkillName.ArmeTranchante, 64, 100);
            SetSkill(SkillName.Parer, 60, 82);
            SetSkill(SkillName.ArmeContondante, 36, 67);
            //SetSkill(SkillName.Focus, 36, 67);
            SetSkill(SkillName.Anatomie, 25, 47);

            AddItem(new Shoes(Utility.RandomNeutralHue()));
            AddItem(new Shirt());

            // Pick a random sword
            switch (Utility.Random(3))
            {
                case 0: AddItem(new Longsword()); break;
                case 2: AddItem(new VikingSword()); break;
                case 1: AddItem(new TwoHandedAxe()); break;
            }

            // Pick a random shield
            switch (Utility.Random(8))
            {
                case 0: AddItem(new BronzeShield()); break;
                case 1: AddItem(new HeaterShield()); break;
                case 2: AddItem(new MetalKiteShield()); break;
                case 3: AddItem(new MetalShield()); break;
                case 4: AddItem(new WoodenKiteShield()); break;
                case 5: AddItem(new WoodenShield()); break;
                case 6: AddItem(new OrderShield()); break;
                case 7: AddItem(new ChaosShield()); break;
            }

            switch (Utility.Random(6))
            {
                case 0: break;
                case 1: AddItem(new Bascinet()); break;
                case 2: AddItem(new CloseHelm()); break;
                case 3: AddItem(new NorseHelm()); break;
                case 4: AddItem(new Helmet()); break;
                case 5: AddItem(new CasqueSudiste()); break;
            }
            // Pick some armour
            switch (Utility.Random(4))
            {
                case 0: // Leather
                    AddItem(new LeatherChest());
                    AddItem(new LeatherArms());
                    AddItem(new LeatherGloves());
                    AddItem(new LeatherGorget());
                    AddItem(new LeatherLegs());
                    break;

                case 1: // Studded Leather
                    AddItem(new StuddedChest());
                    AddItem(new StuddedArms());
                    AddItem(new StuddedGloves());
                    AddItem(new StuddedGorget());
                    AddItem(new StuddedLegs());
                    break;

                case 2: // Ringmail
                    AddItem(new RingmailChest());
                    AddItem(new RingmailArms());
                    AddItem(new RingmailGloves());
                    AddItem(new RingmailLegs());
                    break;

                case 3: // Chain
                    AddItem(new ChainChest());
                    AddItem(new ChainCoif());
                    AddItem(new ChainLegs());
                    break;
            }

            PackGold(25, 100);
        }
        public PaysanNomadeGarde(Serial serial)
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
