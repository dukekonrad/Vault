using Microsoft.AspNetCore.Mvc;
using VaultContracts.BusinessLogicContracts;
using VaultContracts.BindingModels;
using VaultContracts.SearchModels;

namespace VaultClientApp.Controllers
{
    [Route("transactions")]
    public class TransactionController : Controller
    {
        private readonly ITransactionLogic _transactionLogic;
		private readonly IAccountLogic _accountLogic;

		public TransactionController(ITransactionLogic transactionLogic, IAccountLogic accountLogic)
		{
			_transactionLogic = transactionLogic;
			_accountLogic = accountLogic;
		}
		public IActionResult Index(int? account)
		{
			return View(_transactionLogic.ReadList(!account.HasValue ? null : new TransactionSearchModel { AccountId = account }));
		}

        [HttpGet("cr8")]
        public IActionResult CreateTransaction()
        {
			ViewBag.Accounts = _accountLogic.ReadList(null);
			return View();
        }

		[HttpPost("cr8")]
		public IActionResult CreateTransaction(TransactionBindingModel model)
        {
            _transactionLogic.Create(model);
            return Redirect("/transactions");
        }

        [HttpGet("upd")]
        public IActionResult UpdateTransaction(int id)
        {
            return View(_transactionLogic.ReadElement(new TransactionSearchModel { Id = id }));
        }

		[HttpPost("upd")]
		public IActionResult UpdateTransaction(TransactionBindingModel model)
        {
            _transactionLogic.Update(model);
            return Redirect("/transactions");
        }

		[HttpPost("del")]
		public IActionResult DeleteTransaction(int id)
        {
            _transactionLogic.Delete(new TransactionBindingModel { Id = id });
            return Json(new { success = true });
        }
    }
}
