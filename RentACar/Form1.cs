using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RentACar
{
    public partial class Form1 : Form
    {
        Database db = new Database();
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
            timer1.Stop();
            timer2.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnMusteri_Click(object sender, EventArgs e)
        {
            btnMusteri.Visible = false;
            btnArac.Visible = false;
            btnSozlesme.Visible = false;
            btnMusteriEkle.Visible = true;
            btnMusteriListe.Visible = true;
            btnGeri.Visible = true;
            btnGeri.Location = new Point(71, 236);
            progressBar1.Value = 0;
        }

        private void btnArac_Click(object sender, EventArgs e)
        {
            btnMusteri.Visible = false;
            btnArac.Visible = false;
            btnGeri.Visible = true;
            btnSozlesme.Visible = false;
            btnAracEkle.Visible = true;
            btnGeri.Location = new Point(71, 190);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult s = MessageBox.Show("Uygulama'dan çıkmak istediğinize emin misiniz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (s == DialogResult.Yes)
            { Application.Exit(); }
        }

        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            pSozlesmeEkle.Visible = false;
            pAracEkle.Visible = false;
            pSozlesmeList.Visible = false;
            pMusteriEkle.Visible = true;
            pMusteriListe.Visible = false;
        }

        private void btnArac_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnMusteriEkle.Visible = false;
            btnMusteriListe.Visible = false;
            btnArac.Visible = true;
            btnMusteri.Visible = true;
            btnSozlesme.Visible = true;
            btnSozlesmeEkle.Visible = false;
            btnSozlesmeList.Visible = false;
            btnAracEkle.Visible = false;
            btnGeri.Location = new Point(71, 283);
            btnGeri.Visible = false;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            txtAra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            txtAra.ForeColor = Color.Black;
        }

        private void btnMusteriListe_Click(object sender, EventArgs e)
        {
            pSozlesmeEkle.Visible = false;
            pSozlesmeList.Visible = false;
            pAracEkle.Visible = false;
            pMusteriEkle.Visible = false;
            pMusteriListe.Visible = false;
            MusteriListe();
            pBekleme.Visible = true;
            lblBeklemeYazi.Text = "MÜŞTERİ LİSTESİ YÜKLENİYOR..";
            timer2.Start();
        }

        private void txtAra_Click(object sender, EventArgs e)
        {
            txtAra.Clear();
        }

        private void btnAracEkle_Click(object sender, EventArgs e)
        {
            pSozlesmeEkle.Visible = false;
            pAracEkle.Visible = true;
            pSozlesmeList.Visible = false;
            pMusteriEkle.Visible = false;
            pMusteriListe.Visible = false;
            AracListe();
        }

        public void AracListe()
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            db.baglanti.Open();
            cmd = new SqlCommand("select * from Araclar", db.baglanti);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            dtAracListe.DataSource = dt;
            db.baglanti.Close();
        }

        public void MusteriListe()
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            db.baglanti.Open();
            cmd = new SqlCommand("select * from Musteriler", db.baglanti);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            dtMusteriListe.DataSource = dt;
            dtMusteriListe.Columns[0].Visible = false;
            db.baglanti.Close();
        }
        private void btnSozlesme_Click(object sender, EventArgs e)
        {
            btnMusteri.Visible = false;
            btnArac.Visible = false;
            btnSozlesmeEkle.Visible = true;
            btnSozlesmeList.Visible = true;
            btnGeri.Visible = true;
            btnGeri.Location = new Point(71, 236);
            btnSozlesme.Visible = false;
        }

        private void btnSozlesmeEkle_Click(object sender, EventArgs e)
        {
            pAracEkle.Visible = false;
            pMusteriEkle.Visible = false;
            pMusteriListe.Visible = false;
            pSozlesmeList.Visible = false;
            pBekleme.Visible = true;
            pBekleme.BringToFront();
            MusteriComboBox();
            AracComboBox();
            timer1.Start();
            lblBeklemeYazi.Text = "SÖZLEŞME EKRANI YÜKLENİYOR..";
            progressBar1.Value = 0;
        }

        private void btnKaydetArac_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("insert into Araclar(Plaka, Marka, Tip, Model, Renk, Günlük, Haftalık, Aylık, Durum) values (@plaka, @marka, @tip, @model, @renk, @gun, @haf, @ay, @durum)", db.baglanti);
                cmd.Parameters.AddWithValue("@plaka", txtPlaka.Text);
                cmd.Parameters.AddWithValue("@marka", txtMarka.Text);
                cmd.Parameters.AddWithValue("@tip", txtTip.Text);
                cmd.Parameters.AddWithValue("@model", txtModel.Text);
                cmd.Parameters.AddWithValue("@renk", txtRenk.Text);
                cmd.Parameters.AddWithValue("@gun", txtGunluk.Text);
                cmd.Parameters.AddWithValue("@haf", txtHafta.Text);
                cmd.Parameters.AddWithValue("@ay", txtAy.Text);
                cmd.Parameters.AddWithValue("@durum", "Boşta");
                cmd.ExecuteNonQuery();
                AracTextleriSil();
                db.baglanti.Close();
                AracListe();
                MessageBox.Show("Kaydetme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata) { MessageBox.Show("Kaydetme'ye çalıştığınız araç zaten kayıtlı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        public void AracTextleriSil()
        {
            txtPlaka.Clear(); txtMarka.Clear(); txtTip.Clear(); txtModel.Clear(); txtRenk.Clear(); txtGunluk.Clear(); txtHafta.Clear(); txtAy.Clear();
        }

        private void dtAracListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Dikkat!! aracın Plakası değiştirilemez.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            btnGuncelleArac.Enabled = true;
            btnSilArac.Enabled = true;
            lblPlaka.Text = dtAracListe.CurrentRow.Cells[0].Value.ToString();
            txtPlaka.Text = dtAracListe.CurrentRow.Cells[0].Value.ToString();
            txtMarka.Text = dtAracListe.CurrentRow.Cells[1].Value.ToString();
            txtTip.Text = dtAracListe.CurrentRow.Cells[2].Value.ToString();
            txtModel.Text = dtAracListe.CurrentRow.Cells[3].Value.ToString();
            txtRenk.Text = dtAracListe.CurrentRow.Cells[4].Value.ToString();
            txtGunluk.Text = dtAracListe.CurrentRow.Cells[5].Value.ToString();
            txtHafta.Text = dtAracListe.CurrentRow.Cells[6].Value.ToString();
            txtAy.Text = dtAracListe.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnGuncelleArac_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("update Araclar set Plaka=@plaka, Marka=@marka, Tip=@tip, Model=@model, Renk=@renk, Günlük=@gun, Haftalık=@haf, Aylık=@ay where Plaka=@plaka", db.baglanti);
                cmd.Parameters.AddWithValue("@plaka", txtPlaka.Text);
                cmd.Parameters.AddWithValue("@marka", txtMarka.Text);
                cmd.Parameters.AddWithValue("@tip", txtTip.Text);
                cmd.Parameters.AddWithValue("@model", txtModel.Text);
                cmd.Parameters.AddWithValue("@renk", txtRenk.Text);
                cmd.Parameters.AddWithValue("@gun", txtGunluk.Text);
                cmd.Parameters.AddWithValue("@haf", txtHafta.Text);
                cmd.Parameters.AddWithValue("@ay", txtAy.Text);
                cmd.ExecuteNonQuery();
                AracTextleriSil();
                AracListe();
                db.baglanti.Close();
            }
            catch { }
        }

        private void btnKaydetMusteri_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("insert into Musteriler(TC_Kimlik_No, Ad, Soyad, Cinsiyet, DT, DYeri, TelNo, eMail, Adres, Ehliyet_No, Ehliyet_Tarihi ,Ehliyet_V_Yer) values(@tc, @ad, @soyad, @cinsiyet, @dt, @dyeri, @tel, @email, @adres, @ehliyetno, @ehliyettarihi, @ehliyetver)", db.baglanti);
                cmd.Parameters.AddWithValue("@tc", txtTCKimlik.Text);
                cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                cmd.Parameters.AddWithValue("@cinsiyet", cmbCins.Text);
                cmd.Parameters.AddWithValue("@dt", dtDogumTarihi.Value);
                cmd.Parameters.AddWithValue("@dyeri", txtDogumYeri.Text);
                cmd.Parameters.AddWithValue("@tel", txtCepNo.Text);
                cmd.Parameters.AddWithValue("@email", txteMail.Text);
                cmd.Parameters.AddWithValue("@adres", txtAdres.Text);
                cmd.Parameters.AddWithValue("@ehliyetno", txtEhliyetNo.Text);
                cmd.Parameters.AddWithValue("@ehliyettarihi", dtEhliyetTarihi.Value);
                cmd.Parameters.AddWithValue("@ehliyetver", txtEhliyetVYer.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri başarılı bir şekilde kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                db.baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show("Müşteri kaydedilemedi. Bilgileri tekrar kontrol etmeyi deneyin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                DialogResult s = MessageBox.Show("Seçilen aracı silmek istediğinize emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (s == DialogResult.Yes)
                {
                    cmd = new SqlCommand("delete from Araclar where Plaka=@plaka", db.baglanti);
                    cmd.Parameters.AddWithValue("@plaka", txtPlaka.Text);
                    cmd.ExecuteNonQuery();
                    AracListe();
                    AracTextleriSil();
                }
                db.baglanti.Close();
            }
            catch { }
        }

        private void dtMusteriListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {   //Veritabanından seçilen müşteri'nin bilgilerini textBox, comboBox ve DateTimePicker araçlarına aktarılması.
                lblID.Text = dtMusteriListe.CurrentRow.Cells[0].Value.ToString();
                txtTCKimlik2.Text = dtMusteriListe.CurrentRow.Cells[1].Value.ToString();
                txtAd2.Text = dtMusteriListe.CurrentRow.Cells[2].Value.ToString();
                txtSoyad2.Text = dtMusteriListe.CurrentRow.Cells[3].Value.ToString();
                cmbCins2.Text = dtMusteriListe.CurrentRow.Cells[4].Value.ToString();
                dtDT2.Text = dtMusteriListe.CurrentRow.Cells[5].Value.ToString();
                txtDY2.Text = dtMusteriListe.CurrentRow.Cells[6].Value.ToString();
                txtCepNo2.Text = dtMusteriListe.CurrentRow.Cells[7].Value.ToString();
                txteMail2.Text = dtMusteriListe.CurrentRow.Cells[8].Value.ToString();
                txtAdres2.Text = dtMusteriListe.CurrentRow.Cells[9].Value.ToString();
                txtEhliyetNo2.Text = dtMusteriListe.CurrentRow.Cells[10].Value.ToString();
                dtET2.Text = dtMusteriListe.CurrentRow.Cells[11].Value.ToString();
                txtEhliyetVyer2.Text = dtMusteriListe.CurrentRow.Cells[12].Value.ToString();
            }
            catch { }

        }

        private void btnDegisikKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("update Musteriler set TC_Kimlik_No=@tc, Ad=@ad, Soyad=@soyad, Cinsiyet=@cinsiyet, DT=@dt, DYeri=@dyeri, TelNo=@tel, eMail=@email, Adres=@adres, Ehliyet_No=@ehliyetno, Ehliyet_Tarihi=@ehliyettarih, Ehliyet_V_Yer=@ehliyetvyer where ID='" + lblID.Text + "'", db.baglanti);
                cmd.Parameters.AddWithValue("@tc", txtTCKimlik2.Text);
                cmd.Parameters.AddWithValue("@ad", txtAd2.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad2.Text);
                cmd.Parameters.AddWithValue("@cinsiyet", cmbCins2.Text);
                cmd.Parameters.AddWithValue("@dt", dtDT2.Text);
                cmd.Parameters.AddWithValue("@dyeri", txtDY2.Text);
                cmd.Parameters.AddWithValue("@tel", txtCepNo2.Text);
                cmd.Parameters.AddWithValue("@email", txteMail2.Text);
                cmd.Parameters.AddWithValue("@adres", txtAdres2.Text);
                cmd.Parameters.AddWithValue("@ehliyetno", txtEhliyetNo2.Text);
                cmd.Parameters.AddWithValue("@ehliyettarih", dtET2.Text);
                cmd.Parameters.AddWithValue("@ehliyetvyer", txtEhliyetVyer2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri bilgileri başarıyla değiştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MusteriListe();
                db.baglanti.Close();
            }
            catch { }
        }

        private void btnAraMusteri_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                da = new SqlDataAdapter("select * from Musteriler where TC_Kimlik_No='" + txtAra.Text + "'", db.baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dtMusteriListe.DataSource = ds.Tables[0];
                db.baglanti.Close();
            }
            catch { }
        }

        public void MusteriComboBox()
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                dt = new DataTable();
                da = new SqlDataAdapter("select * from Musteriler ORDER BY ID ASC", db.baglanti);
                da.Fill(dt);
                //Müşterileri ComboBox'a aktarma
                cmbMusteriSec.ValueMember = "ID";
                cmbMusteriSec.DisplayMember = "TC_Kimlik_No";
                cmbMusteriSec.DataSource = dt;
                db.baglanti.Close();
            }
            catch { }
        }

        public void AracComboBox()
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                dt = new DataTable();
                da = new SqlDataAdapter("select * from Araclar where Durum='Boşta'", db.baglanti);
                da.Fill(dt);
                //Araçları ComboBox'a aktarma
                cmbAracSec.ValueMember = "Durum";
                cmbAracSec.DisplayMember = "Plaka";
                cmbAracSec.DataSource = dt;
                db.baglanti.Close();
            }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
                pBekleme.Visible = false;
                pSozlesmeEkle.Visible = true;
            }
        }

        public void MusteriBilgi()
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("select * from Musteriler where TC_Kimlik_No='" + cmbMusteriSec.Text + "'", db.baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtTC3.Text = dr[1].ToString();
                    txtAd3.Text = dr[2].ToString();
                    txtSoyad3.Text = dr[3].ToString();
                    cmbCins3.Text = dr[4].ToString();
                    dtDT3.Text = dr[5].ToString();
                    txtDY3.Text = dr[6].ToString();
                    txtCepNo3.Text = dr[7].ToString();
                    txteMail3.Text = dr[8].ToString();
                    txtAdres3.Text = dr[9].ToString();
                    txtEhliyetNo3.Text = dr[10].ToString();
                    dtEhliyetTarihi3.Text = dr[11].ToString();
                    txtEhliyetVYer3.Text = dr[12].ToString();
                    break;
                }
                db.baglanti.Close();
            }
            catch { }
        }

        public void AracBilgi()
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("select * from Araclar where Plaka='" + cmbAracSec.Text + "'", db.baglanti);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtPlaka3.Text = dr[0].ToString();
                    txtMarka3.Text = dr[1].ToString();
                    txtTip3.Text = dr[2].ToString();
                    txtModel3.Text = dr[3].ToString();
                    txtRenk3.Text = dr[4].ToString();
                    txtGunluk3.Text = dr[5].ToString();
                    txtHaftalik3.Text = dr[6].ToString();
                    txtAy3.Text = dr[7].ToString();
                    break;
                }
                db.baglanti.Close();
            }
            catch { }
        }

        public void SozlesmeListe()
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            db.baglanti.Open();
            cmd = new SqlCommand("select * from Sozlesmeler", db.baglanti);
            da = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            dtSozlesmeListesi.DataSource = dt1;
            dtSozlesmeListesi.Columns[0].Visible = false;
            db.baglanti.Close();
        }

        private void cmbMusteriSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            MusteriBilgi();
        }

        private void cmbAracSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            AracBilgi();
        }

        bool ft = false;
        Point bn = new Point(0, 0);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ft = true;
            bn = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            ft = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ft)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - bn.X, p.Y - bn.Y);
            }
        }

        private void btnKirala_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("insert into Sozlesmeler(TC_Kimlik_No, Ad, Soyad, Cinsiyet, DT, DYeri, TelNo, eMail, Adres, Ehliyet_No, Ehliyet_Tarihi, Ehliyet_V_Yer, Surucu_Adi, Kefil_Ad, Kefil_Soyad, Kefil_Adres, Kefil_Cep, Arac_Plaka, Arac_Marka, Arac_Tip, Arac_Model, Arac_Renk ,Günlük, Haftalık, Aylık, Cikis_Tarihi, Donus_Tarihi, Ek_Tutar, Toplam, Aciklama) values(@tc, @ad, @soyad, @cinsiyet, @dt, @dyeri, @telno, @email, @adres, @ehliyetno, @ehliyettarihi, @ehliyetvyer, @surucuadi, @kefilad, @kefilsoyad, @kefiladres, @kefilcep, @plaka, @marka, @tip, @model, @renk, @gunluk, @haftalik, @aylik, @cikis, @donus, @ek, @toplam, @aciklama)", db.baglanti);
                cmd.Parameters.AddWithValue("@tc", txtTC3.Text);
                cmd.Parameters.AddWithValue("@ad", txtAd3.Text);
                cmd.Parameters.AddWithValue("@soyad", txtSoyad3.Text);
                cmd.Parameters.AddWithValue("@cinsiyet", cmbCins3.Text);
                cmd.Parameters.AddWithValue("@dt", dtDT3.Value);
                cmd.Parameters.AddWithValue("@dyeri", txtDY3.Text);
                cmd.Parameters.AddWithValue("@telno", txtCepNo3.Text);
                cmd.Parameters.AddWithValue("@email", txteMail3.Text);
                cmd.Parameters.AddWithValue("@adres", txtAdres3.Text);
                cmd.Parameters.AddWithValue("@ehliyetno", txtEhliyetNo3.Text);
                cmd.Parameters.AddWithValue("@ehliyettarihi", dtEhliyetTarihi3.Value);
                cmd.Parameters.AddWithValue("@ehliyetvyer", txtEhliyetVYer3.Text);
                cmd.Parameters.AddWithValue("@surucuadi", txtSurucuAd.Text);
                cmd.Parameters.AddWithValue("@kefilad", txtKefilAd.Text);
                cmd.Parameters.AddWithValue("@kefilsoyad", txtKefilSoyad.Text);
                cmd.Parameters.AddWithValue("@kefiladres", txtKefilAdres.Text);
                cmd.Parameters.AddWithValue("@kefilcep", txtKefilCepNo.Text);
                cmd.Parameters.AddWithValue("@plaka", txtPlaka3.Text);
                cmd.Parameters.AddWithValue("@marka", txtMarka3.Text);
                cmd.Parameters.AddWithValue("@tip", txtTip3.Text);
                cmd.Parameters.AddWithValue("@model", txtModel3.Text);
                cmd.Parameters.AddWithValue("@renk", txtRenk3.Text);
                cmd.Parameters.AddWithValue("@gunluk", txtGunluk3.Text);
                cmd.Parameters.AddWithValue("@haftalik", txtHaftalik3.Text);
                cmd.Parameters.AddWithValue("@aylik", txtAy3.Text);
                cmd.Parameters.AddWithValue("@cikis", dtCikisTarihi.Value);
                cmd.Parameters.AddWithValue("@donus", dtDonusTarihi.Value);
                cmd.Parameters.AddWithValue("@ek", txtEkTutar.Text);
                cmd.Parameters.AddWithValue("@toplam", txtToplam.Text);
                cmd.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sözleşme başarıyla kaydedildi." + "\n" + txtPlaka.Text + " Plakalı Araç" + txtAd3.Text + " " + txtSoyad3.Text + " adlı kişi'ye kiralanmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AracKirada();
                db.baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show("Gerekli yerleri lütfen boş bırakmayınız!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning); db.baglanti.Close(); }
        }

        public void AracKirada()
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("update Araclar set Durum=@durum where Plaka='" + txtPlaka3.Text + "'", db.baglanti);
                cmd.Parameters.AddWithValue("@durum", "Kirada");
                cmd.ExecuteNonQuery();
                db.baglanti.Close();
            }
            catch { }

        }

        public void AracBosta()
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("update Araclar set Durum=@durum where Plaka='" + lblAracPlaka.Text + "'", db.baglanti);
                cmd.Parameters.AddWithValue("@durum", "Boşta");
                cmd.ExecuteNonQuery();
                db.baglanti.Close();
            }
            catch { }
        }

        private void btnSilMusteri_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                cmd = new SqlCommand("delete from Musteriler where ID=@id", db.baglanti);
                cmd.Parameters.AddWithValue("@id", lblID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Müşteri başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MusteriListe();
                db.baglanti.Close();
            }
            catch (Exception a) { MessageBox.Show("" + a); }
        }

        private void btnSozlesmeList_Click(object sender, EventArgs e)
        {
            pSozlesmeList.Visible = true;
            pSozlesmeEkle.Visible = false;
            pAracEkle.Visible = false;
            pMusteriEkle.Visible = false;
            pMusteriListe.Visible = false;
            SozlesmeListe();
        }

        private void dtSozlesmeListesi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblIDSozlesme.Text = dtSozlesmeListesi.CurrentRow.Cells[0].Value.ToString();
            lblAracPlaka.Text = dtSozlesmeListesi.CurrentRow.Cells[18].Value.ToString();
            btnTeslimAl.Enabled = true;
        }

        private void btnTeslimAl_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult s= MessageBox.Show("'" + lblAracPlaka.Text + "' Plakalı aracı teslim almak ve sözleşmeyi silmek istediğinize emin misiniz ?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (s==DialogResult.Yes)
                {
                    if (db.baglanti.State == ConnectionState.Open)
                    {
                        db.baglanti.Close();
                    }
                    db.baglanti.Open();
                    cmd = new SqlCommand("delete from Sozlesmeler where ID='" + lblIDSozlesme.Text + "'", db.baglanti);
                    cmd.ExecuteNonQuery();
                    AracBosta();
                    SozlesmeListe();
                    MessageBox.Show("'" + lblAracPlaka.Text + "' Plakalı araç başarıyla teslim alınmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    db.baglanti.Close();
                }
            }
            catch { }

            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer2.Stop();
                pBekleme.Visible = false;
                pMusteriListe.Visible = true;
            }
        }

        private void txtAraPlaka_Click(object sender, EventArgs e)
        {
            if(txtAraPlaka.Text== "XX SVS XXXX  Kiralanan aracın plakası")
            {
                txtAraPlaka.Clear();
                txtAraPlaka.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular);
                txtAraPlaka.ForeColor = Color.Black;
            }
        }

        private void btnAraPlaka_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.baglanti.State == ConnectionState.Open)
                {
                    db.baglanti.Close();
                }
                db.baglanti.Open();
                da = new SqlDataAdapter("select * from Sozlesmeler where Arac_Plaka='"+txtAraPlaka.Text+"'",db.baglanti);
                DataSet ds1 = new DataSet();
                da.Fill(ds1);
                dtSozlesmeListesi.DataSource = ds1.Tables[0];
                db.baglanti.Close();
                button2.Enabled = true;
                if(txtAraPlaka.Text=="")
                {
                    MessageBox.Show("Arama yapabilmek için plaka girmeniz gerekiyor.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SozlesmeListe();
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
