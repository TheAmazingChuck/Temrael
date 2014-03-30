﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Server.Items;
using Server.Network;
using Server.DataStructures;


namespace Server.Systemes.Geopolitique
{
    /* Plan: 
     *  - Gump pour ajouter des fonds et des employes (accessible par les gms)
     *  - Gump pour gerer son argent et le retirer
     *  - Gump pour la geopol
     *  - - Ajouter une tresorerie qui recoit des fonds automatiquement 
     *  - - Set up du owner
     *  - - Batisseurs peuvent retirer des fonds pour une construction
     *  - - Log pour les modifications faites.
     *  S'assurer que les joueurs ne voient que les noms qu'ils connaissent.
     */

    public class Tresorier : Mobile
    {
        private Mobile m_Gestionnaire; // Joueur qui controle la tresorerie
        private string m_NomGestionnaire; // Nom utilise par le tresorier pour le personnage
        private Terre m_Terre; // null si pas lie a une terre 
        private string m_Etablissement; // Nom de la tresorerie
        private string m_Description; // Description dans le menu geopol
        private int m_Fonds; // Total des fonds accumules
        private List<string> m_Messages;
        
        private OrderedDictionary<Mobile, Employe> m_Employes; //Liste d'employes a payer
        private Timer m_Paiement; // Timer pour effectuer un paiement

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Gestionnaire { get { return m_Gestionnaire; } set { m_Gestionnaire = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public string NomGestionnaire { get { return m_NomGestionnaire; } set { m_NomGestionnaire = value; } }
        public Terre Terre { get { return m_Terre; } set { m_Terre = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public string Etablissement { get { return m_Etablissement; } set { m_Etablissement = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public string Description { get { return m_Description; } set { m_Description = value; } }
        [CommandProperty(AccessLevel.GameMaster, true)]
        public int Fonds
        {
            get
            {
                if (m_Terre == null)
                    return m_Fonds;
                return m_Terre.Fonds;
            }
            set
            {
                if (m_Terre == null)
                    m_Fonds = value;
                else
                    m_Terre.Fonds = value;
            }
        }

        public Tresorier(string description, Terre terre, Point3D p) : this (p)
        {
            m_Description = description;
            m_Terre = terre;
            m_Etablissement = "";
        }

        public Tresorier(string etablissement, Mobile gestionnaire, Point3D p) : this (p)
        {
            m_Etablissement = etablissement;
            m_Gestionnaire = gestionnaire;
            m_Fonds = 0;
        }

        public Tresorier(Point3D p) : this()
        {
            Location = p;
            Map = Map.Felucca;
        }

        public Tresorier()
        {
            InitStats(75, 75, 75);
            InitBody();
            InitOutfit();
            m_Fonds = 0;
            m_Employes = new OrderedDictionary<Mobile, Employe>();
            CantWalk = true;
            m_Messages = new List<string>();
        }

        public Tresorier(Serial serial) : base (serial)
        {
            
        }

        public void AddEmploye(Mobile employe, string nom, string titre, int paie)
        {
            Employe e;
            if (m_Employes.TryGetValue(employe, out e))
            {
                e.Paie = paie;
                e.Titre = titre;
                e.Removed = false;
            }
            else
            {
                m_Employes.Add(employe, new Employe(employe, nom, titre, paie));
            }
        }

        public void RemoveEmploye(Mobile employe)
        {
            Employe e;
            if (!m_Employes.TryGetValue(employe, out e))
                return;
            if (e.Total > 0)
                e.Removed = true;
            else
                m_Employes.Remove(employe);
        }

        public IEnumerator<Employe> Employes()
        {
            return m_Employes.GetEnumerator();
        }

        public Employe this[int i]
        {
            get { return m_Employes[i]; }
        }

        public Employe this[Mobile m]
        {
            get { return m_Employes[m]; }
        }

        public int EmployeCount
        {
            get { return m_Employes.Count; }
        }

        public void OnPaiementEvent(object source, ElapsedEventArgs e)
        {
            foreach (Employe employe in m_Employes.Values)
            {
                if (employe.Personnage.Deleted)
                {
                    Fonds += employe.Total; // Si le perso a ete delete, l'etablissement reprend l'argent non reclame
                    employe.Total = 0;
                    RemoveEmploye(employe.Personnage);
                    continue;
                }
                int apayer = employe.APayer();
                if (Fonds < apayer)
                {
                    employe.AjouterMessage(String.Format("Une paie de {0} n'a pas pu vous être remis par manque de fonds.", 
                        employe.Paie.ToString("N", Geopolitique.NFI)));
                    AjouterMessage(String.Format("La paie de {0} d'une valeur de {1} n'a pas pu être délivrée par manque de fonds.",
                        employe.Nom, apayer.ToString("N", Geopolitique.NFI)));
                    continue;
                }
                if (employe.Removed)
                    continue;
                employe.Total += apayer;
                Fonds -= apayer;
            }
        }

        public void RetraitEmploye(Mobile employe, int montant)
        {
            Employe e;
            if (!m_Employes.TryGetValue(employe, out e))
                return;
            if (e.Total < montant)
            {
                ReponseAuGump(employe, Etablissement + " ne vous doit que " + e.Total + ".");
                return;
            }
            if (montant < 5000)
            { // Va peut-etre failer si le sac ne peut contenir un tel poids.
                employe.Backpack.DropItem(new Gold(montant));
            }
            else
            {
                employe.Backpack.DropItem(new BankCheck(montant));
            }
            e.Total -= montant;

            if (e.Removed && (e.Total == 0))
                RemoveEmploye(employe);
        }

        public void AjoutFonds(Mobile from, int montant)
        {
            if (from.AccessLevel > AccessLevel.Player)
            {
                Fonds += montant;
                ReponseAuGump(from, "Vous avez ajouté " + montant + " aux fonds.");
            }
            else
            {
                Item[] gold;
                Item[] checks;

                gold = from.Backpack.FindItemsByType(typeof(Gold));
                checks = from.Backpack.FindItemsByType(typeof(BankCheck));

                int totalJoueur = 0;
                for (int i = 0; i < checks.Length; i++)
                {
                    BankCheck check = (BankCheck)checks[i];
                    totalJoueur += check.Worth;
                    if (totalJoueur >= montant)
                    {
                        int reste = totalJoueur - montant;
                        if (reste > 0) 
                            from.Backpack.DropItem(new BankCheck(reste));

                        Fonds += montant;
                        for (int j = 0; j <= i; j++)
                        {
                            checks[j].Delete();
                        }
                        ReponseAuGump(from, "Vous avez ajouté " + montant + " aux fonds.");
                        return;
                    }
                }
                for (int i = 0; i < gold.Length; i++)
                {
                    totalJoueur += gold[i].Amount;
                    if (totalJoueur >= montant)
                    {
                        int reste = totalJoueur - montant;
                        if (reste > 0) 
                            from.Backpack.DropItem(new Gold(reste));

                        Fonds += montant;
                        for (int j = 0; j < checks.Length; j++)
                        {
                            checks[j].Delete();
                        }
                        for (int j = 0; j <= i; j++)
                        {
                            gold[j].Delete();
                        }
                        ReponseAuGump(from, "Vous avez ajouté " + montant + " aux fonds.");
                        return;
                    }
                }
                ReponseAuGump(from, "Vous n'avez pas " + montant + " pièces sur vous.");
            }
        }

        public void RetraitFonds(Mobile from, int montant)
        {
            if (montant > Fonds)
            {
                ReponseAuGump(from, "Nous n'avons pas les fonds pour que vous puissez retirer " + montant + " pièces.");
                return;
            }
            from.Backpack.DropItem(new BankCheck(montant));
            Fonds -= montant;
            ReponseAuGump(from, String.Format("Vous avez retiré {0} aux fonds.", montant.ToString()));
        }

        public void AjouterMessage(string message)
        {
            m_Messages.Add(message);
        }

        public List<string> DelivrerMessages()
        {
            List<string> messages = m_Messages;
            m_Messages = new List<string>();
            return m_Messages; 
        }

        public void ReponseAuGump(Mobile from, string reponse)
        {
            if (InLOS(from))
                PrivateOverheadMessage(MessageType.Regular, 0x3B2, false, reponse, from.NetState);
            else
                from.SendMessage(reponse);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from == m_Gestionnaire)
            {
                List<string> messages = DelivrerMessages();
                if (messages.Count > 0)
                {
                    ReponseAuGump(from, "J'ai des messages pour vous.");
                    ReponseAuGump(from, "*Commence a lire les messages à haute voix*");
                    foreach (string m in messages)
                        ReponseAuGump(from, m);
                }
                from.SendGump(new TresorierGump(this, from, 0));
            }
            else if(from.AccessLevel > AccessLevel.GameMaster)
                from.SendGump(new TresorierGump(this, from, 0));
            else if (m_Employes[from] != null)
            {
                from.SendGump(new EmployeGump(this, this[from], false));
                List<string> messages = this[from].DelivrerMessages();
                if (messages.Count > 0)
                {
                    ReponseAuGump(from, "J'ai des messages pour vous.");
                    ReponseAuGump(from, "*Commence a lire les messages à haute voix*");
                    foreach (string m in messages)
                        ReponseAuGump(from, m);
                }
            }
            else
                base.OnDoubleClick(from);
        }

        public override bool AllowEquipFrom(Mobile mob)
        {
            return (mob == Gestionnaire || base.AllowEquipFrom(mob));
        }

        public override void OnDelete()
        {
            m_Terre.RetirerTresorier(this);
            base.OnDelete();
        }
        
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);

            writer.Write((Mobile)m_Gestionnaire);
            writer.Write((string)m_Etablissement);
            writer.Write((string)m_Description);
            writer.Write((int)m_Fonds);
            
            writer.Write((int)m_Employes.Count);
            foreach (Employe e in m_Employes)
            {
                e.Serialize(writer);
            }

            writer.Write((int)m_Messages.Count);
            foreach (string m in m_Messages)
                writer.Write((string)m);
            
            //new Timer(
            //m_Paiement.
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Gestionnaire = reader.ReadMobile();
            m_Etablissement = reader.ReadString();
            m_Description = reader.ReadString();
            m_Fonds = reader.ReadInt();
            
            int count = reader.ReadInt();
            m_Employes = new OrderedDictionary<Mobile, Employe>();
            for (int i = 0; i < count; i++)
            {
                Employe e = new Employe(reader);
                m_Employes.Add(e.Personnage, e);
            }

            count = reader.ReadInt();
            m_Messages = new List<string>();
            for (int i = 0; i < count; i++)
                m_Messages.Add(reader.ReadString());
        }

        // Fonctions prisent des PlayerVendors. Ce pourrait etre une
        // bonne idee de les installer dans une classe commune.
        public void InitBody()
		{
			Hue = Utility.RandomSkinHue();
			SpeechHue = 0x3B2;

			if ( !Core.AOS )
				NameHue = 0x35;

			if ( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}
		}

		public virtual void InitOutfit()
		{
			Item item = new FancyShirt( Utility.RandomNeutralHue() );
			item.Layer = Layer.InnerTorso;
			AddItem( item );
			AddItem( new LongPants( Utility.RandomNeutralHue() ) );
			AddItem( new BodySash( Utility.RandomNeutralHue() ) );
			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new Cloak( Utility.RandomNeutralHue() ) );

			Utility.AssignRandomHair( this );

			Container pack = new Backpack();
			pack.Movable = false;
			AddItem( pack );
		}
    }
}
