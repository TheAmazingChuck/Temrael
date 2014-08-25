﻿using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class BouclierDecorer : BaseShield
    {
        //public override int NiveauAttirail { get { return 6; } }

        public override int BasePhysicalResistance { get { return ShldDecorer.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldDecorer.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldDecorer.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldDecorer.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldDecorer.resistance_Magique; } }

        public override int InitMinHits { get { return ShldDecorer.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldDecorer.max_Durabilite; } }

        public override int AosStrReq { get { return ShldDecorer.force_Requise; } }
        public override int AosDexBonus { get { return ShldDecorer.malus_Dex; } }

        public override int ArmorBase { get { return 30; } }

        [Constructable]
        public BouclierDecorer()
            : base(0x2883)
        {
            Weight = 2.0;
            Name = "Bouclier Decoré";
        }

        public BouclierDecorer(Serial serial)
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
    
    public class BouclierElfique : BaseShield
    {
        //public override int NiveauAttirail { get { return 5; } }

        public override int BasePhysicalResistance { get { return ShldElfique.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldElfique.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldElfique.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldElfique.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldElfique.resistance_Magique; } }

        public override int InitMinHits { get { return ShldElfique.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldElfique.max_Durabilite; } }

        public override int AosStrReq { get { return ShldElfique.force_Requise; } }
        public override int AosDexBonus { get { return ShldElfique.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierElfique()
            : base(0x289E)
        {
            Weight = 6.0;
            Name = "Bouclier Elfique";
        }

        public BouclierElfique(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
    public class BouclierCuir : BaseShield
    {
        //public override int NiveauAttirail { get { return 1; } }

        public override int BasePhysicalResistance { get { return ShldCuir.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldCuir.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldCuir.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldCuir.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldCuir.resistance_Magique; } }

        public override int InitMinHits { get { return ShldCuir.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldCuir.max_Durabilite; } }

        public override int AosStrReq { get { return ShldCuir.force_Requise; } }
        public override int AosDexBonus { get { return ShldCuir.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierCuir()
            : base(0x2A41)
        {
            Weight = 6.0;
            Name = "Bouclier de Cuir";
        }

        public BouclierCuir(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
    public class BouclierNordique : BaseShield
    {
        //public override int NiveauAttirail { get { return 5; } }

        public override int BasePhysicalResistance { get { return ShldNordique.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldNordique.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldNordique.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldNordique.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldNordique.resistance_Magique; } }

        public override int InitMinHits { get { return ShldNordique.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldNordique.max_Durabilite; } }

        public override int AosStrReq { get { return ShldNordique.force_Requise; } }
        public override int AosDexBonus { get { return ShldNordique.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierNordique()
            : base(0x2A42)
        {
            Weight = 6.0;
            Name = "Bouclier Nordique";
        }

        public BouclierNordique(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
    public class BouclierChevaleresque : BaseShield
    {
        //public override int NiveauAttirail { get { return 5; } }

        public override int BasePhysicalResistance { get { return ShldChevalier.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldChevalier.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldChevalier.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldChevalier.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldChevalier.resistance_Magique; } }

        public override int InitMinHits { get { return ShldChevalier.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldChevalier.max_Durabilite; } }

        public override int AosStrReq { get { return ShldChevalier.force_Requise; } }
        public override int AosDexBonus { get { return ShldChevalier.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierChevaleresque()
            : base(0x2A43)
        {
            Weight = 6.0;
            Name = "Bouclier Chevaleresque";
        }

        public BouclierChevaleresque(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
    public class BouclierVieux : BaseShield
    {
        //public override int NiveauAttirail { get { return 6; } }

        public override int BasePhysicalResistance { get { return ShldVieux.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldVieux.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldVieux.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldVieux.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldVieux.resistance_Magique; } }

        public override int InitMinHits { get { return ShldVieux.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldVieux.max_Durabilite; } }

        public override int AosStrReq { get { return ShldVieux.force_Requise; } }
        public override int AosDexBonus { get { return ShldVieux.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierVieux()
            : base(0x2A44)
        {
            Weight = 6.0;
            Name = "Vieux Bouclier";
        }

        public BouclierVieux(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
    public class BouclierComte : BaseShield
    {
        //public override int NiveauAttirail { get { return 4; } }

        public override int BasePhysicalResistance { get { return ShldComte.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldComte.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldComte.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldComte.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldComte.resistance_Magique; } }

        public override int InitMinHits { get { return ShldComte.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldComte.max_Durabilite; } }

        public override int AosStrReq { get { return ShldComte.force_Requise; } }
        public override int AosDexBonus { get { return ShldComte.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierComte()
            : base(0x2A45)
        {
            Weight = 6.0;
            Name = "Bouclier de Karmilide";
        }

        public BouclierComte(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
    public class BouclierMarquis : BaseShield
    {
        //public override int NiveauAttirail { get { return 4; } }

        public override int BasePhysicalResistance { get { return ShldMarquis.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldMarquis.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldMarquis.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldMarquis.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldMarquis.resistance_Magique; } }

        public override int InitMinHits { get { return ShldMarquis.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldMarquis.max_Durabilite; } }

        public override int AosStrReq { get { return ShldMarquis.force_Requise; } }
        public override int AosStrReq { get { return ShldMarquis.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierMarquis()
            : base(0x2A46)
        {
            Weight = 6.0;
            Name = "Bouclier de Faréligue";
        }

        public BouclierMarquis(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
    public class BouclierDuc : BaseShield
    {
        //public override int NiveauAttirail { get { return 4; } }

        public override int BasePhysicalResistance { get { return ShldDuc.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldDuc.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldDuc.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldDuc.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldDuc.resistance_Magique; } }

        public override int InitMinHits { get { return ShldDuc.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldDuc.max_Durabilite; } }

        public override int AosStrReq { get { return ShldDuc.force_Requise; } }
        public override int AosDexBonus { get { return ShldDuc.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierDuc()
            : base(0x2A47)
        {
            Weight = 6.0;
            Name = "Bouclier d'Horlé";
        }

        public BouclierDuc(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
    public class BouclierPavoisNoir : BaseShield
    {
        //public override int NiveauAttirail { get { return 6; } }

        public override int BasePhysicalResistance { get { return ShldPavoisBlk.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldPavoisBlk.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldPavoisBlk.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldPavoisBlk.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldPavoisBlk.resistance_Magique; } }

        public override int InitMinHits { get { return ShldPavoisBlk.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldPavoisBlk.max_Durabilite; } }

        public override int AosStrReq { get { return ShldPavoisBlk.force_Requise; } }
        public override int AosDexBonus { get { return ShldPavoisBlk.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierPavoisNoir()
            : base(0x2A48)
        {
            Weight = 6.0;
            Name = "Pavois Royal";
        }

        public BouclierPavoisNoir(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
    public class BouclierGarde : BaseShield
    {
        //public override int NiveauAttirail { get { return 3; } }

        public override int BasePhysicalResistance { get { return ShldGarde.resistance_Physique; } }
        public override int BaseContondantResistance { get { return ShldGarde.resistance_Contondant; } }
        public override int BaseTranchantResistance { get { return ShldGarde.resistance_Tranchant; } }
        public override int BasePerforantResistance { get { return ShldGarde.resistance_Perforant; } }
        public override int BaseMagieResistance { get { return ShldGarde.resistance_Magique; } }

        public override int InitMinHits { get { return ShldGarde.min_Durabilite; } }
        public override int InitMaxHits { get { return ShldGarde.max_Durabilite; } }

        public override int AosStrReq { get { return ShldGarde.force_Requise; } }
        public override int AosDexBonus { get { return ShldGarde.malus_Dex; } }

        public override int ArmorBase { get { return 10; } }

        [Constructable]
        public BouclierGarde()
            : base(0x2A49)
        {
            Weight = 6.0;
            Name = "Bouclier Métallique";
        }

        public BouclierGarde(Serial serial)
            : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);//version
        }
    }
}
