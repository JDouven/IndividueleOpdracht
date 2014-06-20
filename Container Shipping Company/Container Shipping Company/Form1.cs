using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Container_Shipping_Company
{
    public partial class Form1 : Form
    {
        private Beheer beheer;
        private DatabaseManager database;
        private Inplanning planning;
        public Form1()
        {
            InitializeComponent();
            beheer = new Beheer();
            database = new DatabaseManager();

            //Comboboxen vullen
            cb_Con_Bedrijf.DataSource = beheer.Bedrijven;
            cb_Con_Bestemming.DataSource = beheer.Bestemmingen;
            cb_Con_Type.DataSource = Enum.GetValues(typeof(ContainerType));
            comboBoxBestemming.DataSource = beheer.Bestemmingen;
            comboBoxSchip.DataSource = beheer.Schepen;
        }

        private void btn_Con_Voegtoe_Click(object sender, EventArgs e)
        {
            bool fout = false;
            //Type container invoer controleren
            ContainerType type;
            if (!Enum.TryParse<ContainerType>(cb_Con_Type.SelectedValue.ToString(), out type))
            {
                MessageBox.Show("Fout type");
                fout = true;
            }
            //Bedrijf invoer controleren
            Containertruckingbedrijf bedrijf = null;
            foreach (Containertruckingbedrijf b in beheer.Bedrijven)
            {
                if (b.ToString() == cb_Con_Bedrijf.SelectedValue.ToString())
                {
                    bedrijf = b;
                }
            }
            if (bedrijf == null)
            {
                MessageBox.Show("Fout bedrijf");
                fout = true;
            }
            //Bestemming invoer controleren
            Bestemming bestemming = null;
            foreach (Bestemming b in beheer.Bestemmingen)
            {
                if (b.ToString() == cb_Con_Bestemming.SelectedValue.ToString())
                {
                    bestemming = b;
                }
            }
            if (bestemming == null)
            {
                MessageBox.Show("Fout bestemming");
                fout = true;
            }
            //Gewicht invoer controleren
            int gewicht = 0;
            if (!int.TryParse(tb_Con_Gewicht.Text, out gewicht))
            {
                MessageBox.Show("Gewicht moet een getal zijn!");
                fout = true;
            }

            if (!fout)
            {
                if (!database.AddContainer(new Container(0, bedrijf.Naam, bestemming, gewicht, type, false)))
                {
                    MessageBox.Show("Fout bij toevoegen");
                }
                else
                {
                    MessageBox.Show("Container toegevoegd.", "Gelukt!", MessageBoxButtons.OK);
                }
            }
            beheer.Refresh();
        }

        private void btn_Bedr_Voegtoe_Click(object sender, EventArgs e)
        {
            bool fout = false;

            //Controleer KvKNummer
            int kvknummer;
            if (!int.TryParse(tb_Bedr_KvKNr.Text, out kvknummer))
            {
                MessageBox.Show("Fout kamer van koophandel nummer");
                fout = true;
            }

            if (!fout)
            {
                if (!database.AddContainertruckingbedrijf(new Containertruckingbedrijf(tb_Bedr_Naam.Text, tb_Bedr_Contact.Text, kvknummer)))
                {
                    MessageBox.Show("Fout bij toevoegen");
                }
                else
                {
                    MessageBox.Show("Containertruckingbedrijf toegevoegd.", "Gelukt!", MessageBoxButtons.OK);
                }
            }
            beheer.Refresh();
        }

        private void btn_Schip_Voegtoe_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tb_Con_Gewicht.Text) <= 30000)
                {

                    if (!database.AddSchip(new Schip(tb_Schip_Type.Text,
                        Convert.ToInt32(nud_Schip_Hoogte.Value),
                        Convert.ToInt32(nud_Schip_Rijen.Value),
                        Convert.ToInt32(nud_Schip_ContainersPerRij.Value),
                        Convert.ToInt32(nud_Schip_Stroom.Value))))
                    {
                        MessageBox.Show("Fout bij toevoegen");
                    }
                    else
                    {
                        MessageBox.Show("Schip toegevoegd.", "Gelukt!", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Een container mag niet meer wegen als 30000kg");
                }
            }
            catch { MessageBox.Show("Verkeerde invoer"); }
            beheer.Refresh();
        }

        private void btn_Best_Voegtoe_Click(object sender, EventArgs e)
        {
            if (!database.AddBestemming(new Bestemming(tb_Best_Naam.Text, tb_Best_Land.Text)))
            {
                MessageBox.Show("Fout bij toevoegen");
            }
            else
            {
                MessageBox.Show("Bestemming toegevoegd.", "Gelukt!", MessageBoxButtons.OK);
            }
            beheer.Refresh();
        }

        private void btnMaakIndeling_Click(object sender, EventArgs e)
        {
            bool fout = false;
            //Bestemming invoer controleren
            Bestemming bestemming = null;
            foreach (Bestemming b in beheer.Bestemmingen)
            {
                if (b.ToString() == comboBoxBestemming.SelectedValue.ToString())
                {
                    bestemming = b;
                }
            }
            if (bestemming == null)
            {
                MessageBox.Show("Fout bestemming");
                fout = true;
            }

            //Schip invoer controleren
            Schip schip = null;
            foreach (Schip s in beheer.Schepen)
            {
                if (s.ToString() == comboBoxSchip.SelectedValue.ToString())
                {
                    schip = s;
                }
            }
            if (schip == null)
            {
                MessageBox.Show("Fout schip");
                fout = true;
            }
            if (!fout)
            {
                listBoxIndeling.Items.Clear();
                listBoxInfo.Items.Clear();
                planning = new Inplanning(bestemming, schip);
                for (int laag = 0; laag < planning.SchipLading.GetLength(0); laag++)
                {
                    listBoxIndeling.Items.Add("Laag " + laag + ":");
                    for (int rij = 0; rij < planning.SchipLading.GetLength(1); rij++)
                    {
                        string regel = string.Empty;
                        for (int diepte = 0; diepte < planning.SchipLading.GetLength(2); diepte++)
                        {
                            string type = "_";
                            if (planning.SchipLading[laag, rij, diepte] != null)
                            {
                                type = planning.SchipLading[laag, rij, diepte].Type.ToString();
                            }
                            regel += type + " ";
                        }
                        listBoxIndeling.Items.Add(regel);
                    }
                }
                //Trivia invoeren in een listbox
                listBoxInfo.Items.Add("Gewicht links: " + planning.GewichtLinks.ToString());
                listBoxInfo.Items.Add("Gewicht rechts: " + planning.GewichtRechts.ToString());
                listBoxInfo.Items.Add("Gewicht totaal: " + (planning.GewichtLinks + planning.GewichtRechts).ToString());
                listBoxInfo.Items.Add(string.Empty);
                listBoxInfo.Items.Add("Balans links: " + Math.Round(Convert.ToDouble(planning.GewichtLinks) / (Convert.ToDouble(planning.GewichtLinks + planning.GewichtRechts)), 2));
                listBoxInfo.Items.Add("Balans rechts: " + Math.Round(Convert.ToDouble(planning.GewichtRechts) / (Convert.ToDouble(planning.GewichtLinks + planning.GewichtRechts)), 2));
                listBoxInfo.Items.Add("Maximaal gewicht op een container: " + planning.MaxVerticaalGewicht.ToString());

                btnMarkeer.Enabled = true;
                btnExporteer.Enabled = true;
            }
        }

        private async void btnExporteer_Click(object sender, EventArgs e)
        {
            string exportText = string.Empty;

            exportText += "Indeling gegenereerd op: " + DateTime.Now.ToShortDateString() + Environment.NewLine + Environment.NewLine +
                "Bestemming: " + planning.PBestemming.Naam + Environment.NewLine + Environment.NewLine +
                "Type vrachtschip: " + planning.PSchip.Type + Environment.NewLine + Environment.NewLine;

            for (int laag = 0; laag < planning.SchipLading.GetLength(0); laag++)
            {
                for (int rij = 0; rij < planning.SchipLading.GetLength(1); rij++)
                {
                    for (int diepte = 0; diepte < planning.SchipLading.GetLength(2); diepte++)
                    {
                        Container current = planning.SchipLading[laag, rij, diepte];
                        if (current != null)
                        {
                            exportText += current.Containertruckingbedrijf + ";" +
                                current.ID.ToString() + ";" +
                                laag.ToString() + ";" +
                                rij.ToString() + ";" +
                                diepte.ToString() + Environment.NewLine;
                        }
                    }
                }
            }

            string path;
            StreamWriter writeStream;
            DialogResult = saveFileDialog.ShowDialog();
            if (DialogResult != DialogResult.Cancel)
            {
                path = saveFileDialog.FileName;
                try
                {
                    using (writeStream = new StreamWriter(path, false))
                    {
                        await writeStream.WriteAsync(exportText);
                    }
                }
                catch
                {
                    MessageBox.Show("An error has occured while saving the file");
                }
            }
        }

        private void btnMarkeer_Click(object sender, EventArgs e)
        {
            int aantal = 0;
            foreach (Container c in planning.SchipLading)
            {
                database.SetContainerIngepland(c.ID);
                aantal++;
            }
            MessageBox.Show(aantal + "containers ingepland!");
        }


    }
}
