﻿using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Corps de Balron Ancien")]
    public class BalronAncien : BaseCreature
    {
        public override bool isBoss
        {
            get
            {
                return true;
            }
        }

        [Constructable]
        public BalronAncien()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Balron Ancien";
            Body = 27;
            BaseSoundID = 357;

            SetStr(986, 1185);
            SetDex(177, 255);
            SetInt(151, 250);

            SetHits(1000, 2000);

            SetDamage(40, 80);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Contondant, 25);
            SetDamageType(ResistanceType.Magie, 25);

            SetResistance(ResistanceType.Physical, 50, 70);
            SetResistance(ResistanceType.Contondant, 50, 70);
            SetResistance(ResistanceType.Tranchant, 50, 70);
            SetResistance(ResistanceType.Perforant, 50, 70);
            SetResistance(ResistanceType.Magie, 50, 70);

            //SetSkill( SkillName.Anatomy, 25.1, 50.0 );
            //SetSkill( SkillName.EvalInt, 90.1, 100.0 );
            SetSkill(SkillName.ArtMagique, 95.5, 100.0);
            SetSkill(SkillName.Concentration, 25.1, 50.0);
            SetSkill(SkillName.Concentration, 100.5, 150.0);
            SetSkill(SkillName.Tactiques, 90.1, 100.0);
            SetSkill(SkillName.ArmePoing, 90.1, 100.0);

            Fame = 24000;
            Karma = -24000;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich);
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override bool AlwaysMurderer { get { return true; } }
        public override double AttackSpeed { get { return 2.5; } }
        public override bool CanRummageCorpses { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }
        //public override int TreasureMapLevel { get { return 5; } }
        public override int Meat { get { return 1; } }
        public override int Bones { get { return 12; } }
        public override int Hides { get { return 15; } }
        public override HideType HideType { get { return HideType.Ancien; } }
        public override BoneType BoneType { get { return BoneType.Balron; } }

        public BalronAncien(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}