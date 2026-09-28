using Buoi7_Annotation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buoi7_Annotation.Controllers
{
    public class TnkMemberController : Controller
    {
        // GET: TnkMemberController
        public static List<TnkMember> tnkMembers = new List<TnkMember>();
        public ActionResult Index()
        {
            return View(tnkMembers);
        }

        // GET: TnkMemberController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TnkMemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TnkMemberController/Create
        // POST: TnkMemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TnkMember tnkMember)
        {
            try
            {
                if (string.IsNullOrEmpty(tnkMember.TnkId))
                {
                    tnkMember.TnkId = "MB" + (tnkMembers.Count + 1).ToString("D3");

                    ModelState.Remove("TnkId");
                }
                if (!ModelState.IsValid)
                {
                    return View(tnkMember);
                }
                tnkMembers.Add(tnkMember);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tnkMember);
            }
        }

        // GET: TnkMemberController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TnkMemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TnkMemberController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TnkMemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
