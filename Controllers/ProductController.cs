using Dapper;
using Microsoft.AspNetCore.Mvc;
using ProductsCRUDUsingDapper.Models;

namespace ProductsCRUDUsingDapper.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {

            return View(DapperORM.ReturnList<ProductsModel>("ProductsVieAll", null));

        }

        [HttpPost]
        public ActionResult AddOrEdit(ProductsModel prd)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ProductID", prd.ProductID);
            param.Add("@ProductName", prd.ProductName);
            param.Add("@Price", prd.Price);
            param.Add("@Quantity", prd.Qauntity);
            DapperORM.ExecuteWithoutReturn("AddOrEditProduct", param);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            if(id == 0)
            {
                return View();

            }
            else
            {
                DynamicParameters param = new DynamicParameters();  
                param.Add("@ProductID",id);
                return View(DapperORM.ReturnList<ProductsModel>("ProductsViewByID", param).FirstOrDefault<ProductsModel>());
            }
        }

        public ActionResult Delete (int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ProductID", id);
            DapperORM.ExecuteWithoutReturn("ProductDeletByID",param); 
            return RedirectToAction("Index");
        }

       
    }
}
