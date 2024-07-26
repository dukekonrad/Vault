using Microsoft.AspNetCore.Mvc;
using VaultBusinessLogic.BusinessLogic;
using VaultContracts.BindingModels;
using VaultContracts.BusinessLogicContracts;
using VaultContracts.SearchModels;

namespace VaultClientApp.Controllers
{
    [Route("transactions")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionLogic _transactionLogic;

        public TransactionController(ILogger<TransactionController> logger, ITransactionLogic transactionLogic)
        {
            _logger = logger;
            _transactionLogic = transactionLogic;
        }

        public IActionResult Index(int? account)
        {
            return View(_transactionLogic.ReadList(new TransactionSearchModel { AccountId = account }));
        }

        #region Создание
        [HttpGet("create")]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(TransactionBindingModel model)
        {
            _transactionLogic.Create(model);
            return Redirect("/transactions");
        }
        #endregion

        #region Изменение
        [HttpGet("update")]
        public IActionResult UpdateAccount(int id)
        {
            return View(_transactionLogic.ReadElement(new TransactionSearchModel { Id = id }));
        }

        [HttpPost]
        public IActionResult UpdateBrand(TransactionBindingModel model)
        {
            _transactionLogic.Update(model);
            return Redirect("/transactions");
        }
        #endregion

        [HttpDelete]
        public IActionResult DeleteAccount(int id)
        {
            _transactionLogic.Delete(new TransactionBindingModel { Id = id });
            return Json(new { success = true });
        }
    }
}
