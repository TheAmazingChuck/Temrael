﻿using System;
using Server;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
    [CorpseName("Corps de Diable")]
    public class Devil : BaseCreature
    {
        public override double DispelDifficulty { get { return 125.0; } }
        public override double DispelFocus { get { return 45.0; } }

        public override Faction FactionAllegiance { get { return Shadowlords.Instance; } }
        public override Ethics.Ethic EthicAllegiance { get { return Ethics.Ethic.Evil; } }

        [Constructable]
        public Devil()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Diable";
            Body = 138;
            BaseSoundID = 357;

            SetStr(476, 505);
            SetDex(76, 95);
            SetInt(301, 325);

            SetHits(400, 750);

            SetDamage(15, 30);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20, 40);
            SetResistance(ResistanceType.Contondant, 20, 40);
            SetResistance(ResistanceType.Tranchant, 20, 40);
            SetResistance(ResistanceType.Perforant, 20, 40);
            SetResistance(ResistanceType.Magie, 20, 40);

            //SetSkill( SkillName.EvalInt, 70.1, 80.0 );
            SetSkill(SkillName.ArtMagique, 70.1, 80.0);
            SetSkill(SkillName.Concentration, 85.1, 95.0);
            SetSkill(SkillName.Tactiques, 70.1, 80.0);
            SetSkill(SkillName.ArmePoing, 60.1, 80.0);

            Fame = 15000;
            Karma = -15000;

            ControlSlots = Core.SE ? 3 : 4;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override bool AlwaysMurderer { get { return true; } }
        public override double AttackSpeed { get { return 2.5; } }
        public override bool CanRummageCorpses { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Regular; } }
        //public override int TreasureMapLevel { get { return 4; } }
        public override int Meat { get { return 1; } }
        public override int Bones { get { return 4; } }
        public override int Hides { get { return 6; } }
        public override HideType HideType { get { return HideType.Demoniaque; } }
        public override BoneType BoneType { get { return BoneType.Demon; } }

        public Devil(Serial serial)
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
