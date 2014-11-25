﻿using Server.Engines.Races;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles.Vendeurs
{
    public class Eclaireur : BaseVendor
    {
        [Constructable]
        public Eclaireur()
            : base("Tavernier")
        {
            Name = "Norim Barbe D'acier";
        }


        public Eclaireur(Serial serial)
            : base(serial)
        {
        }

        public override void InitBody()
        {
            SpeechHue = Utility.RandomDyedHue();
            NameHue = 0x35;
            Body = 0x190; //male

            SetRace(new Nain(1054));
            HairItemID = 10204;
            HairHue = 1149;
            FacialHairItemID = 10302;
            FacialHairHue = 1149;
        }

        public override void InitOutfit()
        {
            AddItem(new Bonnet(1881));
            AddItem(new FullApron(1881));
            AddItem(new TuniqueBourgeoise(1881));
            AddItem(new CeintureBourse(0));
            AddItem(new PantalonsNordique(1130));
            AddItem(new GoldRing(0));
            AddItem(new Shoes(1846));


        }

        private List<SBInfo> m_SBInfos = new List<SBInfo>();
        protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBEclaireur());
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

    public class SBEclaireur : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBEclaireur()
        {
        }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                Add(new BeverageBuyInfo(typeof(GlassMug), BeverageType.Water, 3, 20, 0x1F91, 0)); // 3
                Add(new BeverageBuyInfo(typeof(Pitcher), BeverageType.Ale, 21, 20, 0x1F95, 0)); // 21  
                Add(new BeverageBuyInfo(typeof(Jug), BeverageType.Cider, 9, 20, 0x9C8, 0)); // 9  
                Add(new GenericBuyInfo(typeof(Dices), 20, 0xFA7, 0)); // 6
                Add(new GenericBuyInfo(typeof(Candle), 20, 0x2600, 0)); // 6
                Add(new GenericBuyInfo(typeof(BreadLoaf), 20, 0x103B, 0)); // 3
                Add(new GenericBuyInfo(typeof(CheeseWheel), 20, 0x97E, 0)); // 6
                Add(new GenericBuyInfo(typeof(CookedBird), 20, 0x9B7, 0)); // 3
                Add(new GenericBuyInfo(typeof(WoodenBowlOfStew), 20, 0x1604, 0)); // 6
                Add(new GenericBuyInfo(typeof(Bag), 20, 0xE76, 0)); // 6
                Add(new GenericBuyInfo(typeof(Torch), 20, 0x3947, 0)); // 6
                Add(new GenericBuyInfo(typeof(RedBook), 20, 0xFF1, 0)); // 6
                Add(new GenericBuyInfo(typeof(Bandage), 50, 0xE21, 0)); // 3
                Add(new GenericBuyInfo(typeof(Sextant), 20, 0x1058, 0)); // 21
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
            }
        }
    }
}