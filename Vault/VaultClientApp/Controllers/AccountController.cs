using Microsoft.AspNetCore.Mvc;
using VaultContracts.BusinessLogicContracts;
using VaultContracts.BindingModels;
using VaultContracts.SearchModels;
using VaultDatabase.Models;

namespace VaultClientApp.Controllers
{
    [Route("accounts")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountLogic _accountLogic;

        public AccountController(ILogger<AccountController> logger, IAccountLogic accountLogic)
        {
            _logger = logger;
            _accountLogic = accountLogic;
        }

        public IActionResult Index(int? account)
        {
            return View(_accountLogic.ReadList(new AccountSearchModel { Id = account }));
        }

        #region Создание
        [HttpGet ("create")]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(AccountBindingModel model)
        {
            _accountLogic.Create(model);
            return Redirect("/accounts");
        }
        #endregion

        #region Изменение
        [HttpGet ("update")]
        public IActionResult UpdateAccount(int id)
        {
            return View(_accountLogic.ReadElement(new AccountSearchModel { Id = id }));
        }

        [HttpPost]
        public IActionResult UpdateBrand(AccountBindingModel model)
        {
            _accountLogic.Update(model);
            return Redirect("/accounts");
        }
        #endregion

        [HttpDelete]
        public IActionResult DeleteAccount(int id)
        {
            _accountLogic.Delete(new AccountBindingModel { Id = id });
            return Json(new { success = true });
        }
    }
}
