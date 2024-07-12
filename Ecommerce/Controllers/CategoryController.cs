using Ecommerce.Models;
using Ecommerce.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class CategoryController : Controller
    {
        #region Category List
        public ActionResult CategoryList(string search)
        {
            CategoriesService categoriesService = new CategoriesService();
            ModelState.Clear();

            IEnumerable<Categories> categories;

            if (!string.IsNullOrEmpty(search))
            {
                categories = categoriesService.GetAllCategories().Where(c => c.Name.Contains(search));
            }
            else
            {
                categories = categoriesService.GetAllCategories();
            }

            ViewBag.SlNo = 1;

            return View(categories);
        }

        #endregion

        #region Create Category
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Categories category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CategoriesService categoriesService = new CategoriesService();

                    if (categoriesService.AddCategory(category))
                    {
                        ViewBag.Message = "Category added successfully";
                        //return RedirectToAction("CategoryList", "Category");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Update Category
        public ActionResult UpdateCategory(int id)
        {
            CategoriesService categoriesService = new CategoriesService();

            return View(categoriesService.GetAllCategories().Find(cat => cat.CategoryID == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCategory(int? id, Categories obj)
        {
            try
            {
                //obj.CategoryID = id;

                CategoriesService categoriesService = new CategoriesService();

                categoriesService.UpdateCategory(obj);
                return RedirectToAction("CategoryList");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Delete Category
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                CategoriesService categoriesService = new CategoriesService();

                if (categoriesService.DeleteCategory(id))
                {
                    ViewBag.AlertMsg = "Categories deleted successfully";
                }

                return RedirectToAction("CategoryList");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}