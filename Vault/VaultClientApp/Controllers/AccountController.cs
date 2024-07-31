using Microsoft.AspNetCore.Mvc;
using VaultContracts.BusinessLogicContracts;
using VaultContracts.BindingModels;
using VaultContracts.SearchModels;

namespace VaultClientApp.Controllers
{
	[Route("")]
	[Route("accounts")]
    public class AccountController : Controller
    {
        private readonly IAccountLogic _accountLogic;

        public AccountController(IAccountLogic accountLogic)
        {
            _accountLogic = accountLogic;
        }

		public async Task<IActionResult> Index(int? number)
        {
		    return View(await _accountLogic.ReadList(!number.HasValue ? null : new AccountSearchModel { Id = number }));
		}

        [HttpGet ("cr8")]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost("cr8")]
        public async Task<IActionResult> CreateAccount(AccountBindingModel model)
        {
            await _accountLogic.Create(model);
            return Redirect("/accounts");
        }

        [HttpGet ("upd")]
        public async Task<IActionResult> UpdateAccount(int id)
        {
            return View(await _accountLogic.ReadElement(new AccountSearchModel { Id = id }));
        }

        [HttpPost ("upd")]
        public async Task<IActionResult> UpdateBrand(AccountBindingModel model)
        {
            await _accountLogic.Update(model);
            return Redirect("/accounts");
        }

        [HttpPost ("del")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountLogic.Delete(new AccountBindingModel { Id = id });
            return Json(new { success = true });
        }
    }
}
