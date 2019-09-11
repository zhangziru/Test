using MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using PingFanRen.MongoData;
using PingFanRen.MongoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_MONGO.Controllers
{
    public class CsPageController : Controller
    {
        // GET: CsPage
        public ActionResult Index()
        {
            SmlTest entity = new SmlTest();
            entity.name = "name111";
            entity.sex = "sex111";
            entity.age = "age111";
            entity.like = "like111";

            var B = MdData.Collection<SmlTest>();
            B.Insert(entity);

            var list = B.FindAll().ToList();

            //var list = MdData.SmlTest.FindAll().ToList();


            //var doc = new Document();
            //doc.Add("age", "13");
            //var query = (IMongoQuery)doc.ToQuery();

            //IMongoQuery query = Query.EQ("age", "13");

            //MdData.SmlTest.Remove(query);

            return View();
        }

    }
}