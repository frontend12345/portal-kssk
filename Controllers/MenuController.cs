using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
				string myJsonString = "[{'id':1,'title':'Beranda','description':null,'parentId':null,'isActive':true,'order':0,'url':'home','mode':'full','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':2,'title':'Tentang KSSK','description':null,'parentId':null,'isActive':true,'order':1,'url':'mandat','mode':'single','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':3,'title':'Mandat','description':null,'parentId':2,'isActive':true,'order':2,'url':'mandat','mode':'single','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':4,'title':'Anggota','description':null,'parentId':2,'isActive':true,'order':3,'url':'anggota','mode':'single','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':5,'title':'Sekretariat','description':null,'parentId':2,'isActive':true,'order':4,'url':'sekretariat','mode':'single','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':6,'title':'Struktur Organisasi','description':null,'parentId':2,'isActive':true,'order':5,'url':'struktur','mode':'single','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':7,'title':'Kegiatan','description':null,'parentId':null,'isActive':true,'order':6,'url':'kegiatan','mode':'single','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':8,'title':'Rapat','description':null,'parentId':7,'isActive':true,'order':7,'url':'rapat','mode':'multiplefoto','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':9,'title':'Gallery Foto/Video','description':null,'parentId':7,'isActive':true,'order':8,'url':'gallery','mode':'multiplefoto','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':10,'title':'Publikasi','description':null,'parentId':null,'isActive':true,'order':9,'url':'publikasi','mode':'multiplefile','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':11,'title':'Press Release','description':null,'parentId':10,'isActive':true,'order':10,'url':'pr','mode':'multipletext','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':12,'title':'Q&A','description':null,'parentId':10,'isActive':true,'order':11,'url':'qa','mode':'multipletext','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]},{'id':13,'title':'Peraturan','description':null,'parentId':null,'isActive':true,'order':12,'url':'peraturan','mode':'multiplefile','createdDate':'2018-10-05T10:30:16','createdBy':1,'content':[]}]";
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
				string myJsonString = "[{'id':1,'menuId':1,'url':'home','content1':'<section class=\"banner-area relative\" id=\"home\"><div class=\"overlay overlay-bg\"></div><div class=\"container\"><div class=\"row fullscreen d-flex align-items-center justify-content-between\" style=\"height: 699px\"><div class=\"banner-content col-lg-9 col-md-12\"><h1 class=\"text-uppercase\">mendukung dan menjaga stabilitas sektor dan institusi keuangan</h1><p class=\"pt-10 pb-10\">Sekretariat KSSK memiliki fungsi dukungan dan fasilitasi Komite Stabilitas Sistem Keuangan dalam bentuk kajian dan assesment; koordinasi penyusunan kebijakan dan regulatory setting serta kajian manajemen risiko dan hukum..</p><a href=\"#\" class=\"primary-btn text-uppercase\">Lebih lanjut</a></div></div></div></section><section class=\"feature-area\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-4\"><div class=\"single-feature\"><div class=\"title\"><h4>Evaluasi</h4></div><div class=\"desc-wrap\"><p>Mengevaluasi skala dan dimensi permasalahan likuiditas dan/atau solvabilitas bank/lembaga keuangan bukan bank (LKBB) yang ditengarai Berdampak Sistemik</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-4\"><div class=\"single-feature\"><div class=\"title\"><h4>Penetapan</h4></div><div class=\"desc-wrap\"><p>Menetapkan permasalahan likuiditas dan/atau masalah solvabilitas bank/lembaga keuangan bukan bank (LKBB) Berdampak Sistemik atau tidak Berdampak Sistemik</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-4\"><div class=\"single-feature\"><div class=\"title\"><h4>Penanganan</h4></div><div class=\"desc-wrap\"><p>Menetapkan langkah-langkah penanganan masalah bank/lembaga keuangan bukan bank (LKBB) yang dipandang perlu dalam rangkapencegahan dan penanganan Krisis</p><a href=\"#\">Selanjutnya</a></div></div></div></div></div></section>','isActive':true,'createdDate':'2018-10-05T22:43:05','createdBy':1,'menu':null,'files':[]},{'id':2,'menuId':3,'url':'mandat','content1':'<section class=\"popular-courses-area section-gap courses-page\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-6 no-padding info-area-left\"><img class=\"img-fluid\" src=\"assets/img/about-img.jpg\" alt=\"\"></div><div class=\"col-lg-6 info-area-right\"><h1>Siap menyelenggarakan pencegahan dan penanganan krisis sistem keuangan</h1><p>inappropriate behavior is often laughed off as “boys will be boys,” women face higher conduct standards especially in the workplace. That’s why it’s crucial that, as women, our behavior on the job is beyond reproach.</p><br><p>inappropriate behavior is often laughed off as “boys will be boys,” women face higher conduct standards especially in the workplace. That’s why it’s crucial that, as women, our behavior on the job is beyond reproach. inappropriate behavior is often laughed off as “boys will be boys,” women face higher conduct standards especially in the workplace. That’s why it’s crucial that, as women, our behavior on the job is beyond reproach.</p></div></div></div></section>','isActive':true,'createdDate':'2018-10-05T22:43:05','createdBy':1,'menu':null,'files':[]},{'id':3,'menuId':4,'url':'anggota','content1':'<section class=\"feature-area\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4> Menteri Keuangan</h4></div><div class=\"desc-wrap\"><p>Mengevaluasi skala dan dimensi permasalahan likuiditas dan/atau solvabilitas bank/lembaga keuangan bukan bank (LKBB) yang ditengarai Berdampak Sistemik</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>Gubernur Bank Indonesia</h4></div><div class=\"desc-wrap\"><p>Menetapkan permasalahan likuiditas dan/atau masalah solvabilitas bank/lembaga keuangan bukan bank (LKBB) Berdampak Sistemik atau tidak Berdampak Sistemik</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>Ketua Dewan Komisioner OJK</h4></div><div class=\"desc-wrap\"><p>Menetapkan langkah-langkah penanganan masalah bank/lembaga keuangan bukan bank (LKBB) yang dipandang perlu dalam rangkapencegahan dan penanganan Krisis</p><a href=\"#\">Selanjutnya</a></div></div></div><div class=\"col-lg-3\"><div class=\"single-feature\"><div class=\"title\"><h4>Ketua Dewan Komisioner LPS</h4></div><div class=\"desc-wrap\"><p>Menetapkan langkah-langkah penanganan masalah bank/lembaga keuangan bukan bank (LKBB) yang dipandang perlu dalam rangkapencegahan dan penanganan Krisis</p><a href=\"#\">Selanjutnya</a></div></div></div></div></div></section>   ','isActive':true,'createdDate':'2018-10-05T22:43:05','createdBy':1,'menu':null,'files':[]},{'id':4,'menuId':5,'url':'sekretariat','content1':'<section class=\"event-details-area section-gap\"><div class=\"container\"><div class=\"row\"><div class=\"col-lg-12 event-details-left\"><div class=\"main-img\"><img class=\"img-fluid\" src=\"assets/img/sekretariat.jpg\" alt=\"\"></div><div class=\"details-content\"><a href=\"#\"><h4>Peresmian Kantor Sekretariat Komite Stabilitas Sistem Keuangan Kementerian Keuangan</h4></a><p>Jakarta, 22 Mei 2018 – Menteri Keuangan (Menkeu) Sri Mulyani Indrawati bersama Gubernur Bank Indonesia (BI) Agus Martowardojo, Ketua Dewan Komisioner Otoritas Jasa Keuangan (OJK) Wimboh Santoso serta Ketua Dewan Komisioner Lembaga Penjamin Simpanan (LPS) Halim Alamsyah meresmikan Kantor Sekretariat Komite Stabilitas Sistem Keuangan (KSSK) di Gedung Djuanda II Lantai 18, Kementerian Keuangan pada Senin, (21/05).</p><p>Dalam acara peresmian tersebut, turut hadir Sekretaris Jenderal (Sesjen) Hadiyanto, Direktur Jenderal Kekayaan Negara Isa Rachmatarwata, Inspektur Jenderal Sumiyati dan jajaran pejabat eselon I di Kementerian Keuangan lainnya serta perwakilan BI, OJK dan LPS. </p><p>Dalam sambutannya, Menkeu menyampaikan bahwa Sekretariat KSSK memiliki tugas untuk mendukung stabilitas sistem keuangan serta menjaga stabilitas sektor dan institusi keuangan. Menkeu juga mengucapkan terima kasih kepada seluruh pihak yang telah memberikan dukungan atas terbangunnya kantor Sekretariat KSSK, terutama Sekretariat Jenderal. </p><p>Melalui seremonial pemotongan pita dan pembukaan tirai oleh Menkeu Sri Mulyani Indrawati bersama Gubernur BI Agus Martowardojo, Ketua Dewan Komisioner OJK Wimboh Santoso dan Ketua Dewan Komisioner LPS Halim Alamsyah, kantor Sekretariat KSSK pun diresmikan. </p></div></div></div></div></section>','isActive':true,'createdDate':'2018-10-05T22:43:05','createdBy':1,'menu':null,'files':[]},{'id':5,'menuId':6,'url':'struktur','content1':' <section class=\"popular-courses-area courses-page\"><div class=\"container\"><div class=\"row justify-content-center\"><div class=\"single-popular-carusel col-lg-8 col-md-8\"><div class=\"thumb-wrap relative\"><div class=\"thumb relative\"><div class=\"overlay overlay-bg\"></div><img class=\"img-fluid\" src=\"assets/img/struktur-organisasi.jpg\" alt=\"\"></div><div class=\"meta d-flex justify-content-between\"><p><span class=\"lnr lnr-users\"></span> 355 <span class=\"lnr lnr-bubble\"></span>35</p><h4>$150</h4></div></div><div class=\"details\"><a href=\"course-details.html\"><h4>Struktur Organisasi</h4></a><p>When television was young, there was a hugely popular show based on the still popular fictional characte</p></div></div></div></div></section>','isActive':true,'createdDate':'2018-10-05T22:43:05','createdBy':1,'menu':null,'files':[]},{'id':6,'menuId':7,'url':'kegiatan','content1':'Rapat mengenai ketahanan ekonomi','isActive':true,'createdDate':'2018-10-05T22:43:05','createdBy':1,'menu':null,'files':[]},{'id':7,'menuId':8,'url':'rapat','content1':'Rapat mengenai ketahanan ekonomi','isActive':true,'createdDate':'2018-10-05T22:43:05','createdBy':1,'menu':null,'files':[]},{'id':8,'menuId':13,'url':'peraturan','content1':'Undang-Undang','isActive':true,'createdDate':'2018-10-05T22:43:05','createdBy':1,'menu':null,'files':[]},{'id':9,'menuId':13,'url':'peraturan','content1':'Keputusan KSSK','isActive':true,'createdDate':'2018-10-05T22:43:05','createdBy':1,'menu':null,'files':[]}]";
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
				string myJsonString = "[{'id':1,'contentId':7,'filename':'DSC_1535.jpg','description':null,'order':0,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':2,'contentId':7,'filename':'DSC_1549.jpg','description':null,'order':1,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':3,'contentId':7,'filename':'DSC_1579.jpg','description':null,'order':2,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':4,'contentId':7,'filename':'DSC_1705.jpg','description':null,'order':3,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':5,'contentId':7,'filename':'DSC_1706.jpg','description':null,'order':4,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':6,'contentId':7,'filename':'DSC_1707.jpg','description':null,'order':5,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':7,'contentId':8,'filename':'UU Nomor 9 Tahun 2016.pdf','description':'<p>15 April 2016</p>\r\n         <h4>UU Nomor 9 Tahun 2016</h4>\r\n         <p>\r\n          UNDANG UNDANG TENTANG PENCEGAHAN DAN PENGAMANAN KRISIS SISTEM KEUANGAN.\r\n         </p>','order':0,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':8,'contentId':8,'filename':'UU Nomor 21 Tahun 2011.pdf','description':'\r\n\t\t\t\t\t\t\t\t\t<p>22 November 2011</p>\r\n\t\t\t\t\t\t\t\t\t<h4>UU Nomor 21 Tahun 2011</h4>\r\n\t\t\t\t\t\t\t\t\t<p>\r\n\t\t\t\t\t\t\t\t\t\tUNDANG UNDANG TENTANG OTORITAS JASA KEUANGAN.\r\n\t\t\t\t\t\t\t\t\t</p>','order':1,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':9,'contentId':8,'filename':'UU Nomor 23 Tahun 1999.pdf','description':'\r\n\t\t\t\t\t\t\t\t\t<p>17 Mei 1999</p>\r\n\t\t\t\t\t\t\t\t\t<h4>UU Nomor 23 Tahun 1999</h4>\r\n\t\t\t\t\t\t\t\t\t<p>\r\n\t\t\t\t\t\t\t\t\t\tUNDANG UNDANG TENTANG BANK INDONESIA.\r\n\t\t\t\t\t\t\t\t\t</p>','order':2,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':10,'contentId':9,'filename':'KEP KSSK 01 2017 Protokol Manajemen Krisis  KSSK.pdf','description':'<p>27 Juli 2017</p>\r\n         <h4>KEP KSSK 01 2017 Protokol Manajemen Krisis  KSSK</h4>\r\n         <p>\r\n          PROTOKOL MANAJEMEN KRISIS KOMITE STABILITAS SISTEM KEUANGAN.\r\n         </p>','order':3,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':11,'contentId':9,'filename':'KEP KSSK 04 2016 Kode Etik KSSK.pdf','description':'<p>30 November 2016</p>\r\n         <h4>KEP KSSK 04 2016 Kode Etik KSSKi</h4>\r\n         <p>\r\n          KODE ETIK KOMITE STABILITAS SISTEM KEUANGAN\r\n.\r\n         </p>','order':4,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':12,'contentId':9,'filename':'KEP KSSK 03 2016 POS Pertukaran Data dan Informasi.pdf','description':'<p>30 November 2016</p>\r\n         <h4>KEP KSSK 03 2016 POS Pertukaran Data dan Informasi</h4>\r\n         <p>\r\n          PROSEDUR OPERASIONAL STANDAR PERTUKARAN DATA DAN INFORMASI KOMITE STABILITAS SISTEM KEUANGAN\r\n.\r\n         </p>','order':5,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':13,'contentId':9,'filename':'KEP KSSK 02 2016 POS Rapat Sewaktu-waktu KSSK.pdf','description':'<p>30 November 2016</p>\r\n         <h4>KEP KSSK 02 2016 POS Rapat Sewaktu-waktu KSSK</h4>\r\n         <p>\r\n          PROSEDUR OPERASIONAL STANDAR RAPAT SEWAKTU-WAKTU KOMITE STABILITAS SISTEM KEUANGAN\r\n.\r\n         </p>','order':6,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null},{'id':14,'contentId':9,'filename':'KEP KSSK 01 2016 POS Rapat Berkala KSSK.pdf','description':'<p>30 November 2016</p>\r\n         <h4>KEP KSSK 01 2016 POS Rapat Berkala KSSK</h4>\r\n         <p>\r\n          PROSEDUR OPERASIONAL STANDAR RAPAT BERKALA KOMITE STABILITAS SISTEM KEUANGAN.\r\n         </p>','order':7,'createdDate':'2018-10-05T22:43:05','createdBy':1,'content':null}]";
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
				string myJsonString = "[{'id':1,'username':'brandon.stark@gmail.com','password':'2EA90BF81C8CF1AFC73DA38E917C728614E90EB6031F3D1D8DB002A91D26EB32','role':'Admin','authenticator':'123'},{'id':2,'username':'1','password':'6B86B273FF34FCE19D6B804EFF5A3F5747ADA4EAA22F1D49C01E52DDB7875B4B','role':'Admin','authenticator':'123'},{'id':3,'username':'kssk@kssk.go.id','password':'5A654027471B387BDC86E7B2902D62FB0389FF8C1C7180486F4C3447F9E8A263','role':'Admin','authenticator':'123'}]";
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
		
		// GET: api/Menu
		[HttpGet]
        public IQueryable<Menu> GetMenu()
        {
            return context.Menu;
        }
		
		// PUT: api/Menu/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutMenu([FromRoute] int id, [FromBody] Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menu.Id)
            {
                return BadRequest();
            }

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
		
        // POST: api/Menu
		[HttpPost]
        public async Task<IActionResult> PostMenu([FromBody] Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            menu.CreatedBy = 1;
            menu.CreatedDate = DateTime.Now;
            context.Menu.Add(menu);
            await context.SaveChangesAsync();

            return Ok(menu);
        }
		
        // POST: api/Menu
		[HttpPost("list")]
        public async Task<IActionResult> PostMenu([FromBody] List<Menu> menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			foreach(var mn in menu){
				mn.CreatedBy = 1;
				mn.CreatedDate = DateTime.Now;
				context.Menu.Add(mn);
			}
            await context.SaveChangesAsync();

            return Ok(menu);
        }

        // DELETE: api/Menu/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu([FromRoute] int id)
        {
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
    }
}
