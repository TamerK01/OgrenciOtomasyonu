CREATE DATABASE OgrenciOtomasyon;

CREATE TABLE tbl_bolumler
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	bolumKodu		varchar(100) NOT NULL UNIQUE,
	bolumAdi		varchar(100) NOT NULL UNIQUE,
	bolumBaskani	varchar(100) NOT NULL
);

CREATE TABLE tbl_akademisyenler
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	akaNo			int NOT NULL UNIQUE,
	akaTC			varchar(100) NOT NULL UNIQUE,
	akaAdi			varchar(100) NOT NULL,
	akaSoyadi		varchar(100) NOT NULL,
	akaBolum		varchar(100) NOT NULL FOREIGN KEY REFERENCES tbl_bolumler(bolumKodu),
	akaTel			varchar(50),
	akaAdres		text
);

CREATE TABLE tbl_ogrenciler
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ogrNo			int NOT NULL UNIQUE,
	ogrTC			varchar(100) NOT NULL UNIQUE,
	ogrAdi			varchar(100) NOT NULL,
	ogrSoyadi		varchar(100) NOT NULL,
	ogrTel			varchar(50),
	ogrAdres		text,
	ogrBolum		varchar(100) NOT NULL FOREIGN KEY REFERENCES tbl_bolumler(bolumKodu),
	ogrGNO			float NOT NULL
);

CREATE TABLE tbl_bilgiIslem
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	bilgiNo			int NOT NULL UNIQUE,
	bilgiTC			int NOT NULL UNIQUE,
	bilgiAdi		varchar(100) NOT NULL,
	bilgiSoyadi		varchar(100) NOT NULL,
	bilgiBolum		varchar(100) NOT NULL,
	bilgiTel		varchar(50),
	bilgiAdres		varchar(50)
);

CREATE TABLE tbl_dersler
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	dersKodu		varchar(100) NOT NULL UNIQUE,
	dersAdi			varchar(100) NOT NULL,
	dersKredi		int NOT NULL,
	dersBolum		varchar(100),
	dersSinif		varchar(100)
);

CREATE TABLE tbl_harfSistemi
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	harfAdi			varchar(10) NOT NULL,
	harfNotu		float NOT NULL,
);

CREATE TABLE tbl_notlar
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ogrNo			int NOT NULL FOREIGN KEY REFERENCES tbl_ogrenciler(ogrNo),
	dersKodu		varchar(100) NOT NULL FOREIGN KEY REFERENCES tbl_dersler(dersKodu),
	dersVize		float NOT NULL,
	dersFinal		float NOT NULL,
	dersOrt			float NOT NULL,
	dersHarfID		int NOT NULL FOREIGN KEY REFERENCES tbl_harfSistemi(id)
);

CREATE TABLE tbl_devamsizlik
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ogrNo			int NOT NULL FOREIGN KEY REFERENCES tbl_ogrenciler(ogrNo),
	dersKodu		varchar(100) NOT NULL FOREIGN KEY REFERENCES tbl_dersler(dersKodu),
	devamTarih		varchar(100) NOT NULL,
	akaNo			int NOT NULL FOREIGN KEY REFERENCES tbl_akademisyenler(akaNo)
);

CREATE TABLE tbl_ogrenciSistem
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ogrNo			int NOT NULL FOREIGN KEY REFERENCES tbl_ogrenciler(ogrNo),
	ogrSifre		varchar(500) NOT NULL,
	ogrGiris		varchar(100) NOT NULL,
	ogrYetki		int NOT NULL DEFAULT 0 /* Standart Yetki - 1 Tam Yetki*/
);

CREATE TABLE tbl_akademisyenSistem
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	akaNo			int NOT NULL FOREIGN KEY REFERENCES tbl_akademisyenler(akaNo),
	akaSifre		varchar(100) NOT NULL,
	akaGiris		varchar(100) NOT NULL,
	akaYetki		int NOT NULL DEFAULT 0 /* Standart Yetki - 1 Tam Yetki*/
);

CREATE TABLE tbl_bilgiIslemSistemi
(
	id				int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	bilgiNo			int NOT NULL FOREIGN KEY REFERENCES tbl_bilgiIslem(bilgiNo),
	bilgiSifre		varchar(100) NOT NULL,
	bilgiGiris		varchar(100) NOT NULL,
	bilgiYetki		int NOT NULL DEFAULT 0 /* Standart Yetki - 1 Tam yetki*/
);

CREATE TABLE tbl_ayarlar
(
	id			int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ogrKayit	int NOT NULL DEFAULT 0,
	akaKayit	int NOT NULL DEFAULT 0,
	bilgiKayit	int NOT NULL DEFAULT 0,
	ogrGiris	int NOT NULL DEFAULT 1,
	akaGiris	int NOT NULL DEFAULT 1
);

/*AYARLAR*/
INSERT INTO tbl_ayarlar (bilgiKayit,akaKayit,ogrKayit,akaGiris,ogrGiris) VALUES (1,0,0,0,0);
/*BOLUMLER*/
INSERT INTO tbl_bolumler (bolumKodu,bolumAdi,bolumBaskani) VALUES ('BLG','Bilgisayar Mühendisliði','Tamer Karalürt');
INSERT INTO tbl_bolumler (bolumKodu,bolumAdi,bolumBaskani) VALUES ('EDM','Endüstri Mühendisliði','Ahmet SoyadAhmet');
INSERT INTO tbl_bolumler (bolumKodu,bolumAdi,bolumBaskani) VALUES ('ISM','Ýnþaat Mühendisliði','Ali SoyadAli');
INSERT INTO tbl_bolumler (bolumKodu,bolumAdi,bolumBaskani) VALUES ('EEM','Elektrik-Elektronik Mühendisliði','Veli SoyadVeli');
/*AKADEMISYENLER*/
INSERT INTO tbl_akademisyenler (akaNo,akaAdi,akaSoyadi,akaTC,akaBolum,akaTel,akaAdres) VALUES (01,'Tamer','Karalürt','123456','BLG','532','ADANA');
INSERT INTO tbl_akademisyenler (akaNo,akaAdi,akaSoyadi,akaTC,akaBolum,akaTel,akaAdres) VALUES (02,'Ahmet','SoyadAhmet','123','EDM','533','MERSIN');
INSERT INTO tbl_akademisyenler (akaNo,akaAdi,akaSoyadi,akaTC,akaBolum,akaTel,akaAdres) VALUES (03,'Ali','SoyadAli','1234','ISM','531','ANTALYA');
INSERT INTO tbl_akademisyenler (akaNo,akaAdi,akaSoyadi,akaTC,akaBolum,akaTel,akaAdres) VALUES (04,'Veli','SoyadVeli','12345','EEM','542','KAYSERI');
/*BILGI ISLEM PERSONELI*/
BEGIN TRAN
INSERT INTO tbl_bilgiIslem (bilgiNo,bilgiAdi,bilgiSoyadi,bilgiTC,bilgiBolum,bilgiTel,bilgiAdres) VALUES (155050010,'Tamer','Karalürt','12345','BLG','532','ADANA');
INSERT INTO tbl_bilgiIslemSistemi (bilgiNo,bilgiSifre,bilgiGiris,bilgiYetki) VALUES (155050010,'827ccb0eea8a706c4c34a16891f84e7b',CURRENT_TIMESTAMP,1);
COMMIT
/*DERSLER*/
BEGIN TRAN
INSERT INTO tbl_dersler (dersKodu,dersAdi,dersKredi,dersBolum,dersSinif) VALUES ('PHY101','Fizik 1','4','BLG','1');
INSERT INTO tbl_dersler (dersKodu,dersAdi,dersKredi,dersBolum,dersSinif) VALUES ('CSE111','Bil.Müh.Giriþ','3','BLG','1');
INSERT INTO tbl_dersler (dersKodu,dersAdi,dersKredi,dersBolum,dersSinif) VALUES ('MAT103','Matematik 1','4','BLG','1');
INSERT INTO tbl_dersler (dersKodu,dersAdi,dersKredi,dersBolum,dersSinif) VALUES ('CSE255','Elektronik Temelleri','3','EEM','2');
INSERT INTO tbl_dersler (dersKodu,dersAdi,dersKredi,dersBolum,dersSinif) VALUES ('CSE323','C/C++ Programlama Dili','3','BLG','3');
INSERT INTO tbl_dersler (dersKodu,dersAdi,dersKredi,dersBolum,dersSinif) VALUES ('CSE309','Veritabaný','3','EDM','3');
INSERT INTO tbl_dersler (dersKodu,dersAdi,dersKredi,dersBolum,dersSinif) VALUES ('EEE303','Elektronik 1','3','ISM','3');
COMMIT
/*OGRENCILER*/
BEGIN TRAN
INSERT INTO tbl_ogrenciler (ogrNo,ogrAdi,ogrSoyadi,ogrTC,ogrBolum,ogrTel,ogrAdres,ogrGNO) VALUES ('2016103001','Ahmet','SoyadAhmet','12201610301','BLG','0531','MERSIN',3.50);
INSERT INTO tbl_ogrenciSistem (ogrNo,ogrSifre,ogrGiris,ogrYetki) VALUES ('2016103001','827ccb0eea8a706c4c34a16891f84e7b',CURRENT_TIMESTAMP,0);
INSERT INTO tbl_ogrenciler (ogrNo,ogrAdi,ogrSoyadi,ogrTC,ogrBolum,ogrTel,ogrAdres,ogrGNO) VALUES ('2016103002','Ali','SoyadAli','12201610302','EDM','0533','ADANA',2.50);
INSERT INTO tbl_ogrenciSistem (ogrNo,ogrSifre,ogrGiris,ogrYetki) VALUES ('2016103002','827ccb0eea8a706c4c34a16891f84e7b',CURRENT_TIMESTAMP,0);
INSERT INTO tbl_ogrenciler (ogrNo,ogrAdi,ogrSoyadi,ogrTC,ogrBolum,ogrTel,ogrAdres,ogrGNO) VALUES ('2016103003','Ayþe','SoyadAyþe','12201610303','ISM','0532','ISTANBUL',2.00);
INSERT INTO tbl_ogrenciSistem (ogrNo,ogrSifre,ogrGiris,ogrYetki) VALUES ('2016103003','827ccb0eea8a706c4c34a16891f84e7b',CURRENT_TIMESTAMP,0);
COMMIT
/*HARFLI NOT*/
BEGIN TRAN
INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES ('AA',4.0); /*1*/
INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES ('BA',3.5); /*2*/
INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES ('BB',3.0); /*3*/
INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES ('CB',2.5); /*4*/
INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES ('CC',2.0); /*5*/
INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES ('DC',1.5); /*6*/
INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES ('DD',1.0); /*7*/
INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES ('FD',0.5); /*8*/
INSERT INTO tbl_harfSistemi (harfAdi,harfNotu) VALUES ('FF',0.0); /*9*/
COMMIT
/*NOTLAR*/
BEGIN TRAN
INSERT INTO tbl_notlar (ogrNo,dersKodu,dersVize,dersFinal,dersOrt,dersHarfID) VALUES ('2016103001','PHY101',60,70,(60*0.4)+(70*0.6),3); /*66*/
INSERT INTO tbl_notlar (ogrNo,dersKodu,dersVize,dersFinal,dersOrt,dersHarfID) VALUES ('2016103001','CSE111',50,30,(50*0.4)+(30*0.6),7); /*38*/
INSERT INTO tbl_notlar (ogrNo,dersKodu,dersVize,dersFinal,dersOrt,dersHarfID) VALUES ('2016103001','MAT103',40,40,(40*0.4)+(40*0.6),7); /*40*/
INSERT INTO tbl_notlar (ogrNo,dersKodu,dersVize,dersFinal,dersOrt,dersHarfID) VALUES ('2016103001','CSE255',80,90,(80*0.4)+(90*0.6),1); /*86*/
COMMIT