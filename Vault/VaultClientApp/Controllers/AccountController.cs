using Microsoft.AspNetCore.Mvc;
using VaultContracts.BusinessLogicContracts;
using VaultContracts.BindingModels;
using VaultContracts.SearchModels;

namespace VaultClientApp.Controllers
{
    [Route("accounts")]
    public class AccountController : Controller
    {
        private readonly IAccountLogic _accountLogic;

        public AccountController(IAccountLogic accountLogic)
        {
            _accountLogic = accountLogic;
        }

		public IActionResult Index(int? number)
        {
		    return View(_accountLogic.ReadList(!number.HasValue ? null : new AccountSearchModel { Id = number }));
		}

        [HttpGet ("cr8")]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost("cr8")]
        public IActionResult CreateAccount(AccountBindingModel model)
        {
            _accountLogic.Create(model);
            return Redirect("/accounts");
        }

        [HttpGet ("upd")]
        public IActionResult UpdateAccount(int id)
        {
            return View(_accountLogic.ReadElement(new AccountSearchModel { Id = id }));
        }

        [HttpPost ("upd")]
        public IActionResult UpdateBrand(AccountBindingModel model)
        {
            _accountLogic.Update(model);
            return Redirect("/accounts");
        }

        [HttpPost ("del")]
        public IActionResult DeleteAccount(int id)
        {
            _accountLogic.Delete(new AccountBindingModel { Id = id });
            return Json(new { success = true });
        }
    }
}
