using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Newtonsoft.Json;
using Portal.Models;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
		private frontendContext context;
		public MenuController(){
			context = new frontendContext();
		}
		
		[HttpGet("loadData")]	
		public async Task<IActionResult> GetLoadData(){
			var resMenu = context.Menu.ToList();
			if(resMenu.Count==0){
				string myJsonString = "[{'id':1,'parentId':null,'title':'Beranda','isActive':true,'order':0,'url':'home','mode':'feature'},{'id':2,'parentId':null,'title':'Tentang KSSK','isActive':true,'order':1,'url':'mandat','mode':'single'},{'id':3,'parentId':2,'title':'Mandat','isActive':true,'order':3,'url':'mandat','mode':'single'},{'id':4,'parentId':2,'title':'Anggota','isActive':true,'order':4,'url':'anggota','mode':'single'},{'id':5,'parentId':2,'title':'Sekretariat','isActive':true,'order':5,'url':'sekretariat','mode':'single'},{'id':6,'parentId':2,'title':'Struktur Organisasi','isActive':false,'order':6,'url':'struktur','mode':'single'},{'id':7,'parentId':null,'title':'Kegiatan','isActive':true,'order':7,'url':'kegiatan','mode':'single'},{'id':8,'parentId':7,'title':'Rapat','isActive':true,'order':8,'url':'rapat','mode':'multiplefoto'},{'id':9,'parentId':7,'title':'Gallery Foto/Video','isActive':true,'order':9,'url':'gallery','mode':'multiplefoto'},{'id':10,'parentId':null,'title':'Publikasi','isActive':true,'order':10,'url':'publikasi','mode':'multiplefile'},{'id':11,'parentId':10,'title':'Siaran Pers','isActive':true,'order':11,'url':'pr','mode':'multiplefile'},{'id':12,'parentId':10,'title':'Q&A','isActive':true,'order':13,'url':'qa','mode':'single'},{'id':13,'parentId':null,'title':'Peraturan','isActive':true,'order':14,'url':'peraturan','mode':'multiplefilesmall'},{'id':14,'parentId':2,'title':'Sejarah KSSK','isActive':true,'order':2,'url':'sejarahkssk','mode':'single'},{'id':15,'parentId':10,'title':'Sosialisasi','isActive':true,'order':12,'url':'sosialisasi','mode':'single'},{'id':16,'parentId':13,'title':'UU','isActive':true,'order':15,'url':'uu','mode':'multiplefile'},{'id':17,'parentId':13,'title':'Peraturan BI','isActive':true,'order':16,'url':'peraturanbi','mode':'multiplefile'},{'id':18,'parentId':13,'title':'Peraturan OJK','isActive':true,'order':17,'url':'peraturanojk','mode':'multiplefile'},{'id':19,'parentId':13,'title':'Peraturan LPS','isActive':true,'order':18,'url':'peraturanlps','mode':'multiplefile'},{'id':20,'parentId':13,'title':'Permenkeu','isActive':true,'order':19,'url':'permenkeu','mode':'multiplefile'},{'id':21,'parentId':null,'title':'Hubungi Kami','isActive':false,'order':0,'url':'contactus','mode':'single'},{'id':22,'parentId':null,'title':'Peta Situs','isActive':false,'order':0,'url':'sitemap','mode':'single'},{'id':23,'parentId':13,'title':'Keputusan KSSK','isActive':true,'order':20,'url':'peraturan','mode':'multiplefilesmall'}]";
				List<Menu> menu =  JsonConvert.DeserializeObject<List<Menu>>(myJsonString);
				int i = 0;
				foreach(var mn in menu){
					mn.CreatedBy = 1;
					mn.CreatedDate = DateTime.Now.AddSeconds(i++);
					context.Menu.Add(mn);
				}
				await context.SaveChangesAsync();
			}
			var resContent = context.Content.ToList();
			if(resContent.Count==0){
				string myJsonString = "[{'id':1,'menuId':1,'url':'home','title':'Home','content1':'Home','isActive':true},{'id':2,'menuId':3,'url':'mandat','title':'Mandat','content1':'<section  style=\"font-size:16px\" class=\"popular-courses-area section-gap courses-page\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-12\"><img class=\"img-judul\" src=\"assets/img/about-img.jpg\" alt=\"\"><h1>Mandat Komite Stabilitas Sistem Keuangan&nbsp;</h1><p><br></p><p>Dalam menjaga stabilitas sistem keuangan, tugas dan wewenang KSSK antara lain tugas</p><ul style=\"text-align: left;\"><li>1. Melakukan koordinasi dalam rangka pemantauan dan pemeliharaan SSK</li><li>2. Melakukan penanganan krisis sistem keuangan</li><li>3. Melakukan penanganan permasalahan bank Sistemik, baik dalam kondisi SSK normal maupun kondisi krisis sistem keuanganwewenang</li><li>4. Menetapkan kriteria dan indikator penilaian SSK</li><li>5. Melakukan penilaian SSK berdasarkan masukan anggota</li><li>6. Menetapkan langkah koordinasi untuk mencegah krisis sistem keuangan</li><li>7. Rekomendasi status SSK kepada Presiden</li><li>8. Rekomendasi kepada Presiden langkah penanganan Krisis</li><li>9. Penyerahan penanganan permasalahan solvabilitas Bank Sistemik kepada LPS</li><li>10. Penetapan langkah yang harus dilakukan oleh anggota KSSK</li><li>11. Penetapan keputusan pembelian SBN milik LPS oleh BI</li><li>12. Rekomendasi kepada Presiden untuk penyelenggaraan dan pengakhiran Program Restrukturisasi Perbankan (PRP)</li></ul><p>Dalam menjalankan tugas dan wewenang, KSSK dibantu oleh sekretariat KSSK yang dipimpin Sekretaris KSSK. Organisasi dan tata kerja sekretariat KSSK ditetapkan oleh Menteri Keuangan sesuai dengan ketentuan perundang-undangan.</p></div></div></div></section>','isActive':true},{'id':3,'menuId':4,'url':'anggota','title':'Anggota','content1':'<section  style=\"font-size:16px\" class=\"event-details-area section-gap\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"judul\"><h4> Menteri Keuangan</h4></div><div class=\"desc-wrap\"><p>Mengevaluasi skala dan dimensi permasalahan likuiditas dan/atau solvabilitas bank/lembaga keuangan bukan bank (LKBB) yang ditengarai Berdampak Sistemik</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"judul\"><h4>Gubernur Bank Indonesia</h4></div><div class=\"desc-wrap\"><p>Menetapkan permasalahan likuiditas dan/atau masalah solvabilitas bank/lembaga keuangan bukan bank (LKBB) Berdampak Sistemik atau tidak Berdampak Sistemik</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"judul\"><h4>Ketua Dewan Komisioner OJK</h4></div><div class=\"desc-wrap\"><p>Menetapkan langkah-langkah penanganan masalah bank/lembaga keuangan bukan bank (LKBB) yang dipandang perlu dalam rangkapencegahan dan penanganan Krisis</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"judul\"><h4>Ketua Dewan Komisioner LPS</h4></div><div class=\"desc-wrap\"><p>Menetapkan langkah-langkah penanganan masalah bank/lembaga keuangan bukan bank (LKBB) yang dipandang perlu dalam rangkapencegahan dan penanganan Krisis</p><a href=\"#\">Selanjutnya</a></div></div></div></div></div></section>   ','isActive':true},{'id':4,'menuId':5,'url':'sekretariat','title':'Sekretariat','content1':'<section  style=\"font-size:16px\" class=\"event-details-area section-gap\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-12\"><img class=\"img-judul\" src=\"assets/img/sekretariat.jpg\" alt=\"\"><a href=\"#\"><h1>Sekretariat KSSK</h1></a><p><br/></p><p>Dalam penyelenggaraan tugas dan wewenangnya, KSSK dibantu oleh Komite Stabilitas Sistem Keuangan yang selanjutnya disebut Sekretariat KSSK. Sekretariat KSSK yang dibentuk melalui Peraturan Menteri Keuangan Nomor 92/PMK.01/2017 merupakan unit organisasi noneselon di lingkungan Kementerian Keuangan yang bertanggung jawab kepada Menteri Keuangan selaku Koordinator Komite Stabilitas Sistem Keuangan dan secara administratif berada di bawah Sekretaris Jenderal.</p><p>Dalam menjalankan tugas membantu pelaksanaan tugas Komite Stabilitas Sistem Keuangan baik secara substantif maupun administratif,  Sekretariat KSSK menyelenggarakan fungsi: </p><ol style=\"text-align:left;\"><li>a. perumusan tata kelola Komite Stabilitas Sistem Keuangan dan Sekretariat KSSK; </li><li>b. perumusan kerangka kerja, termasuk kriteria dan indikator, penilaian kondisi stabilitas sistem keuangan; </li><li>c. penyiapan bahan untuk penilaian terhadap kondisi stabilitas sistem keuangan berdasarkan masukan dari setiap anggota Komite Stabilitas Sistem Keuangan, beserta data dan informasi pendukung; </li><li>d. penyiapan usulan langkah koordinasi untuk mencegah krisis sistem keuangan dengan mempertimbangkan rekomendasi dari setiap anggota Komite Stabilitas Sistem Keuangan; </li><li>e. penyiapan rekomendasi kepada Presiden untuk memutuskan perubahan status stabilitas sistem keuangan, langkah penanganan krisis sistem keuangan, serta penyelenggaraan dan pengakhiran Program Restrukturisasi Perbankan; </li><li>f. penyiapan penyerahan penanganan permasalahan solvabilitas bank sistemik kepada Lembaga Penjamin Simpanan beserta usulan langkah yang harus dilakukan oleh anggota Komite Stabilitas Sistem Keuangan untuk mendukung pelaksanaan penanganan permasalahan bank sistemik oleh Lembaga Penjamin Simpanan;</li><li>g. penyiapan keputusan pembelian Surat Berharga Negara yang dimiliki Lembaga Penjamin Simpanan oleh Bank Indonesia guna penanganan bank;</li><li>h. penyiapan masukan bagi anggota Komite Stabilitas Sistem Keuangan undangan mengenai materi di bidang jasa peraturan keuangan mempengaruhi stabilitas sistem keuangan; </li><li>i. penyiapan bahan monitoring dan evaluasi pelaksanaan keputusan Komite Stabilitas Sistem Keuangan;</li><li>j. pengelolaan data dan informasi terkait Stabilitas Sistem Keuangan;</li><li>k. pelaksanaan kajian risiko dan hukum atas kebijakan Komite Stabilitas Sistem Keuangan; </li><li>l. pengelolaan komunikasi publik dan hubungan antar lembaga;</li><li>m. pelaksanaan urusan administrasi Sekretariat KSSK dan Komite Stabilitas Sistem Keuangan; dan </li><li>n. pelaksanaan tugas lain yang ditetapkan Komite Stabilitas Sistem Keuangan</li></ul><p>Sekretariat KSSK dipimpin oleh Sekretaris Komite Stabilitas Sistem Keuangan dan terdiri atas:</p><ul><li>a. Direktur Asesmen dan Kebijakan Stabilitas Sistem Keuangan;</li><li>b. Direktur Manajemen Risiko dan Hukum; </li><li>c. Divisi Manajemen Kantor; dan</li></ul><img class=\"img-fluid fr-fic fr-dii\" src=\"assets/img/struktur-organisasi.jpg\" alt=\"\"></div></div></div></section>','isActive':true},{'id':5,'menuId':6,'url':'struktur','title':'Struktur Organisasi','content1':'<section  style=\"font-size:16px\" class=\"popular-courses-area courses-page\"><div class=\"container\"><div class=\"row justify-content-center\"><div class=\"single-popular-carusel col-lg-8 col-md-8\"><div class=\"thumb-wrap relative\"><img class=\"img-fluid fr-fic fr-dii\" src=\"assets/img/struktur-organisasi.jpg\" alt=\"\"></div><div class=\"details\"><a href=\"course-details.html\">&nbsp;<h4>Struktur Organisasi</h4>&nbsp;</a><p>When television was young, there was a hugely popular show based on the still popular fictional characte</p></div></div></div></div></section>','isActive':true},{'id':6,'menuId':7,'url':'kegiatan','title':'Rapat mengenai ketahanan ekonomi','content1':null,'isActive':true},{'id':7,'menuId':8,'url':'rapat','title':'Rapat Berkala Komite Stabilitas Sistem Keuangan Triwulan II Tahun 2018','content1':'<div style=\"font-size: 16px\"><p>Pada hari Kamis, tanggal 26 Juli 2018 bertempat di Lembaga Penjamin Simpanan, Komite Stabilitas Sistem Keuangan (KSSK) telah menyelenggarakan rapat berkala dalam rangka koordinasi pemantauan dan pemeliharaan Stabilitas Sistem Keuangan. Berdasarkan hasil pemantauan lembaga anggota KSSK terhadap perkembangan perekonomian, moneter, fiskal, pasar keuangan, lembaga jasa keuangan, dan penjaminan simpanan selama Triwulan II tahun 2018 serta mempertimbangkan perkembangan hingga 20 Juli 2018, KSSK menyimpulkan stabilitas sistem keuangan Triwulan II 2018 tetap terjaga di tengah meningkatnya tekanan global.</p></div>','isActive':true},{'id':8,'menuId':16,'url':'uu','title':'Undang-Undang','content1':null,'isActive':true},{'id':9,'menuId':23,'url':'peraturan','title':'Keputusan KSSK','content1':null,'isActive':true},{'id':10,'menuId':11,'url':'pr','title':'KSSK Berkomitmen Memperkuat Koordinasi Untuk Menjaga Stabilitas Sistem Keuangan di Tengah Meningkatnya Tekanan Global','content1':null,'isActive':true},{'id':11,'menuId':9,'url':'gallery','title':'Malam Apresiasi Bapak Agus Martowardojo','content1':'<div style=\"font-size:16px\"><p>Tanggal 21 Mei 2018 bersamaan dengan peresmian kantor Sekretariat KSSK, dilaksanakan juga malam apresiasi kepada Bapak Agus Martowardojo yang dalam waktu dekat akan mengakhiri masa tugasnya sebagai Gubernur Bank Indonesia. Pada kesempatan tersebut Menteri Keuangan, Ketua Dewan Komisioner Otoritas Jasa Keuangan, Ketua Dewan Komisioner Lembaga Penjamin Simpanan menyampaikan apresiasi serta kesan dan pesan atas kontribusi Bapak Agus Martowardojo dalam meletakkan pondasi yang kuat untuk menjaga stabilitas sistem keuangan melalui dukungannya dalam penyusunan UU No. 16 tahun 2016 tentang Pencegahan dan Penanganan Krisis Sistem Keuangan (UU PPKSK).</p></div>','isActive':true},{'id':12,'menuId':9,'url':'gallery','title':'Peresmian Kantor Sekretariat Komite Stabilitas Sistem Keuangan Kementerian Keuangan','content1':'<div style=\"font-size:16px\"><p>Jakarta, 22 Mei 2018 Menteri Keuangan Sri Mulyani Indrawati bersama Gubernur Bank Indonesia (BI) Agus Martowardojo, Ketua Dewan Komisioner Otoritas Jasa Keuangan (OJK) Wimboh Santoso serta Ketua Dewan Komisioner Lembaga Penjamin Simpanan (LPS) Halim Alamsyah meresmikan Kantor Sekretariat Komite Stabilitas Sistem Keuangan (KSSK) di Gedung Djuanda II Lantai 18, Kementerian Keuangan pada Senin, (21/05).</p><p>Dalam acara peresmian tersebut, turut hadir para pimpinan di lingkungan Kementerian Keuangan, Bank Indonesia, Otoritas Jasa Keuangan dan Lembaga Penjamin Simpanan.</p><p>Dalam sambutannya, Menteri Keuangan menyampaikan bahwa Sekretariat KSSK memiliki tugas untuk mendukung stabilitas sistem keuangan serta menjaga stabilitas sektor dan institusi keuangan. Oleh karena itu penting agar Sekretariat KSSK dapat melengkapi prosedur operasi standar, memperbaiki protokol manajemen dan membangun kerjasama yang erat dari keempat lembaga KSSK. Menteri Keuangan meminta dukungan dari pada pimpinan BI, OJK dan LPS atas kepada Sekretariat KSSK agar dapat bekerja dengan efektif.</p><p>Menteri Keuangan juga mengucapkan terima kasih kepada seluruh pihak yang telah memberikan dukungan atas terbangunnya kantor Sekretariat KSSK, terutama Sekretariat Jenderal. Melalui seremonial pemotongan pita dan pembukaan tirai oleh Menteri Keuangan Sri Mulyani Indrawati bersama Gubernur BI Agus Martowardojo, Ketua Dewan Komisioner OJK Wimboh Santoso dan Ketua Dewan Komisioner LPS Halim Alamsyah, kantor Sekretariat KSSK pun diresmikan.</p></div>','isActive':true},{'id':13,'menuId':21,'url':'contactus','title':'Hubungi Kami','content1':'<section class=\"event-details-area section-gap\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-8 event-details-left\"><div class=\"single-feature\"><p>Terima kasih telah menggunakan layanan HUBUNGI KAMI. KSSK berkomitmen memberikan pelayanan terbaik kepada masyarakat. Untuk kemudahan akses layanan informasi, KSSK menyediakan layanan informasi yang dapat dihubungi melalui:</p><div class=\"cover_blue\"><h3>HUBUNGI KAMI</h3><ul style=\"text-align:left;\"><li><span class=\"icon maps\"></span><font>Sekretariat: Gedung Djuanda II Lantai 18 <br>Jalan Dr. Wahidin Nomor 1, Jakarta 10710</font><div class=\"clear\"></div></li><li><a style=\"text-decoration:none;color:#fff;\" href=\"tel:0213503504\"><span class=\"icon phone\"></span><font>Telepon: (021) 350 3504 <br> Faksimili: (021) 350 3506</font><div class=\"clear\"></div></a></li><li style=\"border:0;\"><a style=\"text-decoration:none;color:#fff;\" href=\"mailto:sekretariatkssk@kssk.go.id\"><span class=\"icon mail\"></span><font>sekretariatkssk@kssk.go.id</font><div class=\"clear\"></div></a></li></ul></div></div></div><div class=\"col-lg-4 event-details-right\"><div class=\"single-event-details\"><h4>Kirim Email</h4><br><form action=\"mailto:someone@example.com\" method=\"post\" enctype=\"text/plain\"><div class=\"form-group\"><input class=\"form-control\" placeholder=\"Nama\" name=\"Nama\" type=\"text\"></div><div class=\"form-group\"><input class=\"form-control\" placeholder=\"Email\" name=\"Email\" type=\"text\"></div><div class=\"form-group\"><input class=\"form-control\" placeholder=\"Judul\" name=\"Judul\" type=\"text\"></div><textarea class=\"form-control\" placeholder=\"Pesan\" name=\"Pesan\"></textarea><br><br><input type=\"submit\" value=\"Send\" class=\"primary-btn text-uppercase\">&nbsp;&nbsp;&nbsp;<input type=\"reset\" value=\"Reset\" class=\"primary-btn text-uppercase\"></form></div></div></div></div></section>','isActive':true},{'id':14,'menuId':22,'url':'sitemap','title':'Peta Situs','content1':'<div class=\"event-details-area section-gap\"><div class=\"container\"><table class=\"table table-hover table-sm\">\t<thead class=\"thead-light\">\t<tr>\t\t<th>No</th>\t\t<th>Menu</th>\t\t<th>Sub Menu</th>\t</tr>\t</thead>\t<tbody>\t<tr>\t\t<td>1</td>\t\t<td><a href=\"/home\">Beranda</a></td>\t\t<td></td>\t</tr>\t<tr>\t\t<td>2</td>\t\t<td>Tentang KSSK</td>\t\t<td><a href=\"/sejarahkssk\">Sejarah KSSK</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/mandat\">Mandat</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/anggota\">Anggota</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/struktur\">Struktur KSSK</a></td>\t</tr>\t<tr>\t\t<td>3</td>\t\t<td>Kegiatan</td>\t\t<td><a href=\"/rapat\">Rapat</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/gallery\">Gallery Foto / Video</a></td>\t</tr>\t<tr>\t\t<td>4</td>\t\t<td>Publikasi</td>\t\t<td><a href=\"/pr\">Siaran Pers</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/sosialisasi\">Sosialisasi</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/qa\">Q & A</a></td>\t</tr>\t<tr>\t\t<td>5</td>\t\t<td>Peraturan</td>\t\t<td><a href=\"/uu\">UU</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/peraturanbi\">Peraturan BI</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/peraturanojk\">Peraturan OJK</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/peraturanlps\">Peraturan LPS</a></td>\t</tr>\t<tr>\t\t<td></td>\t\t<td></td>\t\t<td><a href=\"/permenkeu\">Permenkeu</a></td>\t</tr>\t</tbody></table></div></div>','isActive':true},{'id':15,'menuId':17,'url':'peraturanbi','title':'Peraturan BI','content1':null,'isActive':true},{'id':16,'menuId':18,'url':'peraturanojk','title':'Peraturan OJK','content1':null,'isActive':true},{'id':17,'menuId':19,'url':'peraturanlps','title':'Peraturan LPS','content1':null,'isActive':true},{'id':18,'menuId':20,'url':'permenkeu','title':'Peraturan Kementerian Keuangan','content1':null,'isActive':true},{'id':19,'menuId':12,'url':'qa','title':'Questions and Answers','content1':'<div class=\"event-details-area section-gap\"><div class=\"container\"><table class=\"table table-hover table-sm\"><thead class=\"thead-light\"><tr><th>No</th><th>Questions</th><th>Answers</th></tr></thead><tbody><tr><td>1</td><td>Kapan KSSK dibentuk?</td><td><p>KSSK dibentuk tahun 2016 berdasarkan Undang - Undang Nomor 9 Tahun 2016 tentang Pencegahan dan Penanganan Krisis Sistem Keuangan</p></td></tr><tr><td>2</td><td>Siapa Anggota KSSK ?</td><td><p>KSSK beranggotakan :&nbsp;</p><p>- Menteri Keuangan sebagai koordinator merangkap anggota dengan hak suara;</p><p>- Gubernur Bank Indonesia sebagai anggota dengan hak suara;</p><p>- Ketua Dewan Komisioner Otoritas Jasa Keuangan sebagai anggota dengan hak suara; dan</p><p>- Ketua Dewan Komisioner Lembaga Penjamin Simpanan sebagai anggota tanpa hak suara.</p></td></tr></tbody></table></div></div>','isActive':true},{'id':20,'menuId':11,'url':'pr','title':'Stabilitas Sistem Keuangan Triwulan I 2018 Terkendali Di Tengah Volatilitas Pasar Keuangan Global','content1':null,'isActive':true},{'id':21,'menuId':11,'url':'pr','title':'KSSK Menilai Stabilitas Sistem Keuangan Triwulan III 2017 Dalam Kondisi Normal','content1':null,'isActive':true},{'id':22,'menuId':14,'url':'sejarahkssk','title':'Sejarah KSSK','content1':'<section  style=\"font-size:16px\" class=\"popular-courses-area section-gap courses-page\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-12\"><img class=\"img-judul\" src=\"assets/img/about-img.jpg\" alt=\"\"><h1>Sejarah Komite Stabilitas Sistem Keuangan&nbsp;</h1><p><br></p><p>Sebagai negara dengan sistem perekonomian terbuka, Indonesia terpapar langsung dengan dinamika kondisi perekonomian regional maupun global. Sejak periode dekade 1990, Indonesia telah menghadapi atau terimbas rangkaian krisis keuangan yang terjadi baik di tingkat nasional, regional maupun global. Pengalaman menghadapi krisis regional di kawasan Asia pada tahun 1997/1998, krisis reksa dana domestik tahun 2005, dan krisis keuangan global yang dipicu krisis US subprime mortgage tahun 2008, yang berlanjut dengan krisis utang di negara-negara kawasan Eropa tahun 2011 telah memberikan pelajaran berharga bahwa krisis dapat terjadi di mana saja dan kapan saja, sehingga dibutuhkan kesiapan untuk menghadapi kondisi tidak normal sekaligus dampaknya.</p><p>Belajar dari krisis yang terjadi pada tahun 1997-1998 tersebut, Pemerintah secara terus menerus melakukan berbagai upaya perbaikan untuk membangun sistem keuangan yang lebih tangguh dan lebih siap dalam menghadapi kondisi tidak normal. Upaya perbaikan tersebut meliputi penataan kembali kelembagaan yang ada. Penataan kelembagaan yang dilakukan Pemerintah yaitu penegasan peran Bank Indonesia selaku otoritas moneter yang independen serta pengelola sistem pembayaran melalui amandemen Undang-Undang Nomor 23 Tahun 1999 tentang Bank Indonesia serta peran Menteri Keuangan selaku otoritas fiskal dan pengelola keuangan negara melalui penerbitan Undang-Undang Nomor 17 Tahun 2003 tentang Keuangan Negara, serta Undang-Undang Nomor 1 Tahun 2004 tentang Perbendaharaan Negara. Pemerintah juga mendirikan lembaga-lembaga yang berfungsi untuk memperkuat industri sektor keuangan yaitu pendirian Lembaga Penjamin Simpanan (LPS) selaku pelaksana program penjaminan simpanan dan otoritas resolusi bank melalui penerbitan Undang-Undang Nomor 24 Tahun 2004 tentang Lembaga Penjamin Simpanan dan pendirian Otoritas Jasa Keuangan (OJK) selaku regulator dan supervisor industri jasa keuangan yang terintegrasi melalui penerbitan Undang-Undang Nomor 21 Tahun 2011 tentang Otoritas Jasa Keuangan.</p><p>Dalam rangka menjaga stabilitas sektor keuangan, seluruh otoritas dan lembaga yang terkait harus tetap waspada karena tekanan terhadap sistem keuangan dapat terjadi setiap saat. Hal ini dapat terjadi mengingat dinamika perekonomian global yang bergerak sangat cepat dan interaksi antarpasar keuangan yang demikian erat satu sama lain. Forum Koordinasi Stabilitas Sistem Keuangan dibentuk sebagai kerangka kerja dalam melakukan koordinasi dan kerja sama antar lembaga. Forum ini terbentuk berdasarkan Memorandum of Understanding (MoU)  antara Kementerian Keuangan, Bank Indonesia, Otoritas Jasa Keuangan, dan Lembaga Penjamin Simpanan. Namun demikian, untuk menciptakan sistem keuangan yang sehat dan stabil perlu dilakukan dengan cara menetapkan mekanisme koordinasi yang jelas, pengambilan kebijakan serta penetapan langkah-langkah yang diperlukan dalam rangka penanganan kondisi stabilitas sistem keuangan tidak normal. Selanjutnya, Komite Stabilitas Sistem Keuangan (KSSK) dibentuk untuk menggantikan Forum Koordinasi Stabilitas Sistem Keuangan yang dinilai kurang memadai karena dapat diartikan hanya sebagai wadah untuk berkoordinasi tanpa memiliki kewenangan untuk memutuskan dan menetapkan suatu produk hukum. </p><p>Komite Stabilitas Sistem Keuangan (KSSK) dibentuk tahun 2016 berdasarkan Undang - Undang Nomor 9 Tahun 2016 tentang Pencegahan dan Penanganan Krisis Sistem Keuangan sebagai protokol utama bagi koordinasi dan kerja sama antara Kementerian Keuangan, Bank Indonesia, Otoritas Jasa Keuangan, dan Lembaga Penjamin Simpanan dan memiliki kewenangan untuk menetapkan suatu produk hukum. Sesuai dengan UU Pencegahan dan Penanganan Krisis Sistem Keuangan, KSSK mendapatkan mandat untuk menyelenggarakan pencegahan dan penanganan krisis sistem keuangan untuk melaksanakan kepentingan dan ketahanan negara di bidang perekonomian. Beranggotakan Menteri Keuangan (merangkap sebagai koordinator), Gubernur Bank Indonesia dan Ketua Dewan Komisioner Otoritas Jasa Keuangan sebagai anggota dengan hak suara, serta Ketua Dewan Komisioner Lembaga Penjamin Simpanan sebagai anggota tanpa hak suara.</p></div></div></div></section>','isActive':true},{'id':23,'menuId':11,'url':'pr','title':'KSSK Memperkuat Kebijakan Untuk Menjaga Stabilitas Sistem Keuangan di Tengah Meningkatnya Tekanan Global','content1':null,'isActive':true},{'id':24,'menuId':9,'url':'gallery','title':'Diskusi Publik KSSK','content1':'<div style=\"font-size:16px\"><p>Pada tanggal 1 November 2018 bertempat di Aula Mezzanine Kementerian Keuangan, Komite Stabilitas Sistem Keuangan menggelar Diskusi Publik untuk pertama kalinya. Diskusi Publik dengan tema \"Respon Kebijakan Sistem Keuangan terhadap Perkembangan Perekonomian Terkini : Fundamental Perekonomian, Nilai Tukar, dan Defisit Neraca Transaksi Berjalan\" dihadiri oleh 150 peserta yang terdiri dari akademisi, ekonom, perbankan dan asosiasi, serta praktisi pasar keuangan. Menteri Keuangan Sri Mulyani Indrawati bersama Gubernur Bank Indonesia (BI) Perry Warjiyo, Kepala Eksekutif Pengawas Perbankan Otoritas Jasa Keuangan (OJK) Heru Kristiyana serta Kepala Eksekutif Lembaga Penjamin Simpanan (LPS) Fauzi Ichsan menjadi panelis dalam diskusi yang di moderatori oleh Anton Gunawan, Kepala Ekonom Bank Mandiri.</p></div>','isActive':true},{'id':25,'menuId':8,'url':'rapat','title':'Rapat Berkala Komite Stabilitas Sistem Keuangan Triwulan III Tahun 2018','content1':'<div style=\"font-size: 16px\"><p>Rapat Berkala KSSK triwulan III tahun 2018 dilaksanakan pada tanggal 25 Oktober 2018 bertempat di Bank Indonesia. Jalannya rapat dipimpin oleh keempat anggota KSSK serta jajaran pimpinan dari Kementerian Keuangan, Bank Indonesia, Otoritas Jasa Keuangan dan Lembaga Penjamin Simpanan.</p></div>','isActive':true},{'id':26,'menuId':9,'url':'gallery','title':'Konferensi Pers KSSK Oktober 2018','content1':'<div style=\"font-size:16px\"><p>KSSK menggelar konferensi pers untuk menyampaikan informasi kepada publik terkait kondisi perekonomian triwulan III tahun 2018. KSSK menyatakan bahwa dinamika fundamental perekonomian masih berada pada kondisi yang terkendali dan KSSK akan memperkuat kebijakan untuk menjaga stabilitas sistem keuangan ditengah meningkatnya tekanan perekonomian global.</p></div>','isActive':true}]";
				List<Content> content =  JsonConvert.DeserializeObject<List<Content>>(myJsonString);
				int i = 0;
				foreach(var con in content){
					con.CreatedBy = 1;
					con.CreatedDate = DateTime.Now.AddSeconds(i++);
					context.Content.Add(con);
				}
				await context.SaveChangesAsync();
			}
			var resFile = context.Files.ToList();
			if(resFile.Count==0){
				string myJsonString = "[{'id':1,'contentId':11,'filename':'DSC_1535.jpg','description':null,'order':0},{'id':2,'contentId':11,'filename':'DSC_1549.jpg','description':null,'order':1},{'id':3,'contentId':11,'filename':'DSC_1579.jpg','description':null,'order':2},{'id':4,'contentId':11,'filename':'DSC_1705.jpg','description':null,'order':3},{'id':5,'contentId':11,'filename':'DSC_1706.jpg','description':null,'order':4},{'id':6,'contentId':11,'filename':'DSC_1707.jpg','description':null,'order':5},{'id':7,'contentId':8,'filename':'UU Nomor 9 Tahun 2016.pdf','description':'<p>15 April 2016</p>         <h4>UU Nomor 9 Tahun 2016</h4>         <p>          UNDANG UNDANG TENTANG PENCEGAHAN DAN PENGAMANAN KRISIS SISTEM KEUANGAN.         </p>','order':0},{'id':8,'contentId':8,'filename':'UU Nomor 21 Tahun 2011.pdf','description':'\t\t\t\t\t\t\t\t\t<p>22 November 2011</p>\t\t\t\t\t\t\t\t\t<h4>UU Nomor 21 Tahun 2011</h4>\t\t\t\t\t\t\t\t\t<p>\t\t\t\t\t\t\t\t\t\tUNDANG UNDANG TENTANG OTORITAS JASA KEUANGAN.\t\t\t\t\t\t\t\t\t</p>','order':1},{'id':9,'contentId':8,'filename':'UU Nomor 23 Tahun 1999.pdf','description':'\t\t\t\t\t\t\t\t\t<p>17 Mei 1999</p>\t\t\t\t\t\t\t\t\t<h4>UU Nomor 23 Tahun 1999</h4>\t\t\t\t\t\t\t\t\t<p>\t\t\t\t\t\t\t\t\t\tUNDANG UNDANG TENTANG BANK INDONESIA.\t\t\t\t\t\t\t\t\t</p>','order':2},{'id':10,'contentId':9,'filename':'KEP KSSK 01 2017 Protokol Manajemen Krisis  KSSK.pdf','description':'<p>27 Juli 2017</p>         <h4>KEP KSSK 01 2017 Protokol Manajemen Krisis  KSSK</h4>         <p>          PROTOKOL MANAJEMEN KRISIS KOMITE STABILITAS SISTEM KEUANGAN.         </p>','order':3},{'id':11,'contentId':9,'filename':'KEP KSSK 04 2016 Kode Etik KSSK.pdf','description':'<p>30 November 2016</p>         <h4>KEP KSSK 04 2016 Kode Etik KSSKi</h4>         <p>          KODE ETIK KOMITE STABILITAS SISTEM KEUANGAN.         </p>','order':4},{'id':12,'contentId':9,'filename':'KEP KSSK 03 2016 POS Pertukaran Data dan Informasi.pdf','description':'<p>30 November 2016</p>         <h4>KEP KSSK 03 2016 POS Pertukaran Data dan Informasi</h4>         <p>          PROSEDUR OPERASIONAL STANDAR PERTUKARAN DATA DAN INFORMASI KOMITE STABILITAS SISTEM KEUANGAN.         </p>','order':5},{'id':13,'contentId':9,'filename':'KEP KSSK 02 2016 POS Rapat Sewaktu-waktu KSSK.pdf','description':'<p>30 November 2016</p>         <h4>KEP KSSK 02 2016 POS Rapat Sewaktu-waktu KSSK</h4>         <p>          PROSEDUR OPERASIONAL STANDAR RAPAT SEWAKTU-WAKTU KOMITE STABILITAS SISTEM KEUANGAN.         </p>','order':6},{'id':14,'contentId':9,'filename':'KEP KSSK 01 2016 POS Rapat Berkala KSSK.pdf','description':'<p>30 November 2016</p>         <h4>KEP KSSK 01 2016 POS Rapat Berkala KSSK</h4>         <p>          PROSEDUR OPERASIONAL STANDAR RAPAT BERKALA KOMITE STABILITAS SISTEM KEUANGAN.         </p>','order':7},{'id':15,'contentId':20,'filename':'Siaran Pers KSSK 30 April 2018.pdf','description':'<p>Siaran Pers KSSK 30 April 2018</p>','order':1},{'id':16,'contentId':10,'filename':'Siaran Pers KSSK 31 Juli 2018.pdf','description':'<p>Siaran Pers KSSK 31 Juli 2018</p>','order':2},{'id':17,'contentId':21,'filename':'Siaran Pers KSSK 31 Okt 2017.pdf','description':'<p>Siaran Pers KSSK 31 Okt 2017</p>','order':0},{'id':18,'contentId':7,'filename':'DSC_9999.jpg','description':null,'order':0},{'id':19,'contentId':12,'filename':'DSC_1782.jpg','description':null,'order':0},{'id':20,'contentId':12,'filename':'DSC_1889.jpg','description':null,'order':1},{'id':21,'contentId':12,'filename':'DSC_1895.jpg','description':null,'order':2},{'id':22,'contentId':12,'filename':'DSC_1925.jpg','description':null,'order':3},{'id':23,'contentId':12,'filename':'DSC_1986.jpg','description':null,'order':4},{'id':24,'contentId':12,'filename':'DSC_2001.jpg','description':null,'order':5},{'id':25,'contentId':12,'filename':'DSC_2003.jpg','description':null,'order':6},{'id':26,'contentId':15,'filename':'Peraturan-BI.pdf','description':'<p>Peraturan-BI.pdf</p>','order':0},{'id':27,'contentId':16,'filename':'Peraturan-OJK-No-18-2014.pdf','description':'<p>Peraturan-OJK-No-18-2014.pdf</p>','order':0},{'id':28,'contentId':17,'filename':'Peraturan-LPS-No-2-2017.pdf','description':'<p>Peraturan-LPS-No-2-2017.pdf</p>','order':0},{'id':29,'contentId':18,'filename':'PMK-No-92-2017.pdf','description':'<p>PMK-No-92-2017.pdf</p>','order':0},{'id':30,'contentId':23,'filename':'Siaran Pers KSSK November 2018.pdf','description':'<p>Siaran Pers KSSK November 2018</p>','order':0},{'id':31,'contentId':24,'filename':'DSC_5441.jpg','description':null,'order':1},{'id':32,'contentId':24,'filename':'DSC_5463.jpg','description':null,'order':2},{'id':33,'contentId':24,'filename':'DSC_5465.jpg','description':null,'order':3},{'id':34,'contentId':24,'filename':'DSC_5570.jpg','description':null,'order':4},{'id':35,'contentId':25,'filename':'DSC_5622.jpg','description':null,'order':1},{'id':36,'contentId':25,'filename':'DSC_5625.jpg','description':null,'order':2},{'id':37,'contentId':25,'filename':'DSC_5629.jpg','description':null,'order':3},{'id':38,'contentId':26,'filename':'DSC_3714.jpg','description':null,'order':1},{'id':39,'contentId':26,'filename':'DSC_3716.jpg','description':null,'order':2},{'id':40,'contentId':26,'filename':'DSC_3731.jpg','description':null,'order':3},{'id':41,'contentId':26,'filename':'DSC_3739.jpg','description':null,'order':4},{'id':42,'contentId':26,'filename':'DSC_3744.jpg','description':null,'order':5}]";
				List<Files> files =  JsonConvert.DeserializeObject<List<Files>>(myJsonString);
				int i = 0;
				foreach(var fl in files){
					fl.CreatedBy = 1;
					fl.CreatedDate = DateTime.Now.AddSeconds(i++);
					context.Files.Add(fl);
				}
				await context.SaveChangesAsync();
			}
			var resUser = context.User.ToList();
			if(resUser.Count==0){
				string myJsonString = "[{'id':1,'username':'brandon.stark@gmail.com','password':'2EA90BF81C8CF1AFC73DA38E917C728614E90EB6031F3D1D8DB002A91D26EB32','role':'Admin','authenticator':'123'},{'id':2,'username':'1','password':'6B86B273FF34FCE19D6B804EFF5A3F5747ADA4EAA22F1D49C01E52DDB7875B4B','role':'Admin','authenticator':'123'},{'id':3,'username':'kssk@kssk.go.id','password':'5A654027471B387BDC86E7B2902D62FB0389FF8C1C7180486F4C3447F9E8A263','role':'Pengguna','authenticator':'123'}]";
				List<User> user =  JsonConvert.DeserializeObject<List<User>>(myJsonString);
				foreach(var us in user){
					context.User.Add(us);
				}
				await context.SaveChangesAsync();
			}
			var resSecureFile = context.SecureFiles.ToList();
			if(resSecureFile.Count==0){
				string myJsonString = "[{'id':1,'filename':'KEP KSSK 04 2016 Kode Etik KSSK.pdf','description':'<p>30 November 2016</p>','order':4,'createdDate':'2018-10-09T08:58:46','createdBy':1},{'id':2,'filename':'KEP KSSK 03 2016 POS Pertukaran Data dan Informasi.pdf','description':'<p>30 November 2016</p>','order':5,'createdDate':'2018-10-09T08:58:46','createdBy':1}]";
				List<SecureFiles> secFiles =  JsonConvert.DeserializeObject<List<SecureFiles>>(myJsonString);
				foreach(var secFile in secFiles){
					context.SecureFiles.Add(secFile);
				}
				await context.SaveChangesAsync();
			}
			var resNews = context.News.ToList();
			if(resNews.Count==0){
				string myJsonString = "[{'id':1,'text':'Awas Modus Penipuan Catut Nama Bank Indonesia','url':'https://www.liputan6.com/bisnis/read/3673430/awas-modus-penipuan-catut-nama-bank-indonesia'},{'id':2,'text':'Tarik Investasi, Sri Mulyani Siap Beri Insentif Tambahan','url':'https://www.liputan6.com/bisnis/read/3670700/tarik-investasi-sri-mulyani-siap-beri-insentif-tambahan'},{'id':3,'text':'OJK Bakal Atur Pelaku UKM Yang Lepas Saham ke Publik','url':'https://www.liputan6.com/bisnis/read/3672350/ojk-bakal-atur-pelaku-ukm-yang-lepas-saham-ke-publik'}]";
				List<News> listNews =  JsonConvert.DeserializeObject<List<News>>(myJsonString);
				int i = 0;
				foreach(var news in listNews){
					news.CreatedBy = 1;
					news.CreatedDate = DateTime.Now.AddSeconds(i++);
					context.News.Add(news);
				}
				await context.SaveChangesAsync();
			}
			return Ok();
		}

        [HttpGet("navigation")]		
        public IActionResult GetNavigation()
        {
			dynamic result;
			try{
				result = context.Menu.Where(a=>a.IsActive==true).OrderBy(a=>a.Order).Select(a=>new {
					id = a.Id,
					parentId = a.ParentId,
					title = a.Title,
					isActive = a.IsActive,
					order = a.Order,
					url = a.Url,
					mode = a.Mode
				});
			}catch(Exception ex){
				return BadRequest(ex.Message);
			}
			return Ok(result);
        }	

        [HttpGet("url/{url}")]		
        public IActionResult GetByURL(string url)
        {
			dynamic result;
			try{
				result = context.Menu.Where(a=>a.Url==url).Select(a=>new {
					id = a.Id,
					parentId = a.ParentId,
					title = a.Title,
					isActive = a.IsActive,
					order = a.Order,
					url = a.Url,
					mode = a.Mode
				}).FirstOrDefault();
			}catch(Exception ex){
				return BadRequest(ex.Message);
			}
			return Ok(result);
        }		
		
		// GET: api/Menu
		[HttpGet]
		[Authorize]
        public IActionResult GetMenu()
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var result = context.Menu.Select(a=>new {
					id = a.Id,
					parentId = a.ParentId,
					title = a.Title,
					isActive = a.IsActive,
					order = a.Order,
					url = a.Url,
					mode = a.Mode
				});
				return Ok(result);
			}
			return Unauthorized();
        }
		
		// PUT: api/Menu/5
		[HttpPut("{id}")]
		[Authorize]
        public async Task<IActionResult> PutMenu([FromRoute] int id, [FromBody] Menu menu)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (id != menu.Id)
				{
					return BadRequest();
				}

				menu.CreatedBy = Convert.ToInt32(idUser);
				menu.CreatedDate = DateTime.Now;
				context.Menu.Update(menu);

				try
				{
					await context.SaveChangesAsync();
				}
				catch (Exception)
				{
					return BadRequest();
				}

				return Ok(menu);
			}
			return Unauthorized();
        }
		
        // POST: api/Menu
		[HttpPost]
		[Authorize]
        public async Task<IActionResult> PostMenu([FromBody] Menu menu)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				menu.CreatedBy = Convert.ToInt32(idUser);
				menu.CreatedDate = DateTime.Now;
				context.Menu.Add(menu);
				await context.SaveChangesAsync();

				return Ok(menu);
			}
			return Unauthorized();
        }

        // DELETE: api/Menu/5
		[HttpDelete("{id}")]
		[Authorize]
        public async Task<IActionResult> DeleteMenu([FromRoute] int id)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var menu = context.Menu.Where(a=>a.Id==id);
				if (menu == null)
				{
					return NotFound();
				}
				try
				{
					foreach (var mn in menu)
					{
						context.Menu.Remove(mn);
					}
					await context.SaveChangesAsync();
				}
				catch (Exception ex) {
					Console.WriteLine(ex.Message);
					return BadRequest(ex.Message);
				}

				return Ok(menu);
			}
			return Unauthorized();
        }
    }
}
