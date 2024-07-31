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
		public async Task<IActionResult> Index(int? account)
		{
			return View(await _transactionLogic.ReadList(!account.HasValue ? null : new TransactionSearchModel { AccountId = account }));
		}

        [HttpGet("cr8")]
        public async Task<IActionResult> CreateTransaction()
        {
			ViewBag.Accounts = await _accountLogic.ReadList(null);
			return View();
        }

		[HttpPost("cr8")]
		public async Task<IActionResult> CreateTransaction(TransactionBindingModel model)
        {
            await _transactionLogic.Create(model);
            return Redirect("/transactions");
        }

        [HttpGet("upd")]
        public async Task<IActionResult> UpdateTransaction(int id)
        {
            return View(await _transactionLogic.ReadElement(new TransactionSearchModel { Id = id }));
        }

		[HttpPost("upd")]
		public async Task<IActionResult> UpdateTransaction(TransactionBindingModel model)
        {
            await _transactionLogic.Update(model);
            return Redirect("/transactions");
        }

		[HttpPost("del")]
		public async Task<IActionResult> DeleteTransaction(int id)
        {
            await _transactionLogic.Delete(new TransactionBindingModel { Id = id });
            return Json(new { success = true });
        }
    }
}
