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
				string myJsonString = "[{'id':1,'parentId':null,'title':'Beranda','isActive':true,'order':0,'url':'home','mode':'feature'},{'id':2,'parentId':null,'title':'Tentang KSSK','isActive':true,'order':1,'url':'mandat','mode':'single'},{'id':3,'parentId':2,'title':'Mandat','isActive':true,'order':3,'url':'mandat','mode':'single'},{'id':4,'parentId':2,'title':'Anggota','isActive':true,'order':4,'url':'anggota','mode':'single'},{'id':5,'parentId':2,'title':'Sekretariat','isActive':false,'order':5,'url':'sekretariat','mode':'single'},{'id':6,'parentId':2,'title':'Struktur Organisasi','isActive':true,'order':6,'url':'struktur','mode':'single'},{'id':7,'parentId':null,'title':'Kegiatan','isActive':true,'order':7,'url':'kegiatan','mode':'single'},{'id':8,'parentId':7,'title':'Rapat','isActive':true,'order':8,'url':'rapat','mode':'multiplefoto'},{'id':9,'parentId':7,'title':'Gallery Foto/Video','isActive':true,'order':9,'url':'gallery','mode':'multiplefoto'},{'id':10,'parentId':null,'title':'Publikasi','isActive':true,'order':10,'url':'publikasi','mode':'multiplefile'},{'id':11,'parentId':10,'title':'Siaran Pers','isActive':true,'order':11,'url':'pr','mode':'multiplefile'},{'id':12,'parentId':10,'title':'Q&A','isActive':true,'order':13,'url':'qa','mode':'multipletext'},{'id':13,'parentId':null,'title':'Peraturan','isActive':true,'order':14,'url':'peraturan','mode':'multiplefilesmall'},{'id':14,'parentId':2,'title':'Sejarah KSSK','isActive':true,'order':2,'url':'sejarahkssk','mode':'single'},{'id':15,'parentId':10,'title':'Sosialisasi','isActive':true,'order':12,'url':'sosialisasi','mode':'single'},{'id':16,'parentId':13,'title':'UU','isActive':true,'order':15,'url':'uu','mode':'multiplefile'},{'id':17,'parentId':13,'title':'Peraturan BI','isActive':true,'order':16,'url':'peraturanbi','mode':'multiplefile'},{'id':18,'parentId':13,'title':'Peraturan OJK','isActive':true,'order':17,'url':'peraturanojk','mode':'multiplefile'},{'id':19,'parentId':13,'title':'Peraturan LPS','isActive':true,'order':18,'url':'peraturanlps','mode':'multiplefile'},{'id':20,'parentId':13,'title':'Permenkeu','isActive':true,'order':19,'url':'permenkeu','mode':'multiplefile'},{'id':21,'parentId':null,'title':'Contact Us','isActive':false,'order':0,'url':'contactus','mode':'single'},{'id':22,'parentId':null,'title':'Site Map','isActive':false,'order':0,'url':'sitemap','mode':'single'}]";
				List<Menu> menu =  JsonConvert.DeserializeObject<List<Menu>>(myJsonString);
				foreach(var mn in menu){
					mn.CreatedBy = 1;
					mn.CreatedDate = DateTime.Now;
					context.Menu.Add(mn);
				}
				await context.SaveChangesAsync();
			}
			var resContent = context.Content.ToList();
			if(resContent.Count==0){
				string myJsonString = "[{'id':1,'menuId':1,'url':'home','title':'Home','content':'<section class=\"banner-area relative\" id=\"home\"><div class=\"overlay overlay-bg\"></div><div class=\"container\"><div class=\"row fullscreen d-flex align-items-center justify-content-between\" style=\"height: 699px\"><div class=\"banner-content col-lg-9 col-md-12\"><h1 class=\"text-uppercase\">mendukung dan menjaga stabilitas sektor dan institusi keuangan</h1><p class=\"pt-10 pb-10\" style=\"color:white\">Sekretariat KSSK memiliki fungsi dukungan dan fasilitasi Komite Stabilitas Sistem Keuangan dalam bentuk kajian dan assesment; koordinasi penyusunan kebijakan dan regulatory setting serta kajian manajemen risiko dan hukum..</p><a href=\"/mandat\" class=\"primary-btn text-uppercase\">Lebih lanjut</a></div></div></div></section>\r\n<section class=\"feature-area\"><div class=\"container\"><div class=\"row\">\r\n<div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>Siaran Pers</h4></div><div class=\"desc-wrap\"><p>Siaran Pers KSSK 30 April 2018</p><a href=\"/pr\">Selanjutnya</a></div></div></div>\r\n<div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>Peraturan</h4></div><div class=\"desc-wrap\"><p>KEP KSSK 01 2017 Protokol Manajemen Krisis KSSK</p><a href=\"/peraturan\">Selanjutnya</a></div></div></div>\r\n<div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>Rapat</h4></div><div class=\"desc-wrap\"><p>Peresmian Sekretariat KSSK</p><a href=\"/rapat\">Selanjutnya</a></div></div></div>\r\n<div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>News Feed</h4></div><div class=\"desc-wrap\"><p>Awas Modus Penipuan Catut Nama Bank Indonesia<a href=\"https://www.liputan6.com/bisnis/read/3673430/awas-modus-penipuan-catut-nama-bank-indonesia\"> Selanjutnya</a></p><p>Tarik Investasi, Sri Mulyani Siap Beri Insentif Tambahan<a href=\"https://www.liputan6.com/bisnis/read/3670700/tarik-investasi-sri-mulyani-siap-beri-insentif-tambahan\"> Selanjutnya</a></p><p>Saham ke Publik<a href=\"https://www.liputan6.com/bisnis/read/3672350/ojk-bakal-atur-pelaku-ukm-yang-lepas-saham-ke-publik\"> Selanjutnya</a></p></div></div></div></div></div></section>','isActive':true},{'id':2,'menuId':3,'url':'mandat','title':'Mandat','content':'<section class=\"popular-courses-area section-gap courses-page\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-6 no-padding info-area-left\"><img class=\"img-fluid\" src=\"assets/img/about-img.jpg\" alt=\"\"></div><div class=\"col-lg-6 info-area-right\"><h1>Mandat Komite Stabilitas Sistem Keuangan&nbsp;</h1><p><br></p><p><strong>Komite Kebijakan Sistem Keuangan</strong> atau disingkat <em>KSSK</em> yang keanggotaannya terdiri dari <a href=\"https://id.wikipedia.org/wiki/Menteri_Keuangan\" title=\"Menteri Keuangan\">Menteri Keuangan</a> sebagai ketua merangkap anggota dan <a href=\"https://id.wikipedia.org/wiki/Gubernur\" title=\"Gubernur\">Gubernur</a> <a href=\"https://id.wikipedia.org/wiki/Bank_Indonesia\" title=\"Bank Indonesia\">Bank Indonesia</a> sebagai anggota bertujuan untuk menciptakan dan memelihara stabilitas sistem keuangan melalui pencegahan dan penanganan krisis&nbsp;</p><ol><li>Tugas KSSK :</li><li>1. Mengevaluasi skala dan dimensi permasalahan likuiditas dan/atau solvabilitas bank/lembaga keuangan bukan bank (LKBB) yang ditengarai Berdampak Sistemik</li><li>2. Menetapkan permasalahan likuiditas dan/atau masalah solvabilitas bank/lembaga keuangan bukan bank (LKBB) Berdampak Sistemik atau tidak Berdampak Sistemik</li><li>3. Menetapkan langkah-langkah penanganan masalah bank/lembaga keuangan bukan bank (LKBB) yang dipandang perlu dalam rangkapencegahan dan penanganan Krisis</li></ol></div></div></div></section>','isActive':true},{'id':3,'menuId':4,'url':'anggota','title':'Anggota','content':'<section class=\"feature-area\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4> Menteri Keuangan</h4></div><div class=\"desc-wrap\"><p>Mengevaluasi skala dan dimensi permasalahan likuiditas dan/atau solvabilitas bank/lembaga keuangan bukan bank (LKBB) yang ditengarai Berdampak Sistemik</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>Gubernur Bank Indonesia</h4></div><div class=\"desc-wrap\"><p>Menetapkan permasalahan likuiditas dan/atau masalah solvabilitas bank/lembaga keuangan bukan bank (LKBB) Berdampak Sistemik atau tidak Berdampak Sistemik</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>Ketua Dewan Komisioner OJK</h4></div><div class=\"desc-wrap\"><p>Menetapkan langkah-langkah penanganan masalah bank/lembaga keuangan bukan bank (LKBB) yang dipandang perlu dalam rangkapencegahan dan penanganan Krisis</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>Ketua Dewan Komisioner LPS</h4></div><div class=\"desc-wrap\"><p>Menetapkan langkah-langkah penanganan masalah bank/lembaga keuangan bukan bank (LKBB) yang dipandang perlu dalam rangkapencegahan dan penanganan Krisis</p><a href=\"#\">Selanjutnya</a></div></div></div></div></div></section>   ','isActive':true},{'id':4,'menuId':5,'url':'sekretariat','title':'Sekretariat','content':'<section class=\"event-details-area section-gap\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-12 event-details-left\"><div class=\"main-img\"><img class=\"img-fluid\" src=\"assets/img/sekretariat.jpg\" alt=\"\"></div><div class=\"details-content\"><a href=\"#\"><h4>Peresmian Kantor Sekretariat Komite Stabilitas Sistem Keuangan Kementerian Keuangan</h4></a><p>Jakarta, 22 Mei 2018 – Menteri Keuangan (Menkeu) Sri Mulyani Indrawati bersama Gubernur Bank Indonesia (BI) Agus Martowardojo, Ketua Dewan Komisioner Otoritas Jasa Keuangan (OJK) Wimboh Santoso serta Ketua Dewan Komisioner Lembaga Penjamin Simpanan (LPS) Halim Alamsyah meresmikan Kantor Sekretariat Komite Stabilitas Sistem Keuangan (KSSK) di Gedung Djuanda II Lantai 18, Kementerian Keuangan pada Senin, (21/05).</p><p>Dalam acara peresmian tersebut, turut hadir Sekretaris Jenderal (Sesjen) Hadiyanto, Direktur Jenderal Kekayaan Negara Isa Rachmatarwata, Inspektur Jenderal Sumiyati dan jajaran pejabat eselon I di Kementerian Keuangan lainnya serta perwakilan BI, OJK dan LPS. </p><p>Dalam sambutannya, Menkeu menyampaikan bahwa Sekretariat KSSK memiliki tugas untuk mendukung stabilitas sistem keuangan serta menjaga stabilitas sektor dan institusi keuangan. Menkeu juga mengucapkan terima kasih kepada seluruh pihak yang telah memberikan dukungan atas terbangunnya kantor Sekretariat KSSK, terutama Sekretariat Jenderal. </p><p>Melalui seremonial pemotongan pita dan pembukaan tirai oleh Menkeu Sri Mulyani Indrawati bersama Gubernur BI Agus Martowardojo, Ketua Dewan Komisioner OJK Wimboh Santoso dan Ketua Dewan Komisioner LPS Halim Alamsyah, kantor Sekretariat KSSK pun diresmikan. </p></div></div></div></div></section>','isActive':true},{'id':5,'menuId':6,'url':'struktur','title':'Struktur Organisasi','content':'<section class=\"popular-courses-area courses-page\"><div class=\"container\"><div class=\"row justify-content-center\"><div class=\"single-popular-carusel col-lg-8 col-md-8\"><div class=\"thumb-wrap relative\"><img class=\"img-fluid fr-fic fr-dii\" src=\"assets/img/struktur-organisasi.jpg\" alt=\"\"></div><div class=\"details\"><a href=\"course-details.html\">&nbsp;<h4>Struktur Organisasi</h4>&nbsp;</a><p>When television was young, there was a hugely popular show based on the still popular fictional characte</p></div></div></div></div></section>','isActive':true},{'id':6,'menuId':7,'url':'kegiatan','title':'Rapat mengenai ketahanan ekonomi','content':'Rapat mengenai ketahanan ekonomi','isActive':true},{'id':7,'menuId':8,'url':'rapat','title':'Rapat Berkala Komite Stabilitas Sistem Keuangan Triwulan II Tahun 2018','content':'<p>Pada hari Kamis, tanggal 26 Juli 2018 bertempat di Lembaga Penjamin Simpanan, Komite Stabilitas Sistem Keuangan (KSSK) telah menyelenggarakan rapat berkala dalam rangka koordinasi pemantauan dan pemeliharaan Stabilitas Sistem Keuangan. Berdasarkan hasil pemantauan lembaga anggota KSSK terhadap perkembangan perekonomian, moneter, fiskal, pasar keuangan, lembaga jasa keuangan, dan penjaminan simpanan selama Triwulan II tahun 2018 serta mempertimbangkan perkembangan hingga 20 Juli 2018, KSSK menyimpulkan stabilitas sistem keuangan Triwulan II 2018 tetap terjaga di tengah meningkatnya tekanan global.</p>','isActive':true},{'id':8,'menuId':13,'url':'peraturan','title':'Undang-Undang','content':'Undang-Undang','isActive':true},{'id':9,'menuId':13,'url':'peraturan','title':'Keputusan KSSK','content':'Keputusan KSSK','isActive':true},{'id':10,'menuId':11,'url':'pr','title':'Siaran Pers','content':'Siaran Pers','isActive':true},{'id':11,'menuId':9,'url':'gallery','title':'Peresmian Kantor Sekretariat Komite Stabilitas Sistem Keuangan Kementerian Keuangan','content':'<p>Jakarta, 22 Mei 2018 Menteri Keuangan Sri Mulyani Indrawati bersama Gubernur Bank Indonesia (BI) Agus Martowardojo, Ketua Dewan Komisioner Otoritas Jasa Keuangan (OJK) Wimboh Santoso serta Ketua Dewan Komisioner Lembaga Penjamin Simpanan (LPS) Halim Alamsyah meresmikan Kantor Sekretariat Komite Stabilitas Sistem Keuangan (KSSK) di Gedung Djuanda II Lantai 18, Kementerian Keuangan pada Senin, (21/05).\r\nDalam acara peresmian tersebut, turut hadir para pimpinan di lingkungan Kementerian Keuangan, Bank Indonesia, Otoritas Jasa Keuangan dan Lembaga Penjamin Simpanan.</p><p>\r\nDalam sambutannya, Menteri Keuangan menyampaikan bahwa Sekretariat KSSK memiliki tugas untuk mendukung stabilitas sistem keuangan serta menjaga stabilitas sektor dan institusi keuangan. Oleh karena itu penting agar Sekretariat KSSK dapat melengkapi prosedur operasi standar, memperbaiki protokol manajemen dan membangun kerjasama yang erat dari keempat lembaga KSSK. Menteri Keuangan meminta dukungan dari pada pimpinan BI, OJK dan LPS atas kepada Sekretariat KSSK agar dapat bekerja dengan efektif.</p><p>\r\nMenteri Keuangan juga mengucapkan terima kasih kepada seluruh pihak yang telah memberikan dukungan atas terbangunnya kantor Sekretariat KSSK, terutama Sekretariat Jenderal. Melalui seremonial pemotongan pita dan pembukaan tirai oleh Menteri Keuangan Sri Mulyani Indrawati bersama Gubernur BI Agus Martowardojo, Ketua Dewan Komisioner OJK Wimboh Santoso dan Ketua Dewan Komisioner LPS Halim Alamsyah, kantor Sekretariat KSSK pun diresmikan.\r\n</p>','isActive':true},{'id':12,'menuId':9,'url':'gallery','title':'Malam Apresiasi Bapak Agus Martowardojo','content':'<p>Tanggal 21 Mei 2018 bersamaan dengan peresmian kantor Sekretariat KSSK, dilaksanakan juga malam apresiasi kepada Bapak Agus Martowardojo yang dalam waktu dekat akan mengakhiri masa tugasnya sebagai Gubernur Bank Indonesia. Pada kesempatan tersebut Menteri Keuangan, Ketua Dewan Komisioner Otoritas Jasa Keuangan, Ketua Dewan Komisioner Lembaga Penjamin Simpanan menyampaikan apresiasi serta kesan dan pesan atas kontribusi Bapak Agus Martowardojo dalam meletakkan pondasi yang kuat untuk menjaga stabilitas sistem keuangan melalui dukungannya dalam penyusunan UU No. 16 tahun 2016 tentang Pencegahan dan Penanganan Krisis Sistem Keuangan (UU PPKSK).\r\n</p>','isActive':true},{'id':13,'menuId':21,'url':'contactus','title':'Contact Us','content':'<h4>Kontak Kami</h4><ul><li>Sekretariat: Gedung Djuanda II Lantai 18&nbsp;<br>Jalan Dr. Wahidin Nomor 1, Jakarta 10710&nbsp;<br>Telepon: (021) 350 3504 | Faksimili: (021) 350 3506 | Email: sekretariatkssk@kssk.go.id</li></ul>','isActive':true},{'id':14,'menuId':22,'url':'sitemap','title':'Site Map','content':'<div class=\"event-details-area section-gap\">\r\n<div class=\"container\">\r\n<table class=\"table table-hover table-sm\">\r\n\t<thead class=\"thead-light\">\r\n\t<tr>\r\n\t\t<th>No</th>\r\n\t\t<th>Menu</th>\r\n\t\t<th>Sub Menu</th>\r\n\t</tr>\r\n\t</thead>\r\n\t<tbody>\r\n\t<tr>\r\n\t\t<td>1</td>\r\n\t\t<td><a href=\"/home\">Beranda</a></td>\r\n\t\t<td></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td>2</td>\r\n\t\t<td>Tentang KSSK</td>\r\n\t\t<td><a href=\"/sejarahkssk\">Sejarah KSSK</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/mandat\">Mandat</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/anggota\">Anggota</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/struktur\">Struktur KSSK</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td>3</td>\r\n\t\t<td>Kegiatan</td>\r\n\t\t<td><a href=\"/rapat\">Rapat</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/gallery\">Gallery Foto / Video</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td>4</td>\r\n\t\t<td>Publikasi</td>\r\n\t\t<td><a href=\"/pr\">Siaran Pers</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/sosialisasi\">Sosialisasi</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/qa\">Q & A</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td>5</td>\r\n\t\t<td>Peraturan</td>\r\n\t\t<td><a href=\"/uu\">UU</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/peraturanbi\">Peraturan BI</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/peraturanojk\">Peraturan OJK</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/peraturanlps\">Peraturan LPS</a></td>\r\n\t</tr>\r\n\t<tr>\r\n\t\t<td></td>\r\n\t\t<td></td>\r\n\t\t<td><a href=\"/permenkeu\">Permenkeu</a></td>\r\n\t</tr>\r\n\t</tbody>\r\n</table>\r\n</div>\r\n</div>','isActive':true}]";
				List<Content> content =  JsonConvert.DeserializeObject<List<Content>>(myJsonString);
				foreach(var con in content){
					con.CreatedBy = 1;
					con.CreatedDate = DateTime.Now;
					context.Content.Add(con);
				}
				await context.SaveChangesAsync();
			}
			var resFile = context.Files.ToList();
			if(resFile.Count==0){
				string myJsonString = "[{'id':1,'contentId':11,'filename':'DSC_1535.jpg','description':null,'order':0},{'id':2,'contentId':11,'filename':'DSC_1549.jpg','description':null,'order':1},{'id':3,'contentId':11,'filename':'DSC_1579.jpg','description':null,'order':2},{'id':4,'contentId':11,'filename':'DSC_1705.jpg','description':null,'order':3},{'id':5,'contentId':11,'filename':'DSC_1706.jpg','description':null,'order':4},{'id':6,'contentId':11,'filename':'DSC_1707.jpg','description':null,'order':5},{'id':7,'contentId':8,'filename':'UU Nomor 9 Tahun 2016.pdf','description':'<p>15 April 2016</p>\r\n         <h4>UU Nomor 9 Tahun 2016</h4>\r\n         <p>\r\n          UNDANG UNDANG TENTANG PENCEGAHAN DAN PENGAMANAN KRISIS SISTEM KEUANGAN.\r\n         </p>','order':0},{'id':8,'contentId':8,'filename':'UU Nomor 21 Tahun 2011.pdf','description':'\r\n\t\t\t\t\t\t\t\t\t<p>22 November 2011</p>\r\n\t\t\t\t\t\t\t\t\t<h4>UU Nomor 21 Tahun 2011</h4>\r\n\t\t\t\t\t\t\t\t\t<p>\r\n\t\t\t\t\t\t\t\t\t\tUNDANG UNDANG TENTANG OTORITAS JASA KEUANGAN.\r\n\t\t\t\t\t\t\t\t\t</p>','order':1},{'id':9,'contentId':8,'filename':'UU Nomor 23 Tahun 1999.pdf','description':'\r\n\t\t\t\t\t\t\t\t\t<p>17 Mei 1999</p>\r\n\t\t\t\t\t\t\t\t\t<h4>UU Nomor 23 Tahun 1999</h4>\r\n\t\t\t\t\t\t\t\t\t<p>\r\n\t\t\t\t\t\t\t\t\t\tUNDANG UNDANG TENTANG BANK INDONESIA.\r\n\t\t\t\t\t\t\t\t\t</p>','order':2},{'id':10,'contentId':9,'filename':'KEP KSSK 01 2017 Protokol Manajemen Krisis  KSSK.pdf','description':'<p>27 Juli 2017</p>\r\n         <h4>KEP KSSK 01 2017 Protokol Manajemen Krisis  KSSK</h4>\r\n         <p>\r\n          PROTOKOL MANAJEMEN KRISIS KOMITE STABILITAS SISTEM KEUANGAN.\r\n         </p>','order':3},{'id':11,'contentId':9,'filename':'KEP KSSK 04 2016 Kode Etik KSSK.pdf','description':'<p>30 November 2016</p>\r\n         <h4>KEP KSSK 04 2016 Kode Etik KSSKi</h4>\r\n         <p>\r\n          KODE ETIK KOMITE STABILITAS SISTEM KEUANGAN\r\n.\r\n         </p>','order':4},{'id':12,'contentId':9,'filename':'KEP KSSK 03 2016 POS Pertukaran Data dan Informasi.pdf','description':'<p>30 November 2016</p>\r\n         <h4>KEP KSSK 03 2016 POS Pertukaran Data dan Informasi</h4>\r\n         <p>\r\n          PROSEDUR OPERASIONAL STANDAR PERTUKARAN DATA DAN INFORMASI KOMITE STABILITAS SISTEM KEUANGAN\r\n.\r\n         </p>','order':5},{'id':13,'contentId':9,'filename':'KEP KSSK 02 2016 POS Rapat Sewaktu-waktu KSSK.pdf','description':'<p>30 November 2016</p>\r\n         <h4>KEP KSSK 02 2016 POS Rapat Sewaktu-waktu KSSK</h4>\r\n         <p>\r\n          PROSEDUR OPERASIONAL STANDAR RAPAT SEWAKTU-WAKTU KOMITE STABILITAS SISTEM KEUANGAN\r\n.\r\n         </p>','order':6},{'id':14,'contentId':9,'filename':'KEP KSSK 01 2016 POS Rapat Berkala KSSK.pdf','description':'<p>30 November 2016</p>\r\n         <h4>KEP KSSK 01 2016 POS Rapat Berkala KSSK</h4>\r\n         <p>\r\n          PROSEDUR OPERASIONAL STANDAR RAPAT BERKALA KOMITE STABILITAS SISTEM KEUANGAN.\r\n         </p>','order':7},{'id':15,'contentId':10,'filename':'Siaran Pers KSSK 30 April 2018.pdf','description':'<p>Siaran Pers KSSK 30 April 2018</p>','order':1},{'id':16,'contentId':10,'filename':'Siaran Pers KSSK 31 Juli 2018.pdf','description':'<p>Siaran Pers KSSK 31 Juli 2018</p>','order':2},{'id':17,'contentId':10,'filename':'Siaran Pers KSSK 31 Okt 2017.pdf','description':'<p>Siaran Pers KSSK 31 Okt 2017</p>','order':0},{'id':18,'contentId':7,'filename':'DSC_9999.jpg','description':null,'order':0},{'id':19,'contentId':12,'filename':'DSC_1782.jpg','description':null,'order':0},{'id':20,'contentId':12,'filename':'DSC_1889.jpg','description':null,'order':1},{'id':21,'contentId':12,'filename':'DSC_1895.jpg','description':null,'order':2},{'id':22,'contentId':12,'filename':'DSC_1925.jpg','description':null,'order':3},{'id':23,'contentId':12,'filename':'DSC_1986.jpg','description':null,'order':4},{'id':24,'contentId':12,'filename':'DSC_2001.jpg','description':null,'order':5},{'id':25,'contentId':12,'filename':'DSC_2003.jpg','description':null,'order':6}]";
				List<Files> files =  JsonConvert.DeserializeObject<List<Files>>(myJsonString);
				foreach(var fl in files){
					fl.CreatedBy = 1;
					fl.CreatedDate = DateTime.Now;
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
