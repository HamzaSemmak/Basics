using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.UTILS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleDetailClient.model
{
    class DetailsClientOperation
    {
        public DateTime DateOperation { get; set; }
        public Ticket Ticket { get; set; }
        public TypeOperation TypeOperation { get; set; }
        public string MessageFinClient { get; set; }
        public decimal MontantMoins { get; set; }
        public TypePaiment TypePaiement { get; set; }
        public Voucher Voucher { get; set; }
        public TypeVoucher VOUCHER_TYPE { get; set; }
        public DetailsClientOperation()
        {
            DateOperation = DateTime.Now;
        }
        public DetailsClientOperation(int numClient, decimal total, TypeOperation typeOperation)
        {
            TypeOperation = typeOperation;
            DateOperation = DateTime.Now;
            string msg = total >= 0 ? "RECEVEZ" : "DEVEZ";
            MessageFinClient = DateOperation.ToString("HH:mm:ss") + " FIN CLIENT " + numClient + " "+ msg + " " + Math.Abs(total) + "DH";
        }
        public DetailsClientOperation(Ticket ticket, TypeOperation typeOperation)
        {
            Ticket = new Ticket(ticket);
            DateOperation = ticket.DateEmission;
            TypeOperation = typeOperation;
        }
        public DetailsClientOperation(Ticket ticket, decimal montantMoins, TypeOperation typeOperation, TypePaiment typepaiement)
        {
            Ticket = new Ticket(ticket);
            DateOperation = ticket.DateEmission;
            TypeOperation = typeOperation;
            MontantMoins = montantMoins;
            TypePaiement = typepaiement;
        }
        public DetailsClientOperation(Voucher voucher,decimal montant, TypeVoucher type_v)
        {
            TypePaiement = TypePaiment.VOUCHER;
            TypeOperation = TypeOperation.Paiement;
            DateOperation = voucher.DateEmission;
            Voucher = new Voucher(voucher);
            Voucher.Montant = montant;
            VOUCHER_TYPE = type_v;
        }

        public DetailsClientOperation(decimal montant, TypeOperation typeOperation, TypePaiment typePaiment)
        {
            TypeOperation = typeOperation;
            DateOperation = DateTime.Now;
            MontantMoins = montant;
            TypePaiement = typePaiment;
        }
        public List<OperationLigne> OperationLignes()
        {
            List<OperationLigne> operationLignes = new List<OperationLigne>();
            switch (this.TypeOperation)
            {
                case TypeOperation.FinClient:
                    operationLignes.Add(new OperationLigne(MessageFinClient, Color.White, Color.Black));
                    return operationLignes;
                case TypeOperation.Paiement:
                    string l = DateTime.Now.ToString("HH:mm:ss");
                    string l2 = "";
                    switch (this.TypePaiement)
                    {
                        case TypePaiment.ANNULATION:
                            l += "  ANNULATION " + "         N°" + Ticket.IdTicket + "       ...";
                            l2 = "                           " + MontantMoins + "DH";
                            operationLignes.Add(new OperationLigne(l, Color.Gray, Color.LightGreen));
                            operationLignes.Add(new OperationLigne(l2, Color.Gray, Color.LightGreen));
                            return operationLignes;
                        case TypePaiment.PAIEMENT:
                            l += "  PAIEMENT " + "         N°" + Ticket.IdTicket + "       ...";
                            l2 = "                           " + MontantMoins + "DH";
                            operationLignes.Add(new OperationLigne(l, Color.Gray, Color.LightGreen));
                            operationLignes.Add(new OperationLigne(l2, Color.Gray, Color.LightGreen));
                            return operationLignes;
                        case TypePaiment.VOUCHER:
                            string type;
                            switch (this.VOUCHER_TYPE)
                            {
                                case TypeVoucher.DISTRIBUTION:
                                    type = " DISTR";
                                    break;
                                case TypeVoucher.ANNULATION:
                                    type = " ANNUL";
                                    break;
                                case TypeVoucher.PAEIMENT:
                                    type = " PAEIM";
                                    break;
                                default:
                                    type = "";
                                    break;
                            }
                            l += type+" CHEQUE JEUX" + "  N°" + Voucher.IdVoucher + "       ...";
                            l2 = "                           " + Voucher.Montant.ToString() + "DH";
                            operationLignes.Add(new OperationLigne(l, Color.Gray, Color.LightGreen));
                            operationLignes.Add(new OperationLigne(l2, Color.Gray, Color.LightGreen));
                            return operationLignes;
                        case TypePaiment.GROS_GAIN:
                            l += " Gros Gain "+MontantMoins+"DH";
                            operationLignes.Add(new OperationLigne(l, Color.Gray, Color.LightGreen));
                            return operationLignes;
                        default: return operationLignes;
                    }
                case TypeOperation.Pari:
                    string ligne = Ticket.DateEmission.ToString("HH:mm:ss") + " " + Ticket.NumCourse + "   " + Ticket.CodeHippo.Substring(0, 3) + " R" + " " + Ticket.NumReunion + " " + Ticket.DateReunion;
                    OperationLigne opL1 = new OperationLigne(ligne, Color.Blue, Color.Gray);
                    string ligne2 = "                                              N°" + Ticket.IdTicket;
                    OperationLigne opL2 = new OperationLigne(ligne2, Color.Blue, Color.Gray);
                    operationLignes.Add(opL1);
                    operationLignes.Add(opL2);
                    foreach (Formulation f in Ticket.ListeFormulation)
                    {
                        string cource = Ticket.NumCourse <= 9 ? "0" + Ticket.NumCourse : Ticket.NumCourse.ToString();
                        string ligne3 = "C" + cource + " " + StringUtils.Truncate(getNameProduit(f.Produit.NomProduit), 8).ToUpper() + "  Enjeu " + f.MiseCombinaison + "DH";
                        OperationLigne opL3 = new OperationLigne(ligne3, Color.Blue, Color.LightGray);
                        operationLignes.Add(opL3);
                        string ligne4 = "                                  " + f.MiseTotal.ToString() + "DH";
                        OperationLigne opL4 = new OperationLigne(ligne4, Color.Blue, Color.LightGray);
                        operationLignes.Add(opL4);
                        string ligne5 = "                 ";
                        int designationCount = f.Designation.Split(' ').Count();
                        if (designationCount <= 10)
                        {
                            ligne5 += f.Designation;
                            OperationLigne opL5 = new OperationLigne(ligne5, Color.Blue, Color.LightGray);
                            operationLignes.Add(opL5);
                        }
                        else
                        {
                            string[] partants = f.Designation.Split(' ');
                            for (int i = 0; i <= 10; i++)
                            {
                                ligne5 += partants[i].ToString() + "  ";
                            }
                            ligne5 += "...";
                            OperationLigne opL5 = new OperationLigne(ligne5, Color.Blue, Color.LightGray);
                            operationLignes.Add(opL5);
                            string ligne6 = "                 ";
                            for (int i = 11; i < partants.Length; i++)
                            {
                                ligne6 += partants[i].ToString() + " ";
                            }
                            OperationLigne opL6 = new OperationLigne(ligne6, Color.Blue, Color.LightGray);
                            operationLignes.Add(opL6);
                        }
                    }
                    string ligne7 = Ticket.DateEmission.ToString("dd-MM-yyyy") + "    DISTRIBUTION" + "                   ...";
                    OperationLigne opL7 = new OperationLigne(ligne7, Color.Blue, Color.Gray);
                    operationLignes.Add(opL7);
                    string ligne8 = "                                                 " + Ticket.PrixTotalTicket + "DH";
                    OperationLigne opL8 = new OperationLigne(ligne8, Color.Blue, Color.Gray);
                    operationLignes.Add(opL8);
                    return operationLignes;
                default: return operationLignes;
            }
        }
        private static string getNameProduit(string nomProduit)
        {
            string produit;
            switch (nomProduit)
            {
                case "QUARTE PLUS": produit = "QUARTE +"; break;
                case "QUINTE PLUS": produit = "QUINTE +"; break;
                default: produit = nomProduit; break;
            }
            return produit;
        }
    }

}
